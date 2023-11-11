using System;

namespace ZenKit.Daedalus
{
	public class ParticleEffectInstance : DaedalusInstance
	{
		public ParticleEffectInstance(UIntPtr handle) : base(handle)
		{
		}

		public float PpsValue => Native.ZkParticleEffectInstance_getPpsValue(Handle);
		public string PpsScaleKeysS => Native.ZkParticleEffectInstance_getPpsScaleKeysS(Handle).MarshalAsString() ?? string.Empty;
		public int PpsIsLooping => Native.ZkParticleEffectInstance_getPpsIsLooping(Handle);
		public int PpsIsSmooth => Native.ZkParticleEffectInstance_getPpsIsSmooth(Handle);
		public float PpsFps => Native.ZkParticleEffectInstance_getPpsFps(Handle);
		public string PpsCreateEmS => Native.ZkParticleEffectInstance_getPpsCreateEmS(Handle).MarshalAsString() ?? string.Empty;
		public float PpsCreateEmDelay => Native.ZkParticleEffectInstance_getPpsCreateEmDelay(Handle);
		public string ShpTypeS => Native.ZkParticleEffectInstance_getShpTypeS(Handle).MarshalAsString() ?? string.Empty;
		public string ShpForS => Native.ZkParticleEffectInstance_getShpForS(Handle).MarshalAsString() ?? string.Empty;
		public string ShpOffsetVecS => Native.ZkParticleEffectInstance_getShpOffsetVecS(Handle).MarshalAsString() ?? string.Empty;

		public string ShpDistributionTypeS =>
			Native.ZkParticleEffectInstance_getShpDistribTypeS(Handle).MarshalAsString() ?? string.Empty;

		public float ShpDistributionWalkSpeed => Native.ZkParticleEffectInstance_getShpDistribWalkSpeed(Handle);
		public int ShpIsVolume => Native.ZkParticleEffectInstance_getShpIsVolume(Handle);
		public string ShpDimS => Native.ZkParticleEffectInstance_getShpDimS(Handle).MarshalAsString() ?? string.Empty;
		public string ShpMeshS => Native.ZkParticleEffectInstance_getShpMeshS(Handle).MarshalAsString() ?? string.Empty;
		public int ShpMeshRenderB => Native.ZkParticleEffectInstance_getShpMeshRenderB(Handle);
		public string ShpScaleKeysS => Native.ZkParticleEffectInstance_getShpScaleKeysS(Handle).MarshalAsString() ?? string.Empty;
		public int ShpScaleIsLooping => Native.ZkParticleEffectInstance_getShpScaleIsLooping(Handle);
		public int ShpScaleIsSmooth => Native.ZkParticleEffectInstance_getShpScaleIsSmooth(Handle);
		public float ShpScaleFps => Native.ZkParticleEffectInstance_getShpScaleFps(Handle);
		public string DirModeS => Native.ZkParticleEffectInstance_getDirModeS(Handle).MarshalAsString() ?? string.Empty;
		public string DirForS => Native.ZkParticleEffectInstance_getDirForS(Handle).MarshalAsString() ?? string.Empty;

		public string DirModeTargetForS =>
			Native.ZkParticleEffectInstance_getDirModeTargetForS(Handle).MarshalAsString() ?? string.Empty;

		public string DirModeTargetPosS =>
			Native.ZkParticleEffectInstance_getDirModeTargetPosS(Handle).MarshalAsString() ?? string.Empty;

		public float DirAngleHead => Native.ZkParticleEffectInstance_getDirAngleHead(Handle);
		public float DirAngleHeadVar => Native.ZkParticleEffectInstance_getDirAngleHeadVar(Handle);
		public float DirAngleElev => Native.ZkParticleEffectInstance_getDirAngleElev(Handle);
		public float DirAngleElevVar => Native.ZkParticleEffectInstance_getDirAngleElevVar(Handle);
		public float VelAvg => Native.ZkParticleEffectInstance_getVelAvg(Handle);
		public float VelVar => Native.ZkParticleEffectInstance_getVelVar(Handle);
		public float LspPartAvg => Native.ZkParticleEffectInstance_getLspPartAvg(Handle);
		public float LspPartVar => Native.ZkParticleEffectInstance_getLspPartVar(Handle);
		public string FlyGravityS => Native.ZkParticleEffectInstance_getFlyGravityS(Handle).MarshalAsString() ?? string.Empty;
		public int FlyCollisionDetectionB => Native.ZkParticleEffectInstance_getFlyColldetB(Handle);
		public string VisNameS => Native.ZkParticleEffectInstance_getVisNameS(Handle).MarshalAsString() ?? string.Empty;

		public string VisOrientationS =>
			Native.ZkParticleEffectInstance_getVisOrientationS(Handle).MarshalAsString() ?? string.Empty;

		public int VisTexIsQuadPoly => Native.ZkParticleEffectInstance_getVisTexIsQuadpoly(Handle);
		public float VisTexAniFps => Native.ZkParticleEffectInstance_getVisTexAniFps(Handle);
		public int VisTexAniIsLooping => Native.ZkParticleEffectInstance_getVisTexAniIsLooping(Handle);

		public string VisTexColorStartS =>
			Native.ZkParticleEffectInstance_getVisTexColorStartS(Handle).MarshalAsString() ?? string.Empty;

		public string VisTexColorEndS =>
			Native.ZkParticleEffectInstance_getVisTexColorEndS(Handle).MarshalAsString() ?? string.Empty;

		public string VisSizeStartS => Native.ZkParticleEffectInstance_getVisSizeStartS(Handle).MarshalAsString() ?? string.Empty;
		public float VisSizeEndScale => Native.ZkParticleEffectInstance_getVisSizeEndScale(Handle);
		public string VisAlphaFuncS => Native.ZkParticleEffectInstance_getVisAlphaFuncS(Handle).MarshalAsString() ?? string.Empty;
		public float VisAlphaStart => Native.ZkParticleEffectInstance_getVisAlphaStart(Handle);
		public float VisAlphaEnd => Native.ZkParticleEffectInstance_getVisAlphaEnd(Handle);
		public float TrlFadeSpeed => Native.ZkParticleEffectInstance_getTrlFadeSpeed(Handle);
		public string TrlTextureS => Native.ZkParticleEffectInstance_getTrlTextureS(Handle).MarshalAsString() ?? string.Empty;
		public float TrlWidth => Native.ZkParticleEffectInstance_getTrlWidth(Handle);
		public float MrkFadesPeed => Native.ZkParticleEffectInstance_getMrkFadesPeed(Handle);
		public string MrkTextureS => Native.ZkParticleEffectInstance_getMrktExtureS(Handle).MarshalAsString() ?? string.Empty;
		public float MrkSize => Native.ZkParticleEffectInstance_getMrkSize(Handle);
		public string FlockMode => Native.ZkParticleEffectInstance_getFlockMode(Handle).MarshalAsString() ?? string.Empty;
		public float FlockStrength => Native.ZkParticleEffectInstance_getFlockStrength(Handle);
		public int UseEmittersFor => Native.ZkParticleEffectInstance_getUseEmittersFor(Handle);
		public string TimeStartEndS => Native.ZkParticleEffectInstance_getTimeStartEndS(Handle).MarshalAsString() ?? string.Empty;
		public int MBiasAmbientPfx => Native.ZkParticleEffectInstance_getMBiasAmbientPfx(Handle);
	}
}