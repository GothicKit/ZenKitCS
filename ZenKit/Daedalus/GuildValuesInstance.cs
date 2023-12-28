using System;

namespace ZenKit.Daedalus
{
	public class GuildValuesInstance : DaedalusInstance
	{
		public GuildValuesInstance(UIntPtr handle) : base(handle)
		{
		}

		public int GetWaterDepthKnee(int i)
		{
			return Native.ZkGuildValuesInstance_getWaterDepthKnee(Handle, (ulong)i);
		}

		public int GetWaterDepthChest(int i)
		{
			return Native.ZkGuildValuesInstance_getWaterDepthChest(Handle, (ulong)i);
		}

		public int GetJumpUpHeight(int i)
		{
			return Native.ZkGuildValuesInstance_getJumpUpHeight(Handle, (ulong)i);
		}

		public int GetSwimTime(int i)
		{
			return Native.ZkGuildValuesInstance_getSwimTime(Handle, (ulong)i);
		}

		public int GetDiveTime(int i)
		{
			return Native.ZkGuildValuesInstance_getDiveTime(Handle, (ulong)i);
		}

		public int GetStepHeight(int i)
		{
			return Native.ZkGuildValuesInstance_getStepHeight(Handle, (ulong)i);
		}

		public int GetJumpLowHeight(int i)
		{
			return Native.ZkGuildValuesInstance_getJumpLowHeight(Handle, (ulong)i);
		}

		public int GetJumpMidHeight(int i)
		{
			return Native.ZkGuildValuesInstance_getJumpMidHeight(Handle, (ulong)i);
		}

		public int GetSlideAngle(int i)
		{
			return Native.ZkGuildValuesInstance_getSlideAngle(Handle, (ulong)i);
		}

		public int GetSlideAngle2(int i)
		{
			return Native.ZkGuildValuesInstance_getSlideAngle2(Handle, (ulong)i);
		}

		public int GetDisableAutoRoll(int i)
		{
			return Native.ZkGuildValuesInstance_getDisableAutoRoll(Handle, (ulong)i);
		}

		public int GetSurfaceAlign(int i)
		{
			return Native.ZkGuildValuesInstance_getSurfaceAlign(Handle, (ulong)i);
		}

		public int GetClimbHeadingAngle(int i)
		{
			return Native.ZkGuildValuesInstance_getClimbHeadingAngle(Handle, (ulong)i);
		}

		public int GetClimbHorizAngle(int i)
		{
			return Native.ZkGuildValuesInstance_getClimbHorizAngle(Handle, (ulong)i);
		}

		public int GetClimbGroundAngle(int i)
		{
			return Native.ZkGuildValuesInstance_getClimbGroundAngle(Handle, (ulong)i);
		}

		public int GetFightRangeBase(int i)
		{
			return Native.ZkGuildValuesInstance_getFightRangeBase(Handle, (ulong)i);
		}

		public int GetFightRangeFist(int i)
		{
			return Native.ZkGuildValuesInstance_getFightRangeFist(Handle, (ulong)i);
		}

		public int GetFightRangeG(int i)
		{
			return Native.ZkGuildValuesInstance_getFightRangeG(Handle, (ulong)i);
		}

		public int GetFightRange1Hs(int i)
		{
			return Native.ZkGuildValuesInstance_getFightRange1Hs(Handle, (ulong)i);
		}

		public int GetFightRange1Ha(int i)
		{
			return Native.ZkGuildValuesInstance_getFightRange1Ha(Handle, (ulong)i);
		}

		public int GetFightRange2Hs(int i)
		{
			return Native.ZkGuildValuesInstance_getFightRange2Hs(Handle, (ulong)i);
		}

		public int GetFightRange2Ha(int i)
		{
			return Native.ZkGuildValuesInstance_getFightRange2Ha(Handle, (ulong)i);
		}

		public int GetFallDownHeight(int i)
		{
			return Native.ZkGuildValuesInstance_getFallDownHeight(Handle, (ulong)i);
		}

		public int GetFallDownDamage(int i)
		{
			return Native.ZkGuildValuesInstance_getFallDownDamage(Handle, (ulong)i);
		}

		public int GetBloodDisabled(int i)
		{
			return Native.ZkGuildValuesInstance_getBloodDisabled(Handle, (ulong)i);
		}

		public int GetBloodMaxDistance(int i)
		{
			return Native.ZkGuildValuesInstance_getBloodMaxDistance(Handle, (ulong)i);
		}

		public int GetBloodAmount(int i)
		{
			return Native.ZkGuildValuesInstance_getBloodAmount(Handle, (ulong)i);
		}

		public int GetBloodFlow(int i)
		{
			return Native.ZkGuildValuesInstance_getBloodFlow(Handle, (ulong)i);
		}

		public int GetTurnSpeed(int i)
		{
			return Native.ZkGuildValuesInstance_getTurnSpeed(Handle, (ulong)i);
		}

		public string GetBloodEmitter(int i)
		{
			return Native.ZkGuildValuesInstance_getBloodEmitter(Handle, (ulong)i).MarshalAsString() ?? string.Empty;
		}

		public string GetBloodTexture(int i, int v)
		{
			return Native.ZkGuildValuesInstance_getBloodTexture(Handle, (ulong)i).MarshalAsString() ?? string.Empty;
		}

		public void SetWaterDepthKnee(int i, int v)
		{
			Native.ZkGuildValuesInstance_setWaterDepthKnee(Handle, (ulong)i, v);
		}

		public void SetWaterDepthChest(int i, int v)
		{
			Native.ZkGuildValuesInstance_setWaterDepthChest(Handle, (ulong)i, v);
		}

		public void SetJumpUpHeight(int i, int v)
		{
			Native.ZkGuildValuesInstance_setJumpUpHeight(Handle, (ulong)i, v);
		}

		public void SetSwimTime(int i, int v)
		{
			Native.ZkGuildValuesInstance_setSwimTime(Handle, (ulong)i, v);
		}

		public void SetDiveTime(int i, int v)
		{
			Native.ZkGuildValuesInstance_setDiveTime(Handle, (ulong)i, v);
		}

		public void SetStepHeight(int i, int v)
		{
			Native.ZkGuildValuesInstance_setStepHeight(Handle, (ulong)i, v);
		}

		public void SetJumpLowHeight(int i, int v)
		{
			Native.ZkGuildValuesInstance_setJumpLowHeight(Handle, (ulong)i, v);
		}

		public void SetJumpMidHeight(int i, int v)
		{
			Native.ZkGuildValuesInstance_setJumpMidHeight(Handle, (ulong)i, v);
		}

		public void SetSlideAngle(int i, int v)
		{
			Native.ZkGuildValuesInstance_setSlideAngle(Handle, (ulong)i, v);
		}

		public void SetSlideAngle2(int i, int v)
		{
			Native.ZkGuildValuesInstance_setSlideAngle2(Handle, (ulong)i, v);
		}

		public void SetDisableAutoRoll(int i, int v)
		{
			Native.ZkGuildValuesInstance_setDisableAutoRoll(Handle, (ulong)i, v);
		}

		public void SetSurfaceAlign(int i, int v)
		{
			Native.ZkGuildValuesInstance_setSurfaceAlign(Handle, (ulong)i, v);
		}

		public void SetClimbHeadingAngle(int i, int v)
		{
			Native.ZkGuildValuesInstance_setClimbHeadingAngle(Handle, (ulong)i, v);
		}

		public void SetClimbHorizAngle(int i, int v)
		{
			Native.ZkGuildValuesInstance_setClimbHorizAngle(Handle, (ulong)i, v);
		}

		public void SetClimbGroundAngle(int i, int v)
		{
			Native.ZkGuildValuesInstance_setClimbGroundAngle(Handle, (ulong)i, v);
		}

		public void SetFightRangeBase(int i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRangeBase(Handle, (ulong)i, v);
		}

		public void SetFightRangeFist(int i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRangeFist(Handle, (ulong)i, v);
		}

		public void SetFightRangeG(int i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRangeG(Handle, (ulong)i, v);
		}

		public void SetFightRange1Hs(int i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRange1Hs(Handle, (ulong)i, v);
		}

		public void SetFightRange1Ha(int i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRange1Ha(Handle, (ulong)i, v);
		}

		public void SetFightRange2Hs(int i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRange2Hs(Handle, (ulong)i, v);
		}

		public void SetFightRange2Ha(int i, int v)
		{
			Native.ZkGuildValuesInstance_setFightRange2Ha(Handle, (ulong)i, v);
		}

		public void SetFallDownHeight(int i, int v)
		{
			Native.ZkGuildValuesInstance_setFallDownHeight(Handle, (ulong)i, v);
		}

		public void SetFallDownDamage(int i, int v)
		{
			Native.ZkGuildValuesInstance_setFallDownDamage(Handle, (ulong)i, v);
		}

		public void SetBloodDisabled(int i, int v)
		{
			Native.ZkGuildValuesInstance_setBloodDisabled(Handle, (ulong)i, v);
		}

		public void SetBloodMaxDistance(int i, int v)
		{
			Native.ZkGuildValuesInstance_setBloodMaxDistance(Handle, (ulong)i, v);
		}

		public void SetBloodAmount(int i, int v)
		{
			Native.ZkGuildValuesInstance_setBloodAmount(Handle, (ulong)i, v);
		}

		public void SetBloodFlow(int i, int v)
		{
			Native.ZkGuildValuesInstance_setBloodFlow(Handle, (ulong)i, v);
		}

		public void SetTurnSpeed(int i, int v)
		{
			Native.ZkGuildValuesInstance_setTurnSpeed(Handle, (ulong)i, v);
		}

		public void SetBloodEmitter(int i, string s)
		{
			Native.ZkGuildValuesInstance_setBloodEmitter(Handle, (ulong)i, s);
		}

		public void SetBloodTexture(int i, string s)
		{
			Native.ZkGuildValuesInstance_setBloodTexture(Handle, (ulong)i, s);
		}
	}
}