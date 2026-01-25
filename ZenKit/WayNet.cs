using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{

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

	public interface IWayEdge : ICacheable<IWayEdge>
	{
		public IWayPoint A { get; }
		public IWayPoint B { get; }
	}

	[Serializable]
	public struct CachedWayEdge : IWayEdge
	{
		public IWayPoint A { get; set; }
		public IWayPoint B { get; set; }

		public IWayEdge Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class WayEdge : IWayEdge
	{
		private readonly UIntPtr _handle;

		internal WayEdge(UIntPtr handle)
		{
			_handle = handle;
		}

		public IWayPoint A => new WayPoint(Native.ZkWayEdge_getStartPoint(_handle));
		public IWayPoint B => new WayPoint(Native.ZkWayEdge_getEndPoint(_handle));

		public IWayEdge Cache()
		{
			return new CachedWayEdge
			{
				A = A.Cache(),
				B = B.Cache(),
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}


	public interface IWayNet : ICacheable<IWayNet>
	{
		public List<IWayEdge> Edges { get; }
		public List<IWayPoint> Points { get; }
		public int PointCount { get; }
		public IWayPoint GetPoint(int i);
	}

	[Serializable]
	public class CachedWayNet : IWayNet
	{
		public List<IWayEdge> Edges { get; set; }
		public List<IWayPoint> Points { get; set; }
		public int PointCount => Points.Count;

		public IWayPoint GetPoint(int i)
		{
			return Points[i];
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

		public int PointCount => (int)Native.ZkWayNet_getPointCount(_handle);
		public int EdgeCount => (int)Native.ZkWayNet_getEdgeCount(_handle);

		public List<IWayEdge> Edges
		{
			get
			{
				var edges = new List<IWayEdge>();
				var count = EdgeCount;
				for (var i = 0; i < count; ++i) edges.Add(GetEdge(i));
				return edges;
			}
		}

		public List<IWayPoint> Points
		{
			get
			{
				var points = new List<IWayPoint>();
				var count = PointCount;
				for (var i = 0; i < count; ++i) points.Add(GetPoint(i));
				return points;
			}
		}

		public IWayNet Cache()
		{
			return new CachedWayNet
			{
				Points = Points.ConvertAll(point => point.Cache()),
				Edges = Edges.ConvertAll(edge => edge.Cache()),
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IWayPoint GetPoint(int i)
		{
			return new WayPoint(Native.ZkWayNet_getPoint(_handle, (ulong)i));
		}

		public IWayEdge GetEdge(int i)
		{
			return new WayEdge(Native.ZkWayNet_getEdge(_handle, (ulong)i));
		}
	}
}
