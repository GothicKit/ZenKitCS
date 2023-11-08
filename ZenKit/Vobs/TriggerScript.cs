namespace ZenKit.Vobs;

public class TriggerScript : Trigger
{
	public TriggerScript(Read buf, GameVersion version) : base(Native.ZkTriggerScript_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerScript vob");
	}

	public TriggerScript(string path, GameVersion version) : base(Native.ZkTriggerScript_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerScript vob");
	}

	internal TriggerScript(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public string Function => Native.ZkTriggerScript_getFunction(Handle).MarshalAsString() ?? string.Empty;

	protected override void Delete()
	{
		Native.ZkTriggerScript_del(Handle);
	}
}