using System;

namespace ZenKit.Vobs
{
	public enum SoundMode
	{
		Loop = 0,
		Once = 1,
		Random = 2
	}

	public enum SoundTriggerVolumeType
	{
		Spherical = 0,
		Ellipsoidal = 1
	}

	public class Sound : VirtualObject
	{
		public Sound(Read buf, GameVersion version) : base(Native.ZkSound_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Sound vob");
		}

		public Sound(string path, GameVersion version) : base(Native.ZkSound_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Sound vob");
		}

		internal Sound(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public float Volume => Native.ZkSound_getVolume(Handle);
		public SoundMode Mode => Native.ZkSound_getMode(Handle);
		public float RandomDelay => Native.ZkSound_getRandomDelay(Handle);
		public float RandomDelayVar => Native.ZkSound_getRandomDelayVar(Handle);
		public bool InitiallyPlaying => Native.ZkSound_getInitiallyPlaying(Handle);
		public bool Ambient3d => Native.ZkSound_getAmbient3d(Handle);
		public bool Obstruction => Native.ZkSound_getObstruction(Handle);
		public float ConeAngle => Native.ZkSound_getConeAngle(Handle);
		public SoundTriggerVolumeType VolumeType => Native.ZkSound_getVolumeType(Handle);
		public float Radius => Native.ZkSound_getRadius(Handle);
		public string SoundName => Native.ZkSound_getSoundName(Handle).MarshalAsString() ?? string.Empty;

		protected override void Delete()
		{
			Native.ZkSound_del(Handle);
		}
	}

	public class SoundDaytime : Sound
	{
		public SoundDaytime(Read buf, GameVersion version) : base(Native.ZkSoundDaytime_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load SoundDaytime vob");
		}

		public SoundDaytime(string path, GameVersion version) : base(Native.ZkSoundDaytime_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load SoundDaytime vob");
		}

		internal SoundDaytime(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public float StartTime => Native.ZkSoundDaytime_getStartTime(Handle);
		public float EndTime => Native.ZkSoundDaytime_getEndTime(Handle);

		public string SoundNameDaytime =>
			Native.ZkSoundDaytime_getSoundNameDaytime(Handle).MarshalAsString() ?? string.Empty;

		protected override void Delete()
		{
			Native.ZkSoundDaytime_del(Handle);
		}
	}
}