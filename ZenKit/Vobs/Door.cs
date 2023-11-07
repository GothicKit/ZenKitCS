namespace ZenKit.Vobs;

public class Door : InteractiveObject
{
	public Door(Read buf, GameVersion version) : base(Native.ZkDoor_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Door vob");
	}

	public Door(string path, GameVersion version) : base(Native.ZkDoor_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Door vob");
	}

	internal Door(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public bool IsLocked => Native.ZkDoor_getIsLocked(Handle);
	public string Key => Native.ZkDoor_getKey(Handle).MarshalAsString() ?? string.Empty;
	public string PickString => Native.ZkDoor_getPickString(Handle).MarshalAsString() ?? string.Empty;
	
	protected override void Delete()
	{
		Native.ZkDoor_del(Handle);
	}
}