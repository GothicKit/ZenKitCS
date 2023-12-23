using System;
using System.Collections.Generic;
using System.Linq;
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

	public interface IWayPoint : ICacheable<IWayPoint>
	{
		public string Name { get; }
		public int WaterDepth { get; }
		public bool UnderWater { get; }
		public Vector3 Position { get; }
		public Vector3 Direction { get; }
		public bool FreePoint { get; }
	}

	[Serializable]
	public class CachedWayPoint : IWayPoint
	{
		public string Name { get; set; }
		public int WaterDepth { get; set; }
		public bool UnderWater { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 Direction { get; set; }
		public bool FreePoint { get; set; }

		public IWayPoint Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class WayPoint : IWayPoint
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

		public IWayPoint Cache()
		{
			return new CachedWayPoint
			{
				Name = Name,
				WaterDepth = WaterDepth,
				UnderWater = UnderWater,
				Position = Position,
				Direction = Direction,
				FreePoint = FreePoint
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IWayNet : ICacheable<IWayNet>
	{
		public List<WayEdge> Edges { get; }
		public List<IWayPoint> Points { get; }
		public ulong PointCount { get; }
		public IWayPoint GetPoint(ulong i);
	}

	[Serializable]
	public class CachedWayNet : IWayNet
	{
		public List<WayEdge> Edges { get; set; }
		public List<IWayPoint> Points { get; set; }
		public ulong PointCount => (ulong)Points.LongCount();

		public IWayPoint GetPoint(ulong i)
		{
			return Points[(int)i];
		}

		public IWayNet Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class WayNet : IWayNet
	{
		private readonly UIntPtr _handle;

		internal WayNet(UIntPtr handle)
		{
			_handle = handle;
		}

		public List<WayEdge> Edges => Native.ZkWayNet_getEdges(_handle, out var count).MarshalAsList<WayEdge>(count);
		public ulong PointCount => Native.ZkWayNet_getPointCount(_handle);

		public List<IWayPoint> Points
		{
			get
			{
				var points = new List<IWayPoint>();

				Native.ZkWayNet_enumeratePoints(_handle, (_, point) =>
				{
					points.Add(new WayPoint(point));
					return false;
				}, UIntPtr.Zero);

				return points;
			}
		}

		public IWayNet Cache()
		{
			return new CachedWayNet
			{
				Points = Points.ConvertAll(point => point.Cache()),
				Edges = Edges
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IWayPoint GetPoint(ulong i)
		{
			return new WayPoint(Native.ZkWayNet_getPoint(_handle, i));
		}
	}
}