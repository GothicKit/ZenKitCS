using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	public struct SoftSkinWedgeNormal
	{
		public Vector3 Normal;
		public uint Index;
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 20)]
	public struct SoftSkinWeightEntry
	{
		public float Weight;
		public Vector3 Position;
		public byte NodeIndex;
	}

	namespace Materialized
	{
		[Serializable]
		public struct SoftSkinMesh
		{
			public MultiResolutionMesh Mesh;
			public SoftSkinWedgeNormal[] WedgeNormals;
			public int[] Nodes;
			public List<OrientedBoundingBox> BoundingBoxes;
			public List<SoftSkinWeightEntry[]> Weights;
		}
	}

	public class SoftSkinMesh : IMaterializing<Materialized.SoftSkinMesh>
	{
		private readonly UIntPtr _handle;

		internal SoftSkinMesh(UIntPtr handle)
		{
			_handle = handle;
		}

		public ulong NodeCount => Native.ZkSoftSkinMesh_getNodeCount(_handle);
		public MultiResolutionMesh Mesh => new MultiResolutionMesh(Native.ZkSoftSkinMesh_getMesh(_handle));

		public SoftSkinWedgeNormal[] WedgeNormals => Native.ZkSoftSkinMesh_getWedgeNormals(_handle, out var count)
			.MarshalAsArray<SoftSkinWedgeNormal>(count);

		public int[] Nodes => Native.ZkSoftSkinMesh_getNodes(_handle, out var count).MarshalAsArray<int>(count);

		public List<OrientedBoundingBox> BoundingBoxes
		{
			get
			{
				var bboxes = new List<OrientedBoundingBox>();

				Native.ZkSoftSkinMesh_enumerateBoundingBoxes(_handle, (_, box) =>
				{
					bboxes.Add(new OrientedBoundingBox(box));
					return false;
				}, UIntPtr.Zero);

				return bboxes;
			}
		}

		public List<SoftSkinWeightEntry[]> Weights
		{
			get
			{
				var weights = new List<SoftSkinWeightEntry[]>();

				Native.ZkSoftSkinMesh_enumerateWeights(_handle, (_, ptr, count) =>
				{
					weights.Add(ptr.MarshalAsArray<SoftSkinWeightEntry>(count));
					return false;
				}, UIntPtr.Zero);

				return weights;
			}
		}

		public Materialized.SoftSkinMesh Materialize()
		{
			return new Materialized.SoftSkinMesh
			{
				Mesh = Mesh.Materialize(),
				WedgeNormals = WedgeNormals,
				Nodes = Nodes,
				BoundingBoxes = BoundingBoxes.ConvertAll(obb => obb.Materialize()),
				Weights = Weights
			};
		}

		public OrientedBoundingBox GetBoundingBox(ulong node)
		{
			return new OrientedBoundingBox(Native.ZkSoftSkinMesh_getBbox(_handle, node));
		}

		public SoftSkinWeightEntry[] GetWeights(ulong node)
		{
			return Native.ZkSoftSkinMesh_getWeights(_handle, node, out var count)
				.MarshalAsArray<SoftSkinWeightEntry>(count);
		}
	}
}