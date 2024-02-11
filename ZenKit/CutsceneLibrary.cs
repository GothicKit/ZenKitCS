using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZenKit.Util;

namespace ZenKit
{
	/// <summary>A single cutscene message.</summary>
	public interface ICutsceneMessage : ICacheable<ICutsceneMessage>
	{
		public int Type { get; }

		/// <summary>The text associated with the message.</summary>
		/// <remarks>If the message cannot be loaded from the native interface for any reason an empty text will be returned.</remarks>
		public string Text { get; }

		/// <summary>The name of the <tt>WAV</tt> file containing the message's audio. If the message</summary>
		/// <remarks>If the message cannot be loaded from the native interface for any reason an empty text will be returned.</remarks>
		public string Name { get; }
	}

	/// <summary>A cached <see cref="CutsceneMessage"/>.</summary>
	[Serializable]
	public class CachedCutsceneMessage : ICutsceneMessage
	{
		/// <inheritdoc />
		public int Type { get; set; }

		/// <inheritdoc />
		public string Text { get; set; } = "";

		/// <inheritdoc />
		public string Name { get; set; } = "";

		/// <inheritdoc />
		public ICutsceneMessage Cache()
		{
			return this;
		}

		/// <inheritdoc />
		public bool IsCached()
		{
			return true;
		}
	}

	/// <inheritdoc />
	public class CutsceneMessage : ICutsceneMessage
	{
		private readonly UIntPtr _handle;

		internal CutsceneMessage(UIntPtr handle)
		{
			_handle = handle;
		}

		/// <inheritdoc />
		public int Type => (int)Native.ZkCutsceneMessage_getType(_handle);

		/// <inheritdoc />
		public string Text => Native.ZkCutsceneMessage_getText(_handle).MarshalAsString();

		/// <inheritdoc />
		public string Name => Native.ZkCutsceneMessage_getName(_handle).MarshalAsString();

		/// <inheritdoc />
		public ICutsceneMessage Cache()
		{
			return new CachedCutsceneMessage
			{
				Type = Type,
				Text = Text,
				Name = Name
			};
		}

		/// <inheritdoc />
		public bool IsCached()
		{
			return false;
		}
	}

	/// <summary>A block containing a cutscene message and a unique name.</summary>
	public interface ICutsceneBlock : ICacheable<ICutsceneBlock>
	{
		/// <summary>The unique name of the message</summary>
		public string Name { get; }

		/// <summary>The content of the message.</summary>
		/// <remarks>
		/// It seems like it was at one point possible to specify multiple <see cref="CutsceneMessage" />
		/// objects for each <see cref="CutsceneBlock" />. This seems to have been abandoned, however,
		/// so this implementation only supports one <see cref="CutsceneMessage" /> per message block.
		/// </remarks>
		public ICutsceneMessage Message { get; }
	}

	/// <summary>A cached <see cref="CutsceneBlock"/>.</summary>
	[Serializable]
	public class CachedCutsceneBlock : ICutsceneBlock
	{
		/// <inheritdoc />
		public string Name { get; set; } = "";

		/// <inheritdoc />
		public ICutsceneMessage Message { get; set; }

		/// <inheritdoc />
		public ICutsceneBlock Cache()
		{
			return this;
		}

		/// <inheritdoc />
		public bool IsCached()
		{
			return true;
		}
	}

	/// <inheritdoc />
	public class CutsceneBlock : ICutsceneBlock
	{
		private readonly UIntPtr _handle;

		internal CutsceneBlock(UIntPtr handle)
		{
			_handle = handle;
		}

		/// <inheritdoc />
		public string Name => Native.ZkCutsceneBlock_getName(_handle).MarshalAsString();

		/// <inheritdoc />
		/// <exception cref="NativeAccessError">The native backend failed to return a valid value.</exception>
		public ICutsceneMessage Message
		{
			get
			{
				var handle = Native.ZkCutsceneBlock_getMessage(_handle);
				if (handle == UIntPtr.Zero) throw new NativeAccessError("CutsceneBlock.Message");
				return new CutsceneMessage(handle);
			}
		}

		/// <inheritdoc />
		public ICutsceneBlock Cache()
		{
			return new CachedCutsceneBlock
			{
				Name = Name,
				Message = Message.Cache()
			};
		}

		/// <inheritdoc />
		public bool IsCached()
		{
			return false;
		}
	}

	/// <summary>
	/// Represents a cutscene library.
	/// <para>
	/// Cutscene libraries, also called message databases, contain voice lines and a reference to the associated
	/// audio recording of voice actors. These files are used in conjunction with scripts to facilitate PC to NPC
	/// conversations in-game. Cutscene libraries are found within the <c>_work/data/scripts/content/cutscene/</c>
	/// directory of Gothic and Gothic II installations. Cutscene libraries are ZenGin archives in either binary
	/// or ASCII format. They have either the <c>.DAT</c> or <c>.BIN</c> extension for binary files or the
	/// <c>.CSL</c> or <c>.LSC</c> extension for text files.
	/// </para>
	/// </summary>
	public interface ICutsceneLibrary : ICacheable<ICutsceneLibrary>
	{
		/// <summary>A list of all message blocks in the database.</summary>
		public List<ICutsceneBlock> Blocks { get; }

		/// <summary>Retrieves a message block by it's name.</summary>
		/// <param name="name">The name of the block to get</param>
		/// <returns>A pointer to the block or <c>null</c> if the block was not found.</returns>
		public ICutsceneBlock? GetBlock(string name);
	}

	/// <summary>A cached <see cref="CutsceneLibrary"/>.</summary>
	[Serializable]
	public struct CachedCutsceneLibrary : ICutsceneLibrary
	{
		/// <inheritdoc />
		public List<ICutsceneBlock> Blocks { get; set; }

		/// <inheritdoc />
		public ICutsceneBlock? GetBlock(string name)
		{
			return Blocks.FirstOrDefault(block => block.Name == name);
		}

		/// <inheritdoc />
		public ICutsceneLibrary Cache()
		{
			return this;
		}

		/// <inheritdoc />
		public bool IsCached()
		{
			return true;
		}
	}

	/// <inheritdoc />
	public class CutsceneLibrary : ICutsceneLibrary
	{
		private readonly UIntPtr _handle;

		/// <summary>Load a cutscene library from a file.</summary>
		/// <param name="path">The path to the file to load.</param>
		/// <exception cref="IOException">The file loading/parsing failed.</exception>
		public CutsceneLibrary(string path)
		{
			_handle = Native.ZkCutsceneLibrary_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new IOException("Failed to load cutscene library");
		}

		/// <summary>Load a cutscene library from an input stream.</summary>
		/// <param name="r">The stream to read from..</param>
		/// <exception cref="IOException">The file loading/parsing failed.</exception>
		public CutsceneLibrary(Read r)
		{
			_handle = Native.ZkCutsceneLibrary_load(r.Handle);
			if (_handle == UIntPtr.Zero) throw new IOException("Failed to load cutscene library");
		}

		/// <summary>Load a cutscene library from a VFS.</summary>
		/// <param name="vfs">VFS to load the file from.</param>
		/// <param name="name">The name of the file to load from the given <paramref name="vfs"/>.</param>
		/// <exception cref="IOException">The file loading/parsing failed.</exception>
		public CutsceneLibrary(Vfs vfs, string name)
		{
			_handle = Native.ZkCutsceneLibrary_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new IOException("Failed to load cutscene library");
		}

		/// <inheritdoc />
		public List<ICutsceneBlock> Blocks
		{
			get
			{
				var blocks = new List<ICutsceneBlock>();
				var count = (int)Native.ZkCutsceneLibrary_getBlockCount(_handle);
				for (var i = 0; i < count; ++i)
					blocks.Add(new CutsceneBlock(Native.ZkCutsceneLibrary_getBlockByIndex(_handle, (ulong)i)));
				return blocks;
			}
		}

		/// <inheritdoc />
		public ICutsceneLibrary Cache()
		{
			return new CachedCutsceneLibrary
			{
				Blocks = Blocks.ConvertAll(block => block.Cache())
			};
		}

		/// <inheritdoc />
		public bool IsCached()
		{
			return false;
		}

		/// <inheritdoc />
		public ICutsceneBlock? GetBlock(string name)
		{
			var block = Native.ZkCutsceneLibrary_getBlock(_handle, name);
			return block == UIntPtr.Zero ? null : new CutsceneBlock(block);
		}

		~CutsceneLibrary()
		{
			Native.ZkCutsceneLibrary_del(_handle);
		}
	}
}