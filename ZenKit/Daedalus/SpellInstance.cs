using System;

namespace ZenKit.Daedalus
{
	public class SpellInstance : DaedalusInstance
	{
		public SpellInstance(UIntPtr handle) : base(handle)
		{
		}

		public float TimePerMana
		{
			get => Native.ZkSpellInstance_getTimePerMana(Handle);
			set => Native.ZkSpellInstance_setTimePerMana(Handle, value);
		}

		public int DamagePerLevel
		{
			get => Native.ZkSpellInstance_getDamagePerLevel(Handle);
			set => Native.ZkSpellInstance_setDamagePerLevel(Handle, value);
		}

		public int DamageType
		{
			get => Native.ZkSpellInstance_getDamageType(Handle);
			set => Native.ZkSpellInstance_setDamageType(Handle, value);
		}

		public int SpellType
		{
			get => Native.ZkSpellInstance_getSpellType(Handle);
			set => Native.ZkSpellInstance_setSpellType(Handle, value);
		}

		public int CanTurnDuringInvest
		{
			get => Native.ZkSpellInstance_getCanTurnDuringInvest(Handle);
			set => Native.ZkSpellInstance_setCanTurnDuringInvest(Handle, value);
		}

		public int CanChangeTargetDuringInvest
		{
			get => Native.ZkSpellInstance_getCanChangeTargetDuringInvest(Handle);
			set => Native.ZkSpellInstance_setCanChangeTargetDuringInvest(Handle, value);
		}

		public int IsMultiEffect
		{
			get => Native.ZkSpellInstance_getIsMultiEffect(Handle);
			set => Native.ZkSpellInstance_setIsMultiEffect(Handle, value);
		}

		public int TargetCollectAlgo
		{
			get => Native.ZkSpellInstance_getTargetCollectAlgo(Handle);
			set => Native.ZkSpellInstance_setTargetCollectAlgo(Handle, value);
		}

		public int TargetCollectType
		{
			get => Native.ZkSpellInstance_getTargetCollectType(Handle);
			set => Native.ZkSpellInstance_setTargetCollectType(Handle, value);
		}

		public int TargetCollectRange
		{
			get => Native.ZkSpellInstance_getTargetCollectRange(Handle);
			set => Native.ZkSpellInstance_setTargetCollectRange(Handle, value);
		}

		public int TargetCollectAzi
		{
			get => Native.ZkSpellInstance_getTargetCollectAzi(Handle);
			set => Native.ZkSpellInstance_setTargetCollectAzi(Handle, value);
		}

		public int TargetCollectElevation
		{
			get => Native.ZkSpellInstance_getTargetCollectElevation(Handle);
			set => Native.ZkSpellInstance_setTargetCollectElevation(Handle, value);
		}
	}
}