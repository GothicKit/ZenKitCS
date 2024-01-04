using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{
	public enum VfsOverwriteBehavior
	{
		/// <summary>
		///     Overwrite no conflicting nodes.
		/// </summary>
		None = 0,

		/// <summary>
		///     Overwrite all conflicting nodes.
		/// </summary>
		All = 1,

		/// <summary>
		///     Overwrite newer conflicting nodes.
		/// </summary>
		Newer = 2,

		/// <summary>
		///     Overwrite older conflicting nodes.
		/// </summary>
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
				if (!IsDir()) return nodes;
				
				var gch = GCHandle.Alloc(nodes);
				Native.ZkVfsNode_enumerateChildren(Handle, ChildEnumerator, GCHandle.ToIntPtr(gch));
				gch.Free();
				
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
		
		private static readonly Native.Callbacks.ZkVfsNodeEnumerator ChildEnumerator = _enumerateChildrenHandler;

		[MonoPInvokeCallback]
		private static bool _enumerateChildrenHandler(IntPtr ctx, UIntPtr ptr)
		{
			var list = (List<VfsNode>)GCHandle.FromIntPtr(ctx).Target;
			list.Add(new VfsNode(ptr));
			return false;
		}
	}

	/// <summary>
	///     An implementation of the ZenGin's virtual file system.
	/// </summary>
	/// <seealso href="https://zk.gothickit.dev/library/api/virtual-file-system/" />
	public class Vfs
	{
		public Vfs()
		{
			Handle = Native.ZkVfs_new();
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to create Vfs");
		}

		internal UIntPtr Handle { get; }

		/// <summary>
		///     The root node of the virtual file system structure.
		/// </summary>
		/// <exception cref="IOException">Thrown if accessing the native object fails.</exception>
		public VfsNode Root
		{
			get
			{
				var node = Native.ZkVfs_getRoot(Handle);
				if (node == UIntPtr.Zero) throw new IOException("Failed to get root Vfs node");
				return new VfsNode(node);
			}
		}

		~Vfs()
		{
			Native.ZkVfs_del(Handle);
		}

		/// <summary>
		///     Create all missing directories in the given path.
		/// </summary>
		/// <param name="path">Create all missing directories in the given path.</param>
		/// <returns>The newly created directory or <c>null</c> if creating the directory fails..</returns>
		public VfsNode? Mkdir(string path)
		{
			var p = Native.ZkVfs_mkdir(Handle, path);
			return p == UIntPtr.Zero ? null : new VfsNode(p);
		}

		/// <summary>
		///     Delete the file or directory at the given path
		/// </summary>
		/// <param name="path">The path of the node to delete.</param>
		/// <returns><c>true</c> if removal was successful and <c>false</c> if not (ie. the file could not be found).</returns>
		public bool Remove(string path)
		{
			return Native.ZkVfs_remove(Handle, path);
		}

		/// <summary>
		///     <b>Mount the given file system node into the given directory.</b> When the given <paramref name="node" /> is a
		///     directory, it is merged with any existing directory with the same name in the given <paramref name="parent" />
		///     path.
		/// </summary>
		/// <param name="node">The file system node to mount.</param>
		/// <param name="parent">The path of the parent node to mount into.</param>
		/// <param name="overwrite">The behavior of the system when conflicting files are found.</param>
		public void Mount(VfsNode node, string parent, VfsOverwriteBehavior overwrite)
		{
			Native.ZkVfs_mount(Handle, node.Handle, parent, overwrite);
		}

		/// <summary>
		///     Mount a file or directory from the host file system into the Vfs.
		/// </summary>
		/// <remarks>If a path to a directory is provided, only its children are mounted, not the directory itself.</remarks>
		/// <param name="path">The path of the file or directory to mount.</param>
		/// <param name="parent">The path of the parent node to mount into.</param>
		/// <param name="overwrite">The behavior of the system when conflicting files are found.</param>
		public void Mount(string path, string parent, VfsOverwriteBehavior overwrite)
		{
			Native.ZkVfs_mountHost(Handle, path, parent, overwrite);
		}

		/// <summary>
		///     <b>Mount the disk file in the given buffer into the file system.</b> The disk contents are mounted at the root node
		///     of the file system and existing directories are merged together.
		/// </summary>
		/// <param name="buf">A buffer containing the disk file contents.</param>
		/// <param name="overwrite">The behavior of the system when conflicting files are found.</param>
		public void MountDisk(Read buf, VfsOverwriteBehavior overwrite)
		{
			Native.ZkVfs_mountDisk(Handle, buf.Handle, overwrite);
		}

		/// <summary>
		///     <b>Mount the disk file at the given host path into the file system.</b> The disk contents are mounted at the root
		///     node of the file system and existing directories are merged together.
		/// </summary>
		/// <param name="path">The path of the disk to mount.</param>
		/// <param name="overwrite">The behavior of the system when conflicting files are found.</param>
		public void MountDisk(string path, VfsOverwriteBehavior overwrite)
		{
			Native.ZkVfs_mountDiskHost(Handle, path, overwrite);
		}

		/// <summary>
		///     Resolve the given path in the Vfs to a file system node.
		/// </summary>
		/// <param name="path">The path to the node to resolve.</param>
		/// <returns>The node at the given path or <c>null</c> if the path could not be resolved.</returns>
		public VfsNode? Resolve(string path)
		{
			var result = Native.ZkVfs_resolvePath(Handle, path);
			return result == UIntPtr.Zero ? null : new VfsNode(result);
		}

		/// <summary>
		///     Find the first node with the given name in the Vfs.
		/// </summary>
		/// <param name="name">The name of the node to find.</param>
		/// <returns>The node with the given name or <c>null</c> if no node with the given name was found.</returns>
		public VfsNode? Find(string name)
		{
			var result = Native.ZkVfs_findNode(Handle, name);
			return result == UIntPtr.Zero ? null : new VfsNode(result);
		}
	}
}