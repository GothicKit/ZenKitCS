using System;

namespace ZenKit.Vobs
{
	public class Animate : VirtualObject
	{
		public Animate(Read buf, GameVersion version) : base(Native.ZkAnimate_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load animate vob");
		}

		public Animate(string path, GameVersion version) : base(Native.ZkAnimate_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load animate vob");
		}

		internal Animate(UIntPtr handle) : base(handle)
		{
		}

		public bool StartOn
		{
			get => Native.ZkAnimate_getStartOn(Handle);
			set => Native.ZkAnimate_setStartOn(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkAnimate_del(Handle);
		}
	}
}