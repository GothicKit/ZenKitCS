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
		public ulong WedgeNormalCount { get; }
		List<SoftSkinWedgeNormal> WedgeNormals { get; }
		List<int> Nodes { get; }
		List<IOrientedBoundingBox> BoundingBoxes { get; }
		List<List<SoftSkinWeightEntry>> Weights { get; }
		SoftSkinWedgeNormal GetWedgeNormal(ulong i);
		IOrientedBoundingBox GetBoundingBox(ulong node);
		List<SoftSkinWeightEntry> GetWeights(ulong node);
	}

	[Serializable]
	public class CachedSoftSkinMesh : ISoftSkinMesh
	{
		public ulong NodeCount => (ulong)Nodes.LongCount();
		public IMultiResolutionMesh Mesh { get; set; }

		public ulong WedgeNormalCount => (ulong)WedgeNormals.LongCount();

		public List<SoftSkinWedgeNormal> WedgeNormals { get; set; }
		public List<int> Nodes { get; set; }
		public List<IOrientedBoundingBox> BoundingBoxes { get; set; }
		public List<List<SoftSkinWeightEntry>> Weights { get; set; }

		public SoftSkinWedgeNormal GetWedgeNormal(ulong i)
		{
			return WedgeNormals[(int)i];
		}

		public IOrientedBoundingBox GetBoundingBox(ulong node)
		{
			return BoundingBoxes[(int)node];
		}

		public List<SoftSkinWeightEntry> GetWeights(ulong node)
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

		public ulong WedgeNormalCount => Native.ZkSoftSkinMesh_getWedgeNormalCount(_handle);

		public List<SoftSkinWedgeNormal> WedgeNormals
		{
			get
			{
				var normals = new List<SoftSkinWedgeNormal>();

				Native.ZkSoftSkinMesh_enumerateWedgeNormals(_handle, (_, normal) =>
				{
					normals.Add(normal);
					return false;
				}, UIntPtr.Zero);

				return normals;
			}
		}

		public List<int> Nodes => Native.ZkSoftSkinMesh_getNodes(_handle, out var count).MarshalAsList<int>(count);

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

		public List<List<SoftSkinWeightEntry>> Weights
		{
			get
			{
				var weights = new List<List<SoftSkinWeightEntry>>();

				for (ulong i = 0; i < Native.ZkSoftSkinMesh_getWeightTotal(_handle); ++i)
				{
					weights.Add(GetWeights(i));
				}

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

		public SoftSkinWedgeNormal GetWedgeNormal(ulong i)
		{
			return Native.ZkSoftSkinMesh_getWedgeNormal(_handle, i);
		}

		public IOrientedBoundingBox GetBoundingBox(ulong node)
		{
			return new OrientedBoundingBox(Native.ZkSoftSkinMesh_getBbox(_handle, node));
		}

		public List<SoftSkinWeightEntry> GetWeights(ulong node)
		{
			var weights = new List<SoftSkinWeightEntry>();

			Native.ZkSoftSkinMesh_enumerateWeights(_handle, node, (_, ptr) =>
			{
				weights.Add(Marshal.PtrToStructure<SoftSkinWeightEntry>(ptr));
				return false;
			}, UIntPtr.Zero);

			return weights;
		}
	}
}