using System;

namespace ZenKit.Daedalus
{
	public class GuildValuesInstance : DaedalusInstance
	{
		public GuildValuesInstance(UIntPtr handle) : base(handle)
		{
		}

		public int WaterDepthKnee(ulong i)
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

		public string GetBloodTexture(ulong i)
		{
			return Native.ZkGuildValuesInstance_getBloodTexture(Handle, i).MarshalAsString() ?? string.Empty;
		}
	}
}