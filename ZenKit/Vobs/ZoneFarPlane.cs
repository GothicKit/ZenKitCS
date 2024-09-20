using System;

namespace ZenKit.Vobs
{
	public interface IZoneFarPlane : IVirtualObject
	{
		float VobFarPlaneZ { get; set; }
		float InnerRangePercentage { get; set; }
	}

	public class ZoneFarPlane : VirtualObject, IZoneFarPlane
	{
		public ZoneFarPlane() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCZoneVobFarPlane))
		{
		}

		public ZoneFarPlane(Read buf, GameVersion version) : base(Native.ZkZoneFarPlane_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneFarPlane vob");
		}

		public ZoneFarPlane(string path, GameVersion version) : base(Native.ZkZoneFarPlane_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneFarPlane vob");
		}

		internal ZoneFarPlane(UIntPtr handle) : base(handle)
		{
		}

		public float VobFarPlaneZ
		{
			get => Native.ZkZoneFarPlane_getVobFarPlaneZ(Handle);
			set => Native.ZkZoneFarPlane_setVobFarPlaneZ(Handle, value);
		}

		public float InnerRangePercentage
		{
			get => Native.ZkZoneFarPlane_getInnerRangePercentage(Handle);
			set => Native.ZkZoneFarPlane_setInnerRangePercentage(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkZoneFarPlane_del(Handle);
		}
	}

	public interface IZoneFarPlaneDefault : IZoneFarPlane
	{
	}

	public class ZoneFarPlaneDefault : ZoneFarPlane, IZoneFarPlaneDefault
	{
		public ZoneFarPlaneDefault(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public ZoneFarPlaneDefault(string path, GameVersion version) : base(path, version)
		{
		}

		internal ZoneFarPlaneDefault(UIntPtr handle) : base(handle)
		{
		}
	}
}
