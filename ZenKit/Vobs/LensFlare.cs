namespace ZenKit.Vobs;

public class LensFlare : VirtualObject
{
	public LensFlare(Read buf, GameVersion version) : base(Native.ZkLensFlare_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load lens flare vob");
	}

	public LensFlare(string path, GameVersion version) : base(Native.ZkLensFlare_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load lens flare vob");
	}

	internal LensFlare(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public string Effect => Native.ZkLensFlare_getEffect(Handle).MarshalAsString() ??
	                        throw new Exception("Failed to load lens flare vob effect");

	protected override void Delete()
	{
		Native.ZkLensFlare_del(Handle);
	}
}