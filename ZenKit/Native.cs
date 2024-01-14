using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using ZenKit.Daedalus;
using ZenKit.Util;
using ZenKit.Vobs;
using static ZenKit.Native.Callbacks;
using static ZenKit.Native.Structs;

namespace ZenKit
{
	public enum StringEncoding
	{
		CentralEurope = 1250,
		EastEurope = 1251,
		WestEurope = 1252
	}

	public static class StringEncodingController
	{
		private static Encoding _encoding;

		static StringEncodingController()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			_encoding = Encoding.GetEncoding(1252);
		}

		public static void SetEncoding(StringEncoding coding)
		{
			_encoding = Encoding.GetEncoding((int)coding);
		}

		internal static string Decode(byte[] bytes)
		{
			return _encoding.GetString(bytes);
		}
	}

	internal static class Marshalling
	{
		public static string? MarshalAsString(this IntPtr ptr)
		{
			if (ptr == IntPtr.Zero) return null;

			uint length = 0;
			while (Marshal.ReadByte(ptr, (int)length) != 0) length += 1;
			return StringEncodingController.Decode(ptr.MarshalAsArray<byte>(length));
		}

		public static T[] MarshalAsArray<T>(this IntPtr ptr, ulong size)
		{
			if (ptr == IntPtr.Zero) return Array.Empty<T>();

			var array = new T[size];
			switch (array)
			{
				case byte[] _:
				case sbyte[] _:
					Marshal.Copy(ptr, (byte[])(object)array, 0, (int)size);
					break;
				case short[] _:
				case ushort[] _:
					Marshal.Copy(ptr, (short[])(object)array, 0, (int)size);
					break;
				case int[] _:
				case uint[] _:
					Marshal.Copy(ptr, (int[])(object)array, 0, (int)size);
					break;
				case long[] _:
				case ulong[] _:
					Marshal.Copy(ptr, (long[])(object)array, 0, (int)size);
					break;
				case float[] _:
					Marshal.Copy(ptr, (float[])(object)array, 0, (int)size);
					break;
				default:
					var sizeofT = Marshal.SizeOf<T>();

					for (var i = 0; i < (int)size; ++i)
					{
						var val = Marshal.PtrToStructure<T>(new IntPtr(ptr.ToInt64() + sizeofT * i));
						if (val == null) throw new ArgumentException("PtrToStructure is null?");

						array[i] = val;
					}

					break;
			}

			return array;
		}

		public static List<T> MarshalAsList<T>(this IntPtr ptr, ulong size)
		{
			return MarshalAsArray<T>(ptr, size).ToList();
		}
	}

	internal static class Native
	{
		private const string DllName = "zenkitcapi";

		[DllImport(DllName)]
		public static extern UIntPtr ZkObject_takeRef(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimation_getFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimation_getSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLogger_set(LogLevel lvl, ZkLogger logger, IntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkLogger_setDefault(LogLevel lvl);

		[DllImport(DllName)]
		public static extern void ZkLogger_log(LogLevel lvl, string name, string message);

		[DllImport(DllName)]
		public static extern UIntPtr ZkRead_newFile(UIntPtr stream);

		[DllImport(DllName)]
		public static extern UIntPtr ZkRead_newMem([MarshalAs(UnmanagedType.LPArray)] byte[] bytes, ulong length);

		[DllImport(DllName)]
		public static extern UIntPtr ZkRead_newPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkRead_newExt(ZkReadExt ext, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkRead_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkRead_getSize(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkRead_getBytes(UIntPtr slf, byte[] buf, ulong length);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfs_new();

		[DllImport(DllName)]
		public static extern void ZkVfs_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfs_getRoot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfs_mkdir(UIntPtr slf, string path);

		[DllImport(DllName)]
		public static extern bool ZkVfs_remove(UIntPtr slf, string path);

		[DllImport(DllName)]
		public static extern bool ZkVfs_mount(UIntPtr slf, UIntPtr node, string parent, VfsOverwriteBehavior overwrite);

		[DllImport(DllName)]
		public static extern bool ZkVfs_mountHost(UIntPtr slf, string path, string parent,
			VfsOverwriteBehavior overwrite);

		[DllImport(DllName)]
		public static extern bool ZkVfs_mountDisk(UIntPtr slf, UIntPtr buf, VfsOverwriteBehavior overwrite);

		[DllImport(DllName)]
		public static extern bool ZkVfs_mountDiskHost(UIntPtr slf, string path, VfsOverwriteBehavior overwrite);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfs_resolvePath(UIntPtr slf, string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfs_findNode(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfsNode_newFile(string name, byte[] data, ulong size, ulong ts);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfsNode_newDir(string name, ulong ts);

		[DllImport(DllName)]
		public static extern void ZkVfsNode_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkVfsNode_isFile(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkVfsNode_isDir(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkVfsNode_getTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkVfsNode_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfsNode_getChild(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfsNode_create(UIntPtr slf, UIntPtr node);

		[DllImport(DllName)]
		public static extern bool ZkVfsNode_remove(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVfsNode_open(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVfsNode_enumerateChildren(UIntPtr slf, ZkVfsNodeEnumerator callback, IntPtr ctx);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneLibrary_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneLibrary_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneLibrary_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkCutsceneLibrary_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkCutsceneLibrary_getBlockCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneLibrary_getBlock(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneLibrary_getBlockByIndex(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkCutsceneLibrary_enumerateBlocks(UIntPtr slf, ZkCutsceneBlockEnumerator cb,
			IntPtr ctx);

		[DllImport(DllName)]
		public static extern IntPtr ZkCutsceneBlock_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneBlock_getMessage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkCutsceneMessage_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkCutsceneMessage_getText(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkCutsceneMessage_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkFont_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkFont_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkFont_loadVfs(UIntPtr buf, string name);

		[DllImport(DllName)]
		public static extern void ZkFont_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkFont_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkFont_getHeight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkFont_getGlyphCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern FontGlyph ZkFont_getGlyph(UIntPtr slf, [MarshalAs(UnmanagedType.U8)] ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelAnimation_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelAnimation_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelAnimation_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkModelAnimation_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelAnimation_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelAnimation_getNext(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkModelAnimation_getLayer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkModelAnimation_getFrameCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkModelAnimation_getNodeCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkModelAnimation_getFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkModelAnimation_getFpsSource(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AxisAlignedBoundingBox ZkModelAnimation_getBbox(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkModelAnimation_getChecksum(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelAnimation_getSourcePath(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ZkDate ZkModelAnimation_getSourceDate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelAnimation_getSourceScript(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelAnimation_getSampleCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AnimationSample ZkModelAnimation_getSample(UIntPtr slf,
			[MarshalAs(UnmanagedType.U8)] ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelAnimation_getNodeIndices(UIntPtr slf, out ulong size);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelHierarchy_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelHierarchy_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelHierarchy_loadVfs(UIntPtr buf, string name);

		[DllImport(DllName)]
		public static extern void ZkModelHierarchy_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelHierarchy_getNodeCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ModelHierarchyNode ZkModelHierarchy_getNode(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern AxisAlignedBoundingBox ZkModelHierarchy_getBbox(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AxisAlignedBoundingBox ZkModelHierarchy_getCollisionBbox(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkModelHierarchy_getRootTranslation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkModelHierarchy_getChecksum(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ZkDate ZkModelHierarchy_getSourceDate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelHierarchy_getSourcePath(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkOrientedBoundingBox_getCenter(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkOrientedBoundingBox_getAxis(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern Vector3 ZkOrientedBoundingBox_getHalfWidth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkOrientedBoundingBox_getChildCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkOrientedBoundingBox_getChild(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern AxisAlignedBoundingBox ZkOrientedBoundingBox_toAabb(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMaterial_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMaterial_loadPath(string path);

		[DllImport(DllName)]
		public static extern void ZkMaterial_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMaterial_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MaterialGroup ZkMaterial_getGroup(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ZkColor ZkMaterial_getColor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMaterial_getSmoothAngle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMaterial_getTexture(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector2 ZkMaterial_getTextureScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMaterial_getTextureAnimationFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AnimationMapping ZkMaterial_getTextureAnimationMapping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector2 ZkMaterial_getTextureAnimationMappingDirection(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMaterial_getDisableCollision(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMaterial_getDisableLightmap(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMaterial_getDontCollapse(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMaterial_getDetailObject(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMaterial_getDetailObjectScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMaterial_getForceOccluder(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMaterial_getEnvironmentMapping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMaterial_getEnvironmentMappingStrength(UIntPtr slf);

		[DllImport(DllName)]
		public static extern WaveMode ZkMaterial_getWaveMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern WaveSpeed ZkMaterial_getWaveSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMaterial_getWaveAmplitude(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMaterial_getWaveGridSize(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMaterial_getIgnoreSun(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AlphaFunction ZkMaterial_getAlphaFunction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector2 ZkMaterial_getDefaultMapping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMultiResolutionMesh_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMultiResolutionMesh_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMultiResolutionMesh_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkMultiResolutionMesh_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkMultiResolutionMesh_getPositionCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkMultiResolutionMesh_getPosition(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern ulong ZkMultiResolutionMesh_getNormalCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkMultiResolutionMesh_getNormal(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern ulong ZkMultiResolutionMesh_getSubMeshCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMultiResolutionMesh_getSubMesh(UIntPtr slf, ulong i);


		[DllImport(DllName)]
		public static extern ulong ZkMultiResolutionMesh_getMaterialCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMultiResolutionMesh_getMaterial(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern bool ZkMultiResolutionMesh_getAlphaTest(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AxisAlignedBoundingBox ZkMultiResolutionMesh_getBbox(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMultiResolutionMesh_getOrientedBbox(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSubMesh_getMaterial(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getTriangles(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkSubMesh_getWedgeCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MeshWedge ZkSubMesh_getWedge(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getColors(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getTrianglePlaneIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkSubMesh_getTrianglePlaneCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MeshPlane ZkSubMesh_getTrianglePlane(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getTriangleEdges(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getEdges(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getEdgeScores(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getWedgeMap(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkSoftSkinMesh_getNodeCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSoftSkinMesh_getMesh(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSoftSkinMesh_getBbox(UIntPtr slf, ulong node);

		[DllImport(DllName)]
		public static extern ulong ZkSoftSkinMesh_getWeightTotal(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkSoftSkinMesh_getWeightCount(UIntPtr slf, ulong node);

		[DllImport(DllName)]
		public static extern SoftSkinWeightEntry ZkSoftSkinMesh_getWeight(UIntPtr slf, ulong node, ulong i);

		[DllImport(DllName)]
		public static extern ulong ZkSoftSkinMesh_getWedgeNormalCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern SoftSkinWedgeNormal ZkSoftSkinMesh_getWedgeNormal(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoftSkinMesh_getNodes(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelMesh_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelMesh_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelMesh_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkModelMesh_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelMesh_getMeshCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelMesh_getMesh(UIntPtr slf, ulong i);


		[DllImport(DllName)]
		public static extern ulong ZkModelMesh_getAttachmentCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelMesh_getAttachment(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern void ZkModelMesh_enumerateAttachments(UIntPtr slf, ZkAttachmentEnumerator cb,
			IntPtr ctx);

		[DllImport(DllName)]
		public static extern uint ZkModelMesh_getChecksum(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModel_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModel_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModel_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkModel_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModel_getHierarchy(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModel_getMesh(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTexture_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTexture_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTexture_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkTexture_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern TextureFormat ZkTexture_getFormat(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkTexture_getWidth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkTexture_getHeight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkTexture_getWidthMipmap(UIntPtr slf, ulong level);

		[DllImport(DllName)]
		public static extern uint ZkTexture_getHeightMipmap(UIntPtr slf, ulong level);

		[DllImport(DllName)]
		public static extern uint ZkTexture_getWidthRef(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkTexture_getHeightRef(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkTexture_getMipmapCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkTexture_getAverageColor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkTexture_getPaletteSize(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ZkColor ZkTexture_getPaletteItem(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkTexture_getMipmapRaw(UIntPtr slf, ulong level, out ulong size);

		[DllImport(DllName)]
		public static extern ulong ZkTexture_getMipmapRgba(UIntPtr slf, ulong level, byte[] buf, ulong size);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMorphMesh_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMorphMesh_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMorphMesh_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkMorphMesh_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMorphMesh_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMorphMesh_getMesh(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkMorphMesh_getMorphPositionCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkMorphMesh_getMorphPosition(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern ulong ZkMorphMesh_getAnimationCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMorphMesh_getAnimation(UIntPtr slf, ulong i);


		[DllImport(DllName)]
		public static extern ulong ZkMorphMesh_getSourceCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMorphMesh_getSource(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkMorphAnimation_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMorphAnimation_getLayer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMorphAnimation_getBlendIn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMorphAnimation_getBlendOut(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMorphAnimation_getDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMorphAnimation_getSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern byte ZkMorphAnimation_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkMorphAnimation_getFrameCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMorphAnimation_getVertices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkMorphAnimation_getSampleCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkMorphAnimation_getSample(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkMorphSource_getFileName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ZkDate ZkMorphSource_getFileDate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkMesh_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ZkDate ZkMesh_getSourceDate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMesh_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AxisAlignedBoundingBox ZkMesh_getBoundingBox(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_getOrientedBoundingBox(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkMesh_getMaterialCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_getMaterial(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern ulong ZkMesh_getPositionCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkMesh_getPosition(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern ulong ZkMesh_getVertexCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vertex ZkMesh_getVertex(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern ulong ZkMesh_getLightMapCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_getLightMap(UIntPtr slf, ulong i);


		[DllImport(DllName)]
		public static extern ulong ZkMesh_getPolygonCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_getPolygon(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkLightMap_getImage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkLightMap_getOrigin(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkLightMap_getNormal(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern uint ZkPolygon_getMaterialIndex(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkPolygon_getLightMapIndex(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkPolygon_getPositionIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkPolygon_getFeatureIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern bool ZkPolygon_getIsPortal(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkPolygon_getIsOccluder(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkPolygon_getIsSector(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkPolygon_getShouldRelight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkPolygon_getIsOutdoor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkPolygon_getIsGhostOccluder(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkPolygon_getIsDynamicallyLit(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkPolygon_getIsLod(UIntPtr slf);

		[DllImport(DllName)]
		public static extern byte ZkPolygon_getNormalAxis(UIntPtr slf);

		[DllImport(DllName)]
		public static extern short ZkPolygon_getSectorIndex(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelScript_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelScript_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelScript_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkModelScript_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelScript_getSkeletonName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkModelScript_getSkeletonMeshDisabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelScript_getMeshCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelScript_getDisabledAnimationsCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelScript_getAnimationCombineCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelScript_getAnimationBlendCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelScript_getAnimationAliasCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelScript_getModelTagCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelScript_getAnimationCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelScript_getDisabledAnimation(UIntPtr slf, long i);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelScript_getMesh(UIntPtr slf, long i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelScript_getAnimationCombine(UIntPtr slf, long i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelScript_getAnimationBlend(UIntPtr slf, long i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelScript_getAnimationAlias(UIntPtr slf, long i);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelScript_getModelTag(UIntPtr slf, long i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelScript_getAnimation(UIntPtr slf, long i);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimation_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkAnimation_getLayer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimation_getNext(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimation_getBlendIn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimation_getBlendOut(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAnimation_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimation_getModel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AnimationDirection ZkAnimation_getDirection(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAnimation_getFirstFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAnimation_getLastFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimation_getCollisionVolumeScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkAnimation_getEventTagCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkAnimation_getParticleEffectCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkAnimation_getParticleEffectStopCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkAnimation_getSoundEffectCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkAnimation_getSoundEffectGroundCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkAnimation_getMorphAnimationCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkAnimation_getCameraTremorCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimation_getEventTag(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimation_getParticleEffect(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimation_getParticleEffectStop(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimation_getSoundEffect(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimation_getSoundEffectGround(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimation_getMorphAnimation(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimation_getCameraTremor(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkEventTag_getFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern EventType ZkEventTag_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEventTag_getSlot(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkEventTag_getItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEventTag_getFrames(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern FightMode ZkEventTag_getFightMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkEventTag_getIsAttached(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventParticleEffect_getFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventParticleEffect_getIndex(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEventParticleEffect_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEventParticleEffect_getPosition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkEventParticleEffect_getIsAttached(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventParticleEffectStop_getFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventParticleEffectStop_getIndex(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventCameraTremor_getFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventCameraTremor_getField1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventCameraTremor_getField2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventCameraTremor_getField3(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventCameraTremor_getField4(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventSoundEffect_getFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEventSoundEffect_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEventSoundEffect_getRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkEventSoundEffect_getEmptySlot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEventSoundEffectGround_getFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEventSoundEffectGround_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEventSoundEffectGround_getRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkEventSoundEffectGround_getEmptySlot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMorphAnimation_getFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMorphAnimation_getAnimation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMorphAnimation_getNode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimationCombine_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkAnimationCombine_getLayer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimationCombine_getNext(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimationCombine_getBlendIn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimationCombine_getBlendOut(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkAnimationCombine_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimationCombine_getModel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAnimationCombine_getLastFrame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimationBlend_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimationBlend_getNext(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimationBlend_getBlendIn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimationBlend_getBlendOut(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimationAlias_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkAnimationAlias_getLayer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimationAlias_getNext(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimationAlias_getBlendIn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimationAlias_getBlendOut(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkAnimationAlias_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkAnimationAlias_getAlias(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AnimationDirection ZkAnimationAlias_getDirection(UIntPtr slf);

		[DllImport(DllName)]
		public static extern BspTreeType ZkBspTree_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkBspTree_getPolygonIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkBspTree_getLeafPolygonIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkBspTree_getPortalPolygonIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkBspTree_getLightPointCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkBspTree_getLightPoint(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkBspTree_getLeafNodeIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkBspTree_getNodeCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern BspNode ZkBspTree_getNode(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern ulong ZkBspTree_getSectorCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkBspTree_getSector(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkBspSector_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkBspSector_getNodeIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkBspSector_getPortalPolygonIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkWayNet_getEdges(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkWayNet_getPointCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkWayNet_getPoint(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkWayPoint_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkWayPoint_getWaterDepth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkWayPoint_getUnderWater(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkWayPoint_getPosition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkWayPoint_getDirection(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkWayPoint_getFreePoint(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkWorld_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkWorld_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkWorld_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkWorld_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkWorld_getMesh(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkWorld_getBspTree(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkWorld_getWayNet(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkWorld_getRootObjectCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkWorld_getRootObject(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVirtualObject_new(VirtualObjectType type);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVirtualObject_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVirtualObject_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern VirtualObjectType ZkVirtualObject_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkVirtualObject_getId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AxisAlignedBoundingBox ZkVirtualObject_getBbox(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setBbox(UIntPtr slf, AxisAlignedBoundingBox bbox);

		[DllImport(DllName)]
		public static extern Vector3 ZkVirtualObject_getPosition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setPosition(UIntPtr slf, Vector3 position);

		[DllImport(DllName)]
		public static extern ZkMat3x3 ZkVirtualObject_getRotation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setRotation(UIntPtr slf, ZkMat3x3 rotation);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getShowVisual(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setShowVisual(UIntPtr slf, bool showVisual);

		[DllImport(DllName)]
		public static extern SpriteAlignment ZkVirtualObject_getSpriteCameraFacingMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setSpriteCameraFacingMode(UIntPtr slf,
			SpriteAlignment spriteCameraFacingMode);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getCdStatic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setCdStatic(UIntPtr slf, bool cdStatic);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getCdDynamic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setCdDynamic(UIntPtr slf, bool cdDynamic);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getVobStatic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setVobStatic(UIntPtr slf, bool vobStatic);

		[DllImport(DllName)]
		public static extern ShadowType ZkVirtualObject_getDynamicShadows(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setDynamicShadows(UIntPtr slf, ShadowType dynamicShadows);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getPhysicsEnabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setPhysicsEnabled(UIntPtr slf, bool physicsEnabled);

		[DllImport(DllName)]
		public static extern AnimationType ZkVirtualObject_getAnimMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setAnimMode(UIntPtr slf, AnimationType animMode);

		[DllImport(DllName)]
		public static extern int ZkVirtualObject_getBias(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setBias(UIntPtr slf, int bias);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getAmbient(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setAmbient(UIntPtr slf, bool ambient);

		[DllImport(DllName)]
		public static extern float ZkVirtualObject_getAnimStrength(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setAnimStrength(UIntPtr slf, float animStrength);

		[DllImport(DllName)]
		public static extern float ZkVirtualObject_getFarClipScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setFarClipScale(UIntPtr slf, float farClipScale);

		[DllImport(DllName)]
		public static extern IntPtr ZkVirtualObject_getPresetName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setPresetName(UIntPtr slf, string presetName);

		[DllImport(DllName)]
		public static extern IntPtr ZkVirtualObject_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setName(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVirtualObject_getVisual(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setVisual(UIntPtr slf, UIntPtr visual);

		[DllImport(DllName)]
		public static extern ulong ZkVirtualObject_getChildCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVirtualObject_getChild(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern byte ZkVirtualObject_getSleepMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setSleepMode(UIntPtr slf, byte sleepMode);

		[DllImport(DllName)]
		public static extern float ZkVirtualObject_getNextOnTimer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setNextOnTimer(UIntPtr slf, float nextOnTimer);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVirtualObject_getAi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setAi(UIntPtr slf, UIntPtr ai);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVirtualObject_getEventManager(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_setEventManager(UIntPtr slf, UIntPtr em);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_addChild(UIntPtr slf, UIntPtr obj);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_removeChild(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_removeChildren(UIntPtr slf, ZkVirtualObjectEnumerator pred,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVisual_new(VisualType type);

		[DllImport(DllName)]
		public static extern void ZkVisual_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkVisual_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVisual_setName(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern VisualType ZkVisual_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkVisualDecal_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVisualDecal_setName(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern Vector2 ZkVisualDecal_getDimension(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVisualDecal_setDimension(UIntPtr slf, Vector2 dimension);

		[DllImport(DllName)]
		public static extern Vector2 ZkVisualDecal_getOffset(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVisualDecal_setOffset(UIntPtr slf, Vector2 offset);

		[DllImport(DllName)]
		public static extern bool ZkVisualDecal_getTwoSided(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVisualDecal_setTwoSided(UIntPtr slf, bool twoSided);

		[DllImport(DllName)]
		public static extern AlphaFunction ZkVisualDecal_getAlphaFunc(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVisualDecal_setAlphaFunc(UIntPtr slf, AlphaFunction alphaFunc);

		[DllImport(DllName)]
		public static extern float ZkVisualDecal_getTextureAnimFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVisualDecal_setTextureAnimFps(UIntPtr slf, float textureAnimFps);

		[DllImport(DllName)]
		public static extern byte ZkVisualDecal_getAlphaWeight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVisualDecal_setAlphaWeight(UIntPtr slf, byte alphaWeight);

		[DllImport(DllName)]
		public static extern bool ZkVisualDecal_getIgnoreDaylight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkVisualDecal_setIgnoreDaylight(UIntPtr slf, bool ignoreDaylight);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAi_new(AiType type);

		[DllImport(DllName)]
		public static extern void ZkAi_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AiType ZkAi_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAiHuman_getWaterLevel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAiHuman_getFloorY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAiHuman_getWaterY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAiHuman_getCeilY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAiHuman_getFeetY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAiHuman_getHeadY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAiHuman_getFallDistY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAiHuman_getFallStartY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAiHuman_getNpc(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAiHuman_getWalkMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAiHuman_getWeaponMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAiHuman_getWmodeAst(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAiHuman_getWmodeSelect(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkAiHuman_getChangeWeapon(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkAiHuman_getActionMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setWaterLevel(UIntPtr slf, int waterLevel);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setFloorY(UIntPtr slf, float floorY);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setWaterY(UIntPtr slf, float waterY);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setCeilY(UIntPtr slf, float ceilY);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setFeetY(UIntPtr slf, float feetY);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setHeadY(UIntPtr slf, float headY);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setFallDistY(UIntPtr slf, float fallDistY);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setFallStartY(UIntPtr slf, float fallStartY);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setNpc(UIntPtr slf, UIntPtr npc);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setWalkMode(UIntPtr slf, int walkMode);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setWeaponMode(UIntPtr slf, int weaponMode);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setWmodeAst(UIntPtr slf, int wmodeAst);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setWmodeSelect(UIntPtr slf, int wmodeSelect);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setChangeWeapon(UIntPtr slf, bool changeWeapon);

		[DllImport(DllName)]
		public static extern void ZkAiHuman_setActionMode(UIntPtr slf, int actionMode);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAiMove_getVob(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkAiMove_setVob(UIntPtr slf, UIntPtr vob);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAiMove_getOwner(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkAiMove_setOwner(UIntPtr slf, UIntPtr owner);

		[DllImport(DllName)]
		public static extern UIntPtr ZkEventManager_new();

		[DllImport(DllName)]
		public static extern void ZkEventManager_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkEventManager_getCleared(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEventManager_setCleared(UIntPtr slf, bool cleared);

		[DllImport(DllName)]
		public static extern bool ZkEventManager_getActive(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEventManager_setActive(UIntPtr slf, bool active);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneCamera_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneCamera_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern CameraTrajectory ZkCutsceneCamera_getTrajectoryFOR(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setTrajectoryFOR(UIntPtr slf, CameraTrajectory trajectoryFOR);

		[DllImport(DllName)]
		public static extern CameraTrajectory ZkCutsceneCamera_getTargetTrajectoryFOR(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setTargetTrajectoryFOR(UIntPtr slf,
			CameraTrajectory targetTrajectoryFOR);

		[DllImport(DllName)]
		public static extern CameraLoopType ZkCutsceneCamera_getLoopMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setLoopMode(UIntPtr slf, CameraLoopType loopMode);

		[DllImport(DllName)]
		public static extern CameraLerpType ZkCutsceneCamera_getLerpMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setLerpMode(UIntPtr slf, CameraLerpType lerpMode);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getIgnoreFORVobRotation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setIgnoreFORVobRotation(UIntPtr slf, bool ignoreFORVobRotation);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getIgnoreFORVobRotationTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setIgnoreFORVobRotationTarget(UIntPtr slf,
			bool ignoreFORVobRotationTarget);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getAdapt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setAdapt(UIntPtr slf, bool adapt);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getEaseFirst(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setEaseFirst(UIntPtr slf, bool easeFirst);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getEaseLast(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setEaseLast(UIntPtr slf, bool easeLast);

		[DllImport(DllName)]
		public static extern float ZkCutsceneCamera_getTotalDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setTotalDuration(UIntPtr slf, float totalDuration);

		[DllImport(DllName)]
		public static extern IntPtr ZkCutsceneCamera_getAutoFocusVob(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setAutoFocusVob(UIntPtr slf, string autoFocusVob);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getAutoPlayerMovable(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setAutoPlayerMovable(UIntPtr slf, bool autoPlayerMovable);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getAutoUntriggerLast(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setAutoUntriggerLast(UIntPtr slf, bool autoUntriggerLast);

		[DllImport(DllName)]
		public static extern float ZkCutsceneCamera_getAutoUntriggerLastDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_setAutoUntriggerLastDelay(UIntPtr slf, float autoUntriggerLastDelay);

		[DllImport(DllName)]
		public static extern int ZkCutsceneCamera_getPositionCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkCutsceneCamera_getTargetCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkCutsceneCamera_getFrameCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneCamera_getFrame(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setTime(UIntPtr slf, float time);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getRollAngle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setRollAngle(UIntPtr slf, float rollAngle);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getFovScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setFovScale(UIntPtr slf, float fovScale);

		[DllImport(DllName)]
		public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setMotionType(UIntPtr slf, CameraMotion motionType);

		[DllImport(DllName)]
		public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionTypeFov(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setMotionTypeFov(UIntPtr slf, CameraMotion motionTypeFov);

		[DllImport(DllName)]
		public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionTypeRoll(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setMotionTypeRoll(UIntPtr slf, CameraMotion motionTypeRoll);

		[DllImport(DllName)]
		public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionTypeTimeScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setMotionTypeTimeScale(UIntPtr slf,
			CameraMotion motionTypeTimeScale);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getTension(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setTension(UIntPtr slf, float tension);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getCamBias(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setCamBias(UIntPtr slf, float camBias);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getContinuity(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setContinuity(UIntPtr slf, float continuity);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getTimeScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setTimeScale(UIntPtr slf, float timeScale);

		[DllImport(DllName)]
		public static extern bool ZkCameraTrajectoryFrame_getTimeFixed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setTimeFixed(UIntPtr slf, bool timeFixed);

		[DllImport(DllName)]
		public static extern ZkMat4x4 ZkCameraTrajectoryFrame_getOriginalPose(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraTrajectoryFrame_setOriginalPose(UIntPtr slf, ZkMat4x4 originalPose);

		[DllImport(DllName)]
		public static extern UIntPtr ZkLightPreset_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkLightPreset_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkLight_load(UIntPtr slf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkLight_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkLight_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLightPreset_getPreset(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setPreset(UIntPtr slf, string preset);

		[DllImport(DllName)]
		public static extern LightType ZkLightPreset_getLightType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setLightType(UIntPtr slf, LightType lightType);

		[DllImport(DllName)]
		public static extern float ZkLightPreset_getRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setRange(UIntPtr slf, float range);

		[DllImport(DllName)]
		public static extern ZkColor ZkLightPreset_getColor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setColor(UIntPtr slf, ZkColor color);

		[DllImport(DllName)]
		public static extern float ZkLightPreset_getConeAngle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setConeAngle(UIntPtr slf, float coneAngle);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getIsStatic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setIsStatic(UIntPtr slf, bool isStatic);

		[DllImport(DllName)]
		public static extern LightQuality ZkLightPreset_getQuality(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setQuality(UIntPtr slf, LightQuality quality);

		[DllImport(DllName)]
		public static extern IntPtr ZkLightPreset_getLensflareFx(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setLensflareFx(UIntPtr slf, string lensflareFx);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getOn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setOn(UIntPtr slf, bool on);

		[DllImport(DllName)]
		public static extern IntPtr ZkLightPreset_getRangeAnimationScale(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setRangeAnimationScale(UIntPtr slf, float[] rangeAnimationScale,
			ulong count);

		[DllImport(DllName)]
		public static extern float ZkLightPreset_getRangeAnimationFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setRangeAnimationFps(UIntPtr slf, float rangeAnimationFps);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getRangeAnimationSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setRangeAnimationSmooth(UIntPtr slf, bool rangeAnimationSmooth);

		[DllImport(DllName)]
		public static extern ulong ZkLightPreset_getColorAnimationCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ZkColor ZkLightPreset_getColorAnimationItem(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setColorAnimationList(UIntPtr slf, ZkColor[] colorAnimationList,
			ulong count);

		[DllImport(DllName)]
		public static extern float ZkLightPreset_getColorAnimationFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setColorAnimationFps(UIntPtr slf, float colorAnimationFps);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getColorAnimationSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setColorAnimationSmooth(UIntPtr slf, bool colorAnimationSmooth);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getCanMove(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLightPreset_setCanMove(UIntPtr slf, bool canMove);

		[DllImport(DllName)]
		public static extern IntPtr ZkLight_getPreset(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setPreset(UIntPtr slf, string preset);

		[DllImport(DllName)]
		public static extern LightType ZkLight_getLightType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setLightType(UIntPtr slf, LightType lightType);

		[DllImport(DllName)]
		public static extern float ZkLight_getRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setRange(UIntPtr slf, float range);

		[DllImport(DllName)]
		public static extern ZkColor ZkLight_getColor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setColor(UIntPtr slf, ZkColor color);

		[DllImport(DllName)]
		public static extern float ZkLight_getConeAngle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setConeAngle(UIntPtr slf, float coneAngle);

		[DllImport(DllName)]
		public static extern bool ZkLight_getIsStatic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setIsStatic(UIntPtr slf, bool isStatic);

		[DllImport(DllName)]
		public static extern LightQuality ZkLight_getQuality(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setQuality(UIntPtr slf, LightQuality quality);

		[DllImport(DllName)]
		public static extern IntPtr ZkLight_getLensflareFx(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setLensflareFx(UIntPtr slf, string lensflareFx);

		[DllImport(DllName)]
		public static extern bool ZkLight_getOn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setOn(UIntPtr slf, bool on);

		[DllImport(DllName)]
		public static extern IntPtr ZkLight_getRangeAnimationScale(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern void ZkLight_setRangeAnimationScale(UIntPtr slf, float[] rangeAnimationScale, ulong count);

		[DllImport(DllName)]
		public static extern float ZkLight_getRangeAnimationFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setRangeAnimationFps(UIntPtr slf, float rangeAnimationFps);

		[DllImport(DllName)]
		public static extern bool ZkLight_getRangeAnimationSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setRangeAnimationSmooth(UIntPtr slf, bool rangeAnimationSmooth);

		[DllImport(DllName)]
		public static extern ulong ZkLight_getColorAnimationCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ZkColor ZkLight_getColorAnimationItem(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void
			ZkLight_setColorAnimationList(UIntPtr slf, ZkColor[] colorAnimationList, ulong count);

		[DllImport(DllName)]
		public static extern float ZkLight_getColorAnimationFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setColorAnimationFps(UIntPtr slf, float colorAnimationFps);

		[DllImport(DllName)]
		public static extern bool ZkLight_getColorAnimationSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setColorAnimationSmooth(UIntPtr slf, bool colorAnimationSmooth);

		[DllImport(DllName)]
		public static extern bool ZkLight_getCanMove(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLight_setCanMove(UIntPtr slf, bool canMove);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimate_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimate_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkAnimate_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkAnimate_getStartOn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkAnimate_setStartOn(UIntPtr slf, bool startOn);

		[DllImport(DllName)]
		public static extern UIntPtr ZkItem_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkItem_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkItem_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkItem_getInstance(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItem_setInstance(UIntPtr slf, string instance);

		[DllImport(DllName)]
		public static extern UIntPtr ZkLensFlare_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkLensFlare_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkLensFlare_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLensFlare_getEffect(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLensFlare_setEffect(UIntPtr slf, string effect);

		[DllImport(DllName)]
		public static extern UIntPtr ZkParticleEffectController_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkParticleEffectController_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectController_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectController_getEffectName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectController_setEffectName(UIntPtr slf, string effectName);

		[DllImport(DllName)]
		public static extern bool ZkParticleEffectController_getKillWhenDone(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectController_setKillWhenDone(UIntPtr slf, bool killWhenDone);

		[DllImport(DllName)]
		public static extern bool ZkParticleEffectController_getInitiallyRunning(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectController_setInitiallyRunning(UIntPtr slf, bool initiallyRunning);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMessageFilter_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMessageFilter_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkMessageFilter_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMessageFilter_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMessageFilter_setTarget(UIntPtr slf, string target);

		[DllImport(DllName)]
		public static extern MessageFilterAction ZkMessageFilter_getOnTrigger(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMessageFilter_setOnTrigger(UIntPtr slf, MessageFilterAction onTrigger);

		[DllImport(DllName)]
		public static extern MessageFilterAction ZkMessageFilter_getOnUntrigger(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMessageFilter_setOnUntrigger(UIntPtr slf, MessageFilterAction onUntrigger);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCodeMaster_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCodeMaster_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkCodeMaster_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_setTarget(UIntPtr slf, string target);

		[DllImport(DllName)]
		public static extern bool ZkCodeMaster_getOrdered(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_setOrdered(UIntPtr slf, bool ordered);

		[DllImport(DllName)]
		public static extern bool ZkCodeMaster_getFirstFalseIsFailure(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_setFirstFalseIsFailure(UIntPtr slf, bool firstFalseIsFailure);

		[DllImport(DllName)]
		public static extern IntPtr ZkCodeMaster_getFailureTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_setFailureTarget(UIntPtr slf, string failureTarget);

		[DllImport(DllName)]
		public static extern bool ZkCodeMaster_getUntriggeredCancels(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_setUntriggeredCancels(UIntPtr slf, bool untriggeredCancels);

		[DllImport(DllName)]
		public static extern ulong ZkCodeMaster_getSlaveCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkCodeMaster_getSlave(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_setSlave(UIntPtr slf, ulong i, string slave);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_addSlave(UIntPtr slf, string slave);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_removeSlave(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void
			ZkCodeMaster_removeSlaves(UIntPtr slf, ZkStringEnumerator pred, IntPtr ctx);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMoverController_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMoverController_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkMoverController_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMoverController_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMoverController_setTarget(UIntPtr slf, string target);

		[DllImport(DllName)]
		public static extern MoverMessageType ZkMoverController_getMessage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMoverController_setMessage(UIntPtr slf, MoverMessageType message);

		[DllImport(DllName)]
		public static extern int ZkMoverController_getKey(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMoverController_setKey(UIntPtr slf, int key);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTouchDamage_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTouchDamage_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkTouchDamage_getDamage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setDamage(UIntPtr slf, float damage);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsBarrier(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setIsBarrier(UIntPtr slf, bool isBarrier);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsBlunt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setIsBlunt(UIntPtr slf, bool isBlunt);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsEdge(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setIsEdge(UIntPtr slf, bool isEdge);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsFire(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setIsFire(UIntPtr slf, bool isFire);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsFly(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setIsFly(UIntPtr slf, bool isFly);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsMagic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setIsMagic(UIntPtr slf, bool isMagic);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsPoint(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setIsPoint(UIntPtr slf, bool isPoint);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsFall(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setIsFall(UIntPtr slf, bool isFall);

		[DllImport(DllName)]
		public static extern float ZkTouchDamage_getRepeatDelaySeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setRepeatDelaySeconds(UIntPtr slf, float repeatDelaySeconds);

		[DllImport(DllName)]
		public static extern float ZkTouchDamage_getVolumeScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setVolumeScale(UIntPtr slf, float volumeScale);

		[DllImport(DllName)]
		public static extern TouchCollisionType ZkTouchDamage_getCollisionType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_setCollisionType(UIntPtr slf, TouchCollisionType collisionType);

		[DllImport(DllName)]
		public static extern UIntPtr ZkEarthquake_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkEarthquake_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkEarthquake_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEarthquake_getRadius(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEarthquake_setRadius(UIntPtr slf, float radius);

		[DllImport(DllName)]
		public static extern float ZkEarthquake_getDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEarthquake_setDuration(UIntPtr slf, float duration);

		[DllImport(DllName)]
		public static extern Vector3 ZkEarthquake_getAmplitude(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEarthquake_setAmplitude(UIntPtr slf, Vector3 amplitude);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMovableObject_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMovableObject_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMovableObject_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setName(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern int ZkMovableObject_getHp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setHp(UIntPtr slf, int hp);

		[DllImport(DllName)]
		public static extern int ZkMovableObject_getDamage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setDamage(UIntPtr slf, int damage);

		[DllImport(DllName)]
		public static extern bool ZkMovableObject_getMovable(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setMovable(UIntPtr slf, bool movable);

		[DllImport(DllName)]
		public static extern bool ZkMovableObject_getTakable(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setTakable(UIntPtr slf, bool takable);

		[DllImport(DllName)]
		public static extern bool ZkMovableObject_getFocusOverride(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setFocusOverride(UIntPtr slf, bool focusOverride);

		[DllImport(DllName)]
		public static extern SoundMaterialType ZkMovableObject_getMaterial(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setMaterial(UIntPtr slf, SoundMaterialType material);

		[DllImport(DllName)]
		public static extern IntPtr ZkMovableObject_getVisualDestroyed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setVisualDestroyed(UIntPtr slf, string visualDestroyed);

		[DllImport(DllName)]
		public static extern IntPtr ZkMovableObject_getOwner(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setOwner(UIntPtr slf, string owner);

		[DllImport(DllName)]
		public static extern IntPtr ZkMovableObject_getOwnerGuild(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setOwnerGuild(UIntPtr slf, string ownerGuild);

		[DllImport(DllName)]
		public static extern bool ZkMovableObject_getDestroyed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_setDestroyed(UIntPtr slf, bool destroyed);

		[DllImport(DllName)]
		public static extern UIntPtr ZkInteractiveObject_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkInteractiveObject_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkInteractiveObject_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkInteractiveObject_getState(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInteractiveObject_setState(UIntPtr slf, int state);

		[DllImport(DllName)]
		public static extern IntPtr ZkInteractiveObject_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInteractiveObject_setTarget(UIntPtr slf, string target);

		[DllImport(DllName)]
		public static extern IntPtr ZkInteractiveObject_getItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInteractiveObject_setItem(UIntPtr slf, string item);

		[DllImport(DllName)]
		public static extern IntPtr ZkInteractiveObject_getConditionFunction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInteractiveObject_setConditionFunction(UIntPtr slf, string conditionFunction);

		[DllImport(DllName)]
		public static extern IntPtr ZkInteractiveObject_getOnStateChangeFunction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInteractiveObject_setOnStateChangeFunction(UIntPtr slf,
			string onStateChangeFunction);

		[DllImport(DllName)]
		public static extern bool ZkInteractiveObject_getRewind(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInteractiveObject_setRewind(UIntPtr slf, bool rewind);

		[DllImport(DllName)]
		public static extern UIntPtr ZkFire_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkFire_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkFire_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkFire_getSlot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFire_setSlot(UIntPtr slf, string slot);

		[DllImport(DllName)]
		public static extern IntPtr ZkFire_getVobTree(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFire_setVobTree(UIntPtr slf, string vobTree);

		[DllImport(DllName)]
		public static extern UIntPtr ZkContainer_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkContainer_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkContainer_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkContainer_getIsLocked(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkContainer_setIsLocked(UIntPtr slf, bool isLocked);

		[DllImport(DllName)]
		public static extern IntPtr ZkContainer_getKey(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkContainer_setKey(UIntPtr slf, string key);

		[DllImport(DllName)]
		public static extern IntPtr ZkContainer_getPickString(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkContainer_setPickString(UIntPtr slf, string pickString);

		[DllImport(DllName)]
		public static extern IntPtr ZkContainer_getContents(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkContainer_setContents(UIntPtr slf, string contents);

		[DllImport(DllName)]
		public static extern ulong ZkContainer_getItemCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkContainer_getItem(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkContainer_addItem(UIntPtr slf, UIntPtr item);

		[DllImport(DllName)]
		public static extern void ZkContainer_removeItem(UIntPtr slf, ulong i);


		[DllImport(DllName)]
		public static extern UIntPtr ZkDoor_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDoor_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkDoor_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkDoor_getIsLocked(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkDoor_setIsLocked(UIntPtr slf, bool isLocked);

		[DllImport(DllName)]
		public static extern IntPtr ZkDoor_getKey(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkDoor_setKey(UIntPtr slf, string key);

		[DllImport(DllName)]
		public static extern IntPtr ZkDoor_getPickString(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkDoor_setPickString(UIntPtr slf, string pickString);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSound_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSound_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkSound_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSound_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setVolume(UIntPtr slf, float volume);

		[DllImport(DllName)]
		public static extern SoundMode ZkSound_getMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setMode(UIntPtr slf, SoundMode mode);

		[DllImport(DllName)]
		public static extern float ZkSound_getRandomDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setRandomDelay(UIntPtr slf, float randomDelay);

		[DllImport(DllName)]
		public static extern float ZkSound_getRandomDelayVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setRandomDelayVar(UIntPtr slf, float randomDelayVar);

		[DllImport(DllName)]
		public static extern bool ZkSound_getInitiallyPlaying(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setInitiallyPlaying(UIntPtr slf, bool initiallyPlaying);

		[DllImport(DllName)]
		public static extern bool ZkSound_getAmbient3d(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setAmbient3d(UIntPtr slf, bool ambient3d);

		[DllImport(DllName)]
		public static extern bool ZkSound_getObstruction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setObstruction(UIntPtr slf, bool obstruction);

		[DllImport(DllName)]
		public static extern float ZkSound_getConeAngle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setConeAngle(UIntPtr slf, float coneAngle);

		[DllImport(DllName)]
		public static extern SoundTriggerVolumeType ZkSound_getVolumeType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setVolumeType(UIntPtr slf, SoundTriggerVolumeType volumeType);

		[DllImport(DllName)]
		public static extern float ZkSound_getRadius(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setRadius(UIntPtr slf, float radius);

		[DllImport(DllName)]
		public static extern IntPtr ZkSound_getSoundName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setSoundName(UIntPtr slf, string soundName);

		[DllImport(DllName)]
		public static extern bool ZkSound_getIsRunning(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setIsRunning(UIntPtr slf, bool isRunning);

		[DllImport(DllName)]
		public static extern bool ZkSound_getIsAllowedToRun(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSound_setIsAllowedToRun(UIntPtr slf, bool isAllowedToRun);


		[DllImport(DllName)]
		public static extern UIntPtr ZkSoundDaytime_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSoundDaytime_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkSoundDaytime_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSoundDaytime_getStartTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundDaytime_setStartTime(UIntPtr slf, float startTime);

		[DllImport(DllName)]
		public static extern float ZkSoundDaytime_getEndTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundDaytime_setEndTime(UIntPtr slf, float endTime);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoundDaytime_getSoundNameDaytime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundDaytime_setSoundNameDaytime(UIntPtr slf, string soundNameDaytime);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTrigger_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTrigger_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTrigger_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTrigger_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setTarget(UIntPtr slf, string target);

		[DllImport(DllName)]
		public static extern bool ZkTrigger_getStartEnabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTrigger_getSendUntrigger(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTrigger_getReactToOnTrigger(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTrigger_getReactToOnTouch(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTrigger_getReactToOnDamage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTrigger_getRespondToObject(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTrigger_getRespondToPC(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTrigger_getRespondToNPC(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setStartEnabled(UIntPtr slf, bool b);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setSendUntrigger(UIntPtr slf, bool b);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setReactToOnTrigger(UIntPtr slf, bool b);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setReactToOnTouch(UIntPtr slf, bool b);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setReactToOnDamage(UIntPtr slf, bool b);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setRespondToObject(UIntPtr slf, bool b);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setRespondToPC(UIntPtr slf, bool b);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setRespondToNPC(UIntPtr slf, bool b);

		[DllImport(DllName)]
		public static extern IntPtr ZkTrigger_getVobTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setVobTarget(UIntPtr slf, string vobTarget);

		[DllImport(DllName)]
		public static extern int ZkTrigger_getMaxActivationCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setMaxActivationCount(UIntPtr slf, int maxActivationCount);

		[DllImport(DllName)]
		public static extern float ZkTrigger_getRetriggerDelaySeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setRetriggerDelaySeconds(UIntPtr slf, float retriggerDelaySeconds);

		[DllImport(DllName)]
		public static extern float ZkTrigger_getDamageThreshold(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setDamageThreshold(UIntPtr slf, float damageThreshold);

		[DllImport(DllName)]
		public static extern float ZkTrigger_getFireDelaySeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setFireDelaySeconds(UIntPtr slf, float fireDelaySeconds);

		[DllImport(DllName)]
		public static extern float ZkTrigger_getNextTimeTriggerable(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTrigger_getOtherVob(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkTrigger_getCountCanBeActivated(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTrigger_getIsEnabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setNextTimeTriggerable(UIntPtr slf, float nextTimeTriggerable);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setOtherVob(UIntPtr slf, UIntPtr otherVob);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setCountCanBeActivated(UIntPtr slf, int countCanBeActivated);

		[DllImport(DllName)]
		public static extern void ZkTrigger_setIsEnabled(UIntPtr slf, bool isEnabled);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMover_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMover_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkMover_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MoverBehavior ZkMover_getBehavior(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setBehavior(UIntPtr slf, MoverBehavior behavior);

		[DllImport(DllName)]
		public static extern float ZkMover_getTouchBlockerDamage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setTouchBlockerDamage(UIntPtr slf, float touchBlockerDamage);

		[DllImport(DllName)]
		public static extern float ZkMover_getStayOpenTimeSeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setStayOpenTimeSeconds(UIntPtr slf, float stayOpenTimeSeconds);

		[DllImport(DllName)]
		public static extern bool ZkMover_getIsLocked(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setIsLocked(UIntPtr slf, bool isLocked);

		[DllImport(DllName)]
		public static extern bool ZkMover_getAutoLink(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setAutoLink(UIntPtr slf, bool autoLink);

		[DllImport(DllName)]
		public static extern bool ZkMover_getAutoRotate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setAutoRotate(UIntPtr slf, bool autoRotate);

		[DllImport(DllName)]
		public static extern float ZkMover_getSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSpeed(UIntPtr slf, float speed);

		[DllImport(DllName)]
		public static extern MoverLerpType ZkMover_getLerpType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setLerpType(UIntPtr slf, MoverLerpType lerpType);

		[DllImport(DllName)]
		public static extern MoverSpeedType ZkMover_getSpeedType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSpeedType(UIntPtr slf, MoverSpeedType speedType);

		[DllImport(DllName)]
		public static extern ulong ZkMover_getKeyframeCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AnimationSample ZkMover_getKeyframe(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxOpenStart(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSfxOpenStart(UIntPtr slf, string sfxOpenStart);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxOpenEnd(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSfxOpenEnd(UIntPtr slf, string sfxOpenEnd);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxTransitioning(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSfxTransitioning(UIntPtr slf, string sfxTransitioning);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxCloseStart(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSfxCloseStart(UIntPtr slf, string sfxCloseStart);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxCloseEnd(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSfxCloseEnd(UIntPtr slf, string sfxCloseEnd);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxLock(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSfxLock(UIntPtr slf, string sfxLock);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxUnlock(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSfxUnlock(UIntPtr slf, string sfxUnlock);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxUseLocked(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setSfxUseLocked(UIntPtr slf, string sfxUseLocked);

		[DllImport(DllName)]
		public static extern Vector3 ZkMover_getActKeyPosDelta(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMover_getActKeyframeF(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMover_getActKeyframe(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMover_getNextKeyframe(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMover_getMoveSpeedUnit(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMover_getAdvanceDir(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkMover_getMoverState(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMover_getTriggerEventCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMover_getStayOpenTimeDest(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMover_setActKeyPosDelta(UIntPtr slf, Vector3 actKeyPosDelta);

		[DllImport(DllName)]
		public static extern void ZkMover_setActKeyframeF(UIntPtr slf, float actKeyframeF);

		[DllImport(DllName)]
		public static extern void ZkMover_setActKeyframe(UIntPtr slf, int actKeyframe);

		[DllImport(DllName)]
		public static extern void ZkMover_setNextKeyframe(UIntPtr slf, int nextKeyframe);

		[DllImport(DllName)]
		public static extern void ZkMover_setMoveSpeedUnit(UIntPtr slf, float moveSpeedUnit);

		[DllImport(DllName)]
		public static extern void ZkMover_setAdvanceDir(UIntPtr slf, float advanceDir);

		[DllImport(DllName)]
		public static extern void ZkMover_setMoverState(UIntPtr slf, uint moverState);

		[DllImport(DllName)]
		public static extern void ZkMover_setTriggerEventCount(UIntPtr slf, int triggerEventCount);

		[DllImport(DllName)]
		public static extern void ZkMover_setStayOpenTimeDest(UIntPtr slf, float stayOpenTimeDest);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerList_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerList_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerList_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern TriggerBatchMode ZkTriggerList_getMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerList_setMode(UIntPtr slf, TriggerBatchMode mode);

		[DllImport(DllName)]
		public static extern ulong ZkTriggerList_getTargetCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern byte ZkTriggerList_getActTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTriggerList_getSendOnTrigger(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerList_setActTarget(UIntPtr slf, byte actTarget);

		[DllImport(DllName)]
		public static extern void ZkTriggerList_setSendOnTrigger(UIntPtr slf, bool sendOnTrigger);


		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerList_getTarget(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerList_addTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerList_removeTarget(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkTriggerList_removeTargets(UIntPtr slf,
			ZkTriggerListTargetEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerListTarget_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerListTarget_setName(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern float ZkTriggerListTarget_getDelaySeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerListTarget_setDelaySeconds(UIntPtr slf, float delaySeconds);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerScript_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerScript_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerScript_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerScript_getFunction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerScript_setFunction(UIntPtr slf, string function);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerChangeLevel_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerChangeLevel_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerChangeLevel_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerChangeLevel_getLevelName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerChangeLevel_setLevelName(UIntPtr slf, string levelName);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerChangeLevel_getStartVob(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerChangeLevel_setStartVob(UIntPtr slf, string startVob);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerWorldStart_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerWorldStart_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerWorldStart_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerWorldStart_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerWorldStart_setTarget(UIntPtr slf, string target);

		[DllImport(DllName)]
		public static extern bool ZkTriggerWorldStart_getFireOnce(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerWorldStart_setFireOnce(UIntPtr slf, bool fireOnce);

		[DllImport(DllName)]
		public static extern bool ZkTriggerWorldStart_getHasFired(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerWorldStart_setHasFired(UIntPtr slf, bool hasFired);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerUntouch_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerUntouch_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerUntouch_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerUntouch_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkTriggerUntouch_setTarget(UIntPtr slf, string target);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneMusic_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneMusic_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkZoneMusic_getIsEnabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_setIsEnabled(UIntPtr slf, bool isEnabled);

		[DllImport(DllName)]
		public static extern int ZkZoneMusic_getPriority(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_setPriority(UIntPtr slf, int priority);

		[DllImport(DllName)]
		public static extern bool ZkZoneMusic_getIsEllipsoid(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_setIsEllipsoid(UIntPtr slf, bool isEllipsoid);

		[DllImport(DllName)]
		public static extern float ZkZoneMusic_getReverb(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_setReverb(UIntPtr slf, float reverb);

		[DllImport(DllName)]
		public static extern float ZkZoneMusic_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_setVolume(UIntPtr slf, float volume);

		[DllImport(DllName)]
		public static extern bool ZkZoneMusic_getIsLoop(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_setIsLoop(UIntPtr slf, bool isLoop);

		[DllImport(DllName)]
		public static extern bool ZkZoneMusic_getLocalEnabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkZoneMusic_getDayEntranceDone(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkZoneMusic_getNightEntranceDone(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_setLocalEnabled(UIntPtr slf, bool localEnabled);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_setDayEntranceDone(UIntPtr slf, bool dayEntraceDone);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_setNightEntranceDone(UIntPtr slf, bool nightEntranceDone);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneFarPlane_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneFarPlane_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkZoneFarPlane_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkZoneFarPlane_getVobFarPlaneZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneFarPlane_setVobFarPlaneZ(UIntPtr slf, float vobFarPlaneZ);

		[DllImport(DllName)]
		public static extern float ZkZoneFarPlane_getInnerRangePercentage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneFarPlane_setInnerRangePercentage(UIntPtr slf, float innerRangePercentage);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneFog_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneFog_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkZoneFog_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkZoneFog_getRangeCenter(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneFog_setRangeCenter(UIntPtr slf, float rangeCenter);

		[DllImport(DllName)]
		public static extern float ZkZoneFog_getInnerRangePercentage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneFog_setInnerRangePercentage(UIntPtr slf, float innerRangePercentage);

		[DllImport(DllName)]
		public static extern ZkColor ZkZoneFog_getColor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneFog_setColor(UIntPtr slf, ZkColor color);

		[DllImport(DllName)]
		public static extern bool ZkZoneFog_getFadeOutSky(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneFog_setFadeOutSky(UIntPtr slf, bool fadeOutSky);

		[DllImport(DllName)]
		public static extern bool ZkZoneFog_getOverrideColor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkZoneFog_setOverrideColor(UIntPtr slf, bool overrideColor);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpc_getNpcInstance(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkNpc_getModelScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkNpc_getModelFatness(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getGuild(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getGuildTrue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getLevel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getXp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getXpNextLevel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getLp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getFightTactic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getFightMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getWounded(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getMad(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getMadTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getPlayer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpc_getStartAiState(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpc_getScriptWaypoint(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getAttitude(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getAttitudeTemp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getNameNr(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getMoveLock(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getCurrentStateValid(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpc_getCurrentStateName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getCurrentStateIndex(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getCurrentStateIsRoutine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getNextStateValid(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpc_getNextStateName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getNextStateIndex(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getNextStateIsRoutine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getLastAiState(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getHasRoutine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getRoutineChanged(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getRoutineOverlay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getRoutineOverlayCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getWalkmodeRoutine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getWeaponmodeRoutine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getStartNewRoutine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getAiStateDriven(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkNpc_getAiStatePos(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpc_getCurrentRoutine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpc_getRespawn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getRespawnTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getBsInterruptableOverride(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getNpcType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getSpellMana(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkNpc_getCarryVob(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkNpc_getEnemy(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpc_setNpcInstance(UIntPtr slf, string npcInstance);

		[DllImport(DllName)]
		public static extern void ZkNpc_setModelScale(UIntPtr slf, Vector3 modelScale);

		[DllImport(DllName)]
		public static extern void ZkNpc_setModelFatness(UIntPtr slf, float modelFatness);

		[DllImport(DllName)]
		public static extern void ZkNpc_setFlags(UIntPtr slf, int flags);

		[DllImport(DllName)]
		public static extern void ZkNpc_setGuild(UIntPtr slf, int guild);

		[DllImport(DllName)]
		public static extern void ZkNpc_setGuildTrue(UIntPtr slf, int guildTrue);

		[DllImport(DllName)]
		public static extern void ZkNpc_setLevel(UIntPtr slf, int level);

		[DllImport(DllName)]
		public static extern void ZkNpc_setXp(UIntPtr slf, int xp);

		[DllImport(DllName)]
		public static extern void ZkNpc_setXpNextLevel(UIntPtr slf, int xpNextLevel);

		[DllImport(DllName)]
		public static extern void ZkNpc_setLp(UIntPtr slf, int lp);

		[DllImport(DllName)]
		public static extern void ZkNpc_setFightTactic(UIntPtr slf, int fightTactic);

		[DllImport(DllName)]
		public static extern void ZkNpc_setFightMode(UIntPtr slf, int fightMode);

		[DllImport(DllName)]
		public static extern void ZkNpc_setWounded(UIntPtr slf, bool wounded);

		[DllImport(DllName)]
		public static extern void ZkNpc_setMad(UIntPtr slf, bool mad);

		[DllImport(DllName)]
		public static extern void ZkNpc_setMadTime(UIntPtr slf, int madTime);

		[DllImport(DllName)]
		public static extern void ZkNpc_setPlayer(UIntPtr slf, bool player);

		[DllImport(DllName)]
		public static extern void ZkNpc_setStartAiState(UIntPtr slf, string startAiState);

		[DllImport(DllName)]
		public static extern void ZkNpc_setScriptWaypoint(UIntPtr slf, string scriptWaypoint);

		[DllImport(DllName)]
		public static extern void ZkNpc_setAttitude(UIntPtr slf, int attitude);

		[DllImport(DllName)]
		public static extern void ZkNpc_setAttitudeTemp(UIntPtr slf, int attitudeTemp);

		[DllImport(DllName)]
		public static extern void ZkNpc_setNameNr(UIntPtr slf, int nameNr);

		[DllImport(DllName)]
		public static extern void ZkNpc_setMoveLock(UIntPtr slf, bool moveLock);

		[DllImport(DllName)]
		public static extern void ZkNpc_setCurrentStateValid(UIntPtr slf, bool currentStateValid);

		[DllImport(DllName)]
		public static extern void ZkNpc_setCurrentStateName(UIntPtr slf, string currentStateName);

		[DllImport(DllName)]
		public static extern void ZkNpc_setCurrentStateIndex(UIntPtr slf, int currentStateIndex);

		[DllImport(DllName)]
		public static extern void ZkNpc_setCurrentStateIsRoutine(UIntPtr slf, bool currentStateIsRoutine);

		[DllImport(DllName)]
		public static extern void ZkNpc_setNextStateValid(UIntPtr slf, bool nextStateValid);

		[DllImport(DllName)]
		public static extern void ZkNpc_setNextStateName(UIntPtr slf, string nextStateName);

		[DllImport(DllName)]
		public static extern void ZkNpc_setNextStateIndex(UIntPtr slf, int nextStateIndex);

		[DllImport(DllName)]
		public static extern void ZkNpc_setNextStateIsRoutine(UIntPtr slf, bool nextStateIsRoutine);

		[DllImport(DllName)]
		public static extern void ZkNpc_setLastAiState(UIntPtr slf, int lastAiState);

		[DllImport(DllName)]
		public static extern void ZkNpc_setHasRoutine(UIntPtr slf, bool hasRoutine);

		[DllImport(DllName)]
		public static extern void ZkNpc_setRoutineChanged(UIntPtr slf, bool routineChanged);

		[DllImport(DllName)]
		public static extern void ZkNpc_setRoutineOverlay(UIntPtr slf, bool routineOverlay);

		[DllImport(DllName)]
		public static extern void ZkNpc_setRoutineOverlayCount(UIntPtr slf, int routineOverlayCount);

		[DllImport(DllName)]
		public static extern void ZkNpc_setWalkmodeRoutine(UIntPtr slf, int walkmodeRoutine);

		[DllImport(DllName)]
		public static extern void ZkNpc_setWeaponmodeRoutine(UIntPtr slf, bool weaponmodeRoutine);

		[DllImport(DllName)]
		public static extern void ZkNpc_setStartNewRoutine(UIntPtr slf, bool startNewRoutine);

		[DllImport(DllName)]
		public static extern void ZkNpc_setAiStateDriven(UIntPtr slf, int aiStateDriven);

		[DllImport(DllName)]
		public static extern void ZkNpc_setAiStatePos(UIntPtr slf, Vector3 aiStatePos);

		[DllImport(DllName)]
		public static extern void ZkNpc_setCurrentRoutine(UIntPtr slf, string currentRoutine);

		[DllImport(DllName)]
		public static extern void ZkNpc_setRespawn(UIntPtr slf, bool respawn);

		[DllImport(DllName)]
		public static extern void ZkNpc_setRespawnTime(UIntPtr slf, int respawnTime);

		[DllImport(DllName)]
		public static extern void ZkNpc_setBsInterruptableOverride(UIntPtr slf, int bsInterruptableOverride);

		[DllImport(DllName)]
		public static extern void ZkNpc_setNpcType(UIntPtr slf, int npcType);

		[DllImport(DllName)]
		public static extern void ZkNpc_setSpellMana(UIntPtr slf, int spellMana);

		[DllImport(DllName)]
		public static extern void ZkNpc_setCarryVob(UIntPtr slf, UIntPtr carryVob);

		[DllImport(DllName)]
		public static extern void ZkNpc_setEnemy(UIntPtr slf, UIntPtr enemy);

		[DllImport(DllName)]
		public static extern ulong ZkNpc_getOverlayCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpc_getOverlay(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_clearOverlays(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpc_removeOverlay(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_setOverlay(UIntPtr slf, ulong i, string overlay);

		[DllImport(DllName)]
		public static extern void ZkNpc_addOverlay(UIntPtr slf, string overlay);

		[DllImport(DllName)]
		public static extern ulong ZkNpc_getTalentCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkNpc_getTalent(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_clearTalents(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpc_removeTalent(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_setTalent(UIntPtr slf, ulong i, UIntPtr talent);

		[DllImport(DllName)]
		public static extern void ZkNpc_addTalent(UIntPtr slf, UIntPtr talent);

		[DllImport(DllName)]
		public static extern ulong ZkNpc_getItemCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkNpc_getItem(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_clearItems(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpc_removeItem(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_setItem(UIntPtr slf, ulong i, UIntPtr item);

		[DllImport(DllName)]
		public static extern void ZkNpc_addItem(UIntPtr slf, UIntPtr item);

		[DllImport(DllName)]
		public static extern ulong ZkNpc_getSlotCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkNpc_getSlot(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_clearSlots(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpc_removeSlot(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkNpc_addSlot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpc_getProtection(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_setProtection(UIntPtr slf, ulong i, int v);

		[DllImport(DllName)]
		public static extern int ZkNpc_getAttribute(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_setAttribute(UIntPtr slf, ulong i, int v);

		[DllImport(DllName)]
		public static extern int ZkNpc_getHitChance(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_setHitChance(UIntPtr slf, ulong i, int v);

		[DllImport(DllName)]
		public static extern int ZkNpc_getMission(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_setMission(UIntPtr slf, ulong i, int v);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpc_getAiVars(UIntPtr slf, out ulong len);

		[DllImport(DllName)]
		public static extern void ZkNpc_setAiVars(UIntPtr slf, int[] vars, ulong len);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpc_getPacked(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpc_setPacked(UIntPtr slf, ulong i, string v);

		[DllImport(DllName)]
		public static extern UIntPtr ZkNpcTalent_new();

		[DllImport(DllName)]
		public static extern void ZkNpcTalent_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcTalent_getTalent(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcTalent_getValue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcTalent_getSkill(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcTalent_setTalent(UIntPtr slf, int v);

		[DllImport(DllName)]
		public static extern void ZkNpcTalent_setValue(UIntPtr slf, int v);

		[DllImport(DllName)]
		public static extern void ZkNpcTalent_setSkill(UIntPtr slf, int v);

		[DllImport(DllName)]
		public static extern bool ZkNpcSlot_getUsed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcSlot_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkNpcSlot_getItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkNpcSlot_getInInventory(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcSlot_setUsed(UIntPtr slf, bool used);

		[DllImport(DllName)]
		public static extern void ZkNpcSlot_setName(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern void ZkNpcSlot_setItem(UIntPtr slf, UIntPtr item);

		[DllImport(DllName)]
		public static extern void ZkNpcSlot_setInInventory(UIntPtr slf, bool inInventory);


		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusScript_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusScript_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusScript_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkDaedalusScript_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkDaedalusScript_getSymbolCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkDaedalusScript_enumerateInstanceSymbols(UIntPtr slf, string className,
			ZkDaedalusSymbolEnumerator cb, IntPtr ctx);

		[DllImport(DllName)]
		public static extern DaedalusInstruction ZkDaedalusScript_getInstruction(UIntPtr slf, ulong address);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusScript_getSymbolByIndex(UIntPtr slf, uint i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusScript_getSymbolByAddress(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusScript_getSymbolByName(UIntPtr slf, string i);

		[DllImport(DllName)]
		public static extern IntPtr ZkDaedalusSymbol_getString(UIntPtr slf, ushort index, UIntPtr context);

		[DllImport(DllName)]
		public static extern float ZkDaedalusSymbol_getFloat(UIntPtr slf, ushort index, UIntPtr context);

		[DllImport(DllName)]
		public static extern int ZkDaedalusSymbol_getInt(UIntPtr slf, ushort index, UIntPtr context);

		[DllImport(DllName)]
		public static extern void ZkDaedalusSymbol_setString(UIntPtr slf, string value, ushort index, UIntPtr context);

		[DllImport(DllName)]
		public static extern void ZkDaedalusSymbol_setFloat(UIntPtr slf, float value, ushort index, UIntPtr context);

		[DllImport(DllName)]
		public static extern void ZkDaedalusSymbol_setInt(UIntPtr slf, int value, ushort index, UIntPtr context);

		[DllImport(DllName)]
		public static extern bool ZkDaedalusSymbol_getIsConst(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkDaedalusSymbol_getIsMember(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkDaedalusSymbol_getIsExternal(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkDaedalusSymbol_getIsMerged(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkDaedalusSymbol_getIsGenerated(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkDaedalusSymbol_getHasReturn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkDaedalusSymbol_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkDaedalusSymbol_getAddress(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkDaedalusSymbol_getParent(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkDaedalusSymbol_getSize(UIntPtr slf);

		[DllImport(DllName)]
		public static extern DaedalusDataType ZkDaedalusSymbol_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkDaedalusSymbol_getIndex(UIntPtr slf);

		[DllImport(DllName)]
		public static extern DaedalusDataType ZkDaedalusSymbol_getReturnType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern DaedalusInstanceType ZkDaedalusInstance_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkDaedalusInstance_getIndex(UIntPtr ptr);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_pushInt(UIntPtr slf, int value);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_pushFloat(UIntPtr slf, float value);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_pushString(UIntPtr slf, string value);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_pushInstance(UIntPtr slf, UIntPtr value);

		[DllImport(DllName)]
		public static extern int ZkDaedalusVm_popInt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkDaedalusVm_popFloat(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkDaedalusVm_popString(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_popInstance(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_getGlobalSelf(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_getGlobalOther(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_getGlobalVictim(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_getGlobalHero(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_getGlobalItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_setGlobalSelf(UIntPtr slf, UIntPtr value);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_setGlobalOther(UIntPtr slf, UIntPtr value);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_setGlobalVictim(UIntPtr slf, UIntPtr value);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_setGlobalHero(UIntPtr slf, UIntPtr value);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_setGlobalItem(UIntPtr slf, UIntPtr value);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_callFunction(UIntPtr slf, UIntPtr sym);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_allocInstance(UIntPtr slf, UIntPtr sym, DaedalusInstanceType type);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDaedalusVm_initInstance(UIntPtr slf, UIntPtr sym, DaedalusInstanceType type);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_initInstanceDirect(UIntPtr slf, UIntPtr instance);


		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_registerExternal(UIntPtr slf, UIntPtr sym,
			ZkDaedalusVmExternalCallback cb,
			IntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_registerExternalDefault(UIntPtr slf,
			ZkDaedalusVmExternalDefaultCallback cb,
			IntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_printStackTrace(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getWaterDepthKnee(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setWaterDepthKnee(UIntPtr slf, ulong i, int waterDepthKnee);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getWaterDepthChest(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setWaterDepthChest(UIntPtr slf, ulong i, int waterDepthChest);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getJumpUpHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setJumpUpHeight(UIntPtr slf, ulong i, int jumpUpHeight);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getSwimTime(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setSwimTime(UIntPtr slf, ulong i, int swimTime);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getDiveTime(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setDiveTime(UIntPtr slf, ulong i, int diveTime);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getStepHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setStepHeight(UIntPtr slf, ulong i, int stepHeight);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getJumpLowHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setJumpLowHeight(UIntPtr slf, ulong i, int jumpLowHeight);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getJumpMidHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setJumpMidHeight(UIntPtr slf, ulong i, int jumpMidHeight);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getSlideAngle(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setSlideAngle(UIntPtr slf, ulong i, int slideAngle);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getSlideAngle2(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setSlideAngle2(UIntPtr slf, ulong i, int slideAngle2);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getDisableAutoRoll(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setDisableAutoRoll(UIntPtr slf, ulong i, int disableAutoRoll);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getSurfaceAlign(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setSurfaceAlign(UIntPtr slf, ulong i, int surfaceAlign);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getClimbHeadingAngle(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setClimbHeadingAngle(UIntPtr slf, ulong i,
			int climbHeadingAngle);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getClimbHorizAngle(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setClimbHorizAngle(UIntPtr slf, ulong i, int climbHorizAngle);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getClimbGroundAngle(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setClimbGroundAngle(UIntPtr slf, ulong i, int climbGroundAngle);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRangeBase(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setFightRangeBase(UIntPtr slf, ulong i, int fightRangeBase);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRangeFist(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setFightRangeFist(UIntPtr slf, ulong i, int fightRangeFist);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRangeG(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setFightRangeG(UIntPtr slf, ulong i, int fightRangeG);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRange1Hs(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setFightRange1Hs(UIntPtr slf, ulong i, int fightRange1Hs);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRange1Ha(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setFightRange1Ha(UIntPtr slf, ulong i, int fightRange1Ha);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRange2Hs(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setFightRange2Hs(UIntPtr slf, ulong i, int fightRange2Hs);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRange2Ha(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setFightRange2Ha(UIntPtr slf, ulong i, int fightRange2Ha);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFallDownHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setFallDownHeight(UIntPtr slf, ulong i, int fallDownHeight);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFallDownDamage(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setFallDownDamage(UIntPtr slf, ulong i, int fallDownDamage);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getBloodDisabled(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setBloodDisabled(UIntPtr slf, ulong i, int bloodDisabled);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getBloodMaxDistance(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setBloodMaxDistance(UIntPtr slf, ulong i, int bloodMaxDistance);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getBloodAmount(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setBloodAmount(UIntPtr slf, ulong i, int bloodAmount);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getBloodFlow(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setBloodFlow(UIntPtr slf, ulong i, int bloodFlow);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getTurnSpeed(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setTurnSpeed(UIntPtr slf, ulong i, int turnSpeed);

		[DllImport(DllName)]
		public static extern IntPtr ZkGuildValuesInstance_getBloodEmitter(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setBloodEmitter(UIntPtr slf, ulong i, string bloodEmitter);

		[DllImport(DllName)]
		public static extern IntPtr ZkGuildValuesInstance_getBloodTexture(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkGuildValuesInstance_setBloodTexture(UIntPtr slf, ulong i, string bloodTexture);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setId(UIntPtr slf, int id);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getSlot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setSlot(UIntPtr slf, string slot);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getEffect(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setEffect(UIntPtr slf, string effect);

		[DllImport(DllName)]
		public static extern NpcType ZkNpcInstance_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setType(UIntPtr slf, NpcType type);

		[DllImport(DllName)]
		public static extern uint ZkNpcInstance_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setFlags(UIntPtr slf, uint flags);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getDamageType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setDamageType(UIntPtr slf, int damageType);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getGuild(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setGuild(UIntPtr slf, int guild);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getLevel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setLevel(UIntPtr slf, int level);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getFightTactic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setFightTactic(UIntPtr slf, int fightTactic);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getWeapon(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setWeapon(UIntPtr slf, int weapon);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getVoice(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setVoice(UIntPtr slf, int voice);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getVoicePitch(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setVoicePitch(UIntPtr slf, int voicePitch);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getBodyMass(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setBodyMass(UIntPtr slf, int bodyMass);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getDailyRoutine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setDailyRoutine(UIntPtr slf, int dailyRoutine);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getStartAiState(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setStartAiState(UIntPtr slf, int startAiState);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getSpawnPoint(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setSpawnPoint(UIntPtr slf, string spawnPoint);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getSpawnDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setSpawnDelay(UIntPtr slf, int spawnDelay);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getSenses(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setSenses(UIntPtr slf, int senses);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getSensesRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setSensesRange(UIntPtr slf, int sensesRange);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getWp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setWp(UIntPtr slf, string wp);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getExp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setExp(UIntPtr slf, int exp);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getExpNext(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setExpNext(UIntPtr slf, int expNext);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getLp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setLp(UIntPtr slf, int lp);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getBodyStateInterruptableOverride(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setBodyStateInterruptableOverride(UIntPtr slf,
			int bodyStateInterruptableOverride);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getNoFocus(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setNoFocus(UIntPtr slf, int noFocus);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getName(UIntPtr slf, NpcNameSlot slot);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setName(UIntPtr slf, NpcNameSlot slot, string name);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getMission(UIntPtr slf, NpcMissionSlot slot);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setMission(UIntPtr slf, NpcMissionSlot slot, int mission);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getAttribute(UIntPtr slf, NpcAttribute attribute);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setAttribute(UIntPtr slf, NpcAttribute attribute, int value);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getHitChance(UIntPtr slf, NpcTalent talent);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setHitChance(UIntPtr slf, NpcTalent talent, int hitChance);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getProtection(UIntPtr slf, DamageType type);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setProtection(UIntPtr slf, DamageType type, int protection);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getDamage(UIntPtr slf, DamageType type);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setDamage(UIntPtr slf, DamageType type, int damage);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getAiVar(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkNpcInstance_setAiVar(UIntPtr slf, ulong i, int aiVar);

		[DllImport(DllName)]
		public static extern IntPtr ZkMissionInstance_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setName(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern IntPtr ZkMissionInstance_getDescription(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setDescription(UIntPtr slf, string description);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setDuration(UIntPtr slf, int duration);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getImportant(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setImportant(UIntPtr slf, int important);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getOfferConditions(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setOfferConditions(UIntPtr slf, int offerConditions);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getOffer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setOffer(UIntPtr slf, int offer);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getSuccessConditions(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setSuccessConditions(UIntPtr slf, int successConditions);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getSuccess(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setSuccess(UIntPtr slf, int success);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getFailureConditions(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setFailureConditions(UIntPtr slf, int failureConditions);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getFailure(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setFailure(UIntPtr slf, int failure);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getObsoleteConditions(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setObsoleteConditions(UIntPtr slf, int obsoleteConditions);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getObsolete(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setObsolete(UIntPtr slf, int obsolete);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getRunning(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMissionInstance_setRunning(UIntPtr slf, int running);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setId(UIntPtr slf, int id);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setName(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getNameId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setNameId(UIntPtr slf, string nameId);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getHp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setHp(UIntPtr slf, int hp);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getHpMax(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setHpMax(UIntPtr slf, int hpMax);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMainFlag(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setMainFlag(UIntPtr slf, int mainFlag);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setFlags(UIntPtr slf, int flags);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getWeight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setWeight(UIntPtr slf, int weight);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getValue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setValue(UIntPtr slf, int value);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getDamageType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setDamageType(UIntPtr slf, int damageType);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getDamageTotal(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setDamageTotal(UIntPtr slf, int damageTotal);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getWear(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setWear(UIntPtr slf, int wear);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getNutrition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setNutrition(UIntPtr slf, int nutrition);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMagic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setMagic(UIntPtr slf, int magic);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOnEquip(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setOnEquip(UIntPtr slf, int onEquip);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOnUnequip(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setOnUnequip(UIntPtr slf, int onUnequip);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOwner(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setOwner(UIntPtr slf, int owner);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOwnerGuild(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setOwnerGuild(UIntPtr slf, int ownerGuild);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getDisguiseGuild(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setDisguiseGuild(UIntPtr slf, int disguiseGuild);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getVisual(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setVisual(UIntPtr slf, string visual);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getVisualChange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setVisualChange(UIntPtr slf, string visualChange);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getEffect(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setEffect(UIntPtr slf, string effect);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getVisualSkin(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setVisualSkin(UIntPtr slf, int visualSkin);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getSchemeName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setSchemeName(UIntPtr slf, string schemeName);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMaterial(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setMaterial(UIntPtr slf, int material);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMunition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setMunition(UIntPtr slf, int munition);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getSpell(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setSpell(UIntPtr slf, int spell);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setRange(UIntPtr slf, int range);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMagCircle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setMagCircle(UIntPtr slf, int magCircle);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getDescription(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setDescription(UIntPtr slf, string description);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvZBias(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setInvZBias(UIntPtr slf, int invZBias);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvRotX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setInvRotX(UIntPtr slf, int invRotX);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvRotY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setInvRotY(UIntPtr slf, int invRotY);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvRotZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setInvRotZ(UIntPtr slf, int invRotZ);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvAnimate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setInvAnimate(UIntPtr slf, int invAnimate);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getDamage(UIntPtr slf, DamageType type);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setDamage(UIntPtr slf, DamageType type, int damage);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getProtection(UIntPtr slf, DamageType type);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setProtection(UIntPtr slf, DamageType type, int protection);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getCondAtr(UIntPtr slf, ItemConditionSlot slot);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setCondAtr(UIntPtr slf, ItemConditionSlot slot, int condAtr);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getCondValue(UIntPtr slf, ItemConditionSlot slot);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setCondValue(UIntPtr slf, ItemConditionSlot slot, int condValue);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getChangeAtr(UIntPtr slf, ItemConditionSlot slot);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setChangeAtr(UIntPtr slf, ItemConditionSlot slot, int changeAtr);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getChangeValue(UIntPtr slf, ItemConditionSlot slot);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setChangeValue(UIntPtr slf, ItemConditionSlot slot, int changeValue);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOnState(UIntPtr slf, ItemStateSlot slot);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setOnState(UIntPtr slf, ItemStateSlot slot, int onState);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getText(UIntPtr slf, ItemTextSlot slot);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setText(UIntPtr slf, ItemTextSlot slot, string text);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getCount(UIntPtr slf, ItemTextSlot slot);

		[DllImport(DllName)]
		public static extern void ZkItemInstance_setCount(UIntPtr slf, ItemTextSlot slot, int count);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcLongrange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setNpcLongrange(UIntPtr slf, float npcLongrange);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcRange1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setNpcRange1(UIntPtr slf, float npcRange1);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcRange2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setNpcRange2(UIntPtr slf, float npcRange2);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setNpcAzi(UIntPtr slf, float npcAzi);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcElevdo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setNpcElevdo(UIntPtr slf, float npcElevdo);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcElevup(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setNpcElevup(UIntPtr slf, float npcElevup);

		[DllImport(DllName)]
		public static extern int ZkFocusInstance_getNpcPrio(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setNpcPrio(UIntPtr slf, int npcPrio);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemRange1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setItemRange1(UIntPtr slf, float itemRange1);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemRange2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setItemRange2(UIntPtr slf, float itemRange2);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setItemAzi(UIntPtr slf, float itemAzi);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemElevdo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setItemElevdo(UIntPtr slf, float itemElevdo);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemElevup(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setItemElevup(UIntPtr slf, float itemElevup);

		[DllImport(DllName)]
		public static extern int ZkFocusInstance_getItemPrio(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setItemPrio(UIntPtr slf, int itemPrio);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobRange1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setMobRange1(UIntPtr slf, float mobRange1);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobRange2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setMobRange2(UIntPtr slf, float mobRange2);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setMobAzi(UIntPtr slf, float mobAzi);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobElevdo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setMobElevdo(UIntPtr slf, float mobElevdo);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobElevup(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setMobElevup(UIntPtr slf, float mobElevup);

		[DllImport(DllName)]
		public static extern int ZkFocusInstance_getMobPrio(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkFocusInstance_setMobPrio(UIntPtr slf, int mobPrio);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getNpc(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInfoInstance_setNpc(UIntPtr slf, int npc);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getNr(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInfoInstance_setNr(UIntPtr slf, int nr);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getImportant(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInfoInstance_setImportant(UIntPtr slf, int important);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getCondition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInfoInstance_setCondition(UIntPtr slf, int condition);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getInformation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInfoInstance_setInformation(UIntPtr slf, int information);

		[DllImport(DllName)]
		public static extern IntPtr ZkInfoInstance_getDescription(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInfoInstance_setDescription(UIntPtr slf, string description);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getTrade(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInfoInstance_setTrade(UIntPtr slf, int trade);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getPermanent(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkInfoInstance_setPermanent(UIntPtr slf, int permanent);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getNpc(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemReactInstance_setNpc(UIntPtr slf, int npc);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getTradeItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemReactInstance_setTradeItem(UIntPtr slf, int tradeItem);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getTradeAmount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemReactInstance_setTradeAmount(UIntPtr slf, int tradeAmount);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getRequestedCategory(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemReactInstance_setRequestedCategory(UIntPtr slf, int requestedCategory);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getRequestedItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemReactInstance_setRequestedItem(UIntPtr slf, int requestedItem);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getRequestedAmount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemReactInstance_setRequestedAmount(UIntPtr slf, int requestedAmount);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getReaction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkItemReactInstance_setReaction(UIntPtr slf, int reaction);

		[DllImport(DllName)]
		public static extern float ZkSpellInstance_getTimePerMana(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setTimePerMana(UIntPtr slf, float timePerMana);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getDamagePerLevel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setDamagePerLevel(UIntPtr slf, int damagePerLevel);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getDamageType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setDamageType(UIntPtr slf, int damageType);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getSpellType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setSpellType(UIntPtr slf, int spellType);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getCanTurnDuringInvest(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setCanTurnDuringInvest(UIntPtr slf, int canTurnDuringInvest);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getCanChangeTargetDuringInvest(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setCanChangeTargetDuringInvest(UIntPtr slf,
			int canChangeTargetDuringInvest);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getIsMultiEffect(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setIsMultiEffect(UIntPtr slf, int isMultiEffect);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectAlgo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setTargetCollectAlgo(UIntPtr slf, int targetCollectAlgo);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setTargetCollectType(UIntPtr slf, int targetCollectType);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setTargetCollectRange(UIntPtr slf, int targetCollectRange);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setTargetCollectAzi(UIntPtr slf, int targetCollectAzi);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectElevation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSpellInstance_setTargetCollectElevation(UIntPtr slf, int targetCollectElevation);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuInstance_getBackPic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setBackPic(UIntPtr slf, string backPic);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuInstance_getBackWorld(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setBackWorld(UIntPtr slf, string backWorld);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getPosX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setPosX(UIntPtr slf, int posX);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getPosY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setPosY(UIntPtr slf, int posY);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getDimX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setDimX(UIntPtr slf, int dimX);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getDimY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setDimY(UIntPtr slf, int dimY);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getAlpha(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setAlpha(UIntPtr slf, int alpha);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuInstance_getMusicTheme(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setMusicTheme(UIntPtr slf, string musicTheme);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getEventTimerMsec(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setEventTimerMsec(UIntPtr slf, int eventTimerMsec);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setFlags(UIntPtr slf, int flags);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getDefaultOutgame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setDefaultOutgame(UIntPtr slf, int defaultOutgame);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getDefaultIngame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setDefaultIngame(UIntPtr slf, int defaultIngame);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuInstance_getItem(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMenuInstance_setItem(UIntPtr slf, ulong i, string item);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getFontName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setFontName(UIntPtr slf, string fontName);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getBackpic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setBackpic(UIntPtr slf, string backpic);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getAlphaMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setAlphaMode(UIntPtr slf, string alphaMode);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getAlpha(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setAlpha(UIntPtr slf, int alpha);

		[DllImport(DllName)]
		public static extern MenuItemType ZkMenuItemInstance_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setType(UIntPtr slf, MenuItemType type);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getOnChgSetOption(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setOnChgSetOption(UIntPtr slf, string onChgSetOption);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getOnChgSetOptionSection(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void
			ZkMenuItemInstance_setOnChgSetOptionSection(UIntPtr slf, string onChgSetOptionSection);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getPosX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setPosX(UIntPtr slf, int posX);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getPosY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setPosY(UIntPtr slf, int posY);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getDimX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setDimX(UIntPtr slf, int dimX);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getDimY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setDimY(UIntPtr slf, int dimY);

		[DllImport(DllName)]
		public static extern float ZkMenuItemInstance_getSizeStartScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setSizeStartScale(UIntPtr slf, float sizeStartScale);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setFlags(UIntPtr slf, int flags);

		[DllImport(DllName)]
		public static extern float ZkMenuItemInstance_getOpenDelayTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setOpenDelayTime(UIntPtr slf, float openDelayTime);

		[DllImport(DllName)]
		public static extern float ZkMenuItemInstance_getOpenDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setOpenDuration(UIntPtr slf, float openDuration);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFramePosX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setFramePosX(UIntPtr slf, int framePosX);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFramePosY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setFramePosY(UIntPtr slf, int framePosY);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFrameSizeX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setFrameSizeX(UIntPtr slf, int frameSizeX);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFrameSizeY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setFrameSizeY(UIntPtr slf, int frameSizeY);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getHideIfOptionSectionSet(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setHideIfOptionSectionSet(UIntPtr slf,
			string hideIfOptionSectionSet);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getHideIfOptionSet(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setHideIfOptionSet(UIntPtr slf, string hideIfOptionSet);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getHideOnValue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setHideOnValue(UIntPtr slf, int hideOnValue);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getText(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setText(UIntPtr slf, ulong i, string text);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getOnSelAction(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setOnSelAction(UIntPtr slf, ulong i, int onSelAction);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getOnSelActionS(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setOnSelActionS(UIntPtr slf, ulong i, string onSelActionS);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getOnEventAction(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setOnEventAction(UIntPtr slf, ulong i, int onEventAction);

		[DllImport(DllName)]
		public static extern float ZkMenuItemInstance_getUserFloat(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setUserFloat(UIntPtr slf, ulong i, float userFloat);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getUserString(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMenuItemInstance_setUserString(UIntPtr slf, ulong i, string userString);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getBestRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setBestRange(UIntPtr slf, float bestRange);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMinRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setMinRange(UIntPtr slf, float minRange);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMaxRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setMaxRange(UIntPtr slf, float maxRange);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getBestElevation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setBestElevation(UIntPtr slf, float bestElevation);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMinElevation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setMinElevation(UIntPtr slf, float minElevation);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMaxElevation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setMaxElevation(UIntPtr slf, float maxElevation);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getBestAzimuth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setBestAzimuth(UIntPtr slf, float bestAzimuth);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMinAzimuth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setMinAzimuth(UIntPtr slf, float minAzimuth);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMaxAzimuth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setMaxAzimuth(UIntPtr slf, float maxAzimuth);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getBestRotZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setBestRotZ(UIntPtr slf, float bestRotZ);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMinRotZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setMinRotZ(UIntPtr slf, float minRotZ);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMaxRotZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setMaxRotZ(UIntPtr slf, float maxRotZ);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getRotOffsetX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setRotOffsetX(UIntPtr slf, float rotOffsetX);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getRotOffsetY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setRotOffsetY(UIntPtr slf, float rotOffsetY);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getRotOffsetZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setRotOffsetZ(UIntPtr slf, float rotOffsetZ);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getTargetOffsetX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setTargetOffsetX(UIntPtr slf, float targetOffsetX);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getTargetOffsetY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setTargetOffsetY(UIntPtr slf, float targetOffsetY);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getTargetOffsetZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setTargetOffsetZ(UIntPtr slf, float targetOffsetZ);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getVeloTrans(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setVeloTrans(UIntPtr slf, float veloTrans);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getVeloRot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setVeloRot(UIntPtr slf, float veloRot);

		[DllImport(DllName)]
		public static extern int ZkCameraInstance_getTranslate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setTranslate(UIntPtr slf, int translate);

		[DllImport(DllName)]
		public static extern int ZkCameraInstance_getRotate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setRotate(UIntPtr slf, int rotate);

		[DllImport(DllName)]
		public static extern int ZkCameraInstance_getCollision(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkCameraInstance_setCollision(UIntPtr slf, int collision);

		[DllImport(DllName)]
		public static extern float ZkMusicSystemInstance_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicSystemInstance_setVolume(UIntPtr slf, float volume);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getBitResolution(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicSystemInstance_setBitResolution(UIntPtr slf, int bitResolution);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getGlobalReverbEnabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicSystemInstance_setGlobalReverbEnabled(UIntPtr slf, int globalReverbEnabled);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getSampleRate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicSystemInstance_setSampleRate(UIntPtr slf, int sampleRate);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getNumChannels(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicSystemInstance_setNumChannels(UIntPtr slf, int numChannels);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getReverbBufferSize(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicSystemInstance_setReverbBufferSize(UIntPtr slf, int reverbBufferSize);

		[DllImport(DllName)]
		public static extern IntPtr ZkMusicThemeInstance_getFile(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicThemeInstance_setFile(UIntPtr slf, string file);

		[DllImport(DllName)]
		public static extern float ZkMusicThemeInstance_getVol(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicThemeInstance_setVol(UIntPtr slf, float vol);

		[DllImport(DllName)]
		public static extern int ZkMusicThemeInstance_getLoop(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicThemeInstance_setLoop(UIntPtr slf, int loop);

		[DllImport(DllName)]
		public static extern float ZkMusicThemeInstance_getReverbmix(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicThemeInstance_setReverbmix(UIntPtr slf, float reverbmix);

		[DllImport(DllName)]
		public static extern float ZkMusicThemeInstance_getReverbtime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicThemeInstance_setReverbtime(UIntPtr slf, float reverbtime);

		[DllImport(DllName)]
		public static extern MusicTransitionEffect ZkMusicThemeInstance_getTranstype(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicThemeInstance_setTranstype(UIntPtr slf, MusicTransitionEffect transtype);

		[DllImport(DllName)]
		public static extern MusicTransitionType ZkMusicThemeInstance_getTranssubtype(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicThemeInstance_setTranssubtype(UIntPtr slf, MusicTransitionType transsubtype);

		[DllImport(DllName)]
		public static extern IntPtr ZkMusicJingleInstance_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicJingleInstance_setName(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern int ZkMusicJingleInstance_getLoop(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicJingleInstance_setLoop(UIntPtr slf, int loop);

		[DllImport(DllName)]
		public static extern float ZkMusicJingleInstance_getVol(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicJingleInstance_setVol(UIntPtr slf, float vol);

		[DllImport(DllName)]
		public static extern int ZkMusicJingleInstance_getTranssubtype(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkMusicJingleInstance_setTranssubtype(UIntPtr slf, int transsubtype);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getPpsValue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setPpsValue(UIntPtr slf, float ppsValue);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getPpsScaleKeysS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setPpsScaleKeysS(UIntPtr slf, string ppsScaleKeysS);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getPpsIsLooping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setPpsIsLooping(UIntPtr slf, int ppsIsLooping);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getPpsIsSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setPpsIsSmooth(UIntPtr slf, int ppsIsSmooth);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getPpsFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setPpsFps(UIntPtr slf, float ppsFps);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getPpsCreateEmS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setPpsCreateEmS(UIntPtr slf, string ppsCreateEmS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getPpsCreateEmDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setPpsCreateEmDelay(UIntPtr slf, float ppsCreateEmDelay);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpTypeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpTypeS(UIntPtr slf, string shpTypeS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpForS(UIntPtr slf, string shpForS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpOffsetVecS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpOffsetVecS(UIntPtr slf, string shpOffsetVecS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpDistribTypeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpDistribTypeS(UIntPtr slf, string shpDistribTypeS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getShpDistribWalkSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpDistribWalkSpeed(UIntPtr slf,
			float shpDistribWalkSpeed);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getShpIsVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpIsVolume(UIntPtr slf, int shpIsVolume);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpDimS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpDimS(UIntPtr slf, string shpDimS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpMeshS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpMeshS(UIntPtr slf, string shpMeshS);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getShpMeshRenderB(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpMeshRenderB(UIntPtr slf, int shpMeshRenderB);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpScaleKeysS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpScaleKeysS(UIntPtr slf, string shpScaleKeysS);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getShpScaleIsLooping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpScaleIsLooping(UIntPtr slf, int shpScaleIsLooping);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getShpScaleIsSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpScaleIsSmooth(UIntPtr slf, int shpScaleIsSmooth);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getShpScaleFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setShpScaleFps(UIntPtr slf, float shpScaleFps);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getDirModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setDirModeS(UIntPtr slf, string dirModeS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getDirForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setDirForS(UIntPtr slf, string dirForS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getDirModeTargetForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setDirModeTargetForS(UIntPtr slf, string dirModeTargetForS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getDirModeTargetPosS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setDirModeTargetPosS(UIntPtr slf, string dirModeTargetPosS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getDirAngleHead(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setDirAngleHead(UIntPtr slf, float dirAngleHead);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getDirAngleHeadVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setDirAngleHeadVar(UIntPtr slf, float dirAngleHeadVar);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getDirAngleElev(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setDirAngleElev(UIntPtr slf, float dirAngleElev);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getDirAngleElevVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setDirAngleElevVar(UIntPtr slf, float dirAngleElevVar);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVelAvg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVelAvg(UIntPtr slf, float velAvg);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVelVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVelVar(UIntPtr slf, float velVar);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getLspPartAvg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setLspPartAvg(UIntPtr slf, float lspPartAvg);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getLspPartVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setLspPartVar(UIntPtr slf, float lspPartVar);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getFlyGravityS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setFlyGravityS(UIntPtr slf, string flyGravityS);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getFlyColldetB(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setFlyColldetB(UIntPtr slf, int flyColldetB);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisNameS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisNameS(UIntPtr slf, string visNameS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisOrientationS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisOrientationS(UIntPtr slf, string visOrientationS);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getVisTexIsQuadpoly(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisTexIsQuadpoly(UIntPtr slf, int visTexIsQuadpoly);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVisTexAniFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisTexAniFps(UIntPtr slf, float visTexAniFps);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getVisTexAniIsLooping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisTexAniIsLooping(UIntPtr slf, int visTexAniIsLooping);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisTexColorStartS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisTexColorStartS(UIntPtr slf, string visTexColorStartS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisTexColorEndS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisTexColorEndS(UIntPtr slf, string visTexColorEndS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisSizeStartS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisSizeStartS(UIntPtr slf, string visSizeStartS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVisSizeEndScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisSizeEndScale(UIntPtr slf, float visSizeEndScale);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisAlphaFuncS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisAlphaFuncS(UIntPtr slf, string visAlphaFuncS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVisAlphaStart(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisAlphaStart(UIntPtr slf, float visAlphaStart);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVisAlphaEnd(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setVisAlphaEnd(UIntPtr slf, float visAlphaEnd);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getTrlFadeSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setTrlFadeSpeed(UIntPtr slf, float trlFadeSpeed);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getTrlTextureS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setTrlTextureS(UIntPtr slf, string trlTextureS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getTrlWidth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setTrlWidth(UIntPtr slf, float trlWidth);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getMrkFadesPeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setMrkFadesPeed(UIntPtr slf, float mrkFadesPeed);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getMrktExtureS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setMrktExtureS(UIntPtr slf, string mrktExtureS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getMrkSize(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setMrkSize(UIntPtr slf, float mrkSize);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getFlockMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setFlockMode(UIntPtr slf, string flockMode);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getFlockStrength(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setFlockStrength(UIntPtr slf, float flockStrength);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getUseEmittersFor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setUseEmittersFor(UIntPtr slf, int useEmittersFor);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getTimeStartEndS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setTimeStartEndS(UIntPtr slf, string timeStartEndS);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getMBiasAmbientPfx(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectInstance_setMBiasAmbientPfx(UIntPtr slf, int mBiasAmbientPfx);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getVisNameS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setVisNameS(UIntPtr slf, string visNameS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getVisSizeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setVisSizeS(UIntPtr slf, string visSizeS);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getVisAlpha(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setVisAlpha(UIntPtr slf, float visAlpha);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getVisAlphaBlendFuncS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setVisAlphaBlendFuncS(UIntPtr slf, string visAlphaBlendFuncS);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getVisTexAniFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setVisTexAniFps(UIntPtr slf, float visTexAniFps);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getVisTexAniIsLooping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setVisTexAniIsLooping(UIntPtr slf, int visTexAniIsLooping);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjModeS(UIntPtr slf, string emTrjModeS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjOriginNode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjOriginNode(UIntPtr slf, string emTrjOriginNode);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjTargetNode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjTargetNode(UIntPtr slf, string emTrjTargetNode);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjTargetRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjTargetRange(UIntPtr slf, float emTrjTargetRange);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjTargetAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjTargetAzi(UIntPtr slf, float emTrjTargetAzi);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjTargetElev(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjTargetElev(UIntPtr slf, float emTrjTargetElev);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmTrjNumKeys(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjNumKeys(UIntPtr slf, int emTrjNumKeys);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmTrjNumKeysVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjNumKeysVar(UIntPtr slf, int emTrjNumKeysVar);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjAngleElevVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjAngleElevVar(UIntPtr slf, float emTrjAngleElevVar);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjAngleHeadVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjAngleHeadVar(UIntPtr slf, float emTrjAngleHeadVar);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjKeyDistVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjKeyDistVar(UIntPtr slf, float emTrjKeyDistVar);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjLoopModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjLoopModeS(UIntPtr slf, string emTrjLoopModeS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjEaseFuncS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjEaseFuncS(UIntPtr slf, string emTrjEaseFuncS);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjEaseVel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjEaseVel(UIntPtr slf, float emTrjEaseVel);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjDynUpdateDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjDynUpdateDelay(UIntPtr slf, float emTrjDynUpdateDelay);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmTrjDynUpdateTargetOnly(UIntPtr slf,
			int emTrjDynUpdateTargetOnly);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCreateS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxCreateS(UIntPtr slf, string emFxCreateS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxInvestOriginS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxInvestOriginS(UIntPtr slf, string emFxInvestOriginS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxInvestTargetS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxInvestTargetS(UIntPtr slf, string emFxInvestTargetS);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmFxTriggerDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxTriggerDelay(UIntPtr slf, float emFxTriggerDelay);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmFxCreateDownTrj(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxCreateDownTrj(UIntPtr slf, int emFxCreateDownTrj);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmActionCollDynS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmActionCollDynS(UIntPtr slf, string emActionCollDynS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmActionCollStatS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmActionCollStatS(UIntPtr slf, string emActionCollStatS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollStatS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxCollStatS(UIntPtr slf, string emFxCollStatS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollDynS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxCollDynS(UIntPtr slf, string emFxCollDynS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollStatAlignS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxCollStatAlignS(UIntPtr slf, string emFxCollStatAlignS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollDynAlignS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxCollDynAlignS(UIntPtr slf, string emFxCollDynAlignS);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmFxLifespan(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxLifespan(UIntPtr slf, float emFxLifespan);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmCheckCollision(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmCheckCollision(UIntPtr slf, int emCheckCollision);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmAdjustShpToOrigin(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmAdjustShpToOrigin(UIntPtr slf, int emAdjustShpToOrigin);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmInvestNextKeyDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmInvestNextKeyDuration(UIntPtr slf,
			float emInvestNextKeyDuration);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmFlyGravity(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFlyGravity(UIntPtr slf, float emFlyGravity);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmSelfRotVelS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmSelfRotVelS(UIntPtr slf, string emSelfRotVelS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getLightPresetName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setLightPresetName(UIntPtr slf, string lightPresetName);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getSfxId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setSfxId(UIntPtr slf, string sfxId);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getSfxIsAmbient(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setSfxIsAmbient(UIntPtr slf, int sfxIsAmbient);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getSendAssessMagic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setSendAssessMagic(UIntPtr slf, int sendAssessMagic);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getSecsPerDamage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setSecsPerDamage(UIntPtr slf, float secsPerDamage);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollDynPercS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setEmFxCollDynPercS(UIntPtr slf, string emFxCollDynPercS);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getUserString(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkEffectBaseInstance_setUserString(UIntPtr slf, ulong i, string userString);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getVisNameS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setVisNameS(UIntPtr slf, string visNameS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getVisSizeScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setVisSizeScale(UIntPtr slf, float visSizeScale);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getScaleDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setScaleDuration(UIntPtr slf, float scaleDuration);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxPpsValue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxPpsValue(UIntPtr slf, float pfxPpsValue);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxPpsIsSmoothChg(UIntPtr slf,
			int pfxPpsIsSmoothChg);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxPpsIsLoopingChg(UIntPtr slf,
			int pfxPpsIsLoopingChg);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxScTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxScTime(UIntPtr slf, float pfxScTime);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxFlyGravityS(UIntPtr slf, string pfxFlyGravityS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxShpDimS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxShpDimS(UIntPtr slf, string pfxShpDimS);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxShpIsVolumeChg(UIntPtr slf,
			int pfxShpIsVolumeChg);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxShpScaleFps(UIntPtr slf, float pfxShpScaleFps);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxShpDistribWalksPeed(UIntPtr slf,
			float pfxShpDistribWalksPeed);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxShpOffsetVecS(UIntPtr slf,
			string pfxShpOffsetVecS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxShpDistribTypeS(UIntPtr slf,
			string pfxShpDistribTypeS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxDirModeS(UIntPtr slf, string pfxDirModeS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxDirForS(UIntPtr slf, string pfxDirForS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxDirModeTargetForS(UIntPtr slf,
			string pfxDirModeTargetForS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxDirModeTargetPosS(UIntPtr slf,
			string pfxDirModeTargetPosS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxVelAvg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxVelAvg(UIntPtr slf, float pfxVelAvg);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxLspPartAvg(UIntPtr slf, float pfxLspPartAvg);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setPfxVisAlphaStart(UIntPtr slf,
			float pfxVisAlphaStart);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getLightPresetName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setLightPresetName(UIntPtr slf,
			string lightPresetName);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getLightRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setLightRange(UIntPtr slf, float lightRange);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getSfxId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setSfxId(UIntPtr slf, string sfxId);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getSfxIsAmbient(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setSfxIsAmbient(UIntPtr slf, int sfxIsAmbient);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getEmCreateFxId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setEmCreateFxId(UIntPtr slf, string emCreateFxId);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getEmFlyGravity(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setEmFlyGravity(UIntPtr slf, float emFlyGravity);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setEmSelfRotVelS(UIntPtr slf, string emSelfRotVelS);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getEmTrjModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setEmTrjModeS(UIntPtr slf, string emTrjModeS);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setEmTrjEaseVel(UIntPtr slf, float emTrjEaseVel);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getEmCheckCollision(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void
			ZkParticleEffectEmitKeyInstance_setEmCheckCollision(UIntPtr slf, int emCheckCollision);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getEmFxLifespan(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectEmitKeyInstance_setEmFxLifespan(UIntPtr slf, float emFxLifespan);

		[DllImport(DllName)]
		public static extern FightAiMove ZkFightAiInstance_getMove(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkFightAiInstance_setMove(UIntPtr slf, ulong i, FightAiMove move);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoundEffectInstance_getFile(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundEffectInstance_setFile(UIntPtr slf, string file);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getPitchOff(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundEffectInstance_setPitchOff(UIntPtr slf, int pitchOff);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getPitchVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundEffectInstance_setPitchVar(UIntPtr slf, int pitchVar);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundEffectInstance_setVolume(UIntPtr slf, int volume);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getLoop(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundEffectInstance_setLoop(UIntPtr slf, int loop);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getLoopStartOffset(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundEffectInstance_setLoopStartOffset(UIntPtr slf, int loopStartOffset);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getLoopEndOffset(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundEffectInstance_setLoopEndOffset(UIntPtr slf, int loopEndOffset);

		[DllImport(DllName)]
		public static extern float ZkSoundEffectInstance_getReverbLevel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundEffectInstance_setReverbLevel(UIntPtr slf, float reverbLevel);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoundEffectInstance_getPfxName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundEffectInstance_setPfxName(UIntPtr slf, string pfxName);

		[DllImport(DllName)]
		public static extern float ZkSoundSystemInstance_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundSystemInstance_setVolume(UIntPtr slf, float volume);

		[DllImport(DllName)]
		public static extern int ZkSoundSystemInstance_getBitResolution(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundSystemInstance_setBitResolution(UIntPtr slf, int bitResolution);

		[DllImport(DllName)]
		public static extern int ZkSoundSystemInstance_getSampleRate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundSystemInstance_setSampleRate(UIntPtr slf, int sampleRate);

		[DllImport(DllName)]
		public static extern int ZkSoundSystemInstance_getUseStereo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundSystemInstance_setUseStereo(UIntPtr slf, int useStereo);

		[DllImport(DllName)]
		public static extern int ZkSoundSystemInstance_getNumSfxChannels(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundSystemInstance_setNumSfxChannels(UIntPtr slf, int numSfxChannels);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoundSystemInstance_getUsed3DProviderName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkSoundSystemInstance_setUsed3DProviderName(UIntPtr slf, string used3DProviderName);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMilGreetings(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getPalGreetings(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWeather(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getIGetYouStill(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getDieEnemy(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getDieMonster(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonDieMonster(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonDieMonster2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getDirtyThief(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHandsOff(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSheepKiller(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSheepKillerMonster(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouMurderer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getDieStupidBeast(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouDareHitMe(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouAskedForIt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getThenIBeatYouOutOfHere(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWhatDidYouDoInThere(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWillYouStopFighting(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getKillEnemy(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getEnemyKilled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMonsterKilled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonMonsterKilled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonMonsterKilled2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getThiefDown(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getRumfummlerDown(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSheepAttackerDown(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getKillMurderer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getStupidBeastKilled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNeverHitMeAgain(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouBetterShouldHaveListened(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGetUpAndBegone(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNeverEnterRoomAgain(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getThereIsNoFightingHere(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSpareMe(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getRunAway(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAlarm(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGuards(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHelp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGoodMonsterKill(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGoodKill(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNotNow(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getRunCoward(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGetOutOfHere(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWhyAreYouInHere(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYesGoOutOfHere(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWhatsThisSupposedToBe(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouDisturbedMySlumber(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getITookYourGold(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getShitNoGold(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getITakeYourWeapon(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWhatAreYouDoing(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getLookingForTroubleAgain(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getStopMagic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getISaidStopMagic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWeaponDown(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getISaidWeaponDown(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWiseMove(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNextTimeYoureInForIt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOhMyHead(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTheresAFight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOhMyGodItsAFight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGoodVictory(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNotBad(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOhMyGodHesDown(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getCheerFriend01(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getCheerFriend02(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getCheerFriend03(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOoh01(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOoh02(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOoh03(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWhatWasThat(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGetOutOfMyBed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAwake(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAbsCommander(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAbsMonastery(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAbsFarm(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAbsGood(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSheepKillerCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAttackCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTheftCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMurderCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getPalCityCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMilCityCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getCityCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMonaCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getFarmCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOcCrime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getToughguyAttackLost(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getToughguyAttackWon(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getToughguyPlayerAttack(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold1000(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold950(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold900(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold850(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold800(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold750(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold700(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold650(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold600(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold550(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold500(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold450(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold400(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold350(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold300(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold250(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold200(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold150(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold100(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold90(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold80(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold70(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold60(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold50(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold40(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold30(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold20(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGold10(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk01(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk02(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk03(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk04(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk05(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk06(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk07(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk08(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk09(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk10(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk11(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk12(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk13(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk14(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk15(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk16(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk17(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk18(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk19(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk20(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk21(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk22(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk23(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk24(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk25(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk26(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk27(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk28(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk29(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSmalltalk30(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNoLearnNoPoints(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNoLearnOverPersonalMax(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNoLearnYoureBetter(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouLearnedSomething(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getUnterstadt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOberstadt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTempel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMarkt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGalgen(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getKaserne(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHafen(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWhereto(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOberstadt2Unterstadt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getUnterstadt2Oberstadt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getUnterstadt2Tempel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getUnterstadt2Hafen(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTempel2Unterstadt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTempel2Markt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTempel2Galgen(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMarkt2Tempel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMarkt2Kaserne(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMarkt2Galgen(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGalgen2Tempel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGalgen2Markt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGalgen2Kaserne(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getKaserne2Markt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getKaserne2Galgen(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHafen2Unterstadt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getDead(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAargh1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAargh2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAargh3(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonWrongArmor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonWrongArmorSld(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonWrongArmorMil(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonWrongArmorKdf(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonNoArmorBdt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonDieBandit(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonDirtyPirate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getScHeyTurnAround(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getScHeyTurnAround02(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getScHeyTurnAround03(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getScHeyTurnAround04(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getScHeyWaitASecond(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getDoesntMork(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getPickBroke(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNeedKey(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNoMorePicks(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNoPickLockTalent(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNoSweeping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getPickLockOrKeyMissing(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getKeyMissing(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getPickLockMissing(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNeverOpen(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMissingItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getDontKnow(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNothingToGet(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNothingToGet02(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNothingToGet03(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHealShrine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHealLastShrine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getIrdorathThereYouAre(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getScOpensIrdorathBook(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getScOpensLastDoor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTrade1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTrade2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTrade3(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getVerstehe(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getFoundTreasure(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getCantUnderstandThis(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getCantReadThis(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getStoneplate1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getStoneplate2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getStoneplate3(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getCough(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHui(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonThisLittleBastard(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonOpenAdanosTemple(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAttentatAddonDescription(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAttentatAddonDescription2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAttentatAddonPro(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAttentatAddonContra(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMineAddonDescription(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonSummonAncientGhost(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonAncientGhostNotNear(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAddonGoldDescription(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWatchYourAim(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWatchYourAimAngry(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getLetsForgetOurLittleFight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getStrange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getDieMortalEnemy(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNowWait(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNowWaitIntruder(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouStillNotHaveEnough(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouAttackedMyCharge(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getIWillTeachYouRespectForForeignProperty(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouKilledOneOfUs(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getBerzerk(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYoullBeSorryForThis(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYesYes(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getShitWhatAMonster(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWeWillMeetAgain(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNeverTryThatAgain(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getITookYourOre(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getShitNoOre(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouViolatedForbiddenTerritory(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouWannaFoolMe(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWhatDidYouInThere(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getIntruderAlert(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getBehindYou(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHeyHeyHey(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getCheerFight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getCheerFriend(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOoh(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYeahWellDone(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHeDefeatedhim(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHeDeservEdit(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHeKilledHim(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getItWasAGoodFight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getFriendlyGreetings(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getAlGreetings(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMageGreetings(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSectGreetings(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getThereHeIs(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNoLearnOverMax(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getNoLearnYouAlreadyKnow(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getHeyYou(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWhatDoYouWant(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getISaidWhatDoYouWant(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getMakeWay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOutOfMyWay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouDeafOrWhat(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getLookAway(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOkayKeepIt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getWhatsThat(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getThatsMyWeapon(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGiveItTome(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouCanKeepTheCrap(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getTheyKilledMyFriend(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSuckerGotSome(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSuckerDefeatedEbr(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSuckerDefeatedGur(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSuckerDefeatedMage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSuckerDefeatedNovGuard(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getSuckerDefeatedVlkGuard(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouDefeatedMyComrade(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouDefeatedNovGuard(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouDefeatedVlkGuard(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouStoleFromMe(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouStoleFromUs(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouStoleFromEbr(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouStoleFromGur(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getStoleUromMage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouKilledmyfriend(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouKilledEbr(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouKilledGur(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouKilledMage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouKilledOcFolk(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouKilledNcFolk(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouKilledPsiFolk(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getGetThingsRight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getYouDefeatedMeWell(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSvmInstance_getOm(UIntPtr slf);

		public class Callbacks
		{
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkAttachmentEnumerator(IntPtr ctx, IntPtr name, UIntPtr mesh);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkCutsceneBlockEnumerator(IntPtr ctx, UIntPtr block);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkDaedalusSymbolEnumerator(IntPtr ctx, UIntPtr symbol);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate void ZkDaedalusVmExternalCallback(IntPtr ctx, UIntPtr vm);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate void ZkDaedalusVmExternalDefaultCallback(IntPtr ctx, UIntPtr vm, UIntPtr sym);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate void ZkLogger(IntPtr ctx, LogLevel lvl, string name, string message);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkStringEnumerator(IntPtr ctx, IntPtr value);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkTriggerListTargetEnumerator(UIntPtr ctx, UIntPtr target);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkVfsNodeEnumerator(IntPtr ctx, UIntPtr node);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkVirtualObjectEnumerator(UIntPtr ctx, UIntPtr vob);
		}

		public class Structs
		{
			[StructLayout(LayoutKind.Sequential, Size = 4)]
			public struct ZkColor
			{
				public byte R;
				public byte G;
				public byte B;
				public byte A;

				public ZkColor(Color c)
				{
					R = c.R;
					G = c.G;
					B = c.B;
					A = c.A;
				}

				public Color ToColor()
				{
					return Color.FromArgb(A, R, G, B);
				}
			}

			[StructLayout(LayoutKind.Sequential, Size = 4 * 9)]
			public struct ZkMat3x3
			{
				public float m00,
					m01,
					m02,
					m10,
					m11,
					m12,
					m20,
					m21,
					m22;

				public static ZkMat3x3 FromPublicMatrix(Matrix3x3 mat)
				{
					return new ZkMat3x3
					{
						m00 = mat.M11,
						m10 = mat.M12,
						m20 = mat.M13,
						m01 = mat.M21,
						m11 = mat.M22,
						m21 = mat.M23,
						m02 = mat.M31,
						m12 = mat.M32,
						m22 = mat.M33
					};
				}

				public Matrix3x3 ToPublicMatrix()
				{
					return new Matrix3x3(
						m00,
						m10,
						m20,
						m01,
						m11,
						m21,
						m02,
						m12,
						m22
					);
				}
			}

			[StructLayout(LayoutKind.Sequential, Size = 4 * 16)]
			public struct ZkMat4x4
			{
				public float m00,
					m01,
					m02,
					m03,
					m10,
					m11,
					m12,
					m13,
					m20,
					m21,
					m22,
					m23,
					m30,
					m31,
					m32,
					m33;

				public ZkMat4x4(Matrix4x4 v)
				{
					m00 = v.M11;
					m01 = v.M21;
					m02 = v.M31;
					m03 = v.M41;
					m10 = v.M12;
					m11 = v.M22;
					m12 = v.M32;
					m13 = v.M42;
					m20 = v.M13;
					m21 = v.M23;
					m22 = v.M33;
					m23 = v.M43;
					m30 = v.M14;
					m31 = v.M24;
					m32 = v.M34;
					m33 = v.M44;
				}

				public Matrix4x4 ToCSharp()
				{
					return new Matrix4x4(
						m00,
						m10,
						m20,
						m30,
						m01,
						m11,
						m21,
						m31,
						m02,
						m12,
						m22,
						m32,
						m03,
						m13,
						m23,
						m33
					);
				}
			}

			[StructLayout(LayoutKind.Sequential, Size = 16)]
			public struct ZkDate
			{
				[MarshalAs(UnmanagedType.U4)] public uint year;
				[MarshalAs(UnmanagedType.U2)] public ushort month;
				[MarshalAs(UnmanagedType.U2)] public ushort day;
				[MarshalAs(UnmanagedType.U2)] public ushort hour;
				[MarshalAs(UnmanagedType.U2)] public ushort minute;
				[MarshalAs(UnmanagedType.U2)] public ushort second;

				public DateTime? AsDateTime()
				{
					try
					{
						return new DateTime((int)year, month, day, hour, minute, second);
					}
					catch (Exception)
					{
						return null;
					}
				}
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct ZkReadExt
			{
				public delegate ulong ReadFn(UIntPtr ctx, IntPtr buf, ulong len);

				public delegate ulong SeekFn(UIntPtr ctx, long off, Whence whence);

				public delegate ulong TellFn(UIntPtr ctx);

				public delegate bool EofFn(UIntPtr ctx);

				public delegate bool DelFn(UIntPtr ctx);

				public ReadFn read;
				public SeekFn seek;
				public TellFn tell;
				public EofFn eof;
				public DelFn del;
			}
		}
	}
}