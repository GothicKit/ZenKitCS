using System.Reflection.Metadata;

namespace ZenKit;

public class CutsceneMessage
{
	private readonly UIntPtr _handle;

	internal CutsceneMessage(UIntPtr handle) => _handle = handle;

	public uint Type => Native.ZkCutsceneMessage_getType(_handle);

	public string Text => Native.ZkCutsceneMessage_getText(_handle).MarshalAsString() ??
	                      throw new Exception("Failed to get cutscene message text");
	
	public string Name => Native.ZkCutsceneMessage_getName(_handle).MarshalAsString() ??
	                      throw new Exception("Failed to get cutscene message name");
}

public class CutsceneBlock
{
	private readonly UIntPtr _handle;

	internal CutsceneBlock(UIntPtr handle) => _handle = handle;

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
}

public class CutsceneLibrary
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

	~CutsceneLibrary() => Native.ZkCutsceneLibrary_del(_handle);

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

	public CutsceneBlock? GetBlock(string name)
	{
		var block = Native.ZkCutsceneLibrary_getBlock(_handle, name);
		return block == UIntPtr.Zero ? null : new CutsceneBlock(block);
	}
}