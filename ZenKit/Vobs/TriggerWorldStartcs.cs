using System;

namespace ZenKit.Vobs
{
	public class TriggerWorldStart : VirtualObject
	{
		public TriggerWorldStart(Read buf, GameVersion version) : base(
			Native.ZkTriggerWorldStart_load(buf.Handle, version),
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

		protected override void Delete()
		{
			Native.ZkTriggerWorldStart_del(Handle);
		}
	}
}