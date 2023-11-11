using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs
{
	public class TestItem
	{
		[Test]
		public void TestLoad()
		{
			var vob = new Item("./Samples/G2/VOb/oCItem.zen", GameVersion.Gothic2);
			Assert.That(vob.Instance, Is.EqualTo("ITPL_BLUEPLANT"));
		}
	}
}