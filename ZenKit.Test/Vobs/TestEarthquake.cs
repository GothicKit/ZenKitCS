using System;
using System.Numerics;
using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestEarthquake
{
	[Test]
	public void TestLoad()
	{
		var vob = new Earthquake("./Samples/G2/VOb/zCEarthquake.zen", GameVersion.Gothic2);
		Assert.That(vob.Radius, Is.EqualTo(1000.0f));
		Assert.That(vob.Duration.TotalSeconds, Is.EqualTo(5));
		Assert.That(vob.Amplitude.X, Is.EqualTo(2.0));
		Assert.That(vob.Amplitude.Y, Is.EqualTo(10.0));
		Assert.That(vob.Amplitude.Z, Is.EqualTo(2.0));
	}

	[Test]
	public void TestSetters()
	{
		var vob = new Earthquake("./Samples/G2/VOb/zCEarthquake.zen", GameVersion.Gothic2);
		vob.Radius = 1000.0f;
		vob.Duration = TimeSpan.FromSeconds(5);
		vob.Amplitude = new Vector3(2, 10, 2);
	}
}