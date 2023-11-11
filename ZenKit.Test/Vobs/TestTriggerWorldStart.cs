using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
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
}