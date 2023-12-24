using System;
using System.Collections.Generic;
using System.Drawing;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	public enum TextureFormat
	{
		B8G8R8A8 = 0x0,
		R8G8B8A8 = 0x1,
		A8B8G8R8 = 0x2,
		A8R8G8B8 = 0x3,
		B8G8R8 = 0x4,
		R8G8B8 = 0x5,
		A4R4G4B4 = 0x6,
		A1R5G5B5 = 0x7,
		R5G6B5 = 0x8,
		P8 = 0x9,
		Dxt1 = 0xA,
		Dxt2 = 0xB,
		Dxt3 = 0xC,
		Dxt4 = 0xD,
		Dxt5 = 0xE
	}

	public interface ITexture : ICacheable<ITexture>
	{
		public TextureFormat Format { get; }
		public uint Width { get; }
		public uint Height { get; }
		public uint WidthRef { get; }
		public uint HeightRef { get; }
		public uint MipmapCount { get; }
		public Color AverageColor { get; }
		public Color[]? Palette { get; }
		public List<byte[]> AllMipmapsRgba { get; }
		public List<byte[]> AllMipmapsRaw { get; }

		public byte[] GetMipmapRaw(ulong level);
		public byte[] GetMipmapRgba(ulong level);
		public uint GetWidth(ulong level);
		public uint GetHeight(ulong level);
	}

	[Serializable]
	public struct CachedTexture : ITexture
	{
		public TextureFormat Format { get; set; }
		public uint Width { get; set; }
		public uint Height { get; set; }
		public uint WidthRef { get; set; }
		public uint HeightRef { get; set; }
		public uint MipmapCount { get; set; }
		public Color AverageColor { get; set; }
		public Color[]? Palette { get; set; }
		public List<byte[]> AllMipmapsRgba { get; set; }
		public List<byte[]> AllMipmapsRaw { get; set; }

		public ITexture Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}

		public byte[] GetMipmapRaw(ulong level)
		{
			return AllMipmapsRaw[(int)level];
		}

		public byte[] GetMipmapRgba(ulong level)
		{
			return AllMipmapsRaw[(int)level];
		}

		public uint GetWidth(ulong level)
		{
			return Width >> (int)level;
		}

		public uint GetHeight(ulong level)
		{
			return Height >> (int)level;
		}
	}

	public class Texture : ITexture
	{
		private readonly bool _delete = true;
		private readonly UIntPtr _handle;

		public Texture(string path)
		{
			_handle = Native.ZkTexture_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load texture");
		}

		public Texture(Read buf)
		{
			_handle = Native.ZkTexture_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load texture");
		}

		public Texture(Vfs vfs, string name)
		{
			_handle = Native.ZkTexture_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load texture");
		}

		internal Texture(UIntPtr handle)
		{
			_handle = handle;
			_delete = false;
		}

		public TextureFormat Format => Native.ZkTexture_getFormat(_handle);
		public uint Width => Native.ZkTexture_getWidth(_handle);
		public uint Height => Native.ZkTexture_getHeight(_handle);
		public uint WidthRef => Native.ZkTexture_getWidthRef(_handle);
		public uint HeightRef => Native.ZkTexture_getHeightRef(_handle);
		public uint MipmapCount => Native.ZkTexture_getMipmapCount(_handle);

		public Color AverageColor
		{
			get
			{
				var color = Native.ZkTexture_getAverageColor(_handle);
				return Color.FromArgb((int)((color >> 24) & 0xFF), (int)((color >> 16) & 0xFF),
					(int)((color >> 8) & 0xFF),
					(int)(color & 0xFF));
			}
		}

		public Color[]? Palette
		{
			get
			{
				if (Format != TextureFormat.P8) return null;

				var palette = new Color[Native.ZkTexture_getPaletteSize(_handle)];
				var i = 0;

				Native.ZkTexture_enumeratePaletteItems(_handle, (_, c) =>
				{
					palette[i++] = c.ToColor();
					return false;
				}, UIntPtr.Zero);

				return palette;
			}
		}

		public List<byte[]> AllMipmapsRaw
		{
			get
			{
				var mipmaps = new List<byte[]>();

				Native.ZkTexture_enumerateRawMipmaps(_handle, (_0, _1, data, size) =>
				{
					mipmaps.Add(data.MarshalAsArray<byte>(size));
					return false;
				}, UIntPtr.Zero);

				return mipmaps;
			}
		}

		public List<byte[]> AllMipmapsRgba
		{
			get
			{
				var mipmaps = new List<byte[]>();

				Native.ZkTexture_enumerateRgbaMipmaps(_handle, (_0, _1, data, size) =>
				{
					mipmaps.Add(data.MarshalAsArray<byte>(size));
					return false;
				}, UIntPtr.Zero);

				return mipmaps;
			}
		}

		public ITexture Cache()
		{
			return new CachedTexture
			{
				Format = Format,
				Width = Width,
				Height = Height,
				WidthRef = WidthRef,
				HeightRef = HeightRef,
				MipmapCount = MipmapCount,
				AverageColor = AverageColor,
				Palette = Palette,
				AllMipmapsRgba = AllMipmapsRgba,
				AllMipmapsRaw = AllMipmapsRaw
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public byte[] GetMipmapRaw(ulong level)
		{
			return Native.ZkTexture_getMipmapRaw(_handle, level, out var size).MarshalAsArray<byte>(size);
		}

		public byte[] GetMipmapRgba(ulong level)
		{
			var data = new byte[GetWidth(level) * GetHeight(level) * 4];
			Native.ZkTexture_getMipmapRgba(_handle, level, data, (ulong)data.Length);
			return data;
		}

		public uint GetWidth(ulong level)
		{
			return Native.ZkTexture_getWidthMipmap(_handle, level);
		}

		public uint GetHeight(ulong level)
		{
			return Native.ZkTexture_getHeightMipmap(_handle, level);
		}

		~Texture()
		{
			if (_delete) Native.ZkTexture_del(_handle);
		}
	}
}