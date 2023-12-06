using System;
using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestCamera
	{
		[OneTimeSetUp]
		public void SetUp()
		{
			Logger.Set(LogLevel.Trace,
				(level, name, message) =>
					Console.WriteLine(new DateTime() + " [ZenKit] (" + level + ") > " + name + ": " + message));
		}

		[Test]
		public void TestLoadG2()
		{
			var vob = new CutsceneCamera("./Samples/G2/VOb/zCCSCamera.zen", GameVersion.Gothic2);

			Assert.That(vob.TrajectoryFOR, Is.EqualTo(CameraTrajectory.World));
			Assert.That(vob.TargetTrajectoryFOR, Is.EqualTo(CameraTrajectory.World));
			Assert.That(vob.LoopMode, Is.EqualTo(CameraLoopType.None));
			Assert.That(vob.LerpMode, Is.EqualTo(CameraLerpType.Path));
			Assert.That(vob.IgnoreFORVobRotation, Is.False);
			Assert.That(vob.IgnoreFORVobRotationTarget, Is.False);
			Assert.That(vob.Adapt, Is.False);
			Assert.That(vob.EaseFirst, Is.False);
			Assert.That(vob.EaseLast, Is.False);
			Assert.That(vob.TotalDuration, Is.EqualTo(20.0f));
			Assert.That(vob.AutoFocusVob, Is.EqualTo(""));
			Assert.That(vob.AutoPlayerMovable, Is.False);
			Assert.That(vob.AutoUntriggerLast, Is.False);
			Assert.That(vob.AutoUntriggerLastDelay, Is.EqualTo(0.0f));
			Assert.That(vob.PositionCount, Is.EqualTo(2));
			Assert.That(vob.TargetCount, Is.EqualTo(1));

			var frames = vob.Frames;
			Assert.That(frames, Has.Count.EqualTo(3));
			Assert.That(frames[0].Time, Is.EqualTo(0.0f));
			Assert.That(frames[0].RollAngle, Is.EqualTo(0.0f));
			Assert.That(frames[0].FovScale, Is.EqualTo(1.0f));
			Assert.That(frames[0].MotionType, Is.EqualTo(CameraMotion.Slow));
			Assert.That(frames[0].MotionTypeFov, Is.EqualTo(CameraMotion.Smooth));
			Assert.That(frames[0].MotionTypeRoll, Is.EqualTo(CameraMotion.Smooth));
			Assert.That(frames[0].MotionTypeTimeScale, Is.EqualTo(CameraMotion.Smooth));
			Assert.That(frames[0].Tension, Is.EqualTo(0.0f));
			Assert.That(frames[0].CamBias, Is.EqualTo(0.0f));
			Assert.That(frames[0].Continuity, Is.EqualTo(0.0f));
			Assert.That(frames[0].TimeScale, Is.EqualTo(1.0f));
			Assert.That(frames[0].TimeFixed, Is.False);

			var pose = frames[0].OriginalPose;
			Assert.That(pose.M11, Is.EqualTo(0.202226311f));
			Assert.That(pose.M21, Is.EqualTo(3.00647909e-11f));
			Assert.That(pose.M31, Is.EqualTo(-0.979338825f));
			Assert.That(pose.M41, Is.EqualTo(0));

			Assert.That(pose.M12, Is.EqualTo(0.00805913191f));
			Assert.That(pose.M22, Is.EqualTo(0.999966145f));
			Assert.That(pose.M32, Is.EqualTo(0.00166415179f));
			Assert.That(pose.M42, Is.EqualTo(0.0f));

			Assert.That(pose.M13, Is.EqualTo(0.979305685f));
			Assert.That(pose.M23, Is.EqualTo(-0.00822915602f));
			Assert.That(pose.M33, Is.EqualTo(0.202219456f));
			Assert.That(pose.M43, Is.EqualTo(0.0f));

			Assert.That(pose.M14, Is.EqualTo(81815.7656f));
			Assert.That(pose.M24, Is.EqualTo(3905.95044f));
			Assert.That(pose.M34, Is.EqualTo(29227.1875f));
			Assert.That(pose.M44, Is.EqualTo(1.0f));
		}

		[Test]
		public void TestSetters()
		{
			var vob = new CutsceneCamera("./Samples/G2/VOb/zCCSCamera.zen", GameVersion.Gothic2);
			vob.TrajectoryFOR = CameraTrajectory.World;
			vob.TargetTrajectoryFOR = CameraTrajectory.World;
			vob.LoopMode = CameraLoopType.None;
			vob.LerpMode = CameraLerpType.Path;
			vob.IgnoreFORVobRotation = false;
			vob.IgnoreFORVobRotationTarget = false;
			vob.Adapt = false;
			vob.EaseFirst = false;
			vob.EaseLast = false;
			vob.TotalDuration = 20.0f;
			vob.AutoFocusVob = "";
			vob.AutoPlayerMovable = false;
			vob.AutoUntriggerLast = false;
			vob.AutoUntriggerLastDelay = 1.0f;

			vob.Frames[0].Time = 0;
			vob.Frames[0].RollAngle = 0;
			vob.Frames[0].FovScale = 1;
			vob.Frames[0].MotionType = CameraMotion.Slow;
			vob.Frames[0].MotionTypeFov = CameraMotion.Smooth;
			vob.Frames[0].MotionTypeRoll = CameraMotion.Smooth;
			vob.Frames[0].MotionTypeTimeScale = CameraMotion.Smooth;
			vob.Frames[0].Tension = 0;
			vob.Frames[0].CamBias = 0;
			vob.Frames[0].Continuity = 0;
			vob.Frames[0].TimeScale = 1;
			vob.Frames[0].TimeFixed = false;
		}
	}
}