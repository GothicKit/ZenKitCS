using System;
using System.Collections.Generic;
using ZenKit.Util;
using ZenKit.Vobs;

namespace ZenKit
{
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
		private readonly UIntPtr _handle;

		public World(string path)
		{
			_handle = Native.ZkWorld_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load world");
		}

		public World(Read buf)
		{
			_handle = Native.ZkWorld_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load world");
		}

		public World(Vfs vfs, string name)
		{
			_handle = Native.ZkWorld_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load world");
		}

		public IMesh Mesh => new Mesh(Native.ZkWorld_getMesh(_handle));
		public IBspTree BspTree => new BspTree(Native.ZkWorld_getBspTree(_handle));
		public IWayNet WayNet => new WayNet(Native.ZkWorld_getWayNet(_handle));
		public int RootObjectCount => (int)Native.ZkWorld_getRootObjectCount(_handle);

		public List<IVirtualObject> RootObjects
		{
			get
			{
				var objects = new List<IVirtualObject>();
				var count = RootObjectCount;
				for (var i = 0; i < count; ++i) objects.Add(GetRootObject(i));
				return objects;
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
			var handle = Native.ZkWorld_getRootObject(_handle, (ulong)i);
			return VirtualObject.FromNative(Native.ZkObject_takeRef(handle));
		}

		~World()
		{
			Native.ZkWorld_del(_handle);
		}
	}
}