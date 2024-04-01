using System;
using System.Collections.Generic;
using System.Numerics;
using ZenKit.Util;
using ZenKit.Vobs;

namespace ZenKit
{
	public struct SpawnLocation
	{
		public Npc Npc;
		public Vector3 Position;
		public float Timer;
	}

	public class CutscenePlayer
	{
		internal readonly UIntPtr Handle;

		public CutscenePlayer()
		{
			Handle = Native.ZkCutscenePlayer_new();
		}

		internal CutscenePlayer(UIntPtr handle)
		{
			Handle = handle;
		}

		public int LastProcessDay
		{
			get => Native.ZkCutscenePlayer_getLastProcessDay(Handle);
			set => Native.ZkCutscenePlayer_setLastProcessDay(Handle, value);
		}

		public int LastProcessHour
		{
			get => Native.ZkCutscenePlayer_getLastProcessHour(Handle);
			set => Native.ZkCutscenePlayer_setLastProcessHour(Handle, value);
		}

		public int PlayListCount
		{
			get => Native.ZkCutscenePlayer_getPlayListCount(Handle);
			set => Native.ZkCutscenePlayer_setPlayListCount(Handle, value);
		}

		~CutscenePlayer()
		{
			Native.ZkCutscenePlayer_del(Handle);
		}
	}

	public class SkyController
	{
		internal readonly UIntPtr Handle;

		public SkyController()
		{
			Handle = Native.ZkSkyController_new();
		}

		internal SkyController(UIntPtr handle)
		{
			Handle = handle;
		}

		public float MasterTime
		{
			get => Native.ZkSkyController_getMasterTime(Handle);
			set => Native.ZkSkyController_setMasterTime(Handle, value);
		}

		public float RainWeight
		{
			get => Native.ZkSkyController_getRainWeight(Handle);
			set => Native.ZkSkyController_setRainWeight(Handle, value);
		}

		public float RainStart
		{
			get => Native.ZkSkyController_getRainStart(Handle);
			set => Native.ZkSkyController_setRainStart(Handle, value);
		}

		public float RainStop
		{
			get => Native.ZkSkyController_getRainStop(Handle);
			set => Native.ZkSkyController_setRainStop(Handle, value);
		}

		public float RainSctTimer
		{
			get => Native.ZkSkyController_getRainSctTimer(Handle);
			set => Native.ZkSkyController_setRainSctTimer(Handle, value);
		}

		public float RainSndVol
		{
			get => Native.ZkSkyController_getRainSndVol(Handle);
			set => Native.ZkSkyController_setRainSndVol(Handle, value);
		}

		public float DayCtr
		{
			get => Native.ZkSkyController_getDayCtr(Handle);
			set => Native.ZkSkyController_setDayCtr(Handle, value);
		}

		public float FadeScale
		{
			get => Native.ZkSkyController_getFadeScale(Handle);
			set => Native.ZkSkyController_setFadeScale(Handle, value);
		}

		public bool enderLightning
		{
			get => Native.ZkSkyController_getRenderLightning(Handle);
			set => Native.ZkSkyController_setRenderLightning(Handle, value);
		}

		public bool sRaining
		{
			get => Native.ZkSkyController_getIsRaining(Handle);
			set => Native.ZkSkyController_setIsRaining(Handle, value);
		}

		public int inCtr
		{
			get => Native.ZkSkyController_getRainCtr(Handle);
			set => Native.ZkSkyController_setRainCtr(Handle, value);
		}

		~SkyController()
		{
			Native.ZkSkyController_del(Handle);
		}
	}

	public interface IWorld : ICacheable<IWorld>
	{
		public IMesh Mesh { get; }
		public IBspTree BspTree { get; }
		public IWayNet WayNet { get; }
		public List<IVirtualObject> RootObjects { get; }
		public int RootObjectCount { get; }

		public IVirtualObject GetRootObject(int i);
	}

	[Serializable]
	public class CachedWorld : IWorld
	{
		public IMesh Mesh { get; set; }
		public IBspTree BspTree { get; set; }
		public IWayNet WayNet { get; set; }
		public List<IVirtualObject> RootObjects { get; set; }
		public int RootObjectCount => RootObjects.Count;

		public IVirtualObject GetRootObject(int i)
		{
			return RootObjects[i];
		}

		public IWorld Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class World : IWorld
	{
		internal readonly UIntPtr Handle;

		public World(string path, GameVersion? version = null)
		{
			Handle = version == null ? Native.ZkWorld_loadPath(path) : Native.ZkWorld_loadPathVersioned(path, version.Value);
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load world");
		}

		public World(Read buf, GameVersion? version = null)
		{
			Handle = version == null ? Native.ZkWorld_load(buf.Handle) : Native.ZkWorld_loadVersioned(buf.Handle, version.Value);
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load world");
		}

		public World(Vfs vfs, string name, GameVersion? version = null)
		{
			Handle = version == null ? Native.ZkWorld_loadVfs(vfs.Handle, name) : Native.ZkWorld_loadVfsVersioned(vfs.Handle, name, version.Value);
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load world");
		}

		internal World(UIntPtr handle)
		{
			Handle = handle;
		}

		public IMesh Mesh => new Mesh(Native.ZkWorld_getMesh(Handle));
		public IBspTree BspTree => new BspTree(Native.ZkWorld_getBspTree(Handle));
		public IWayNet WayNet => new WayNet(Native.ZkWorld_getWayNet(Handle));
		public int RootObjectCount => (int)Native.ZkWorld_getRootObjectCount(Handle);

		public bool NpcSpawnEnabled
		{
			get => Native.ZkWorld_getNpcSpawnEnabled(Handle);
			set => Native.ZkWorld_setNpcSpawnEnabled(Handle, value);
		}

		public int NpcSpawnFlags
		{
			get => Native.ZkWorld_getNpcSpawnFlags(Handle);
			set => Native.ZkWorld_setNpcSpawnFlags(Handle, value);
		}

		public CutscenePlayer? Player
		{
			get
			{
				var handle = Native.ZkObject_takeRef(Native.ZkWorld_getCutscenePlayer(Handle));
				return handle == UIntPtr.Zero ? null : new CutscenePlayer(handle);
			}
			set => Native.ZkWorld_setCutscenePlayer(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public SkyController? SkyController
		{
			get
			{
				var handle = Native.ZkObject_takeRef(Native.ZkWorld_getSkyController(Handle));
				return handle == UIntPtr.Zero ? null : new SkyController(handle);
			}
			set => Native.ZkWorld_setSkyController(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public List<IVirtualObject> RootObjects
		{
			get
			{
				var objects = new List<IVirtualObject>();
				var count = RootObjectCount;
				for (var i = 0; i < count; ++i) objects.Add(GetRootObject(i));
				return objects;
			}
			set
			{
				Native.ZkWorld_clearRootObjects(Handle);
				value.ConvertAll(v => (VirtualObject)v).ForEach(v => Native.ZkWorld_addRootObject(Handle, v.Handle));
			}
		}

		public List<Npc> Npcs
		{
			get
			{
				var objects = new List<Npc>();
				var count = Native.ZkWorld_getNpcCount(Handle);

				for (var i = 0u; i < count; ++i)
				{
					var handle = Native.ZkObject_takeRef(Native.ZkWorld_getNpc(Handle, i));
					objects.Add(new Npc(handle));
				}

				return objects;
			}
			set
			{
				Native.ZkWorld_clearNpcs(Handle);
				value.ForEach(v => Native.ZkWorld_addNpc(Handle, v.Handle));
			}
		}

		public List<SpawnLocation> SpawnLocations
		{
			get
			{
				var objects = new List<SpawnLocation>();
				var count = Native.ZkWorld_getSpawnLocationCount(Handle);

				for (var i = 0u; i < count; ++i)
				{
					var handle = Native.ZkWorld_getSpawnLocation(Handle, i);

					var loc = new SpawnLocation
					{
						Npc = new Npc(Native.ZkObject_takeRef(handle.Npc)),
						Position = handle.Position,
						Timer = handle.Timer
					};

					objects.Add(loc);
				}

				return objects;
			}
			set
			{
				Native.ZkWorld_clearSpawnLocations(Handle);
				value.ForEach(v => Native.ZkWorld_addSpawnLocation(Handle, new Native.Structs.ZkSpawnLocation
				{
					Npc = v.Npc.Handle,
					Position = v.Position,
					Timer = v.Timer
				}));
			}
		}

		public IWorld Cache()
		{
			return new CachedWorld
			{
				Mesh = Mesh.Cache(),
				BspTree = BspTree.Cache(),
				WayNet = WayNet.Cache(),
				RootObjects = RootObjects.ConvertAll(obj => obj.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IVirtualObject GetRootObject(int i)
		{
			var handle = Native.ZkWorld_getRootObject(Handle, (ulong)i);
			return VirtualObject.FromNative(Native.ZkObject_takeRef(handle));
		}

		~World()
		{
			Native.ZkWorld_del(Handle);
		}
	}
}