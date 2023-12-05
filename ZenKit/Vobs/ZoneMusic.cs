using System;

namespace ZenKit.Vobs
{
	public class ZoneMusic : VirtualObject
	{
		public ZoneMusic(Read buf, GameVersion version) : base(Native.ZkZoneMusic_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneMusic vob");
		}

		public ZoneMusic(string path, GameVersion version) : base(Native.ZkZoneMusic_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneMusic vob");
		}

		internal ZoneMusic(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public bool IsEnabled => Native.ZkZoneMusic_getIsEnabled(Handle);
		public int Priority => Native.ZkZoneMusic_getPriority(Handle);
		public bool IsEllipsoid => Native.ZkZoneMusic_getIsEllipsoid(Handle);
		public float Reverb => Native.ZkZoneMusic_getReverb(Handle);
		public float Volume => Native.ZkZoneMusic_getVolume(Handle);
		public bool IsLoop => Native.ZkZoneMusic_getIsLoop(Handle);

		protected override void Delete()
		{
			Native.ZkZoneMusic_del(Handle);
		}
	}

	public class ZoneMusicDefault : ZoneMusic
	{
		public ZoneMusicDefault(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public ZoneMusicDefault(string path, GameVersion version) : base(path, version)
		{
		}

		internal ZoneMusicDefault(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
}