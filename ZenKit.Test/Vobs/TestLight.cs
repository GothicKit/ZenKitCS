using System;
using System.Drawing;
using NUnit.Framework;
using ZenKit.Vobs;

namespace ZenKit.Test.Vobs;

public class TestLight
{
	private static readonly float[] G1LightRangeAnimationScale = { };

	private static readonly Color[] G1LightColorAnimationList =
	{
		Color.FromArgb(255, 211, 147, 107), Color.FromArgb(255, 223, 173, 117), Color.FromArgb(255, 211, 147, 107),
		Color.FromArgb(255, 223, 173, 117), Color.FromArgb(255, 225, 197, 100), Color.FromArgb(255, 223, 173, 117),
		Color.FromArgb(255, 227, 209, 106), Color.FromArgb(255, 223, 173, 117), Color.FromArgb(255, 211, 147, 107),
		Color.FromArgb(255, 223, 173, 117), Color.FromArgb(255, 225, 197, 100), Color.FromArgb(255, 227, 209, 106),
		Color.FromArgb(255, 223, 173, 117), Color.FromArgb(255, 211, 147, 107), Color.FromArgb(255, 225, 197, 100),
		Color.FromArgb(255, 223, 173, 117), Color.FromArgb(255, 225, 197, 100), Color.FromArgb(255, 211, 147, 107),
		Color.FromArgb(255, 223, 173, 117), Color.FromArgb(255, 227, 209, 106), Color.FromArgb(255, 225, 197, 100),
		Color.FromArgb(255, 211, 147, 107), Color.FromArgb(255, 225, 197, 100), Color.FromArgb(255, 223, 173, 117),
		Color.FromArgb(255, 225, 197, 100), Color.FromArgb(255, 227, 209, 106), Color.FromArgb(255, 223, 173, 117),
		Color.FromArgb(255, 211, 147, 107), Color.FromArgb(255, 223, 173, 117), Color.FromArgb(255, 211, 147, 107),
		Color.FromArgb(255, 225, 197, 100), Color.FromArgb(255, 227, 209, 106), Color.FromArgb(255, 223, 173, 117)
	};

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
		var vob = new Light("./Samples/G1/VOb/zCVobLight.zen", GameVersion.Gothic1);

		Assert.That(vob.PresetName, Is.EqualTo(""));
		Assert.That(vob.LightType, Is.EqualTo(LightType.Point));
		Assert.That(vob.Range, Is.EqualTo(2000.0f));
		Assert.That(vob.Color, Is.EqualTo(Color.FromArgb(255, 223, 173, 117)));
		Assert.That(vob.ConeAngle, Is.EqualTo(0.0f));
		Assert.That(vob.LightStatic, Is.False);
		Assert.That(vob.Quality, Is.EqualTo(LightQuality.Low));
		Assert.That(vob.LensflareFx, Is.EqualTo(""));
		Assert.That(vob.RangeAnimationScale, Is.EqualTo(G1LightRangeAnimationScale));
		Assert.That(vob.RangeAnimationFps, Is.EqualTo(0.0f));
		Assert.That(vob.ColorAnimationList, Is.EqualTo(G1LightColorAnimationList));
		Assert.That(vob.ColorAnimationFps, Is.EqualTo(11.0000067f));
		Assert.That(vob.On, Is.True);
		Assert.That(vob.RangeAnimationSmooth, Is.True);
		Assert.That(vob.CanMove, Is.True);
		Assert.That(vob.ColorAnimationSmooth, Is.False);
	}


	[Test]
	public void TestSetters()
	{
		var vob = new Light("./Samples/G1/VOb/zCVobLight.zen", GameVersion.Gothic1);

		vob.PresetName = "";
		vob.LightType = LightType.Point;
		vob.Range = 2000.0f;
		vob.Color = Color.FromArgb(255, 223, 173, 117);
		vob.ConeAngle = 0.0f;
		vob.LightStatic = false;
		vob.Quality = LightQuality.Low;
		vob.LensflareFx = "";
		vob.RangeAnimationScale = G1LightRangeAnimationScale;
		vob.RangeAnimationFps = 0.0f;
		vob.ColorAnimationList = G1LightColorAnimationList;
		vob.ColorAnimationFps = 11.0000067f;
		vob.On = true;
		vob.RangeAnimationSmooth = false;
		vob.CanMove = true;
		vob.ColorAnimationSmooth = false;
	}
}