using System;
using System.Drawing;

namespace ZenKit.Vobs
{
	public class ZoneFog : VirtualObject
	{
		public ZoneFog() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCZoneZFog))
		{
		}

		public ZoneFog(Read buf, GameVersion version) : base(Native.ZkZoneFog_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneFog vob");
		}

		public ZoneFog(string path, GameVersion version) : base(Native.ZkZoneFog_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneFog vob");
		}

		internal ZoneFog(UIntPtr handle) : base(handle)
		{
		}

		public float RangeCenter
		{
			get => Native.ZkZoneFog_getRangeCenter(Handle);
			set => Native.ZkZoneFog_setRangeCenter(Handle, value);
		}

		public float InnerRangePercentage
		{
			get => Native.ZkZoneFog_getInnerRangePercentage(Handle);
			set => Native.ZkZoneFog_setInnerRangePercentage(Handle, value);
		}

		public Color Color
		{
			get => Native.ZkZoneFog_getColor(Handle).ToColor();
			set => Native.ZkZoneFog_setColor(Handle, new Native.Structs.ZkColor(value));
		}

		public bool FadeOutSky
		{
			get => Native.ZkZoneFog_getFadeOutSky(Handle);
			set => Native.ZkZoneFog_setFadeOutSky(Handle, value);
		}

		public bool OverrideColor
		{
			get => Native.ZkZoneFog_getOverrideColor(Handle);
			set => Native.ZkZoneFog_setOverrideColor(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkZoneFog_del(Handle);
		}
	}

	public class ZoneFogDefault : ZoneFog
	{
		public ZoneFogDefault() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCZoneZFogDefault))
		{
		}

		public ZoneFogDefault(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public ZoneFogDefault(string path, GameVersion version) : base(path, version)
		{
		}

		internal ZoneFogDefault(UIntPtr handle) : base(handle)
		{
		}
	}
}