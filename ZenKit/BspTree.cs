using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace ZenKit
{
	public enum BspTreeType
	{
		Indoor = 0,
		Outdoor = 1
	}

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

	public class BspSector
	{
		private readonly UIntPtr _handle;

		internal BspSector(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkBspSector_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load bsp sector name");

		public uint[] NodeIndices => Native.ZkBspSector_getNodeIndices(_handle, out var count).MarshalAsArray<uint>(count);

		public uint[] PortalPolygonIndices =>
			Native.ZkBspSector_getPortalPolygonIndices(_handle, out var count).MarshalAsArray<uint>(count);
	}

	public class BspTree
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

		public List<BspSector> Sectors
		{
			get
			{
				var sectors = new List<BspSector>();

				Native.ZkBspTree_enumerateSectors(_handle, (_, sector) =>
				{
					sectors.Add(new BspSector(sector));
					return false;
				}, UIntPtr.Zero);

				return sectors;
			}
		}

		public BspSector GetSector(ulong i)
		{
			return new BspSector(Native.ZkBspTree_getSector(_handle, i));
		}
	}
}