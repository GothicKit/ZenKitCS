using System;

namespace ZenKit.Daedalus
{
	public enum NpcType
	{
		G1Ambient = 0,
		G1Main = 1,
		G1Guard = 2,
		G1Friend = 3,
		G1MineAmbient = 4,
		G1MineGuard = 5,
		G1OwAmbient = 6,
		G1OwGuard = 7,
		G1Rogue = 8,
		G2Ambient = 0,
		G2Main = 1,
		G2Friend = 2,
		G2OcAmbient = 3,
		G2OcMain = 4,
		G2BlAmbient = 5,
		G2TalAmbient = 6,
		G2BlMain = 7
	}

	public enum NpcNameSlot
	{
		Slot0 = 0,
		Slot1 = 1,
		Slot2 = 2,
		Slot3 = 3,
		Slot4 = 4
	}

	public enum NpcMissionSlot
	{
		Slot0 = 0,
		Slot1 = 1,
		Slot2 = 2,
		Slot3 = 3,
		Slot4 = 4
	}

	public enum NpcAttribute
	{
		HitPoints = 0,
		HitPointsMax = 1,
		Mana = 2,
		ManaMax = 3,
		Strength = 4,
		Dexterity = 5,
		RegenerateHp = 6,
		RegenerateMana = 7
	}

	public enum DamageType
	{
		Barrier = 0,
		Blunt = 1,
		Edge = 2,
		Fire = 3,
		Fly = 4,
		Magic = 5,
		Point = 6,
		Fall = 7
	}

	public enum NpcTalent
	{
		Unknown = 0,
		OneHanded = 1,
		TwoHanded = 2,
		Bow = 3,
		Crossbow = 4
	}

	public class NpcInstance : DaedalusInstance
	{
		public NpcInstance(UIntPtr handle) : base(handle)
		{
		}

		public int Id
		{
			get => Native.ZkNpcInstance_getId(Handle);
			set => Native.ZkNpcInstance_setId(Handle, value);
		}

		public string Slot
		{
			get => Native.ZkNpcInstance_getSlot(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkNpcInstance_setSlot(Handle, value);
		}

		public string Effect
		{
			get => Native.ZkNpcInstance_getEffect(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkNpcInstance_setEffect(Handle, value);
		}

		public NpcType NpcType
		{
			get => Native.ZkNpcInstance_getType(Handle);
			set => Native.ZkNpcInstance_setType(Handle, value);
		}

		public uint Flags
		{
			get => Native.ZkNpcInstance_getFlags(Handle);
			set => Native.ZkNpcInstance_setFlags(Handle, value);
		}

		public int DamageType
		{
			get => Native.ZkNpcInstance_getDamageType(Handle);
			set => Native.ZkNpcInstance_setDamageType(Handle, value);
		}

		public int Guild
		{
			get => Native.ZkNpcInstance_getGuild(Handle);
			set => Native.ZkNpcInstance_setGuild(Handle, value);
		}

		public int Level
		{
			get => Native.ZkNpcInstance_getLevel(Handle);
			set => Native.ZkNpcInstance_setLevel(Handle, value);
		}

		public int FightTactic
		{
			get => Native.ZkNpcInstance_getFightTactic(Handle);
			set => Native.ZkNpcInstance_setFightTactic(Handle, value);
		}

		public int Weapon
		{
			get => Native.ZkNpcInstance_getWeapon(Handle);
			set => Native.ZkNpcInstance_setWeapon(Handle, value);
		}

		public int Voice
		{
			get => Native.ZkNpcInstance_getVoice(Handle);
			set => Native.ZkNpcInstance_setVoice(Handle, value);
		}

		public int VoicePitch
		{
			get => Native.ZkNpcInstance_getVoicePitch(Handle);
			set => Native.ZkNpcInstance_setVoicePitch(Handle, value);
		}

		public int BodyMass
		{
			get => Native.ZkNpcInstance_getBodyMass(Handle);
			set => Native.ZkNpcInstance_setBodyMass(Handle, value);
		}

		public int DailyRoutine
		{
			get => Native.ZkNpcInstance_getDailyRoutine(Handle);
			set => Native.ZkNpcInstance_setDailyRoutine(Handle, value);
		}

		public int StartAiState
		{
			get => Native.ZkNpcInstance_getStartAiState(Handle);
			set => Native.ZkNpcInstance_setStartAiState(Handle, value);
		}

		public string SpawnPoint
		{
			get => Native.ZkNpcInstance_getSpawnPoint(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkNpcInstance_setSpawnPoint(Handle, value);
		}

		public int SpawnDelay
		{
			get => Native.ZkNpcInstance_getSpawnDelay(Handle);
			set => Native.ZkNpcInstance_setSpawnDelay(Handle, value);
		}

		public int Senses
		{
			get => Native.ZkNpcInstance_getSenses(Handle);
			set => Native.ZkNpcInstance_setSenses(Handle, value);
		}

		public int SensesRange
		{
			get => Native.ZkNpcInstance_getSensesRange(Handle);
			set => Native.ZkNpcInstance_setSensesRange(Handle, value);
		}

		public string Wp
		{
			get => Native.ZkNpcInstance_getWp(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkNpcInstance_setWp(Handle, value);
		}

		public int Exp
		{
			get => Native.ZkNpcInstance_getExp(Handle);
			set => Native.ZkNpcInstance_setExp(Handle, value);
		}

		public int ExpNext
		{
			get => Native.ZkNpcInstance_getExpNext(Handle);
			set => Native.ZkNpcInstance_setExpNext(Handle, value);
		}

		public int Lp
		{
			get => Native.ZkNpcInstance_getLp(Handle);
			set => Native.ZkNpcInstance_setLp(Handle, value);
		}

		public int BodyStateInterruptableOverride
		{
			get => Native.ZkNpcInstance_getBodyStateInterruptableOverride(Handle);
			set => Native.ZkNpcInstance_setBodyStateInterruptableOverride(Handle, value);
		}

		public int NoFocus
		{
			get => Native.ZkNpcInstance_getNoFocus(Handle);
			set => Native.ZkNpcInstance_setNoFocus(Handle, value);
		}

		public string GetName(NpcNameSlot slot)
		{
			return Native.ZkNpcInstance_getName(Handle, slot).MarshalAsString() ?? string.Empty;
		}

		public int GetMission(NpcMissionSlot slot)
		{
			return Native.ZkNpcInstance_getMission(Handle, slot);
		}

		public int GetAttribute(NpcAttribute attribute)
		{
			return Native.ZkNpcInstance_getAttribute(Handle, attribute);
		}

		public int GetHitChance(NpcTalent talent)
		{
			return Native.ZkNpcInstance_getHitChance(Handle, talent);
		}

		public int GetProtection(DamageType type)
		{
			return Native.ZkNpcInstance_getProtection(Handle, type);
		}

		public int GetDamage(DamageType type)
		{
			return Native.ZkNpcInstance_getDamage(Handle, type);
		}

		public int GetAiVar(ulong i)
		{
			return Native.ZkNpcInstance_getAiVar(Handle, i);
		}

		public void SetName(NpcNameSlot slot, string v)
		{
			Native.ZkNpcInstance_setName(Handle, slot, v);
		}

		public void SetMission(NpcMissionSlot slot, int v)
		{
			Native.ZkNpcInstance_setMission(Handle, slot, v);
		}

		public void SetAttribute(NpcAttribute attribute, int v)
		{
			Native.ZkNpcInstance_setAttribute(Handle, attribute, v);
		}

		public void SetHitChance(NpcTalent talent, int v)
		{
			Native.ZkNpcInstance_setHitChance(Handle, talent, v);
		}

		public void SetProtection(DamageType type, int v)
		{
			Native.ZkNpcInstance_setProtection(Handle, type, v);
		}

		public void setDamage(DamageType type, int v)
		{
			Native.ZkNpcInstance_setDamage(Handle, type, v);
		}

		public void SetAiVar(ulong i, int v)
		{
			Native.ZkNpcInstance_setAiVar(Handle, i, v);
		}
	}
}