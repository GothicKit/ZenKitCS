using System;

namespace ZenKit.Vobs
{
	public interface ITriggerUntouch : IVirtualObject
	{
		string Target { get; set; }
	}

	public class TriggerUntouch : VirtualObject, ITriggerUntouch
	{
		public TriggerUntouch() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCTriggerUntouch))
		{
		}

		public TriggerUntouch(Read buf, GameVersion version) : base(Native.ZkTriggerUntouch_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerUntouch vob");
		}

		public TriggerUntouch(string path, GameVersion version) : base(Native.ZkTriggerUntouch_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerUntouch vob");
		}

		internal TriggerUntouch(UIntPtr handle) : base(handle)
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
