using System;

namespace ZenKit.Vobs
{
	public class ZoneFarPlane : VirtualObject
	{
		public ZoneFarPlane(Read buf, GameVersion version) : base(Native.ZkZoneFarPlane_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneFarPlane vob");
		}

		public ZoneFarPlane(string path, GameVersion version) : base(Native.ZkZoneFarPlane_loadPath(path, version),
			true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneFarPlane vob");
		}

		internal ZoneFarPlane(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public float VobFarPlaneZ => Native.ZkZoneFarPlane_getVobFarPlaneZ(Handle);
		public float InnerRangePercentage => Native.ZkZoneFarPlane_getInnerRangePercentage(Handle);

		protected override void Delete()
		{
			Native.ZkZoneFarPlane_del(Handle);
		}
	}
	
	public class ZoneFarPlaneDefault : ZoneFarPlane
	{
		public ZoneFarPlaneDefault(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public ZoneFarPlaneDefault(string path, GameVersion version) : base(path, version)
		{
		}

		internal ZoneFarPlaneDefault(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
}