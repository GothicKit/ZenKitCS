using System.Numerics;

namespace ZenKit.Vobs;

public class Earthquake : VirtualObject
{
	public Earthquake(Read buf, GameVersion version) : base(Native.ZkEarthquake_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load earthquake vob");
	}

	public Earthquake(string path, GameVersion version) : base(Native.ZkEarthquake_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load earthquake vob");
	}

	internal Earthquake(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public float Radius => Native.ZkEarthquake_getRadius(Handle);
	public TimeSpan Duration => TimeSpan.FromSeconds(Native.ZkEarthquake_getDuration(Handle));
	public Vector3 Amplitude => Native.ZkEarthquake_getAmplitude(Handle);

	protected override void Delete()
	{
		Native.ZkEarthquake_del(Handle);
	}
}