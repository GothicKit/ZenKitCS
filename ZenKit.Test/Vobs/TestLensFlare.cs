using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestLensFlare
{
	[Test]
	public void TestLoad()
	{
		var vob = new LensFlare("./Samples/G1/VOb/zCVobLensFlare.zen", GameVersion.Gothic1);
		Assert.That(vob.Effect, Is.EqualTo("TORCHFX01"));
	}

	[Test]
	public void TestSetters()
	{
		var vob = new LensFlare("./Samples/G1/VOb/zCVobLensFlare.zen", GameVersion.Gothic1);
		vob.Effect = "TORCHFX01";
	}
}