using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestParticleEffectController
	{
		[Test]
		public void TestLoad()
		{
			var vob = new ParticleEffectController("./Samples/G2/VOb/zCPFXControler.zen", GameVersion.Gothic2);
			Assert.That(vob.EffectName, Is.EqualTo("STARGATE_EDGES.PFX"));
			Assert.That(vob.KillWhenDone, Is.False);
			Assert.That(vob.InitiallyRunning, Is.False);
		}

		[Test]
		public void TestSetters()
		{
			var vob = new ParticleEffectController("./Samples/G2/VOb/zCPFXControler.zen", GameVersion.Gothic2);
			vob.EffectName = "STARGATE_EDGES.PFX";
			vob.KillWhenDone = false;
			vob.InitiallyRunning = false;
		}
	}
}