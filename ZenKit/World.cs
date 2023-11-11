using System;
using System.Collections.Generic;
using ZenKit.Util;
using ZenKit.Vobs.Materialized;

namespace ZenKit
{
	namespace Materialized
	{
		[Serializable]
		public struct World
		{
			public Mesh Mesh;
			public BspTree BspTree;
			public WayNet WayNet;
			public List<VirtualObject> RootObjects;
		}
	}

	public class World : IMaterializing<Materialized.World>
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

		public Mesh Mesh => new Mesh(Native.ZkWorld_getMesh(_handle));
		public BspTree BspTree => new BspTree(Native.ZkWorld_getBspTree(_handle));
		public WayNet WayNet => new WayNet(Native.ZkWorld_getWayNet(_handle));
		public ulong RootObjectCount => Native.ZkWorld_getRootObjectCount(_handle);

		public List<Vobs.VirtualObject> RootObjects
		{
			get
			{
				var objects = new List<Vobs.VirtualObject>();

				Native.ZkWorld_enumerateRootObjects(_handle, (_, vob) =>
				{
					objects.Add(Vobs.VirtualObject.FromNative(vob));
					return false;
				}, UIntPtr.Zero);

				return objects;
			}
		}

		public Materialized.World Materialize()
		{
			return new Materialized.World
			{
				Mesh = Mesh.Materialize(),
				BspTree = BspTree.Materialize(),
				WayNet = WayNet.Materialize(),
				RootObjects = RootObjects.ConvertAll(obj => obj.Materialize())
			};
		}

		~World()
		{
			Native.ZkWorld_del(_handle);
		}

		public Vobs.VirtualObject GetRootObject(ulong i)
		{
			return Vobs.VirtualObject.FromNative(Native.ZkWorld_getRootObject(_handle, i));
		}
	}
}