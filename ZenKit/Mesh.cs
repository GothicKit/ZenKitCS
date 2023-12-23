using System;
using System.Collections.Generic;
using System.Linq;
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
		public uint MaterialIndex { get; }
		public int LightMapIndex { get; }
		public List<uint> PositionIndices { get; }
		public List<uint> FeatureIndices { get; }
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
		public uint MaterialIndex { get; set; }
		public int LightMapIndex { get; set; }
		public List<uint> PositionIndices { get; set; }
		public List<uint> FeatureIndices { get; set; }
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

		public uint MaterialIndex => Native.ZkPolygon_getMaterialIndex(_handle);
		public int LightMapIndex => Native.ZkPolygon_getLightMapIndex(_handle);

		public List<uint> PositionIndices =>
			Native.ZkPolygon_getPositionIndices(_handle, out var count).MarshalAsList<uint>(count);

		public List<uint> FeatureIndices =>
			Native.ZkPolygon_getFeatureIndices(_handle, out var count).MarshalAsList<uint>(count);

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
		public ulong MaterialCount { get; }
		public List<Vector3> Positions { get; }
		public List<Vertex> Features { get; }
		public List<ILightMap> LightMap { get; }
		public ulong LightMapCount { get; }
		public List<IPolygon> Polygons { get; }
		public ulong PolygonCount { get; }

		public IMaterial GetMaterial(ulong i);

		public ILightMap GetLightMap(ulong i);

		public IPolygon GetPolygon(ulong i);
	}


	[Serializable]
	public class CachedMesh : IMesh
	{
		public DateTime? SourceDate { get; set; }
		public string Name { get; set; }
		public AxisAlignedBoundingBox BoundingBox { get; set; }
		public IOrientedBoundingBox OrientedBoundingBox { get; set; }
		public List<IMaterial> Materials { get; set; }
		public ulong MaterialCount => (ulong)Materials.LongCount();
		public List<Vector3> Positions { get; set; }
		public List<Vertex> Features { get; set; }
		public List<ILightMap> LightMap { get; set; }
		public ulong LightMapCount => (ulong)LightMap.LongCount();
		public List<IPolygon> Polygons { get; set; }
		public ulong PolygonCount => (ulong)Polygons.LongCount();

		public IMesh Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}

		public IMaterial GetMaterial(ulong i)
		{
			return Materials[(int)i];
		}

		public ILightMap GetLightMap(ulong i)
		{
			return LightMap[(int)i];
		}

		public IPolygon GetPolygon(ulong i)
		{
			return Polygons[(int)i];
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

		public ulong MaterialCount => Native.ZkMesh_getMaterialCount(_handle);

		public List<IMaterial> Materials
		{
			get
			{
				var materials = new List<IMaterial>();

				Native.ZkMesh_enumerateMaterials(_handle, (_, material) =>
				{
					materials.Add(new Material(material));
					return false;
				}, UIntPtr.Zero);

				return materials;
			}
		}

		public List<Vector3> Positions =>
			Native.ZkMesh_getPositions(_handle, out var count).MarshalAsList<Vector3>(count);

		public List<Vertex> Features => Native.ZkMesh_getVertices(_handle, out var count).MarshalAsList<Vertex>(count);

		public ulong LightMapCount => Native.ZkMesh_getLightMapCount(_handle);

		public List<ILightMap> LightMap
		{
			get
			{
				var lightMaps = new List<ILightMap>();

				Native.ZkMesh_enumerateLightMaps(_handle, (_, lm) =>
				{
					lightMaps.Add(new LightMap(lm));
					return false;
				}, UIntPtr.Zero);

				return lightMaps;
			}
		}

		public ulong PolygonCount => Native.ZkMesh_getPolygonCount(_handle);

		public List<IPolygon> Polygons
		{
			get
			{
				var polygons = new List<IPolygon>();

				Native.ZkMesh_enumeratePolygons(_handle, (_, poly) =>
				{
					polygons.Add(new Polygon(poly));
					return false;
				}, UIntPtr.Zero);

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

		public IMaterial GetMaterial(ulong i)
		{
			return new Material(Native.ZkMesh_getMaterial(_handle, i));
		}

		public ILightMap GetLightMap(ulong i)
		{
			return new LightMap(Native.ZkMesh_getLightMap(_handle, i));
		}

		public IPolygon GetPolygon(ulong i)
		{
			return new Polygon(Native.ZkMesh_getPolygon(_handle, i));
		}

		~Mesh()
		{
			if (_delete) Native.ZkMesh_del(_handle);
		}
	}
}