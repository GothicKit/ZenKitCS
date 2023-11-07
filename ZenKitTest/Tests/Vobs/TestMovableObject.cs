using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs;

public class TestMovableObject
{
	[Test]
	public void TestLoad()
	{
		var vob = new MovableObject("./Samples/G2/VOb/oCMOB.zen", GameVersion.Gothic2);
		Assert.That(vob.FocusName, Is.EqualTo("MOBNAME_GRAVE_18"));
		Assert.That(vob.Hp, Is.EqualTo(10));
		Assert.That(vob.Damage, Is.EqualTo(0));
		Assert.That(vob.Movable, Is.False);
		Assert.That(vob.Takable, Is.False);
		Assert.That(vob.FocusOverride, Is.False);
		Assert.That(vob.Material, Is.EqualTo(SoundMaterialType.Wood));
		Assert.That(vob.VisualDestroyed, Is.EqualTo(""));
		Assert.That(vob.Owner, Is.EqualTo(""));
		Assert.That(vob.OwnerGuild, Is.EqualTo(""));
		Assert.That(vob.Destroyed, Is.False);
	}
}