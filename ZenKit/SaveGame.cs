using System;
using System.Collections.Generic;

namespace ZenKit
{
	public enum SaveTopicSection
	{
		Missions = 0,
		Notes = 1
	}

	public enum SaveTopicStatus
	{
		Free = 0,
		Running = 1,
		Success = 2,
		Failure = 3,
		Obsolete = 4
	}

	public struct SaveMission
	{
		public string Name;
		public int Id;
		public bool Available;
		public int StatusIndex;
	}

	public struct SaveInfoState
	{
		public string Name;
		public bool Told;
	}

	public struct SaveSymbolState
	{
		public string Name;
		public List<int> Values;
	}

	public struct SaveLogTopic
	{
		public string Description;
		public SaveTopicSection Section;
		public SaveTopicStatus Status;
		public List<string> Entries;
	}


	public class SaveMetadata
	{
		private readonly UIntPtr _handle;

		internal SaveMetadata(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Title
		{
			get => Native.ZkSaveMetadata_getTitle(_handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkSaveMetadata_setTitle(_handle, value);
		}

		public string World
		{
			get => Native.ZkSaveMetadata_getWorld(_handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkSaveMetadata_setWorld(_handle, value);
		}

		public int TimeDay
		{
			get => Native.ZkSaveMetadata_getTimeDay(_handle);
			set => Native.ZkSaveMetadata_setTimeDay(_handle, value);
		}

		public int TimeHour
		{
			get => Native.ZkSaveMetadata_getTimeHour(_handle);
			set => Native.ZkSaveMetadata_setTimeHour(_handle, value);
		}

		public int TimeMinute
		{
			get => Native.ZkSaveMetadata_getTimeMinute(_handle);
			set => Native.ZkSaveMetadata_setTimeMinute(_handle, value);
		}

		public string SaveDate
		{
			get => Native.ZkSaveMetadata_getSaveDate(_handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkSaveMetadata_setSaveDate(_handle, value);
		}

		public int VersionMajor
		{
			get => Native.ZkSaveMetadata_getVersionMajor(_handle);
			set => Native.ZkSaveMetadata_setVersionMajor(_handle, value);
		}

		public int VersionMinor
		{
			get => Native.ZkSaveMetadata_getVersionMinor(_handle);
			set => Native.ZkSaveMetadata_setVersionMinor(_handle, value);
		}

		public TimeSpan PlayTime
		{
			get => TimeSpan.FromSeconds(Native.ZkSaveMetadata_getPlayTimeSeconds(_handle));
			set => Native.ZkSaveMetadata_setPlayTimeSeconds(_handle, (int)value.TotalSeconds);
		}

		public int VersionPoint
		{
			get => Native.ZkSaveMetadata_getVersionPoint(_handle);
			set => Native.ZkSaveMetadata_setVersionPoint(_handle, value);
		}

		public int VersionInt
		{
			get => Native.ZkSaveMetadata_getVersionInt(_handle);
			set => Native.ZkSaveMetadata_setVersionInt(_handle, value);
		}

		public string VersionAppName
		{
			get => Native.ZkSaveMetadata_getVersionAppName(_handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkSaveMetadata_setVersionAppName(_handle, value);
		}
	}

	public class SaveState
	{
		private readonly UIntPtr _handle;

		internal SaveState(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Day
		{
			get => Native.ZkSaveState_getDay(_handle);
			set => Native.ZkSaveState_setDay(_handle, value);
		}

		public int Hour
		{
			get => Native.ZkSaveState_getHour(_handle);
			set => Native.ZkSaveState_setHour(_handle, value);
		}

		public int Minute
		{
			get => Native.ZkSaveState_getMinute(_handle);
			set => Native.ZkSaveState_setMinute(_handle, value);
		}


		public int MissionCount => (int)Native.ZkSaveState_getMissionCount(_handle);

		public List<SaveMission> Mission
		{
			get
			{
				var missions = new List<SaveMission>();

				var count = MissionCount;
				for (var i = 0; i < count; ++i) missions.Add(GetMission(i));

				return missions;
			}

			set
			{
				ClearMissions();
				value.ForEach(AddMission);
			}
		}


		public int InfoStateCount => (int)Native.ZkSaveState_getInfoStateCount(_handle);

		public List<SaveInfoState> InfoStates
		{
			get
			{
				var states = new List<SaveInfoState>();

				var count = InfoStateCount;
				for (var i = 0; i < count; ++i) states.Add(GetInfoState(i));

				return states;
			}

			set
			{
				ClearInfoStates();
				value.ForEach(AddInfoState);
			}
		}


		public int SymbolStateCount => (int)Native.ZkSaveState_getSymbolStateCount(_handle);

		public List<SaveSymbolState> SymbolStates
		{
			get
			{
				var states = new List<SaveSymbolState>();

				var count = SymbolStateCount;
				for (var i = 0; i < count; ++i) states.Add(GetSymbolState(i));

				return states;
			}

			set
			{
				ClearSymbolStates();
				value.ForEach(AddSymbolState);
			}
		}


		public int LogTopicCount => (int)Native.ZkSaveState_getLogTopicCount(_handle);

		public List<SaveLogTopic> LogTopics
		{
			get
			{
				var states = new List<SaveLogTopic>();

				var count = LogTopicCount;
				for (var i = 0; i < count; ++i) states.Add(GetLogTopic(i));

				return states;
			}

			set
			{
				ClearLogTopics();
				value.ForEach(AddLogTopic);
			}
		}

		public byte[,] GuildAttitudes
		{
			get
			{
				var val = Native.ZkSaveState_getGuildAttitudes(_handle, out var dimension);
				var flat = val.MarshalAsArray<byte>(dimension * dimension);

				var matrix = new byte[dimension, dimension];
				for (var i = 0u; i < dimension; ++i)
				for (var j = 0u; j < dimension; ++j)
					matrix[i, j] = flat[i * dimension + j];

				return matrix;
			}

			set
			{
				var dim = (int)Math.Sqrt(value.Length);
				var val = new byte[dim * dim];

				for (var i = 0; i < dim; ++i)
				for (var j = 0u; j < dim; ++j)
					val[i * dim + j] = value[i, j];

				Native.ZkSaveState_setGuildAttitudes(_handle, val, (ulong)dim);
			}
		}

		public SaveMission GetMission(int i)
		{
			var mission = new SaveMission();

			Native.ZkSaveState_getMission(_handle, (ulong)i, out var name, out mission.Id, out mission.Available,
				out mission.StatusIndex);
			mission.Name = name.MarshalAsString() ?? string.Empty;

			return mission;
		}

		public void SetMission(int i, SaveMission mission)
		{
			Native.ZkSaveState_setMission(_handle, (ulong)i, mission.Name, mission.Id, mission.Available, mission.StatusIndex);
		}

		public void AddMission(SaveMission mission)
		{
			Native.ZkSaveState_addMission(_handle, mission.Name, mission.Id, mission.Available, mission.StatusIndex);
		}

		public void RemoveMission(int i)
		{
			Native.ZkSaveState_removeMission(_handle, (ulong)i);
		}

		public void ClearMissions()
		{
			Native.ZkSaveState_clearMissions(_handle);
		}

		public SaveInfoState GetInfoState(int i)
		{
			var state = new SaveInfoState();

			Native.ZkSaveState_getInfoState(_handle, (ulong)i, out var name, out state.Told);
			state.Name = name.MarshalAsString() ?? string.Empty;

			return state;
		}

		public void SetInfoState(int i, SaveInfoState state)
		{
			Native.ZkSaveState_setInfoState(_handle, (ulong)i, state.Name, state.Told);
		}

		public void AddInfoState(SaveInfoState state)
		{
			Native.ZkSaveState_addInfoState(_handle, state.Name, state.Told);
		}

		public void RemoveInfoState(int i)
		{
			Native.ZkSaveState_removeInfoState(_handle, (ulong)i);
		}

		public void ClearInfoStates()
		{
			Native.ZkSaveState_clearInfoStates(_handle);
		}

		public SaveSymbolState GetSymbolState(int i)
		{
			Native.ZkSaveState_getSymbolState(_handle, (ulong)i, out var name, out var values, out var valueCount);
			return new SaveSymbolState
			{
				Name = name.MarshalAsString() ?? string.Empty,
				Values = values.MarshalAsList<int>(valueCount)
			};
		}

		public void SetSymbolState(int i, SaveSymbolState state)
		{
			var values = state.Values.ToArray();
			Native.ZkSaveState_setSymbolState(_handle, (ulong)i, state.Name, values, (ulong)values.Length);
		}

		public void AddSymbolState(SaveSymbolState state)
		{
			var values = state.Values.ToArray();
			Native.ZkSaveState_addSymbolState(_handle, state.Name, values, (ulong)values.Length);
		}

		public void RemoveSymbolState(int i)
		{
			Native.ZkSaveState_removeSymbolState(_handle, (ulong)i);
		}

		public void ClearSymbolStates()
		{
			Native.ZkSaveState_clearSymbolStates(_handle);
		}

		public SaveLogTopic GetLogTopic(int i)
		{
			var topic = new SaveLogTopic();

			Native.ZkSaveState_getLogTopic(_handle, (ulong)i, out var name, out topic.Section, out topic.Status);
			topic.Description = name.MarshalAsString() ?? string.Empty;
			topic.Entries = new List<string>();

			var count = Native.ZkSaveState_getLogTopicEntryCount(_handle, (ulong)i);
			for (var j = 0u; j < count; ++j) {
				var s = Native.ZkSaveState_getLogTopicEntry(_handle, (ulong)i, j).MarshalAsString();
				if (s == null) continue;
				topic.Entries.Add(s);
			}

			return topic;
		}

		public void SetLogTopic(int i, SaveLogTopic state)
		{
			Native.ZkSaveState_setLogTopic(_handle, (ulong)i, state.Description, state.Section, state.Status);
			Native.ZkSaveState_clearLogTopicEntries(_handle, (ulong)i);
			state.Entries.ForEach(v => Native.ZkSaveState_addLogTopicEntry(_handle, (ulong)i, v));
		}

		public void AddLogTopic(SaveLogTopic state)
		{
			Native.ZkSaveState_addLogTopic(_handle, state.Description, state.Section, state.Status);
			Native.ZkSaveState_clearLogTopicEntries(_handle, (ulong)(LogTopicCount - 1));
			state.Entries.ForEach(v => Native.ZkSaveState_addLogTopicEntry(_handle, (ulong)(LogTopicCount - 1), v));
		}

		public void RemoveLogTopic(int i)
		{
			Native.ZkSaveState_removeLogTopic(_handle, (ulong)i);
		}

		public void ClearLogTopics()
		{
			Native.ZkSaveState_clearLogTopics(_handle);
		}
	}


	public class SaveGame
	{
		private readonly UIntPtr _handle;

		public SaveGame(GameVersion version)
		{
			_handle = Native.ZkSaveGame_new(version);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to create savegame");
		}

		public SaveMetadata Metadata => new SaveMetadata(Native.ZkSaveGame_getMetadata(_handle));

		public SaveState State => new SaveState(Native.ZkSaveGame_getState(_handle));

		public Texture? Thumbnail
		{
			get
			{
				var val = Native.ZkSaveGame_getThumbnail(_handle);
				return val == UIntPtr.Zero ? null : new Texture(val);
			}
			set => Native.ZkSaveGame_setThumbnail(_handle, value?.Handle ?? UIntPtr.Zero);
		}

		~SaveGame()
		{
			Native.ZkSaveGame_del(_handle);
		}


		public bool Load(string path)
		{
			return Native.ZkSaveGame_load(_handle, path);
		}

		public bool Save(string path, World world, string worldName)
		{
			return Native.ZkSaveGame_save(_handle, path, world.Handle, worldName);
		}

		public World LoadWorld()
		{
			var handle = Native.ZkSaveGame_loadCurrentWorld(_handle);
			return new World(handle);
		}

		public World LoadWorld(string name)
		{
			var handle = Native.ZkSaveGame_loadWorld(_handle, name);
			return new World(handle);
		}
	}
}