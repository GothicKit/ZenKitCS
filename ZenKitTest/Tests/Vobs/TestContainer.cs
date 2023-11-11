using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs
{
	public class TestContainer
	{
		[Test]
		public void TestLoad()
		{
			var vob = new Container("./Samples/G2/VOb/oCMobContainer.zen", GameVersion.Gothic2);
			Assert.That(vob.IsLocked, Is.False);
			Assert.That(vob.Key, Is.EqualTo(""));
			Assert.That(vob.PickString, Is.EqualTo(""));
			Assert.That(vob.Contents, Is.EqualTo("ItMi_Gold:35"));
		}
	}
}
