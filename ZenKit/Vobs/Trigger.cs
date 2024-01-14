using System;

namespace ZenKit.Vobs
{
	public class Trigger : VirtualObject
	{
		public Trigger() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCTrigger))
		{
		}

		public Trigger(Read buf, GameVersion version) : base(Native.ZkTrigger_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Trigger vob");
		}

		public Trigger(string path, GameVersion version) : base(Native.ZkTrigger_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Trigger vob");
		}

		internal Trigger(UIntPtr handle) : base(handle)
		{
		}

		public string Target
		{
			get => Native.ZkTrigger_getTarget(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkTrigger_setTarget(Handle, value);
		}

		public bool StartEnabled
		{
			get => Native.ZkTrigger_getStartEnabled(Handle);
			set => Native.ZkTrigger_setStartEnabled(Handle, value);
		}

		public bool SendUntrigger
		{
			get => Native.ZkTrigger_getSendUntrigger(Handle);
			set => Native.ZkTrigger_setSendUntrigger(Handle, value);
		}

		public bool ReactToOnTrigger
		{
			get => Native.ZkTrigger_getReactToOnTrigger(Handle);
			set => Native.ZkTrigger_setReactToOnTrigger(Handle, value);
		}

		public bool ReactToOnTouch
		{
			get => Native.ZkTrigger_getReactToOnTouch(Handle);
			set => Native.ZkTrigger_setReactToOnTouch(Handle, value);
		}

		public bool ReactToOnDamage
		{
			get => Native.ZkTrigger_getReactToOnDamage(Handle);
			set => Native.ZkTrigger_setReactToOnDamage(Handle, value);
		}

		public bool RespondToObject
		{
			get => Native.ZkTrigger_getRespondToObject(Handle);
			set => Native.ZkTrigger_setRespondToObject(Handle, value);
		}

		public bool RespondToPC
		{
			get => Native.ZkTrigger_getRespondToPC(Handle);
			set => Native.ZkTrigger_setRespondToPC(Handle, value);
		}

		public bool RespondToNPC
		{
			get => Native.ZkTrigger_getRespondToNPC(Handle);
			set => Native.ZkTrigger_setRespondToNPC(Handle, value);
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

		public float NextTimeTriggerable
		{
			get => Native.ZkTrigger_getNextTimeTriggerable(Handle);
			set => Native.ZkTrigger_setNextTimeTriggerable(Handle, value);
		}

		public VirtualObject? OtherVob
		{
			get
			{
				var val = Native.ZkTrigger_getOtherVob(Handle);
				return VirtualObject.FromNative(Native.ZkObject_takeRef(val));
			}
			set => Native.ZkTrigger_setOtherVob(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public int CountCanBeActivated
		{
			get => Native.ZkTrigger_getCountCanBeActivated(Handle);
			set => Native.ZkTrigger_setCountCanBeActivated(Handle, value);
		}

		public bool IsEnabled
		{
			get => Native.ZkTrigger_getIsEnabled(Handle);
			set => Native.ZkTrigger_setIsEnabled(Handle, value);
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

		internal CutsceneTrigger(UIntPtr handle) : base(handle)
		{
		}
	}
}