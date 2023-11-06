using System.Numerics;
using System.Runtime.InteropServices;

namespace ZenKit;

[StructLayout(LayoutKind.Sequential, Size = 8)]
public struct WayEdge
{
	public uint A, B;
}

public class WayPoint
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
}

public class WayNet
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

	public WayPoint GetPoint(ulong i)
	{
		return new WayPoint(Native.ZkWayNet_getPoint(_handle, i));
	}
}