using System;

namespace ZenKit.Vobs
{
	public class TriggerUntouch : VirtualObject
	{
		public TriggerUntouch(Read buf, GameVersion version) : base(Native.ZkTriggerUntouch_load(buf.Handle, version),
			true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerUntouch vob");
		}

		public TriggerUntouch(string path, GameVersion version) : base(Native.ZkTriggerUntouch_loadPath(path, version),
			true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerUntouch vob");
		}

		internal TriggerUntouch(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public string Target
		{
			get => Native.ZkTriggerUntouch_getTarget(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkTriggerUntouch_setTarget(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkTriggerUntouch_del(Handle);
		}
	}
}