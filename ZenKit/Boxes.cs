using System.Numerics;
using System.Runtime.InteropServices;

namespace ZenKit;

[StructLayout(LayoutKind.Sequential)]
public struct AxisAlignedBoundingBox
{
	public Vector3 Min;
	public Vector3 Max;
}