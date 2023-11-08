namespace ZenKit.Vobs;

public class Container : InteractiveObject
{
	public Container(Read buf, GameVersion version) : base(Native.ZkContainer_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Container vob");
	}

	public Container(string path, GameVersion version) : base(Native.ZkContainer_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Container vob");
	}

	internal Container(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public bool IsLocked => Native.ZkContainer_getIsLocked(Handle);
	public string Key => Native.ZkContainer_getKey(Handle).MarshalAsString() ?? string.Empty;
	public string PickString => Native.ZkContainer_getPickString(Handle).MarshalAsString() ?? string.Empty;
	public string Contents => Native.ZkContainer_getContents(Handle).MarshalAsString() ?? string.Empty;

	protected override void Delete()
	{
		Native.ZkContainer_del(Handle);
	}
}