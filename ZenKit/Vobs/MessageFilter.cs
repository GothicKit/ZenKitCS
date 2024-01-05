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
		public MessageFilter(Read buf, GameVersion version) : base(Native.ZkMessageFilter_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load message filter vob");
		}

		public MessageFilter(string path, GameVersion version) : base(Native.ZkMessageFilter_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load message filter vob");
		}

		internal MessageFilter(UIntPtr handle) : base(handle)
		{
		}

		public string Target
		{
			get => Native.ZkMessageFilter_getTarget(Handle).MarshalAsString() ??
			       throw new Exception("Failed to load message filter vob target");
			set => Native.ZkMessageFilter_setTarget(Handle, value);
		}


		public MessageFilterAction OnTrigger
		{
			get => Native.ZkMessageFilter_getOnTrigger(Handle);
			set => Native.ZkMessageFilter_setOnTrigger(Handle, value);
		}

		public MessageFilterAction OnUntrigger
		{
			get => Native.ZkMessageFilter_getOnUntrigger(Handle);
			set => Native.ZkMessageFilter_setOnUntrigger(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkMessageFilter_del(Handle);
		}
	}
}