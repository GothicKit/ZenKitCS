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
		G2BlMain = 7,
	}

	public enum NpcNameSlot
	{
		Slot0 = 0,
		Slot1 = 1,
		Slot2 = 2,
		Slot3 = 3,
		Slot4 = 4,
	}

	public enum NpcMissionSlot
	{
		Slot0 = 0,
		Slot1 = 1,
		Slot2 = 2,
		Slot3 = 3,
		Slot4 = 4,
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
		RegenerateMana = 7,
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
		Fall = 7,
	}

	public enum NpcTalent
	{
		Unknown = 0,
		OneHanded = 1,
		TwoHanded = 2,
		Bow = 3,
		Crossbow = 4,
	}

	public class NpcInstance : DaedalusInstance
	{
		public NpcInstance(UIntPtr handle) : base(handle)
		{
		}

		public int Id => Native.ZkNpcInstance_getId(Handle);
		public string Slot => Native.ZkNpcInstance_getSlot(Handle).MarshalAsString() ?? string.Empty;
		public string Effect => Native.ZkNpcInstance_getEffect(Handle).MarshalAsString() ?? string.Empty;
		public NpcType NpcType => Native.ZkNpcInstance_getType(Handle);
		public uint Flags => Native.ZkNpcInstance_getFlags(Handle);
		public int DamageType => Native.ZkNpcInstance_getDamageType(Handle);
		public int Guild => Native.ZkNpcInstance_getGuild(Handle);
		public int Level => Native.ZkNpcInstance_getLevel(Handle);
		public int FightTactic => Native.ZkNpcInstance_getFightTactic(Handle);
		public int Weapon => Native.ZkNpcInstance_getWeapon(Handle);
		public int Voice => Native.ZkNpcInstance_getVoice(Handle);
		public int VoicePitch => Native.ZkNpcInstance_getVoicePitch(Handle);
		public int BodyMass => Native.ZkNpcInstance_getBodyMass(Handle);
		public int DailyRoutine => Native.ZkNpcInstance_getDailyRoutine(Handle);
		public int StartAiState => Native.ZkNpcInstance_getStartAiState(Handle);
		public string SpawnPoint => Native.ZkNpcInstance_getSpawnPoint(Handle).MarshalAsString() ?? string.Empty;
		public int SpawnDelay => Native.ZkNpcInstance_getSpawnDelay(Handle);
		public int Senses => Native.ZkNpcInstance_getSenses(Handle);
		public int SensesRange => Native.ZkNpcInstance_getSensesRange(Handle);
		public string Wp => Native.ZkNpcInstance_getWp(Handle).MarshalAsString() ?? string.Empty;
		public int Exp => Native.ZkNpcInstance_getExp(Handle);
		public int ExpNext => Native.ZkNpcInstance_getExpNext(Handle);
		public int Lp => Native.ZkNpcInstance_getLp(Handle);
		public int BodyStateInterruptableOverride => Native.ZkNpcInstance_getBodyStateInterruptableOverride(Handle);
		public int NoFocus => Native.ZkNpcInstance_getNoFocus(Handle);

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
	}
}