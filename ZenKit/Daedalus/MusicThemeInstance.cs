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
		EndAndInto = 7,
	}

	public enum MusicTransitionType
	{
		Unknown = 0,
		Immediate = 1,
		Beat = 2,
		Measure = 3,
	}

	public class MusicThemeInstance : DaedalusInstance
	{
		public MusicThemeInstance(UIntPtr handle) : base(handle)
		{
		}
	
		public string File => Native.ZkMusicThemeInstance_getFile(Handle).MarshalAsString() ?? string.Empty;
		public float Vol => Native.ZkMusicThemeInstance_getVol(Handle);
		public int Loop => Native.ZkMusicThemeInstance_getLoop(Handle);
		public float ReverbMix => Native.ZkMusicThemeInstance_getReverbmix(Handle);
		public float ReverbTime => Native.ZkMusicThemeInstance_getReverbtime(Handle);
		public MusicTransitionEffect TransType => Native.ZkMusicThemeInstance_getTranstype(Handle);
		public MusicTransitionType TransSubType => Native.ZkMusicThemeInstance_getTranssubtype(Handle);
	}
}