using System;

namespace ZenKit.Vobs
{
	public enum MoverMessageType
	{
		FixedDirect = 0,
		FixedOrder = 1,
		Next = 2,
		Previous = 3
	}

	public class MoverController : VirtualObject
	{
		public MoverController() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCMoverController))
		{
		}

		public MoverController(Read buf, GameVersion version) : base(Native.ZkMoverController_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load mover controller vob");
		}

		public MoverController(string path, GameVersion version) : base(
			Native.ZkMoverController_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load mover controller vob");
		}

		internal MoverController(UIntPtr handle) : base(handle)
		{
		}

		public string Target
		{
			get => Native.ZkMoverController_getTarget(Handle).MarshalAsString() ??
			       throw new Exception("Failed to load mover controller target");
			set => Native.ZkMoverController_setTarget(Handle, value);
		}


		public MoverMessageType Message
		{
			get => Native.ZkMoverController_getMessage(Handle);
			set => Native.ZkMoverController_setMessage(Handle, value);
		}

		public int Key
		{
			get => Native.ZkMoverController_getKey(Handle);
			set => Native.ZkMoverController_setKey(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkMoverController_del(Handle);
		}
	}
}