using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs
{
	public class TestMessageFilter
	{
		[Test]
		public void TestLoad()
		{
			var vob = new MessageFilter("./Samples/G2/VOb/zCMessageFilter.zen", GameVersion.Gothic2);
			Assert.That(vob.Target, Is.EqualTo("EVT_ADDON_TROLLPORTAL_CAMERA_01"));
			Assert.That(vob.OnTrigger, Is.EqualTo(MessageFilterAction.Untrigger));
			Assert.That(vob.OnUntrigger, Is.EqualTo(MessageFilterAction.Untrigger));
		}
	}
}