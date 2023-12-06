using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestInteractiveObject
{
	[Test]
	public void TestLoad()
	{
		var vob = new InteractiveObject("./Samples/G2/VOb/oCMobInter.zen", GameVersion.Gothic2);
		Assert.That(vob.State, Is.EqualTo(1));
		Assert.That(vob.Target, Is.EqualTo(""));
		Assert.That(vob.Item, Is.EqualTo(""));
		Assert.That(vob.ConditionFunction, Is.EqualTo(""));
		Assert.That(vob.OnStateChangeFunction, Is.EqualTo("PRAYIDOL"));
		Assert.That(vob.Rewind, Is.False);
	}

	[Test]
	public void TestSetters()
	{
		var vob = new InteractiveObject("./Samples/G2/VOb/oCMobInter.zen", GameVersion.Gothic2);
		vob.State = 1;
		vob.Target = "";
		vob.Item = "";
		vob.ConditionFunction = "";
		vob.OnStateChangeFunction = "PRAYIDOL";
		vob.Rewind = true;
	}
}