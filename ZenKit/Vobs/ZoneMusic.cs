using System;

namespace ZenKit.Vobs
{
	public class ZoneMusic : VirtualObject
	{
		public ZoneMusic() : base(Native.ZkVirtualObject_new(VirtualObjectType.oCZoneMusic))
		{
		}

		public ZoneMusic(Read buf, GameVersion version) : base(Native.ZkZoneMusic_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneMusic vob");
		}

		public ZoneMusic(string path, GameVersion version) : base(Native.ZkZoneMusic_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load ZoneMusic vob");
		}

		internal ZoneMusic(UIntPtr handle) : base(handle)
		{
		}

		public bool IsEnabled
		{
			get => Native.ZkZoneMusic_getIsEnabled(Handle);
			set => Native.ZkZoneMusic_setIsEnabled(Handle, value);
		}

		public int Priority
		{
			get => Native.ZkZoneMusic_getPriority(Handle);
			set => Native.ZkZoneMusic_setPriority(Handle, value);
		}

		public bool IsEllipsoid
		{
			get => Native.ZkZoneMusic_getIsEllipsoid(Handle);
			set => Native.ZkZoneMusic_setIsEllipsoid(Handle, value);
		}

		public float Reverb
		{
			get => Native.ZkZoneMusic_getReverb(Handle);
			set => Native.ZkZoneMusic_setReverb(Handle, value);
		}

		public float Volume
		{
			get => Native.ZkZoneMusic_getVolume(Handle);
			set => Native.ZkZoneMusic_setVolume(Handle, value);
		}

		public bool IsLoop
		{
			get => Native.ZkZoneMusic_getIsLoop(Handle);
			set => Native.ZkZoneMusic_setIsLoop(Handle, value);
		}

		public bool LocalEnabled
		{
			get => Native.ZkZoneMusic_getLocalEnabled(Handle);
			set => Native.ZkZoneMusic_setLocalEnabled(Handle, value);
		}

		public bool DayEntranceDone
		{
			get => Native.ZkZoneMusic_getDayEntranceDone(Handle);
			set => Native.ZkZoneMusic_setDayEntranceDone(Handle, value);
		}

		public bool NightEntranceDone
		{
			get => Native.ZkZoneMusic_getNightEntranceDone(Handle);
			set => Native.ZkZoneMusic_setNightEntranceDone(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkZoneMusic_del(Handle);
		}
	}

	public class ZoneMusicDefault : ZoneMusic
	{
		public ZoneMusicDefault() : base(Native.ZkVirtualObject_new(VirtualObjectType.oCZoneMusicDefault))
		{
		}

		public ZoneMusicDefault(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public ZoneMusicDefault(string path, GameVersion version) : base(path, version)
		{
		}

		internal ZoneMusicDefault(UIntPtr handle) : base(handle)
		{
		}
	}
}