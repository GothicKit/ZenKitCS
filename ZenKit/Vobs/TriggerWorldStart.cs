using System;

namespace ZenKit.Vobs
{
	public class TriggerWorldStart : VirtualObject
	{
		public TriggerWorldStart() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCTriggerWorldStart))
		{
		}

		public TriggerWorldStart(Read buf, GameVersion version) : base(
			Native.ZkTriggerWorldStart_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerWorldStart vob");
		}

		public TriggerWorldStart(string path, GameVersion version) : base(
			Native.ZkTriggerWorldStart_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerWorldStart vob");
		}

		internal TriggerWorldStart(UIntPtr handle) : base(handle)
		{
		}

		public string Target
		{
			get => Native.ZkTriggerWorldStart_getTarget(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkTriggerWorldStart_setTarget(Handle, value);
		}

		public bool FireOnce
		{
			get => Native.ZkTriggerWorldStart_getFireOnce(Handle);
			set => Native.ZkTriggerWorldStart_setFireOnce(Handle, value);
		}

		public bool HasFired
		{
			get => Native.ZkTriggerWorldStart_getHasFired(Handle);
			set => Native.ZkTriggerWorldStart_setHasFired(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkTriggerWorldStart_del(Handle);
		}
	}
}