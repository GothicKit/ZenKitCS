using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

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

	[Test]
	public void TestSetters()
	{
		var vob = new Sound("./Samples/G2/VOb/zCVobSound.zen", GameVersion.Gothic2);
		vob.Volume = 80.0f;
		vob.Mode = SoundMode.Random;
		vob.RandomDelay = 30.0f;
		vob.RandomDelayVar = 20.0f;
		vob.InitiallyPlaying = true;
		vob.Ambient3d = false;
		vob.Obstruction = true;
		vob.ConeAngle = 0.0f;
		vob.VolumeType = SoundTriggerVolumeType.Spherical;
		vob.Radius = 3000.0f;
		vob.SoundName = "OW_CROW";
	}
}