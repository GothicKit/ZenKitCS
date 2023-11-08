namespace ZenKit.Vobs;

public class TriggerWorldStart : VirtualObject
{
	public TriggerWorldStart(Read buf, GameVersion version) : base(Native.ZkTriggerWorldStart_load(buf.Handle, version),
		true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerWorldStart vob");
	}

	public TriggerWorldStart(string path, GameVersion version) : base(
		Native.ZkTriggerWorldStart_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerWorldStart vob");
	}

	internal TriggerWorldStart(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public string Target => Native.ZkTriggerWorldStart_getTarget(Handle).MarshalAsString() ?? string.Empty;
	public bool FireOnce => Native.ZkTriggerWorldStart_getFireOnce(Handle);


	protected override void Delete()
	{
		Native.ZkTriggerWorldStart_del(Handle);
	}
}