using System;

namespace ZenKit.Daedalus
{
	public class ParticleEffectEmitKeyInstance : DaedalusInstance
	{
		public ParticleEffectEmitKeyInstance(UIntPtr handle) : base(handle)
		{
		}

		public string VisNameS => Native.ZkParticleEffectEmitKeyInstance_getVisNameS(Handle).MarshalAsString() ?? string.Empty;
		public float VisSizeScale => Native.ZkParticleEffectEmitKeyInstance_getVisSizeScale(Handle);
		public float ScaleDuration => Native.ZkParticleEffectEmitKeyInstance_getScaleDuration(Handle);
		public float PfxPpsValue => Native.ZkParticleEffectEmitKeyInstance_getPfxPpsValue(Handle);
		public int PfxPpsIsSmoothChg => Native.ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg(Handle);
		public int PfxPpsIsLoopingChg => Native.ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg(Handle);
		public float PfxScTime => Native.ZkParticleEffectEmitKeyInstance_getPfxScTime(Handle);

		public string PfxFlyGravityS =>
			Native.ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS(Handle).MarshalAsString() ?? string.Empty;

		public string PfxShpDimS => Native.ZkParticleEffectEmitKeyInstance_getPfxShpDimS(Handle).MarshalAsString() ?? string.Empty;
		public int PfxShpIsVolumeChg => Native.ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg(Handle);
		public float PfxShpScaleFps => Native.ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps(Handle);
		public float PfxShpDistributionWalksPeed => Native.ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed(Handle);

		public string PfxShpOffsetVecS =>
			Native.ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS(Handle).MarshalAsString() ?? string.Empty;

		public string PfxShpDistributionTypeS =>
			Native.ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS(Handle).MarshalAsString() ?? string.Empty;

		public string PfxDirModeS =>
			Native.ZkParticleEffectEmitKeyInstance_getPfxDirModeS(Handle).MarshalAsString() ?? string.Empty;

		public string PfxDirForS => Native.ZkParticleEffectEmitKeyInstance_getPfxDirForS(Handle).MarshalAsString() ?? string.Empty;

		public string PfxDirModeTargetForS =>
			Native.ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS(Handle).MarshalAsString() ?? string.Empty;

		public string PfxDirModeTargetPosS =>
			Native.ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS(Handle).MarshalAsString() ?? string.Empty;

		public float PfxVelAvg => Native.ZkParticleEffectEmitKeyInstance_getPfxVelAvg(Handle);
		public float PfxLspPartAvg => Native.ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg(Handle);
		public float PfxVisAlphaStart => Native.ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart(Handle);

		public string LightPresetName =>
			Native.ZkParticleEffectEmitKeyInstance_getLightPresetName(Handle).MarshalAsString() ?? string.Empty;

		public float LightRange => Native.ZkParticleEffectEmitKeyInstance_getLightRange(Handle);
		public string SfxId => Native.ZkParticleEffectEmitKeyInstance_getSfxId(Handle).MarshalAsString() ?? string.Empty;
		public int SfxIsAmbient => Native.ZkParticleEffectEmitKeyInstance_getSfxIsAmbient(Handle);

		public string EmCreateFxId =>
			Native.ZkParticleEffectEmitKeyInstance_getEmCreateFxId(Handle).MarshalAsString() ?? string.Empty;

		public float EmFlyGravity => Native.ZkParticleEffectEmitKeyInstance_getEmFlyGravity(Handle);

		public string EmSelfRotVelS =>
			Native.ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS(Handle).MarshalAsString() ?? string.Empty;

		public string EmTrjModeS => Native.ZkParticleEffectEmitKeyInstance_getEmTrjModeS(Handle).MarshalAsString() ?? string.Empty;
		public float EmTrjEaseVel => Native.ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel(Handle);
		public int EmCheckCollision => Native.ZkParticleEffectEmitKeyInstance_getEmCheckCollision(Handle);
		public float EmFxLifespan => Native.ZkParticleEffectEmitKeyInstance_getEmFxLifespan(Handle);
	}
}