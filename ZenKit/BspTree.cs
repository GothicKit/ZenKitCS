using System;
using System.Collections.Generic;
using System.Linq;
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
		public uint PolygonIndex;
		public uint PolygonCount;
		public int FrontIndex;
		public int BackIndex;
		public int ParentIndex;
	}

	public interface IBspSector : ICacheable<IBspSector>
	{
		public string Name { get; }
		public uint[] NodeIndices { get; }
		public uint[] PortalPolygonIndices { get; }
	}

	[Serializable]
	public class CachedBspSector : IBspSector
	{
		public string Name { get; set; }
		public uint[] NodeIndices { get; set; }
		public uint[] PortalPolygonIndices { get; set; }

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

		public uint[] NodeIndices =>
			Native.ZkBspSector_getNodeIndices(_handle, out var count).MarshalAsArray<uint>(count);

		public uint[] PortalPolygonIndices =>
			Native.ZkBspSector_getPortalPolygonIndices(_handle, out var count).MarshalAsArray<uint>(count);

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
		public uint[] PolygonIndices { get; }
		public uint[] LeafPolygonIndices { get; }
		public uint[] PortalPolygonIndices { get; }
		public Vector3[] LightPoints { get; }
		public ulong[] LeafNodeIndices { get; }
		public BspNode[] Nodes { get; }
		public List<IBspSector> Sectors { get; }

		public ulong SectorCount { get; }

		public IBspSector GetSector(ulong i)
		{
			return Sectors[(int)i];
		}
	}

	[Serializable]
	public class CachedBspTree : IBspTree
	{
		public BspTreeType Type { get; set; }
		public uint[] PolygonIndices { get; set; }
		public uint[] LeafPolygonIndices { get; set; }
		public uint[] PortalPolygonIndices { get; set; }
		public Vector3[] LightPoints { get; set; }
		public ulong[] LeafNodeIndices { get; set; }
		public BspNode[] Nodes { get; set; }
		public List<IBspSector> Sectors { get; set; }
		public ulong SectorCount => (ulong)Sectors.LongCount();

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

		public uint[] PolygonIndices =>
			Native.ZkBspTree_getPolygonIndices(_handle, out var count).MarshalAsArray<uint>(count);

		public uint[] LeafPolygonIndices =>
			Native.ZkBspTree_getLeafPolygonIndices(_handle, out var count).MarshalAsArray<uint>(count);

		public uint[] PortalPolygonIndices =>
			Native.ZkBspTree_getPortalPolygonIndices(_handle, out var count).MarshalAsArray<uint>(count);

		public Vector3[] LightPoints =>
			Native.ZkBspTree_getLightPoints(_handle, out var count).MarshalAsArray<Vector3>(count);

		public ulong[] LeafNodeIndices =>
			Native.ZkBspTree_getLeafNodeIndices(_handle, out var count).MarshalAsArray<ulong>(count);

		public BspNode[] Nodes => Native.ZkBspTree_getNodes(_handle, out var count).MarshalAsArray<BspNode>(count);
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

		public BspSector GetSector(ulong i)
		{
			return new BspSector(Native.ZkBspTree_getSector(_handle, i));
		}
	}
}