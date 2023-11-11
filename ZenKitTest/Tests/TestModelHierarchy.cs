using System;
using NUnit.Framework;
using ZenKit;

namespace ZenKitTest.Tests
{
	public class TestModelHierarchy
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
			var mdh = new ModelHierarchy("./Samples/hierarchy0.mdh");
			var mdhNodes = mdh.Nodes;
		
			Assert.That(mdh.NodeCount, Is.EqualTo(7));
			Assert.That(mdhNodes, Has.Count.EqualTo(7));
	
			Assert.That(mdhNodes[0].Name, Is.EqualTo("BIP01 MUEHLE"));
			Assert.That(mdhNodes[0].ParentIndex, Is.EqualTo(-1));
			Assert.Multiple(() =>
			{
				Assert.That(mdhNodes[0].Transform.M11, Is.EqualTo(-1));
				Assert.That(mdhNodes[0].Transform.M21, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M31, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M41, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M12, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M22, Is.EqualTo(1));
				Assert.That(mdhNodes[0].Transform.M32, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M42, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M13, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M23, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M33, Is.EqualTo(-1));
				Assert.That(mdhNodes[0].Transform.M43, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M14, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M24, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M34, Is.EqualTo(0));
				Assert.That(mdhNodes[0].Transform.M44, Is.EqualTo(1));
			});

			var node = mdh.GetNode(1);
			Assert.That(node.Name, Is.EqualTo("BIP01 NABE"));
			Assert.That(node.ParentIndex, Is.EqualTo(0));
			Assert.Multiple(() =>
			{
				Assert.That(node.Transform.M11, Is.EqualTo(1));
				Assert.That(node.Transform.M21, Is.EqualTo(0));
				Assert.That(node.Transform.M31, Is.EqualTo(0));
				Assert.That(node.Transform.M41, Is.EqualTo(0));
				Assert.That(node.Transform.M12, Is.EqualTo(0));
				Assert.That(node.Transform.M22, Is.EqualTo(1));
				Assert.That(node.Transform.M32, Is.EqualTo(-0.0));
				Assert.That(node.Transform.M42, Is.EqualTo(0));
				Assert.That(node.Transform.M13, Is.EqualTo(0));
				Assert.That(node.Transform.M23, Is.EqualTo(0));
				Assert.That(node.Transform.M33, Is.EqualTo(1));
				Assert.That(node.Transform.M43, Is.EqualTo(0));
				Assert.That(node.Transform.M14, Is.EqualTo(0));
				Assert.That(node.Transform.M24, Is.EqualTo(0));
				Assert.That(node.Transform.M34, Is.EqualTo(-394.040466f));
				Assert.That(node.Transform.M44, Is.EqualTo(1));
			});
        
			Assert.Multiple(() =>
			{
				Assert.That(mdh.BoundingBox.Min.X, Is.EqualTo(-497.17572f));
				Assert.That(mdh.BoundingBox.Min.Y, Is.EqualTo(-0.575592041f));
				Assert.That(mdh.BoundingBox.Min.Z, Is.EqualTo(-105.896698f));
				Assert.That(mdh.BoundingBox.Max.X, Is.EqualTo(515.717346f));
				Assert.That(mdh.BoundingBox.Max.Y, Is.EqualTo(364.943878f));
				Assert.That(mdh.BoundingBox.Max.Z, Is.EqualTo(893.536743f));
			});

			Assert.Multiple(() =>
			{
				Assert.That(mdh.CollisionBoundingBox.Min.X, Is.EqualTo(-248.58786f));
				Assert.That(mdh.CollisionBoundingBox.Min.Y, Is.EqualTo(-0.402914435f));
				Assert.That(mdh.CollisionBoundingBox.Min.Z, Is.EqualTo(-52.948349f));
				Assert.That(mdh.CollisionBoundingBox.Max.X, Is.EqualTo(257.858673f));
				Assert.That(mdh.CollisionBoundingBox.Max.Y, Is.EqualTo(291.955109f));
				Assert.That(mdh.CollisionBoundingBox.Max.Z, Is.EqualTo(446.768372f));
			});

			Assert.Multiple(() =>
			{
				Assert.That(mdh.RootTranslation.X, Is.EqualTo(0));
				Assert.That(mdh.RootTranslation.Y, Is.EqualTo(0));
				Assert.That(mdh.RootTranslation.Z, Is.EqualTo(-394.040466f));
			});
        
			Assert.That(mdh.Checksum, Is.EqualTo(965956401));
			Assert.That(mdh.SourcePath, Is.EqualTo(@"\_WORK\DATA\ANIMS\\_WORK\DATA\ANIMS\ASC_MOBSI\ANIMATED\STONEMILL_OM.ASC"));
		}
	}
}