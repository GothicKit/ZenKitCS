using System;

namespace ZenKit.Daedalus
{
	public class GuildValuesInstance : DaedalusInstance
	{
		public GuildValuesInstance(UIntPtr handle) : base(handle)
		{
		}

		public int GetWaterDepthKnee(ulong i)
		{
			return Native.ZkGuildValuesInstance_getWaterDepthKnee(Handle, i);
		}

		public int GetWaterDepthChest(ulong i)
		{
			return Native.ZkGuildValuesInstance_getWaterDepthChest(Handle, i);
		}

		public int GetJumpUpHeight(ulong i)
		{
			return Native.ZkGuildValuesInstance_getJumpUpHeight(Handle, i);
		}

		public int GetSwimTime(ulong i)
		{
			return Native.ZkGuildValuesInstance_getSwimTime(Handle, i);
		}

		public int GetDiveTime(ulong i)
		{
			return Native.ZkGuildValuesInstance_getDiveTime(Handle, i);
		}

		public int GetStepHeight(ulong i)
		{
			return Native.ZkGuildValuesInstance_getStepHeight(Handle, i);
		}

		public int GetJumpLowHeight(ulong i)
		{
			return Native.ZkGuildValuesInstance_getJumpLowHeight(Handle, i);
		}

		public int GetJumpMidHeight(ulong i)
		{
			return Native.ZkGuildValuesInstance_getJumpMidHeight(Handle, i);
		}

		public int GetSlideAngle(ulong i)
		{
			return Native.ZkGuildValuesInstance_getSlideAngle(Handle, i);
		}

		public int GetSlideAngle2(ulong i)
		{
			return Native.ZkGuildValuesInstance_getSlideAngle2(Handle, i);
		}

		public int GetDisableAutoRoll(ulong i)
		{
			return Native.ZkGuildValuesInstance_getDisableAutoRoll(Handle, i);
		}

		public int GetSurfaceAlign(ulong i)
		{
			return Native.ZkGuildValuesInstance_getSurfaceAlign(Handle, i);
		}

		public int GetClimbHeadingAngle(ulong i)
		{
			return Native.ZkGuildValuesInstance_getClimbHeadingAngle(Handle, i);
		}

		public int GetClimbHorizAngle(ulong i)
		{
			return Native.ZkGuildValuesInstance_getClimbHorizAngle(Handle, i);
		}

		public int GetClimbGroundAngle(ulong i)
		{
			return Native.ZkGuildValuesInstance_getClimbGroundAngle(Handle, i);
		}

		public int GetFightRangeBase(ulong i)
		{
			return Native.ZkGuildValuesInstance_getFightRangeBase(Handle, i);
		}

		public int GetFightRangeFist(ulong i)
		{
			return Native.ZkGuildValuesInstance_getFightRangeFist(Handle, i);
		}

		public int GetFightRangeG(ulong i)
		{
			return Native.ZkGuildValuesInstance_getFightRangeG(Handle, i);
		}

		public int GetFightRange1Hs(ulong i)
		{
			return Native.ZkGuildValuesInstance_getFightRange1Hs(Handle, i);
		}

		public int GetFightRange1Ha(ulong i)
		{
			return Native.ZkGuildValuesInstance_getFightRange1Ha(Handle, i);
		}

		public int GetFightRange2Hs(ulong i)
		{
			return Native.ZkGuildValuesInstance_getFightRange2Hs(Handle, i);
		}

		public int GetFightRange2Ha(ulong i)
		{
			return Native.ZkGuildValuesInstance_getFightRange2Ha(Handle, i);
		}

		public int GetFallDownHeight(ulong i)
		{
			return Native.ZkGuildValuesInstance_getFallDownHeight(Handle, i);
		}

		public int GetFallDownDamage(ulong i)
		{
			return Native.ZkGuildValuesInstance_getFallDownDamage(Handle, i);
		}

		public int GetBloodDisabled(ulong i)
		{
			return Native.ZkGuildValuesInstance_getBloodDisabled(Handle, i);
		}

		public int GetBloodMaxDistance(ulong i)
		{
			return Native.ZkGuildValuesInstance_getBloodMaxDistance(Handle, i);
		}

		public int GetBloodAmount(ulong i)
		{
			return Native.ZkGuildValuesInstance_getBloodAmount(Handle, i);
		}

		public int GetBloodFlow(ulong i)
		{
			return Native.ZkGuildValuesInstance_getBloodFlow(Handle, i);
		}

		public int GetTurnSpeed(ulong i)
		{
			return Native.ZkGuildValuesInstance_getTurnSpeed(Handle, i);
		}

		public string GetBloodEmitter(ulong i)
		{
			return Native.ZkGuildValuesInstance_getBloodEmitter(Handle, i).MarshalAsString() ?? string.Empty;
		}

		public string GetBloodTexture(ulong i, int v)
		{
			return Native.ZkGuildValuesInstance_getBloodTexture(Handle, i).MarshalAsString() ?? string.Empty;
		}

		public void SetWaterDepthKnee(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setWaterDepthKnee(Handle, i, v);
		}

		public void SetWaterDepthChest(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setWaterDepthChest(Handle, i, v);
		}

		public void SetJumpUpHeight(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setJumpUpHeight(Handle, i, v);
		}

		public void SetSwimTime(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setSwimTime(Handle, i, v);
		}

		public void SetDiveTime(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setDiveTime(Handle, i, v);
		}

		public void SetStepHeight(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setStepHeight(Handle, i, v);
		}

		public void SetJumpLowHeight(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setJumpLowHeight(Handle, i, v);
		}

		public void SetJumpMidHeight(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setJumpMidHeight(Handle, i, v);
		}

		public void SetSlideAngle(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setSlideAngle(Handle, i, v);
		}

		public void SetSlideAngle2(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setSlideAngle2(Handle, i, v);
		}

		public void SetDisableAutoRoll(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setDisableAutoRoll(Handle, i, v);
		}

		public void SetSurfaceAlign(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setSurfaceAlign(Handle, i, v);
		}

		public void SetClimbHeadingAngle(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setClimbHeadingAngle(Handle, i, v);
		}

		public void SetClimbHorizAngle(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setClimbHorizAngle(Handle, i, v);
		}

		public void SetClimbGroundAngle(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setClimbGroundAngle(Handle, i, v);
		}

		public void SetFightRangeBase(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRangeBase(Handle, i, v);
		}

		public void SetFightRangeFist(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRangeFist(Handle, i, v);
		}

		public void SetFightRangeG(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRangeG(Handle, i, v);
		}

		public void SetFightRange1Hs(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRange1Hs(Handle, i, v);
		}

		public void SetFightRange1Ha(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRange1Ha(Handle, i, v);
		}

		public void SetFightRange2Hs(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRange2Hs(Handle, i, v);
		}

		public void SetFightRange2Ha(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRange2Ha(Handle, i, v);
		}

		public void SetFallDownHeight(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setFallDownHeight(Handle, i, v);
		}

		public void SetFallDownDamage(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setFallDownDamage(Handle, i, v);
		}

		public void SetBloodDisabled(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setBloodDisabled(Handle, i, v);
		}

		public void SetBloodMaxDistance(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setBloodMaxDistance(Handle, i, v);
		}

		public void SetBloodAmount(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setBloodAmount(Handle, i, v);
		}

		public void SetBloodFlow(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setBloodFlow(Handle, i, v);
		}

		public void SetTurnSpeed(ulong i, int v)
		{
			Native.ZkGuildValuesInstance_setTurnSpeed(Handle, i, v);
		}

		public void SetBloodEmitter(ulong i, string s)
		{
			Native.ZkGuildValuesInstance_setBloodEmitter(Handle, i, s);
		}

		public void SetBloodTexture(ulong i, string s)
		{
			Native.ZkGuildValuesInstance_setBloodTexture(Handle, i, s);
		}
	}
}