using System;
using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestTriggerList
{
	[Test]
	public void TestLoad()
	{
		var vob = new TriggerList("./Samples/G2/VOb/zCTriggerList.zen", GameVersion.Gothic2);
		Assert.That(vob.Mode, Is.EqualTo(TriggerBatchMode.All));
		Assert.That(vob.Targets, Has.Count.EqualTo(2));
		Assert.That(vob.Targets[0].Name, Is.EqualTo("EVT_ADDON_TROLLPORTAL_MASTERTRIGGERLIST_ALPHA_01"));
		Assert.That(vob.Targets[0].Delay.TotalSeconds, Is.EqualTo(0.0f));
		Assert.That(vob.Targets[1].Name, Is.EqualTo("EVT_ADDON_TROLLPORTAL_TRIGGERSCRIPT_01"));
		Assert.That(vob.Targets[1].Delay.TotalSeconds, Is.EqualTo(0.0f));
	}

	[Test]
	public void TestSetters()
	{
		var vob = new TriggerList("./Samples/G2/VOb/zCTriggerList.zen", GameVersion.Gothic2);
		vob.Mode = TriggerBatchMode.All;
		vob.Targets[0].Name = "EVT_ADDON_TROLLPORTAL_MASTERTRIGGERLIST_ALPHA_01";
		vob.Targets[0].Delay = TimeSpan.FromSeconds(0.0f);
	}
}