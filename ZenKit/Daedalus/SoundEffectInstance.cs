using System;

namespace ZenKit.Daedalus
{
	public class SoundEffectInstance : DaedalusInstance
	{
		public SoundEffectInstance(UIntPtr handle) : base(handle)
		{
		}

		public string File
		{
			get => Native.ZkSoundEffectInstance_getFile(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkSoundEffectInstance_setFile(Handle, value);
		}

		public int PitchOff
		{
			get => Native.ZkSoundEffectInstance_getPitchOff(Handle);
			set => Native.ZkSoundEffectInstance_setPitchOff(Handle, value);
		}

		public int PitchVar
		{
			get => Native.ZkSoundEffectInstance_getPitchVar(Handle);
			set => Native.ZkSoundEffectInstance_setPitchVar(Handle, value);
		}

		public int Volume
		{
			get => Native.ZkSoundEffectInstance_getVolume(Handle);
			set => Native.ZkSoundEffectInstance_setVolume(Handle, value);
		}

		public int Loop
		{
			get => Native.ZkSoundEffectInstance_getLoop(Handle);
			set => Native.ZkSoundEffectInstance_setLoop(Handle, value);
		}

		public int LoopStartOffset
		{
			get => Native.ZkSoundEffectInstance_getLoopStartOffset(Handle);
			set => Native.ZkSoundEffectInstance_setLoopStartOffset(Handle, value);
		}

		public int LoopEndOffset
		{
			get => Native.ZkSoundEffectInstance_getLoopEndOffset(Handle);
			set => Native.ZkSoundEffectInstance_setLoopEndOffset(Handle, value);
		}

		public float ReverbLevel
		{
			get => Native.ZkSoundEffectInstance_getReverbLevel(Handle);
			set => Native.ZkSoundEffectInstance_setReverbLevel(Handle, value);
		}

		public string PfxName
		{
			get => Native.ZkSoundEffectInstance_getPfxName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkSoundEffectInstance_setPfxName(Handle, value);
		}
	}
}