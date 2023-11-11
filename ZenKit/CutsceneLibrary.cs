using System;
using System.Collections.Generic;
using ZenKit.Util;

namespace ZenKit
{
	namespace Materialized
	{
		[Serializable]
		public struct CutsceneMessage
		{
			public uint Type;
			public string Text;
			public string Name;
		}

		[Serializable]
		public struct CutsceneBlock
		{
			public string Name;
			public CutsceneMessage Message;
		}

		[Serializable]
		public struct CutsceneLibrary
		{
			public List<CutsceneBlock> Blocks;
		}
	}

	public class CutsceneMessage : IMaterializing<Materialized.CutsceneMessage>
	{
		private readonly UIntPtr _handle;

		internal CutsceneMessage(UIntPtr handle)
		{
			_handle = handle;
		}

		public uint Type => Native.ZkCutsceneMessage_getType(_handle);

		public string Text => Native.ZkCutsceneMessage_getText(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to get cutscene message text");

		public string Name => Native.ZkCutsceneMessage_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to get cutscene message name");

		public Materialized.CutsceneMessage Materialize()
		{
			return new Materialized.CutsceneMessage
			{
				Type = Type,
				Text = Text,
				Name = Name
			};
		}
	}

	public class CutsceneBlock : IMaterializing<Materialized.CutsceneBlock>
	{
		private readonly UIntPtr _handle;

		internal CutsceneBlock(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkCutsceneBlock_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to get cutscene block name");

		public CutsceneMessage Message
		{
			get
			{
				var handle = Native.ZkCutsceneBlock_getMessage(_handle);
				return handle != UIntPtr.Zero
					? new CutsceneMessage(handle)
					: throw new Exception("Failed to load cutscene block message");
			}
		}

		public Materialized.CutsceneBlock Materialize()
		{
			return new Materialized.CutsceneBlock
			{
				Name = Name,
				Message = Message.Materialize()
			};
		}
	}

	public class CutsceneLibrary : IMaterializing<Materialized.CutsceneLibrary>
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

		public List<CutsceneBlock> Blocks
		{
			get
			{
				var blocks = new List<CutsceneBlock>();

				Native.ZkCutsceneLibrary_enumerateBlocks(_handle, (_, block) =>
				{
					blocks.Add(new CutsceneBlock(block));
					return false;
				}, UIntPtr.Zero);

				return blocks;
			}
		}

		public Materialized.CutsceneLibrary Materialize()
		{
			return new Materialized.CutsceneLibrary
			{
				Blocks = Blocks.ConvertAll(block => block.Materialize())
			};
		}

		~CutsceneLibrary()
		{
			Native.ZkCutsceneLibrary_del(_handle);
		}

		public CutsceneBlock? GetBlock(string name)
		{
			var block = Native.ZkCutsceneLibrary_getBlock(_handle, name);
			return block == UIntPtr.Zero ? null : new CutsceneBlock(block);
		}
	}
}