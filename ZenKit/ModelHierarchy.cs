using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.NativeLoader.NativeStructs;
using ZenKit.Util;

namespace ZenKit
{
	namespace Materialized
	{
		[Serializable]
		public struct ModelHierarchyNode
		{
			public short ParentIndex;
			public string Name;
			public Matrix4x4 Transform;
		}

		[Serializable]
		public struct ModelHierarchy
		{
			public AxisAlignedBoundingBox BoundingBox;
			public AxisAlignedBoundingBox CollisionBoundingBox;
			public Vector3 RootTranslation;
			public uint Checksum;
			public DateTime SourceDate;
			public string SourcePath;
			public List<ModelHierarchyNode> Nodes;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct ModelHierarchyNode : IMaterializing<Materialized.ModelHierarchyNode>
	{
		public short ParentIndex;
		private IntPtr _name;
		private ZkMat4x4 _transform;

		public string Name =>
			_name.MarshalAsString() ?? throw new Exception("Failed to load model hierarchy node name");

		public Matrix4x4 Transform => _transform.ToCSharp();

		public Materialized.ModelHierarchyNode Materialize()
		{
			return new Materialized.ModelHierarchyNode
			{
				ParentIndex = ParentIndex,
				Name = Name,
				Transform = Transform
			};
		}
	}

	public class ModelHierarchy : IMaterializing<Materialized.ModelHierarchy>
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

		public ulong NodeCount => Native.ZkModelHierarchy_getNodeCount(_handle);
		public AxisAlignedBoundingBox BoundingBox => Native.ZkModelHierarchy_getBbox(_handle);
		public AxisAlignedBoundingBox CollisionBoundingBox => Native.ZkModelHierarchy_getCollisionBbox(_handle);
		public Vector3 RootTranslation => Native.ZkModelHierarchy_getRootTranslation(_handle);
		public uint Checksum => Native.ZkModelHierarchy_getChecksum(_handle);
		public DateTime SourceDate => Native.ZkModelHierarchy_getSourceDate(_handle).AsDateTime();

		public string SourcePath => Native.ZkModelHierarchy_getSourcePath(_handle).MarshalAsString() ??
		                            throw new Exception("Failed to load model hierarchy source path");

		public List<ModelHierarchyNode> Nodes
		{
			get
			{
				var nodes = new List<ModelHierarchyNode>();

				Native.ZkModelHierarchy_enumerateNodes(_handle, (_, node) =>
				{
					nodes.Add(Marshal.PtrToStructure<ModelHierarchyNode>(node));
					return false;
				}, UIntPtr.Zero);

				return nodes;
			}
		}

		public Materialized.ModelHierarchy Materialize()
		{
			return new Materialized.ModelHierarchy
			{
				BoundingBox = BoundingBox,
				CollisionBoundingBox = CollisionBoundingBox,
				RootTranslation = RootTranslation,
				Checksum = Checksum,
				SourceDate = SourceDate,
				SourcePath = SourcePath,
				Nodes = Nodes.ConvertAll(node => node.Materialize())
			};
		}

		~ModelHierarchy()
		{
			if (_delete) Native.ZkModelHierarchy_del(_handle);
		}

		public ModelHierarchyNode GetNode(ulong i)
		{
			return Native.ZkModelHierarchy_getNode(_handle, i);
		}
	}
}