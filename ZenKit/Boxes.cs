using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct AxisAlignedBoundingBox
	{
		public Vector3 Min;
		public Vector3 Max;
	}

	namespace Materialized
	{
		[Serializable]
		public struct OrientedBoundingBox
		{
			public Vector3 Center;
			public Tuple<Vector3, Vector3, Vector3> Axes;
			public Vector3 HalfWidth;
			public List<OrientedBoundingBox> Children;
		}
	}

	/// <summary>
	///     The interface to native oriented bounding boxes.
	/// </summary>
	public class OrientedBoundingBox : IMaterializing<Materialized.OrientedBoundingBox>
	{
		private readonly UIntPtr _handle;

		internal OrientedBoundingBox(UIntPtr handle)
		{
			_handle = handle;
		}

		public Vector3 Center => Native.ZkOrientedBoundingBox_getCenter(_handle);

		public Tuple<Vector3, Vector3, Vector3> Axes => new Tuple<Vector3, Vector3, Vector3>(
			Native.ZkOrientedBoundingBox_getAxis(_handle, 0),
			Native.ZkOrientedBoundingBox_getAxis(_handle, 1),
			Native.ZkOrientedBoundingBox_getAxis(_handle, 2)
		);

		public Vector3 HalfWidth => Native.ZkOrientedBoundingBox_getHalfWidth(_handle);
		public ulong ChildCount => Native.ZkOrientedBoundingBox_getChildCount(_handle);

		public List<OrientedBoundingBox> Children
		{
			get
			{
				var children = new List<OrientedBoundingBox>();

				Native.ZkOrientedBoundingBox_enumerateChildren(_handle, (_, box) =>
				{
					children.Add(new OrientedBoundingBox(box));
					return false;
				}, UIntPtr.Zero);

				return children;
			}
		}

		/// <summary>
		///     Fully loads this native object into a C# serializable object, disassociated
		///     from the underlying native implementation.
		/// </summary>
		/// <returns>This native object in a pure C# representation.</returns>
		public Materialized.OrientedBoundingBox Materialize()
		{
			return new Materialized.OrientedBoundingBox
			{
				Center = Center,
				HalfWidth = HalfWidth,
				Axes = Axes,
				Children = Children.ConvertAll(obb => obb.Materialize())
			};
		}

		public OrientedBoundingBox GetChild(ulong i)
		{
			var handle = Native.ZkOrientedBoundingBox_getChild(_handle, i);
			if (handle == UIntPtr.Zero) throw new Exception("Failed to load oriented bounding box child");
			return new OrientedBoundingBox(handle);
		}

		public AxisAlignedBoundingBox ToAabb()
		{
			return Native.ZkOrientedBoundingBox_toAabb(_handle);
		}
	}
}