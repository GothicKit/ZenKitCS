using System;
using NUnit.Framework;

namespace ZenKit.Test;

public class TestModelAnimation
{
	private static readonly int[] NodeIndicesG1 =
		{ 0, 1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 15, 16, 17, 18, 19, 26, 27, 28, 29, 30, 31, 32, 33 };

	[OneTimeSetUp]
	public void SetUp()
	{
		Logger.Set(LogLevel.Trace,
			(level, name, message) =>
				Console.WriteLine(new DateTime() + " [ZenKit] (" + level + ") > " + name + ": " + message));
	}

	private void CheckSample(AnimationSample sample, float pX, float pY, float pZ, float rX, float rY, float rZ,
		float rW)
	{
		Assert.That(sample.Position.X, Is.EqualTo(pX));
		Assert.That(sample.Position.Y, Is.EqualTo(pY));
		Assert.That(sample.Position.Z, Is.EqualTo(pZ));
		Assert.That(sample.Rotation.X, Is.EqualTo(rX));
		Assert.That(sample.Rotation.Y, Is.EqualTo(rY));
		Assert.That(sample.Rotation.Z, Is.EqualTo(rZ));
		Assert.That(sample.Rotation.W, Is.EqualTo(rW));
	}

	[Test]
	public void TestLoadG1()
	{
		var ani = new ModelAnimation("./Samples/G1/HUMANS-S_FISTRUN.MAN");

		var bbox = ani.BoundingBox;
		Assert.Multiple(() =>
		{
			Assert.That(bbox.Max.X, Is.EqualTo(46.33139419555664f));
			Assert.That(bbox.Max.Y, Is.EqualTo(67.0935287475586f));
			Assert.That(bbox.Max.Z, Is.EqualTo(49.88602828979492f));
			Assert.That(bbox.Min.X, Is.EqualTo(-51.09061050415039f));
			Assert.That(bbox.Min.Y, Is.EqualTo(-94.02226257324219f));
			Assert.That(bbox.Min.Z, Is.EqualTo(-31.280731201171875f));
		});

		Assert.Multiple(() =>
		{
			Assert.That(ani.Checksum, Is.EqualTo(3325331650));
			Assert.That(ani.Fps, Is.EqualTo(10.0f));
			Assert.That(ani.FpsSource, Is.EqualTo(25.0));
			Assert.That(ani.FrameCount, Is.EqualTo(20));
			Assert.That(ani.Layer, Is.EqualTo(1));
			Assert.That(ani.Name, Is.EqualTo("S_FISTRUN"));
			Assert.That(ani.Next, Is.EqualTo("S_FISTRUN"));
		});

		Assert.Multiple(() =>
		{
			var nodeIndices = ani.NodeIndices;
			Assert.That(ani.NodeCount, Is.EqualTo(25));
			Assert.That(nodeIndices, Has.Count.EqualTo(25));
			Assert.That(nodeIndices, Is.EqualTo(NodeIndicesG1));
		});

		var aniSamples = ani.Samples;
		Assert.That(ani.SampleCount, Is.EqualTo(25 * 20));
		Assert.That(aniSamples, Has.Count.EqualTo(25 * 20));

		Assert.Multiple(() => CheckSample(aniSamples[0], 12.635274887084961f, 88.75251770019531f, -1.093428611755371f,
			0.0f, 0.6293110251426697f, 0.0f, 0.7771535515785217f));

		Assert.Multiple(() => CheckSample(ani.GetSample(249), 12.626323699951172f, -0.00145721435546875f,
			22.643518447875977f, 0.0f, 0.70708167552948f, 0.0f, 0.7071319222450256f));

		Assert.Multiple(() => CheckSample(aniSamples[499], 12.626323699951172f, -0.00145721435546875f,
			22.643518447875977f, 0.0f, 0.70708167552948f, 0.0f, 0.7071319222450256f));

		Assert.That(ani.SourcePath, Is.EqualTo("\\_WORK\\DATA\\ANIMS\\HUM_AMB_FISTRUN_M01.ASC"));
		Assert.That(ani.SourceScript,
			Is.EqualTo(
				"\t\t\tANI\t\t\t(\"S_FISTRUN\"\t\t\t\t1\t\"S_FISTRUN\"\t\t0.0 0.1 MI\t\"HUM_AMB_FISTRUN_M01.ASC\"\tF   1\t50\tFPS:10)"));
	}

	[Test]
	public void TestLoadG2()
	{
		var ani = new ModelAnimation("./Samples/G2/HUMANS-S_FISTRUN.MAN");

		var bbox = ani.BoundingBox;
		Assert.Multiple(() =>
		{
			Assert.That(bbox.Max.X, Is.EqualTo(46.33139419555664f));
			Assert.That(bbox.Max.Y, Is.EqualTo(67.0935287475586f));
			Assert.That(bbox.Max.Z, Is.EqualTo(49.88602828979492f));
			Assert.That(bbox.Min.X, Is.EqualTo(-51.090614318847656f));
			Assert.That(bbox.Min.Y, Is.EqualTo(-94.02226257324219f));
			Assert.That(bbox.Min.Z, Is.EqualTo(-31.280733108520508f));
		});

		Assert.Multiple(() =>
		{
			Assert.That(ani.Checksum, Is.EqualTo(3325331650));
			Assert.That(ani.Fps, Is.EqualTo(10.0f));
			Assert.That(ani.FpsSource, Is.EqualTo(25.0));
			Assert.That(ani.FrameCount, Is.EqualTo(20));
			Assert.That(ani.Layer, Is.EqualTo(1));
			Assert.That(ani.Name, Is.EqualTo("S_FISTRUN"));
			Assert.That(ani.Next, Is.EqualTo("S_FISTRUN"));
		});

		Assert.Multiple(() =>
		{
			var nodeIndices = ani.NodeIndices;
			Assert.That(ani.NodeCount, Is.EqualTo(25));
			Assert.That(nodeIndices, Has.Count.EqualTo(25));
			Assert.That(nodeIndices, Is.EqualTo(NodeIndicesG1));
		});

		var aniSamples = ani.Samples;
		Assert.That(ani.SampleCount, Is.EqualTo(25 * 20));
		Assert.That(aniSamples, Has.Count.EqualTo(25 * 20));

		Assert.Multiple(() => CheckSample(aniSamples[0], 12.635274887084961f, 88.75251770019531f, -1.093428611755371f,
			0.0f, 0.6293110251426697f, 0.0f, 0.7771535515785217f));

		Assert.Multiple(() => CheckSample(ani.GetSample(249), 12.626323699951172f, -0.00145721435546875f,
			22.643518447875977f, 0.0f, 0.70708167552948f, 0.0f, 0.7071319222450256f));

		Assert.Multiple(() => CheckSample(aniSamples[499], 12.626323699951172f, -0.00145721435546875f,
			22.643518447875977f, 0.0f, 0.70708167552948f, 0.0f, 0.7071319222450256f));

		Assert.That(ani.SourcePath, Is.EqualTo("\\_WORK\\DATA\\ANIMS\\HUM_AMB_FISTRUN_M01.ASC"));
		Assert.That(ani.SourceScript,
			Is.EqualTo(
				"\t\t\tANI\t\t\t(\"S_FISTRUN\"\t\t\t\t1\t\"S_FISTRUN\"\t\t0.0 0.1 MI\t\"HUM_AMB_FISTRUN_M01.ASC\"\tF   1\t50\tFPS:10)"));
	}
}