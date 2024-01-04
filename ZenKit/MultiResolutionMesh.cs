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


	public interface IMultiResolutionSubMesh : ICacheable<IMultiResolutionSubMesh>
	{
		IMaterial Material { get; }
		List<MeshTriangle> Triangles { get; }
		public int WedgeCount { get; }
		List<MeshWedge> Wedges { get; }
		List<float> Colors { get; }
		List<ushort> TrianglePlaneIndices { get; }
		public int TrianglePlaneCount { get; }
		List<MeshPlane> TrianglePlanes { get; }
		List<MeshTriangleEdge> TriangleEdges { get; }
		List<MeshEdge> Edges { get; }
		List<float> EdgeScores { get; }
		List<ushort> WedgeMap { get; }

		MeshWedge GetWedge(int i);
		MeshPlane GetTrianglePlane(int i);
	}

	[Serializable]
	public class CachedMultiResolutionSubMesh : IMultiResolutionSubMesh
	{
		public IMaterial Material { get; set; }
		public List<MeshTriangle> Triangles { get; set; }

		public int WedgeCount => Wedges.Count;

		public List<MeshWedge> Wedges { get; set; }
		public List<float> Colors { get; set; }
		public List<ushort> TrianglePlaneIndices { get; set; }

		public int TrianglePlaneCount => TrianglePlanes.Count;

		public List<MeshPlane> TrianglePlanes { get; set; }
		public List<MeshTriangleEdge> TriangleEdges { get; set; }
		public List<MeshEdge> Edges { get; set; }
		public List<float> EdgeScores { get; set; }
		public List<ushort> WedgeMap { get; set; }

		public MeshWedge GetWedge(int i)
		{
			return Wedges[i];
		}

		public MeshPlane GetTrianglePlane(int i)
		{
			return TrianglePlanes[i];
		}

		public IMultiResolutionSubMesh Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class MultiResolutionSubMesh : IMultiResolutionSubMesh
	{
		private readonly UIntPtr _handle;

		internal MultiResolutionSubMesh(UIntPtr handle)
		{
			_handle = handle;
		}

		public IMaterial Material => new Material(Native.ZkSubMesh_getMaterial(_handle));

		public List<MeshTriangle> Triangles =>
			Native.ZkSubMesh_getTriangles(_handle, out var count).MarshalAsList<MeshTriangle>(count);

		public int WedgeCount => (int)Native.ZkSubMesh_getWedgeCount(_handle);

		public List<MeshWedge> Wedges
		{
			get
			{
				var wedges = new List<MeshWedge>();
				var count = WedgeCount;
				for (var i = 0;i < count; ++i) wedges.Add(GetWedge(i));
				return wedges;
			}
		}

		public List<float> Colors => Native.ZkSubMesh_getColors(_handle, out var count).MarshalAsList<float>(count);

		public List<ushort> TrianglePlaneIndices => Native.ZkSubMesh_getTrianglePlaneIndices(_handle, out var count)
			.MarshalAsList<ushort>(count);

		public int TrianglePlaneCount => (int)Native.ZkSubMesh_getTrianglePlaneCount(_handle);

		public List<MeshPlane> TrianglePlanes
		{
			get
			{
				var planes = new List<MeshPlane>();
				var count = TrianglePlaneCount;
				for (var i = 0;i < count; ++i) planes.Add(GetTrianglePlane(i));
				return planes;
			}
		}

		public List<MeshTriangleEdge> TriangleEdges => Native.ZkSubMesh_getTriangleEdges(_handle, out var count)
			.MarshalAsList<MeshTriangleEdge>(count);

		public List<MeshEdge> Edges => Native.ZkSubMesh_getEdges(_handle, out var count).MarshalAsList<MeshEdge>(count);

		public List<float> EdgeScores =>
			Native.ZkSubMesh_getEdgeScores(_handle, out var count).MarshalAsList<float>(count);

		public List<ushort> WedgeMap =>
			Native.ZkSubMesh_getWedgeMap(_handle, out var count).MarshalAsList<ushort>(count);

		public IMultiResolutionSubMesh Cache()
		{
			return new CachedMultiResolutionSubMesh
			{
				Material = Material.Cache(),
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

		public bool IsCached()
		{
			return false;
		}

		public MeshWedge GetWedge(int i)
		{
			return Native.ZkSubMesh_getWedge(_handle, (ulong)i);
		}

		public MeshPlane GetTrianglePlane(int i)
		{
			return Native.ZkSubMesh_getTrianglePlane(_handle, (ulong)i);
		}
	}

	public interface IMultiResolutionMesh : ICacheable<IMultiResolutionMesh>
	{
		public int PositionCount { get; }
		List<Vector3> Positions { get; }
		public int NormalCount { get; }
		List<Vector3> Normals { get; }
		int SubMeshCount { get; }
		List<IMultiResolutionSubMesh> SubMeshes { get; }
		int MaterialCount { get; }
		List<IMaterial> Materials { get; }
		bool AlphaTest { get; }
		AxisAlignedBoundingBox BoundingBox { get; }
		IOrientedBoundingBox OrientedBoundingBox { get; }
		IMultiResolutionSubMesh? GetSubMesh(int i);
		IMaterial? GetMaterial(int i);
		Vector3 GetPosition(int i);
		Vector3 GetNormal(int i);
	}

	[Serializable]
	public class CachedMultiResolutionMesh : IMultiResolutionMesh
	{
		public int PositionCount => Positions.Count;
		public List<Vector3> Positions { get; set; }
		public int NormalCount => Normals.Count;
		public List<Vector3> Normals { get; set; }
		public int SubMeshCount => SubMeshes.Count;
		public List<IMultiResolutionSubMesh> SubMeshes { get; set; }
		public int MaterialCount => Materials.Count;
		public List<IMaterial> Materials { get; set; }
		public bool AlphaTest { get; set; }
		public AxisAlignedBoundingBox BoundingBox { get; set; }
		public IOrientedBoundingBox OrientedBoundingBox { get; set; }

		public IMultiResolutionSubMesh? GetSubMesh(int i)
		{
			return SubMeshes[i];
		}

		public IMaterial? GetMaterial(int i)
		{
			return Materials[i];
		}

		public Vector3 GetPosition(int i)
		{
			return Positions[i];
		}

		public Vector3 GetNormal(int i)
		{
			return Normals[i];
		}

		public IMultiResolutionMesh Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class MultiResolutionMesh : IMultiResolutionMesh
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

		public int PositionCount => (int)Native.ZkMultiResolutionMesh_getPositionCount(_handle);

		public List<Vector3> Positions
		{
			get
			{
				var positions = new List<Vector3>();
				var count = PositionCount;
				for (var i = 0;i < count; ++i) positions.Add(GetPosition(i));
				return positions;
			}
		}

		public int NormalCount => (int)Native.ZkMultiResolutionMesh_getNormalCount(_handle);

		public List<Vector3> Normals
		{
			get
			{
				var positions = new List<Vector3>();
				var count = NormalCount;
				for (var i = 0;i < count; ++i) positions.Add(GetNormal(i));
				return positions;
			}
		}

		public int SubMeshCount => (int)Native.ZkMultiResolutionMesh_getSubMeshCount(_handle);

		public List<IMultiResolutionSubMesh> SubMeshes
		{
			get
			{
				var meshes = new List<IMultiResolutionSubMesh>();
				var count = SubMeshCount;
				for (var i = 0;i < count; ++i) meshes.Add(GetSubMesh(i));
				return meshes;
			}
		}

		public int MaterialCount => (int)Native.ZkMultiResolutionMesh_getMaterialCount(_handle);

		public List<IMaterial> Materials
		{
			get
			{
				var materials = new List<IMaterial>();
				var count = MaterialCount;
				for (var i = 0;i < count; ++i) materials.Add(GetMaterial(i));
				return materials;
			}
		}

		public bool AlphaTest => Native.ZkMultiResolutionMesh_getAlphaTest(_handle);
		public AxisAlignedBoundingBox BoundingBox => Native.ZkMultiResolutionMesh_getBbox(_handle);

		public IOrientedBoundingBox OrientedBoundingBox =>
			new OrientedBoundingBox(Native.ZkMultiResolutionMesh_getOrientedBbox(_handle));

		public IMultiResolutionMesh Cache()
		{
			return new CachedMultiResolutionMesh
			{
				Positions = Positions,
				Normals = Normals,
				SubMeshes = SubMeshes.ConvertAll(sm => sm.Cache()),
				Materials = Materials.ConvertAll(mat => mat.Cache()),
				AlphaTest = AlphaTest,
				BoundingBox = BoundingBox,
				OrientedBoundingBox = OrientedBoundingBox.Cache()
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IMultiResolutionSubMesh? GetSubMesh(int i)
		{
			var mesh = Native.ZkMultiResolutionMesh_getSubMesh(_handle, (ulong)i);
			return mesh == UIntPtr.Zero ? null : new MultiResolutionSubMesh(mesh);
		}

		public IMaterial? GetMaterial(int i)
		{
			var mesh = Native.ZkMultiResolutionMesh_getMaterial(_handle, (ulong)i);
			return mesh == UIntPtr.Zero ? null : new Material(mesh);
		}

		public Vector3 GetPosition(int i)
		{
			return Native.ZkMultiResolutionMesh_getPosition(_handle, (ulong)i);
		}

		public Vector3 GetNormal(int i)
		{
			return Native.ZkMultiResolutionMesh_getNormal(_handle, (ulong)i);
		}

		~MultiResolutionMesh()
		{
			if (_delete) Native.ZkMultiResolutionMesh_del(_handle);
		}
	}
}