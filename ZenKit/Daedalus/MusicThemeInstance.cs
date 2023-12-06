using System;

namespace ZenKit.Daedalus
{
	public enum MusicTransitionEffect
	{
		Unknown = 0,
		None = 1,
		Groove = 2,
		Fill = 3,
		Break = 4,
		Intro = 5,
		End = 6,
		EndAndInto = 7
	}

	public enum MusicTransitionType
	{
		Unknown = 0,
		Immediate = 1,
		Beat = 2,
		Measure = 3
	}

	public class MusicThemeInstance : DaedalusInstance
	{
		public MusicThemeInstance(UIntPtr handle) : base(handle)
		{
		}

		public string File
		{
			get => Native.ZkMusicThemeInstance_getFile(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMusicThemeInstance_setFile(Handle, value);
		}

		public float Vol
		{
			get => Native.ZkMusicThemeInstance_getVol(Handle);
			set => Native.ZkMusicThemeInstance_setVol(Handle, value);
		}

		public int Loop
		{
			get => Native.ZkMusicThemeInstance_getLoop(Handle);
			set => Native.ZkMusicThemeInstance_setLoop(Handle, value);
		}

		public float ReverbMix
		{
			get => Native.ZkMusicThemeInstance_getReverbmix(Handle);
			set => Native.ZkMusicThemeInstance_setReverbmix(Handle, value);
		}

		public float ReverbTime
		{
			get => Native.ZkMusicThemeInstance_getReverbtime(Handle);
			set => Native.ZkMusicThemeInstance_setReverbtime(Handle, value);
		}

		public MusicTransitionEffect TransType
		{
			get => Native.ZkMusicThemeInstance_getTranstype(Handle);
			set => Native.ZkMusicThemeInstance_setTranstype(Handle, value);
		}

		public MusicTransitionType TransSubType
		{
			get => Native.ZkMusicThemeInstance_getTranssubtype(Handle);
			set => Native.ZkMusicThemeInstance_setTranssubtype(Handle, value);
		}
	}
}