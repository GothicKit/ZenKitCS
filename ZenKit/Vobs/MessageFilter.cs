using System;

namespace ZenKit.Vobs
{
	public enum MessageFilterAction
	{
		None = 0,
		Trigger = 1,
		Untrigger = 2,
		Enable = 3,
		Disable = 4,
		Toggle = 5
	}

	public class MessageFilter : VirtualObject
	{
		public MessageFilter(Read buf, GameVersion version) : base(Native.ZkMessageFilter_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load message filter vob");
		}

		public MessageFilter(string path, GameVersion version) : base(Native.ZkMessageFilter_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load message filter vob");
		}

		internal MessageFilter(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public string Target => Native.ZkMessageFilter_getTarget(Handle).MarshalAsString() ??
		                        throw new Exception("Failed to load message filter vob target");

		public MessageFilterAction OnTrigger => Native.ZkMessageFilter_getOnTrigger(Handle);
		public MessageFilterAction OnUntrigger => Native.ZkMessageFilter_getOnUntrigger(Handle);

		protected override void Delete()
		{
			Native.ZkMessageFilter_del(Handle);
		}
	}
}