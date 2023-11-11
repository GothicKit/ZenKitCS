using System;
using System.Numerics;
using NUnit.Framework;

namespace ZenKit.Test
{
	public class TestModelMesh
	{
		[OneTimeSetUp]
		public void SetUp()
		{
			Logger.Set(LogLevel.Trace,
				(level, name, message) =>
					Console.WriteLine(new DateTime() + " [ZenKit] (" + level + ") > " + name + ": " + message));
		}

		private static void CheckVec3(Vector3 v, float x, float y, float z)
		{
			Assert.That(v.X, Is.EqualTo(x));
			Assert.That(v.Y, Is.EqualTo(y));
			Assert.That(v.Z, Is.EqualTo(z));
		}

		[Test]
		public void TestLoadAttachments()
		{
			var mdm = new ModelMesh("./Samples/secretdoor.mdm");
			var attachments = mdm.Attachments;

			Assert.That(attachments, Has.Count.EqualTo(1));
			Assert.That(mdm.AttachmentCount, Is.EqualTo(1));

			Assert.That(attachments.ContainsKey("BIP01 DOOR"), Is.Not.EqualTo(null));
			Assert.That(attachments["BIP01 DOOR"].Positions, Has.Length.EqualTo(8));

			var fromNative = mdm.GetAttachment("BIP01 DOOR");
			Assert.That(fromNative, Is.Not.EqualTo(null));
			Assert.That(fromNative!.Positions, Has.Length.EqualTo(8));
		}

		[Test]
		public void TestLoad()
		{
			var mdm = new ModelMesh("./Samples/smoke_waterpipe.mdm");

			var meshes = mdm.Meshes;
			Assert.That(meshes, Has.Count.EqualTo(1));

			var rawMesh = meshes[0].Mesh;
			Assert.That(rawMesh.Positions, Has.Length.EqualTo(115));
			Assert.That(rawMesh.Normals, Has.Length.EqualTo(115));
			Assert.That(rawMesh.Materials, Has.Count.EqualTo(1));
			Assert.That(rawMesh.SubMeshes, Has.Count.EqualTo(1));

			var weights = meshes[0].Weights;
			Assert.That(weights, Has.Count.EqualTo(115));
			Assert.That(weights[0], Has.Length.EqualTo(1));
			Assert.That(weights[0][0].Weight, Is.EqualTo(1.0f));
			Assert.Multiple(() => CheckVec3(weights[0][0].Position, -5.49776077f, 35.086731f, -2.64756012f));
			Assert.That(weights[0][0].NodeIndex, Is.EqualTo(0));
		
			Assert.That(weights[62], Has.Length.EqualTo(1));
			Assert.That(weights[62][0].Weight, Is.EqualTo(1.0f));
			Assert.Multiple(() => CheckVec3(weights[62][0].Position, 0.260997772f, 18.0412712f, -23.9048882f));
			Assert.That(weights[62][0].NodeIndex, Is.EqualTo(4));
		
			Assert.That(weights[114], Has.Length.EqualTo(1));
			Assert.That(weights[114][0].Weight, Is.EqualTo(1.0f));
			Assert.Multiple(() => CheckVec3(weights[114][0].Position, 1.05304337f, 71.0284958f, 1.32049942f));
			Assert.That(weights[114][0].NodeIndex, Is.EqualTo(0));
		
			Assert.That(meshes[0].WedgeNormals, Has.Length.EqualTo(0));

			var nodes = meshes[0].Nodes;
			Assert.That(nodes, Has.Length.EqualTo(6));
			Assert.That(nodes[0], Is.EqualTo(0));
			Assert.That(nodes[1], Is.EqualTo(5));
			Assert.That(nodes[2], Is.EqualTo(3));

			var bboxes = meshes[0].BoundingBoxes;
			Assert.That(bboxes, Has.Count.EqualTo(6));
			Assert.Multiple(() => CheckVec3(bboxes[0].Center, 0.612892151f, 41.7827187f, 0.705307007f));
			Assert.Multiple(() => CheckVec3(bboxes[0].HalfWidth, 15.2073612f, 33.4261742f, 14.8513918f));
			Assert.Multiple(() => CheckVec3(bboxes[0].Axes[0], 0.777145922f, 0, -0.629320442f));
			Assert.Multiple(() => CheckVec3(bboxes[0].Axes[1], 0, 1, 0));
			Assert.Multiple(() => CheckVec3(bboxes[0].Axes[2], 0.629320442f, 0, 0.777145922f));
			Assert.That(bboxes[0].Children, Has.Count.EqualTo(0));
		}
	
	}
}