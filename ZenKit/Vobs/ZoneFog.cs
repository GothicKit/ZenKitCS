using System;
using System.Drawing;

namespace ZenKit.Vobs
{
	public class ZoneFog : VirtualObject
	{
		public ZoneFog(Read buf, GameVersion version) : base(Native.ZkZoneFog_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneFog vob");
		}

		public ZoneFog(string path, GameVersion version) : base(Native.ZkZoneFog_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneFog vob");
		}

		internal ZoneFog(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public float RangeCenter => Native.ZkZoneFog_getRangeCenter(Handle);
		public float InnerRangePercentage => Native.ZkZoneFog_getInnerRangePercentage(Handle);
		public Color Color => Native.ZkZoneFog_getColor(Handle).ToColor();
		public bool FadeOutSky => Native.ZkZoneFog_getFadeOutSky(Handle);
		public bool OverrideColor => Native.ZkZoneFog_getOverrideColor(Handle);

		protected override void Delete()
		{
			Native.ZkZoneFog_del(Handle);
		}
	}
}