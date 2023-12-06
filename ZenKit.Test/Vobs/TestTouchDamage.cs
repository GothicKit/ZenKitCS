using System;
using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestTouchDamage
{
	[Test]
	public void TestLoad()
	{
		var vob = new TouchDamage("./Samples/G2/VOb/oCTouchDamage.zen", GameVersion.Gothic2);
		Assert.That(vob.Damage, Is.EqualTo(1000.0f));
		Assert.That(vob.IsBarrier, Is.True);
		Assert.That(vob.IsBlunt, Is.False);
		Assert.That(vob.IsEdge, Is.True);
		Assert.That(vob.IsFire, Is.False);
		Assert.That(vob.IsFly, Is.False);
		Assert.That(vob.IsMagic, Is.False);
		Assert.That(vob.IsPoint, Is.False);
		Assert.That(vob.IsFall, Is.False);
		Assert.That(vob.RepeatDelay.TotalSeconds, Is.EqualTo(0.0f));
		Assert.That(vob.VolumeScale, Is.EqualTo(1.0f));
		Assert.That(vob.CollisionType, Is.EqualTo(TouchCollisionType.Box));
	}

	[Test]
	public void TestSetters()
	{
		var vob = new TouchDamage("./Samples/G2/VOb/oCTouchDamage.zen", GameVersion.Gothic2);
		vob.Damage = 1000.0f;
		vob.IsBarrier = true;
		vob.IsBlunt = false;
		vob.IsEdge = true;
		vob.IsFire = false;
		vob.IsFly = false;
		vob.IsMagic = false;
		vob.IsPoint = false;
		vob.IsFall = false;
		vob.RepeatDelay = TimeSpan.FromSeconds(0.0f);
		vob.VolumeScale = 1.0f;
		vob.CollisionType = TouchCollisionType.Box;
	}
}