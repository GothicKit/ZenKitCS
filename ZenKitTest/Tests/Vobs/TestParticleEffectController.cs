using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs
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
	}
}