using System;
using NUnit.Framework;

namespace ZenKit.Test;

public class TestTexture
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
		var tex = new Texture("./Samples/erz.tex");

		Assert.That(tex.Width, Is.EqualTo(128));
		Assert.That(tex.Height, Is.EqualTo(128));
		Assert.That(tex.WidthRef, Is.EqualTo(128));
		Assert.That(tex.HeightRef, Is.EqualTo(128));
		Assert.That(tex.GetWidth(0), Is.EqualTo(128));
		Assert.That(tex.GetHeight(0), Is.EqualTo(128));
		Assert.That(tex.GetWidth(1), Is.EqualTo(64));
		Assert.That(tex.GetHeight(1), Is.EqualTo(64));
		Assert.That(tex.GetWidth(2), Is.EqualTo(32));
		Assert.That(tex.GetHeight(2), Is.EqualTo(32));
		Assert.That(tex.AverageColor.A, Is.EqualTo(255));
		Assert.That(tex.AverageColor.R, Is.EqualTo(0x44));
		Assert.That(tex.AverageColor.G, Is.EqualTo(0x3a));
		Assert.That(tex.AverageColor.B, Is.EqualTo(0x3c));
		Assert.That(tex.MipmapCount, Is.EqualTo(5));
		Assert.That(tex.AllMipmapsRaw, Has.Count.EqualTo(5));
		Assert.That(tex.AllMipmapsRgba, Has.Count.EqualTo(5));
		Assert.That(tex.Format, Is.EqualTo(TextureFormat.Dxt1));
	}
}