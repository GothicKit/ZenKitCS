using System;

namespace ZenKit.Daedalus
{
	public class MusicSystemInstance : DaedalusInstance
	{
		public MusicSystemInstance(UIntPtr handle) : base(handle)
		{
		}

		public float Volume
		{
			get => Native.ZkMusicSystemInstance_getVolume(Handle);
			set => Native.ZkMusicSystemInstance_setVolume(Handle, value);
		}

		public int BitResolution
		{
			get => Native.ZkMusicSystemInstance_getBitResolution(Handle);
			set => Native.ZkMusicSystemInstance_setBitResolution(Handle, value);
		}

		public int GlobalReverbEnabled
		{
			get => Native.ZkMusicSystemInstance_getGlobalReverbEnabled(Handle);
			set => Native.ZkMusicSystemInstance_setGlobalReverbEnabled(Handle, value);
		}

		public int SampleRate
		{
			get => Native.ZkMusicSystemInstance_getSampleRate(Handle);
			set => Native.ZkMusicSystemInstance_setSampleRate(Handle, value);
		}

		public int NumChannels
		{
			get => Native.ZkMusicSystemInstance_getNumChannels(Handle);
			set => Native.ZkMusicSystemInstance_setNumChannels(Handle, value);
		}

		public int ReverbBufferSize
		{
			get => Native.ZkMusicSystemInstance_getReverbBufferSize(Handle);
			set => Native.ZkMusicSystemInstance_setReverbBufferSize(Handle, value);
		}
	}
}