using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestTriggerChangeLevel
	{
		[Test]
		public void TestLoad()
		{
			var vob = new TriggerChangeLevel("./Samples/G2/VOb/oCTriggerChangeLevel.zen", GameVersion.Gothic2);
			Assert.That(vob.LevelName, Is.EqualTo("ADDON\\ADDONWORLD.ZEN"));
			Assert.That(vob.StartVob, Is.EqualTo("START_ADDON"));
		}
		
		[Test]
		public void TestSetters()
		{
			var vob = new TriggerChangeLevel("./Samples/G2/VOb/oCTriggerChangeLevel.zen", GameVersion.Gothic2);
			vob.LevelName = "ADDON\\ADDONWORLD.ZEN";
			vob.StartVob = "START_ADDON";
		}
	}
}