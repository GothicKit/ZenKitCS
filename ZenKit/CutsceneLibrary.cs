using System;
using System.Collections.Generic;
using System.Linq;
using ZenKit.Util;

namespace ZenKit
{
	public interface ICutsceneMessage : ICacheable<ICutsceneMessage>
	{
		public int Type { get; }
		public string Text { get; }
		public string Name { get; }
	}

	[Serializable]
	public class CachedCutsceneMessage : ICutsceneMessage
	{
		public int Type { get; set; }
		public string Text { get; set; }
		public string Name { get; set; }

		public ICutsceneMessage Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class CutsceneMessage : ICutsceneMessage
	{
		private readonly UIntPtr _handle;

		internal CutsceneMessage(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Type => (int)Native.ZkCutsceneMessage_getType(_handle);

		public string Text => Native.ZkCutsceneMessage_getText(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to get cutscene message text");

		public string Name => Native.ZkCutsceneMessage_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to get cutscene message name");

		public ICutsceneMessage Cache()
		{
			return new CachedCutsceneMessage
			{
				Type = Type,
				Text = Text,
				Name = Name
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}


	public interface ICutsceneBlock : ICacheable<ICutsceneBlock>
	{
		public string Name { get; }
		public ICutsceneMessage Message { get; }
	}

	[Serializable]
	public class CachedCutsceneBlock : ICutsceneBlock
	{
		public string Name { get; set; }
		public ICutsceneMessage Message { get; set; }

		public ICutsceneBlock Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class CutsceneBlock : ICutsceneBlock
	{
		private readonly UIntPtr _handle;

		internal CutsceneBlock(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkCutsceneBlock_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to get cutscene block name");

		public ICutsceneMessage Message
		{
			get
			{
				var handle = Native.ZkCutsceneBlock_getMessage(_handle);
				return handle != UIntPtr.Zero
					? new CutsceneMessage(handle)
					: throw new Exception("Failed to load cutscene block message");
			}
		}

		public ICutsceneBlock Cache()
		{
			return new CachedCutsceneBlock
			{
				Name = Name,
				Message = Message.Cache()
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface ICutsceneLibrary : ICacheable<ICutsceneLibrary>
	{
		public List<ICutsceneBlock> Blocks { get; }
		public ICutsceneBlock? GetBlock(string name);
	}

	[Serializable]
	public struct CachedCutsceneLibrary : ICutsceneLibrary
	{
		public List<ICutsceneBlock> Blocks { get; set; }

		public ICutsceneBlock? GetBlock(string name)
		{
			return Blocks.FirstOrDefault(block => block.Name == name);
		}

		public ICutsceneLibrary Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class CutsceneLibrary : ICutsceneLibrary
	{
		private readonly UIntPtr _handle;

		public CutsceneLibrary(string path)
		{
			_handle = Native.ZkCutsceneLibrary_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load cutscene library");
		}

		public CutsceneLibrary(Read r)
		{
			_handle = Native.ZkCutsceneLibrary_load(r.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load cutscene library");
		}

		public CutsceneLibrary(Vfs vfs, string name)
		{
			_handle = Native.ZkCutsceneLibrary_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load cutscene library");
		}

		public List<ICutsceneBlock> Blocks
		{
			get
			{
				var blocks = new List<ICutsceneBlock>();

				Native.ZkCutsceneLibrary_enumerateBlocks(_handle, (_, block) =>
				{
					blocks.Add(new CutsceneBlock(block));
					return false;
				}, UIntPtr.Zero);

				return blocks;
			}
		}

		public ICutsceneLibrary Cache()
		{
			return new CachedCutsceneLibrary
			{
				Blocks = Blocks.ConvertAll(block => block.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

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