using System;
using System.Collections.Generic;
using System.Linq;
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

	public interface ISoftSkinMesh : ICacheable<ISoftSkinMesh>
	{
		ulong NodeCount { get; }
		IMultiResolutionMesh Mesh { get; }
		SoftSkinWedgeNormal[] WedgeNormals { get; }
		int[] Nodes { get; }
		List<IOrientedBoundingBox> BoundingBoxes { get; }
		List<SoftSkinWeightEntry[]> Weights { get; }
		IOrientedBoundingBox GetBoundingBox(ulong node);
		SoftSkinWeightEntry[] GetWeights(ulong node);
	}

	[Serializable]
	public class CachedSoftSkinMesh : ISoftSkinMesh
	{
		public ulong NodeCount => (ulong)Nodes.LongCount();
		public IMultiResolutionMesh Mesh { get; set; }
		public SoftSkinWedgeNormal[] WedgeNormals { get; set; }
		public int[] Nodes { get; set; }
		public List<IOrientedBoundingBox> BoundingBoxes { get; set; }
		public List<SoftSkinWeightEntry[]> Weights { get; set; }

		public IOrientedBoundingBox GetBoundingBox(ulong node)
		{
			return BoundingBoxes[(int)node];
		}

		public SoftSkinWeightEntry[] GetWeights(ulong node)
		{
			return Weights[(int)node];
		}

		public ISoftSkinMesh Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class SoftSkinMesh : ISoftSkinMesh
	{
		private readonly UIntPtr _handle;

		internal SoftSkinMesh(UIntPtr handle)
		{
			_handle = handle;
		}

		public ulong NodeCount => Native.ZkSoftSkinMesh_getNodeCount(_handle);
		public IMultiResolutionMesh Mesh => new MultiResolutionMesh(Native.ZkSoftSkinMesh_getMesh(_handle));

		public SoftSkinWedgeNormal[] WedgeNormals => Native.ZkSoftSkinMesh_getWedgeNormals(_handle, out var count)
			.MarshalAsArray<SoftSkinWedgeNormal>(count);

		public int[] Nodes => Native.ZkSoftSkinMesh_getNodes(_handle, out var count).MarshalAsArray<int>(count);

		public List<IOrientedBoundingBox> BoundingBoxes
		{
			get
			{
				var bboxes = new List<IOrientedBoundingBox>();

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

		public ISoftSkinMesh Cache()
		{
			return new CachedSoftSkinMesh
			{
				Mesh = Mesh.Cache(),
				WedgeNormals = WedgeNormals,
				Nodes = Nodes,
				BoundingBoxes = BoundingBoxes.ConvertAll(obb => obb.Cache()),
				Weights = Weights
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IOrientedBoundingBox GetBoundingBox(ulong node)
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