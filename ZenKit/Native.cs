using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ZenKit;

internal static class Marshalling
{
	public static string? MarshalAsString(this IntPtr ptr)
	{
		return Marshal.PtrToStringUTF8(ptr);
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
	public delegate bool ZkAnimationSampleEnumerator(UIntPtr ctx, IntPtr sample);

	public delegate bool ZkAttachmentEnumerator(UIntPtr ctx, IntPtr name, UIntPtr mesh);

	public delegate bool ZkCutsceneBlockEnumerator(UIntPtr ctx, UIntPtr block);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate bool ZkFontGlyphEnumerator(UIntPtr ctx, IntPtr glyph);

	public delegate bool ZkLightMapEnumerator(UIntPtr ctx, UIntPtr lightMap);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ZkLogger(UIntPtr ctx, LogLevel lvl, string name, string message);

	public delegate bool ZkMaterialEnumerator(UIntPtr ctx, UIntPtr material);


	public delegate bool ZkModelHierarchyNodeEnumerator(UIntPtr ctx, IntPtr node);

	public delegate bool ZkMorphAnimationEnumerator(UIntPtr ctx, UIntPtr anim);

	public delegate bool ZkMorphSourceEnumerator(UIntPtr ctx, UIntPtr src);

	public delegate bool ZkOrientedBoundingBoxEnumerator(UIntPtr ctx, UIntPtr box);

	public delegate bool ZkPolygonEnumerator(UIntPtr ctx, UIntPtr polygon);

	public delegate bool ZkSoftSkinMeshEnumerator(UIntPtr ctx, UIntPtr mesh);


	public delegate bool ZkSoftSkinWeightEnumerator(UIntPtr ctx, IntPtr entry, ulong count);


	public delegate bool ZkSubMeshEnumerator(UIntPtr ctx, UIntPtr subMesh);

	public delegate bool ZkTextureMipmapEnumerator(UIntPtr ctx, ulong level, IntPtr data, ulong size);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate bool ZkVfsNodeEnumerator(UIntPtr ctx, UIntPtr node);

	private const string DLLNAME = "zenkit";

	static Native()
	{
		NativeLibrary.SetDllImportResolver(typeof(Native).Assembly, ImportResolver);
	}

	private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
	{
		if (!Environment.Is64BitProcess) return IntPtr.Zero;
		if (libraryName != DLLNAME) return IntPtr.Zero;

		string libraryPath;

		if (OperatingSystem.IsWindows()) libraryPath = "./Runtimes/win-x64/libczenkit.dll";
		else if (OperatingSystem.IsAndroid()) libraryPath = "./Runtimes/android-x64/libczenkit.so";
		else if (OperatingSystem.IsLinux()) libraryPath = "./Runtimes/linux-x64/libczenkit.so";
		else if (OperatingSystem.IsMacOS()) libraryPath = "./Runtimes/osx-x64/libczenkit.so";
		else return IntPtr.Zero;

		if (!NativeLibrary.TryLoad(libraryPath, out var libraryHandle)) return IntPtr.Zero;
		return libraryHandle;
	}

	[DllImport(DLLNAME)]
	public static extern void ZkLogger_set(LogLevel lvl, ZkLogger logger, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkLogger_setDefault(LogLevel lvl);

	[DllImport(DLLNAME)]
	public static extern void ZkLogger_log(LogLevel lvl, string name, string message);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkRead_newFile(UIntPtr stream);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkRead_newMem([MarshalAs(UnmanagedType.LPArray)] byte[] bytes, ulong length);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkRead_newPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkRead_newExt(ZkReadExt ext, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkRead_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_new();

	[DllImport(DLLNAME)]
	public static extern void ZkVfs_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_getRoot(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_mkdir(UIntPtr slf, string path);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_remove(UIntPtr slf, string path);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_mount(UIntPtr slf, UIntPtr node, string parent, VfsOverwriteBehavior overwrite);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_mountHost(UIntPtr slf, string path, string parent, VfsOverwriteBehavior overwrite);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_mountDisk(UIntPtr slf, UIntPtr buf, VfsOverwriteBehavior overwrite);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_mountDiskHost(UIntPtr slf, string path, VfsOverwriteBehavior overwrite);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_resolvePath(UIntPtr slf, string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_findNode(UIntPtr slf, string name);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_newFile(string name, byte[] data, ulong size, ulong ts);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_newDir(string name, ulong ts);

	[DllImport(DLLNAME)]
	public static extern void ZkVfsNode_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfsNode_isFile(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfsNode_isDir(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkVfsNode_getTime(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkVfsNode_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_getChild(UIntPtr slf, string name);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_create(UIntPtr slf, UIntPtr node);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfsNode_remove(UIntPtr slf, string name);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_open(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern void ZkVfsNode_enumerateChildren(UIntPtr slf, ZkVfsNodeEnumerator callback, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneLibrary_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneLibrary_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneLibrary_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkCutsceneLibrary_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneLibrary_getBlock(UIntPtr slf, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkCutsceneLibrary_enumerateBlocks(UIntPtr slf, ZkCutsceneBlockEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkCutsceneBlock_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneBlock_getMessage(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint ZkCutsceneMessage_getType(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkCutsceneMessage_getText(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkCutsceneMessage_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkFont_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkFont_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkFont_loadVfs(UIntPtr buf, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkFont_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkFont_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint ZkFont_getHeight(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U8)]
	public static extern ulong ZkFont_getGlyphCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern FontGlyph ZkFont_getGlyph(UIntPtr slf, [MarshalAs(UnmanagedType.U8)] ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkFont_enumerateGlyphs(UIntPtr slf, ZkFontGlyphEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelAnimation_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelAnimation_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelAnimation_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkModelAnimation_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelAnimation_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelAnimation_getNext(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint ZkModelAnimation_getLayer(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint ZkModelAnimation_getFrameCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint ZkModelAnimation_getNodeCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkModelAnimation_getFps(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkModelAnimation_getFpsSource(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AxisAlignedBoundingBox ZkModelAnimation_getBbox(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint ZkModelAnimation_getChecksum(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelAnimation_getSourcePath(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ZkDate ZkModelAnimation_getSourceDate(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelAnimation_getSourceScript(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U8)]
	public static extern ulong ZkModelAnimation_getSampleCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AnimationSample ZkModelAnimation_getSample(UIntPtr slf, [MarshalAs(UnmanagedType.U8)] ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkModelAnimation_enumerateSamples(UIntPtr slf, ZkAnimationSampleEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelAnimation_getNodeIndices(UIntPtr slf, out ulong size);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelHierarchy_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelHierarchy_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelHierarchy_loadVfs(UIntPtr buf, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkModelHierarchy_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelHierarchy_getNodeCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ModelHierarchyNode ZkModelHierarchy_getNode(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern AxisAlignedBoundingBox ZkModelHierarchy_getBbox(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AxisAlignedBoundingBox ZkModelHierarchy_getCollisionBbox(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector3 ZkModelHierarchy_getRootTranslation(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkModelHierarchy_getChecksum(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ZkDate ZkModelHierarchy_getSourceDate(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelHierarchy_getSourcePath(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern void ZkModelHierarchy_enumerateNodes(UIntPtr slf, ZkModelHierarchyNodeEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern Vector3 ZkOrientedBoundingBox_getCenter(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector3 ZkOrientedBoundingBox_getAxis(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern Vector3 ZkOrientedBoundingBox_getHalfWidth(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkOrientedBoundingBox_getChildCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkOrientedBoundingBox_getChild(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkOrientedBoundingBox_enumerateChildren(UIntPtr slf, ZkOrientedBoundingBoxEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern AxisAlignedBoundingBox ZkOrientedBoundingBox_toAabb(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMaterial_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMaterial_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern void ZkMaterial_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMaterial_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern MaterialGroup ZkMaterial_getGroup(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ZkColor ZkMaterial_getColor(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMaterial_getSmoothAngle(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMaterial_getTexture(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector2 ZkMaterial_getTextureScale(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMaterial_getTextureAnimationFps(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AnimationMapping ZkMaterial_getTextureAnimationMapping(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector2 ZkMaterial_getTextureAnimationMappingDirection(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkMaterial_getDisableCollision(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkMaterial_getDisableLightmap(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkMaterial_getDontCollapse(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMaterial_getDetailObject(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMaterial_getDetailObjectScale(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkMaterial_getForceOccluder(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkMaterial_getEnvironmentMapping(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMaterial_getEnvironmentMappingStrength(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern WaveMode ZkMaterial_getWaveMode(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern WaveSpeed ZkMaterial_getWaveSpeed(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMaterial_getWaveAmplitude(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMaterial_getWaveGridSize(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkMaterial_getIgnoreSun(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AlphaFunction ZkMaterial_getAlphaFunction(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector2 ZkMaterial_getDefaultMapping(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMultiResolutionMesh_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMultiResolutionMesh_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMultiResolutionMesh_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkMultiResolutionMesh_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMultiResolutionMesh_getPositions(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMultiResolutionMesh_getNormals(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern ulong ZkMultiResolutionMesh_getSubMeshCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMultiResolutionMesh_getSubMesh(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void
		ZkMultiResolutionMesh_enumerateSubMeshes(UIntPtr slf, ZkSubMeshEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern ulong ZkMultiResolutionMesh_getMaterialCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMultiResolutionMesh_getMaterial(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkMultiResolutionMesh_enumerateMaterials(UIntPtr slf, ZkMaterialEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern bool ZkMultiResolutionMesh_getAlphaTest(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AxisAlignedBoundingBox ZkMultiResolutionMesh_getBbox(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMultiResolutionMesh_getOrientedBbox(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkSubMesh_getMaterial(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSubMesh_getTriangles(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSubMesh_getWedges(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSubMesh_getColors(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSubMesh_getTrianglePlaneIndices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSubMesh_getTrianglePlanes(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSubMesh_getTriangleEdges(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSubMesh_getEdges(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSubMesh_getEdgeScores(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSubMesh_getWedgeMap(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern ulong ZkSoftSkinMesh_getNodeCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkSoftSkinMesh_getMesh(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkSoftSkinMesh_getBbox(UIntPtr slf, ulong node);

	[DllImport(DLLNAME)]
	public static extern void ZkSoftSkinMesh_enumerateBoundingBoxes(UIntPtr slf, ZkOrientedBoundingBoxEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSoftSkinMesh_getWeights(UIntPtr slf, ulong node, out ulong count);

	[DllImport(DLLNAME)]
	public static extern void ZkSoftSkinMesh_enumerateWeights(UIntPtr slf, ZkSoftSkinWeightEnumerator cb, UIntPtr node);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSoftSkinMesh_getWedgeNormals(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkSoftSkinMesh_getNodes(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelMesh_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelMesh_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelMesh_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkModelMesh_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelMesh_getMeshCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelMesh_getMesh(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkModelMesh_enumerateMeshes(UIntPtr slf, ZkSoftSkinMeshEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelMesh_getAttachmentCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelMesh_getAttachment(UIntPtr slf, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkModelMesh_enumerateAttachments(UIntPtr slf, ZkAttachmentEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern uint ZkModelMesh_getChecksum(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModel_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModel_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModel_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkModel_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModel_getHierarchy(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModel_getMesh(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkTexture_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkTexture_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkTexture_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkTexture_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern TextureFormat ZkTexture_getFormat(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkTexture_getWidth(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkTexture_getHeight(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkTexture_getWidthMipmap(UIntPtr slf, ulong level);

	[DllImport(DLLNAME)]
	public static extern uint ZkTexture_getHeightMipmap(UIntPtr slf, ulong level);

	[DllImport(DLLNAME)]
	public static extern uint ZkTexture_getWidthRef(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkTexture_getHeightRef(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkTexture_getMipmapCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkTexture_getAverageColor(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkTexture_getPalette(UIntPtr slf, out ulong size);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkTexture_getMipmapRaw(UIntPtr slf, ulong level, out ulong size);

	[DllImport(DLLNAME)]
	public static extern ulong ZkTexture_getMipmapRgba(UIntPtr slf, ulong level, byte[] buf, ulong size);

	[DllImport(DLLNAME)]
	public static extern void ZkTexture_enumerateRawMipmaps(UIntPtr slf, ZkTextureMipmapEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkTexture_enumerateRgbaMipmaps(UIntPtr slf, ZkTextureMipmapEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMorphMesh_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMorphMesh_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMorphMesh_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkMorphMesh_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMorphMesh_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMorphMesh_getMesh(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMorphMesh_getMorphPositions(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern ulong ZkMorphMesh_getAnimationCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMorphMesh_getAnimation(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkMorphMesh_enumerateAnimations(UIntPtr slf, ZkMorphAnimationEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern ulong ZkMorphMesh_getSourceCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMorphMesh_getSource(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkMorphMesh_enumerateSources(UIntPtr slf, ZkMorphSourceEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMorphAnimation_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkMorphAnimation_getLayer(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMorphAnimation_getBlendIn(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMorphAnimation_getBlendOut(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMorphAnimation_getDuration(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkMorphAnimation_getSpeed(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern byte ZkMorphAnimation_getFlags(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkMorphAnimation_getFrameCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMorphAnimation_getVertices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMorphAnimation_getSamples(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMorphSource_getFileName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ZkDate ZkMorphSource_getFileDate(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMesh_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMesh_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMesh_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkMesh_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ZkDate ZkMesh_getSourceDate(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMesh_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AxisAlignedBoundingBox ZkMesh_getBoundingBox(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMesh_getOrientedBoundingBox(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkMesh_getMaterialCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMesh_getMaterial(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkMesh_enumerateMaterials(UIntPtr slf, ZkMaterialEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMesh_getPositions(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMesh_getVertices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern ulong ZkMesh_getLightMapCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMesh_getLightMap(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkMesh_enumerateLightMaps(UIntPtr slf, ZkLightMapEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern ulong ZkMesh_getPolygonCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkMesh_getPolygon(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkMesh_enumeratePolygons(UIntPtr slf, ZkPolygonEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkLightMap_getImage(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector3 ZkLightMap_getOrigin(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector3 ZkLightMap_getNormal(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern uint ZkPolygon_getMaterialIndex(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkPolygon_getLightMapIndex(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkPolygon_getPositionIndices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkPolygon_getPolygonIndices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern bool ZkPolygon_getIsPortal(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkPolygon_getIsOccluder(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkPolygon_getIsSector(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkPolygon_getShouldRelight(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkPolygon_getIsOutdoor(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkPolygon_getIsGhostOccluder(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkPolygon_getIsDynamicallyLit(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkPolygon_getIsLod(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern byte ZkPolygon_getNormalAxis(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ushort ZkPolygon_getSectorIndex(UIntPtr slf);


	[StructLayout(LayoutKind.Sequential)]
	public struct ZkColor
	{
		public byte R, G, B, A;

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
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ReadFn(UIntPtr ctx, IntPtr buf, ulong len);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong SeekFn(UIntPtr ctx, long off, Whence whence);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong TellFn(UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool EofFn(UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool DelFn(UIntPtr ctx);

		public ReadFn read;
		public SeekFn seek;
		public TellFn tell;
		public EofFn eof;
		public DelFn del;
	}
}