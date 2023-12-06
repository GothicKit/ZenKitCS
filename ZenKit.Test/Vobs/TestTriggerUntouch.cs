using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestTriggerUntouch
{
	[Test]
	public void TestLoad()
	{
		var vob = new TriggerUntouch("./Samples/G2/VOb/zCTriggerUntouch.zen", GameVersion.Gothic2);
		Assert.That(vob.Target, Is.EqualTo("EVT_TROLL_GRAVE_TRIGGERLIST_01"));
	}

	[Test]
	public void TestSetters()
	{
		var vob = new TriggerUntouch("./Samples/G2/VOb/zCTriggerUntouch.zen", GameVersion.Gothic2);
		vob.Target = "EVT_TROLL_GRAVE_TRIGGERLIST_01";
	}
}