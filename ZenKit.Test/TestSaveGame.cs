using System;
using NUnit.Framework;

namespace ZenKit.Test;

public class TestSaveGame
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
		var sav = new SaveGame(GameVersion.Gothic1);
		sav.Load("./Samples/G1/Save");

		var meta = sav.Metadata;
		Assert.That(meta.Title, Is.EqualTo("sds"));
		Assert.That(meta.World, Is.EqualTo("WORLD"));
		Assert.That(meta.TimeDay, Is.EqualTo(0));
		Assert.That(meta.TimeHour, Is.EqualTo(8));
		Assert.That(meta.TimeMinute, Is.EqualTo(6));
		Assert.That(meta.SaveDate, Is.EqualTo("24.12.2022 - 21:36"));
		Assert.That(meta.VersionMajor, Is.EqualTo(1));
		Assert.That(meta.VersionMinor, Is.EqualTo(87));
		Assert.That(meta.PlayTime.TotalSeconds, Is.EqualTo(49));

		var state = sav.State;
		Assert.That(state.Day, Is.EqualTo(0));
		Assert.That(state.Hour, Is.EqualTo(8));
		Assert.That(state.Minute, Is.EqualTo(6));

		// Try to parse the world data.
		var wld = sav.LoadWorld();
	}
}