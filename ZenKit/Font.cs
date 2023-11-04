using System.Numerics;
using System.Runtime.InteropServices;

namespace ZenKit;

[StructLayout(LayoutKind.Sequential)]
public struct FontGlyph
{
	[MarshalAs(UnmanagedType.U1)] public byte width;
	public Vector2 topLeft;
	public Vector2 bottomRight;
}

public class Font
{
	private readonly UIntPtr _handle;

	public Font(string path)
	{
		_handle = Native.ZkFont_loadPath(path);
		if (_handle == UIntPtr.Zero) throw new Exception("Failed to load font");
	}

	public Font(Read r)
	{
		_handle = Native.ZkFont_load(r.Handle);
		if (_handle == UIntPtr.Zero) throw new Exception("Failed to load font");
	}

	public Font(Vfs vfs, string name)
	{
		_handle = Native.ZkFont_loadVfs(vfs.Handle, name);
		if (_handle == UIntPtr.Zero) throw new Exception("Failed to load font");
	}

	public string Name => Native.ZkFont_getName(_handle).MarshalAsString() ??
	                      throw new Exception("Failed to load font name");

	public uint Height => Native.ZkFont_getHeight(_handle);

	public ulong GlyphCount => Native.ZkFont_getGlyphCount(_handle);

	public List<FontGlyph> Glyphs
	{
		get
		{
			var glyphs = new List<FontGlyph>(256);

			Native.ZkFont_enumerateGlyphs(_handle, (_, glyph) =>
			{
				glyphs.Add(Marshal.PtrToStructure<FontGlyph>(glyph));
				return false;
			}, UIntPtr.Zero);

			return glyphs;
		}
	}

	~Font()
	{
		Native.ZkFont_del(_handle);
	}

	public FontGlyph GetGlyph(uint index)
	{
		return Native.ZkFont_getGlyph(_handle, index);
	}
}