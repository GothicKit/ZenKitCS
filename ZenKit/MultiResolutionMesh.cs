using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 6)]
	public struct MeshTriangle
	{
		public ushort Wedge0, Wedge1, Wedge2;
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 6)]
	public struct MeshTriangleEdge
	{
		public ushort Edge0, Edge1, Edge2;
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 4)]
	public struct MeshEdge
	{
		public ushort Edge0, Edge1;
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 24)]
	public struct MeshWedge
	{
		public Vector3 Normal;
		public Vector2 Texture;
		public ushort Index;
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	public struct MeshPlane
	{
		public float Distance;
		public Vector3 Normal;
	}

	namespace Materialized
	{
		[Serializable]
		public struct MultiResolutionSubMesh
		{
			public Material Material;
			public MeshTriangle[] Triangles;
			public MeshWedge[] Wedges;
			public float[] Colors;
			public ushort[] TrianglePlaneIndices;
			public MeshPlane[] TrianglePlanes;
			public MeshTriangleEdge[] TriangleEdges;
			public MeshEdge[] Edges;
			public float[] EdgeScores;
			public ushort[] WedgeMap;
		}

		[Serializable]
		public struct MultiResolutionMesh
		{
			public Vector3[] Positions;
			public Vector3[] Normals;
			public List<MultiResolutionSubMesh> SubMeshes;
			public List<Material> Materials;
			public bool AlphaTest;
			public AxisAlignedBoundingBox BoundingBox;
			public OrientedBoundingBox OrientedBoundingBox;
		}
	}

	public class MultiResolutionSubMesh : IMaterializing<Materialized.MultiResolutionSubMesh>
	{
		private readonly UIntPtr _handle;

		internal MultiResolutionSubMesh(UIntPtr handle)
		{
			_handle = handle;
		}

		public Material Material => new Material(Native.ZkSubMesh_getMaterial(_handle));

		public MeshTriangle[] Triangles =>
			Native.ZkSubMesh_getTriangles(_handle, out var count).MarshalAsArray<MeshTriangle>(count);

		public MeshWedge[] Wedges =>
			Native.ZkSubMesh_getWedges(_handle, out var count).MarshalAsArray<MeshWedge>(count);

		public float[] Colors => Native.ZkSubMesh_getColors(_handle, out var count).MarshalAsArray<float>(count);

		public ushort[] TrianglePlaneIndices => Native.ZkSubMesh_getTrianglePlaneIndices(_handle, out var count)
			.MarshalAsArray<ushort>(count);

		public MeshPlane[] TrianglePlanes =>
			Native.ZkSubMesh_getTrianglePlanes(_handle, out var count).MarshalAsArray<MeshPlane>(count);

		public MeshTriangleEdge[] TriangleEdges => Native.ZkSubMesh_getTriangleEdges(_handle, out var count)
			.MarshalAsArray<MeshTriangleEdge>(count);

		public MeshEdge[] Edges => Native.ZkSubMesh_getEdges(_handle, out var count).MarshalAsArray<MeshEdge>(count);

		public float[] EdgeScores =>
			Native.ZkSubMesh_getEdgeScores(_handle, out var count).MarshalAsArray<float>(count);

		public ushort[] WedgeMap => Native.ZkSubMesh_getWedgeMap(_handle, out var count).MarshalAsArray<ushort>(count);

		public Materialized.MultiResolutionSubMesh Materialize()
		{
			return new Materialized.MultiResolutionSubMesh
			{
				Material = Material.Materialize(),
				Triangles = Triangles,
				Wedges = Wedges,
				Colors = Colors,
				TrianglePlaneIndices = TrianglePlaneIndices,
				TrianglePlanes = TrianglePlanes,
				TriangleEdges = TriangleEdges,
				Edges = Edges,
				EdgeScores = EdgeScores,
				WedgeMap = WedgeMap
			};
		}
	}

	public class MultiResolutionMesh : IMaterializing<Materialized.MultiResolutionMesh>
	{
		private readonly bool _delete = true;
		private readonly UIntPtr _handle;

		public MultiResolutionMesh(Read buf)
		{
			_handle = Native.ZkMultiResolutionMesh_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load multi resolution mesh");
		}

		public MultiResolutionMesh(string path)
		{
			_handle = Native.ZkMultiResolutionMesh_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load multi resolution mesh");
		}

		public MultiResolutionMesh(Vfs vfs, string name)
		{
			_handle = Native.ZkMultiResolutionMesh_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load multi resolution mesh");
		}

		internal MultiResolutionMesh(UIntPtr handle)
		{
			_handle = handle;
			_delete = false;
		}

		public Vector3[] Positions =>
			Native.ZkMultiResolutionMesh_getPositions(_handle, out var count).MarshalAsArray<Vector3>(count);

		public Vector3[] Normals =>
			Native.ZkMultiResolutionMesh_getNormals(_handle, out var count).MarshalAsArray<Vector3>(count);

		public ulong SubMeshCount => Native.ZkMultiResolutionMesh_getSubMeshCount(_handle);

		public List<MultiResolutionSubMesh> SubMeshes
		{
			get
			{
				var meshes = new List<MultiResolutionSubMesh>();

				Native.ZkMultiResolutionMesh_enumerateSubMeshes(_handle, (_, mesh) =>
				{
					meshes.Add(new MultiResolutionSubMesh(mesh));
					return false;
				}, UIntPtr.Zero);

				return meshes;
			}
		}

		public ulong MaterialCount => Native.ZkMultiResolutionMesh_getMaterialCount(_handle);

		public List<Material> Materials
		{
			get
			{
				var materials = new List<Material>();

				Native.ZkMultiResolutionMesh_enumerateMaterials(_handle, (_, mat) =>
				{
					materials.Add(new Material(mat));
					return false;
				}, UIntPtr.Zero);

				return materials;
			}
		}

		public bool AlphaTest => Native.ZkMultiResolutionMesh_getAlphaTest(_handle);
		public AxisAlignedBoundingBox BoundingBox => Native.ZkMultiResolutionMesh_getBbox(_handle);

		public OrientedBoundingBox OrientedBoundingBox =>
			new OrientedBoundingBox(Native.ZkMultiResolutionMesh_getOrientedBbox(_handle));

		public Materialized.MultiResolutionMesh Materialize()
		{
			return new Materialized.MultiResolutionMesh
			{
				Positions = Positions,
				Normals = Normals,
				SubMeshes = SubMeshes.ConvertAll(sm => sm.Materialize()),
				Materials = Materials.ConvertAll(mat => mat.Materialize()),
				AlphaTest = AlphaTest,
				BoundingBox = BoundingBox,
				OrientedBoundingBox = OrientedBoundingBox.Materialize()
			};
		}

		~MultiResolutionMesh()
		{
			if (_delete) Native.ZkMultiResolutionMesh_del(_handle);
		}

		public MultiResolutionSubMesh? GetSubMesh(ulong i)
		{
			var mesh = Native.ZkMultiResolutionMesh_getSubMesh(_handle, i);
			return mesh == UIntPtr.Zero ? null : new MultiResolutionSubMesh(mesh);
		}

		public Material? GetMaterial(ulong i)
		{
			var mesh = Native.ZkMultiResolutionMesh_getMaterial(_handle, i);
			return mesh == UIntPtr.Zero ? null : new Material(mesh);
		}
	}
}