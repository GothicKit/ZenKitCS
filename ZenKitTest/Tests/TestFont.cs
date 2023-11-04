using NUnit.Framework;
using ZenKit;

namespace ZenKitTest.Tests;

public class TestFont
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
		var fnt = new Font("./Samples/G1/FONT_OLD_10_WHITE_HI.FNT");
		var fntGlyphs = fnt.Glyphs;

		Assert.That(fnt.Name, Is.EqualTo("FONT_OLD_10_WHITE_HI.TGA"));
		Assert.That(fnt.Height, Is.EqualTo(17));
		Assert.That(fnt.GlyphCount, Is.EqualTo(256));

		Assert.Multiple(() =>
		{
			var glyph = fnt.GetGlyph(0);
			Assert.That(glyph.width, Is.EqualTo(0));
			Assert.That(glyph.topLeft.X, Is.EqualTo(0));
			Assert.That(glyph.topLeft.Y, Is.EqualTo(0));
			Assert.That(glyph.bottomRight.X, Is.EqualTo(0));
			Assert.That(glyph.bottomRight.Y, Is.EqualTo(0));
		});

		Assert.Multiple(() =>
		{
			var glyph = fntGlyphs[127];
			Assert.That(glyph.width, Is.EqualTo(8));
			Assert.That(glyph.topLeft.X, Is.EqualTo(0.3984375f));
			Assert.That(glyph.topLeft.Y, Is.EqualTo(0.23828125f));
			Assert.That(glyph.bottomRight.X, Is.EqualTo(0.412109375f));
			Assert.That(glyph.bottomRight.Y, Is.EqualTo(0.30859375f));
		});

		Assert.Multiple(() =>
		{
			var glyph = fnt.GetGlyph(255);
			Assert.That(glyph.width, Is.EqualTo(9));
			Assert.That(glyph.topLeft.X, Is.EqualTo(0.95703125f));
			Assert.That(glyph.topLeft.Y, Is.EqualTo(0.55078125f));
			Assert.That(glyph.bottomRight.X, Is.EqualTo(0.97265625f));
			Assert.That(glyph.bottomRight.Y, Is.EqualTo(0.62109375f));
		});
	}

	[Test]
	public void TestLoadG2()
	{
		var fnt = new Font("./Samples/G2/FONT_OLD_10_WHITE_HI.FNT");
		var fntGlyphs = fnt.Glyphs;

		Assert.That(fnt.Name, Is.EqualTo("FONT_OLD_10_WHITE_HI.TGA"));
		Assert.That(fnt.Height, Is.EqualTo(18));
		Assert.That(fnt.GlyphCount, Is.EqualTo(256));

		Assert.Multiple(() =>
		{
			var glyph = fnt.GetGlyph(0);
			Assert.That(glyph.width, Is.EqualTo(0));
			Assert.That(glyph.topLeft.X, Is.EqualTo(0));
			Assert.That(glyph.topLeft.Y, Is.EqualTo(0));
			Assert.That(glyph.bottomRight.X, Is.EqualTo(0));
			Assert.That(glyph.bottomRight.Y, Is.EqualTo(0));
		});

		Assert.Multiple(() =>
		{
			var glyph = fntGlyphs[127];
			Assert.That(glyph.width, Is.EqualTo(8));
			Assert.That(glyph.topLeft.X, Is.EqualTo(0.3984375f));
			Assert.That(glyph.topLeft.Y, Is.EqualTo(0.23828125f));
			Assert.That(glyph.bottomRight.X, Is.EqualTo(0.412109375f));
			Assert.That(glyph.bottomRight.Y, Is.EqualTo(0.30859375f));
		});

		Assert.Multiple(() =>
		{
			var glyph = fnt.GetGlyph(255);
			Assert.That(glyph.width, Is.EqualTo(10));
			Assert.That(glyph.topLeft.X, Is.EqualTo(0.958984375f));
			Assert.That(glyph.topLeft.Y, Is.EqualTo(0.55078125f));
			Assert.That(glyph.bottomRight.X, Is.EqualTo(0.9765625f));
			Assert.That(glyph.bottomRight.Y, Is.EqualTo(0.62109375f));
		});
	}
}