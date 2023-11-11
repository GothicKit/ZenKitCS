using System;

namespace ZenKit.Daedalus
{
	public enum ItemConditionSlot
	{
		Slot0 = 0,
		Slot1 = 1,
		Slot2 = 2
	}

	public enum ItemStateSlot
	{
		Slot0 = 0,
		Slot1 = 1,
		Slot2 = 2,
		Slot3 = 3
	}

	public enum ItemTextSlot
	{
		Slot0 = 0,
		Slot1 = 1,
		Slot2 = 2,
		Slot3 = 3,
		Slot4 = 4,
		Slot5 = 5
	}

	public class ItemInstance : DaedalusInstance
	{
		public ItemInstance(UIntPtr handle) : base(handle)
		{
		}

		public int Id => Native.ZkItemInstance_getId(Handle);
		public string Name => Native.ZkItemInstance_getName(Handle).MarshalAsString() ?? string.Empty;
		public string NameId => Native.ZkItemInstance_getNameId(Handle).MarshalAsString() ?? string.Empty;
		public int Hp => Native.ZkItemInstance_getHp(Handle);
		public int HpMax => Native.ZkItemInstance_getHpMax(Handle);
		public int MainFlag => Native.ZkItemInstance_getMainFlag(Handle);
		public int Flags => Native.ZkItemInstance_getFlags(Handle);
		public int Weight => Native.ZkItemInstance_getWeight(Handle);
		public int Value => Native.ZkItemInstance_getValue(Handle);
		public int DamageType => Native.ZkItemInstance_getDamageType(Handle);
		public int DamageTotal => Native.ZkItemInstance_getDamageTotal(Handle);
		public int Wear => Native.ZkItemInstance_getWear(Handle);
		public int Nutrition => Native.ZkItemInstance_getNutrition(Handle);
		public int Magic => Native.ZkItemInstance_getMagic(Handle);
		public int OnEquip => Native.ZkItemInstance_getOnEquip(Handle);
		public int OnUnEquip => Native.ZkItemInstance_getOnUnequip(Handle);
		public int Owner => Native.ZkItemInstance_getOwner(Handle);
		public int OwnerGuild => Native.ZkItemInstance_getOwnerGuild(Handle);
		public int DisguiseGuild => Native.ZkItemInstance_getDisguiseGuild(Handle);
		public string Visual => Native.ZkItemInstance_getVisual(Handle).MarshalAsString() ?? string.Empty;
		public string VisualChange => Native.ZkItemInstance_getVisualChange(Handle).MarshalAsString() ?? string.Empty;
		public string Effect => Native.ZkItemInstance_getEffect(Handle).MarshalAsString() ?? string.Empty;
		public int VisualSkin => Native.ZkItemInstance_getVisualSkin(Handle);
		public string SchemeName => Native.ZkItemInstance_getSchemeName(Handle).MarshalAsString() ?? string.Empty;
		public int Material => Native.ZkItemInstance_getMaterial(Handle);
		public int Munition => Native.ZkItemInstance_getMunition(Handle);
		public int Spell => Native.ZkItemInstance_getSpell(Handle);
		public int Range => Native.ZkItemInstance_getRange(Handle);
		public int MagCircle => Native.ZkItemInstance_getMagCircle(Handle);
		public string Description => Native.ZkItemInstance_getDescription(Handle).MarshalAsString() ?? string.Empty;
		public int InvZBias => Native.ZkItemInstance_getInvZBias(Handle);
		public int InvRotX => Native.ZkItemInstance_getInvRotX(Handle);
		public int InvRotY => Native.ZkItemInstance_getInvRotY(Handle);
		public int InvRotZ => Native.ZkItemInstance_getInvRotZ(Handle);
		public int InvAnimate => Native.ZkItemInstance_getInvAnimate(Handle);

		public int GetDamage(DamageType type)
		{
			return Native.ZkItemInstance_getDamage(Handle, type);
		}

		public int GetProtection(DamageType type)
		{
			return Native.ZkItemInstance_getProtection(Handle, type);
		}

		public int GetCondAtr(ItemConditionSlot slot)
		{
			return Native.ZkItemInstance_getCondAtr(Handle, slot);
		}

		public int GetCondValue(ItemConditionSlot slot)
		{
			return Native.ZkItemInstance_getCondValue(Handle, slot);
		}

		public int GetChangeAtr(ItemConditionSlot slot)
		{
			return Native.ZkItemInstance_getChangeAtr(Handle, slot);
		}

		public int GetChangeValue(ItemConditionSlot slot)
		{
			return Native.ZkItemInstance_getChangeValue(Handle, slot);
		}

		public int GetOnState(ItemStateSlot slot)
		{
			return Native.ZkItemInstance_getOnState(Handle, slot);
		}

		public string GetText(ItemTextSlot slot)
		{
			return Native.ZkItemInstance_getText(Handle, slot).MarshalAsString() ?? string.Empty;
		}

		public int GetCount(ItemTextSlot slot)
		{
			return Native.ZkItemInstance_getCount(Handle, slot);
		}
	}
}