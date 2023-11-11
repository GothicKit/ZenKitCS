using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestZoneFarPlane
	{
		[Test]
		public void TestLoad()
		{
			var vob = new ZoneFarPlane("./Samples/G2/VOb/zCZoneVobFarPlane.zen", GameVersion.Gothic2);
			Assert.That(vob.VobFarPlaneZ, Is.EqualTo(6500.0f));
			Assert.That(vob.InnerRangePercentage, Is.EqualTo(0.699999988f));
		}
	}
}