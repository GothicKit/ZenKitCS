using System;

namespace ZenKit.Daedalus
{
	public class ParticleEffectInstance : DaedalusInstance
	{
		public ParticleEffectInstance(UIntPtr handle) : base(handle)
		{
		}

		public float PpsValue
		{
			get => Native.ZkParticleEffectInstance_getPpsValue(Handle);
			set => Native.ZkParticleEffectInstance_setPpsValue(Handle, value);
		}


		public string PpsScaleKeysS
		{
			get => Native.ZkParticleEffectInstance_getPpsScaleKeysS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setPpsScaleKeysS(Handle, value);
		}


		public int PpsIsLooping
		{
			get => Native.ZkParticleEffectInstance_getPpsIsLooping(Handle);
			set => Native.ZkParticleEffectInstance_setPpsIsLooping(Handle, value);
		}

		public int PpsIsSmooth
		{
			get => Native.ZkParticleEffectInstance_getPpsIsSmooth(Handle);
			set => Native.ZkParticleEffectInstance_setPpsIsSmooth(Handle, value);
		}

		public float PpsFps
		{
			get => Native.ZkParticleEffectInstance_getPpsFps(Handle);
			set => Native.ZkParticleEffectInstance_setPpsFps(Handle, value);
		}


		public string PpsCreateEmS
		{
			get => Native.ZkParticleEffectInstance_getPpsCreateEmS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setPpsCreateEmS(Handle, value);
		}


		public float PpsCreateEmDelay
		{
			get => Native.ZkParticleEffectInstance_getPpsCreateEmDelay(Handle);
			set => Native.ZkParticleEffectInstance_setPpsCreateEmDelay(Handle, value);
		}

		public string ShpTypeS
		{
			get => Native.ZkParticleEffectInstance_getShpTypeS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setShpTypeS(Handle, value);
		}

		public string ShpForS
		{
			get => Native.ZkParticleEffectInstance_getShpForS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setShpForS(Handle, value);
		}


		public string ShpOffsetVecS
		{
			get => Native.ZkParticleEffectInstance_getShpOffsetVecS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setShpOffsetVecS(Handle, value);
		}


		public string ShpDistributionTypeS
		{
			get => Native.ZkParticleEffectInstance_getShpDistribTypeS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setShpDistribTypeS(Handle, value);
		}


		public float ShpDistributionWalkSpeed
		{
			get => Native.ZkParticleEffectInstance_getShpDistribWalkSpeed(Handle);
			set => Native.ZkParticleEffectInstance_setShpDistribWalkSpeed(Handle, value);
		}

		public int ShpIsVolume
		{
			get => Native.ZkParticleEffectInstance_getShpIsVolume(Handle);
			set => Native.ZkParticleEffectInstance_setShpIsVolume(Handle, value);
		}

		public string ShpDimS
		{
			get => Native.ZkParticleEffectInstance_getShpDimS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setShpDimS(Handle, value);
		}

		public string ShpMeshS
		{
			get => Native.ZkParticleEffectInstance_getShpMeshS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setShpMeshS(Handle, value);
		}

		public int ShpMeshRenderB
		{
			get => Native.ZkParticleEffectInstance_getShpMeshRenderB(Handle);
			set => Native.ZkParticleEffectInstance_setShpMeshRenderB(Handle, value);
		}


		public string ShpScaleKeysS
		{
			get => Native.ZkParticleEffectInstance_getShpScaleKeysS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setShpScaleKeysS(Handle, value);
		}


		public int ShpScaleIsLooping
		{
			get => Native.ZkParticleEffectInstance_getShpScaleIsLooping(Handle);
			set => Native.ZkParticleEffectInstance_setShpScaleIsLooping(Handle, value);
		}

		public int ShpScaleIsSmooth
		{
			get => Native.ZkParticleEffectInstance_getShpScaleIsSmooth(Handle);
			set => Native.ZkParticleEffectInstance_setShpScaleIsSmooth(Handle, value);
		}

		public float ShpScaleFps
		{
			get => Native.ZkParticleEffectInstance_getShpScaleFps(Handle);
			set => Native.ZkParticleEffectInstance_setShpScaleFps(Handle, value);
		}

		public string DirModeS
		{
			get => Native.ZkParticleEffectInstance_getDirModeS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setDirModeS(Handle, value);
		}

		public string DirForS
		{
			get => Native.ZkParticleEffectInstance_getDirForS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setDirForS(Handle, value);
		}


		public string DirModeTargetForS
		{
			get => Native.ZkParticleEffectInstance_getDirModeTargetForS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setDirModeTargetForS(Handle, value);
		}


		public string DirModeTargetPosS
		{
			get => Native.ZkParticleEffectInstance_getDirModeTargetPosS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setDirModeTargetPosS(Handle, value);
		}


		public float DirAngleHead
		{
			get => Native.ZkParticleEffectInstance_getDirAngleHead(Handle);
			set => Native.ZkParticleEffectInstance_setDirAngleHead(Handle, value);
		}

		public float DirAngleHeadVar
		{
			get => Native.ZkParticleEffectInstance_getDirAngleHeadVar(Handle);
			set => Native.ZkParticleEffectInstance_setDirAngleHeadVar(Handle, value);
		}

		public float DirAngleElev
		{
			get => Native.ZkParticleEffectInstance_getDirAngleElev(Handle);
			set => Native.ZkParticleEffectInstance_setDirAngleElev(Handle, value);
		}

		public float DirAngleElevVar
		{
			get => Native.ZkParticleEffectInstance_getDirAngleElevVar(Handle);
			set => Native.ZkParticleEffectInstance_setDirAngleElevVar(Handle, value);
		}

		public float VelAvg
		{
			get => Native.ZkParticleEffectInstance_getVelAvg(Handle);
			set => Native.ZkParticleEffectInstance_setVelAvg(Handle, value);
		}

		public float VelVar
		{
			get => Native.ZkParticleEffectInstance_getVelVar(Handle);
			set => Native.ZkParticleEffectInstance_setVelVar(Handle, value);
		}

		public float LspPartAvg
		{
			get => Native.ZkParticleEffectInstance_getLspPartAvg(Handle);
			set => Native.ZkParticleEffectInstance_setLspPartAvg(Handle, value);
		}

		public float LspPartVar
		{
			get => Native.ZkParticleEffectInstance_getLspPartVar(Handle);
			set => Native.ZkParticleEffectInstance_setLspPartVar(Handle, value);
		}


		public string FlyGravityS
		{
			get => Native.ZkParticleEffectInstance_getFlyGravityS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setFlyGravityS(Handle, value);
		}


		public int FlyCollisionDetectionB
		{
			get => Native.ZkParticleEffectInstance_getFlyColldetB(Handle);
			set => Native.ZkParticleEffectInstance_setFlyColldetB(Handle, value);
		}

		public string VisNameS
		{
			get => Native.ZkParticleEffectInstance_getVisNameS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setVisNameS(Handle, value);
		}


		public string VisOrientationS
		{
			get => Native.ZkParticleEffectInstance_getVisOrientationS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setVisOrientationS(Handle, value);
		}


		public int VisTexIsQuadPoly
		{
			get => Native.ZkParticleEffectInstance_getVisTexIsQuadpoly(Handle);
			set => Native.ZkParticleEffectInstance_setVisTexIsQuadpoly(Handle, value);
		}

		public float VisTexAniFps
		{
			get => Native.ZkParticleEffectInstance_getVisTexAniFps(Handle);
			set => Native.ZkParticleEffectInstance_setVisTexAniFps(Handle, value);
		}

		public int VisTexAniIsLooping
		{
			get => Native.ZkParticleEffectInstance_getVisTexAniIsLooping(Handle);
			set => Native.ZkParticleEffectInstance_setVisTexAniIsLooping(Handle, value);
		}


		public string VisTexColorStartS
		{
			get => Native.ZkParticleEffectInstance_getVisTexColorStartS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setVisTexColorStartS(Handle, value);
		}


		public string VisTexColorEndS
		{
			get => Native.ZkParticleEffectInstance_getVisTexColorEndS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setVisTexColorEndS(Handle, value);
		}


		public string VisSizeStartS
		{
			get => Native.ZkParticleEffectInstance_getVisSizeStartS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setVisSizeStartS(Handle, value);
		}


		public float VisSizeEndScale
		{
			get => Native.ZkParticleEffectInstance_getVisSizeEndScale(Handle);
			set => Native.ZkParticleEffectInstance_setVisSizeEndScale(Handle, value);
		}


		public string VisAlphaFuncS
		{
			get => Native.ZkParticleEffectInstance_getVisAlphaFuncS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setVisAlphaFuncS(Handle, value);
		}


		public float VisAlphaStart
		{
			get => Native.ZkParticleEffectInstance_getVisAlphaStart(Handle);
			set => Native.ZkParticleEffectInstance_setVisAlphaStart(Handle, value);
		}

		public float VisAlphaEnd
		{
			get => Native.ZkParticleEffectInstance_getVisAlphaEnd(Handle);
			set => Native.ZkParticleEffectInstance_setVisAlphaEnd(Handle, value);
		}

		public float TrlFadeSpeed
		{
			get => Native.ZkParticleEffectInstance_getTrlFadeSpeed(Handle);
			set => Native.ZkParticleEffectInstance_setTrlFadeSpeed(Handle, value);
		}


		public string TrlTextureS
		{
			get => Native.ZkParticleEffectInstance_getTrlTextureS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setTrlTextureS(Handle, value);
		}


		public float TrlWidth
		{
			get => Native.ZkParticleEffectInstance_getTrlWidth(Handle);
			set => Native.ZkParticleEffectInstance_setTrlWidth(Handle, value);
		}

		public float MrkFadesPeed
		{
			get => Native.ZkParticleEffectInstance_getMrkFadesPeed(Handle);
			set => Native.ZkParticleEffectInstance_setMrkFadesPeed(Handle, value);
		}


		public string MrkTextureS
		{
			get => Native.ZkParticleEffectInstance_getMrktExtureS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setMrktExtureS(Handle, value);
		}


		public float MrkSize
		{
			get => Native.ZkParticleEffectInstance_getMrkSize(Handle);
			set => Native.ZkParticleEffectInstance_setMrkSize(Handle, value);
		}


		public string FlockMode
		{
			get => Native.ZkParticleEffectInstance_getFlockMode(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setFlockMode(Handle, value);
		}


		public float FlockStrength
		{
			get => Native.ZkParticleEffectInstance_getFlockStrength(Handle);
			set => Native.ZkParticleEffectInstance_setFlockStrength(Handle, value);
		}

		public int UseEmittersFor
		{
			get => Native.ZkParticleEffectInstance_getUseEmittersFor(Handle);
			set => Native.ZkParticleEffectInstance_setUseEmittersFor(Handle, value);
		}


		public string TimeStartEndS
		{
			get => Native.ZkParticleEffectInstance_getTimeStartEndS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkParticleEffectInstance_setTimeStartEndS(Handle, value);
		}


		public int MBiasAmbientPfx
		{
			get => Native.ZkParticleEffectInstance_getMBiasAmbientPfx(Handle);
			set => Native.ZkParticleEffectInstance_setMBiasAmbientPfx(Handle, value);
		}
	}
}