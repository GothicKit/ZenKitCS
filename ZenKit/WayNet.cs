using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 8)]
	public struct WayEdge
	{
		public uint A, B;
	}

	namespace Materialized
	{
		[Serializable]
		public struct WayPoint
		{
			public string Name;
			public int WaterDepth;
			public bool UnderWater;
			public Vector3 Position;
			public Vector3 Direction;
			public bool FreePoint;
		}

		[Serializable]
		public struct WayNet
		{
			public WayEdge[] Edges;
			public List<WayPoint> Points;
		}
	}

	public class WayPoint : IMaterializing<Materialized.WayPoint>
	{
		private readonly UIntPtr _handle;

		internal WayPoint(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkWayPoint_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load way point name");

		public int WaterDepth => Native.ZkWayPoint_getWaterDepth(_handle);
		public bool UnderWater => Native.ZkWayPoint_getUnderWater(_handle);
		public Vector3 Position => Native.ZkWayPoint_getPosition(_handle);
		public Vector3 Direction => Native.ZkWayPoint_getDirection(_handle);
		public bool FreePoint => Native.ZkWayPoint_getFreePoint(_handle);

		public Materialized.WayPoint Materialize()
		{
			return new Materialized.WayPoint
			{
				Name = Name,
				WaterDepth = WaterDepth,
				UnderWater = UnderWater,
				Position = Position,
				Direction = Direction,
				FreePoint = FreePoint
			};
		}
	}

	public class WayNet : IMaterializing<Materialized.WayNet>
	{
		private readonly UIntPtr _handle;

		internal WayNet(UIntPtr handle)
		{
			_handle = handle;
		}

		public WayEdge[] Edges => Native.ZkWayNet_getEdges(_handle, out var count).MarshalAsArray<WayEdge>(count);
		public ulong PointCount => Native.ZkWayNet_getPointCount(_handle);

		public List<WayPoint> Points
		{
			get
			{
				var points = new List<WayPoint>();

				Native.ZkWayNet_enumeratePoints(_handle, (_, point) =>
				{
					points.Add(new WayPoint(point));
					return false;
				}, UIntPtr.Zero);

				return points;
			}
		}

		public Materialized.WayNet Materialize()
		{
			return new Materialized.WayNet
			{
				Points = Points.ConvertAll(point => point.Materialize()),
				Edges = Edges
			};
		}

		public WayPoint GetPoint(ulong i)
		{
			return new WayPoint(Native.ZkWayNet_getPoint(_handle, i));
		}
	}
}