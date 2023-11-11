using System;
using System.Collections.Generic;

namespace ZenKit
{
	public enum VfsOverwriteBehavior
	{
		None = 0,
		All = 1,
		Newer = 2,
		Older = 3
	}

	public class VfsNode
	{
		private readonly byte[]? _data;
		private readonly bool _delete;

		public VfsNode(string name, byte[] data, DateTime timestamp)
		{
			Handle = Native.ZkVfsNode_newFile(
				name,
				data,
				(ulong)data.Length,
				(ulong)timestamp.Subtract(DateTime.UnixEpoch).TotalSeconds
			);

			if (Handle == UIntPtr.Zero) throw new Exception("Failed to create Vfs node");

			_data = data;
			_delete = true;
		}

		public VfsNode(string name, DateTime timestamp)
		{
			Handle = Native.ZkVfsNode_newDir(
				name,
				(ulong)timestamp.Subtract(DateTime.UnixEpoch).TotalSeconds
			);

			if (Handle == UIntPtr.Zero) throw new Exception("Failed to create Vfs node");

			_delete = true;
		}

		internal VfsNode(UIntPtr handle)
		{
			Handle = handle;
		}

		internal UIntPtr Handle { get; }

		public DateTime Timestamp => DateTime.UnixEpoch.AddSeconds(Native.ZkVfsNode_getTime(Handle));

		public string Name => Native.ZkVfsNode_getName(Handle).MarshalAsString() ??
		                      throw new Exception("Failed to get Vfs node name");

		public Read Buffer => IsFile()
			? new Read(Native.ZkVfsNode_open(Handle))
			: throw new Exception("Buffer is only available on file nodes");

		public List<VfsNode> Children
		{
			get
			{
				var nodes = new List<VfsNode>();

				if (IsDir())
					Native.ZkVfsNode_enumerateChildren(Handle, (_, node) =>
					{
						nodes.Add(new VfsNode(node));
						return false;
					}, UIntPtr.Zero);

				return nodes;
			}
		}

		~VfsNode()
		{
			if (_delete) Native.ZkVfsNode_del(Handle);
		}

		public bool IsFile()
		{
			return Native.ZkVfsNode_isFile(Handle);
		}

		public bool IsDir()
		{
			return Native.ZkVfsNode_isDir(Handle);
		}

		public VfsNode? GetChild(string name)
		{
			if (!IsDir()) return null;
			var result = Native.ZkVfsNode_getChild(Handle, name);
			return result == UIntPtr.Zero ? null : new VfsNode(result);
		}

		public VfsNode Create(VfsNode node)
		{
			if (!IsDir()) throw new Exception("Create() is only available for directory nodes!");

			return new VfsNode(Native.ZkVfsNode_create(Handle, node.Handle));
		}

		public bool Remove(string name)
		{
			if (!IsDir()) return false;
			return Native.ZkVfsNode_remove(Handle, name);
		}
	}

	public class Vfs
	{
		public Vfs()
		{
			Handle = Native.ZkVfs_new();
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to create Vfs");
		}

		public UIntPtr Handle { get; }

		public VfsNode Root
		{
			get
			{
				var node = Native.ZkVfs_getRoot(Handle);
				if (node == UIntPtr.Zero) throw new Exception("Failed to get root Vfs node");
				return new VfsNode(node);
			}
		}

		~Vfs()
		{
			Native.ZkVfs_del(Handle);
		}

		public void Mkdir(string path)
		{
			Native.ZkVfs_mkdir(Handle, path);
		}

		public bool Remove(string path)
		{
			return Native.ZkVfs_remove(Handle, path);
		}

		public void Mount(VfsNode node, string parent, VfsOverwriteBehavior overwrite)
		{
			Native.ZkVfs_mount(Handle, node.Handle, parent, overwrite);
		}

		public void Mount(string path, string parent, VfsOverwriteBehavior overwrite)
		{
			Native.ZkVfs_mountHost(Handle, path, parent, overwrite);
		}

		public void MountDisk(Read buf, VfsOverwriteBehavior overwrite)
		{
			Native.ZkVfs_mountDisk(Handle, buf.Handle, overwrite);
		}

		public void MountDisk(string path, VfsOverwriteBehavior overwrite)
		{
			Native.ZkVfs_mountDiskHost(Handle, path, overwrite);
		}

		public VfsNode? Resolve(string path)
		{
			var result = Native.ZkVfs_resolvePath(Handle, path);
			return result == UIntPtr.Zero ? null : new VfsNode(result);
		}

		public VfsNode? Find(string name)
		{
			var result = Native.ZkVfs_findNode(Handle, name);
			return result == UIntPtr.Zero ? null : new VfsNode(result);
		}
	}
}