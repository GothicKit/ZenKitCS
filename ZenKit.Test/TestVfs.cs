using System;
using NUnit.Framework;

namespace ZenKit.Test
{
	public class TestVfs
	{
		[OneTimeSetUp]
		public void SetUp()
		{
			Logger.Set(LogLevel.Trace,
				(level, name, message) =>
					Console.WriteLine(new DateTime() + " [ZenKit] (" + level + ") > " + name + ": " + message));
		}

		private void CheckContents(Vfs vfs)
		{
			var children = vfs.Root.Children;
			Assert.That(children.Count, Is.EqualTo(3));

			var configYml = vfs.Find("config.yml");
			Assert.That(configYml, Is.Not.EqualTo(null));
			Assert.That(configYml!.IsFile);

			var licensesDir = vfs.Find("licenses");
			Assert.That(licensesDir, Is.Not.EqualTo(null));
			Assert.That(licensesDir!.IsDir);
			Assert.That(licensesDir.Children.Count, Is.EqualTo(2));

			var mitMd = vfs.Find("MIT.MD");
			Assert.That(mitMd, Is.Not.EqualTo(null));
			Assert.That(mitMd!.IsFile);

			var gplDir = licensesDir.GetChild("gpl");
			Assert.That(gplDir, Is.Not.EqualTo(null));
			Assert.That(gplDir!.IsDir);
			Assert.That(gplDir.Children.Count, Is.EqualTo(2));
			Assert.That(gplDir.GetChild("lgpl"), Is.EqualTo(null));

			var lgplMd = gplDir.GetChild("lgpl-3.0.md");
			Assert.That(lgplMd, Is.Not.EqualTo(null));
			Assert.That(lgplMd!.IsFile);

			var gplMd = gplDir.GetChild("gpl-3.0.MD");
			Assert.That(gplMd, Is.Not.EqualTo(null));
			Assert.That(gplMd!.IsFile);

			Assert.That(vfs.Find("lGpL-3.0.Md"), Is.Not.EqualTo(null));
			Assert.That(vfs.Find("nonexistent"), Is.EqualTo(null));
			Assert.That(vfs.Find("liceNSES"), Is.Not.EqualTo(null));
			Assert.That(vfs.Find(""), Is.EqualTo(null));

			Assert.That(vfs.Resolve("licEnSES/GPL/gpl-3.0.md"), Is.Not.EqualTo(null));
			Assert.That(vfs.Resolve("licEnSES/GPL/nonexistent"), Is.EqualTo(null));
			Assert.That(vfs.Resolve("/LICENSES"), Is.Not.EqualTo(null));
			Assert.That(vfs.Resolve(""), Is.Not.EqualTo(null));
			Assert.That(vfs.Resolve("/"), Is.Not.EqualTo(null));

			Assert.That(vfs.Find("config.yml "), Is.Not.EqualTo(null));
			Assert.That(vfs.Resolve("licEnSES /GPL/gpl-3.0.md "), Is.Not.EqualTo(null));
		}

		[Test]
		public void TestMountDisk()
		{
			var vfs = new Vfs();
			vfs.MountDisk("./Samples/basic.vdf", VfsOverwriteBehavior.Older);
			Assert.Multiple((() => CheckContents(vfs)));
		}

		[Test]
		public void TestMountHost()
		{
			var vfs = new Vfs();
			vfs.Mount("./Samples/basic.vdf.dir", "/", VfsOverwriteBehavior.All);
			Assert.Multiple((() => CheckContents(vfs)));
		}
	}
}