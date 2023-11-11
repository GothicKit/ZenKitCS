using System;

namespace ZenKit.Daedalus
{
	public class MusicSystemInstance : DaedalusInstance
	{
		public MusicSystemInstance(UIntPtr handle) : base(handle)
		{
		}

		public float Volume => Native.ZkMusicSystemInstance_getVolume(Handle);
		public int BitResolution => Native.ZkMusicSystemInstance_getBitResolution(Handle);
		public int GlobalReverbEnabled => Native.ZkMusicSystemInstance_getGlobalReverbEnabled(Handle);
		public int SampleRate => Native.ZkMusicSystemInstance_getSampleRate(Handle);
		public int NumChannels => Native.ZkMusicSystemInstance_getNumChannels(Handle);
		public int ReverbBufferSize => Native.ZkMusicSystemInstance_getReverbBufferSize(Handle);
	}
}