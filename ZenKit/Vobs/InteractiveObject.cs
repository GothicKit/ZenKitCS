namespace ZenKit.Vobs;

public class InteractiveObject : MovableObject
{
	public InteractiveObject(Read buf, GameVersion version) : base(Native.ZkInteractiveObject_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load InteractiveObject vob");
	}

	public InteractiveObject(string path, GameVersion version) : base(Native.ZkInteractiveObject_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load InteractiveObject vob");
	}

	internal InteractiveObject(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}
	
	public int State => Native.ZkInteractiveObject_getState(Handle);
	public string Target => Native.ZkInteractiveObject_getTarget(Handle).MarshalAsString() ?? String.Empty;
	public string Item => Native.ZkInteractiveObject_getItem(Handle).MarshalAsString() ?? String.Empty;
	public string ConditionFunction => Native.ZkInteractiveObject_getConditionFunction(Handle).MarshalAsString() ?? String.Empty;
	public string OnStateChangeFunction => Native.ZkInteractiveObject_getOnStateChangeFunction(Handle).MarshalAsString() ?? String.Empty;
	public bool Rewind => Native.ZkInteractiveObject_getRewind(Handle);

	protected override void Delete()
	{
		Native.ZkInteractiveObject_del(Handle);
	}
}