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

	public interface IOrientedBoundingBox : ICacheable<IOrientedBoundingBox>
	{
		public Vector3 Center { get; }
		public Tuple<Vector3, Vector3, Vector3> Axes { get; }
		public Vector3 HalfWidth { get; }
		public List<IOrientedBoundingBox> Children { get; }
		public int ChildCount { get; }

		public IOrientedBoundingBox GetChild(int i);

		public AxisAlignedBoundingBox ToAabb();
	}

	[Serializable]
	public class CachedOrientedBoundingBox : IOrientedBoundingBox
	{
		public Vector3 Center { get; set; }
		public Tuple<Vector3, Vector3, Vector3> Axes { get; set; }
		public Vector3 HalfWidth { get; set; }
		public List<IOrientedBoundingBox> Children { get; set; }

		public int ChildCount => Children.Count;

		public IOrientedBoundingBox Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}

		public IOrientedBoundingBox GetChild(int i)
		{
			return Children[i];
		}

		public AxisAlignedBoundingBox ToAabb()
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	///     The interface to native oriented bounding boxes.
	/// </summary>
	public class OrientedBoundingBox : IOrientedBoundingBox
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
		public int ChildCount => (int)Native.ZkOrientedBoundingBox_getChildCount(_handle);

		public List<IOrientedBoundingBox> Children
		{
			get
			{
				var children = new List<IOrientedBoundingBox>();
				var count = ChildCount;
				for (var i = 0; i < count; ++i) children.Add(GetChild(i));
				return children;
			}
		}

		/// <summary>
		///     Fully loads this native object into a C# serializable object, disassociated
		///     from the underlying native implementation.
		/// </summary>
		/// <returns>This native object in a pure C# representation.</returns>
		public IOrientedBoundingBox Cache()
		{
			return new CachedOrientedBoundingBox
			{
				Center = Center,
				HalfWidth = HalfWidth,
				Axes = Axes,
				Children = Children.ConvertAll(obb => obb.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IOrientedBoundingBox GetChild(int i)
		{
			var handle = Native.ZkOrientedBoundingBox_getChild(_handle, (ulong)i);
			if (handle == UIntPtr.Zero) throw new Exception("Failed to load oriented bounding box child");
			return new OrientedBoundingBox(handle);
		}

		public AxisAlignedBoundingBox ToAabb()
		{
			return Native.ZkOrientedBoundingBox_toAabb(_handle);
		}
	}
}