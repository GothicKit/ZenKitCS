using System;
using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestTrigger
	{
		[Test]
		public void TestLoad()
		{
			var vob = new Trigger("./Samples/G2/VOb/zCTrigger.zen", GameVersion.Gothic2);
			Assert.That(vob.Target, Is.EqualTo(""));
			Assert.That(vob.Flags, Is.EqualTo(3));
			Assert.That(vob.FilterFlags, Is.EqualTo(0));
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
			vob.Flags = 3;
			vob.FilterFlags = 0;
			vob.VobTarget = "";
			vob.MaxActivationCount = -1;
			vob.RetriggerDelay = TimeSpan.FromSeconds(0.0f);
			vob.DamageThreshold = 0.0f;
			vob.FireDelay = TimeSpan.FromSeconds(0.0f);
		}
	}
}