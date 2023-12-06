using System;

namespace ZenKit.Daedalus
{
	public class MusicJingleInstance : DaedalusInstance
	{
		public MusicJingleInstance(UIntPtr handle) : base(handle)
		{
		}

		public string Name
		{
			get => Native.ZkMusicJingleInstance_getName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMusicJingleInstance_setName(Handle, value);
		}

		public int Loop
		{
			get => Native.ZkMusicJingleInstance_getLoop(Handle);
			set => Native.ZkMusicJingleInstance_setLoop(Handle, value);
		}

		public float Vol
		{
			get => Native.ZkMusicJingleInstance_getVol(Handle);
			set => Native.ZkMusicJingleInstance_setVol(Handle, value);
		}

		public int TransSubType
		{
			get => Native.ZkMusicJingleInstance_getTranssubtype(Handle);
			set => Native.ZkMusicJingleInstance_setTranssubtype(Handle, value);
		}
	}
}