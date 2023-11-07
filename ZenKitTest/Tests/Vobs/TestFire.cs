using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs;

public class TestFire
{
	[Test]
	public void TestLoad()
	{
		var vob = new Fire("./Samples/G2/VOb/oCMobFire.zen", GameVersion.Gothic2);
		Assert.That(vob.Slot, Is.EqualTo("BIP01 FIRE"));
		Assert.That(vob.VobTree, Is.EqualTo("FIRETREE_LAMP.ZEN"));
	}
}