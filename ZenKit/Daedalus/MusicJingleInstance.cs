using System;

namespace ZenKit.Daedalus
{
	public class MusicJingleInstance : DaedalusInstance
	{
		public MusicJingleInstance(UIntPtr handle) : base(handle)
		{
		}
	
		public string Name => Native.ZkMusicJingleInstance_getName(Handle).MarshalAsString() ?? string.Empty;
		public int Loop => Native.ZkMusicJingleInstance_getLoop(Handle);
		public float Vol => Native.ZkMusicJingleInstance_getVol(Handle);
		public int TransSubType => Native.ZkMusicJingleInstance_getTranssubtype(Handle);
	}
}