using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestTriggerScript
	{
		[Test]
		public void TestLoad()
		{
			var vob = new TriggerScript("./Samples/G2/VOb/oCTriggerScript.zen", GameVersion.Gothic2);
			Assert.That(vob.Function, Is.EqualTo("EVT_CAVALORNSGOBBOS_FUNC"));
		}
	}
}