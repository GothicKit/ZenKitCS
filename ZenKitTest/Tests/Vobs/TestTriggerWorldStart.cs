using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs;

public class TestTriggerWorldStart
{
	[Test]
	public void TestLoad()
	{
		var vob = new TriggerWorldStart("./Samples/G2/VOb/zCTriggerWorldStart.zen", GameVersion.Gothic2);
		Assert.That(vob.Target, Is.EqualTo("EVT_TROLL_GRAVE_MOVER_01"));
		Assert.That(vob.FireOnce, Is.True);
	}
}