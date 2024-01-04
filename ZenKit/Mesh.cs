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

	public interface ILightMap : ICacheable<ILightMap>
	{
		public ITexture Image { get; }
		public Vector3 Origin { get; }
		public Tuple<Vector3, Vector3> Normals { get; }
	}

	[Serializable]
	public class CachedLightMap : ILightMap
	{
		public ITexture Image { get; set; }
		public Vector3 Origin { get; set; }
		public Tuple<Vector3, Vector3> Normals { get; set; }

		public ILightMap Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class LightMap : ILightMap
	{
		private readonly UIntPtr _handle;

		internal LightMap(UIntPtr handle)
		{
			_handle = handle;
		}

		public ITexture Image => new Texture(Native.ZkLightMap_getImage(_handle));
		public Vector3 Origin => Native.ZkLightMap_getOrigin(_handle);

		public Tuple<Vector3, Vector3> Normals => new Tuple<Vector3, Vector3>(Native.ZkLightMap_getNormal(_handle, 0),
			Native.ZkLightMap_getNormal(_handle, 1));

		public ILightMap Cache()
		{
			return new CachedLightMap
			{
				Image = Image.Cache(),
				Origin = Origin,
				Normals = Normals
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IPolygon : ICacheable<IPolygon>
	{
		public int MaterialIndex { get; }
		public int LightMapIndex { get; }
		public List<int> PositionIndices { get; }
		public List<int> FeatureIndices { get; }
		public bool IsPortal { get; }
		public bool IsOccluder { get; }
		public bool IsSector { get; }
		public bool ShouldRelight { get; }
		public bool IsOutdoor { get; }
		public bool IsGhostOccluder { get; }
		public bool IsDynamicallyLit { get; }
		public bool IsLod { get; }
		public byte NormalAxis { get; }
		public short SectorIndex { get; }
	}

	[Serializable]
	public class CachedPolygon : IPolygon
	{
		public int MaterialIndex { get; set; }
		public int LightMapIndex { get; set; }
		public List<int> PositionIndices { get; set; }
		public List<int> FeatureIndices { get; set; }
		public bool IsPortal { get; set; }
		public bool IsOccluder { get; set; }
		public bool IsSector { get; set; }
		public bool ShouldRelight { get; set; }
		public bool IsOutdoor { get; set; }
		public bool IsGhostOccluder { get; set; }
		public bool IsDynamicallyLit { get; set; }
		public bool IsLod { get; set; }
		public byte NormalAxis { get; set; }
		public short SectorIndex { get; set; }

		public IPolygon Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class Polygon : IPolygon
	{
		private readonly UIntPtr _handle;

		public Polygon(UIntPtr handle)
		{
			_handle = handle;
		}

		public int MaterialIndex => (int)Native.ZkPolygon_getMaterialIndex(_handle);
		public int LightMapIndex => Native.ZkPolygon_getLightMapIndex(_handle);

		public List<int> PositionIndices =>
			Native.ZkPolygon_getPositionIndices(_handle, out var count).MarshalAsList<int>(count);

		public List<int> FeatureIndices =>
			Native.ZkPolygon_getFeatureIndices(_handle, out var count).MarshalAsList<int>(count);

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

		public IPolygon Cache()
		{
			return new CachedPolygon
			{
				MaterialIndex = MaterialIndex,
				LightMapIndex = LightMapIndex,
				PositionIndices = PositionIndices,
				FeatureIndices = FeatureIndices,
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

		public bool IsCached()
		{
			return false;
		}
	}


	public interface IMesh : ICacheable<IMesh>
	{
		public DateTime? SourceDate { get; }
		public string Name { get; }
		public AxisAlignedBoundingBox BoundingBox { get; }
		public IOrientedBoundingBox OrientedBoundingBox { get; }
		public List<IMaterial> Materials { get; }
		public int MaterialCount { get; }
		public List<Vector3> Positions { get; }
		public int PositionCount { get; }
		public List<Vertex> Features { get; }
		public int FeatureCount { get; }
		public List<ILightMap> LightMap { get; }
		public int LightMapCount { get; }
		public List<IPolygon> Polygons { get; }
		public int PolygonCount { get; }

		public IMaterial GetMaterial(int i);
		public Vector3 GetPosition(int i);
		public Vertex GetFeature(int i);

		public ILightMap GetLightMap(int i);

		public IPolygon GetPolygon(int i);
	}


	[Serializable]
	public class CachedMesh : IMesh
	{
		public DateTime? SourceDate { get; set; }
		public string Name { get; set; }
		public AxisAlignedBoundingBox BoundingBox { get; set; }
		public IOrientedBoundingBox OrientedBoundingBox { get; set; }
		public List<IMaterial> Materials { get; set; }
		public int MaterialCount => Materials.Count;
		public List<Vector3> Positions { get; set; }
		public int PositionCount => Positions.Count;
		public List<Vertex> Features { get; set; }
		public int FeatureCount => Features.Count;
		public List<ILightMap> LightMap { get; set; }
		public int LightMapCount => LightMap.Count;
		public List<IPolygon> Polygons { get; set; }
		public int PolygonCount => Polygons.Count;

		public IMesh Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}

		public IMaterial GetMaterial(int i)
		{
			return Materials[i];
		}

		public Vector3 GetPosition(int i)
		{
			return Positions[i];
		}

		public Vertex GetFeature(int i)
		{
			return Features[i];
		}

		public ILightMap GetLightMap(int i)
		{
			return LightMap[i];
		}

		public IPolygon GetPolygon(int i)
		{
			return Polygons[i];
		}
	}

	public class Mesh : IMesh
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

		public DateTime? SourceDate => Native.ZkMesh_getSourceDate(_handle).AsDateTime();

		public string Name => Native.ZkMesh_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load mesh name");

		public AxisAlignedBoundingBox BoundingBox => Native.ZkMesh_getBoundingBox(_handle);

		public IOrientedBoundingBox OrientedBoundingBox =>
			new OrientedBoundingBox(Native.ZkMesh_getOrientedBoundingBox(_handle));

		public int MaterialCount => (int)Native.ZkMesh_getMaterialCount(_handle);

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

		public int PositionCount => (int)Native.ZkMesh_getPositionCount(_handle);

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

		public int FeatureCount => (int)Native.ZkMesh_getVertexCount(_handle);

		public List<Vertex> Features
		{
			get
			{
				var features = new List<Vertex>();
				var count = FeatureCount;
				for (var i = 0;i < count; ++i) features.Add(GetFeature(i));
				return features;
			}
		}

		public int LightMapCount => (int)Native.ZkMesh_getLightMapCount(_handle);

		public List<ILightMap> LightMap
		{
			get
			{
				var lightMaps = new List<ILightMap>();
				var count = LightMapCount;
				for (var i = 0;i < count; ++i) lightMaps.Add(GetLightMap(i));
				return lightMaps;
			}
		}

		public int PolygonCount => (int)Native.ZkMesh_getPolygonCount(_handle);

		public List<IPolygon> Polygons
		{
			get
			{
				var polygons = new List<IPolygon>();
				var count = PolygonCount;
				for (var i = 0;i < count; ++i) polygons.Add(GetPolygon(i));
				return polygons;
			}
		}

		public IMesh Cache()
		{
			return new CachedMesh
			{
				SourceDate = SourceDate,
				Name = Name,
				BoundingBox = BoundingBox,
				OrientedBoundingBox = OrientedBoundingBox.Cache(),
				Materials = Materials.ConvertAll(mat => mat.Cache()),
				Positions = Positions,
				Features = Features,
				LightMap = LightMap.ConvertAll(lm => lm.Cache()),
				Polygons = Polygons.ConvertAll(polygon => polygon.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IMaterial GetMaterial(int i)
		{
			return new Material(Native.ZkMesh_getMaterial(_handle, (ulong)i));
		}

		public Vector3 GetPosition(int i)
		{
			return Native.ZkMesh_getPosition(_handle, (ulong)i);
		}

		public Vertex GetFeature(int i)
		{
			return Native.ZkMesh_getVertex(_handle, (ulong)i);
		}

		public ILightMap GetLightMap(int i)
		{
			return new LightMap(Native.ZkMesh_getLightMap(_handle, (ulong)i));
		}

		public IPolygon GetPolygon(int i)
		{
			return new Polygon(Native.ZkMesh_getPolygon(_handle, (ulong)i));
		}

		~Mesh()
		{
			if (_delete) Native.ZkMesh_del(_handle);
		}
	}
}