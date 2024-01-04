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
		public int Index;
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
		int NodeCount { get; }
		IMultiResolutionMesh Mesh { get; }
		public int WedgeNormalCount { get; }
		List<SoftSkinWedgeNormal> WedgeNormals { get; }
		List<int> Nodes { get; }
		List<IOrientedBoundingBox> BoundingBoxes { get; }
		List<List<SoftSkinWeightEntry>> Weights { get; }
		SoftSkinWedgeNormal GetWedgeNormal(int i);
		IOrientedBoundingBox GetBoundingBox(int node);
		List<SoftSkinWeightEntry> GetWeights(int node);
	}

	[Serializable]
	public class CachedSoftSkinMesh : ISoftSkinMesh
	{
		public int NodeCount => Nodes.Count;
		public IMultiResolutionMesh Mesh { get; set; }

		public int WedgeNormalCount => WedgeNormals.Count;

		public List<SoftSkinWedgeNormal> WedgeNormals { get; set; }
		public List<int> Nodes { get; set; }
		public List<IOrientedBoundingBox> BoundingBoxes { get; set; }
		public List<List<SoftSkinWeightEntry>> Weights { get; set; }

		public SoftSkinWedgeNormal GetWedgeNormal(int i)
		{
			return WedgeNormals[i];
		}

		public IOrientedBoundingBox GetBoundingBox(int node)
		{
			return BoundingBoxes[node];
		}

		public List<SoftSkinWeightEntry> GetWeights(int node)
		{
			return Weights[node];
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

		public int NodeCount => (int)Native.ZkSoftSkinMesh_getNodeCount(_handle);
		public IMultiResolutionMesh Mesh => new MultiResolutionMesh(Native.ZkSoftSkinMesh_getMesh(_handle));

		public int WedgeNormalCount => (int)Native.ZkSoftSkinMesh_getWedgeNormalCount(_handle);

		public List<SoftSkinWedgeNormal> WedgeNormals
		{
			get
			{
				var normals = new List<SoftSkinWedgeNormal>();
				var count = WedgeNormalCount;
				for (var i = 0; i < count; ++i) normals.Add(GetWedgeNormal(i));
				return normals;
			}
		}

		public List<int> Nodes => Native.ZkSoftSkinMesh_getNodes(_handle, out var count).MarshalAsList<int>(count);

		public List<IOrientedBoundingBox> BoundingBoxes
		{
			get
			{
				var bboxes = new List<IOrientedBoundingBox>();
				var count = NodeCount;
				for (var i = 0; i < count; ++i) bboxes.Add(GetBoundingBox(i));
				return bboxes;
			}
		}

		public List<List<SoftSkinWeightEntry>> Weights
		{
			get
			{
				var weights = new List<List<SoftSkinWeightEntry>>();
				var count = (int)Native.ZkSoftSkinMesh_getWeightTotal(_handle);
				for (var i = 0; i < count; ++i) weights.Add(GetWeights(i));
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

		public SoftSkinWedgeNormal GetWedgeNormal(int i)
		{
			return Native.ZkSoftSkinMesh_getWedgeNormal(_handle, (ulong)i);
		}

		public IOrientedBoundingBox GetBoundingBox(int node)
		{
			return new OrientedBoundingBox(Native.ZkSoftSkinMesh_getBbox(_handle, (ulong)node));
		}

		public List<SoftSkinWeightEntry> GetWeights(int node)
		{
			var weights = new List<SoftSkinWeightEntry>();
			var count = (int)Native.ZkSoftSkinMesh_getWeightCount(_handle, (ulong)node);
			for (var i = 0; i < count; ++i)
				weights.Add(Native.ZkSoftSkinMesh_getWeight(_handle, (ulong)node, (ulong)i));
			return weights;
		}
	}
}