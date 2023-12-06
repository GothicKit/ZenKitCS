using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestCodeMaster
	{

		private static readonly string[] Slaves =
		{
			"EVT_ORNAMENT_SWITCH_BIGFARM_01",
			"EVT_ORNAMENT_SWITCH_BIGFARM_02",
			"EVT_ORNAMENT_SWITCH_BIGFARM_03",
		}; 
	
		[Test]
		public void TestLoad()
		{
			var vob = new CodeMaster("./Samples/G2/VOb/zCCodeMaster.zen", GameVersion.Gothic2);
			Assert.That(vob.Target, Is.EqualTo("EVT_ORNAMENT_TRIGGER_BIGFARM_01"));
			Assert.That(vob.Ordered, Is.False);
			Assert.That(vob.FirstFalseIsFailure, Is.False);
			Assert.That(vob.FailureTarget, Is.EqualTo(""));
			Assert.That(vob.UntriggeredCancels, Is.False);
			Assert.That(vob.Slaves, Is.EqualTo(Slaves));
		}

		[Test]
		public void TestSetters()
		{
			var vob = new CodeMaster("./Samples/G2/VOb/zCCodeMaster.zen", GameVersion.Gothic2);
			vob.Target = "EVT_ORNAMENT_TRIGGER_BIGFARM_01";
			vob.Ordered = false;
			vob.FirstFalseIsFailure = false;
			vob.FailureTarget = "";
			vob.UntriggeredCancels = true;
			
			vob.AddSlave("Test");
			vob.RemoveSlave(0);
		}
	}
}