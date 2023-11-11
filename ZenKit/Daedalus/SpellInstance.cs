using System;

namespace ZenKit.Daedalus
{
	public class SpellInstance : DaedalusInstance
	{
		public SpellInstance(UIntPtr handle) : base(handle)
		{
		}
	
		public float TimePerMana => Native.ZkSpellInstance_getTimePerMana(Handle);
		public int DamagePerLevel => Native.ZkSpellInstance_getDamagePerLevel(Handle);
		public int DamageType => Native.ZkSpellInstance_getDamageType(Handle);
		public int SpellType => Native.ZkSpellInstance_getSpellType(Handle);
		public int CanTurnDuringInvest => Native.ZkSpellInstance_getCanTurnDuringInvest(Handle);
		public int CanChangeTargetDuringInvest => Native.ZkSpellInstance_getCanChangeTargetDuringInvest(Handle);
		public int IsMultiEffect => Native.ZkSpellInstance_getIsMultiEffect(Handle);
		public int TargetCollectAlgo => Native.ZkSpellInstance_getTargetCollectAlgo(Handle);
		public int TargetCollectType => Native.ZkSpellInstance_getTargetCollectType(Handle);
		public int TargetCollectRange => Native.ZkSpellInstance_getTargetCollectRange(Handle);
		public int TargetCollectAzi => Native.ZkSpellInstance_getTargetCollectAzi(Handle);
		public int TargetCollectElevation => Native.ZkSpellInstance_getTargetCollectElevation(Handle);
	}
}