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

		public int Id
		{
			get => Native.ZkItemInstance_getId(Handle);
			set => Native.ZkItemInstance_setId(Handle, value);
		}

		public string Name
		{
			get => Native.ZkItemInstance_getName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkItemInstance_setName(Handle, value);
		}

		public string NameId
		{
			get => Native.ZkItemInstance_getNameId(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkItemInstance_setNameId(Handle, value);
		}

		public int Hp
		{
			get => Native.ZkItemInstance_getHp(Handle);
			set => Native.ZkItemInstance_setHp(Handle, value);
		}

		public int HpMax
		{
			get => Native.ZkItemInstance_getHpMax(Handle);
			set => Native.ZkItemInstance_setHpMax(Handle, value);
		}

		public int MainFlag
		{
			get => Native.ZkItemInstance_getMainFlag(Handle);
			set => Native.ZkItemInstance_setMainFlag(Handle, value);
		}

		public int Flags
		{
			get => Native.ZkItemInstance_getFlags(Handle);
			set => Native.ZkItemInstance_setFlags(Handle, value);
		}

		public int Weight
		{
			get => Native.ZkItemInstance_getWeight(Handle);
			set => Native.ZkItemInstance_setWeight(Handle, value);
		}

		public int Value
		{
			get => Native.ZkItemInstance_getValue(Handle);
			set => Native.ZkItemInstance_setValue(Handle, value);
		}

		public int DamageType
		{
			get => Native.ZkItemInstance_getDamageType(Handle);
			set => Native.ZkItemInstance_setDamageType(Handle, value);
		}

		public int DamageTotal
		{
			get => Native.ZkItemInstance_getDamageTotal(Handle);
			set => Native.ZkItemInstance_setDamageTotal(Handle, value);
		}

		public int Wear
		{
			get => Native.ZkItemInstance_getWear(Handle);
			set => Native.ZkItemInstance_setWear(Handle, value);
		}

		public int Nutrition
		{
			get => Native.ZkItemInstance_getNutrition(Handle);
			set => Native.ZkItemInstance_setNutrition(Handle, value);
		}

		public int Magic
		{
			get => Native.ZkItemInstance_getMagic(Handle);
			set => Native.ZkItemInstance_setMagic(Handle, value);
		}

		public int OnEquip
		{
			get => Native.ZkItemInstance_getOnEquip(Handle);
			set => Native.ZkItemInstance_setOnEquip(Handle, value);
		}

		public int OnUnEquip
		{
			get => Native.ZkItemInstance_getOnUnequip(Handle);
			set => Native.ZkItemInstance_setOnUnequip(Handle, value);
		}

		public int Owner
		{
			get => Native.ZkItemInstance_getOwner(Handle);
			set => Native.ZkItemInstance_setOwner(Handle, value);
		}

		public int OwnerGuild
		{
			get => Native.ZkItemInstance_getOwnerGuild(Handle);
			set => Native.ZkItemInstance_setOwnerGuild(Handle, value);
		}

		public int DisguiseGuild
		{
			get => Native.ZkItemInstance_getDisguiseGuild(Handle);
			set => Native.ZkItemInstance_setDisguiseGuild(Handle, value);
		}

		public string Visual
		{
			get => Native.ZkItemInstance_getVisual(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkItemInstance_setVisual(Handle, value);
		}

		public string VisualChange
		{
			get => Native.ZkItemInstance_getVisualChange(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkItemInstance_setVisualChange(Handle, value);
		}

		public string Effect
		{
			get => Native.ZkItemInstance_getEffect(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkItemInstance_setEffect(Handle, value);
		}

		public int VisualSkin
		{
			get => Native.ZkItemInstance_getVisualSkin(Handle);
			set => Native.ZkItemInstance_setVisualSkin(Handle, value);
		}

		public string SchemeName
		{
			get => Native.ZkItemInstance_getSchemeName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkItemInstance_setSchemeName(Handle, value);
		}

		public int Material
		{
			get => Native.ZkItemInstance_getMaterial(Handle);
			set => Native.ZkItemInstance_setMaterial(Handle, value);
		}

		public int Munition
		{
			get => Native.ZkItemInstance_getMunition(Handle);
			set => Native.ZkItemInstance_setMunition(Handle, value);
		}

		public int Spell
		{
			get => Native.ZkItemInstance_getSpell(Handle);
			set => Native.ZkItemInstance_setSpell(Handle, value);
		}

		public int Range
		{
			get => Native.ZkItemInstance_getRange(Handle);
			set => Native.ZkItemInstance_setRange(Handle, value);
		}

		public int MagCircle
		{
			get => Native.ZkItemInstance_getMagCircle(Handle);
			set => Native.ZkItemInstance_setMagCircle(Handle, value);
		}

		public string Description
		{
			get => Native.ZkItemInstance_getDescription(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkItemInstance_setDescription(Handle, value);
		}

		public int InvZBias
		{
			get => Native.ZkItemInstance_getInvZBias(Handle);
			set => Native.ZkItemInstance_setInvZBias(Handle, value);
		}

		public int InvRotX
		{
			get => Native.ZkItemInstance_getInvRotX(Handle);
			set => Native.ZkItemInstance_setInvRotX(Handle, value);
		}

		public int InvRotY
		{
			get => Native.ZkItemInstance_getInvRotY(Handle);
			set => Native.ZkItemInstance_setInvRotY(Handle, value);
		}

		public int InvRotZ
		{
			get => Native.ZkItemInstance_getInvRotZ(Handle);
			set => Native.ZkItemInstance_setInvRotZ(Handle, value);
		}

		public int InvAnimate
		{
			get => Native.ZkItemInstance_getInvAnimate(Handle);
			set => Native.ZkItemInstance_setInvAnimate(Handle, value);
		}

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

		public void SetDamage(DamageType type, int v)
		{
			Native.ZkItemInstance_setDamage(Handle, type, v);
		}

		public void SetProtection(DamageType type, int v)
		{
			Native.ZkItemInstance_setProtection(Handle, type, v);
		}

		public void SetCondAtr(ItemConditionSlot slot, int v)
		{
			Native.ZkItemInstance_setCondAtr(Handle, slot, v);
		}

		public void SetCondValue(ItemConditionSlot slot, int v)
		{
			Native.ZkItemInstance_setCondValue(Handle, slot, v);
		}

		public void SetChangeAtr(ItemConditionSlot slot, int v)
		{
			Native.ZkItemInstance_setChangeAtr(Handle, slot, v);
		}

		public void SetChangeValue(ItemConditionSlot slot, int v)
		{
			Native.ZkItemInstance_setChangeValue(Handle, slot, v);
		}

		public void SetOnState(ItemStateSlot slot, int v)
		{
			Native.ZkItemInstance_setOnState(Handle, slot, v);
		}

		public void SetText(ItemTextSlot slot, string v)
		{
			Native.ZkItemInstance_setText(Handle, slot, v);
		}

		public void SetCount(ItemTextSlot slot, int v)
		{
			Native.ZkItemInstance_setCount(Handle, slot, v);
		}
	}
}