using System;

namespace ZenKit.Vobs
{
	public class ParticleEffectController : VirtualObject
	{
		public ParticleEffectController() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCPFXController))
		{
		}

		public ParticleEffectController(Read buf, GameVersion version) : base(
			Native.ZkParticleEffectController_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load particle effect controller vob");
		}

		public ParticleEffectController(string path, GameVersion version) : base(
			Native.ZkParticleEffectController_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load particle effect controller vob");
		}

		internal ParticleEffectController(UIntPtr handle) : base(handle)
		{
		}

		public string EffectName
		{
			get => Native.ZkParticleEffectController_getEffectName(Handle).MarshalAsString() ??
			       throw new Exception("Failed to load particle effect controller vob effect name");
			set => Native.ZkParticleEffectController_setEffectName(Handle, value);
		}

		public bool KillWhenDone
		{
			get => Native.ZkParticleEffectController_getKillWhenDone(Handle);
			set => Native.ZkParticleEffectController_setKillWhenDone(Handle, value);
		}

		public bool InitiallyRunning
		{
			get => Native.ZkParticleEffectController_getInitiallyRunning(Handle);
			set => Native.ZkParticleEffectController_setInitiallyRunning(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkParticleEffectController_del(Handle);
		}
	}
}