using NUnit.Framework;
using ZenKit;
using ZenKit.Vobs;

namespace ZenKitTest.Tests.Vobs;

public class TestZoneMusic
{
	[Test]
	public void TestLoad()
	{
		var vob = new ZoneMusic("./Samples/G2/VOb/oCZoneMusic.zen", GameVersion.Gothic2);
		Assert.That(vob.IsEnabled, Is.True);
		Assert.That(vob.Priority, Is.EqualTo(1));
		Assert.That(vob.IsEllipsoid, Is.False);
		Assert.That(vob.Reverb, Is.EqualTo(-3.2190001f));
		Assert.That(vob.Volume, Is.EqualTo(1.0f));
		Assert.That(vob.IsLoop, Is.True);
	}
}