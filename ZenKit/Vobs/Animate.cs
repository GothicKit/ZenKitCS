using System;

namespace ZenKit.Vobs
{
	public class Animate : VirtualObject
	{
		public Animate(Read buf, GameVersion version) : base(Native.ZkAnimate_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load animate vob");
		}

		public Animate(string path, GameVersion version) : base(Native.ZkAnimate_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load animate vob");
		}

		internal Animate(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public bool StartOn => Native.ZkAnimate_getStartOn(Handle);

		protected override void Delete()
		{
			Native.ZkAnimate_del(Handle);
		}
	}
}