using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	public enum BspTreeType
	{
		Indoor = 0,
		Outdoor = 1
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 60)]
	public struct BspNode
	{
		public Vector4 Plane;
		public AxisAlignedBoundingBox BoundingBox;
		public uint PolygonIndex;
		public uint PolygonCount;
		public int FrontIndex;
		public int BackIndex;
		public int ParentIndex;
	}

	public interface IBspSector : ICacheable<IBspSector>
	{
		public string Name { get; }
		public List<uint> NodeIndices { get; }
		public List<uint> PortalPolygonIndices { get; }
	}

	[Serializable]
	public class CachedBspSector : IBspSector
	{
		public string Name { get; set; }
		public List<uint> NodeIndices { get; set; }
		public List<uint> PortalPolygonIndices { get; set; }

		public IBspSector Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class BspSector : IBspSector
	{
		private readonly UIntPtr _handle;

		internal BspSector(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkBspSector_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load bsp sector name");

		public List<uint> NodeIndices =>
			Native.ZkBspSector_getNodeIndices(_handle, out var count).MarshalAsList<uint>(count);

		public List<uint> PortalPolygonIndices =>
			Native.ZkBspSector_getPortalPolygonIndices(_handle, out var count).MarshalAsList<uint>(count);

		/// <summary>
		///     Fully loads this native object into a C# serializable object, disassociated
		///     from the underlying native implementation.
		/// </summary>
		/// <returns>This native object in a pure C# representation.</returns>
		public IBspSector Cache()
		{
			return new CachedBspSector
			{
				Name = Name,
				NodeIndices = NodeIndices,
				PortalPolygonIndices = PortalPolygonIndices
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IBspTree : ICacheable<IBspTree>
	{
		public BspTreeType Type { get; }
		public List<uint> PolygonIndices { get; }
		public List<uint> LeafPolygonIndices { get; }
		public List<uint> PortalPolygonIndices { get; }
		public ulong LightPointCount { get; }
		public List<Vector3> LightPoints { get; }
		public List<ulong> LeafNodeIndices { get; }
		public ulong NodeCount { get; }
		public List<BspNode> Nodes { get; }
		public List<IBspSector> Sectors { get; }

		public ulong SectorCount { get; }

		public IBspSector GetSector(ulong i);

		public Vector3 GetLightPoint(ulong i);
		public BspNode GetNode(ulong i);
	}

	[Serializable]
	public class CachedBspTree : IBspTree
	{
		public BspTreeType Type { get; set; }
		public List<uint> PolygonIndices { get; set; }
		public List<uint> LeafPolygonIndices { get; set; }
		public List<uint> PortalPolygonIndices { get; set; }

		public ulong LightPointCount => (ulong)LightPoints.LongCount();

		public List<Vector3> LightPoints { get; set; }
		public List<ulong> LeafNodeIndices { get; set; }

		public ulong NodeCount => (ulong)Nodes.LongCount();

		public List<BspNode> Nodes { get; set; }
		public List<IBspSector> Sectors { get; set; }
		public ulong SectorCount => (ulong)Sectors.LongCount();

		public IBspSector GetSector(ulong i)
		{
			return Sectors[(int)i];
		}

		public Vector3 GetLightPoint(ulong i)
		{
			return LightPoints[(int)i];
		}

		public BspNode GetNode(ulong i)
		{
			return Nodes[(int)i];
		}

		public IBspTree Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class BspTree : IBspTree
	{
		private readonly UIntPtr _handle;

		internal BspTree(UIntPtr handle)
		{
			_handle = handle;
		}

		public BspTreeType Type => Native.ZkBspTree_getType(_handle);

		public List<uint> PolygonIndices =>
			Native.ZkBspTree_getPolygonIndices(_handle, out var count).MarshalAsList<uint>(count);

		public List<uint> LeafPolygonIndices =>
			Native.ZkBspTree_getLeafPolygonIndices(_handle, out var count).MarshalAsList<uint>(count);

		public List<uint> PortalPolygonIndices =>
			Native.ZkBspTree_getPortalPolygonIndices(_handle, out var count).MarshalAsList<uint>(count);

		public ulong LightPointCount => Native.ZkBspTree_getLightPointCount(_handle);

		public List<Vector3> LightPoints
		{
			get
			{
				var points = new List<Vector3>();

				Native.ZkBspTree_enumerateLightPoints(_handle, (_, v) =>
				{
					points.Add(v);
					return false;
				}, UIntPtr.Zero);

				return points;
			}
		}

		public List<ulong> LeafNodeIndices =>
			Native.ZkBspTree_getLeafNodeIndices(_handle, out var count).MarshalAsList<ulong>(count);

		public ulong NodeCount => Native.ZkBspTree_getNodeCount(_handle);

		public List<BspNode> Nodes
		{
			get
			{
				var nodes = new List<BspNode>();

				Native.ZkBspTree_enumerateNodes(_handle, (_, node) =>
				{
					nodes.Add(Marshal.PtrToStructure<BspNode>(node));
					return false;
				}, UIntPtr.Zero);

				return nodes;
			}
		}

		public ulong SectorCount => Native.ZkBspTree_getSectorCount(_handle);

		public List<IBspSector> Sectors
		{
			get
			{
				var sectors = new List<IBspSector>();

				Native.ZkBspTree_enumerateSectors(_handle, (_, sector) =>
				{
					sectors.Add(new BspSector(sector));
					return false;
				}, UIntPtr.Zero);

				return sectors;
			}
		}

		/// <summary>
		///     Fully loads this native object into a C# serializable object, disassociated
		///     from the underlying native implementation.
		/// </summary>
		/// <returns>This native object in a pure C# representation.</returns>
		public IBspTree Cache()
		{
			return new CachedBspTree
			{
				Type = Type,
				PolygonIndices = PolygonIndices,
				LeafPolygonIndices = LeafPolygonIndices,
				PortalPolygonIndices = PortalPolygonIndices,
				LightPoints = LightPoints,
				LeafNodeIndices = LeafNodeIndices,
				Nodes = Nodes,
				Sectors = Sectors.ConvertAll(sector => sector.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IBspSector GetSector(ulong i)
		{
			return new BspSector(Native.ZkBspTree_getSector(_handle, i));
		}

		public Vector3 GetLightPoint(ulong i)
		{
			return Native.ZkBspTree_getLightPoint(_handle, i);
		}

		public BspNode GetNode(ulong i)
		{
			return Native.ZkBspTree_getNode(_handle, i);
		}
	}
}