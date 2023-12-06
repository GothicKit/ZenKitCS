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

		public string Target
		{
			get => Native.ZkTrigger_getTarget(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkTrigger_setTarget(Handle, value);
		}

		public byte Flags
		{
			get => Native.ZkTrigger_getFlags(Handle);
			set => Native.ZkTrigger_setFlags(Handle, value);
		}

		public byte FilterFlags
		{
			get => Native.ZkTrigger_getFilterFlags(Handle);
			set => Native.ZkTrigger_setFilterFlags(Handle, value);
		}

		public string VobTarget
		{
			get => Native.ZkTrigger_getVobTarget(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkTrigger_setVobTarget(Handle, value);
		}

		public int MaxActivationCount
		{
			get => Native.ZkTrigger_getMaxActivationCount(Handle);
			set => Native.ZkTrigger_setMaxActivationCount(Handle, value);
		}

		public TimeSpan RetriggerDelay
		{
			get => TimeSpan.FromSeconds(Native.ZkTrigger_getRetriggerDelaySeconds(Handle));
			set => Native.ZkTrigger_setRetriggerDelaySeconds(Handle, (float)value.TotalSeconds);
		}

		public float DamageThreshold
		{
			get => Native.ZkTrigger_getDamageThreshold(Handle);
			set => Native.ZkTrigger_setDamageThreshold(Handle, value);
		}

		public TimeSpan FireDelay
		{
			get => TimeSpan.FromSeconds(Native.ZkTrigger_getFireDelaySeconds(Handle));
			set => Native.ZkTrigger_setFireDelaySeconds(Handle, (float)value.TotalSeconds);
		}

		protected override void Delete()
		{
			Native.ZkTrigger_del(Handle);
		}
	}

	public class CutsceneTrigger : Trigger
	{
		public CutsceneTrigger(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public CutsceneTrigger(string path, GameVersion version) : base(path, version)
		{
		}

		internal CutsceneTrigger(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
}