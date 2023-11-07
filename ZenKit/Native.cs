using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using ZenKit.Vobs;

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
	public delegate bool ZkAnimationAliasEnumerator(UIntPtr ctx, UIntPtr v);

	public delegate bool ZkAnimationBlendEnumerator(UIntPtr ctx, UIntPtr v);


	public delegate bool ZkAnimationCombineEnumerator(UIntPtr ctx, UIntPtr v);

	public delegate bool ZkAnimationEnumerator(UIntPtr ctx, UIntPtr v);

	public delegate bool ZkAnimationSampleEnumerator(UIntPtr ctx, IntPtr sample);

	public delegate bool ZkAttachmentEnumerator(UIntPtr ctx, IntPtr name, UIntPtr mesh);

	public delegate bool ZkBspSectorEnumerator(UIntPtr ctx, UIntPtr sector);

	public delegate bool ZkCutsceneBlockEnumerator(UIntPtr ctx, UIntPtr block);

	public delegate bool ZkEventCameraTremorEnumerator(UIntPtr ctx, UIntPtr evt);

	public delegate bool ZkEventMorphAnimationEnumerator(UIntPtr ctx, UIntPtr evt);

	public delegate bool ZkEventParticleEffectStopEnumerator(UIntPtr ctx, UIntPtr evt);

	public delegate bool ZkEventParticlEffectEnumerator(UIntPtr ctx, UIntPtr evt);

	public delegate bool ZkEventSoundEffectEnumerator(UIntPtr ctx, UIntPtr evt);

	public delegate bool ZkEventSoundEffectGroundEnumerator(UIntPtr ctx, UIntPtr evt);

	public delegate bool ZkEventTagEnumerator(UIntPtr ctx, UIntPtr evt);

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

	public delegate bool ZkStringEnumerator(UIntPtr ctx, IntPtr v);


	public delegate bool ZkSubMeshEnumerator(UIntPtr ctx, UIntPtr subMesh);

	public delegate bool ZkTextureMipmapEnumerator(UIntPtr ctx, ulong level, IntPtr data, ulong size);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate bool ZkVfsNodeEnumerator(UIntPtr ctx, UIntPtr node);

	public delegate bool ZkVirtualObjectEnumerator(UIntPtr ctx, UIntPtr vob);

	public delegate bool ZkWayPointEnumerator(UIntPtr ctx, UIntPtr point);

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

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelScript_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelScript_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelScript_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkModelScript_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelScript_getSkeletonName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkModelScript_getSkeletonMeshDisabled(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelScript_getMeshCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelScript_getDisabledAnimationsCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelScript_getAnimationCombineCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelScript_getAnimationBlendCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelScript_getAnimationAliasCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelScript_getModelTagCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkModelScript_getAnimationCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelScript_getDisabledAnimation(UIntPtr slf, long i);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelScript_getMesh(UIntPtr slf, long i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelScript_getAnimationCombine(UIntPtr slf, long i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelScript_getAnimationBlend(UIntPtr slf, long i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelScript_getAnimationAlias(UIntPtr slf, long i);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkModelScript_getModelTag(UIntPtr slf, long i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkModelScript_getAnimation(UIntPtr slf, long i);

	[DllImport(DLLNAME)]
	public static extern void ZkModelScript_enumerateAnimationCombines(UIntPtr slf, ZkAnimationCombineEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkModelScript_enumerateMeshes(UIntPtr slf, ZkStringEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void
		ZkModelScript_enumerateDisabledAnimations(UIntPtr slf, ZkStringEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkModelScript_enumerateAnimationBlends(UIntPtr slf, ZkAnimationBlendEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkModelScript_enumerateAnimationAliases(UIntPtr slf, ZkAnimationAliasEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkModelScript_enumerateModelTags(UIntPtr slf, ZkStringEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkModelScript_enumerateAnimations(UIntPtr slf, ZkAnimationEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimation_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkAnimation_getLayer(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimation_getNext(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimation_getBlendIn(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimation_getBlendOut(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkAnimation_getFlags(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimation_getModel(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AnimationDirection ZkAnimation_getDirection(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkAnimation_getFirstFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkAnimation_getLastFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimation_getFps(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimation_getSpeed(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimation_getCollisionVolumeScale(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkAnimation_getEventTagCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkAnimation_getParticleEffectCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkAnimation_getParticleEffectStopCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkAnimation_getSoundEffectCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkAnimation_getSoundEffectGroundCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkAnimation_getMorphAnimationCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkAnimation_getCameraTremorCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkAnimation_getEventTag(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkAnimation_getParticleEffect(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkAnimation_getParticleEffectStop(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkAnimation_getSoundEffect(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkAnimation_getSoundEffectGround(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkAnimation_getMorphAnimation(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkAnimation_getCameraTremor(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkAnimation_enumerateEventTags(UIntPtr slf, ZkEventTagEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkAnimation_enumerateParticleEffects(UIntPtr slf, ZkEventParticlEffectEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkAnimation_enumerateParticleEffectStops(UIntPtr slf,
		ZkEventParticleEffectStopEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkAnimation_enumerateSoundEffects(UIntPtr slf, ZkEventSoundEffectEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkAnimation_enumerateSoundEffectGrounds(UIntPtr slf,
		ZkEventSoundEffectGroundEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkAnimation_enumerateMorphAnimations(UIntPtr slf, ZkEventMorphAnimationEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkAnimation_enumerateCameraTremors(UIntPtr slf, ZkEventCameraTremorEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern int ZkEventTag_getFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern EventType ZkEventTag_getType(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkEventTag_getSlot(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkEventTag_getItem(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkEventTag_getFrames(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern FightMode ZkEventTag_getFightMode(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkEventTag_getIsAttached(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventParticleEffect_getFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventParticleEffect_getIndex(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkEventParticleEffect_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkEventParticleEffect_getPosition(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkEventParticleEffect_getIsAttached(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventParticleEffectStop_getFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventParticleEffectStop_getIndex(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventCameraTremor_getFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventCameraTremor_getField1(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventCameraTremor_getField2(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventCameraTremor_getField3(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventCameraTremor_getField4(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventSoundEffect_getFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkEventSoundEffect_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkEventSoundEffect_getRange(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkEventSoundEffect_getEmptySlot(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkEventSoundEffectGround_getFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkEventSoundEffectGround_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkEventSoundEffectGround_getRange(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkEventSoundEffectGround_getEmptySlot(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkMorphAnimation_getFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMorphAnimation_getAnimation(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkMorphAnimation_getNode(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimationCombine_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkAnimationCombine_getLayer(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimationCombine_getNext(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimationCombine_getBlendIn(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimationCombine_getBlendOut(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkAnimationCombine_getFlags(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimationCombine_getModel(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkAnimationCombine_getLastFrame(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimationBlend_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimationBlend_getNext(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimationBlend_getBlendIn(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimationBlend_getBlendOut(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimationAlias_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkAnimationAlias_getLayer(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimationAlias_getNext(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimationAlias_getBlendIn(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkAnimationAlias_getBlendOut(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkAnimationAlias_getFlags(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkAnimationAlias_getAlias(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AnimationDirection ZkAnimationAlias_getDirection(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern BspTreeType ZkBspTree_getType(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkBspTree_getPolygonIndices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkBspTree_getLeafPolygonIndices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkBspTree_getPortalPolygonIndices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkBspTree_getLightPoints(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkBspTree_getLeafNodeIndices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkBspTree_getNodes(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern ulong ZkBspTree_getSectorCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkBspTree_getSector(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkBspTree_enumerateSectors(UIntPtr slf, ZkBspSectorEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkBspSector_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkBspSector_getNodeIndices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkBspSector_getPortalPolygonIndices(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkWayNet_getEdges(UIntPtr slf, out ulong count);

	[DllImport(DLLNAME)]
	public static extern ulong ZkWayNet_getPointCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkWayNet_getPoint(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkWayNet_enumeratePoints(UIntPtr slf, ZkWayPointEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkWayPoint_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkWayPoint_getWaterDepth(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkWayPoint_getUnderWater(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector3 ZkWayPoint_getPosition(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector3 ZkWayPoint_getDirection(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkWayPoint_getFreePoint(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkWorld_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkWorld_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkWorld_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkWorld_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkWorld_getMesh(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkWorld_getBspTree(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkWorld_getWayNet(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkWorld_getRootObjectCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkWorld_getRootObject(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkWorld_enumerateRootObjects(UIntPtr slf, ZkVirtualObjectEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVirtualObject_load(UIntPtr buf, GameVersion version);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVirtualObject_loadPath(string path, GameVersion version);

	[DllImport(DLLNAME)]
	public static extern void ZkVirtualObject_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern VirtualObjectType ZkVirtualObject_getType(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern uint ZkVirtualObject_getId(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AxisAlignedBoundingBox ZkVirtualObject_getBbox(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector3 ZkVirtualObject_getPosition(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ZkMat3x3 ZkVirtualObject_getRotation(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVirtualObject_getShowVisual(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern SpriteAlignment ZkVirtualObject_getSpriteCameraFacingMode(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVirtualObject_getCdStatic(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVirtualObject_getCdDynamic(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVirtualObject_getVobStatic(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ShadowType ZkVirtualObject_getDynamicShadows(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVirtualObject_getPhysicsEnabled(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AnimationType ZkVirtualObject_getAnimMode(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkVirtualObject_getBias(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVirtualObject_getAmbient(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkVirtualObject_getAnimStrength(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkVirtualObject_getFarClipScale(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkVirtualObject_getPresetName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkVirtualObject_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkVirtualObject_getVisualName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern VisualType ZkVirtualObject_getVisualType(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVirtualObject_getVisualDecal(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkVirtualObject_getChildCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVirtualObject_getChild(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkVirtualObject_enumerateChildren(UIntPtr slf, ZkVirtualObjectEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkDecal_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector2 ZkDecal_getDimension(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern Vector2 ZkDecal_getOffset(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkDecal_getTwoSided(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern AlphaFunction ZkDecal_getAlphaFunc(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkDecal_getTextureAnimFps(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern byte ZkDecal_getAlphaWeight(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkDecal_getIgnoreDaylight(UIntPtr slf);

	public delegate bool ZkCameraTrajectoryFrameEnumerator(UIntPtr ctx, UIntPtr frame);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneCamera_load(UIntPtr buf, GameVersion version);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneCamera_loadPath(string path, GameVersion version);

	[DllImport(DLLNAME)]
	public static extern void ZkCutsceneCamera_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern CameraTrajectory ZkCutsceneCamera_getTrajectoryFOR(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern CameraTrajectory ZkCutsceneCamera_getTargetTrajectoryFOR(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern CameraLoopType ZkCutsceneCamera_getLoopMode(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern CameraLerpType ZkCutsceneCamera_getLerpMode(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkCutsceneCamera_getIgnoreFORVobRotation(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkCutsceneCamera_getIgnoreFORVobRotationTarget(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkCutsceneCamera_getAdapt(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkCutsceneCamera_getEaseFirst(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkCutsceneCamera_getEaseLast(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkCutsceneCamera_getTotalDuration(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkCutsceneCamera_getAutoFocusVob(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkCutsceneCamera_getAutoPlayerMovable(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkCutsceneCamera_getAutoUntriggerLast(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkCutsceneCamera_getAutoUntriggerLastDelay(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkCutsceneCamera_getPositionCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern int ZkCutsceneCamera_getTargetCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkCutsceneCamera_getFrameCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneCamera_getFrame(UIntPtr slf, ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkCutsceneCamera_enumerateFrames(UIntPtr slf, ZkCameraTrajectoryFrameEnumerator cb,
		UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern float ZkCameraTrajectoryFrame_getTime(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkCameraTrajectoryFrame_getRollAngle(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkCameraTrajectoryFrame_getFovScale(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionType(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionTypeFov(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionTypeRoll(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern CameraMotion ZkCameraTrajectoryFrame_getMotionTypeTimeScale(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkCameraTrajectoryFrame_getTension(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkCameraTrajectoryFrame_getCamBias(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkCameraTrajectoryFrame_getContinuity(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern float ZkCameraTrajectoryFrame_getTimeScale(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkCameraTrajectoryFrame_getTimeFixed(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ZkMat4x4 ZkCameraTrajectoryFrame_getOriginalPose(UIntPtr slf);


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