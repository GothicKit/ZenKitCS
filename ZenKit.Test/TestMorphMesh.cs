using System;
using NUnit.Framework;

namespace ZenKit.Test;

public class TestMorphMesh
{
	[OneTimeSetUp]
	public void SetUp()
	{
		Logger.Set(LogLevel.Trace,
			(level, name, message) =>
				Console.WriteLine(new DateTime() + " [ZenKit] (" + level + ") > " + name + ": " + message));
	}

	[Test]
	public void TestLoad()
	{
		var mmb = new MorphMesh("./Samples/morph0.mmb");
		Assert.That(mmb.Name, Is.EqualTo("ITRWSMALLBOW"));

		var positions = mmb.MorphPositions;
		Assert.That(positions, Has.Count.EqualTo(28));
		Assert.Multiple(() =>
		{
			Assert.That(positions[0].X, Is.EqualTo(-40.9461403f));
			Assert.That(positions[0].Y, Is.EqualTo(-1.73638999f));
			Assert.That(positions[0].Z, Is.EqualTo(-26.7512894f));
			Assert.That(positions[1].X, Is.EqualTo(-29.6147194f));
			Assert.That(positions[1].Y, Is.EqualTo(-1.97599006f));
			Assert.That(positions[1].Z, Is.EqualTo(-9.19756984f));
		});

		var animations = mmb.Animations;
		Assert.That(animations, Has.Count.EqualTo(4));
		Assert.That(mmb.AnimationCount, Is.EqualTo(4));

		var ani = animations[1];
		Assert.That(ani.Name, Is.EqualTo("S_SHOOT"));
		Assert.That(ani.Layer, Is.EqualTo(1));
		Assert.That(ani.BlendIn, Is.EqualTo(0.0100000007f));
		Assert.That(ani.BlendOut, Is.EqualTo(-0.0100000007f));
		Assert.That(ani.Duration.TotalSeconds, Is.EqualTo(0.4));
		Assert.That(ani.Speed, Is.EqualTo(0.0250000004f));
		Assert.That(ani.Flags, Is.EqualTo(0));
		Assert.That(ani.FrameCount, Is.EqualTo(10));

		var vertices = ani.Vertices;
		Assert.That(vertices, Has.Count.EqualTo(3));
		Assert.That(vertices[0], Is.EqualTo(25));
		Assert.That(vertices[1], Is.EqualTo(26));
		Assert.That(vertices[2], Is.EqualTo(27));

		var samples = ani.Samples;
		Assert.That(samples, Has.Count.EqualTo(30));
		Assert.That(samples[0].X, Is.EqualTo(0.519770026f));
		Assert.That(samples[0].Y, Is.EqualTo(0));
		Assert.That(samples[0].Z, Is.EqualTo(1.27206039f));
		Assert.That(samples[9].X, Is.EqualTo(0));
		Assert.That(samples[9].Y, Is.EqualTo(0));
		Assert.That(samples[9].Z, Is.EqualTo(0));
		Assert.That(samples[19].X, Is.EqualTo(-8.51126003f));
		Assert.That(samples[19].Y, Is.EqualTo(0));
		Assert.That(samples[19].Z, Is.EqualTo(-20.8299408f));

		var sources = mmb.Sources;
		Assert.That(sources, Has.Count.EqualTo(4));

		var source = sources[1];
		Assert.That(source.FileDate?.Year, Is.EqualTo(2000));
		Assert.That(source.FileDate?.Month, Is.EqualTo(5));
		Assert.That(source.FileDate?.Day, Is.EqualTo(8));
		Assert.That(source.FileDate?.Hour, Is.EqualTo(9));
		Assert.That(source.FileDate?.Minute, Is.EqualTo(13));
		Assert.That(source.FileDate?.Second, Is.EqualTo(58));
		Assert.That(source.FileName, Is.EqualTo("ITRWSMALLBOWSHOOT.ASC"));
	}
}