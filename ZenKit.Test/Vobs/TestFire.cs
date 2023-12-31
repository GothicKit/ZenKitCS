using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestFire
{
	[Test]
	public void TestLoad()
	{
		var vob = new Fire("./Samples/G2/VOb/oCMobFire.zen", GameVersion.Gothic2);
		Assert.That(vob.Slot, Is.EqualTo("BIP01 FIRE"));
		Assert.That(vob.VobTree, Is.EqualTo("FIRETREE_LAMP.ZEN"));
	}

	[Test]
	public void TestSetters()
	{
		var vob = new Fire("./Samples/G2/VOb/oCMobFire.zen", GameVersion.Gothic2);
		vob.Slot = "BIP01 FIRE";
		vob.VobTree = "FIRETREE_LAMP.ZEN";
	}
}