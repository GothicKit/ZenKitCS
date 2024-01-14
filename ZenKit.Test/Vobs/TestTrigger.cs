using System;
using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestTrigger
{
	[Test]
	public void TestLoad()
	{
		var vob = new Trigger("./Samples/G2/VOb/zCTrigger.zen", GameVersion.Gothic2);
		Assert.That(vob.Target, Is.EqualTo(""));

		Assert.That(vob.StartEnabled, Is.True);
		Assert.That(vob.SendUntrigger, Is.False);
		Assert.That(vob.ReactToOnTrigger, Is.False);
		Assert.That(vob.ReactToOnTouch, Is.False);
		Assert.That(vob.ReactToOnDamage, Is.False);
		Assert.That(vob.RespondToObject, Is.False);
		Assert.That(vob.RespondToPC, Is.False);
		Assert.That(vob.RespondToNPC, Is.False);

		Assert.That(vob.VobTarget, Is.EqualTo(""));
		Assert.That(vob.MaxActivationCount, Is.EqualTo(-1));
		Assert.That(vob.RetriggerDelay.TotalSeconds, Is.EqualTo(0.0f));
		Assert.That(vob.DamageThreshold, Is.EqualTo(0.0f));
		Assert.That(vob.FireDelay.TotalSeconds, Is.EqualTo(0.0f));
	}

	[Test]
	public void TestSetters()
	{
		var vob = new Trigger("./Samples/G2/VOb/zCTrigger.zen", GameVersion.Gothic2);
		vob.Target = "";

		vob.StartEnabled = true;
		vob.SendUntrigger = true;
		vob.ReactToOnTrigger = false;
		vob.ReactToOnTouch = false;
		vob.ReactToOnDamage = false;
		vob.RespondToObject = false;
		vob.RespondToPC = false;
		vob.RespondToNPC = false;

		vob.VobTarget = "";
		vob.MaxActivationCount = -1;
		vob.RetriggerDelay = TimeSpan.FromSeconds(0.0f);
		vob.DamageThreshold = 0.0f;
		vob.FireDelay = TimeSpan.FromSeconds(0.0f);
	}
}