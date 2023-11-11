using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 24)]
	public struct Vertex
	{
		public Vector2 Texture;
		public uint Light;
		public Vector3 Normal;
	}

	namespace Materialized
	{
		[Serializable]
		public struct LightMap
		{
			public Texture Image;
			public Vector3 Origin;
			public Tuple<Vector3, Vector3> Normals;
		}

		[Serializable]
		public struct Polygon
		{
			public uint MaterialIndex;
			public int LightMapIndex;
			public uint[] PositionIndices;
			public uint[] PolygonIndices;
			public bool IsPortal;
			public bool IsOccluder;
			public bool IsSector;
			public bool ShouldRelight;
			public bool IsOutdoor;
			public bool IsGhostOccluder;
			public bool IsDynamicallyLit;
			public bool IsLod;
			public byte NormalAxis;
			public short SectorIndex;
		}

		[Serializable]
		public struct Mesh
		{
			public DateTime SourceDate;
			public string Name;
			public AxisAlignedBoundingBox BoundingBox;
			public OrientedBoundingBox OrientedBoundingBox;
			public List<Material> Materials;
			public Vector3[] Positions;
			public Vertex[] Vertices;
			public List<LightMap> LightMap;
			public List<Polygon> Polygons;
		}
	}

	public class LightMap : IMaterializing<Materialized.LightMap>
	{
		private readonly UIntPtr _handle;

		internal LightMap(UIntPtr handle)
		{
			_handle = handle;
		}

		public Texture Image => new Texture(Native.ZkLightMap_getImage(_handle));
		public Vector3 Origin => Native.ZkLightMap_getOrigin(_handle);

		public Tuple<Vector3, Vector3> Normals => new Tuple<Vector3, Vector3>(Native.ZkLightMap_getNormal(_handle, 0),
			Native.ZkLightMap_getNormal(_handle, 1));

		public Materialized.LightMap Materialize()
		{
			return new Materialized.LightMap
			{
				Image = Image.Materialize(),
				Origin = Origin,
				Normals = Normals
			};
		}
	}

	public class Polygon : IMaterializing<Materialized.Polygon>
	{
		private readonly UIntPtr _handle;

		public Polygon(UIntPtr handle)
		{
			_handle = handle;
		}

		public uint MaterialIndex => Native.ZkPolygon_getMaterialIndex(_handle);
		public int LightMapIndex => Native.ZkPolygon_getLightMapIndex(_handle);

		public uint[] PositionIndices =>
			Native.ZkPolygon_getPositionIndices(_handle, out var count).MarshalAsArray<uint>(count);

		public uint[] PolygonIndices =>
			Native.ZkPolygon_getPolygonIndices(_handle, out var count).MarshalAsArray<uint>(count);

		public bool IsPortal => Native.ZkPolygon_getIsPortal(_handle);
		public bool IsOccluder => Native.ZkPolygon_getIsOccluder(_handle);
		public bool IsSector => Native.ZkPolygon_getIsSector(_handle);
		public bool ShouldRelight => Native.ZkPolygon_getShouldRelight(_handle);
		public bool IsOutdoor => Native.ZkPolygon_getIsOutdoor(_handle);
		public bool IsGhostOccluder => Native.ZkPolygon_getIsGhostOccluder(_handle);
		public bool IsDynamicallyLit => Native.ZkPolygon_getIsDynamicallyLit(_handle);
		public bool IsLod => Native.ZkPolygon_getIsLod(_handle);
		public byte NormalAxis => Native.ZkPolygon_getNormalAxis(_handle);
		public short SectorIndex => Native.ZkPolygon_getSectorIndex(_handle);

		public Materialized.Polygon Materialize()
		{
			return new Materialized.Polygon
			{
				MaterialIndex = MaterialIndex,
				LightMapIndex = LightMapIndex,
				PositionIndices = PositionIndices,
				PolygonIndices = PolygonIndices,
				IsPortal = IsPortal,
				IsOccluder = IsOccluder,
				IsSector = IsSector,
				ShouldRelight = ShouldRelight,
				IsOutdoor = IsOutdoor,
				IsGhostOccluder = IsGhostOccluder,
				IsDynamicallyLit = IsDynamicallyLit,
				IsLod = IsLod,
				NormalAxis = NormalAxis,
				SectorIndex = SectorIndex
			};
		}
	}

	public class Mesh : IMaterializing<Materialized.Mesh>
	{
		private readonly bool _delete = true;
		private readonly UIntPtr _handle;


		public Mesh(string path)
		{
			_handle = Native.ZkMesh_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load mesh");
		}

		public Mesh(Read buf)
		{
			_handle = Native.ZkMesh_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load mesh");
		}

		public Mesh(Vfs vfs, string name)
		{
			_handle = Native.ZkMesh_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load mesh");
		}

		internal Mesh(UIntPtr handle)
		{
			_handle = handle;
			_delete = false;
		}

		public DateTime SourceDate => Native.ZkMesh_getSourceDate(_handle).AsDateTime();

		public string Name => Native.ZkMesh_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load mesh name");

		public AxisAlignedBoundingBox BoundingBox => Native.ZkMesh_getBoundingBox(_handle);

		public OrientedBoundingBox OrientedBoundingBox =>
			new OrientedBoundingBox(Native.ZkMesh_getOrientedBoundingBox(_handle));

		public ulong MaterialCount => Native.ZkMesh_getMaterialCount(_handle);

		public List<Material> Materials
		{
			get
			{
				var materials = new List<Material>();

				Native.ZkMesh_enumerateMaterials(_handle, (_, material) =>
				{
					materials.Add(new Material(material));
					return false;
				}, UIntPtr.Zero);

				return materials;
			}
		}

		public Vector3[] Positions => Native.ZkMesh_getPositions(_handle, out var count).MarshalAsArray<Vector3>(count);
		public Vertex[] Vertices => Native.ZkMesh_getVertices(_handle, out var count).MarshalAsArray<Vertex>(count);

		public ulong LightMapCount => Native.ZkMesh_getLightMapCount(_handle);

		public List<LightMap> LightMap
		{
			get
			{
				var lightMaps = new List<LightMap>();

				Native.ZkMesh_enumerateLightMaps(_handle, (_, lm) =>
				{
					lightMaps.Add(new LightMap(lm));
					return false;
				}, UIntPtr.Zero);

				return lightMaps;
			}
		}

		public ulong PolygonCount => Native.ZkMesh_getPolygonCount(_handle);

		public List<Polygon> Polygons
		{
			get
			{
				var polygons = new List<Polygon>();

				Native.ZkMesh_enumeratePolygons(_handle, (_, poly) =>
				{
					polygons.Add(new Polygon(poly));
					return false;
				}, UIntPtr.Zero);

				return polygons;
			}
		}

		public Materialized.Mesh Materialize()
		{
			return new Materialized.Mesh
			{
				SourceDate = SourceDate,
				Name = Name,
				BoundingBox = BoundingBox,
				OrientedBoundingBox = OrientedBoundingBox.Materialize(),
				Materials = Materials.ConvertAll(mat => mat.Materialize()),
				Positions = Positions,
				Vertices = Vertices,
				LightMap = LightMap.ConvertAll(lm => lm.Materialize()),
				Polygons = Polygons.ConvertAll(polygon => polygon.Materialize())
			};
		}

		~Mesh()
		{
			if (_delete) Native.ZkMesh_del(_handle);
		}

		public Material GetMaterial(ulong i)
		{
			return new Material(Native.ZkMesh_getMaterial(_handle, i));
		}

		public LightMap GetLightMap(ulong i)
		{
			return new LightMap(Native.ZkMesh_getLightMap(_handle, i));
		}

		public Polygon GetPolygon(ulong i)
		{
			return new Polygon(Native.ZkMesh_getPolygon(_handle, i));
		}
	}
}