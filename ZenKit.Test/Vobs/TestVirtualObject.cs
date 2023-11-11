using System;
using System.Numerics;
using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs
{
	public class TestVirtualObject
	{
		[OneTimeSetUp]
		public void SetUp()
		{
			Logger.Set(LogLevel.Trace,
				(level, name, message) =>
					Console.WriteLine(new DateTime() + " [ZenKit] (" + level + ") > " + name + ": " + message));
		}

		private void CheckVec2(Vector2 v, float x, float y)
		{
			Assert.That(v.X, Is.EqualTo(x));
			Assert.That(v.Y, Is.EqualTo(y));
		}

		private void CheckVec3(Vector3 v, float x, float y, float z)
		{
			Assert.That(v.X, Is.EqualTo(x));
			Assert.That(v.Y, Is.EqualTo(y));
			Assert.That(v.Z, Is.EqualTo(z));
		}

		[Test]
		public void TestLoadG1()
		{
			var vob = new VirtualObject("./Samples/G1/VOb/zCVob.zen", GameVersion.Gothic1);

			Assert.Multiple(() => CheckVec3(vob.BoundingBox.Min, -18966.623f, -236.707687f, 4373.23486f));
			Assert.Multiple(() => CheckVec3(vob.BoundingBox.Max, -18772.623f, -42.7076874f, 4567.23486f));
			Assert.Multiple(() => CheckVec3(vob.Position, -18869.623f, -139.707687f, 4470.23486f));

			Assert.That(vob.Rotation.X, Is.EqualTo(0));
			Assert.That(vob.Rotation.Y, Is.EqualTo(0));
			Assert.That(vob.Rotation.Z, Is.EqualTo(0));
			Assert.That(vob.Rotation.W, Is.EqualTo(1.0f));
			Assert.That(vob.ShowVisual, Is.True);
			Assert.That(vob.SpriteCameraFacingMode, Is.EqualTo(SpriteAlignment.None));
			Assert.That(vob.CdStatic, Is.False);
			Assert.That(vob.CdDynamic, Is.False);
			Assert.That(vob.Static, Is.False);
			Assert.That(vob.DynamicShadows, Is.EqualTo(ShadowType.None));
			Assert.That(vob.PhysicsEnabled, Is.False);
			Assert.That(vob.AnimationType, Is.EqualTo(AnimationType.None));
			Assert.That(vob.Bias, Is.EqualTo(0));
			Assert.That(vob.Ambient, Is.False);
			Assert.That(vob.AnimationStrength, Is.EqualTo(0));
			Assert.That(vob.FarClipScale, Is.EqualTo(0));
			Assert.That(vob.PresetName, Is.EqualTo(""));
			Assert.That(vob.Name, Is.EqualTo(""));
			Assert.That(vob.VisualName, Is.EqualTo("FIRE.pfx"));
			Assert.That(vob.VisualType, Is.EqualTo(VisualType.ParticleEffect));
			Assert.That(vob.VisualDecal, Is.Null);
		}

		[Test]
		public void TestLoadG2()
		{
			var vob = new VirtualObject("./Samples/G2/VOb/zCVob.zen", GameVersion.Gothic2);

			Assert.Multiple(() => CheckVec3(vob.BoundingBox.Min, 30897.1035f, 4760.24951f, -14865.5723f));
			Assert.Multiple(() => CheckVec3(vob.BoundingBox.Max, 30929.8301f, 4836.17529f, -14817.3135f));
			Assert.Multiple(() => CheckVec3(vob.Position, 30913.4668f, 4798.9751f, -14841.4434f));

			Assert.That(vob.Rotation.X, Is.EqualTo(0));
			Assert.That(vob.Rotation.Y, Is.EqualTo(-0.199367985f));
			Assert.That(vob.Rotation.Z, Is.EqualTo(0));
			Assert.That(vob.Rotation.W, Is.EqualTo(0.979924798f));
			Assert.That(vob.ShowVisual, Is.True);
			Assert.That(vob.SpriteCameraFacingMode, Is.EqualTo(SpriteAlignment.None));
			Assert.That(vob.CdStatic, Is.False);
			Assert.That(vob.CdDynamic, Is.False);
			Assert.That(vob.Static, Is.True);
			Assert.That(vob.DynamicShadows, Is.EqualTo(ShadowType.None));
			Assert.That(vob.PhysicsEnabled, Is.False);
			Assert.That(vob.AnimationType, Is.EqualTo(AnimationType.None));
			Assert.That(vob.Bias, Is.EqualTo(0));
			Assert.That(vob.Ambient, Is.False);
			Assert.That(vob.AnimationStrength, Is.EqualTo(0));
			Assert.That(vob.FarClipScale, Is.EqualTo(1));
			Assert.That(vob.PresetName, Is.EqualTo(""));
			Assert.That(vob.Name, Is.EqualTo(""));
			Assert.That(vob.VisualName, Is.EqualTo("OW_MISC_WALL_TORCH_01.3DS"));
			Assert.That(vob.VisualType, Is.EqualTo(VisualType.MultiResolutionMesh));
			Assert.That(vob.VisualDecal, Is.Null);
		}
	}
}