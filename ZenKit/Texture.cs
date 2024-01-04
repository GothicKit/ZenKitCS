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
		public int Width { get; }
		public int Height { get; }
		public int WidthRef { get; }
		public int HeightRef { get; }
		public int MipmapCount { get; }
		public Color AverageColor { get; }
		public Color[]? Palette { get; }
		public List<byte[]> AllMipmapsRgba { get; }
		public List<byte[]> AllMipmapsRaw { get; }

		public byte[] GetMipmapRaw(int level);
		public byte[] GetMipmapRgba(int level);
		public int GetWidth(int level);
		public int GetHeight(int level);
	}

	[Serializable]
	public struct CachedTexture : ITexture
	{
		public TextureFormat Format { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int WidthRef { get; set; }
		public int HeightRef { get; set; }
		public int MipmapCount { get; set; }
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

		public byte[] GetMipmapRaw(int level)
		{
			return AllMipmapsRaw[level];
		}

		public byte[] GetMipmapRgba(int level)
		{
			return AllMipmapsRaw[level];
		}

		public int GetWidth(int level)
		{
			return Width >> level;
		}

		public int GetHeight(int level)
		{
			return Height >> level;
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
		public int Width => (int)Native.ZkTexture_getWidth(_handle);
		public int Height => (int)Native.ZkTexture_getHeight(_handle);
		public int WidthRef => (int)Native.ZkTexture_getWidthRef(_handle);
		public int HeightRef => (int)Native.ZkTexture_getHeightRef(_handle);
		public int MipmapCount => (int)Native.ZkTexture_getMipmapCount(_handle);

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

				for (var i = 0; i < palette.Length; ++i) palette[i] = Native.ZkTexture_getPaletteItem(_handle, (ulong)i).ToColor();
				return palette;
			}
		}

		public List<byte[]> AllMipmapsRaw
		{
			get
			{
				var mipmaps = new List<byte[]>();
				var count = MipmapCount;
				for (var i = 0;i < count; ++i) mipmaps.Add(GetMipmapRaw(i));
				return mipmaps;
			}
		}

		public List<byte[]> AllMipmapsRgba
		{
			get
			{
				var mipmaps = new List<byte[]>();
				var count = MipmapCount;
				for (var i = 0;i < count; ++i) mipmaps.Add(GetMipmapRgba(i));
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

		public byte[] GetMipmapRaw(int level)
		{
			return Native.ZkTexture_getMipmapRaw(_handle, (ulong)level, out var size).MarshalAsArray<byte>(size);
		}

		public byte[] GetMipmapRgba(int level)
		{
			var data = new byte[GetWidth(level) * GetHeight(level) * 4];
			Native.ZkTexture_getMipmapRgba(_handle, (ulong)level, data, (ulong)data.Length);
			return data;
		}

		public int GetWidth(int level)
		{
			return (int)Native.ZkTexture_getWidthMipmap(_handle, (ulong)level);
		}

		public int GetHeight(int level)
		{
			return (int)Native.ZkTexture_getHeightMipmap(_handle, (ulong)level);
		}

		~Texture()
		{
			if (_delete) Native.ZkTexture_del(_handle);
		}
	}
}