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