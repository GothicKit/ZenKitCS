using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs
{
	public class TestSound
	{
		[Test]
		public void TestLoadSound()
		{
			var vob = new Sound("./Samples/G2/VOb/zCVobSound.zen", GameVersion.Gothic2);
			Assert.That(vob.Volume, Is.EqualTo(80.0f));
			Assert.That(vob.Mode, Is.EqualTo(SoundMode.Random));
			Assert.That(vob.RandomDelay, Is.EqualTo(30.0f));
			Assert.That(vob.RandomDelayVar, Is.EqualTo(20.0f));
			Assert.That(vob.InitiallyPlaying, Is.True);
			Assert.That(vob.Ambient3d, Is.False);
			Assert.That(vob.Obstruction, Is.False);
			Assert.That(vob.ConeAngle, Is.EqualTo(0.0f));
			Assert.That(vob.VolumeType, Is.EqualTo(SoundTriggerVolumeType.Spherical));
			Assert.That(vob.Radius, Is.EqualTo(3000.0f));
			Assert.That(vob.SoundName, Is.EqualTo("OW_CROW"));
		}
	
		[Test]
		public void TestLoadSoundDaytime()
		{
			var vob = new SoundDaytime("./Samples/G2/VOb/zCVobSoundDaytime.zen", GameVersion.Gothic2);
			Assert.That(vob.StartTime, Is.EqualTo(5));
			Assert.That(vob.EndTime, Is.EqualTo(21));
			Assert.That(vob.SoundNameDaytime, Is.EqualTo("InsectsFrogs_Night"));
		}
	}
}
