using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using ZenKit.Daedalus;
using static ZenKit.Native.Callbacks;
using static ZenKit.Native.Structs;
using ZenKit.Vobs;

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
	}
	
	internal static class Native
	{
		private const string DllName = "zenkitcapi";

		[DllImport(DllName)]
		public static extern float ZkAnimation_getFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkAnimation_getSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkLogger_set(LogLevel lvl, Callbacks.ZkLogger logger, UIntPtr ctx);

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
		public static extern UIntPtr ZkRead_newExt(Structs.ZkReadExt ext, UIntPtr ctx);

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
		public static extern void ZkVfsNode_enumerateChildren(UIntPtr slf, Callbacks.ZkVfsNodeEnumerator callback, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneLibrary_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneLibrary_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneLibrary_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkCutsceneLibrary_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneLibrary_getBlock(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern void ZkCutsceneLibrary_enumerateBlocks(UIntPtr slf, Callbacks.ZkCutsceneBlockEnumerator cb,
			UIntPtr ctx);

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
		public static extern void ZkFont_enumerateGlyphs(UIntPtr slf, Callbacks.ZkFontGlyphEnumerator cb, UIntPtr ctx);

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
		public static extern Structs.ZkDate ZkModelAnimation_getSourceDate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelAnimation_getSourceScript(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkModelAnimation_getSampleCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AnimationSample ZkModelAnimation_getSample(UIntPtr slf,
			[MarshalAs(UnmanagedType.U8)] ulong i);

		[DllImport(DllName)]
		public static extern void
			ZkModelAnimation_enumerateSamples(UIntPtr slf, Callbacks.ZkAnimationSampleEnumerator cb, UIntPtr ctx);

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
		public static extern Structs.ZkDate ZkModelHierarchy_getSourceDate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkModelHierarchy_getSourcePath(UIntPtr slf);

		[DllImport(DllName)]
		public static extern void ZkModelHierarchy_enumerateNodes(UIntPtr slf, Callbacks.ZkModelHierarchyNodeEnumerator cb,
			UIntPtr ctx);

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
		public static extern void ZkOrientedBoundingBox_enumerateChildren(UIntPtr slf,
			Callbacks.ZkOrientedBoundingBoxEnumerator cb,
			UIntPtr ctx);

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
		public static extern Structs.ZkColor ZkMaterial_getColor(UIntPtr slf);

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
		public static extern IntPtr ZkMultiResolutionMesh_getPositions(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkMultiResolutionMesh_getNormals(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkMultiResolutionMesh_getSubMeshCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMultiResolutionMesh_getSubMesh(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMultiResolutionMesh_enumerateSubMeshes(UIntPtr slf, Callbacks.ZkSubMeshEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern ulong ZkMultiResolutionMesh_getMaterialCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMultiResolutionMesh_getMaterial(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void
			ZkMultiResolutionMesh_enumerateMaterials(UIntPtr slf, Callbacks.ZkMaterialEnumerator cb, UIntPtr ctx);

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
		public static extern IntPtr ZkSubMesh_getWedges(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getColors(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getTrianglePlaneIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkSubMesh_getTrianglePlanes(UIntPtr slf, out ulong count);

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
		public static extern void ZkSoftSkinMesh_enumerateBoundingBoxes(UIntPtr slf, Callbacks.ZkOrientedBoundingBoxEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoftSkinMesh_getWeights(UIntPtr slf, ulong node, out ulong count);

		[DllImport(DllName)]
		public static extern void ZkSoftSkinMesh_enumerateWeights(UIntPtr slf, Callbacks.ZkSoftSkinWeightEnumerator cb,
			UIntPtr node);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoftSkinMesh_getWedgeNormals(UIntPtr slf, out ulong count);

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
		public static extern void ZkModelMesh_enumerateMeshes(UIntPtr slf, Callbacks.ZkSoftSkinMeshEnumerator cb, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern ulong ZkModelMesh_getAttachmentCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkModelMesh_getAttachment(UIntPtr slf, string name);

		[DllImport(DllName)]
		public static extern void ZkModelMesh_enumerateAttachments(UIntPtr slf, Callbacks.ZkAttachmentEnumerator cb, UIntPtr ctx);

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
		public static extern IntPtr ZkTexture_getPalette(UIntPtr slf, out ulong size);

		[DllImport(DllName)]
		public static extern IntPtr ZkTexture_getMipmapRaw(UIntPtr slf, ulong level, out ulong size);

		[DllImport(DllName)]
		public static extern ulong ZkTexture_getMipmapRgba(UIntPtr slf, ulong level, byte[] buf, ulong size);

		[DllImport(DllName)]
		public static extern void ZkTexture_enumerateRawMipmaps(UIntPtr slf, Callbacks.ZkTextureMipmapEnumerator cb, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void
			ZkTexture_enumerateRgbaMipmaps(UIntPtr slf, Callbacks.ZkTextureMipmapEnumerator cb, UIntPtr ctx);

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
		public static extern IntPtr ZkMorphMesh_getMorphPositions(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkMorphMesh_getAnimationCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMorphMesh_getAnimation(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMorphMesh_enumerateAnimations(UIntPtr slf, Callbacks.ZkMorphAnimationEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern ulong ZkMorphMesh_getSourceCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMorphMesh_getSource(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMorphMesh_enumerateSources(UIntPtr slf, Callbacks.ZkMorphSourceEnumerator cb, UIntPtr ctx);

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
		public static extern IntPtr ZkMorphAnimation_getSamples(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkMorphSource_getFileName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Structs.ZkDate ZkMorphSource_getFileDate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_load(UIntPtr buf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_loadPath(string path);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_loadVfs(UIntPtr vfs, string name);

		[DllImport(DllName)]
		public static extern void ZkMesh_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Structs.ZkDate ZkMesh_getSourceDate(UIntPtr slf);

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
		public static extern void ZkMesh_enumerateMaterials(UIntPtr slf, Callbacks.ZkMaterialEnumerator cb, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern IntPtr ZkMesh_getPositions(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkMesh_getVertices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkMesh_getLightMapCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_getLightMap(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMesh_enumerateLightMaps(UIntPtr slf, Callbacks.ZkLightMapEnumerator cb, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern ulong ZkMesh_getPolygonCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMesh_getPolygon(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkMesh_enumeratePolygons(UIntPtr slf, Callbacks.ZkPolygonEnumerator cb, UIntPtr ctx);

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
		public static extern IntPtr ZkPolygon_getPolygonIndices(UIntPtr slf, out ulong count);

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
		public static extern void ZkModelScript_enumerateAnimationCombines(UIntPtr slf, Callbacks.ZkAnimationCombineEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkModelScript_enumerateMeshes(UIntPtr slf, Callbacks.ZkStringEnumerator cb, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkModelScript_enumerateDisabledAnimations(UIntPtr slf, Callbacks.ZkStringEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkModelScript_enumerateAnimationBlends(UIntPtr slf, Callbacks.ZkAnimationBlendEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkModelScript_enumerateAnimationAliases(UIntPtr slf, Callbacks.ZkAnimationAliasEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkModelScript_enumerateModelTags(UIntPtr slf, Callbacks.ZkStringEnumerator cb, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkModelScript_enumerateAnimations(UIntPtr slf, Callbacks.ZkAnimationEnumerator cb, UIntPtr ctx);

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
		public static extern void ZkAnimation_enumerateEventTags(UIntPtr slf, Callbacks.ZkEventTagEnumerator cb, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkAnimation_enumerateParticleEffects(UIntPtr slf, Callbacks.ZkEventParticlEffectEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkAnimation_enumerateParticleEffectStops(UIntPtr slf,
			Callbacks.ZkEventParticleEffectStopEnumerator cb, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkAnimation_enumerateSoundEffects(UIntPtr slf, Callbacks.ZkEventSoundEffectEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkAnimation_enumerateSoundEffectGrounds(UIntPtr slf,
			Callbacks.ZkEventSoundEffectGroundEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkAnimation_enumerateMorphAnimations(UIntPtr slf, Callbacks.ZkEventMorphAnimationEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkAnimation_enumerateCameraTremors(UIntPtr slf, Callbacks.ZkEventCameraTremorEnumerator cb,
			UIntPtr ctx);

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
		public static extern IntPtr ZkBspTree_getLightPoints(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkBspTree_getLeafNodeIndices(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkBspTree_getNodes(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern ulong ZkBspTree_getSectorCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkBspTree_getSector(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkBspTree_enumerateSectors(UIntPtr slf, Callbacks.ZkBspSectorEnumerator cb, UIntPtr ctx);

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
		public static extern void ZkWayNet_enumeratePoints(UIntPtr slf, Callbacks.ZkWayPointEnumerator cb, UIntPtr ctx);

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
		public static extern void ZkWorld_enumerateRootObjects(UIntPtr slf, Callbacks.ZkVirtualObjectEnumerator cb, UIntPtr ctx);

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
		public static extern Vector3 ZkVirtualObject_getPosition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Structs.ZkMat3x3 ZkVirtualObject_getRotation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getShowVisual(UIntPtr slf);

		[DllImport(DllName)]
		public static extern SpriteAlignment ZkVirtualObject_getSpriteCameraFacingMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getCdStatic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getCdDynamic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getVobStatic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ShadowType ZkVirtualObject_getDynamicShadows(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getPhysicsEnabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AnimationType ZkVirtualObject_getAnimMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkVirtualObject_getBias(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkVirtualObject_getAmbient(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkVirtualObject_getAnimStrength(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkVirtualObject_getFarClipScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkVirtualObject_getPresetName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkVirtualObject_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkVirtualObject_getVisualName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern VisualType ZkVirtualObject_getVisualType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVirtualObject_getVisualDecal(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkVirtualObject_getChildCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkVirtualObject_getChild(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkVirtualObject_enumerateChildren(UIntPtr slf, Callbacks.ZkVirtualObjectEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern IntPtr ZkDecal_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector2 ZkDecal_getDimension(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector2 ZkDecal_getOffset(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkDecal_getTwoSided(UIntPtr slf);

		[DllImport(DllName)]
		public static extern AlphaFunction ZkDecal_getAlphaFunc(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkDecal_getTextureAnimFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern byte ZkDecal_getAlphaWeight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkDecal_getIgnoreDaylight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneCamera_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneCamera_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern CameraTrajectory ZkCutsceneCamera_getTrajectoryFOR(UIntPtr slf);

		[DllImport(DllName)]
		public static extern CameraTrajectory ZkCutsceneCamera_getTargetTrajectoryFOR(UIntPtr slf);

		[DllImport(DllName)]
		public static extern CameraLoopType ZkCutsceneCamera_getLoopMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern CameraLerpType ZkCutsceneCamera_getLerpMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getIgnoreFORVobRotation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getIgnoreFORVobRotationTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getAdapt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getEaseFirst(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getEaseLast(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCutsceneCamera_getTotalDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkCutsceneCamera_getAutoFocusVob(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getAutoPlayerMovable(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCutsceneCamera_getAutoUntriggerLast(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCutsceneCamera_getAutoUntriggerLastDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkCutsceneCamera_getPositionCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkCutsceneCamera_getTargetCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkCutsceneCamera_getFrameCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCutsceneCamera_getFrame(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkCutsceneCamera_enumerateFrames(UIntPtr slf, Callbacks.ZkCameraTrajectoryFrameEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getRollAngle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getFovScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionTypeFov(UIntPtr slf);

		[DllImport(DllName)]
		public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionTypeRoll(UIntPtr slf);

		[DllImport(DllName)]
		public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionTypeTimeScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getTension(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getCamBias(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getContinuity(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraTrajectoryFrame_getTimeScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCameraTrajectoryFrame_getTimeFixed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Structs.ZkMat4x4 ZkCameraTrajectoryFrame_getOriginalPose(UIntPtr slf);

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
		public static extern LightType ZkLightPreset_getLightType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkLightPreset_getRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Structs.ZkColor ZkLightPreset_getColor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkLightPreset_getConeAngle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getIsStatic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern LightQuality ZkLightPreset_getQuality(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLightPreset_getLensflareFx(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getOn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLightPreset_getRangeAnimationScale(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern float ZkLightPreset_getRangeAnimationFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getRangeAnimationSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLightPreset_getColorAnimationList(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern float ZkLightPreset_getColorAnimationFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getColorAnimationSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLightPreset_getCanMove(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLight_getPreset(UIntPtr slf);

		[DllImport(DllName)]
		public static extern LightType ZkLight_getLightType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkLight_getRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Structs.ZkColor ZkLight_getColor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkLight_getConeAngle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLight_getIsStatic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern LightQuality ZkLight_getQuality(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLight_getLensflareFx(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLight_getOn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLight_getRangeAnimationScale(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern float ZkLight_getRangeAnimationFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLight_getRangeAnimationSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLight_getColorAnimationList(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern float ZkLight_getColorAnimationFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLight_getColorAnimationSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkLight_getCanMove(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimate_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkAnimate_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkAnimate_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkAnimate_getStartOn(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkItem_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkItem_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkItem_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkItem_getInstance(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkLensFlare_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkLensFlare_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkLensFlare_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkLensFlare_getEffect(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkParticleEffectController_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkParticleEffectController_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkParticleEffectController_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectController_getEffectName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkParticleEffectController_getKillWhenDone(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkParticleEffectController_getInitiallyRunning(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMessageFilter_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMessageFilter_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkMessageFilter_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMessageFilter_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MessageFilterAction ZkMessageFilter_getOnTrigger(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MessageFilterAction ZkMessageFilter_getOnUntrigger(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCodeMaster_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkCodeMaster_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkCodeMaster_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCodeMaster_getOrdered(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCodeMaster_getFirstFalseIsFailure(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkCodeMaster_getFailureTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkCodeMaster_getUntriggeredCancels(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkCodeMaster_getSlaveCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkCodeMaster_getSlave(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkCodeMaster_enumerateSlaves(UIntPtr slf, Callbacks.ZkStringEnumerator cb, UIntPtr ctx);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMoverController_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMoverController_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkMoverController_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMoverController_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MoverMessageType ZkMoverController_getMessage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMoverController_getKey(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTouchDamage_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTouchDamage_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTouchDamage_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkTouchDamage_getDamage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsBarrier(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsBlunt(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsEdge(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsFire(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsFly(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsMagic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsPoint(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTouchDamage_getIsFall(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkTouchDamage_getRepeatDelaySeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkTouchDamage_getVolumeScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern TouchCollisionType ZkTouchDamage_getCollisionType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkEarthquake_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkEarthquake_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkEarthquake_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEarthquake_getRadius(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEarthquake_getDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Vector3 ZkEarthquake_getAmplitude(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMovableObject_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMovableObject_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkMovableObject_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMovableObject_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMovableObject_getHp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMovableObject_getDamage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMovableObject_getMovable(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMovableObject_getTakable(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMovableObject_getFocusOverride(UIntPtr slf);

		[DllImport(DllName)]
		public static extern SoundMaterialType ZkMovableObject_getMaterial(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMovableObject_getVisualDestroyed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMovableObject_getOwner(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMovableObject_getOwnerGuild(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMovableObject_getDestroyed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkInteractiveObject_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkInteractiveObject_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkInteractiveObject_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkInteractiveObject_getState(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkInteractiveObject_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkInteractiveObject_getItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkInteractiveObject_getConditionFunction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkInteractiveObject_getOnStateChangeFunction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkInteractiveObject_getRewind(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkFire_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkFire_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkFire_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkFire_getSlot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkFire_getVobTree(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkContainer_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkContainer_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkContainer_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkContainer_getIsLocked(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkContainer_getKey(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkContainer_getPickString(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkContainer_getContents(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDoor_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkDoor_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkDoor_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkDoor_getIsLocked(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkDoor_getKey(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkDoor_getPickString(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSound_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSound_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkSound_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSound_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern SoundMode ZkSound_getMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSound_getRandomDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSound_getRandomDelayVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkSound_getInitiallyPlaying(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkSound_getAmbient3d(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkSound_getObstruction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSound_getConeAngle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern SoundTriggerVolumeType ZkSound_getVolumeType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSound_getRadius(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSound_getSoundName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSoundDaytime_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkSoundDaytime_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkSoundDaytime_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSoundDaytime_getStartTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSoundDaytime_getEndTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoundDaytime_getSoundNameDaytime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTrigger_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTrigger_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTrigger_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTrigger_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern byte ZkTrigger_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern byte ZkTrigger_getFilterFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTrigger_getVobTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkTrigger_getMaxActivationCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkTrigger_getRetriggerDelaySeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkTrigger_getDamageThreshold(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkTrigger_getFireDelaySeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMover_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkMover_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkMover_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MoverBehavior ZkMover_getBehavior(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMover_getTouchBlockerDamage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMover_getStayOpenTimeSeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMover_getIsLocked(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMover_getAutoLink(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkMover_getAutoRotate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMover_getSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MoverLerpType ZkMover_getLerpType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MoverSpeedType ZkMover_getSpeedType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getKeyframes(UIntPtr slf, out ulong count);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxOpenStart(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxOpenEnd(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxTransitioning(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxCloseStart(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxCloseEnd(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxLock(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxUnlock(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMover_getSfxUseLocked(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerList_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerList_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerList_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern TriggerBatchMode ZkTriggerList_getMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern ulong ZkTriggerList_getTargetCount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerList_getTarget(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern void ZkTriggerList_enumerateTargets(UIntPtr slf, Callbacks.ZkTriggerListTargetEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerListTarget_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkTriggerListTarget_getDelaySeconds(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerScript_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerScript_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerScript_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerScript_getFunction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerChangeLevel_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerChangeLevel_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerChangeLevel_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerChangeLevel_getLevelName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerChangeLevel_getStartVob(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerWorldStart_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerWorldStart_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerWorldStart_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerWorldStart_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkTriggerWorldStart_getFireOnce(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerUntouch_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkTriggerUntouch_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkTriggerUntouch_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkTriggerUntouch_getTarget(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneMusic_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneMusic_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkZoneMusic_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkZoneMusic_getIsEnabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkZoneMusic_getPriority(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkZoneMusic_getIsEllipsoid(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkZoneMusic_getReverb(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkZoneMusic_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkZoneMusic_getIsLoop(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneFarPlane_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneFarPlane_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkZoneFarPlane_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkZoneFarPlane_getVobFarPlaneZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkZoneFarPlane_getInnerRangePercentage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneFog_load(UIntPtr buf, GameVersion version);

		[DllImport(DllName)]
		public static extern UIntPtr ZkZoneFog_loadPath(string path, GameVersion version);

		[DllImport(DllName)]
		public static extern void ZkZoneFog_del(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkZoneFog_getRangeCenter(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkZoneFog_getInnerRangePercentage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern Structs.ZkColor ZkZoneFog_getColor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkZoneFog_getFadeOutSky(UIntPtr slf);

		[DllImport(DllName)]
		public static extern bool ZkZoneFog_getOverrideColor(UIntPtr slf);

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
		public static extern void ZkDaedalusScript_enumerateSymbols(UIntPtr slf, Callbacks.ZkDaedalusSymbolEnumerator cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkDaedalusScript_enumerateInstanceSymbols(UIntPtr slf, string className,
			Callbacks.ZkDaedalusSymbolEnumerator cb, UIntPtr ctx);

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
		public static extern UIntPtr ZkDaedalusVm_initInstance(UIntPtr slf, UIntPtr sym, DaedalusInstanceType type);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_registerExternal(UIntPtr slf, UIntPtr sym,
			Callbacks.ZkDaedalusVmExternalCallback cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_registerExternalDefault(UIntPtr slf,
			Callbacks.ZkDaedalusVmExternalDefaultCallback cb,
			UIntPtr ctx);

		[DllImport(DllName)]
		public static extern void ZkDaedalusVm_printStackTrace(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getWaterDepthKnee(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getWaterDepthChest(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getJumpUpHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getSwimTime(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getDiveTime(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getStepHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getJumpLowHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getJumpMidHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getSlideAngle(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getSlideAngle2(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getDisableAutoRoll(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getSurfaceAlign(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getClimbHeadingAngle(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getClimbHorizAngle(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getClimbGroundAngle(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRangeBase(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRangeFist(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRangeG(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRange1Hs(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRange1Ha(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRange2Hs(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFightRange2Ha(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFallDownHeight(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getFallDownDamage(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getBloodDisabled(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getBloodMaxDistance(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getBloodAmount(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getBloodFlow(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkGuildValuesInstance_getTurnSpeed(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkGuildValuesInstance_getBloodEmitter(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkGuildValuesInstance_getBloodTexture(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getSlot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getEffect(UIntPtr slf);

		[DllImport(DllName)]
		public static extern NpcType ZkNpcInstance_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern uint ZkNpcInstance_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getDamageType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getGuild(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getLevel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getFightTactic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getWeapon(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getVoice(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getVoicePitch(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getBodyMass(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getDailyRoutine(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getStartAiState(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getSpawnPoint(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getSpawnDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getSenses(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getSensesRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getWp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getExp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getExpNext(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getLp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getBodyStateInterruptableOverride(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getNoFocus(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkNpcInstance_getName(UIntPtr slf, NpcNameSlot slot);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getMission(UIntPtr slf, NpcMissionSlot slot);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getAttribute(UIntPtr slf, NpcAttribute attribute);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getHitChance(UIntPtr slf, NpcTalent talent);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getProtection(UIntPtr slf, DamageType type);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getDamage(UIntPtr slf, DamageType type);

		[DllImport(DllName)]
		public static extern int ZkNpcInstance_getAiVar(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkMissionInstance_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMissionInstance_getDescription(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getImportant(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getOfferConditions(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getOffer(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getSuccessConditions(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getSuccess(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getFailureConditions(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getFailure(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getObsoleteConditions(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getObsolete(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMissionInstance_getRunning(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getNameId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getHp(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getHpMax(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMainFlag(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getWeight(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getValue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getDamageType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getDamageTotal(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getWear(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getNutrition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMagic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOnEquip(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOnUnequip(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOwner(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOwnerGuild(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getDisguiseGuild(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getVisual(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getVisualChange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getEffect(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getVisualSkin(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getSchemeName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMaterial(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMunition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getSpell(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getMagCircle(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getDescription(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvZBias(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvRotX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvRotY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvRotZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getInvAnimate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getDamage(UIntPtr slf, DamageType type);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getProtection(UIntPtr slf, DamageType type);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getCondAtr(UIntPtr slf, ItemConditionSlot slot);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getCondValue(UIntPtr slf, ItemConditionSlot slot);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getChangeAtr(UIntPtr slf, ItemConditionSlot slot);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getChangeValue(UIntPtr slf, ItemConditionSlot slot);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getOnState(UIntPtr slf, ItemStateSlot slot);

		[DllImport(DllName)]
		public static extern IntPtr ZkItemInstance_getText(UIntPtr slf, ItemTextSlot slot);

		[DllImport(DllName)]
		public static extern int ZkItemInstance_getCount(UIntPtr slf, ItemTextSlot slot);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcLongrange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcRange1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcRange2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcElevdo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getNpcElevup(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkFocusInstance_getNpcPrio(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemRange1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemRange2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemElevdo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getItemElevup(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkFocusInstance_getItemPrio(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobRange1(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobRange2(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobElevdo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkFocusInstance_getMobElevup(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkFocusInstance_getMobPrio(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getNpc(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getNr(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getImportant(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getCondition(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getInformation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkInfoInstance_getDescription(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getTrade(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkInfoInstance_getPermanent(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getNpc(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getTradeItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getTradeAmount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getRequestedCategory(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getRequestedItem(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getRequestedAmount(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkItemReactInstance_getReaction(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSpellInstance_getTimePerMana(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getDamagePerLevel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getDamageType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getSpellType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getCanTurnDuringInvest(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getCanChangeTargetDuringInvest(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getIsMultiEffect(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectAlgo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSpellInstance_getTargetCollectElevation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuInstance_getBackPic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuInstance_getBackWorld(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getPosX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getPosY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getDimX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getDimY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getAlpha(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuInstance_getMusicTheme(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getEventTimerMsec(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getDefaultOutgame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuInstance_getDefaultIngame(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuInstance_getItem(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getFontName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getBackpic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getAlphaMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getAlpha(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MenuItemType ZkMenuItemInstance_getType(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getOnChgSetOption(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getOnChgSetOptionSection(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getPosX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getPosY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getDimX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getDimY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMenuItemInstance_getSizeStartScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFlags(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMenuItemInstance_getOpenDelayTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMenuItemInstance_getOpenDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFramePosX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFramePosY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFrameSizeX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getFrameSizeY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getHideIfOptionSectionSet(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getHideIfOptionSet(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getHideOnValue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getText(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getOnSelAction(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getOnSelActionS(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern int ZkMenuItemInstance_getOnEventAction(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern float ZkMenuItemInstance_getUserFloat(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkMenuItemInstance_getUserString(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getBestRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMinRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMaxRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getBestElevation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMinElevation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMaxElevation(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getBestAzimuth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMinAzimuth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMaxAzimuth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getBestRotZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMinRotZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getMaxRotZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getRotOffsetX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getRotOffsetY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getRotOffsetZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getTargetOffsetX(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getTargetOffsetY(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getTargetOffsetZ(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getVeloTrans(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkCameraInstance_getVeloRot(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkCameraInstance_getTranslate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkCameraInstance_getRotate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkCameraInstance_getCollision(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMusicSystemInstance_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getBitResolution(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getGlobalReverbEnabled(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getSampleRate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getNumChannels(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMusicSystemInstance_getReverbBufferSize(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMusicThemeInstance_getFile(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMusicThemeInstance_getVol(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMusicThemeInstance_getLoop(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMusicThemeInstance_getReverbmix(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMusicThemeInstance_getReverbtime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MusicTransitionEffect ZkMusicThemeInstance_getTranstype(UIntPtr slf);

		[DllImport(DllName)]
		public static extern MusicTransitionType ZkMusicThemeInstance_getTranssubtype(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkMusicJingleInstance_getName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMusicJingleInstance_getLoop(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkMusicJingleInstance_getVol(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkMusicJingleInstance_getTranssubtype(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getPpsValue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getPpsScaleKeysS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getPpsIsLooping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getPpsIsSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getPpsFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getPpsCreateEmS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getPpsCreateEmDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpTypeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpOffsetVecS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpDistribTypeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getShpDistribWalkSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getShpIsVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpDimS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpMeshS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getShpMeshRenderB(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getShpScaleKeysS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getShpScaleIsLooping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getShpScaleIsSmooth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getShpScaleFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getDirModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getDirForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getDirModeTargetForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getDirModeTargetPosS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getDirAngleHead(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getDirAngleHeadVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getDirAngleElev(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getDirAngleElevVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVelAvg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVelVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getLspPartAvg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getLspPartVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getFlyGravityS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getFlyColldetB(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisNameS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisOrientationS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getVisTexIsQuadpoly(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVisTexAniFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getVisTexAniIsLooping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisTexColorStartS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisTexColorEndS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisSizeStartS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVisSizeEndScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getVisAlphaFuncS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVisAlphaStart(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getVisAlphaEnd(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getTrlFadeSpeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getTrlTextureS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getTrlWidth(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getMrkFadesPeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getMrktExtureS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getMrkSize(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getFlockMode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectInstance_getFlockStrength(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getUseEmittersFor(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectInstance_getTimeStartEndS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectInstance_getMBiasAmbientPfx(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getVisNameS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getVisSizeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getVisAlpha(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getVisAlphaBlendFuncS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getVisTexAniFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getVisTexAniIsLooping(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjOriginNode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjTargetNode(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjTargetRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjTargetAzi(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjTargetElev(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmTrjNumKeys(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmTrjNumKeysVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjAngleElevVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjAngleHeadVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjKeyDistVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjLoopModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmTrjEaseFuncS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjEaseVel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmTrjDynUpdateDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCreateS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxInvestOriginS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxInvestTargetS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmFxTriggerDelay(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmFxCreateDownTrj(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmActionCollDynS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmActionCollStatS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollStatS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollDynS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollStatAlignS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollDynAlignS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmFxLifespan(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmCheckCollision(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getEmAdjustShpToOrigin(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmInvestNextKeyDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getEmFlyGravity(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmSelfRotVelS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getLightPresetName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getSfxId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getSfxIsAmbient(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkEffectBaseInstance_getSendAssessMagic(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkEffectBaseInstance_getSecsPerDamage(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getEmFxCollDynPercS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkEffectBaseInstance_getUserString(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getVisNameS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getVisSizeScale(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getScaleDuration(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxPpsValue(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxScTime(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxShpDimS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxVelAvg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getLightPresetName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getLightRange(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getSfxId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getSfxIsAmbient(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getEmCreateFxId(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getEmFlyGravity(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkParticleEffectEmitKeyInstance_getEmTrjModeS(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkParticleEffectEmitKeyInstance_getEmCheckCollision(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkParticleEffectEmitKeyInstance_getEmFxLifespan(UIntPtr slf);

		[DllImport(DllName)]
		public static extern FightAiMove ZkFightAiInstance_getMove(UIntPtr slf, ulong i);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoundEffectInstance_getFile(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getPitchOff(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getPitchVar(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getLoop(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getLoopStartOffset(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundEffectInstance_getLoopEndOffset(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSoundEffectInstance_getReverbLevel(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoundEffectInstance_getPfxName(UIntPtr slf);

		[DllImport(DllName)]
		public static extern float ZkSoundSystemInstance_getVolume(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundSystemInstance_getBitResolution(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundSystemInstance_getSampleRate(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundSystemInstance_getUseStereo(UIntPtr slf);

		[DllImport(DllName)]
		public static extern int ZkSoundSystemInstance_getNumSfxChannels(UIntPtr slf);

		[DllImport(DllName)]
		public static extern IntPtr ZkSoundSystemInstance_getUsed3DProviderName(UIntPtr slf);

		public class Callbacks
		{
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkAnimationAliasEnumerator(UIntPtr ctx, UIntPtr v);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkAnimationBlendEnumerator(UIntPtr ctx, UIntPtr v);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkAnimationCombineEnumerator(UIntPtr ctx, UIntPtr v);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkAnimationEnumerator(UIntPtr ctx, UIntPtr v);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkAnimationSampleEnumerator(UIntPtr ctx, IntPtr sample);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkAttachmentEnumerator(UIntPtr ctx, IntPtr name, UIntPtr mesh);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkBspSectorEnumerator(UIntPtr ctx, UIntPtr sector);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkCameraTrajectoryFrameEnumerator(UIntPtr ctx, UIntPtr frame);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkCutsceneBlockEnumerator(UIntPtr ctx, UIntPtr block);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkDaedalusSymbolEnumerator(UIntPtr ctx, UIntPtr symbol);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkEventCameraTremorEnumerator(UIntPtr ctx, UIntPtr evt);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkEventMorphAnimationEnumerator(UIntPtr ctx, UIntPtr evt);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkEventParticleEffectStopEnumerator(UIntPtr ctx, UIntPtr evt);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkEventParticlEffectEnumerator(UIntPtr ctx, UIntPtr evt);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkEventSoundEffectEnumerator(UIntPtr ctx, UIntPtr evt);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkEventSoundEffectGroundEnumerator(UIntPtr ctx, UIntPtr evt);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkEventTagEnumerator(UIntPtr ctx, UIntPtr evt);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkFontGlyphEnumerator(UIntPtr ctx, IntPtr glyph);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkLightMapEnumerator(UIntPtr ctx, UIntPtr lightMap);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate void ZkLogger(UIntPtr ctx, LogLevel lvl, string name, string message);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkMaterialEnumerator(UIntPtr ctx, UIntPtr material);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkModelHierarchyNodeEnumerator(UIntPtr ctx, IntPtr node);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkMorphAnimationEnumerator(UIntPtr ctx, UIntPtr anim);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkMorphSourceEnumerator(UIntPtr ctx, UIntPtr src);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkOrientedBoundingBoxEnumerator(UIntPtr ctx, UIntPtr box);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkPolygonEnumerator(UIntPtr ctx, UIntPtr polygon);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkSoftSkinMeshEnumerator(UIntPtr ctx, UIntPtr mesh);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkSoftSkinWeightEnumerator(UIntPtr ctx, IntPtr entry, ulong count);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkStringEnumerator(UIntPtr ctx, IntPtr v);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkSubMeshEnumerator(UIntPtr ctx, UIntPtr subMesh);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkTextureMipmapEnumerator(UIntPtr ctx, ulong level, IntPtr data, ulong size);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkTriggerListTargetEnumerator(UIntPtr ctx, UIntPtr target);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkVfsNodeEnumerator(UIntPtr ctx, UIntPtr node);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkVirtualObjectEnumerator(UIntPtr ctx, UIntPtr vob);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate bool ZkWayPointEnumerator(UIntPtr ctx, UIntPtr point);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate void ZkDaedalusVmExternalCallback(UIntPtr ctx, UIntPtr vm);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate void ZkDaedalusVmExternalDefaultCallback(UIntPtr ctx, UIntPtr vm, UIntPtr sym);
		}

		public class Structs
		{
			[StructLayout(LayoutKind.Explicit)]
			public struct ZkColor
			{
				[FieldOffset(0)] public byte R;
				[FieldOffset(1)] public byte G;
				[FieldOffset(2)] public byte B;
				[FieldOffset(3)] public byte A;

				public Color ToColor()
				{
					return Color.FromArgb(A, R, G, B);
				}
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct ZkColorArgb
			{
				public byte A, R, G, B;

				public Color ToColor()
				{
					return Color.FromArgb(A, R, G, B);
				}
			}

			[StructLayout(LayoutKind.Sequential)]
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

				public Quaternion ToQuaternion()
				{
					// TODO(lmichaelis): Make this faster.
					var mat = new Matrix4x4(
						m00,
						m10,
						m20,
						0,
						m01,
						m11,
						m21,
						0,
						m02,
						m12,
						m22,
						0,
						0,
						0,
						0,
						1
					);
					return Quaternion.CreateFromRotationMatrix(mat);
				}
			}

			[StructLayout(LayoutKind.Sequential)]
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

				public DateTime AsDateTime()
				{
					return new DateTime((int)year, month, day, hour, minute, second);
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