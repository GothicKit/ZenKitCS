using System;
using System.Numerics;
using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestMover
{
	[Test]
	public void TestLoad()
	{
		var vob = new Mover("./Samples/G2/VOb/zCMover.zen", GameVersion.Gothic2);

		Assert.That(vob.Behavior, Is.EqualTo(MoverBehavior.Toggle));
		Assert.That(vob.TouchBlockerDamage, Is.EqualTo(0.0f));
		Assert.That(vob.StayOpenTime.TotalSeconds, Is.EqualTo(2.0f));
		Assert.That(vob.Speed, Is.EqualTo(0.0500000007f));
		Assert.That(vob.LerpType, Is.EqualTo(MoverLerpType.Curve));
		Assert.That(vob.SpeedType, Is.EqualTo(MoverSpeedType.SlowStartEnd));
		Assert.That(vob.Keyframes, Has.Length.EqualTo(2));
		Assert.That(vob.Keyframes[0].Position.X, Is.EqualTo(29785.9609f));
		Assert.That(vob.Keyframes[0].Position.Y, Is.EqualTo(5140.81982f));
		Assert.That(vob.Keyframes[0].Position.Z, Is.EqualTo(-16279.8477f));
		Assert.That(vob.Keyframes[0].Rotation.W, Is.EqualTo(0.999809802f));
		Assert.That(vob.Keyframes[0].Rotation.X, Is.EqualTo(-0.000760567724f));
		Assert.That(vob.Keyframes[0].Rotation.Y, Is.EqualTo(0.0174517576f));
		Assert.That(vob.Keyframes[0].Rotation.Z, Is.EqualTo(0.00869333092f));
		Assert.That(vob.SfxOpenStart, Is.EqualTo("GATE_START"));
		Assert.That(vob.SfxOpenEnd, Is.EqualTo("GATE_STOP"));
		Assert.That(vob.SfxTransitioning, Is.EqualTo("GATE_LOOP"));
		Assert.That(vob.SfxCloseStart, Is.EqualTo("GATE_START"));
		Assert.That(vob.SfxCloseEnd, Is.EqualTo("GATE_STOP"));
		Assert.That(vob.SfxLock, Is.EqualTo(""));
		Assert.That(vob.SfxUnlock, Is.EqualTo(""));
		Assert.That(vob.SfxUseLocked, Is.EqualTo(""));
		Assert.That(vob.IsLocked, Is.False);
		Assert.That(vob.AutoLink, Is.False);
		Assert.That(vob.AutoRotate, Is.False);
	}

	[Test]
	public void TestSetters()
	{
		var vob = new Mover("./Samples/G2/VOb/zCMover.zen", GameVersion.Gothic2);

		vob.Behavior = MoverBehavior.Toggle;
		vob.TouchBlockerDamage = 0.0f;
		vob.StayOpenTime = TimeSpan.FromSeconds(2.0f);
		vob.Speed = 0.0500000007f;
		vob.LerpType = MoverLerpType.Curve;
		vob.SpeedType = MoverSpeedType.SlowStartEnd;
		vob.Keyframes[0].Position = new Vector3(29785.9609f, 5140.81982f, -16279.8477f);
		vob.Keyframes[0].Rotation = new Quaternion(-0.000760567724f, 0.0174517576f, 0.00869333092f, 0.999809802f);
		vob.SfxOpenStart = "GATE_START";
		vob.SfxOpenEnd = "GATE_STOP";
		vob.SfxTransitioning = "GATE_LOOP";
		vob.SfxCloseStart = "GATE_START";
		vob.SfxCloseEnd = "GATE_STOP";
		vob.SfxLock = "";
		vob.SfxUnlock = "";
		vob.SfxUseLocked = "";
		vob.IsLocked = false;
		vob.AutoLink = false;
		vob.AutoRotate = false;
	}
}