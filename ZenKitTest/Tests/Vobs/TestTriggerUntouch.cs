using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs
{
	public class TestTriggerUntouch
	{
		[Test]
		public void TestLoad()
		{
			var vob = new TriggerUntouch("./Samples/G2/VOb/zCTriggerUntouch.zen", GameVersion.Gothic2);
			Assert.That(vob.Target, Is.EqualTo("EVT_TROLL_GRAVE_TRIGGERLIST_01"));
		}
	}
}