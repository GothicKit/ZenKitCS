using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{
	/// <summary>
	///     A single font glyph.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct FontGlyph
	{
		/// <summary>
		///     The width of the glyph in pixels.
		/// </summary>
		[MarshalAs(UnmanagedType.U1)] public byte width;

		/// <summary>
		///     <b>The position of the top left corner of the glyph in the font texture.</b>
		///     This value is not stored as absolute pixels but rather in percent of the width and
		///     height of the image. Thus to calculate the real pixel position of the top left corner,
		///     one multiplies `topLeft.x` by the width of the font texture and `topLeft.y` by its height.
		/// </summary>
		/// <seealso href="https://zk.gothickit.dev/library/api/font/#dealing-with-glyphs" />
		public Vector2 topLeft;

		/// <summary>
		///     <b>The position of the bottom right corner of the glyph in the font texture.</b>
		///     This value is not stored as absolute pixels but rather in percent of the width and
		///     height of the image. Thus to calculate the real pixel position of the bottom right corner,
		///     one multiplies `bottomRight.x` by the width of the font texture and `bottomRight.y` by its height.
		/// </summary>
		/// <seealso href="https://zk.gothickit.dev/library/api/font/#dealing-with-glyphs" />
		public Vector2 bottomRight;
	}

	/// <summary>
	///     <b>Represents a ZenGin font file.</b>
	///     Fonts in the <i>ZenGin</i> consist of a font definition file and a font texture. This class represents the former.
	///     Font
	///     definition files contain a set of glyphs which define the extents of a
	///     <a href="https://en.wikipedia.org/wiki/Windows-1252">Windows-1252</a> encoded character within the font texture
	///     file. Font files can be identified most easily by their <c>.FNT</c> extension or alternatively through the
	///     <c>"1\n"</c>
	///     string at the beginning of the file.
	/// </summary>
	/// <seealso href="https://zk.gothickit.dev/library/api/font/" />
	public interface IFont : ICacheable<IFont>
	{
		/// <summary>
		///     The name of this font.
		/// </summary>
		public string Name { get; }

		/// <summary>
		///     The height of each glyph of this font in pixels.
		/// </summary>
		public int Height { get; }

		/// <summary>
		///     <b>All glyphs of this font.</b>
		///     <i>ZenGin</i> fonts contain characters present in the
		///     <a href="https://en.wikipedia.org/wiki/Windows-1252">Windows-1252 character encoding</a>
		///     which is generally used by <i>Gothic</i> and <i>Gothic II</i>. The returned list contains these glyphs in order.
		///     Some glyphs may not actually be present in the font texture. For those glyphs, the stored UV-coordinates will be
		///     invalid in some way (ie. they may point to a pixel not actually in the texture or be negative).
		/// </summary>
		/// <remarks>
		///     Repeated access to this property will lead to poor performance if access to a native ZenKit object is
		///     required. Either cache the value in a variable or use <see cref="GetGlyph" />.
		/// </remarks>
		/// <seealso href="https://zk.gothickit.dev/library/api/font/#dealing-with-glyphs" />
		public List<FontGlyph> Glyphs { get; }

		/// <summary>
		///     The total number of glyphs stored in this font.
		/// </summary>
		public int GlyphCount { get; }

		/// <summary>
		///     Get a single glyph of this font.
		/// </summary>
		/// <param name="index">
		///     The index of the glyph to get. Corresponds to a
		///     <a href="https://en.wikipedia.org/wiki/Windows-1252">Windows-1252</a> code point.
		/// </param>
		/// <returns>The font glyph at the given <paramref name="index" /></returns>
		/// <seealso cref="GlyphCount" />
		/// <seealso cref="Glyphs" />
		/// <seealso href="https://zk.gothickit.dev/library/api/font/#dealing-with-glyphs" />
		public FontGlyph GetGlyph(int index);
	}

	/// <summary>
	///     A ZenKit font object which has been fully loaded into C#. An object of this type is independent from any native
	///     object and incurs none of its disadvantages.
	/// </summary>
	[Serializable]
	public class CachedFont : IFont
	{
		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public int Height { get; set; }

		/// <inheritdoc />
		public List<FontGlyph> Glyphs { get; set; }

		/// <inheritdoc />
		public int GlyphCount => Glyphs.Count;

		/// <inheritdoc />
		public IFont Cache()
		{
			return this;
		}

		/// <inheritdoc />
		public bool IsCached()
		{
			return true;
		}

		/// <inheritdoc />
		public FontGlyph GetGlyph(int index)
		{
			return Glyphs[index];
		}
	}

	public class Font : IFont
	{
		private readonly UIntPtr _handle;

		/// <summary>
		///     Loads a ZenGin font from the given file on disk using ZenKit.
		/// </summary>
		/// <example>
		///     For example:
		///     <code>
		///  		var font = new Font("FONT_OLD_20.FNT");
		///     </code>
		/// </example>
		/// <param name="path">The path of the font file (usually ends in <c>.FNT</c>.</param>
		/// <exception cref="IOException">Thrown if loading the font fails for any reason.</exception>
		public Font(string path)
		{
			_handle = Native.ZkFont_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new IOException("Failed to load font");
		}

		/// <summary>
		///     Loads a ZenGin font from the given <see cref="Read" /> stream.
		/// </summary>
		/// <example>
		///     For example:
		///     <code>
		/// 	var rd = new Read("FONT_OLD_20.FNT");
		/// 	var font = new Font(rd);
		///  </code>
		/// </example>
		/// <param name="r">The stream to read from.</param>
		/// <exception cref="IOException">Thrown if loading the font fails for any reason.</exception>
		public Font(Read r)
		{
			_handle = Native.ZkFont_load(r.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load font");
		}

		/// <summary>
		///     Loads a ZenGin font from the given file on disk using ZenKit.
		/// </summary>
		/// <example>
		///     For example:
		///     <code>
		/// 	var vfs = new Vfs();
		/// 	vfs.MountDisk("Textures.vdf", VfsOverwriteBehavior.Older);
		/// 	var font = new Font(vfs, "FONT_OLD_20.FNT");
		///  </code>
		/// </example>
		/// <param name="vfs"></param>
		/// <param name="name"></param>
		/// <exception cref="IOException">Thrown if loading the font fails for any reason.</exception>
		public Font(Vfs vfs, string name)
		{
			_handle = Native.ZkFont_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load font");
		}

		/// <inheritdoc />
		public string Name => Native.ZkFont_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load font name");

		/// <inheritdoc />
		public int Height => (int)Native.ZkFont_getHeight(_handle);

		/// <inheritdoc />
		public int GlyphCount => (int)Native.ZkFont_getGlyphCount(_handle);

		/// <inheritdoc />
		public List<FontGlyph> Glyphs
		{
			get
			{
				var glyphs = new List<FontGlyph>(GlyphCount);
				var count = GlyphCount;
				for (var i = 0;i < count; ++i) glyphs.Add(GetGlyph(i));
				return glyphs;
			}
		}

		/// <inheritdoc />
		public IFont Cache()
		{
			return new CachedFont
			{
				Name = Name,
				Height = Height,
				Glyphs = Glyphs
			};
		}

		/// <inheritdoc />
		public bool IsCached()
		{
			return false;
		}

		/// <inheritdoc />
		public FontGlyph GetGlyph(int index)
		{
			return Native.ZkFont_getGlyph(_handle, (ulong)index);
		}

		~Font()
		{
			Native.ZkFont_del(_handle);
		}
	}
}