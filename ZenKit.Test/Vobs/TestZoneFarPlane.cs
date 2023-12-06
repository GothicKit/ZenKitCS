using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestZoneFarPlane
{
	[Test]
	public void TestLoad()
	{
		var vob = new ZoneFarPlane("./Samples/G2/VOb/zCZoneVobFarPlane.zen", GameVersion.Gothic2);
		Assert.That(vob.VobFarPlaneZ, Is.EqualTo(6500.0f));
		Assert.That(vob.InnerRangePercentage, Is.EqualTo(0.699999988f));
	}

	[Test]
	public void TestSetters()
	{
		var vob = new ZoneFarPlane("./Samples/G2/VOb/zCZoneVobFarPlane.zen", GameVersion.Gothic2);
		vob.VobFarPlaneZ = 6500.0f;
		vob.InnerRangePercentage = 0.699999988f;
	}
}