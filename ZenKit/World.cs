using ZenKit.Vobs;

namespace ZenKit;

public class World
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

	public Mesh Mesh => new(Native.ZkWorld_getMesh(_handle));
	public BspTree BspTree => new(Native.ZkWorld_getBspTree(_handle));
	public WayNet WayNet => new(Native.ZkWorld_getWayNet(_handle));
	public ulong RootObjectCount => Native.ZkWorld_getRootObjectCount(_handle);

	public List<VirtualObject> RootObjects
	{
		get
		{
			var objects = new List<VirtualObject>();

			Native.ZkWorld_enumerateRootObjects(_handle, (_, vob) =>
			{
				objects.Add(VirtualObject.FromNative(vob));
				return false;
			}, UIntPtr.Zero);

			return objects;
		}
	}

	~World()
	{
		Native.ZkWorld_del(_handle);
	}

	public VirtualObject GetRootObject(ulong i)
	{
		return VirtualObject.FromNative(Native.ZkWorld_getRootObject(_handle, i));
	}
}