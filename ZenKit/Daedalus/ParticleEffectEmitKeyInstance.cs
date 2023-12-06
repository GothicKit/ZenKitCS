using System;

namespace ZenKit.Daedalus
{
	public class ParticleEffectEmitKeyInstance : DaedalusInstance
	{
		public ParticleEffectEmitKeyInstance(UIntPtr handle) : base(handle)
		{
		}

		public string VisNameS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getVisNameS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setVisNameS(Handle, value);
		}


		public float VisSizeScale
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getVisSizeScale(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setVisSizeScale(Handle, value);
		}

		public float ScaleDuration
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getScaleDuration(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setScaleDuration(Handle, value);
		}

		public float PfxPpsValue
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxPpsValue(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxPpsValue(Handle, value);
		}

		public int PfxPpsIsSmoothChg
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxPpsIsSmoothChg(Handle, value);
		}

		public int PfxPpsIsLoopingChg
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxPpsIsLoopingChg(Handle, value);
		}

		public float PfxScTime
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxScTime(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxScTime(Handle, value);
		}


		public string PfxFlyGravityS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxFlyGravityS(Handle, value);
		}


		public string PfxShpDimS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxShpDimS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxShpDimS(Handle, value);
		}


		public int PfxShpIsVolumeChg
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxShpIsVolumeChg(Handle, value);
		}

		public float PfxShpScaleFps
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxShpScaleFps(Handle, value);
		}


		public float PfxShpDistributionWalksPeed
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxShpDistribWalksPeed(Handle, value);
		}


		public string PfxShpOffsetVecS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxShpOffsetVecS(Handle, value);
		}


		public string PfxShpDistributionTypeS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS(Handle).MarshalAsString() ??
			       string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxShpDistribTypeS(Handle, value);
		}


		public string PfxDirModeS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxDirModeS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxDirModeS(Handle, value);
		}


		public string PfxDirForS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxDirForS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxDirForS(Handle, value);
		}

		public string PfxDirModeTargetForS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS(Handle).MarshalAsString() ??
			       string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxDirModeTargetForS(Handle, value);
		}


		public string PfxDirModeTargetPosS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS(Handle).MarshalAsString() ??
			       string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxDirModeTargetPosS(Handle, value);
		}


		public float PfxVelAvg
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxVelAvg(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxVelAvg(Handle, value);
		}

		public float PfxLspPartAvg
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxLspPartAvg(Handle, value);
		}

		public float PfxVisAlphaStart
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setPfxVisAlphaStart(Handle, value);
		}


		public string LightPresetName
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getLightPresetName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setLightPresetName(Handle, value);
		}


		public float LightRange
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getLightRange(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setLightRange(Handle, value);
		}


		public string SfxId
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getSfxId(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setSfxId(Handle, value);
		}

		public int SfxIsAmbient
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getSfxIsAmbient(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setSfxIsAmbient(Handle, value);
		}


		public string EmCreateFxId
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getEmCreateFxId(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setEmCreateFxId(Handle, value);
		}

		public float EmFlyGravity
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getEmFlyGravity(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setEmFlyGravity(Handle, value);
		}


		public string EmSelfRotVelS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setEmSelfRotVelS(Handle, value);
		}


		public string EmTrjModeS
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getEmTrjModeS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectEmitKeyInstance_setEmTrjModeS(Handle, value);
		}


		public float EmTrjEaseVel
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setEmTrjEaseVel(Handle, value);
		}

		public int EmCheckCollision
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getEmCheckCollision(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setEmCheckCollision(Handle, value);
		}

		public float EmFxLifespan
		{
			get => Native.ZkParticleEffectEmitKeyInstance_getEmFxLifespan(Handle);
			set => Native.ZkParticleEffectEmitKeyInstance_setEmFxLifespan(Handle, value);
		}
	}
}