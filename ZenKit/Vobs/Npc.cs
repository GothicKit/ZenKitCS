using System;
using System.Collections.Generic;
using System.Numerics;

namespace ZenKit.Vobs
{
	public class Talent
	{
		internal readonly UIntPtr Handle;

		public Talent()
		{
			Handle = Native.ZkNpcTalent_new();
		}

		internal Talent(UIntPtr handle)
		{
			Handle = handle;
		}

		~Talent()
		{
			Native.ZkNpcTalent_del(Handle);
		}

		public int Type
		{
			get => Native.ZkNpcTalent_getTalent(Handle);
			set => Native.ZkNpcTalent_setTalent(Handle, value);
		}

		public int Value
		{
			get => Native.ZkNpcTalent_getValue(Handle);
			set => Native.ZkNpcTalent_setValue(Handle, value);
		}

		public int Skill
		{
			get => Native.ZkNpcTalent_getSkill(Handle);
			set => Native.ZkNpcTalent_setSkill(Handle, value);
		}
	}

	public class Slot
	{
		private readonly UIntPtr handle;


		internal Slot(UIntPtr handle)
		{
			this.handle = handle;
		}

		public bool Used
		{
			get => Native.ZkNpcSlot_getUsed(handle);
			set => Native.ZkNpcSlot_setUsed(handle, value);
		}

		public string Name
		{
			get => Native.ZkNpcSlot_getName(handle).MarshalAsString() ?? throw new InvalidOperationException();
			set => Native.ZkNpcSlot_setName(handle, value);
		}

		public Item? Item
		{
			get
			{
				var val = Native.ZkNpcSlot_getItem(handle);
				return val == UIntPtr.Zero ? null : new Item(Native.ZkObject_takeRef(val));
			}
			set => Native.ZkNpcSlot_setItem(handle, value?.Handle ?? UIntPtr.Zero);
		}

		public bool InInventory
		{
			get => Native.ZkNpcSlot_getInInventory(handle);
			set => Native.ZkNpcSlot_setInInventory(handle, value);
		}
	}

	public class Npc : VirtualObject
	{
		public Npc() : base(Native.ZkVirtualObject_new(VirtualObjectType.oCNpc))
		{
		}

		internal Npc(UIntPtr handle) : base(handle)
		{
		}

		public string NpcInstance
		{
			get => Native.ZkNpc_getNpcInstance(Handle).MarshalAsString() ?? throw new InvalidOperationException();
			set => Native.ZkNpc_setNpcInstance(Handle, value);
		}

		public Vector3 ModelScale
		{
			get => Native.ZkNpc_getModelScale(Handle);
			set => Native.ZkNpc_setModelScale(Handle, value);
		}

		public float ModelFatness
		{
			get => Native.ZkNpc_getModelFatness(Handle);
			set => Native.ZkNpc_setModelFatness(Handle, value);
		}

		public int Flags
		{
			get => Native.ZkNpc_getFlags(Handle);
			set => Native.ZkNpc_setFlags(Handle, value);
		}

		public int Guild
		{
			get => Native.ZkNpc_getGuild(Handle);
			set => Native.ZkNpc_setGuild(Handle, value);
		}

		public int GuildTrue
		{
			get => Native.ZkNpc_getGuildTrue(Handle);
			set => Native.ZkNpc_setGuildTrue(Handle, value);
		}

		public int Level
		{
			get => Native.ZkNpc_getLevel(Handle);
			set => Native.ZkNpc_setLevel(Handle, value);
		}

		public int Xp
		{
			get => Native.ZkNpc_getXp(Handle);
			set => Native.ZkNpc_setXp(Handle, value);
		}

		public int XpNextLevel
		{
			get => Native.ZkNpc_getXpNextLevel(Handle);
			set => Native.ZkNpc_setXpNextLevel(Handle, value);
		}

		public int Lp
		{
			get => Native.ZkNpc_getLp(Handle);
			set => Native.ZkNpc_setLp(Handle, value);
		}

		public int FightTactic
		{
			get => Native.ZkNpc_getFightTactic(Handle);
			set => Native.ZkNpc_setFightTactic(Handle, value);
		}

		public int FightMode
		{
			get => Native.ZkNpc_getFightMode(Handle);
			set => Native.ZkNpc_setFightMode(Handle, value);
		}

		public bool Wounded
		{
			get => Native.ZkNpc_getWounded(Handle);
			set => Native.ZkNpc_setWounded(Handle, value);
		}

		public bool Mad
		{
			get => Native.ZkNpc_getMad(Handle);
			set => Native.ZkNpc_setMad(Handle, value);
		}

		public int MadTime
		{
			get => Native.ZkNpc_getMadTime(Handle);
			set => Native.ZkNpc_setMadTime(Handle, value);
		}

		public bool Player
		{
			get => Native.ZkNpc_getPlayer(Handle);
			set => Native.ZkNpc_setPlayer(Handle, value);
		}

		public string StartAiState
		{
			get => Native.ZkNpc_getStartAiState(Handle).MarshalAsString() ?? throw new InvalidOperationException();
			set => Native.ZkNpc_setStartAiState(Handle, value);
		}

		public string ScriptWaypoint
		{
			get => Native.ZkNpc_getScriptWaypoint(Handle).MarshalAsString() ?? throw new InvalidOperationException();
			set => Native.ZkNpc_setScriptWaypoint(Handle, value);
		}

		public int Attitude
		{
			get => Native.ZkNpc_getAttitude(Handle);
			set => Native.ZkNpc_setAttitude(Handle, value);
		}

		public int AttitudeTemp
		{
			get => Native.ZkNpc_getAttitudeTemp(Handle);
			set => Native.ZkNpc_setAttitudeTemp(Handle, value);
		}

		public int NameNr
		{
			get => Native.ZkNpc_getNameNr(Handle);
			set => Native.ZkNpc_setNameNr(Handle, value);
		}

		public bool MoveLock
		{
			get => Native.ZkNpc_getMoveLock(Handle);
			set => Native.ZkNpc_setMoveLock(Handle, value);
		}

		public bool CurrentStateValid
		{
			get => Native.ZkNpc_getCurrentStateValid(Handle);
			set => Native.ZkNpc_setCurrentStateValid(Handle, value);
		}

		public string CurrentStateName
		{
			get => Native.ZkNpc_getCurrentStateName(Handle).MarshalAsString() ?? throw new InvalidOperationException();
			set => Native.ZkNpc_setCurrentStateName(Handle, value);
		}

		public int CurrentStateIndex
		{
			get => Native.ZkNpc_getCurrentStateIndex(Handle);
			set => Native.ZkNpc_setCurrentStateIndex(Handle, value);
		}

		public bool CurrentStateIsRoutine
		{
			get => Native.ZkNpc_getCurrentStateIsRoutine(Handle);
			set => Native.ZkNpc_setCurrentStateIsRoutine(Handle, value);
		}

		public bool NextStateValid
		{
			get => Native.ZkNpc_getNextStateValid(Handle);
			set => Native.ZkNpc_setNextStateValid(Handle, value);
		}

		public string NextStateName
		{
			get => Native.ZkNpc_getNextStateName(Handle).MarshalAsString() ?? throw new InvalidOperationException();
			set => Native.ZkNpc_setNextStateName(Handle, value);
		}

		public int NextStateIndex
		{
			get => Native.ZkNpc_getNextStateIndex(Handle);
			set => Native.ZkNpc_setNextStateIndex(Handle, value);
		}

		public bool NextStateIsRoutine
		{
			get => Native.ZkNpc_getNextStateIsRoutine(Handle);
			set => Native.ZkNpc_setNextStateIsRoutine(Handle, value);
		}

		public int LastAiState
		{
			get => Native.ZkNpc_getLastAiState(Handle);
			set => Native.ZkNpc_setLastAiState(Handle, value);
		}

		public bool HasRoutine
		{
			get => Native.ZkNpc_getHasRoutine(Handle);
			set => Native.ZkNpc_setHasRoutine(Handle, value);
		}

		public bool RoutineChanged
		{
			get => Native.ZkNpc_getRoutineChanged(Handle);
			set => Native.ZkNpc_setRoutineChanged(Handle, value);
		}

		public bool RoutineOverlay
		{
			get => Native.ZkNpc_getRoutineOverlay(Handle);
			set => Native.ZkNpc_setRoutineOverlay(Handle, value);
		}

		public int RoutineOverlayCount
		{
			get => Native.ZkNpc_getRoutineOverlayCount(Handle);
			set => Native.ZkNpc_setRoutineOverlayCount(Handle, value);
		}

		public int WalkmodeRoutine
		{
			get => Native.ZkNpc_getWalkmodeRoutine(Handle);
			set => Native.ZkNpc_setWalkmodeRoutine(Handle, value);
		}

		public bool WeaponmodeRoutine
		{
			get => Native.ZkNpc_getWeaponmodeRoutine(Handle);
			set => Native.ZkNpc_setWeaponmodeRoutine(Handle, value);
		}

		public bool StartNewRoutine
		{
			get => Native.ZkNpc_getStartNewRoutine(Handle);
			set => Native.ZkNpc_setStartNewRoutine(Handle, value);
		}

		public int AiStateDriven
		{
			get => Native.ZkNpc_getAiStateDriven(Handle);
			set => Native.ZkNpc_setAiStateDriven(Handle, value);
		}

		public Vector3 AiStatePos
		{
			get => Native.ZkNpc_getAiStatePos(Handle);
			set => Native.ZkNpc_setAiStatePos(Handle, value);
		}

		public string CurrentRoutine
		{
			get => Native.ZkNpc_getCurrentRoutine(Handle).MarshalAsString() ?? throw new InvalidOperationException();
			set => Native.ZkNpc_setCurrentRoutine(Handle, value);
		}

		public bool Respawn
		{
			get => Native.ZkNpc_getRespawn(Handle);
			set => Native.ZkNpc_setRespawn(Handle, value);
		}

		public int RespawnTime
		{
			get => Native.ZkNpc_getRespawnTime(Handle);
			set => Native.ZkNpc_setRespawnTime(Handle, value);
		}

		public int BsInterruptableOverride
		{
			get => Native.ZkNpc_getBsInterruptableOverride(Handle);
			set => Native.ZkNpc_setBsInterruptableOverride(Handle, value);
		}

		public int NpcType
		{
			get => Native.ZkNpc_getNpcType(Handle);
			set => Native.ZkNpc_setNpcType(Handle, value);
		}

		public int SpellMana
		{
			get => Native.ZkNpc_getSpellMana(Handle);
			set => Native.ZkNpc_setSpellMana(Handle, value);
		}

		public VirtualObject? CarryVob
		{
			get
			{
				var val = Native.ZkNpc_getCarryVob(Handle);
				return VirtualObject.FromNative(Native.ZkObject_takeRef(val));
			}
			set => Native.ZkNpc_setCarryVob(Handle, value?.Handle ?? System.UIntPtr.Zero);
		}

		public VirtualObject? Enemy
		{
			get
			{
				var val = Native.ZkNpc_getEnemy(Handle);
				return VirtualObject.FromNative(Native.ZkObject_takeRef(val));
			}
			set => Native.ZkNpc_setEnemy(Handle, value?.Handle ?? System.UIntPtr.Zero);
		}


		public int OverlayCount => (int)Native.ZkNpc_getOverlayCount(Handle);

		public List<string> Overlays
		{
			get
			{
				var overlays = new List<string>();

				for (var i = 0; i < OverlayCount; ++i)
				{
					overlays.Add(GetOverlay(i));
				}

				return overlays;
			}

			set
			{
				ClearOverlays();
				value.ForEach(AddOverlay);
			}
		}

		public string GetOverlay(int i)
		{
			return Native.ZkNpc_getOverlay(Handle, (ulong)i).MarshalAsString() ?? throw new InvalidOperationException();
		}

		public void ClearOverlays()
		{
			Native.ZkNpc_clearOverlays(Handle);
		}

		public void RemoveOverlay(int i)
		{
			Native.ZkNpc_removeOverlay(Handle, (ulong)i);
		}

		public void SetOverlay(int i, string overlay)
		{
			Native.ZkNpc_setOverlay(Handle, (ulong)i, overlay);
		}

		public void AddOverlay(string overlay)
		{
			Native.ZkNpc_addOverlay(Handle, overlay);
		}


		public int TalentCount => (int)Native.ZkNpc_getTalentCount(Handle);

		public List<Talent> Talents
		{
			get
			{
				var talents = new List<Talent>();

				for (var i = 0; i < TalentCount; ++i)
				{
					talents.Add(GetTalent(i));
				}

				return talents;
			}

			set
			{
				ClearTalents();
				value.ForEach(AddTalent);
			}
		}


		public Talent GetTalent(int i)
		{
			return new Talent(Native.ZkObject_takeRef(Native.ZkNpc_getTalent(Handle, (ulong)i)));
		}

		public void ClearTalents()
		{
			Native.ZkNpc_clearTalents(Handle);
		}

		public void RemoveTalent(int i)
		{
			Native.ZkNpc_removeTalent(Handle, (ulong)i);
		}

		public void SetTalent(int i, Talent talent)
		{
			Native.ZkNpc_setTalent(Handle, (ulong)i, talent.Handle);
		}

		public void AddTalent(Talent talent)
		{
			Native.ZkNpc_addTalent(Handle, talent.Handle);
		}

		public int ItemCount => (int)Native.ZkNpc_getItemCount(Handle);

		public List<Item> Items
		{
			get
			{
				var items = new List<Item>();

				for (var i = 0; i < ItemCount; ++i)
				{
					items.Add(GetItem(i));
				}

				return items;
			}
			set
			{
				ClearItems();
				value.ForEach(AddItem);
			}
		}


		public Item GetItem(int i)
		{
			return new Item(Native.ZkObject_takeRef(Native.ZkNpc_getItem(Handle, (ulong)i)));
		}

		public void ClearItems()
		{
			Native.ZkNpc_clearItems(Handle);
		}

		public void RemoveItem(int i)
		{
			Native.ZkNpc_removeItem(Handle, (ulong)i);
		}

		public void SetItem(int i, Item item)
		{
			Native.ZkNpc_setItem(Handle, (ulong)i, item.Handle);
		}

		public void AddItem(Item item)
		{
			Native.ZkNpc_addItem(Handle, item.Handle);
		}

		public int SlotCount => (int)Native.ZkNpc_getSlotCount(Handle);

		public List<Slot> Slots
		{
			get
			{
				var items = new List<Slot>();

				for (var i = 0; i < ItemCount; ++i)
				{
					items.Add(GetSlot(i));
				}

				return items;
			}
		}

		public Slot GetSlot(int i)
		{
			return new Slot(Native.ZkNpc_getSlot(Handle, (ulong)i));
		}

		public void ClearSlots()
		{
			Native.ZkNpc_clearSlots(Handle);
		}

		public void RemoveSlot(int i)
		{
			Native.ZkNpc_removeSlot(Handle, (ulong)i);
		}

		public Slot AddSlot()
		{
			return new Slot(Native.ZkNpc_addSlot(Handle));
		}

		public const int ProtectionCount = 8;

		public List<int> Protection
		{
			get
			{
				var items = new List<int>();

				for (var i = 0; i < ProtectionCount; ++i)
				{
					items.Add(GetProtection(i));
				}

				return items;
			}
			set
			{
				for (var i = 0; i < Math.Min(value.Count, ProtectionCount); ++i)
				{
					SetProtection(i, value[i]);
				}
			}
		}

		public int GetProtection(int i)
		{
			return Native.ZkNpc_getProtection(Handle, (ulong)i);
		}

		public void SetProtection(int i, int v)
		{
			Native.ZkNpc_setProtection(Handle, (ulong)i, v);
		}

		public const int AttributeCount = 8;

		public List<int> Attributes
		{
			get
			{
				var items = new List<int>();

				for (var i = 0; i < AttributeCount; ++i)
				{
					items.Add(GetAttribute(i));
				}

				return items;
			}
			set
			{
				for (var i = 0; i < Math.Min(value.Count, AttributeCount); ++i)
				{
					SetAttribute(i, value[i]);
				}
			}
		}

		public int GetAttribute(int i)
		{
			return Native.ZkNpc_getAttribute(Handle, (ulong)i);
		}

		public void SetAttribute(int i, int v)
		{
			Native.ZkNpc_setAttribute(Handle, (ulong)i, v);
		}

		public const int HitChanceCount = 8;

		public List<int> HitChance
		{
			get
			{
				var items = new List<int>();

				for (var i = 0; i < HitChanceCount; ++i)
				{
					items.Add(GetHitChance(i));
				}

				return items;
			}
			set
			{
				for (var i = 0; i < Math.Min(value.Count, HitChanceCount); ++i)
				{
					SetHitChance(i, value[i]);
				}
			}
		}

		public int GetHitChance(int i)
		{
			return Native.ZkNpc_getHitChance(Handle, (ulong)i);
		}

		public void SetHitChance(int i, int v)
		{
			Native.ZkNpc_setHitChance(Handle, (ulong)i, v);
		}

		public const int MissionCount = 8;

		public List<int> Missions
		{
			get
			{
				var items = new List<int>();

				for (var i = 0; i < MissionCount; ++i)
				{
					items.Add(GetMission(i));
				}

				return items;
			}
			set
			{
				for (var i = 0; i < Math.Min(value.Count, MissionCount); ++i)
				{
					SetMission(i, value[i]);
				}
			}
		}

		public int GetMission(int i)
		{
			return Native.ZkNpc_getMission(Handle, (ulong)i);
		}

		public void SetMission(int i, int v)
		{
			Native.ZkNpc_setMission(Handle, (ulong)i, v);
		}

		public int[] AiVars
		{
			get => Native.ZkNpc_getAiVars(Handle, out ulong len).MarshalAsArray<int>(len);
			set => Native.ZkNpc_setAiVars(Handle, value, (ulong)value.Length);
		}

		public const int PackedCount = 8;

		public List<string> Packed
		{
			get
			{
				var items = new List<string>();

				for (var i = 0; i < PackedCount; ++i)
				{
					items.Add(GetPacked(i));
				}

				return items;
			}
			set
			{
				for (var i = 0; i < Math.Min(value.Count, PackedCount); ++i)
				{
					SetPacked(i, value[i]);
				}
			}
		}

		public string GetPacked(int i)
		{
			return Native.ZkNpc_getPacked(Handle, (ulong)i).MarshalAsString() ?? throw new InvalidOperationException();
		}

		public void SetPacked(int i, string v)
		{
			Native.ZkNpc_setPacked(Handle, (ulong)i, v);
		}
	}
}