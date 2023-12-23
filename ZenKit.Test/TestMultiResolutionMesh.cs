using System;
using System.Numerics;
using NUnit.Framework;

namespace ZenKit.Test;

public class TestMultiResolutionMesh
{
	[OneTimeSetUp]
	public void SetUp()
	{
		Logger.Set(LogLevel.Trace,
			(level, name, message) =>
				Console.WriteLine(new DateTime() + " [ZenKit] (" + level + ") > " + name + ": " + message));
	}

	private void CheckVec3(Vector3 v, float x, float y, float z)
	{
		Assert.That(v.X, Is.EqualTo(x));
		Assert.That(v.Y, Is.EqualTo(y));
		Assert.That(v.Z, Is.EqualTo(z));
	}

	private void CheckTriangle(MeshTriangle v, ushort a, ushort b, ushort c)
	{
		Assert.That(v.Wedge0, Is.EqualTo(a));
		Assert.That(v.Wedge1, Is.EqualTo(b));
		Assert.That(v.Wedge2, Is.EqualTo(c));
	}

	private void CheckWedge(MeshWedge v, float nx, float ny, float nz, float tx, float ty, ushort i)
	{
		Assert.That(v.Normal.X, Is.EqualTo(nx));
		Assert.That(v.Normal.Y, Is.EqualTo(ny));
		Assert.That(v.Normal.Z, Is.EqualTo(nz));
		Assert.That(v.Texture.X, Is.EqualTo(tx));
		Assert.That(v.Texture.Y, Is.EqualTo(ty));
		Assert.That(v.Index, Is.EqualTo(i));
	}

	private void CheckPlane(MeshPlane v, float d, float nx, float ny, float nz)
	{
		Assert.That(v.Distance, Is.EqualTo(d));
		Assert.That(v.Normal.X, Is.EqualTo(nx));
		Assert.That(v.Normal.Y, Is.EqualTo(ny));
		Assert.That(v.Normal.Z, Is.EqualTo(nz));
	}

	[Test]
	public void TestLoad()
	{
		var mrm = new MultiResolutionMesh("./Samples/mesh0.mrm");

		var positions = mrm.Positions;
		Assert.That(positions, Has.Count.EqualTo(8));
		Assert.Multiple(() => CheckVec3(positions[0], 200, 398.503906f, 200));
		Assert.Multiple(() => CheckVec3(positions[1], -200, 398.503906f, 200));
		Assert.Multiple(() => CheckVec3(positions[7], -200, 0, -200));

		var normals = mrm.Normals;
		Assert.That(normals, Has.Count.EqualTo(0));

		Assert.That(mrm.AlphaTest, Is.EqualTo(true));
		Assert.Multiple(() => CheckVec3(mrm.BoundingBox.Min, -200, 0, -200));
		Assert.Multiple(() => CheckVec3(mrm.BoundingBox.Max, 200, 398.503906f, 200));

		var subMeshes = mrm.SubMeshes;
		Assert.That(subMeshes, Has.Count.EqualTo(1));

		var subMesh = subMeshes[0];
		Assert.That(subMesh.Material.Name, Is.EqualTo("EVT_TPL_GITTERKAEFIG_01"));
		Assert.That(subMesh.Material.Texture, Is.EqualTo("OCODFLGATELI.TGA"));
		Assert.That(subMesh.Colors, Has.Count.EqualTo(0));
		Assert.That(subMesh.TriangleEdges, Has.Count.EqualTo(0));
		Assert.That(subMesh.EdgeScores, Has.Count.EqualTo(0));
		Assert.That(subMesh.Edges, Has.Count.EqualTo(0));

		var triangles = subMesh.Triangles;
		Assert.That(triangles, Has.Count.EqualTo(16));
		Assert.Multiple(() => CheckTriangle(triangles[0], 26, 19, 12));
		Assert.Multiple(() => CheckTriangle(triangles[1], 8, 13, 18));
		Assert.Multiple(() => CheckTriangle(triangles[14], 2, 6, 29));
		Assert.Multiple(() => CheckTriangle(triangles[15], 28, 20, 3));

		var wedges = subMesh.Wedges;
		Assert.That(wedges, Has.Count.EqualTo(32));
		Assert.Multiple(() => CheckWedge(wedges[0], 0, 0, -1, -1.50000048f, -1.49251938f, 4));
		Assert.Multiple(() => CheckWedge(wedges[1], -1, 0, 0, 2.49999952f, -1.49251938f, 4));
		Assert.Multiple(() => CheckWedge(wedges[31], 0, 0, -1, -1.50000048f, 2.49251938f, 7));

		var tpi = subMesh.TrianglePlaneIndices;
		Assert.That(tpi, Has.Count.EqualTo(16));
		Assert.That(tpi[0], Is.EqualTo(0));
		Assert.That(tpi[1], Is.EqualTo(1));
		Assert.That(tpi[14], Is.EqualTo(7));
		Assert.That(tpi[15], Is.EqualTo(5));

		var tp = subMesh.TrianglePlanes;
		Assert.That(tp, Has.Count.EqualTo(8));
		Assert.Multiple(() => CheckPlane(tp[0], 200, 1, 0, 0));
		Assert.Multiple(() => CheckPlane(tp[1], 200, 0, 0, 1));
		Assert.Multiple(() => CheckPlane(tp[6], 200, 0, 0, -1));
		Assert.Multiple(() => CheckPlane(tp[7], -200, 0, 0, 1));

		var wedgeMap = subMesh.WedgeMap;
		Assert.That(wedgeMap, Has.Count.EqualTo(32));
		Assert.That(wedgeMap[0], Is.EqualTo(65535));
		Assert.That(wedgeMap[1], Is.EqualTo(65535));
		Assert.That(wedgeMap[29], Is.EqualTo(2));
		Assert.That(wedgeMap[30], Is.EqualTo(1));
		Assert.That(wedgeMap[31], Is.EqualTo(0));
	}
}