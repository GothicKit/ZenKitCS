using System.Drawing;

namespace ZenKit;

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

public class Texture
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
			return Color.FromArgb((int)((color >> 24) & 0xFF), (int)((color >> 16) & 0xFF), (int)((color >> 8) & 0xFF),
				(int)(color & 0xFF));
		}
	}

	public Color[]? Palette
	{
		get
		{
			if (Format != TextureFormat.P8) return null;

			var palette = Native.ZkTexture_getPalette(_handle, out var size);
			var argb = palette.MarshalAsArray<Native.ZkColorArgb>(size);

			var colors = new Color[argb.Length];
			for (var i = 0; i < argb.Length; i++) colors[i] = argb[i].ToColor();

			return colors;
		}
	}

	public List<byte[]> AllMipmapsRaw
	{
		get
		{
			var mipmaps = new List<byte[]>();

			Native.ZkTexture_enumerateRawMipmaps(_handle, (_, _, data, size) =>
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

			Native.ZkTexture_enumerateRgbaMipmaps(_handle, (_, _, data, size) =>
			{
				mipmaps.Add(data.MarshalAsArray<byte>(size));
				return false;
			}, UIntPtr.Zero);

			return mipmaps;
		}
	}

	~Texture()
	{
		if (_delete) Native.ZkTexture_del(_handle);
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
}