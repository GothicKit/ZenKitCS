namespace ZenKit.Vobs;

public enum MoverMessageType
{
	FixedDirect = 0,
	FixedOrder = 1,
	Next = 2,
	Previous = 3
}

public class MoverController : VirtualObject
{
	public MoverController(Read buf, GameVersion version) : base(Native.ZkMoverController_load(buf.Handle, version),
		true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load mover controller vob");
	}

	public MoverController(string path, GameVersion version) : base(Native.ZkMoverController_loadPath(path, version),
		true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load mover controller vob");
	}

	internal MoverController(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public string Target => Native.ZkMoverController_getTarget(Handle).MarshalAsString() ??
	                        throw new Exception("Failed to load mover controller target");

	public MoverMessageType Message => Native.ZkMoverController_getMessage(Handle);
	public int Key => Native.ZkMoverController_getKey(Handle);

	protected override void Delete()
	{
		Native.ZkMoverController_del(Handle);
	}
}