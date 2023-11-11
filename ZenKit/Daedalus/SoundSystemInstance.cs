using System;

namespace ZenKit.Daedalus
{
	public class SoundSystemInstance : DaedalusInstance
	{
		public SoundSystemInstance(UIntPtr handle) : base(handle)
		{
		}
	
		public float Volume => Native.ZkSoundSystemInstance_getVolume(Handle);
		public int BitResolution => Native.ZkSoundSystemInstance_getBitResolution(Handle);
		public int SampleRate => Native.ZkSoundSystemInstance_getSampleRate(Handle);
		public int UseStereo => Native.ZkSoundSystemInstance_getUseStereo(Handle);
		public int NumSfxChannels => Native.ZkSoundSystemInstance_getNumSfxChannels(Handle);

		public string Used3DProviderName =>
			Native.ZkSoundSystemInstance_getUsed3DProviderName(Handle).MarshalAsString() ?? string.Empty;
	}
}