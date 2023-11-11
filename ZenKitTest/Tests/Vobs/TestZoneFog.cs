using System.Drawing;
using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs
{
	public class TestZoneFog
	{
		[Test]
		public void TestLoad()
		{
			var vob = new ZoneFog("./Samples/G2/VOb/zCZoneZFog.zen", GameVersion.Gothic2);
			Assert.That(vob.RangeCenter, Is.EqualTo(16000.0f));
			Assert.That(vob.InnerRangePercentage, Is.EqualTo(0.699999988f));
			Assert.That(vob.Color, Is.EqualTo(Color.FromArgb(255, 120, 120, 120)));
			Assert.That(vob.FadeOutSky, Is.False);
			Assert.That(vob.OverrideColor, Is.False);
		}
	}
}