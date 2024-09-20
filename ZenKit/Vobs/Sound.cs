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

	public interface ISound : IVirtualObject
	{
		float Volume { get; set; }
		SoundMode Mode { get; set; }
		float RandomDelay { get; set; }
		float RandomDelayVar { get; set; }
		bool InitiallyPlaying { get; set; }
		bool Ambient3d { get; set; }
		bool Obstruction { get; set; }
		float ConeAngle { get; set; }
		SoundTriggerVolumeType VolumeType { get; set; }
		float Radius { get; set; }
		string SoundName { get; set; }
		bool IsRunning { get; set; }
		bool IsAllowedToRun { get; set; }
	}

	public class Sound : VirtualObject, ISound
	{
		public Sound() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCVobSound))
		{
		}

		public Sound(Read buf, GameVersion version) : base(Native.ZkSound_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Sound vob");
		}

		public Sound(string path, GameVersion version) : base(Native.ZkSound_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Sound vob");
		}

		internal Sound(UIntPtr handle) : base(handle)
		{
		}

		public float Volume
		{
			get => Native.ZkSound_getVolume(Handle);
			set => Native.ZkSound_setVolume(Handle, value);
		}

		public SoundMode Mode
		{
			get => Native.ZkSound_getMode(Handle);
			set => Native.ZkSound_setMode(Handle, value);
		}

		public float RandomDelay
		{
			get => Native.ZkSound_getRandomDelay(Handle);
			set => Native.ZkSound_setRandomDelay(Handle, value);
		}

		public float RandomDelayVar
		{
			get => Native.ZkSound_getRandomDelayVar(Handle);
			set => Native.ZkSound_setRandomDelayVar(Handle, value);
		}

		public bool InitiallyPlaying
		{
			get => Native.ZkSound_getInitiallyPlaying(Handle);
			set => Native.ZkSound_setInitiallyPlaying(Handle, value);
		}

		public bool Ambient3d
		{
			get => Native.ZkSound_getAmbient3d(Handle);
			set => Native.ZkSound_setAmbient3d(Handle, value);
		}

		public bool Obstruction
		{
			get => Native.ZkSound_getObstruction(Handle);
			set => Native.ZkSound_setObstruction(Handle, value);
		}

		public float ConeAngle
		{
			get => Native.ZkSound_getConeAngle(Handle);
			set => Native.ZkSound_setConeAngle(Handle, value);
		}

		public SoundTriggerVolumeType VolumeType
		{
			get => Native.ZkSound_getVolumeType(Handle);
			set => Native.ZkSound_setVolumeType(Handle, value);
		}

		public float Radius
		{
			get => Native.ZkSound_getRadius(Handle);
			set => Native.ZkSound_setRadius(Handle, value);
		}

		public string SoundName
		{
			get => Native.ZkSound_getSoundName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkSound_setSoundName(Handle, value);
		}

		public bool IsRunning
		{
			get => Native.ZkSound_getIsRunning(Handle);
			set => Native.ZkSound_setIsRunning(Handle, value);
		}

		public bool IsAllowedToRun
		{
			get => Native.ZkSound_getIsAllowedToRun(Handle);
			set => Native.ZkSound_setIsAllowedToRun(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkSound_del(Handle);
		}
	}

	public interface ISoundDaytime : ISound
	{
		float StartTime { get; set; }
		float EndTime { get; set; }
		string SoundNameDaytime { get; set; }
	}

	public class SoundDaytime : Sound, ISoundDaytime
	{
		public SoundDaytime() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCVobSoundDaytime))
		{
		}

		public SoundDaytime(Read buf, GameVersion version) : base(Native.ZkSoundDaytime_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load SoundDaytime vob");
		}

		public SoundDaytime(string path, GameVersion version) : base(Native.ZkSoundDaytime_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load SoundDaytime vob");
		}

		internal SoundDaytime(UIntPtr handle) : base(handle)
		{
		}

		public float StartTime
		{
			get => Native.ZkSoundDaytime_getStartTime(Handle);
			set => Native.ZkSoundDaytime_setStartTime(Handle, value);
		}

		public float EndTime
		{
			get => Native.ZkSoundDaytime_getEndTime(Handle);
			set => Native.ZkSoundDaytime_setEndTime(Handle, value);
		}


		public string SoundNameDaytime
		{
			get => Native.ZkSoundDaytime_getSoundNameDaytime(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkSoundDaytime_setSoundNameDaytime(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkSoundDaytime_del(Handle);
		}
	}
}
