using System;

namespace ZenKit.Vobs
{
	public class Trigger : VirtualObject
	{
		public Trigger(Read buf, GameVersion version) : base(Native.ZkTrigger_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Trigger vob");
		}

		public Trigger(string path, GameVersion version) : base(Native.ZkTrigger_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Trigger vob");
		}

		internal Trigger(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public string Target => Native.ZkTrigger_getTarget(Handle).MarshalAsString() ?? string.Empty;
		public byte Flags => Native.ZkTrigger_getFlags(Handle);
		public byte FilterFlags => Native.ZkTrigger_getFilterFlags(Handle);
		public string VobTarget => Native.ZkTrigger_getVobTarget(Handle).MarshalAsString() ?? string.Empty;
		public int MaxActivationCount => Native.ZkTrigger_getMaxActivationCount(Handle);
		public TimeSpan RetriggerDelay => TimeSpan.FromSeconds(Native.ZkTrigger_getRetriggerDelaySeconds(Handle));
		public float DamageThreshold => Native.ZkTrigger_getDamageThreshold(Handle);
		public TimeSpan FireDelay => TimeSpan.FromSeconds(Native.ZkTrigger_getFireDelaySeconds(Handle));


		protected override void Delete()
		{
			Native.ZkTrigger_del(Handle);
		}
	}
}