using System.Numerics;
using NUnit.Framework;

namespace ZenKit.Test;

public class TestWorld
{
	private void CheckVec2(Vector2 v, float x, float y)
	{
		Assert.That(v.X, Is.EqualTo(x));
		Assert.That(v.Y, Is.EqualTo(y));
	}

	private void CheckVec3(Vector3 v, float x, float y, float z)
	{
		Assert.That(v.X, Is.EqualTo(x));
		Assert.That(v.Y, Is.EqualTo(y));
		Assert.That(v.Z, Is.EqualTo(z));
	}

	[Test]
	[Category("Proprietary")]
	public void TestLoad()
	{
		_ = new World("./Samples/world.proprietary.zen", GameVersion.Gothic1);
		var wld = new World("./Samples/world.proprietary.zen");

		var mesh = wld.Mesh;
		Assert.That(mesh.Positions, Has.Count.EqualTo(55439));
		Assert.That(mesh.Features, Has.Count.EqualTo(419936));
		Assert.That(mesh.Materials, Has.Count.EqualTo(2263));
		Assert.That(mesh.Name, Is.EqualTo(""));

		var box0 = mesh.BoundingBox;
		Assert.Multiple(() => CheckVec3(box0.Min, 0, 0, 0));
		Assert.Multiple(() => CheckVec3(box0.Max, 0, 0, 0));

		var obb = mesh.OrientedBoundingBox;
		Assert.Multiple(() => CheckVec3(obb.Center, 0, 0, 0));
		Assert.Multiple(() => CheckVec3(obb.Axes.Item1, 0, 0, 0));
		Assert.Multiple(() => CheckVec3(obb.Axes.Item2, 0, 0, 0));
		Assert.Multiple(() => CheckVec3(obb.Axes.Item3, 0, 0, 0));
		Assert.Multiple(() => CheckVec3(obb.HalfWidth, 0, 0, 0));

		var verts = mesh.Positions;
		Assert.Multiple(() => CheckVec3(verts[0], 91365, -4026.60083f, 46900));
		Assert.Multiple(() => CheckVec3(verts[1], 92900, -4029.99976f, 38399.9961f));
		Assert.Multiple(() => CheckVec3(verts[500], 44263.8203f, 708.517822f, 6841.18262f));
		Assert.Multiple(() => CheckVec3(verts[501], 45672.6094f, 640.436157f, 6877.81543f));

		var feats = mesh.Features;
		Assert.Multiple(() => CheckVec2(feats[0].Texture, 1.11193848f, 2.64415169f));
		Assert.Multiple(() => CheckVec3(feats[0].Normal, 0.0000220107158f, 1, -0.000121058853f));
		Assert.That(feats[0].Light, Is.EqualTo(4292927712));

		Assert.Multiple(() => CheckVec2(feats[1].Texture, -0.371101379f, -0.909111022f));
		Assert.Multiple(() => CheckVec3(feats[1].Normal, 0.0000251403726f, 1, -0.000138271935f));
		Assert.That(feats[1].Light, Is.EqualTo(4292927712));

		Assert.Multiple(() => CheckVec2(feats[500].Texture, -0.524971008f, 2.59478664f));
		Assert.Multiple(() => CheckVec3(feats[500].Normal, -0.000046945388f, -0.99999994f, 0.000258200336f));
		Assert.That(feats[500].Light, Is.EqualTo(4281084972));

		Assert.Multiple(() => CheckVec2(feats[501].Texture, 1.93376923f, -0.734043121f));
		Assert.Multiple(() => CheckVec3(feats[501].Normal, 0.000102534526f, -1, -0.00014051389f));
		Assert.That(feats[501].Light, Is.EqualTo(4281084972));

		var mats = mesh.Materials;
		var mat0 = mats[0];
		var mat500 = mats[500];

		Assert.That(mat0.Name, Is.EqualTo("OWODWATSTOP"));
		Assert.That(mat0.Group, Is.EqualTo(MaterialGroup.Water));
		Assert.That(mat0.Texture, Is.EqualTo("OWODSEA_A0.TGA"));

		Assert.That(mat500.Name, Is.EqualTo("OMWABROWNGREEN01"));
		Assert.That(mat500.Group, Is.EqualTo(MaterialGroup.Stone));
		Assert.That(mat500.Texture, Is.EqualTo("OMWABROWNGREEN01.TGA"));

		// Check the BSP tree
		var tree = wld.BspTree;
		Assert.That(tree.Type, Is.EqualTo(BspTreeType.Outdoor));

		var treePolys = tree.PolygonIndices;
		Assert.That(treePolys, Has.Count.EqualTo(480135));
		Assert.That(treePolys[0], Is.EqualTo(0));
		Assert.That(treePolys[1], Is.EqualTo(1));
		Assert.That(treePolys[2], Is.EqualTo(2));
		Assert.That(treePolys[150], Is.EqualTo(102));
		Assert.That(treePolys[151], Is.EqualTo(103));
		Assert.That(treePolys[152], Is.EqualTo(92));

		var nodes = tree.Nodes;
		Assert.That(nodes, Has.Count.EqualTo(6644));
		Assert.That(nodes[0].Plane.X, Is.EqualTo(1));
		Assert.That(nodes[0].Plane.Y, Is.EqualTo(0));
		Assert.That(nodes[0].Plane.Z, Is.EqualTo(0));
		Assert.That(nodes[0].Plane.W, Is.EqualTo(18540.0156f));
		Assert.That(nodes[0].FrontIndex, Is.EqualTo(1));
		Assert.That(nodes[0].ParentIndex, Is.EqualTo(-1));
		Assert.That(nodes[0].BackIndex, Is.EqualTo(1599));
		Assert.That(nodes[0].PolygonIndex, Is.EqualTo(0));
		Assert.That(nodes[0].PolygonCount, Is.EqualTo(0));
		Assert.Multiple(() => CheckVec3(nodes[0].BoundingBox.Min, -71919.9609f, -12000, -59900));
		Assert.Multiple(() => CheckVec3(nodes[0].BoundingBox.Max, 108999.992f, 20014.0371f, 67399.9921f));

		Assert.That(nodes[1].Plane.X, Is.EqualTo(0));
		Assert.That(nodes[1].Plane.Y, Is.EqualTo(0));
		Assert.That(nodes[1].Plane.Z, Is.EqualTo(1));
		Assert.That(nodes[1].Plane.W, Is.EqualTo(3749.99609f));
		Assert.That(nodes[1].FrontIndex, Is.EqualTo(2));
		Assert.That(nodes[1].ParentIndex, Is.EqualTo(0));
		Assert.That(nodes[1].BackIndex, Is.EqualTo(445));
		Assert.That(nodes[1].PolygonIndex, Is.EqualTo(0));
		Assert.That(nodes[1].PolygonCount, Is.EqualTo(0));
		Assert.Multiple(() => CheckVec3(nodes[1].BoundingBox.Min, 15499.999f, -12000, -59900));
		Assert.Multiple(() => CheckVec3(nodes[1].BoundingBox.Max, 108999.992f, 19502.1973f, 67399.9921f));

		var leaves = tree.LeafNodeIndices;
		Assert.That(leaves, Has.Count.EqualTo(3318));
		Assert.That(leaves[0], Is.EqualTo(5));
		Assert.That(leaves[10], Is.EqualTo(26));

		var sectors = tree.Sectors;
		Assert.That(sectors, Has.Count.EqualTo(299));

		Assert.That(sectors[0].Name, Is.EqualTo("WALD11"));
		Assert.That(sectors[0].NodeIndices, Has.Count.EqualTo(9));
		Assert.That(sectors[0].PortalPolygonIndices, Has.Count.EqualTo(24));

		Assert.That(sectors[50].Name, Is.EqualTo("OWCAVE01"));
		Assert.That(sectors[50].NodeIndices, Has.Count.EqualTo(4));
		Assert.That(sectors[50].PortalPolygonIndices, Has.Count.EqualTo(2));

		var portalPolys = tree.PortalPolygonIndices;
		Assert.That(portalPolys, Has.Count.EqualTo(0));

		Assert.That(tree.LightPoints, Has.Count.EqualTo(3318));
		Assert.Multiple(() => CheckVec3(tree.LightPoints[0], -99, -99, -99));

		// TODO(lmichaelis): Check the VOb tree
		/*
		auto& vobs = wld.world_vobs;
		Assert.That(vobs.size(), 14);

		auto& vob0 = vobs[0];
		auto& vob13 = vobs[13];

		{
			box0 = vob0->bbox;
			Assert.That(box0.min, -71919.9609, -13091.8232, -59900);
			Assert.That(box0.max, 108999.992, 20014.0352, 67399.9921);

			auto mat = vob0->rotation;
			Assert.That(mat[0][0], 1.0f);
			Assert.That(mat[1][0], 0.0f);
			Assert.That(mat[2][0], 0.0f);
			Assert.That(mat[0][1], 0.0f);
			Assert.That(mat[1][1], 1.0f);
			Assert.That(mat[2][1], 0.0f);
			Assert.That(mat[0][2], 0.0f);
			Assert.That(mat[1][2], 0.0f);
			Assert.That(mat[2][2], 1.0f);

			Assert.That(vob0->vob_name, "LEVEL-VOB");
			Assert.That(vob0->visual_name, "SURFACE.3DS");
			CHECK(vob0->preset_name.empty());
			Assert.That(vob0->position, 0, 0, 0);
			CHECK_FALSE(vob0->show_visual);
			Assert.That(vob0->sprite_camera_facing_mode, zenkit::SpriteAlignment::NONE);
			Assert.That(vob0->anim_mode, zenkit::AnimationType::NONE);
			Assert.That(vob0->anim_strength, 0.0f);
			Assert.That(vob0->far_clip_scale, 0.0f);
			CHECK(vob0->cd_static);
			CHECK_FALSE(vob0->cd_dynamic);
			CHECK_FALSE(vob0->vob_static);
			Assert.That(vob0->dynamic_shadows, zenkit::ShadowType::NONE);
			Assert.That(vob0->bias, 0);
			CHECK_FALSE(vob0->ambient);
			CHECK_FALSE(vob0->physics_enabled);

			auto& children = vob0->children;
			Assert.That(children.size(), 7496);

			auto& child1 = children[0];

			{
				auto box1 = child1->bbox;
				Assert.That(box1.min, -18596.9004, -161.17189, 4091.1333);
				Assert.That(box1.max, -18492.0723, -111.171906, 4191.26221);

				auto matc = child1->rotation;
				Assert.That(matc[0][0], -0.779196978f);
				Assert.That(matc[1][0], 0.0f);
				Assert.That(matc[2][0], 0.626779079f);

				Assert.That(matc[0][1], 0.0f);
				Assert.That(matc[1][1], 1.0f);
				Assert.That(matc[2][1], 0.0f);

				Assert.That(matc[0][2], -0.626779079f);
				Assert.That(matc[1][2], 0.0f);
				Assert.That(matc[2][2], -0.779196978f);

				Assert.That(child1->vob_name, "FP_CAMPFIRE_PATH_BANDITOS2_03_02");
				CHECK(child1->visual_name.empty());
				CHECK(child1->preset_name.empty());
				Assert.That(child1->position, -18544.4863, -136.171906, 4141.19727);
				CHECK_FALSE(child1->show_visual);
				Assert.That(child1->sprite_camera_facing_mode, zenkit::SpriteAlignment::NONE);
				Assert.That(child1->anim_mode, zenkit::AnimationType::NONE);
				Assert.That(child1->anim_strength, 0.0f);
				Assert.That(child1->far_clip_scale, 0.0f);
				CHECK_FALSE(child1->cd_static);
				CHECK_FALSE(child1->cd_dynamic);
				CHECK_FALSE(child1->vob_static);
				Assert.That(child1->dynamic_shadows, zenkit::ShadowType::NONE);
				Assert.That(child1->bias, 0);
				CHECK_FALSE(child1->ambient);
				CHECK_FALSE(child1->physics_enabled);

				CHECK(child1->children.empty());
			}
		}

		{
			auto box2 = vob13->bbox;
			Assert.That(box2.min, -9999.40234, -10000.0039, -9200);
			Assert.That(box2.max, 9060.59765, 5909.90039, 7537.47461);

			auto mat = vob13->rotation;
			Assert.That(mat[0][0], 1.0f);
			Assert.That(mat[1][0], 0.0f);
			Assert.That(mat[2][0], 0.0f);
			Assert.That(mat[0][1], 0.0f);
			Assert.That(mat[1][1], 1.0f);
			Assert.That(mat[2][1], 0.0f);
			Assert.That(mat[0][2], 0.0f);
			Assert.That(mat[1][2], 0.0f);
			Assert.That(mat[2][2], 1.0f);

			Assert.That(vob13->vob_name, "LEVEL-VOB");
			Assert.That(vob13->visual_name, "OLDCAMP.3DS");
			CHECK(vob13->preset_name.empty());
			Assert.That(vob13->position, 0, 0, 0);
			CHECK_FALSE(vob13->show_visual);
			Assert.That(vob13->sprite_camera_facing_mode, zenkit::SpriteAlignment::NONE);
			Assert.That(vob13->anim_mode, zenkit::AnimationType::NONE);
			Assert.That(vob13->anim_strength, 0.0f);
			Assert.That(vob13->far_clip_scale, 0.0f);
			CHECK_FALSE(vob13->cd_static);
			CHECK_FALSE(vob13->cd_dynamic);
			CHECK_FALSE(vob13->vob_static);
			Assert.That(vob13->dynamic_shadows, zenkit::ShadowType::NONE);
			Assert.That(vob13->bias, 0);
			CHECK_FALSE(vob13->ambient);
			CHECK_FALSE(vob13->physics_enabled);

			auto& children = vob13->children;
			Assert.That(children.size(), 3250);
		}
		*/

		// Check the waynet

		var waynet = wld.WayNet;
		var points = waynet.Points;
		Assert.That(points, Has.Count.EqualTo(2784));
		Assert.That(waynet.Edges, Has.Count.EqualTo(3500));

		var wp0 = points[0];
		var wp100 = points[100];

		Assert.That(wp0.Name, Is.EqualTo("LOCATION_28_07"));
		Assert.That(wp0.WaterDepth, Is.EqualTo(0));
		Assert.That(wp0.UnderWater, Is.False);
		Assert.Multiple(() => CheckVec3(wp0.Position, 23871.457f, -553.283813f, 27821.3516f));
		Assert.Multiple(() => CheckVec3(wp0.Direction, 0.86651814f, 0, -0.499145567f));
		Assert.That(wp0.FreePoint, Is.True);

		Assert.That(wp100.Name, Is.EqualTo("CASTLE_MOVEMENT_STRAIGHT3"));
		Assert.That(wp100.WaterDepth, Is.EqualTo(0));
		Assert.That(wp100.UnderWater, Is.False);
		Assert.Multiple(() => CheckVec3(wp100.Position, 3362.21948f, 8275.1709f, -21067.9473f));
		Assert.Multiple(() => CheckVec3(wp100.Direction, -0.342115372f, 0, 0.939657927f));
		Assert.That(wp100.FreePoint, Is.False);


		var edges = waynet.Edges;
		var edge0 = edges[0];
		var edge5 = edges[5];
		var edge100 = edges[100];
		var edge500 = edges[500];

		Assert.That(edge0.A, Is.EqualTo(20));
		Assert.That(edge0.B, Is.EqualTo(21));

		// edge 6 is a reference
		Assert.That(edge5.A, Is.EqualTo(28));
		Assert.That(edge5.B, Is.EqualTo(30));

		Assert.That(edge100.A, Is.EqualTo(123));
		Assert.That(edge100.B, Is.EqualTo(126));

		Assert.That(edge500.A, Is.EqualTo(521));
		Assert.That(edge500.B, Is.EqualTo(515));
	}
}