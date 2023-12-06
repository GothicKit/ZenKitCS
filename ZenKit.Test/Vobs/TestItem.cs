using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestItem
{
	[Test]
	public void TestLoad()
	{
		var vob = new Item("./Samples/G2/VOb/oCItem.zen", GameVersion.Gothic2);
		Assert.That(vob.Instance, Is.EqualTo("ITPL_BLUEPLANT"));
	}

	[Test]
	public void TestSetters()
	{
		var vob = new Item("./Samples/G2/VOb/oCItem.zen", GameVersion.Gothic2);
		vob.Instance = "ITPL_BLUEPLANT";
	}
}