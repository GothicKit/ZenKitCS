using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Daedalus;
using ZenKit.NativeLoader.NativeCallbacks;
using ZenKit.NativeLoader.NativeStructs;
using ZenKit.Vobs;

namespace ZenKit.NativeLoader
{
	namespace NativeCallbacks
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

	namespace NativeFunctions
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimation_getFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimation_getSpeed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkLogger_set(LogLevel lvl, ZkLogger logger, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkLogger_setDefault(LogLevel lvl);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkLogger_log(LogLevel lvl, string name, string message);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkRead_newFile(UIntPtr stream);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkRead_newMem([MarshalAs(UnmanagedType.LPArray)] byte[] bytes, ulong length);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkRead_newPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkRead_newExt(ZkReadExt ext, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkRead_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkRead_getSize(UIntPtr slf);
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkRead_getBytes(UIntPtr slf, byte[] buf, ulong length);
        
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfs_new();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkVfs_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfs_getRoot(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfs_mkdir(UIntPtr slf, string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVfs_remove(UIntPtr slf, string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVfs_mount(UIntPtr slf, UIntPtr node, string parent, VfsOverwriteBehavior overwrite);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVfs_mountHost(UIntPtr slf, string path, string parent, VfsOverwriteBehavior overwrite);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVfs_mountDisk(UIntPtr slf, UIntPtr buf, VfsOverwriteBehavior overwrite);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVfs_mountDiskHost(UIntPtr slf, string path, VfsOverwriteBehavior overwrite);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfs_resolvePath(UIntPtr slf, string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfs_findNode(UIntPtr slf, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfsNode_newFile(string name, byte[] data, ulong size, ulong ts);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfsNode_newDir(string name, ulong ts);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkVfsNode_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVfsNode_isFile(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVfsNode_isDir(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkVfsNode_getTime(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkVfsNode_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfsNode_getChild(UIntPtr slf, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfsNode_create(UIntPtr slf, UIntPtr node);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVfsNode_remove(UIntPtr slf, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVfsNode_open(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkVfsNode_enumerateChildren(UIntPtr slf, ZkVfsNodeEnumerator callback, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCutsceneLibrary_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCutsceneLibrary_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCutsceneLibrary_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkCutsceneLibrary_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCutsceneLibrary_getBlock(UIntPtr slf, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkCutsceneLibrary_enumerateBlocks(UIntPtr slf, ZkCutsceneBlockEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkCutsceneBlock_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCutsceneBlock_getMessage(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkCutsceneMessage_getType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkCutsceneMessage_getText(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkCutsceneMessage_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkFont_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkFont_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkFont_loadVfs(UIntPtr buf, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkFont_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkFont_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkFont_getHeight(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkFont_getGlyphCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate FontGlyph ZkFont_getGlyph(UIntPtr slf, [MarshalAs(UnmanagedType.U8)] ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkFont_enumerateGlyphs(UIntPtr slf, ZkFontGlyphEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelAnimation_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelAnimation_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelAnimation_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelAnimation_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelAnimation_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelAnimation_getNext(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkModelAnimation_getLayer(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkModelAnimation_getFrameCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkModelAnimation_getNodeCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkModelAnimation_getFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkModelAnimation_getFpsSource(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AxisAlignedBoundingBox ZkModelAnimation_getBbox(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkModelAnimation_getChecksum(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelAnimation_getSourcePath(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkDate ZkModelAnimation_getSourceDate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelAnimation_getSourceScript(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelAnimation_getSampleCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AnimationSample ZkModelAnimation_getSample(UIntPtr slf, [MarshalAs(UnmanagedType.U8)] ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void
			ZkModelAnimation_enumerateSamples(UIntPtr slf, ZkAnimationSampleEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelAnimation_getNodeIndices(UIntPtr slf, out ulong size);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelHierarchy_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelHierarchy_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelHierarchy_loadVfs(UIntPtr buf, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelHierarchy_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelHierarchy_getNodeCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ModelHierarchyNode ZkModelHierarchy_getNode(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AxisAlignedBoundingBox ZkModelHierarchy_getBbox(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AxisAlignedBoundingBox ZkModelHierarchy_getCollisionBbox(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkModelHierarchy_getRootTranslation(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkModelHierarchy_getChecksum(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkDate ZkModelHierarchy_getSourceDate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelHierarchy_getSourcePath(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelHierarchy_enumerateNodes(UIntPtr slf, ZkModelHierarchyNodeEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkOrientedBoundingBox_getCenter(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkOrientedBoundingBox_getAxis(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkOrientedBoundingBox_getHalfWidth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkOrientedBoundingBox_getChildCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkOrientedBoundingBox_getChild(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkOrientedBoundingBox_enumerateChildren(UIntPtr slf, ZkOrientedBoundingBoxEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AxisAlignedBoundingBox ZkOrientedBoundingBox_toAabb(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMaterial_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMaterial_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMaterial_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMaterial_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MaterialGroup ZkMaterial_getGroup(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkColor ZkMaterial_getColor(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMaterial_getSmoothAngle(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMaterial_getTexture(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector2 ZkMaterial_getTextureScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMaterial_getTextureAnimationFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AnimationMapping ZkMaterial_getTextureAnimationMapping(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector2 ZkMaterial_getTextureAnimationMappingDirection(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMaterial_getDisableCollision(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMaterial_getDisableLightmap(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMaterial_getDontCollapse(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMaterial_getDetailObject(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMaterial_getDetailObjectScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMaterial_getForceOccluder(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMaterial_getEnvironmentMapping(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMaterial_getEnvironmentMappingStrength(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate WaveMode ZkMaterial_getWaveMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate WaveSpeed ZkMaterial_getWaveSpeed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMaterial_getWaveAmplitude(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMaterial_getWaveGridSize(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMaterial_getIgnoreSun(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AlphaFunction ZkMaterial_getAlphaFunction(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector2 ZkMaterial_getDefaultMapping(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMultiResolutionMesh_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMultiResolutionMesh_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMultiResolutionMesh_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMultiResolutionMesh_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMultiResolutionMesh_getPositions(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMultiResolutionMesh_getNormals(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkMultiResolutionMesh_getSubMeshCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMultiResolutionMesh_getSubMesh(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMultiResolutionMesh_enumerateSubMeshes(UIntPtr slf, ZkSubMeshEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkMultiResolutionMesh_getMaterialCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMultiResolutionMesh_getMaterial(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void
			ZkMultiResolutionMesh_enumerateMaterials(UIntPtr slf, ZkMaterialEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMultiResolutionMesh_getAlphaTest(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AxisAlignedBoundingBox ZkMultiResolutionMesh_getBbox(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMultiResolutionMesh_getOrientedBbox(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkSubMesh_getMaterial(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSubMesh_getTriangles(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSubMesh_getWedges(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSubMesh_getColors(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSubMesh_getTrianglePlaneIndices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSubMesh_getTrianglePlanes(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSubMesh_getTriangleEdges(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSubMesh_getEdges(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSubMesh_getEdgeScores(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSubMesh_getWedgeMap(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkSoftSkinMesh_getNodeCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkSoftSkinMesh_getMesh(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkSoftSkinMesh_getBbox(UIntPtr slf, ulong node);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkSoftSkinMesh_enumerateBoundingBoxes(UIntPtr slf, ZkOrientedBoundingBoxEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSoftSkinMesh_getWeights(UIntPtr slf, ulong node, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkSoftSkinMesh_enumerateWeights(UIntPtr slf, ZkSoftSkinWeightEnumerator cb, UIntPtr node);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSoftSkinMesh_getWedgeNormals(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSoftSkinMesh_getNodes(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelMesh_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelMesh_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelMesh_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelMesh_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelMesh_getMeshCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelMesh_getMesh(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelMesh_enumerateMeshes(UIntPtr slf, ZkSoftSkinMeshEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelMesh_getAttachmentCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelMesh_getAttachment(UIntPtr slf, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelMesh_enumerateAttachments(UIntPtr slf, ZkAttachmentEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkModelMesh_getChecksum(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModel_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModel_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModel_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModel_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModel_getHierarchy(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModel_getMesh(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTexture_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTexture_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTexture_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTexture_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate TextureFormat ZkTexture_getFormat(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkTexture_getWidth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkTexture_getHeight(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkTexture_getWidthMipmap(UIntPtr slf, ulong level);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkTexture_getHeightMipmap(UIntPtr slf, ulong level);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkTexture_getWidthRef(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkTexture_getHeightRef(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkTexture_getMipmapCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkTexture_getAverageColor(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTexture_getPalette(UIntPtr slf, out ulong size);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTexture_getMipmapRaw(UIntPtr slf, ulong level, out ulong size);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkTexture_getMipmapRgba(UIntPtr slf, ulong level, byte[] buf, ulong size);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTexture_enumerateRawMipmaps(UIntPtr slf, ZkTextureMipmapEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTexture_enumerateRgbaMipmaps(UIntPtr slf, ZkTextureMipmapEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMorphMesh_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMorphMesh_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMorphMesh_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMorphMesh_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMorphMesh_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMorphMesh_getMesh(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMorphMesh_getMorphPositions(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkMorphMesh_getAnimationCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMorphMesh_getAnimation(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMorphMesh_enumerateAnimations(UIntPtr slf, ZkMorphAnimationEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkMorphMesh_getSourceCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMorphMesh_getSource(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMorphMesh_enumerateSources(UIntPtr slf, ZkMorphSourceEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMorphAnimation_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMorphAnimation_getLayer(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMorphAnimation_getBlendIn(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMorphAnimation_getBlendOut(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMorphAnimation_getDuration(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMorphAnimation_getSpeed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte ZkMorphAnimation_getFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkMorphAnimation_getFrameCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMorphAnimation_getVertices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMorphAnimation_getSamples(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMorphSource_getFileName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkDate ZkMorphSource_getFileDate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMesh_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMesh_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMesh_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMesh_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkDate ZkMesh_getSourceDate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMesh_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AxisAlignedBoundingBox ZkMesh_getBoundingBox(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMesh_getOrientedBoundingBox(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkMesh_getMaterialCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMesh_getMaterial(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMesh_enumerateMaterials(UIntPtr slf, ZkMaterialEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMesh_getPositions(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMesh_getVertices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkMesh_getLightMapCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMesh_getLightMap(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMesh_enumerateLightMaps(UIntPtr slf, ZkLightMapEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkMesh_getPolygonCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMesh_getPolygon(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMesh_enumeratePolygons(UIntPtr slf, ZkPolygonEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkLightMap_getImage(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkLightMap_getOrigin(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkLightMap_getNormal(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkPolygon_getMaterialIndex(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkPolygon_getLightMapIndex(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkPolygon_getPositionIndices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkPolygon_getPolygonIndices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkPolygon_getIsPortal(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkPolygon_getIsOccluder(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkPolygon_getIsSector(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkPolygon_getShouldRelight(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkPolygon_getIsOutdoor(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkPolygon_getIsGhostOccluder(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkPolygon_getIsDynamicallyLit(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkPolygon_getIsLod(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte ZkPolygon_getNormalAxis(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate short ZkPolygon_getSectorIndex(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelScript_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelScript_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelScript_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelScript_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelScript_getSkeletonName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkModelScript_getSkeletonMeshDisabled(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelScript_getMeshCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelScript_getDisabledAnimationsCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelScript_getAnimationCombineCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelScript_getAnimationBlendCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelScript_getAnimationAliasCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelScript_getModelTagCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkModelScript_getAnimationCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelScript_getDisabledAnimation(UIntPtr slf, long i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelScript_getMesh(UIntPtr slf, long i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelScript_getAnimationCombine(UIntPtr slf, long i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelScript_getAnimationBlend(UIntPtr slf, long i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelScript_getAnimationAlias(UIntPtr slf, long i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkModelScript_getModelTag(UIntPtr slf, long i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkModelScript_getAnimation(UIntPtr slf, long i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelScript_enumerateAnimationCombines(UIntPtr slf, ZkAnimationCombineEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelScript_enumerateMeshes(UIntPtr slf, ZkStringEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelScript_enumerateDisabledAnimations(UIntPtr slf, ZkStringEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelScript_enumerateAnimationBlends(UIntPtr slf, ZkAnimationBlendEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelScript_enumerateAnimationAliases(UIntPtr slf, ZkAnimationAliasEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelScript_enumerateModelTags(UIntPtr slf, ZkStringEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkModelScript_enumerateAnimations(UIntPtr slf, ZkAnimationEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimation_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkAnimation_getLayer(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimation_getNext(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimation_getBlendIn(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimation_getBlendOut(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkAnimation_getFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimation_getModel(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AnimationDirection ZkAnimation_getDirection(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkAnimation_getFirstFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkAnimation_getLastFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimation_getCollisionVolumeScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkAnimation_getEventTagCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkAnimation_getParticleEffectCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkAnimation_getParticleEffectStopCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkAnimation_getSoundEffectCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkAnimation_getSoundEffectGroundCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkAnimation_getMorphAnimationCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkAnimation_getCameraTremorCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkAnimation_getEventTag(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkAnimation_getParticleEffect(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkAnimation_getParticleEffectStop(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkAnimation_getSoundEffect(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkAnimation_getSoundEffectGround(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkAnimation_getMorphAnimation(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkAnimation_getCameraTremor(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkAnimation_enumerateEventTags(UIntPtr slf, ZkEventTagEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkAnimation_enumerateParticleEffects(UIntPtr slf, ZkEventParticlEffectEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkAnimation_enumerateParticleEffectStops(UIntPtr slf,
			ZkEventParticleEffectStopEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkAnimation_enumerateSoundEffects(UIntPtr slf, ZkEventSoundEffectEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkAnimation_enumerateSoundEffectGrounds(UIntPtr slf, ZkEventSoundEffectGroundEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkAnimation_enumerateMorphAnimations(UIntPtr slf, ZkEventMorphAnimationEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkAnimation_enumerateCameraTremors(UIntPtr slf, ZkEventCameraTremorEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventTag_getFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate EventType ZkEventTag_getType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEventTag_getSlot(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEventTag_getItem(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEventTag_getFrames(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate FightMode ZkEventTag_getFightMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkEventTag_getIsAttached(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventParticleEffect_getFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventParticleEffect_getIndex(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEventParticleEffect_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEventParticleEffect_getPosition(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkEventParticleEffect_getIsAttached(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventParticleEffectStop_getFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventParticleEffectStop_getIndex(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventCameraTremor_getFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventCameraTremor_getField1(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventCameraTremor_getField2(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventCameraTremor_getField3(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventCameraTremor_getField4(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventSoundEffect_getFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEventSoundEffect_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEventSoundEffect_getRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkEventSoundEffect_getEmptySlot(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEventSoundEffectGround_getFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEventSoundEffectGround_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEventSoundEffectGround_getRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkEventSoundEffectGround_getEmptySlot(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMorphAnimation_getFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMorphAnimation_getAnimation(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMorphAnimation_getNode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimationCombine_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkAnimationCombine_getLayer(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimationCombine_getNext(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimationCombine_getBlendIn(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimationCombine_getBlendOut(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkAnimationCombine_getFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimationCombine_getModel(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkAnimationCombine_getLastFrame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimationBlend_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimationBlend_getNext(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimationBlend_getBlendIn(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimationBlend_getBlendOut(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimationAlias_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkAnimationAlias_getLayer(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimationAlias_getNext(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimationAlias_getBlendIn(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkAnimationAlias_getBlendOut(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkAnimationAlias_getFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkAnimationAlias_getAlias(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AnimationDirection ZkAnimationAlias_getDirection(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate BspTreeType ZkBspTree_getType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkBspTree_getPolygonIndices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkBspTree_getLeafPolygonIndices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkBspTree_getPortalPolygonIndices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkBspTree_getLightPoints(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkBspTree_getLeafNodeIndices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkBspTree_getNodes(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkBspTree_getSectorCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkBspTree_getSector(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkBspTree_enumerateSectors(UIntPtr slf, ZkBspSectorEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkBspSector_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkBspSector_getNodeIndices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkBspSector_getPortalPolygonIndices(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkWayNet_getEdges(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkWayNet_getPointCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkWayNet_getPoint(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkWayNet_enumeratePoints(UIntPtr slf, ZkWayPointEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkWayPoint_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkWayPoint_getWaterDepth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkWayPoint_getUnderWater(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkWayPoint_getPosition(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkWayPoint_getDirection(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkWayPoint_getFreePoint(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkWorld_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkWorld_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkWorld_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkWorld_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkWorld_getMesh(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkWorld_getBspTree(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkWorld_getWayNet(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkWorld_getRootObjectCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkWorld_getRootObject(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkWorld_enumerateRootObjects(UIntPtr slf, ZkVirtualObjectEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVirtualObject_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVirtualObject_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkVirtualObject_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate VirtualObjectType ZkVirtualObject_getType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkVirtualObject_getId(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AxisAlignedBoundingBox ZkVirtualObject_getBbox(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkVirtualObject_getPosition(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkMat3x3 ZkVirtualObject_getRotation(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVirtualObject_getShowVisual(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SpriteAlignment ZkVirtualObject_getSpriteCameraFacingMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVirtualObject_getCdStatic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVirtualObject_getCdDynamic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVirtualObject_getVobStatic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ShadowType ZkVirtualObject_getDynamicShadows(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVirtualObject_getPhysicsEnabled(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AnimationType ZkVirtualObject_getAnimMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkVirtualObject_getBias(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkVirtualObject_getAmbient(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkVirtualObject_getAnimStrength(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkVirtualObject_getFarClipScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkVirtualObject_getPresetName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkVirtualObject_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkVirtualObject_getVisualName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate VisualType ZkVirtualObject_getVisualType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVirtualObject_getVisualDecal(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkVirtualObject_getChildCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkVirtualObject_getChild(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkVirtualObject_enumerateChildren(UIntPtr slf, ZkVirtualObjectEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkDecal_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector2 ZkDecal_getDimension(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector2 ZkDecal_getOffset(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkDecal_getTwoSided(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AlphaFunction ZkDecal_getAlphaFunc(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkDecal_getTextureAnimFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte ZkDecal_getAlphaWeight(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkDecal_getIgnoreDaylight(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCutsceneCamera_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCutsceneCamera_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkCutsceneCamera_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate CameraTrajectory ZkCutsceneCamera_getTrajectoryFOR(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate CameraTrajectory ZkCutsceneCamera_getTargetTrajectoryFOR(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate CameraLoopType ZkCutsceneCamera_getLoopMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate CameraLerpType ZkCutsceneCamera_getLerpMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCutsceneCamera_getIgnoreFORVobRotation(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCutsceneCamera_getIgnoreFORVobRotationTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCutsceneCamera_getAdapt(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCutsceneCamera_getEaseFirst(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCutsceneCamera_getEaseLast(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCutsceneCamera_getTotalDuration(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkCutsceneCamera_getAutoFocusVob(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCutsceneCamera_getAutoPlayerMovable(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCutsceneCamera_getAutoUntriggerLast(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCutsceneCamera_getAutoUntriggerLastDelay(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkCutsceneCamera_getPositionCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkCutsceneCamera_getTargetCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkCutsceneCamera_getFrameCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCutsceneCamera_getFrame(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkCutsceneCamera_enumerateFrames(UIntPtr slf, ZkCameraTrajectoryFrameEnumerator cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraTrajectoryFrame_getTime(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraTrajectoryFrame_getRollAngle(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraTrajectoryFrame_getFovScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate CameraMotion ZkCameraTrajectoryFrame_getMotionType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate CameraMotion ZkCameraTrajectoryFrame_getMotionTypeFov(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate CameraMotion ZkCameraTrajectoryFrame_getMotionTypeRoll(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate CameraMotion ZkCameraTrajectoryFrame_getMotionTypeTimeScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraTrajectoryFrame_getTension(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraTrajectoryFrame_getCamBias(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraTrajectoryFrame_getContinuity(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraTrajectoryFrame_getTimeScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCameraTrajectoryFrame_getTimeFixed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkMat4x4 ZkCameraTrajectoryFrame_getOriginalPose(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkLightPreset_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkLightPreset_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkLightPreset_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkLight_load(UIntPtr slf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkLight_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkLight_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkLightPreset_getPreset(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate LightType ZkLightPreset_getLightType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkLightPreset_getRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkColor ZkLightPreset_getColor(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkLightPreset_getConeAngle(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLightPreset_getIsStatic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate LightQuality ZkLightPreset_getQuality(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkLightPreset_getLensflareFx(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLightPreset_getOn(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkLightPreset_getRangeAnimationScale(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkLightPreset_getRangeAnimationFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLightPreset_getRangeAnimationSmooth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkLightPreset_getColorAnimationList(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkLightPreset_getColorAnimationFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLightPreset_getColorAnimationSmooth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLightPreset_getCanMove(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkLight_getPreset(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate LightType ZkLight_getLightType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkLight_getRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkColor ZkLight_getColor(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkLight_getConeAngle(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLight_getIsStatic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate LightQuality ZkLight_getQuality(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkLight_getLensflareFx(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLight_getOn(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkLight_getRangeAnimationScale(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkLight_getRangeAnimationFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLight_getRangeAnimationSmooth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkLight_getColorAnimationList(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkLight_getColorAnimationFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLight_getColorAnimationSmooth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkLight_getCanMove(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkAnimate_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkAnimate_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkAnimate_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkAnimate_getStartOn(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkItem_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkItem_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkItem_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkItem_getInstance(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkLensFlare_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkLensFlare_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkLensFlare_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkLensFlare_getEffect(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkParticleEffectController_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkParticleEffectController_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkParticleEffectController_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectController_getEffectName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkParticleEffectController_getKillWhenDone(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkParticleEffectController_getInitiallyRunning(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMessageFilter_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMessageFilter_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMessageFilter_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMessageFilter_getTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MessageFilterAction ZkMessageFilter_getOnTrigger(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MessageFilterAction ZkMessageFilter_getOnUntrigger(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCodeMaster_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkCodeMaster_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkCodeMaster_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkCodeMaster_getTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCodeMaster_getOrdered(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCodeMaster_getFirstFalseIsFailure(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkCodeMaster_getFailureTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkCodeMaster_getUntriggeredCancels(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkCodeMaster_getSlaveCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkCodeMaster_getSlave(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkCodeMaster_enumerateSlaves(UIntPtr slf, ZkStringEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMoverController_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMoverController_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMoverController_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMoverController_getTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MoverMessageType ZkMoverController_getMessage(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMoverController_getKey(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTouchDamage_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTouchDamage_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTouchDamage_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkTouchDamage_getDamage(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkTouchDamage_getIsBarrier(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkTouchDamage_getIsBlunt(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkTouchDamage_getIsEdge(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkTouchDamage_getIsFire(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkTouchDamage_getIsFly(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkTouchDamage_getIsMagic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkTouchDamage_getIsPoint(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkTouchDamage_getIsFall(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkTouchDamage_getRepeatDelaySeconds(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkTouchDamage_getVolumeScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate TouchCollisionType ZkTouchDamage_getCollisionType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkEarthquake_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkEarthquake_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkEarthquake_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEarthquake_getRadius(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEarthquake_getDuration(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Vector3 ZkEarthquake_getAmplitude(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMovableObject_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMovableObject_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMovableObject_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMovableObject_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMovableObject_getHp(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMovableObject_getDamage(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMovableObject_getMovable(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMovableObject_getTakable(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMovableObject_getFocusOverride(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SoundMaterialType ZkMovableObject_getMaterial(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMovableObject_getVisualDestroyed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMovableObject_getOwner(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMovableObject_getOwnerGuild(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMovableObject_getDestroyed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkInteractiveObject_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkInteractiveObject_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkInteractiveObject_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkInteractiveObject_getState(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkInteractiveObject_getTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkInteractiveObject_getItem(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkInteractiveObject_getConditionFunction(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkInteractiveObject_getOnStateChangeFunction(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkInteractiveObject_getRewind(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkFire_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkFire_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkFire_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkFire_getSlot(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkFire_getVobTree(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkContainer_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkContainer_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkContainer_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkContainer_getIsLocked(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkContainer_getKey(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkContainer_getPickString(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkContainer_getContents(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDoor_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDoor_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDoor_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkDoor_getIsLocked(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkDoor_getKey(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkDoor_getPickString(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkSound_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkSound_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkSound_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSound_getVolume(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SoundMode ZkSound_getMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSound_getRandomDelay(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSound_getRandomDelayVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkSound_getInitiallyPlaying(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkSound_getAmbient3d(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkSound_getObstruction(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSound_getConeAngle(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SoundTriggerVolumeType ZkSound_getVolumeType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSound_getRadius(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSound_getSoundName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkSoundDaytime_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkSoundDaytime_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkSoundDaytime_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSoundDaytime_getStartTime(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSoundDaytime_getEndTime(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSoundDaytime_getSoundNameDaytime(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTrigger_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTrigger_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTrigger_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTrigger_getTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte ZkTrigger_getFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte ZkTrigger_getFilterFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTrigger_getVobTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkTrigger_getMaxActivationCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkTrigger_getRetriggerDelaySeconds(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkTrigger_getDamageThreshold(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkTrigger_getFireDelaySeconds(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMover_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkMover_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkMover_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MoverBehavior ZkMover_getBehavior(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMover_getTouchBlockerDamage(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMover_getStayOpenTimeSeconds(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMover_getIsLocked(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMover_getAutoLink(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkMover_getAutoRotate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMover_getSpeed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MoverLerpType ZkMover_getLerpType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MoverSpeedType ZkMover_getSpeedType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMover_getKeyframes(UIntPtr slf, out ulong count);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMover_getSfxOpenStart(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMover_getSfxOpenEnd(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMover_getSfxTransitioning(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMover_getSfxCloseStart(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMover_getSfxCloseEnd(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMover_getSfxLock(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMover_getSfxUnlock(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMover_getSfxUseLocked(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerList_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerList_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTriggerList_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate TriggerBatchMode ZkTriggerList_getMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ZkTriggerList_getTargetCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerList_getTarget(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTriggerList_enumerateTargets(UIntPtr slf, ZkTriggerListTargetEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTriggerListTarget_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkTriggerListTarget_getDelaySeconds(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerScript_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerScript_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTriggerScript_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTriggerScript_getFunction(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerChangeLevel_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerChangeLevel_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTriggerChangeLevel_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTriggerChangeLevel_getLevelName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTriggerChangeLevel_getStartVob(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerWorldStart_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerWorldStart_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTriggerWorldStart_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTriggerWorldStart_getTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkTriggerWorldStart_getFireOnce(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerUntouch_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkTriggerUntouch_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkTriggerUntouch_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkTriggerUntouch_getTarget(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkZoneMusic_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkZoneMusic_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkZoneMusic_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkZoneMusic_getIsEnabled(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkZoneMusic_getPriority(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkZoneMusic_getIsEllipsoid(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkZoneMusic_getReverb(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkZoneMusic_getVolume(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkZoneMusic_getIsLoop(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkZoneFarPlane_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkZoneFarPlane_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkZoneFarPlane_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkZoneFarPlane_getVobFarPlaneZ(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkZoneFarPlane_getInnerRangePercentage(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkZoneFog_load(UIntPtr buf, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkZoneFog_loadPath(string path, GameVersion version);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkZoneFog_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkZoneFog_getRangeCenter(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkZoneFog_getInnerRangePercentage(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ZkColor ZkZoneFog_getColor(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkZoneFog_getFadeOutSky(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkZoneFog_getOverrideColor(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusScript_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusScript_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusScript_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusScript_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkDaedalusScript_getSymbolCount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusScript_enumerateSymbols(UIntPtr slf, ZkDaedalusSymbolEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusScript_enumerateInstanceSymbols(UIntPtr slf, string className,
			ZkDaedalusSymbolEnumerator cb, UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate DaedalusInstruction ZkDaedalusScript_getInstruction(UIntPtr slf, ulong address);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusScript_getSymbolByIndex(UIntPtr slf, uint i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusScript_getSymbolByAddress(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusScript_getSymbolByName(UIntPtr slf, string i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkDaedalusSymbol_getString(UIntPtr slf, ushort index, UIntPtr context);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkDaedalusSymbol_getFloat(UIntPtr slf, ushort index, UIntPtr context);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkDaedalusSymbol_getInt(UIntPtr slf, ushort index, UIntPtr context);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusSymbol_setString(UIntPtr slf, string value, ushort index, UIntPtr context);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusSymbol_setFloat(UIntPtr slf, float value, ushort index, UIntPtr context);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusSymbol_setInt(UIntPtr slf, int value, ushort index, UIntPtr context);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkDaedalusSymbol_getIsConst(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkDaedalusSymbol_getIsMember(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkDaedalusSymbol_getIsExternal(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkDaedalusSymbol_getIsMerged(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkDaedalusSymbol_getIsGenerated(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ZkDaedalusSymbol_getHasReturn(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkDaedalusSymbol_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkDaedalusSymbol_getAddress(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkDaedalusSymbol_getParent(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkDaedalusSymbol_getSize(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate DaedalusDataType ZkDaedalusSymbol_getType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkDaedalusSymbol_getIndex(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate DaedalusDataType ZkDaedalusSymbol_getReturnType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate DaedalusInstanceType ZkDaedalusInstance_getType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkDaedalusInstance_getIndex(UIntPtr ptr);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_load(UIntPtr buf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_loadPath(string path);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_loadVfs(UIntPtr vfs, string name);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_del(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_pushInt(UIntPtr slf, int value);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_pushFloat(UIntPtr slf, float value);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_pushString(UIntPtr slf, string value);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_pushInstance(UIntPtr slf, UIntPtr value);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkDaedalusVm_popInt(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkDaedalusVm_popFloat(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkDaedalusVm_popString(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_popInstance(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_getGlobalSelf(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_getGlobalOther(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_getGlobalVictim(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_getGlobalHero(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_getGlobalItem(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_setGlobalSelf(UIntPtr slf, UIntPtr value);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_setGlobalOther(UIntPtr slf, UIntPtr value);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_setGlobalVictim(UIntPtr slf, UIntPtr value);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_setGlobalHero(UIntPtr slf, UIntPtr value);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_setGlobalItem(UIntPtr slf, UIntPtr value);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_callFunction(UIntPtr slf, UIntPtr sym);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate UIntPtr ZkDaedalusVm_initInstance(UIntPtr slf, UIntPtr sym, DaedalusInstanceType type);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_registerExternal(UIntPtr slf, UIntPtr sym, ZkDaedalusVmExternalCallback cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_registerExternalDefault(UIntPtr slf, ZkDaedalusVmExternalDefaultCallback cb,
			UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ZkDaedalusVm_printStackTrace(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getWaterDepthKnee(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getWaterDepthChest(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getJumpUpHeight(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getSwimTime(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getDiveTime(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getStepHeight(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getJumpLowHeight(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getJumpMidHeight(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getSlideAngle(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getSlideAngle2(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getDisableAutoRoll(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getSurfaceAlign(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getClimbHeadingAngle(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getClimbHorizAngle(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getClimbGroundAngle(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getFightRangeBase(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getFightRangeFist(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getFightRangeG(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getFightRange1Hs(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getFightRange1Ha(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getFightRange2Hs(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getFightRange2Ha(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getFallDownHeight(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getFallDownDamage(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getBloodDisabled(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getBloodMaxDistance(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getBloodAmount(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getBloodFlow(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkGuildValuesInstance_getTurnSpeed(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkGuildValuesInstance_getBloodEmitter(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkGuildValuesInstance_getBloodTexture(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getId(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkNpcInstance_getSlot(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkNpcInstance_getEffect(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NpcType ZkNpcInstance_getType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ZkNpcInstance_getFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getDamageType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getGuild(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getLevel(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getFightTactic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getWeapon(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getVoice(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getVoicePitch(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getBodyMass(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getDailyRoutine(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getStartAiState(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkNpcInstance_getSpawnPoint(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getSpawnDelay(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getSenses(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getSensesRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkNpcInstance_getWp(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getExp(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getExpNext(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getLp(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getBodyStateInterruptableOverride(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getNoFocus(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkNpcInstance_getName(UIntPtr slf, NpcNameSlot slot);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getMission(UIntPtr slf, NpcMissionSlot slot);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getAttribute(UIntPtr slf, NpcAttribute attribute);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getHitChance(UIntPtr slf, NpcTalent talent);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getProtection(UIntPtr slf, DamageType type);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getDamage(UIntPtr slf, DamageType type);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkNpcInstance_getAiVar(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMissionInstance_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMissionInstance_getDescription(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getDuration(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getImportant(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getOfferConditions(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getOffer(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getSuccessConditions(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getSuccess(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getFailureConditions(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getFailure(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getObsoleteConditions(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getObsolete(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMissionInstance_getRunning(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getId(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkItemInstance_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkItemInstance_getNameId(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getHp(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getHpMax(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getMainFlag(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getWeight(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getValue(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getDamageType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getDamageTotal(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getWear(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getNutrition(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getMagic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getOnEquip(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getOnUnequip(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getOwner(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getOwnerGuild(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getDisguiseGuild(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkItemInstance_getVisual(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkItemInstance_getVisualChange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkItemInstance_getEffect(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getVisualSkin(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkItemInstance_getSchemeName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getMaterial(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getMunition(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getSpell(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getMagCircle(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkItemInstance_getDescription(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getInvZBias(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getInvRotX(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getInvRotY(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getInvRotZ(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getInvAnimate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getDamage(UIntPtr slf, DamageType type);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getProtection(UIntPtr slf, DamageType type);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getCondAtr(UIntPtr slf, ItemConditionSlot slot);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getCondValue(UIntPtr slf, ItemConditionSlot slot);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getChangeAtr(UIntPtr slf, ItemConditionSlot slot);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getChangeValue(UIntPtr slf, ItemConditionSlot slot);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getOnState(UIntPtr slf, ItemStateSlot slot);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkItemInstance_getText(UIntPtr slf, ItemTextSlot slot);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemInstance_getCount(UIntPtr slf, ItemTextSlot slot);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getNpcLongrange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getNpcRange1(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getNpcRange2(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getNpcAzi(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getNpcElevdo(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getNpcElevup(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkFocusInstance_getNpcPrio(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getItemRange1(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getItemRange2(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getItemAzi(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getItemElevdo(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getItemElevup(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkFocusInstance_getItemPrio(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getMobRange1(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getMobRange2(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getMobAzi(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getMobElevdo(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkFocusInstance_getMobElevup(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkFocusInstance_getMobPrio(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkInfoInstance_getNpc(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkInfoInstance_getNr(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkInfoInstance_getImportant(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkInfoInstance_getCondition(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkInfoInstance_getInformation(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkInfoInstance_getDescription(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkInfoInstance_getTrade(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkInfoInstance_getPermanent(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemReactInstance_getNpc(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemReactInstance_getTradeItem(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemReactInstance_getTradeAmount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemReactInstance_getRequestedCategory(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemReactInstance_getRequestedItem(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemReactInstance_getRequestedAmount(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkItemReactInstance_getReaction(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSpellInstance_getTimePerMana(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getDamagePerLevel(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getDamageType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getSpellType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getCanTurnDuringInvest(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getCanChangeTargetDuringInvest(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getIsMultiEffect(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getTargetCollectAlgo(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getTargetCollectType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getTargetCollectRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getTargetCollectAzi(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSpellInstance_getTargetCollectElevation(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuInstance_getBackPic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuInstance_getBackWorld(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuInstance_getPosX(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuInstance_getPosY(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuInstance_getDimX(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuInstance_getDimY(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuInstance_getAlpha(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuInstance_getMusicTheme(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuInstance_getEventTimerMsec(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuInstance_getFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuInstance_getDefaultOutgame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuInstance_getDefaultIngame(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuInstance_getItem(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getFontName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getBackpic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getAlphaMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getAlpha(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MenuItemType ZkMenuItemInstance_getType(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getOnChgSetOption(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getOnChgSetOptionSection(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getPosX(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getPosY(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getDimX(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getDimY(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMenuItemInstance_getSizeStartScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getFlags(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMenuItemInstance_getOpenDelayTime(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMenuItemInstance_getOpenDuration(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getFramePosX(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getFramePosY(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getFrameSizeX(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getFrameSizeY(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getHideIfOptionSectionSet(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getHideIfOptionSet(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getHideOnValue(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getText(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getOnSelAction(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getOnSelActionS(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMenuItemInstance_getOnEventAction(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMenuItemInstance_getUserFloat(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMenuItemInstance_getUserString(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getBestRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getMinRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getMaxRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getBestElevation(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getMinElevation(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getMaxElevation(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getBestAzimuth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getMinAzimuth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getMaxAzimuth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getBestRotZ(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getMinRotZ(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getMaxRotZ(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getRotOffsetX(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getRotOffsetY(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getRotOffsetZ(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getTargetOffsetX(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getTargetOffsetY(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getTargetOffsetZ(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getVeloTrans(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkCameraInstance_getVeloRot(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkCameraInstance_getTranslate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkCameraInstance_getRotate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkCameraInstance_getCollision(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMusicSystemInstance_getVolume(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMusicSystemInstance_getBitResolution(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMusicSystemInstance_getGlobalReverbEnabled(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMusicSystemInstance_getSampleRate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMusicSystemInstance_getNumChannels(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMusicSystemInstance_getReverbBufferSize(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMusicThemeInstance_getFile(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMusicThemeInstance_getVol(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMusicThemeInstance_getLoop(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMusicThemeInstance_getReverbmix(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMusicThemeInstance_getReverbtime(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MusicTransitionEffect ZkMusicThemeInstance_getTranstype(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate MusicTransitionType ZkMusicThemeInstance_getTranssubtype(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkMusicJingleInstance_getName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMusicJingleInstance_getLoop(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkMusicJingleInstance_getVol(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkMusicJingleInstance_getTranssubtype(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getPpsValue(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getPpsScaleKeysS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getPpsIsLooping(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getPpsIsSmooth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getPpsFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getPpsCreateEmS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getPpsCreateEmDelay(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getShpTypeS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getShpForS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getShpOffsetVecS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getShpDistribTypeS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getShpDistribWalkSpeed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getShpIsVolume(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getShpDimS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getShpMeshS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getShpMeshRenderB(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getShpScaleKeysS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getShpScaleIsLooping(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getShpScaleIsSmooth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getShpScaleFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getDirModeS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getDirForS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getDirModeTargetForS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getDirModeTargetPosS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getDirAngleHead(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getDirAngleHeadVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getDirAngleElev(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getDirAngleElevVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getVelAvg(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getVelVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getLspPartAvg(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getLspPartVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getFlyGravityS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getFlyColldetB(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getVisNameS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getVisOrientationS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getVisTexIsQuadpoly(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getVisTexAniFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getVisTexAniIsLooping(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getVisTexColorStartS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getVisTexColorEndS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getVisSizeStartS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getVisSizeEndScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getVisAlphaFuncS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getVisAlphaStart(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getVisAlphaEnd(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getTrlFadeSpeed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getTrlTextureS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getTrlWidth(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getMrkFadesPeed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getMrktExtureS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getMrkSize(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getFlockMode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectInstance_getFlockStrength(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getUseEmittersFor(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectInstance_getTimeStartEndS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectInstance_getMBiasAmbientPfx(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getVisNameS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getVisSizeS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getVisAlpha(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getVisAlphaBlendFuncS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getVisTexAniFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEffectBaseInstance_getVisTexAniIsLooping(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmTrjModeS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmTrjOriginNode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmTrjTargetNode(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmTrjTargetRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmTrjTargetAzi(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmTrjTargetElev(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEffectBaseInstance_getEmTrjNumKeys(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEffectBaseInstance_getEmTrjNumKeysVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmTrjAngleElevVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmTrjAngleHeadVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmTrjKeyDistVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmTrjLoopModeS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmTrjEaseFuncS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmTrjEaseVel(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmTrjDynUpdateDelay(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmFxCreateS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmFxInvestOriginS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmFxInvestTargetS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmFxTriggerDelay(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEffectBaseInstance_getEmFxCreateDownTrj(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmActionCollDynS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmActionCollStatS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmFxCollStatS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmFxCollDynS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmFxCollStatAlignS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmFxCollDynAlignS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmFxLifespan(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEffectBaseInstance_getEmCheckCollision(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEffectBaseInstance_getEmAdjustShpToOrigin(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmInvestNextKeyDuration(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getEmFlyGravity(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmSelfRotVelS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getLightPresetName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getSfxId(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEffectBaseInstance_getSfxIsAmbient(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkEffectBaseInstance_getSendAssessMagic(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkEffectBaseInstance_getSecsPerDamage(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getEmFxCollDynPercS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkEffectBaseInstance_getUserString(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getVisNameS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getVisSizeScale(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getScaleDuration(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getPfxPpsValue(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getPfxScTime(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getPfxShpDimS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirModeS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirForS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getPfxVelAvg(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getLightPresetName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getLightRange(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getSfxId(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectEmitKeyInstance_getSfxIsAmbient(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getEmCreateFxId(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getEmFlyGravity(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkParticleEffectEmitKeyInstance_getEmTrjModeS(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkParticleEffectEmitKeyInstance_getEmCheckCollision(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkParticleEffectEmitKeyInstance_getEmFxLifespan(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate FightAiMove ZkFightAiInstance_getMove(UIntPtr slf, ulong i);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSoundEffectInstance_getFile(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundEffectInstance_getPitchOff(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundEffectInstance_getPitchVar(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundEffectInstance_getVolume(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundEffectInstance_getLoop(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundEffectInstance_getLoopStartOffset(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundEffectInstance_getLoopEndOffset(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSoundEffectInstance_getReverbLevel(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSoundEffectInstance_getPfxName(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float ZkSoundSystemInstance_getVolume(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundSystemInstance_getBitResolution(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundSystemInstance_getSampleRate(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundSystemInstance_getUseStereo(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int ZkSoundSystemInstance_getNumSfxChannels(UIntPtr slf);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ZkSoundSystemInstance_getUsed3DProviderName(UIntPtr slf);
	}

	namespace NativeStructs
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