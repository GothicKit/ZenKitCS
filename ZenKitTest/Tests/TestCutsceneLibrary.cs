using System;
using NUnit.Framework;
using ZenKit;

namespace ZenKitTest.Tests
{
	public class TestCutsceneLibrary
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
			var csl = new CutsceneLibrary("./Samples/ou.proprietary.bin");
			var cslBlocks = csl.Blocks;
			Assert.That(cslBlocks, Has.Count.EqualTo(7360));

			var msg20 = csl.GetBlock("DIA_ARTO_PERM_15_00");
			var msg100 = csl.GetBlock("DIA_BaalKagan_WasDrin_13_01");
			var msg200 = cslBlocks[200];
			var msgNone = csl.GetBlock("nonexistent");

			Assert.Multiple(() =>
			{
				Assert.That(msg20, Is.Not.EqualTo(null));
				Assert.That(msg100, Is.Not.EqualTo(null));
				Assert.That(msg200, Is.Not.EqualTo(null));
				Assert.That(msgNone, Is.EqualTo(null));
			});
        
			Assert.Multiple(() =>
			{
				Assert.That(msg20!.Message.Type, Is.EqualTo(0));
				Assert.That(msg20.Message.Text, Is.EqualTo("Du redest nicht viel, was?"));
				Assert.That(msg20.Message.Name, Is.EqualTo("DIA_ARTO_PERM_15_00.WAV"));
			});
        
			Assert.Multiple(() =>
			{
				Assert.That(msg100!.Message.Type, Is.EqualTo(0));
				Assert.That(msg100.Message.Text, Is.EqualTo("Ich kann dich auf viele Arten entlohnen."));
				Assert.That(msg100.Message.Name, Is.EqualTo("DIA_BAALKAGAN_WASDRIN_13_01.WAV"));
			});
        
			Assert.Multiple(() =>
			{
				Assert.That(msg200.Message.Type, Is.EqualTo(0));
				Assert.That(msg200.Message.Text, Is.EqualTo("Stimmt genau."));
				Assert.That(msg200.Message.Name, Is.EqualTo("DIA_BAALTARAN_INTOCASTLE_EXACTLY_15_00.WAV"));
			});
		}
	}
}