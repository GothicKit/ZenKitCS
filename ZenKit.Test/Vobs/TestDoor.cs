using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestDoor
	{
		[Test]
		public void TestLoad()
		{
			var vob = new Door("./Samples/G2/VOb/oCMobDoor.zen", GameVersion.Gothic2);
			Assert.That(vob.IsLocked, Is.False);
			Assert.That(vob.Key, Is.EqualTo(""));
			Assert.That(vob.PickString, Is.EqualTo(""));
		}
	}
}