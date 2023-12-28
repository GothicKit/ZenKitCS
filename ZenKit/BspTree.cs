using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
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
		public int PolygonIndex;
		public int PolygonCount;
		public int FrontIndex;
		public int BackIndex;
		public int ParentIndex;
	}

	public interface IBspSector : ICacheable<IBspSector>
	{
		public string Name { get; }
		public List<int> NodeIndices { get; }
		public List<int> PortalPolygonIndices { get; }
	}

	[Serializable]
	public class CachedBspSector : IBspSector
	{
		public string Name { get; set; }
		public List<int> NodeIndices { get; set; }
		public List<int> PortalPolygonIndices { get; set; }

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

		public List<int> NodeIndices =>
			Native.ZkBspSector_getNodeIndices(_handle, out var count).MarshalAsList<int>(count);

		public List<int> PortalPolygonIndices =>
			Native.ZkBspSector_getPortalPolygonIndices(_handle, out var count).MarshalAsList<int>(count);

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
		public List<int> PolygonIndices { get; }
		public List<int> LeafPolygonIndices { get; }
		public List<int> PortalPolygonIndices { get; }
		public int LightPointCount { get; }
		public List<Vector3> LightPoints { get; }
		public List<int> LeafNodeIndices { get; }
		public int NodeCount { get; }
		public List<BspNode> Nodes { get; }
		public List<IBspSector> Sectors { get; }

		public int SectorCount { get; }

		public IBspSector GetSector(int i);

		public Vector3 GetLightPoint(int i);
		public BspNode GetNode(int i);
	}

	[Serializable]
	public class CachedBspTree : IBspTree
	{
		public BspTreeType Type { get; set; }
		public List<int> PolygonIndices { get; set; }
		public List<int> LeafPolygonIndices { get; set; }
		public List<int> PortalPolygonIndices { get; set; }

		public int LightPointCount => LightPoints.Count;

		public List<Vector3> LightPoints { get; set; }
		public List<int> LeafNodeIndices { get; set; }

		public int NodeCount => Nodes.Count;

		public List<BspNode> Nodes { get; set; }
		public List<IBspSector> Sectors { get; set; }
		public int SectorCount => Sectors.Count;

		public IBspSector GetSector(int i)
		{
			return Sectors[i];
		}

		public Vector3 GetLightPoint(int i)
		{
			return LightPoints[i];
		}

		public BspNode GetNode(int i)
		{
			return Nodes[i];
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

		public List<int> PolygonIndices =>
			Native.ZkBspTree_getPolygonIndices(_handle, out var count).MarshalAsList<int>(count);

		public List<int> LeafPolygonIndices =>
			Native.ZkBspTree_getLeafPolygonIndices(_handle, out var count).MarshalAsList<int>(count);

		public List<int> PortalPolygonIndices =>
			Native.ZkBspTree_getPortalPolygonIndices(_handle, out var count).MarshalAsList<int>(count);

		public int LightPointCount => (int)Native.ZkBspTree_getLightPointCount(_handle);

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

		public List<int> LeafNodeIndices =>
			Native.ZkBspTree_getLeafNodeIndices(_handle, out var count).MarshalAsList<ulong>(count)
				.ConvertAll(v => (int)v);

		public int NodeCount => (int)Native.ZkBspTree_getNodeCount(_handle);

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

		public int SectorCount => (int)Native.ZkBspTree_getSectorCount(_handle);

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

		public IBspSector GetSector(int i)
		{
			return new BspSector(Native.ZkBspTree_getSector(_handle, (ulong)i));
		}

		public Vector3 GetLightPoint(int i)
		{
			return Native.ZkBspTree_getLightPoint(_handle, (ulong)i);
		}

		public BspNode GetNode(int i)
		{
			return Native.ZkBspTree_getNode(_handle, (ulong)i);
		}
	}
}