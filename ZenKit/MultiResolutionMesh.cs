using System;
using System.Collections.Generic;
using System.Linq;
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
		List<MeshWedge> Wedges { get; }
		List<float> Colors { get; }
		List<ushort> TrianglePlaneIndices { get; }
		List<MeshPlane> TrianglePlanes { get; }
		List<MeshTriangleEdge> TriangleEdges { get; }
		List<MeshEdge> Edges { get; }
		List<float> EdgeScores { get; }
		List<ushort> WedgeMap { get; }
	}

	[Serializable]
	public class CachedMultiResolutionSubMesh : IMultiResolutionSubMesh
	{
		public IMaterial Material { get; set; }
		public List<MeshTriangle> Triangles { get; set; }
		public List<MeshWedge> Wedges { get; set; }
		public List<float> Colors { get; set; }
		public List<ushort> TrianglePlaneIndices { get; set; }
		public List<MeshPlane> TrianglePlanes { get; set; }
		public List<MeshTriangleEdge> TriangleEdges { get; set; }
		public List<MeshEdge> Edges { get; set; }
		public List<float> EdgeScores { get; set; }
		public List<ushort> WedgeMap { get; set; }

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

		public List<MeshWedge> Wedges =>
			Native.ZkSubMesh_getWedges(_handle, out var count).MarshalAsList<MeshWedge>(count);

		public List<float> Colors => Native.ZkSubMesh_getColors(_handle, out var count).MarshalAsList<float>(count);

		public List<ushort> TrianglePlaneIndices => Native.ZkSubMesh_getTrianglePlaneIndices(_handle, out var count)
			.MarshalAsList<ushort>(count);

		public List<MeshPlane> TrianglePlanes =>
			Native.ZkSubMesh_getTrianglePlanes(_handle, out var count).MarshalAsList<MeshPlane>(count);

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
	}

	public interface IMultiResolutionMesh : ICacheable<IMultiResolutionMesh>
	{
		List<Vector3> Positions { get; }
		List<Vector3> Normals { get; }
		ulong SubMeshCount { get; }
		List<IMultiResolutionSubMesh> SubMeshes { get; }
		ulong MaterialCount { get; }
		List<IMaterial> Materials { get; }
		bool AlphaTest { get; }
		AxisAlignedBoundingBox BoundingBox { get; }
		IOrientedBoundingBox OrientedBoundingBox { get; }
		IMultiResolutionSubMesh? GetSubMesh(ulong i);
		IMaterial? GetMaterial(ulong i);
	}

	[Serializable]
	public class CachedMultiResolutionMesh : IMultiResolutionMesh
	{
		public List<Vector3> Positions { get; set; }
		public List<Vector3> Normals { get; set; }
		public ulong SubMeshCount => (ulong)SubMeshes.LongCount();
		public List<IMultiResolutionSubMesh> SubMeshes { get; set; }
		public ulong MaterialCount => (ulong)Materials.LongCount();
		public List<IMaterial> Materials { get; set; }
		public bool AlphaTest { get; set; }
		public AxisAlignedBoundingBox BoundingBox { get; set; }
		public IOrientedBoundingBox OrientedBoundingBox { get; set; }

		public IMultiResolutionSubMesh? GetSubMesh(ulong i)
		{
			return SubMeshes[(int)i];
		}

		public IMaterial? GetMaterial(ulong i)
		{
			return Materials[(int)i];
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

		public List<Vector3> Positions =>
			Native.ZkMultiResolutionMesh_getPositions(_handle, out var count).MarshalAsList<Vector3>(count);

		public List<Vector3> Normals =>
			Native.ZkMultiResolutionMesh_getNormals(_handle, out var count).MarshalAsList<Vector3>(count);

		public ulong SubMeshCount => Native.ZkMultiResolutionMesh_getSubMeshCount(_handle);

		public List<IMultiResolutionSubMesh> SubMeshes
		{
			get
			{
				var meshes = new List<IMultiResolutionSubMesh>();

				Native.ZkMultiResolutionMesh_enumerateSubMeshes(_handle, (_, mesh) =>
				{
					meshes.Add(new MultiResolutionSubMesh(mesh));
					return false;
				}, UIntPtr.Zero);

				return meshes;
			}
		}

		public ulong MaterialCount => Native.ZkMultiResolutionMesh_getMaterialCount(_handle);

		public List<IMaterial> Materials
		{
			get
			{
				var materials = new List<IMaterial>();

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

		public IMultiResolutionSubMesh? GetSubMesh(ulong i)
		{
			var mesh = Native.ZkMultiResolutionMesh_getSubMesh(_handle, i);
			return mesh == UIntPtr.Zero ? null : new MultiResolutionSubMesh(mesh);
		}

		public IMaterial? GetMaterial(ulong i)
		{
			var mesh = Native.ZkMultiResolutionMesh_getMaterial(_handle, i);
			return mesh == UIntPtr.Zero ? null : new Material(mesh);
		}

		~MultiResolutionMesh()
		{
			if (_delete) Native.ZkMultiResolutionMesh_del(_handle);
		}
	}
}