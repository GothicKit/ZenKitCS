using NUnit.Framework;
using ZenKit;

namespace ZenKitTest;

public class TestMaterial
{
	[OneTimeSetUp]
	public void SetUp()
	{
		Logger.Set(LogLevel.Trace,
			(level, name, message) =>
				Console.WriteLine(new DateTime() + " [ZenKit] (" + level + ") > " + name + ": " + message));
	}

	[Test]
	public void TestLoadG1()
	{
		var mat = new Material("./Samples/G1/DEMON_DIE_BODY.MAT");
		Assert.That(mat.Name, Is.EqualTo("BODY"));
		Assert.That(mat.Group, Is.EqualTo(MaterialGroup.Undefined));
		Assert.That(mat.Color.R, Is.EqualTo(115));
		Assert.That(mat.Color.G, Is.EqualTo(91));
		Assert.That(mat.Color.B, Is.EqualTo(77));
		Assert.That(mat.Color.A, Is.EqualTo(255));
		Assert.That(mat.SmoothAngle, Is.EqualTo(60.0f));
		Assert.That(mat.Texture, Is.EqualTo("DEM_BODY_V0.TGA"));
		Assert.That(mat.TextureScale.X, Is.EqualTo(512.0f));
		Assert.That(mat.TextureScale.Y, Is.EqualTo(512.0f));
		Assert.That(mat.TextureAnimationFps, Is.EqualTo(0.0f));
		Assert.That(mat.TextureAnimationMapping, Is.EqualTo(AnimationMapping.None));
		Assert.That(mat.TextureAnimationMappingDirection.X, Is.EqualTo(9.9999997e-005f));
		Assert.That(mat.TextureAnimationMappingDirection.Y, Is.EqualTo(0.0f));
		Assert.That(mat.DisableCollision, Is.EqualTo(false));
		Assert.That(mat.DisableLightmap, Is.EqualTo(false));
		Assert.That(mat.DontCollapse, Is.EqualTo(false));
		Assert.That(mat.DetailObject, Is.EqualTo(""));
		Assert.That(mat.DefaultMapping.X, Is.EqualTo(2.34375f));
		Assert.That(mat.DefaultMapping.Y, Is.EqualTo(2.34375f));
	}

	[Test]
	public void TestLoadG2()
	{
		var mat = new Material("./Samples/G2/DEMON_DIE_BODY.MAT");
		Assert.That(mat.Name, Is.EqualTo("BODY"));
		Assert.That(mat.Group, Is.EqualTo(MaterialGroup.Undefined));
		Assert.That(mat.Color.R, Is.EqualTo(115));
		Assert.That(mat.Color.G, Is.EqualTo(91));
		Assert.That(mat.Color.B, Is.EqualTo(77));
		Assert.That(mat.Color.A, Is.EqualTo(255));
		Assert.That(mat.SmoothAngle, Is.EqualTo(60.0f));
		Assert.That(mat.Texture, Is.EqualTo("DEM_BODY_V0.TGA"));
		Assert.That(mat.TextureScale.X, Is.EqualTo(512.0f));
		Assert.That(mat.TextureScale.Y, Is.EqualTo(512.0f));
		Assert.That(mat.TextureAnimationFps, Is.EqualTo(0.0f));
		Assert.That(mat.TextureAnimationMapping, Is.EqualTo(AnimationMapping.None));
		Assert.That(mat.TextureAnimationMappingDirection.X, Is.EqualTo(0.0f));
		Assert.That(mat.TextureAnimationMappingDirection.Y, Is.EqualTo(0.0f));
		Assert.That(mat.DisableCollision, Is.EqualTo(false));
		Assert.That(mat.DisableLightmap, Is.EqualTo(false));
		Assert.That(mat.DontCollapse, Is.EqualTo(false));
		Assert.That(mat.DetailObject, Is.EqualTo(""));
		Assert.That(mat.DefaultMapping.X, Is.EqualTo(2.34375f));
		Assert.That(mat.DefaultMapping.Y, Is.EqualTo(2.34375f));
		Assert.That(mat.AlphaFunction, Is.EqualTo(AlphaFunction.None));
		Assert.That(mat.DetailObjectScale, Is.EqualTo(1.0f));
		Assert.That(mat.ForceOccluder, Is.EqualTo(false));
		Assert.That(mat.EnvironmentMapping, Is.EqualTo(false));
		Assert.That(mat.EnvironmentMappingStrength, Is.EqualTo(1.0f));
		Assert.That(mat.WaveMode, Is.EqualTo(WaveMode.None));
		Assert.That(mat.WaveSpeed, Is.EqualTo(WaveSpeed.Normal));
		Assert.That(mat.WaveAmplitude, Is.EqualTo(30.0f));
		Assert.That(mat.WaveGridSize, Is.EqualTo(100.0f));
		Assert.That(mat.IgnoreSun, Is.EqualTo(false));
	}
}