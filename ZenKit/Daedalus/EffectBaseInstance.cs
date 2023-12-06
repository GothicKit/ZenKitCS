using System;

namespace ZenKit.Daedalus
{
	public class EffectBaseInstance : DaedalusInstance
	{
		public EffectBaseInstance(UIntPtr handle) : base(handle)
		{
		}

		public string VisNameS
		{
			get => Native.ZkEffectBaseInstance_getVisNameS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setVisNameS(Handle, value);
		}

		public string VisSizeS
		{
			get => Native.ZkEffectBaseInstance_getVisSizeS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setVisSizeS(Handle, value);
		}

		public float VisAlpha
		{
			get => Native.ZkEffectBaseInstance_getVisAlpha(Handle);
			set => Native.ZkEffectBaseInstance_setVisAlpha(Handle, value);
		}


		public string VisAlphaBlendFuncS
		{
			get => Native.ZkEffectBaseInstance_getVisAlphaBlendFuncS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setVisAlphaBlendFuncS(Handle, value);
		}


		public float VisTexAniFps
		{
			get => Native.ZkEffectBaseInstance_getVisTexAniFps(Handle);
			set => Native.ZkEffectBaseInstance_setVisTexAniFps(Handle, value);
		}

		public int VisTexAniIsLooping
		{
			get => Native.ZkEffectBaseInstance_getVisTexAniIsLooping(Handle);
			set => Native.ZkEffectBaseInstance_setVisTexAniIsLooping(Handle, value);
		}

		public string EmTrjModeS
		{
			get => Native.ZkEffectBaseInstance_getEmTrjModeS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmTrjModeS(Handle, value);
		}


		public string EmTrjOriginNode
		{
			get => Native.ZkEffectBaseInstance_getEmTrjOriginNode(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmTrjOriginNode(Handle, value);
		}


		public string EmTrjTargetNode
		{
			get => Native.ZkEffectBaseInstance_getEmTrjTargetNode(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmTrjTargetNode(Handle, value);
		}


		public float EmTrjTargetRange
		{
			get => Native.ZkEffectBaseInstance_getEmTrjTargetRange(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjTargetRange(Handle, value);
		}

		public float EmTrjTargetAzi
		{
			get => Native.ZkEffectBaseInstance_getEmTrjTargetAzi(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjTargetAzi(Handle, value);
		}

		public float EmTrjTargetElev
		{
			get => Native.ZkEffectBaseInstance_getEmTrjTargetElev(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjTargetElev(Handle, value);
		}

		public int EmTrjNumKeys
		{
			get => Native.ZkEffectBaseInstance_getEmTrjNumKeys(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjNumKeys(Handle, value);
		}

		public int EmTrjNumKeysVar
		{
			get => Native.ZkEffectBaseInstance_getEmTrjNumKeysVar(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjNumKeysVar(Handle, value);
		}

		public float EmTrjAngleElevVar
		{
			get => Native.ZkEffectBaseInstance_getEmTrjAngleElevVar(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjAngleElevVar(Handle, value);
		}

		public float EmTrjAngleHeadVar
		{
			get => Native.ZkEffectBaseInstance_getEmTrjAngleHeadVar(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjAngleHeadVar(Handle, value);
		}

		public float EmTrjKeyDistVar
		{
			get => Native.ZkEffectBaseInstance_getEmTrjKeyDistVar(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjKeyDistVar(Handle, value);
		}


		public string EmTrjLoopModeS
		{
			get => Native.ZkEffectBaseInstance_getEmTrjLoopModeS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmTrjLoopModeS(Handle, value);
		}


		public string EmTrjEaseFuncS
		{
			get => Native.ZkEffectBaseInstance_getEmTrjEaseFuncS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmTrjEaseFuncS(Handle, value);
		}


		public float EmTrjEaseVel
		{
			get => Native.ZkEffectBaseInstance_getEmTrjEaseVel(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjEaseVel(Handle, value);
		}

		public float EmTrjDynUpdateDelay
		{
			get => Native.ZkEffectBaseInstance_getEmTrjDynUpdateDelay(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjDynUpdateDelay(Handle, value);
		}

		public int EmTrjDynUpdateTargetOnly
		{
			get => Native.ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly(Handle);
			set => Native.ZkEffectBaseInstance_setEmTrjDynUpdateTargetOnly(Handle, value);
		}


		public string EmFxCreateS
		{
			get => Native.ZkEffectBaseInstance_getEmFxCreateS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmFxCreateS(Handle, value);
		}


		public string EmFxInvestOriginS
		{
			get => Native.ZkEffectBaseInstance_getEmFxInvestOriginS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmFxInvestOriginS(Handle, value);
		}


		public string EmFxInvestTargetS
		{
			get => Native.ZkEffectBaseInstance_getEmFxInvestTargetS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmFxInvestTargetS(Handle, value);
		}


		public float EmFxTriggerDelay
		{
			get => Native.ZkEffectBaseInstance_getEmFxTriggerDelay(Handle);
			set => Native.ZkEffectBaseInstance_setEmFxTriggerDelay(Handle, value);
		}

		public int EmFxCreateDownTrj
		{
			get => Native.ZkEffectBaseInstance_getEmFxCreateDownTrj(Handle);
			set => Native.ZkEffectBaseInstance_setEmFxCreateDownTrj(Handle, value);
		}


		public string EmActionCollDynS
		{
			get => Native.ZkEffectBaseInstance_getEmActionCollDynS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmActionCollDynS(Handle, value);
		}


		public string EmActionCollStatS
		{
			get => Native.ZkEffectBaseInstance_getEmActionCollStatS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmActionCollStatS(Handle, value);
		}


		public string EmFxCollStatS
		{
			get => Native.ZkEffectBaseInstance_getEmFxCollStatS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmFxCollStatS(Handle, value);
		}


		public string EmFxCollDynS
		{
			get => Native.ZkEffectBaseInstance_getEmFxCollDynS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmFxCollDynS(Handle, value);
		}


		public string EmFxCollStatAlignS
		{
			get => Native.ZkEffectBaseInstance_getEmFxCollStatAlignS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmFxCollStatAlignS(Handle, value);
		}


		public string EmFxCollDynAlignS
		{
			get => Native.ZkEffectBaseInstance_getEmFxCollDynAlignS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmFxCollDynAlignS(Handle, value);
		}


		public float EmFxLifespan
		{
			get => Native.ZkEffectBaseInstance_getEmFxLifespan(Handle);
			set => Native.ZkEffectBaseInstance_setEmFxLifespan(Handle, value);
		}

		public int EmCheckCollision
		{
			get => Native.ZkEffectBaseInstance_getEmCheckCollision(Handle);
			set => Native.ZkEffectBaseInstance_setEmCheckCollision(Handle, value);
		}

		public int EmAdjustShpToOrigin
		{
			get => Native.ZkEffectBaseInstance_getEmAdjustShpToOrigin(Handle);
			set => Native.ZkEffectBaseInstance_setEmAdjustShpToOrigin(Handle, value);
		}

		public float EmInvestNextKeyDuration
		{
			get => Native.ZkEffectBaseInstance_getEmInvestNextKeyDuration(Handle);
			set => Native.ZkEffectBaseInstance_setEmInvestNextKeyDuration(Handle, value);
		}

		public float EmFlyGravity
		{
			get => Native.ZkEffectBaseInstance_getEmFlyGravity(Handle);
			set => Native.ZkEffectBaseInstance_setEmFlyGravity(Handle, value);
		}


		public string EmSelfRotVelS
		{
			get => Native.ZkEffectBaseInstance_getEmSelfRotVelS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmSelfRotVelS(Handle, value);
		}


		public string LightPresetName
		{
			get => Native.ZkEffectBaseInstance_getLightPresetName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setLightPresetName(Handle, value);
		}


		public string SfxId
		{
			get => Native.ZkEffectBaseInstance_getSfxId(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setSfxId(Handle, value);
		}

		public int SfxIsAmbient
		{
			get => Native.ZkEffectBaseInstance_getSfxIsAmbient(Handle);
			set => Native.ZkEffectBaseInstance_setSfxIsAmbient(Handle, value);
		}

		public int SendAssessMagic
		{
			get => Native.ZkEffectBaseInstance_getSendAssessMagic(Handle);
			set => Native.ZkEffectBaseInstance_setSendAssessMagic(Handle, value);
		}

		public float SecsPerDamage
		{
			get => Native.ZkEffectBaseInstance_getSecsPerDamage(Handle);
			set => Native.ZkEffectBaseInstance_setSecsPerDamage(Handle, value);
		}


		public string EmFxCollDynPercS
		{
			get => Native.ZkEffectBaseInstance_getEmFxCollDynPercS(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkEffectBaseInstance_setEmFxCollDynPercS(Handle, value);
		}


		public string GetUserString(ulong i)
		{
			return Native.ZkEffectBaseInstance_getUserString(Handle, i).MarshalAsString() ?? string.Empty;
		}

		public void SetUserString(ulong i, string userString)
		{
			Native.ZkEffectBaseInstance_setUserString(Handle, i, userString);
		}
	}
}