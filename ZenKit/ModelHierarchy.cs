using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{
	public interface IModelHierarchyNode : ICacheable<IModelHierarchyNode>
	{
		short ParentIndex { get; }
		string Name { get; }
		Matrix4x4 Transform { get; }
	}

	[Serializable]
	public class CachedModelHierarchyNode : IModelHierarchyNode
	{
		public short ParentIndex { get; set; }
		public string Name { get; set; }
		public Matrix4x4 Transform { get; set; }

		public IModelHierarchyNode Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}


	[StructLayout(LayoutKind.Sequential)]
	public struct ModelHierarchyNode : IModelHierarchyNode
	{
		public short ParentIndex { get; }
		private IntPtr _name;
		private Native.Structs.ZkMat4x4 _transform;

		public string Name =>
			_name.MarshalAsString() ?? throw new Exception("Failed to load model hierarchy node name");

		public Matrix4x4 Transform => _transform.ToCSharp();

		public IModelHierarchyNode Cache()
		{
			return new CachedModelHierarchyNode
			{
				ParentIndex = ParentIndex,
				Name = Name,
				Transform = Transform
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IModelHierarchy : ICacheable<IModelHierarchy>
	{
		int NodeCount { get; }
		AxisAlignedBoundingBox BoundingBox { get; }
		AxisAlignedBoundingBox CollisionBoundingBox { get; }
		Vector3 RootTranslation { get; }
		int Checksum { get; }
		DateTime? SourceDate { get; }
		string SourcePath { get; }
		List<IModelHierarchyNode> Nodes { get; }
		IModelHierarchyNode GetNode(int i);
	}

	[Serializable]
	public class CachedModelHierarchy : IModelHierarchy
	{
		public int NodeCount => Nodes.Count;
		public AxisAlignedBoundingBox BoundingBox { get; set; }
		public AxisAlignedBoundingBox CollisionBoundingBox { get; set; }
		public Vector3 RootTranslation { get; set; }
		public int Checksum { get; set; }
		public DateTime? SourceDate { get; set; }
		public string SourcePath { get; set; }
		public List<IModelHierarchyNode> Nodes { get; set; }

		public IModelHierarchyNode GetNode(int i)
		{
			return Nodes[i];
		}

		public IModelHierarchy Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class ModelHierarchy : IModelHierarchy
	{
		private readonly bool _delete = true;
		private readonly UIntPtr _handle;

		public ModelHierarchy(string path)
		{
			_handle = Native.ZkModelHierarchy_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model hierarchy");
		}

		public ModelHierarchy(Read buf)
		{
			_handle = Native.ZkModelHierarchy_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model hierarchy");
		}

		public ModelHierarchy(Vfs vfs, string name)
		{
			_handle = Native.ZkModelHierarchy_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model hierarchy");
		}

		internal ModelHierarchy(UIntPtr handle)
		{
			_handle = handle;
			_delete = false;
		}

		public int NodeCount => (int)Native.ZkModelHierarchy_getNodeCount(_handle);
		public AxisAlignedBoundingBox BoundingBox => Native.ZkModelHierarchy_getBbox(_handle);
		public AxisAlignedBoundingBox CollisionBoundingBox => Native.ZkModelHierarchy_getCollisionBbox(_handle);
		public Vector3 RootTranslation => Native.ZkModelHierarchy_getRootTranslation(_handle);
		public int Checksum => (int)Native.ZkModelHierarchy_getChecksum(_handle);

		public DateTime? SourceDate => Native.ZkModelHierarchy_getSourceDate(_handle).AsDateTime();

		public string SourcePath => Native.ZkModelHierarchy_getSourcePath(_handle).MarshalAsString() ??
		                            throw new Exception("Failed to load model hierarchy source path");

		public List<IModelHierarchyNode> Nodes
		{
			get
			{
				var nodes = new List<IModelHierarchyNode>();

				Native.ZkModelHierarchy_enumerateNodes(_handle, (_, node) =>
				{
					nodes.Add(Marshal.PtrToStructure<ModelHierarchyNode>(node));
					return false;
				}, UIntPtr.Zero);

				return nodes;
			}
		}

		public IModelHierarchy Cache()
		{
			return new CachedModelHierarchy
			{
				BoundingBox = BoundingBox,
				CollisionBoundingBox = CollisionBoundingBox,
				RootTranslation = RootTranslation,
				Checksum = Checksum,
				SourceDate = SourceDate,
				SourcePath = SourcePath,
				Nodes = Nodes.ConvertAll(node => node.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IModelHierarchyNode GetNode(int i)
		{
			return Native.ZkModelHierarchy_getNode(_handle, (ulong)i);
		}

		~ModelHierarchy()
		{
			if (_delete) Native.ZkModelHierarchy_del(_handle);
		}
	}
}