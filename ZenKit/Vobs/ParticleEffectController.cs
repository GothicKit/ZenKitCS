namespace ZenKit.Vobs;

public class ParticleEffectController : VirtualObject
{
	public ParticleEffectController(Read buf, GameVersion version) : base(
		Native.ZkParticleEffectController_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load particle effect controller vob");
	}

	public ParticleEffectController(string path, GameVersion version) : base(
		Native.ZkParticleEffectController_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load particle effect controller vob");
	}

	internal ParticleEffectController(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public string EffectName => Native.ZkParticleEffectController_getEffectName(Handle).MarshalAsString() ??
	                            throw new Exception("Failed to load particle effect controller vob effect name");

	public bool KillWhenDone => Native.ZkParticleEffectController_getKillWhenDone(Handle);
	public bool InitiallyRunning => Native.ZkParticleEffectController_getInitiallyRunning(Handle);


	protected override void Delete()
	{
		Native.ZkParticleEffectController_del(Handle);
	}
}