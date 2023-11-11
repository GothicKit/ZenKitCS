using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs
{
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
	}
}