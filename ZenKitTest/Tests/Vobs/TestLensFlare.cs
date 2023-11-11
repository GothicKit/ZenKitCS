using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs
{
	public class TestLensFlare
	{
		[Test]
		public void TestLoad()
		{
			var vob = new LensFlare("./Samples/G1/VOb/zCVobLensFlare.zen", GameVersion.Gothic1);
			Assert.That(vob.Effect, Is.EqualTo("TORCHFX01"));
		}
	}
}