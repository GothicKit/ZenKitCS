using System;
using System.Numerics;

namespace ZenKit.Vobs
{
	public class Earthquake : VirtualObject
	{
		public Earthquake() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCEarthquake))
		{
		}

		public Earthquake(Read buf, GameVersion version) : base(Native.ZkEarthquake_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load earthquake vob");
		}

		public Earthquake(string path, GameVersion version) : base(Native.ZkEarthquake_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load earthquake vob");
		}

		internal Earthquake(UIntPtr handle) : base(handle)
		{
		}

		public float Radius
		{
			get => Native.ZkEarthquake_getRadius(Handle);
			set => Native.ZkEarthquake_setRadius(Handle, value);
		}

		public TimeSpan Duration
		{
			get => TimeSpan.FromSeconds(Native.ZkEarthquake_getDuration(Handle));
			set => Native.ZkEarthquake_setDuration(Handle, (float)value.TotalSeconds);
		}

		public Vector3 Amplitude
		{
			get => Native.ZkEarthquake_getAmplitude(Handle);
			set => Native.ZkEarthquake_setAmplitude(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkEarthquake_del(Handle);
		}
	}
}