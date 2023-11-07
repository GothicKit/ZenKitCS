using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs;

public class TestAnimate
{
	[Test]
	public void TestLoad()
	{
		var vob = new Animate("./Samples/G2/VOb/zCVobAnimate.zen", GameVersion.Gothic2);
		Assert.That(vob.StartOn, Is.True);
	}
}