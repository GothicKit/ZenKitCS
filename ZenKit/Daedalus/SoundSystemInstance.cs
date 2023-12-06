using System;

namespace ZenKit.Daedalus
{
	public class SoundSystemInstance : DaedalusInstance
	{
		public SoundSystemInstance(UIntPtr handle) : base(handle)
		{
		}

		public float Volume
		{
			get => Native.ZkSoundSystemInstance_getVolume(Handle);
			set => Native.ZkSoundSystemInstance_setVolume(Handle, value);
		}

		public int BitResolution
		{
			get => Native.ZkSoundSystemInstance_getBitResolution(Handle);
			set => Native.ZkSoundSystemInstance_setBitResolution(Handle, value);
		}

		public int SampleRate
		{
			get => Native.ZkSoundSystemInstance_getSampleRate(Handle);
			set => Native.ZkSoundSystemInstance_setSampleRate(Handle, value);
		}

		public int UseStereo
		{
			get => Native.ZkSoundSystemInstance_getUseStereo(Handle);
			set => Native.ZkSoundSystemInstance_setUseStereo(Handle, value);
		}

		public int NumSfxChannels
		{
			get => Native.ZkSoundSystemInstance_getNumSfxChannels(Handle);
			set => Native.ZkSoundSystemInstance_setNumSfxChannels(Handle, value);
		}


		public string Used3DProviderName
		{
			get => Native.ZkSoundSystemInstance_getUsed3DProviderName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkSoundSystemInstance_setUsed3DProviderName(Handle, value);
		}
	}
}