using System;

namespace ZenKit.Daedalus
{
	public class SoundEffectInstance : DaedalusInstance
	{
		public SoundEffectInstance(UIntPtr handle) : base(handle)
		{
		}

		public string File => Native.ZkSoundEffectInstance_getFile(Handle).MarshalAsString() ?? string.Empty;
		public int PitchOff => Native.ZkSoundEffectInstance_getPitchOff(Handle);
		public int PitchVar => Native.ZkSoundEffectInstance_getPitchVar(Handle);
		public int Volume => Native.ZkSoundEffectInstance_getVolume(Handle);
		public int Loop => Native.ZkSoundEffectInstance_getLoop(Handle);
		public int LoopStartOffset => Native.ZkSoundEffectInstance_getLoopStartOffset(Handle);
		public int LoopEndOffset => Native.ZkSoundEffectInstance_getLoopEndOffset(Handle);
		public float ReverbLevel => Native.ZkSoundEffectInstance_getReverbLevel(Handle);
		public string PfxName => Native.ZkSoundEffectInstance_getPfxName(Handle).MarshalAsString() ?? string.Empty;
	}
}