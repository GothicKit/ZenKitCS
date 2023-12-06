using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestAnimate
	{
		[Test]
		public void TestLoad()
		{
			var vob = new Animate("./Samples/G2/VOb/zCVobAnimate.zen", GameVersion.Gothic2);
			Assert.That(vob.StartOn, Is.True);

		}

		[Test]
		public void TestSetters()
		{
			var vob = new Animate("./Samples/G2/VOb/zCVobAnimate.zen", GameVersion.Gothic2);
			vob.StartOn = true;
		}
	}
}