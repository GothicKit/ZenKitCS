using System;

namespace ZenKit.Daedalus
{
	public class EffectBaseInstance : DaedalusInstance
	{
		public EffectBaseInstance(UIntPtr handle) : base(handle)
		{
		}
	
		public string VisNameS => Native.ZkEffectBaseInstance_getVisNameS(Handle).MarshalAsString() ?? string.Empty;
		public string VisSizeS => Native.ZkEffectBaseInstance_getVisSizeS(Handle).MarshalAsString() ?? string.Empty;
		public float VisAlpha => Native.ZkEffectBaseInstance_getVisAlpha(Handle);
		public string VisAlphaBlendFuncS => Native.ZkEffectBaseInstance_getVisAlphaBlendFuncS(Handle).MarshalAsString() ?? string.Empty;
		public float VisTexAniFps => Native.ZkEffectBaseInstance_getVisTexAniFps(Handle);
		public int VisTexAniIsLooping => Native.ZkEffectBaseInstance_getVisTexAniIsLooping(Handle);
		public string EmTrjModeS => Native.ZkEffectBaseInstance_getEmTrjModeS(Handle).MarshalAsString() ?? string.Empty;
		public string EmTrjOriginNode => Native.ZkEffectBaseInstance_getEmTrjOriginNode(Handle).MarshalAsString() ?? string.Empty;
		public string EmTrjTargetNode => Native.ZkEffectBaseInstance_getEmTrjTargetNode(Handle).MarshalAsString() ?? string.Empty;
		public float EmTrjTargetRange => Native.ZkEffectBaseInstance_getEmTrjTargetRange(Handle);
		public float EmTrjTargetAzi => Native.ZkEffectBaseInstance_getEmTrjTargetAzi(Handle);
		public float EmTrjTargetElev => Native.ZkEffectBaseInstance_getEmTrjTargetElev(Handle);
		public int EmTrjNumKeys => Native.ZkEffectBaseInstance_getEmTrjNumKeys(Handle);
		public int EmTrjNumKeysVar => Native.ZkEffectBaseInstance_getEmTrjNumKeysVar(Handle);
		public float EmTrjAngleElevVar => Native.ZkEffectBaseInstance_getEmTrjAngleElevVar(Handle);
		public float EmTrjAngleHeadVar => Native.ZkEffectBaseInstance_getEmTrjAngleHeadVar(Handle);
		public float EmTrjKeyDistVar => Native.ZkEffectBaseInstance_getEmTrjKeyDistVar(Handle);
		public string EmTrjLoopModeS => Native.ZkEffectBaseInstance_getEmTrjLoopModeS(Handle).MarshalAsString() ?? string.Empty;
		public string EmTrjEaseFuncS => Native.ZkEffectBaseInstance_getEmTrjEaseFuncS(Handle).MarshalAsString() ?? string.Empty;
		public float EmTrjEaseVel => Native.ZkEffectBaseInstance_getEmTrjEaseVel(Handle);
		public float EmTrjDynUpdateDelay => Native.ZkEffectBaseInstance_getEmTrjDynUpdateDelay(Handle);
		public int EmTrjDynUpdateTargetOnly => Native.ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly(Handle);
		public string EmFxCreateS => Native.ZkEffectBaseInstance_getEmFxCreateS(Handle).MarshalAsString() ?? string.Empty;
		public string EmFxInvestOriginS => Native.ZkEffectBaseInstance_getEmFxInvestOriginS(Handle).MarshalAsString() ?? string.Empty;
		public string EmFxInvestTargetS => Native.ZkEffectBaseInstance_getEmFxInvestTargetS(Handle).MarshalAsString() ?? string.Empty;
		public float EmFxTriggerDelay => Native.ZkEffectBaseInstance_getEmFxTriggerDelay(Handle);
		public int EmFxCreateDownTrj => Native.ZkEffectBaseInstance_getEmFxCreateDownTrj(Handle);
		public string EmActionCollDynS => Native.ZkEffectBaseInstance_getEmActionCollDynS(Handle).MarshalAsString() ?? string.Empty;
		public string EmActionCollStatS => Native.ZkEffectBaseInstance_getEmActionCollStatS(Handle).MarshalAsString() ?? string.Empty;
		public string EmFxCollStatS => Native.ZkEffectBaseInstance_getEmFxCollStatS(Handle).MarshalAsString() ?? string.Empty;
		public string EmFxCollDynS => Native.ZkEffectBaseInstance_getEmFxCollDynS(Handle).MarshalAsString() ?? string.Empty;
		public string EmFxCollStatAlignS => Native.ZkEffectBaseInstance_getEmFxCollStatAlignS(Handle).MarshalAsString() ?? string.Empty;
		public string EmFxCollDynAlignS => Native.ZkEffectBaseInstance_getEmFxCollDynAlignS(Handle).MarshalAsString() ?? string.Empty;
		public float EmFxLifespan => Native.ZkEffectBaseInstance_getEmFxLifespan(Handle);
		public int EmCheckCollision => Native.ZkEffectBaseInstance_getEmCheckCollision(Handle);
		public int EmAdjustShpToOrigin => Native.ZkEffectBaseInstance_getEmAdjustShpToOrigin(Handle);
		public float EmInvestNextKeyDuration => Native.ZkEffectBaseInstance_getEmInvestNextKeyDuration(Handle);
		public float EmFlyGravity => Native.ZkEffectBaseInstance_getEmFlyGravity(Handle);
		public string EmSelfRotVelS => Native.ZkEffectBaseInstance_getEmSelfRotVelS(Handle).MarshalAsString() ?? string.Empty;
		public string LightPresetName => Native.ZkEffectBaseInstance_getLightPresetName(Handle).MarshalAsString() ?? string.Empty;
		public string SfxId => Native.ZkEffectBaseInstance_getSfxId(Handle).MarshalAsString() ?? string.Empty;
		public int SfxIsAmbient => Native.ZkEffectBaseInstance_getSfxIsAmbient(Handle);
		public int SendAssessMagic => Native.ZkEffectBaseInstance_getSendAssessMagic(Handle);
		public float SecsPerDamage => Native.ZkEffectBaseInstance_getSecsPerDamage(Handle);
		public string EmFxCollDynPercS => Native.ZkEffectBaseInstance_getEmFxCollDynPercS(Handle).MarshalAsString() ?? string.Empty;
		public string UserString(ulong i) => Native.ZkEffectBaseInstance_getUserString(Handle, i).MarshalAsString() ?? string.Empty;
	}
}