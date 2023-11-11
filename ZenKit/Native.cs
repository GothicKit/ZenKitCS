using System;
using System.Runtime.InteropServices;
using System.Text;
using NativeLibraryLoader;
using ZenKit.NativeLoader;
using ZenKit.NativeLoader.NativeFunctions;

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
		public static ZkAnimation_getFps ZkAnimation_getFps;
		public static ZkAnimation_getSpeed ZkAnimation_getSpeed;
		public static ZkLogger_set ZkLogger_set;
		public static ZkLogger_setDefault ZkLogger_setDefault;
		public static ZkLogger_log ZkLogger_log;
		public static ZkRead_newFile ZkRead_newFile;
		public static ZkRead_newMem ZkRead_newMem;
		public static ZkRead_newPath ZkRead_newPath;
		public static ZkRead_newExt ZkRead_newExt;
		public static ZkRead_del ZkRead_del;
		public static ZkVfs_new ZkVfs_new;
		public static ZkVfs_del ZkVfs_del;
		public static ZkVfs_getRoot ZkVfs_getRoot;
		public static ZkVfs_mkdir ZkVfs_mkdir;
		public static ZkVfs_remove ZkVfs_remove;
		public static ZkVfs_mount ZkVfs_mount;
		public static ZkVfs_mountHost ZkVfs_mountHost;
		public static ZkVfs_mountDisk ZkVfs_mountDisk;
		public static ZkVfs_mountDiskHost ZkVfs_mountDiskHost;
		public static ZkVfs_resolvePath ZkVfs_resolvePath;
		public static ZkVfs_findNode ZkVfs_findNode;
		public static ZkVfsNode_newFile ZkVfsNode_newFile;
		public static ZkVfsNode_newDir ZkVfsNode_newDir;
		public static ZkVfsNode_del ZkVfsNode_del;
		public static ZkVfsNode_isFile ZkVfsNode_isFile;
		public static ZkVfsNode_isDir ZkVfsNode_isDir;
		public static ZkVfsNode_getTime ZkVfsNode_getTime;
		public static ZkVfsNode_getName ZkVfsNode_getName;
		public static ZkVfsNode_getChild ZkVfsNode_getChild;
		public static ZkVfsNode_create ZkVfsNode_create;
		public static ZkVfsNode_remove ZkVfsNode_remove;
		public static ZkVfsNode_open ZkVfsNode_open;
		public static ZkVfsNode_enumerateChildren ZkVfsNode_enumerateChildren;
		public static ZkCutsceneLibrary_load ZkCutsceneLibrary_load;
		public static ZkCutsceneLibrary_loadPath ZkCutsceneLibrary_loadPath;
		public static ZkCutsceneLibrary_loadVfs ZkCutsceneLibrary_loadVfs;
		public static ZkCutsceneLibrary_del ZkCutsceneLibrary_del;
		public static ZkCutsceneLibrary_getBlock ZkCutsceneLibrary_getBlock;
		public static ZkCutsceneLibrary_enumerateBlocks ZkCutsceneLibrary_enumerateBlocks;
		public static ZkCutsceneBlock_getName ZkCutsceneBlock_getName;
		public static ZkCutsceneBlock_getMessage ZkCutsceneBlock_getMessage;
		public static ZkCutsceneMessage_getType ZkCutsceneMessage_getType;
		public static ZkCutsceneMessage_getText ZkCutsceneMessage_getText;
		public static ZkCutsceneMessage_getName ZkCutsceneMessage_getName;
		public static ZkFont_load ZkFont_load;
		public static ZkFont_loadPath ZkFont_loadPath;
		public static ZkFont_loadVfs ZkFont_loadVfs;
		public static ZkFont_del ZkFont_del;
		public static ZkFont_getName ZkFont_getName;
		public static ZkFont_getHeight ZkFont_getHeight;
		public static ZkFont_getGlyphCount ZkFont_getGlyphCount;
		public static ZkFont_getGlyph ZkFont_getGlyph;
		public static ZkFont_enumerateGlyphs ZkFont_enumerateGlyphs;
		public static ZkModelAnimation_load ZkModelAnimation_load;
		public static ZkModelAnimation_loadPath ZkModelAnimation_loadPath;
		public static ZkModelAnimation_loadVfs ZkModelAnimation_loadVfs;
		public static ZkModelAnimation_del ZkModelAnimation_del;
		public static ZkModelAnimation_getName ZkModelAnimation_getName;
		public static ZkModelAnimation_getNext ZkModelAnimation_getNext;
		public static ZkModelAnimation_getLayer ZkModelAnimation_getLayer;
		public static ZkModelAnimation_getFrameCount ZkModelAnimation_getFrameCount;
		public static ZkModelAnimation_getNodeCount ZkModelAnimation_getNodeCount;
		public static ZkModelAnimation_getFps ZkModelAnimation_getFps;
		public static ZkModelAnimation_getFpsSource ZkModelAnimation_getFpsSource;
		public static ZkModelAnimation_getBbox ZkModelAnimation_getBbox;
		public static ZkModelAnimation_getChecksum ZkModelAnimation_getChecksum;
		public static ZkModelAnimation_getSourcePath ZkModelAnimation_getSourcePath;
		public static ZkModelAnimation_getSourceDate ZkModelAnimation_getSourceDate;
		public static ZkModelAnimation_getSourceScript ZkModelAnimation_getSourceScript;
		public static ZkModelAnimation_getSampleCount ZkModelAnimation_getSampleCount;
		public static ZkModelAnimation_getSample ZkModelAnimation_getSample;
		public static ZkModelAnimation_enumerateSamples ZkModelAnimation_enumerateSamples;
		public static ZkModelAnimation_getNodeIndices ZkModelAnimation_getNodeIndices;
		public static ZkModelHierarchy_load ZkModelHierarchy_load;
		public static ZkModelHierarchy_loadPath ZkModelHierarchy_loadPath;
		public static ZkModelHierarchy_loadVfs ZkModelHierarchy_loadVfs;
		public static ZkModelHierarchy_del ZkModelHierarchy_del;
		public static ZkModelHierarchy_getNodeCount ZkModelHierarchy_getNodeCount;
		public static ZkModelHierarchy_getNode ZkModelHierarchy_getNode;
		public static ZkModelHierarchy_getBbox ZkModelHierarchy_getBbox;
		public static ZkModelHierarchy_getCollisionBbox ZkModelHierarchy_getCollisionBbox;
		public static ZkModelHierarchy_getRootTranslation ZkModelHierarchy_getRootTranslation;
		public static ZkModelHierarchy_getChecksum ZkModelHierarchy_getChecksum;
		public static ZkModelHierarchy_getSourceDate ZkModelHierarchy_getSourceDate;
		public static ZkModelHierarchy_getSourcePath ZkModelHierarchy_getSourcePath;
		public static ZkModelHierarchy_enumerateNodes ZkModelHierarchy_enumerateNodes;
		public static ZkOrientedBoundingBox_getCenter ZkOrientedBoundingBox_getCenter;
		public static ZkOrientedBoundingBox_getAxis ZkOrientedBoundingBox_getAxis;
		public static ZkOrientedBoundingBox_getHalfWidth ZkOrientedBoundingBox_getHalfWidth;
		public static ZkOrientedBoundingBox_getChildCount ZkOrientedBoundingBox_getChildCount;
		public static ZkOrientedBoundingBox_getChild ZkOrientedBoundingBox_getChild;
		public static ZkOrientedBoundingBox_enumerateChildren ZkOrientedBoundingBox_enumerateChildren;
		public static ZkOrientedBoundingBox_toAabb ZkOrientedBoundingBox_toAabb;
		public static ZkMaterial_load ZkMaterial_load;
		public static ZkMaterial_loadPath ZkMaterial_loadPath;
		public static ZkMaterial_del ZkMaterial_del;
		public static ZkMaterial_getName ZkMaterial_getName;
		public static ZkMaterial_getGroup ZkMaterial_getGroup;
		public static ZkMaterial_getColor ZkMaterial_getColor;
		public static ZkMaterial_getSmoothAngle ZkMaterial_getSmoothAngle;
		public static ZkMaterial_getTexture ZkMaterial_getTexture;
		public static ZkMaterial_getTextureScale ZkMaterial_getTextureScale;
		public static ZkMaterial_getTextureAnimationFps ZkMaterial_getTextureAnimationFps;
		public static ZkMaterial_getTextureAnimationMapping ZkMaterial_getTextureAnimationMapping;
		public static ZkMaterial_getTextureAnimationMappingDirection ZkMaterial_getTextureAnimationMappingDirection;
		public static ZkMaterial_getDisableCollision ZkMaterial_getDisableCollision;
		public static ZkMaterial_getDisableLightmap ZkMaterial_getDisableLightmap;
		public static ZkMaterial_getDontCollapse ZkMaterial_getDontCollapse;
		public static ZkMaterial_getDetailObject ZkMaterial_getDetailObject;
		public static ZkMaterial_getDetailObjectScale ZkMaterial_getDetailObjectScale;
		public static ZkMaterial_getForceOccluder ZkMaterial_getForceOccluder;
		public static ZkMaterial_getEnvironmentMapping ZkMaterial_getEnvironmentMapping;
		public static ZkMaterial_getEnvironmentMappingStrength ZkMaterial_getEnvironmentMappingStrength;
		public static ZkMaterial_getWaveMode ZkMaterial_getWaveMode;
		public static ZkMaterial_getWaveSpeed ZkMaterial_getWaveSpeed;
		public static ZkMaterial_getWaveAmplitude ZkMaterial_getWaveAmplitude;
		public static ZkMaterial_getWaveGridSize ZkMaterial_getWaveGridSize;
		public static ZkMaterial_getIgnoreSun ZkMaterial_getIgnoreSun;
		public static ZkMaterial_getAlphaFunction ZkMaterial_getAlphaFunction;
		public static ZkMaterial_getDefaultMapping ZkMaterial_getDefaultMapping;
		public static ZkMultiResolutionMesh_load ZkMultiResolutionMesh_load;
		public static ZkMultiResolutionMesh_loadPath ZkMultiResolutionMesh_loadPath;
		public static ZkMultiResolutionMesh_loadVfs ZkMultiResolutionMesh_loadVfs;
		public static ZkMultiResolutionMesh_del ZkMultiResolutionMesh_del;
		public static ZkMultiResolutionMesh_getPositions ZkMultiResolutionMesh_getPositions;
		public static ZkMultiResolutionMesh_getNormals ZkMultiResolutionMesh_getNormals;
		public static ZkMultiResolutionMesh_getSubMeshCount ZkMultiResolutionMesh_getSubMeshCount;
		public static ZkMultiResolutionMesh_getSubMesh ZkMultiResolutionMesh_getSubMesh;
		public static ZkMultiResolutionMesh_enumerateSubMeshes ZkMultiResolutionMesh_enumerateSubMeshes;
		public static ZkMultiResolutionMesh_getMaterialCount ZkMultiResolutionMesh_getMaterialCount;
		public static ZkMultiResolutionMesh_getMaterial ZkMultiResolutionMesh_getMaterial;
		public static ZkMultiResolutionMesh_enumerateMaterials ZkMultiResolutionMesh_enumerateMaterials;
		public static ZkMultiResolutionMesh_getAlphaTest ZkMultiResolutionMesh_getAlphaTest;
		public static ZkMultiResolutionMesh_getBbox ZkMultiResolutionMesh_getBbox;
		public static ZkMultiResolutionMesh_getOrientedBbox ZkMultiResolutionMesh_getOrientedBbox;
		public static ZkSubMesh_getMaterial ZkSubMesh_getMaterial;
		public static ZkSubMesh_getTriangles ZkSubMesh_getTriangles;
		public static ZkSubMesh_getWedges ZkSubMesh_getWedges;
		public static ZkSubMesh_getColors ZkSubMesh_getColors;
		public static ZkSubMesh_getTrianglePlaneIndices ZkSubMesh_getTrianglePlaneIndices;
		public static ZkSubMesh_getTrianglePlanes ZkSubMesh_getTrianglePlanes;
		public static ZkSubMesh_getTriangleEdges ZkSubMesh_getTriangleEdges;
		public static ZkSubMesh_getEdges ZkSubMesh_getEdges;
		public static ZkSubMesh_getEdgeScores ZkSubMesh_getEdgeScores;
		public static ZkSubMesh_getWedgeMap ZkSubMesh_getWedgeMap;
		public static ZkSoftSkinMesh_getNodeCount ZkSoftSkinMesh_getNodeCount;
		public static ZkSoftSkinMesh_getMesh ZkSoftSkinMesh_getMesh;
		public static ZkSoftSkinMesh_getBbox ZkSoftSkinMesh_getBbox;
		public static ZkSoftSkinMesh_enumerateBoundingBoxes ZkSoftSkinMesh_enumerateBoundingBoxes;
		public static ZkSoftSkinMesh_getWeights ZkSoftSkinMesh_getWeights;
		public static ZkSoftSkinMesh_enumerateWeights ZkSoftSkinMesh_enumerateWeights;
		public static ZkSoftSkinMesh_getWedgeNormals ZkSoftSkinMesh_getWedgeNormals;
		public static ZkSoftSkinMesh_getNodes ZkSoftSkinMesh_getNodes;
		public static ZkModelMesh_load ZkModelMesh_load;
		public static ZkModelMesh_loadPath ZkModelMesh_loadPath;
		public static ZkModelMesh_loadVfs ZkModelMesh_loadVfs;
		public static ZkModelMesh_del ZkModelMesh_del;
		public static ZkModelMesh_getMeshCount ZkModelMesh_getMeshCount;
		public static ZkModelMesh_getMesh ZkModelMesh_getMesh;
		public static ZkModelMesh_enumerateMeshes ZkModelMesh_enumerateMeshes;
		public static ZkModelMesh_getAttachmentCount ZkModelMesh_getAttachmentCount;
		public static ZkModelMesh_getAttachment ZkModelMesh_getAttachment;
		public static ZkModelMesh_enumerateAttachments ZkModelMesh_enumerateAttachments;
		public static ZkModelMesh_getChecksum ZkModelMesh_getChecksum;
		public static ZkModel_load ZkModel_load;
		public static ZkModel_loadPath ZkModel_loadPath;
		public static ZkModel_loadVfs ZkModel_loadVfs;
		public static ZkModel_del ZkModel_del;
		public static ZkModel_getHierarchy ZkModel_getHierarchy;
		public static ZkModel_getMesh ZkModel_getMesh;
		public static ZkTexture_load ZkTexture_load;
		public static ZkTexture_loadPath ZkTexture_loadPath;
		public static ZkTexture_loadVfs ZkTexture_loadVfs;
		public static ZkTexture_del ZkTexture_del;
		public static ZkTexture_getFormat ZkTexture_getFormat;
		public static ZkTexture_getWidth ZkTexture_getWidth;
		public static ZkTexture_getHeight ZkTexture_getHeight;
		public static ZkTexture_getWidthMipmap ZkTexture_getWidthMipmap;
		public static ZkTexture_getHeightMipmap ZkTexture_getHeightMipmap;
		public static ZkTexture_getWidthRef ZkTexture_getWidthRef;
		public static ZkTexture_getHeightRef ZkTexture_getHeightRef;
		public static ZkTexture_getMipmapCount ZkTexture_getMipmapCount;
		public static ZkTexture_getAverageColor ZkTexture_getAverageColor;
		public static ZkTexture_getPalette ZkTexture_getPalette;
		public static ZkTexture_getMipmapRaw ZkTexture_getMipmapRaw;
		public static ZkTexture_getMipmapRgba ZkTexture_getMipmapRgba;
		public static ZkTexture_enumerateRawMipmaps ZkTexture_enumerateRawMipmaps;
		public static ZkTexture_enumerateRgbaMipmaps ZkTexture_enumerateRgbaMipmaps;
		public static ZkMorphMesh_load ZkMorphMesh_load;
		public static ZkMorphMesh_loadPath ZkMorphMesh_loadPath;
		public static ZkMorphMesh_loadVfs ZkMorphMesh_loadVfs;
		public static ZkMorphMesh_del ZkMorphMesh_del;
		public static ZkMorphMesh_getName ZkMorphMesh_getName;
		public static ZkMorphMesh_getMesh ZkMorphMesh_getMesh;
		public static ZkMorphMesh_getMorphPositions ZkMorphMesh_getMorphPositions;
		public static ZkMorphMesh_getAnimationCount ZkMorphMesh_getAnimationCount;
		public static ZkMorphMesh_getAnimation ZkMorphMesh_getAnimation;
		public static ZkMorphMesh_enumerateAnimations ZkMorphMesh_enumerateAnimations;
		public static ZkMorphMesh_getSourceCount ZkMorphMesh_getSourceCount;
		public static ZkMorphMesh_getSource ZkMorphMesh_getSource;
		public static ZkMorphMesh_enumerateSources ZkMorphMesh_enumerateSources;
		public static ZkMorphAnimation_getName ZkMorphAnimation_getName;
		public static ZkMorphAnimation_getLayer ZkMorphAnimation_getLayer;
		public static ZkMorphAnimation_getBlendIn ZkMorphAnimation_getBlendIn;
		public static ZkMorphAnimation_getBlendOut ZkMorphAnimation_getBlendOut;
		public static ZkMorphAnimation_getDuration ZkMorphAnimation_getDuration;
		public static ZkMorphAnimation_getSpeed ZkMorphAnimation_getSpeed;
		public static ZkMorphAnimation_getFlags ZkMorphAnimation_getFlags;
		public static ZkMorphAnimation_getFrameCount ZkMorphAnimation_getFrameCount;
		public static ZkMorphAnimation_getVertices ZkMorphAnimation_getVertices;
		public static ZkMorphAnimation_getSamples ZkMorphAnimation_getSamples;
		public static ZkMorphSource_getFileName ZkMorphSource_getFileName;
		public static ZkMorphSource_getFileDate ZkMorphSource_getFileDate;
		public static ZkMesh_load ZkMesh_load;
		public static ZkMesh_loadPath ZkMesh_loadPath;
		public static ZkMesh_loadVfs ZkMesh_loadVfs;
		public static ZkMesh_del ZkMesh_del;
		public static ZkMesh_getSourceDate ZkMesh_getSourceDate;
		public static ZkMesh_getName ZkMesh_getName;
		public static ZkMesh_getBoundingBox ZkMesh_getBoundingBox;
		public static ZkMesh_getOrientedBoundingBox ZkMesh_getOrientedBoundingBox;
		public static ZkMesh_getMaterialCount ZkMesh_getMaterialCount;
		public static ZkMesh_getMaterial ZkMesh_getMaterial;
		public static ZkMesh_enumerateMaterials ZkMesh_enumerateMaterials;
		public static ZkMesh_getPositions ZkMesh_getPositions;
		public static ZkMesh_getVertices ZkMesh_getVertices;
		public static ZkMesh_getLightMapCount ZkMesh_getLightMapCount;
		public static ZkMesh_getLightMap ZkMesh_getLightMap;
		public static ZkMesh_enumerateLightMaps ZkMesh_enumerateLightMaps;
		public static ZkMesh_getPolygonCount ZkMesh_getPolygonCount;
		public static ZkMesh_getPolygon ZkMesh_getPolygon;
		public static ZkMesh_enumeratePolygons ZkMesh_enumeratePolygons;
		public static ZkLightMap_getImage ZkLightMap_getImage;
		public static ZkLightMap_getOrigin ZkLightMap_getOrigin;
		public static ZkLightMap_getNormal ZkLightMap_getNormal;
		public static ZkPolygon_getMaterialIndex ZkPolygon_getMaterialIndex;
		public static ZkPolygon_getLightMapIndex ZkPolygon_getLightMapIndex;
		public static ZkPolygon_getPositionIndices ZkPolygon_getPositionIndices;
		public static ZkPolygon_getPolygonIndices ZkPolygon_getPolygonIndices;
		public static ZkPolygon_getIsPortal ZkPolygon_getIsPortal;
		public static ZkPolygon_getIsOccluder ZkPolygon_getIsOccluder;
		public static ZkPolygon_getIsSector ZkPolygon_getIsSector;
		public static ZkPolygon_getShouldRelight ZkPolygon_getShouldRelight;
		public static ZkPolygon_getIsOutdoor ZkPolygon_getIsOutdoor;
		public static ZkPolygon_getIsGhostOccluder ZkPolygon_getIsGhostOccluder;
		public static ZkPolygon_getIsDynamicallyLit ZkPolygon_getIsDynamicallyLit;
		public static ZkPolygon_getIsLod ZkPolygon_getIsLod;
		public static ZkPolygon_getNormalAxis ZkPolygon_getNormalAxis;
		public static ZkPolygon_getSectorIndex ZkPolygon_getSectorIndex;
		public static ZkModelScript_load ZkModelScript_load;
		public static ZkModelScript_loadPath ZkModelScript_loadPath;
		public static ZkModelScript_loadVfs ZkModelScript_loadVfs;
		public static ZkModelScript_del ZkModelScript_del;
		public static ZkModelScript_getSkeletonName ZkModelScript_getSkeletonName;
		public static ZkModelScript_getSkeletonMeshDisabled ZkModelScript_getSkeletonMeshDisabled;
		public static ZkModelScript_getMeshCount ZkModelScript_getMeshCount;
		public static ZkModelScript_getDisabledAnimationsCount ZkModelScript_getDisabledAnimationsCount;
		public static ZkModelScript_getAnimationCombineCount ZkModelScript_getAnimationCombineCount;
		public static ZkModelScript_getAnimationBlendCount ZkModelScript_getAnimationBlendCount;
		public static ZkModelScript_getAnimationAliasCount ZkModelScript_getAnimationAliasCount;
		public static ZkModelScript_getModelTagCount ZkModelScript_getModelTagCount;
		public static ZkModelScript_getAnimationCount ZkModelScript_getAnimationCount;
		public static ZkModelScript_getDisabledAnimation ZkModelScript_getDisabledAnimation;
		public static ZkModelScript_getMesh ZkModelScript_getMesh;
		public static ZkModelScript_getAnimationCombine ZkModelScript_getAnimationCombine;
		public static ZkModelScript_getAnimationBlend ZkModelScript_getAnimationBlend;
		public static ZkModelScript_getAnimationAlias ZkModelScript_getAnimationAlias;
		public static ZkModelScript_getModelTag ZkModelScript_getModelTag;
		public static ZkModelScript_getAnimation ZkModelScript_getAnimation;
		public static ZkModelScript_enumerateAnimationCombines ZkModelScript_enumerateAnimationCombines;
		public static ZkModelScript_enumerateMeshes ZkModelScript_enumerateMeshes;
		public static ZkModelScript_enumerateDisabledAnimations ZkModelScript_enumerateDisabledAnimations;
		public static ZkModelScript_enumerateAnimationBlends ZkModelScript_enumerateAnimationBlends;
		public static ZkModelScript_enumerateAnimationAliases ZkModelScript_enumerateAnimationAliases;
		public static ZkModelScript_enumerateModelTags ZkModelScript_enumerateModelTags;
		public static ZkModelScript_enumerateAnimations ZkModelScript_enumerateAnimations;
		public static ZkAnimation_getName ZkAnimation_getName;
		public static ZkAnimation_getLayer ZkAnimation_getLayer;
		public static ZkAnimation_getNext ZkAnimation_getNext;
		public static ZkAnimation_getBlendIn ZkAnimation_getBlendIn;
		public static ZkAnimation_getBlendOut ZkAnimation_getBlendOut;
		public static ZkAnimation_getFlags ZkAnimation_getFlags;
		public static ZkAnimation_getModel ZkAnimation_getModel;
		public static ZkAnimation_getDirection ZkAnimation_getDirection;
		public static ZkAnimation_getFirstFrame ZkAnimation_getFirstFrame;
		public static ZkAnimation_getLastFrame ZkAnimation_getLastFrame;
		public static ZkAnimation_getCollisionVolumeScale ZkAnimation_getCollisionVolumeScale;
		public static ZkAnimation_getEventTagCount ZkAnimation_getEventTagCount;
		public static ZkAnimation_getParticleEffectCount ZkAnimation_getParticleEffectCount;
		public static ZkAnimation_getParticleEffectStopCount ZkAnimation_getParticleEffectStopCount;
		public static ZkAnimation_getSoundEffectCount ZkAnimation_getSoundEffectCount;
		public static ZkAnimation_getSoundEffectGroundCount ZkAnimation_getSoundEffectGroundCount;
		public static ZkAnimation_getMorphAnimationCount ZkAnimation_getMorphAnimationCount;
		public static ZkAnimation_getCameraTremorCount ZkAnimation_getCameraTremorCount;
		public static ZkAnimation_getEventTag ZkAnimation_getEventTag;
		public static ZkAnimation_getParticleEffect ZkAnimation_getParticleEffect;
		public static ZkAnimation_getParticleEffectStop ZkAnimation_getParticleEffectStop;
		public static ZkAnimation_getSoundEffect ZkAnimation_getSoundEffect;
		public static ZkAnimation_getSoundEffectGround ZkAnimation_getSoundEffectGround;
		public static ZkAnimation_getMorphAnimation ZkAnimation_getMorphAnimation;
		public static ZkAnimation_getCameraTremor ZkAnimation_getCameraTremor;
		public static ZkAnimation_enumerateEventTags ZkAnimation_enumerateEventTags;
		public static ZkAnimation_enumerateParticleEffects ZkAnimation_enumerateParticleEffects;
		public static ZkAnimation_enumerateParticleEffectStops ZkAnimation_enumerateParticleEffectStops;
		public static ZkAnimation_enumerateSoundEffects ZkAnimation_enumerateSoundEffects;
		public static ZkAnimation_enumerateSoundEffectGrounds ZkAnimation_enumerateSoundEffectGrounds;
		public static ZkAnimation_enumerateMorphAnimations ZkAnimation_enumerateMorphAnimations;
		public static ZkAnimation_enumerateCameraTremors ZkAnimation_enumerateCameraTremors;
		public static ZkEventTag_getFrame ZkEventTag_getFrame;
		public static ZkEventTag_getType ZkEventTag_getType;
		public static ZkEventTag_getSlot ZkEventTag_getSlot;
		public static ZkEventTag_getItem ZkEventTag_getItem;
		public static ZkEventTag_getFrames ZkEventTag_getFrames;
		public static ZkEventTag_getFightMode ZkEventTag_getFightMode;
		public static ZkEventTag_getIsAttached ZkEventTag_getIsAttached;
		public static ZkEventParticleEffect_getFrame ZkEventParticleEffect_getFrame;
		public static ZkEventParticleEffect_getIndex ZkEventParticleEffect_getIndex;
		public static ZkEventParticleEffect_getName ZkEventParticleEffect_getName;
		public static ZkEventParticleEffect_getPosition ZkEventParticleEffect_getPosition;
		public static ZkEventParticleEffect_getIsAttached ZkEventParticleEffect_getIsAttached;
		public static ZkEventParticleEffectStop_getFrame ZkEventParticleEffectStop_getFrame;
		public static ZkEventParticleEffectStop_getIndex ZkEventParticleEffectStop_getIndex;
		public static ZkEventCameraTremor_getFrame ZkEventCameraTremor_getFrame;
		public static ZkEventCameraTremor_getField1 ZkEventCameraTremor_getField1;
		public static ZkEventCameraTremor_getField2 ZkEventCameraTremor_getField2;
		public static ZkEventCameraTremor_getField3 ZkEventCameraTremor_getField3;
		public static ZkEventCameraTremor_getField4 ZkEventCameraTremor_getField4;
		public static ZkEventSoundEffect_getFrame ZkEventSoundEffect_getFrame;
		public static ZkEventSoundEffect_getName ZkEventSoundEffect_getName;
		public static ZkEventSoundEffect_getRange ZkEventSoundEffect_getRange;
		public static ZkEventSoundEffect_getEmptySlot ZkEventSoundEffect_getEmptySlot;
		public static ZkEventSoundEffectGround_getFrame ZkEventSoundEffectGround_getFrame;
		public static ZkEventSoundEffectGround_getName ZkEventSoundEffectGround_getName;
		public static ZkEventSoundEffectGround_getRange ZkEventSoundEffectGround_getRange;
		public static ZkEventSoundEffectGround_getEmptySlot ZkEventSoundEffectGround_getEmptySlot;
		public static ZkMorphAnimation_getFrame ZkMorphAnimation_getFrame;
		public static ZkMorphAnimation_getAnimation ZkMorphAnimation_getAnimation;
		public static ZkMorphAnimation_getNode ZkMorphAnimation_getNode;
		public static ZkAnimationCombine_getName ZkAnimationCombine_getName;
		public static ZkAnimationCombine_getLayer ZkAnimationCombine_getLayer;
		public static ZkAnimationCombine_getNext ZkAnimationCombine_getNext;
		public static ZkAnimationCombine_getBlendIn ZkAnimationCombine_getBlendIn;
		public static ZkAnimationCombine_getBlendOut ZkAnimationCombine_getBlendOut;
		public static ZkAnimationCombine_getFlags ZkAnimationCombine_getFlags;
		public static ZkAnimationCombine_getModel ZkAnimationCombine_getModel;
		public static ZkAnimationCombine_getLastFrame ZkAnimationCombine_getLastFrame;
		public static ZkAnimationBlend_getName ZkAnimationBlend_getName;
		public static ZkAnimationBlend_getNext ZkAnimationBlend_getNext;
		public static ZkAnimationBlend_getBlendIn ZkAnimationBlend_getBlendIn;
		public static ZkAnimationBlend_getBlendOut ZkAnimationBlend_getBlendOut;
		public static ZkAnimationAlias_getName ZkAnimationAlias_getName;
		public static ZkAnimationAlias_getLayer ZkAnimationAlias_getLayer;
		public static ZkAnimationAlias_getNext ZkAnimationAlias_getNext;
		public static ZkAnimationAlias_getBlendIn ZkAnimationAlias_getBlendIn;
		public static ZkAnimationAlias_getBlendOut ZkAnimationAlias_getBlendOut;
		public static ZkAnimationAlias_getFlags ZkAnimationAlias_getFlags;
		public static ZkAnimationAlias_getAlias ZkAnimationAlias_getAlias;
		public static ZkAnimationAlias_getDirection ZkAnimationAlias_getDirection;
		public static ZkBspTree_getType ZkBspTree_getType;
		public static ZkBspTree_getPolygonIndices ZkBspTree_getPolygonIndices;
		public static ZkBspTree_getLeafPolygonIndices ZkBspTree_getLeafPolygonIndices;
		public static ZkBspTree_getPortalPolygonIndices ZkBspTree_getPortalPolygonIndices;
		public static ZkBspTree_getLightPoints ZkBspTree_getLightPoints;
		public static ZkBspTree_getLeafNodeIndices ZkBspTree_getLeafNodeIndices;
		public static ZkBspTree_getNodes ZkBspTree_getNodes;
		public static ZkBspTree_getSectorCount ZkBspTree_getSectorCount;
		public static ZkBspTree_getSector ZkBspTree_getSector;
		public static ZkBspTree_enumerateSectors ZkBspTree_enumerateSectors;
		public static ZkBspSector_getName ZkBspSector_getName;
		public static ZkBspSector_getNodeIndices ZkBspSector_getNodeIndices;
		public static ZkBspSector_getPortalPolygonIndices ZkBspSector_getPortalPolygonIndices;
		public static ZkWayNet_getEdges ZkWayNet_getEdges;
		public static ZkWayNet_getPointCount ZkWayNet_getPointCount;
		public static ZkWayNet_getPoint ZkWayNet_getPoint;
		public static ZkWayNet_enumeratePoints ZkWayNet_enumeratePoints;
		public static ZkWayPoint_getName ZkWayPoint_getName;
		public static ZkWayPoint_getWaterDepth ZkWayPoint_getWaterDepth;
		public static ZkWayPoint_getUnderWater ZkWayPoint_getUnderWater;
		public static ZkWayPoint_getPosition ZkWayPoint_getPosition;
		public static ZkWayPoint_getDirection ZkWayPoint_getDirection;
		public static ZkWayPoint_getFreePoint ZkWayPoint_getFreePoint;
		public static ZkWorld_load ZkWorld_load;
		public static ZkWorld_loadPath ZkWorld_loadPath;
		public static ZkWorld_loadVfs ZkWorld_loadVfs;
		public static ZkWorld_del ZkWorld_del;
		public static ZkWorld_getMesh ZkWorld_getMesh;
		public static ZkWorld_getBspTree ZkWorld_getBspTree;
		public static ZkWorld_getWayNet ZkWorld_getWayNet;
		public static ZkWorld_getRootObjectCount ZkWorld_getRootObjectCount;
		public static ZkWorld_getRootObject ZkWorld_getRootObject;
		public static ZkWorld_enumerateRootObjects ZkWorld_enumerateRootObjects;
		public static ZkVirtualObject_load ZkVirtualObject_load;
		public static ZkVirtualObject_loadPath ZkVirtualObject_loadPath;
		public static ZkVirtualObject_del ZkVirtualObject_del;
		public static ZkVirtualObject_getType ZkVirtualObject_getType;
		public static ZkVirtualObject_getId ZkVirtualObject_getId;
		public static ZkVirtualObject_getBbox ZkVirtualObject_getBbox;
		public static ZkVirtualObject_getPosition ZkVirtualObject_getPosition;
		public static ZkVirtualObject_getRotation ZkVirtualObject_getRotation;
		public static ZkVirtualObject_getShowVisual ZkVirtualObject_getShowVisual;
		public static ZkVirtualObject_getSpriteCameraFacingMode ZkVirtualObject_getSpriteCameraFacingMode;
		public static ZkVirtualObject_getCdStatic ZkVirtualObject_getCdStatic;
		public static ZkVirtualObject_getCdDynamic ZkVirtualObject_getCdDynamic;
		public static ZkVirtualObject_getVobStatic ZkVirtualObject_getVobStatic;
		public static ZkVirtualObject_getDynamicShadows ZkVirtualObject_getDynamicShadows;
		public static ZkVirtualObject_getPhysicsEnabled ZkVirtualObject_getPhysicsEnabled;
		public static ZkVirtualObject_getAnimMode ZkVirtualObject_getAnimMode;
		public static ZkVirtualObject_getBias ZkVirtualObject_getBias;
		public static ZkVirtualObject_getAmbient ZkVirtualObject_getAmbient;
		public static ZkVirtualObject_getAnimStrength ZkVirtualObject_getAnimStrength;
		public static ZkVirtualObject_getFarClipScale ZkVirtualObject_getFarClipScale;
		public static ZkVirtualObject_getPresetName ZkVirtualObject_getPresetName;
		public static ZkVirtualObject_getName ZkVirtualObject_getName;
		public static ZkVirtualObject_getVisualName ZkVirtualObject_getVisualName;
		public static ZkVirtualObject_getVisualType ZkVirtualObject_getVisualType;
		public static ZkVirtualObject_getVisualDecal ZkVirtualObject_getVisualDecal;
		public static ZkVirtualObject_getChildCount ZkVirtualObject_getChildCount;
		public static ZkVirtualObject_getChild ZkVirtualObject_getChild;
		public static ZkVirtualObject_enumerateChildren ZkVirtualObject_enumerateChildren;
		public static ZkDecal_getName ZkDecal_getName;
		public static ZkDecal_getDimension ZkDecal_getDimension;
		public static ZkDecal_getOffset ZkDecal_getOffset;
		public static ZkDecal_getTwoSided ZkDecal_getTwoSided;
		public static ZkDecal_getAlphaFunc ZkDecal_getAlphaFunc;
		public static ZkDecal_getTextureAnimFps ZkDecal_getTextureAnimFps;
		public static ZkDecal_getAlphaWeight ZkDecal_getAlphaWeight;
		public static ZkDecal_getIgnoreDaylight ZkDecal_getIgnoreDaylight;
		public static ZkCutsceneCamera_load ZkCutsceneCamera_load;
		public static ZkCutsceneCamera_loadPath ZkCutsceneCamera_loadPath;
		public static ZkCutsceneCamera_del ZkCutsceneCamera_del;
		public static ZkCutsceneCamera_getTrajectoryFOR ZkCutsceneCamera_getTrajectoryFOR;
		public static ZkCutsceneCamera_getTargetTrajectoryFOR ZkCutsceneCamera_getTargetTrajectoryFOR;
		public static ZkCutsceneCamera_getLoopMode ZkCutsceneCamera_getLoopMode;
		public static ZkCutsceneCamera_getLerpMode ZkCutsceneCamera_getLerpMode;
		public static ZkCutsceneCamera_getIgnoreFORVobRotation ZkCutsceneCamera_getIgnoreFORVobRotation;
		public static ZkCutsceneCamera_getIgnoreFORVobRotationTarget ZkCutsceneCamera_getIgnoreFORVobRotationTarget;
		public static ZkCutsceneCamera_getAdapt ZkCutsceneCamera_getAdapt;
		public static ZkCutsceneCamera_getEaseFirst ZkCutsceneCamera_getEaseFirst;
		public static ZkCutsceneCamera_getEaseLast ZkCutsceneCamera_getEaseLast;
		public static ZkCutsceneCamera_getTotalDuration ZkCutsceneCamera_getTotalDuration;
		public static ZkCutsceneCamera_getAutoFocusVob ZkCutsceneCamera_getAutoFocusVob;
		public static ZkCutsceneCamera_getAutoPlayerMovable ZkCutsceneCamera_getAutoPlayerMovable;
		public static ZkCutsceneCamera_getAutoUntriggerLast ZkCutsceneCamera_getAutoUntriggerLast;
		public static ZkCutsceneCamera_getAutoUntriggerLastDelay ZkCutsceneCamera_getAutoUntriggerLastDelay;
		public static ZkCutsceneCamera_getPositionCount ZkCutsceneCamera_getPositionCount;
		public static ZkCutsceneCamera_getTargetCount ZkCutsceneCamera_getTargetCount;
		public static ZkCutsceneCamera_getFrameCount ZkCutsceneCamera_getFrameCount;
		public static ZkCutsceneCamera_getFrame ZkCutsceneCamera_getFrame;
		public static ZkCutsceneCamera_enumerateFrames ZkCutsceneCamera_enumerateFrames;
		public static ZkCameraTrajectoryFrame_getTime ZkCameraTrajectoryFrame_getTime;
		public static ZkCameraTrajectoryFrame_getRollAngle ZkCameraTrajectoryFrame_getRollAngle;
		public static ZkCameraTrajectoryFrame_getFovScale ZkCameraTrajectoryFrame_getFovScale;
		public static ZkCameraTrajectoryFrame_getMotionType ZkCameraTrajectoryFrame_getMotionType;
		public static ZkCameraTrajectoryFrame_getMotionTypeFov ZkCameraTrajectoryFrame_getMotionTypeFov;
		public static ZkCameraTrajectoryFrame_getMotionTypeRoll ZkCameraTrajectoryFrame_getMotionTypeRoll;
		public static ZkCameraTrajectoryFrame_getMotionTypeTimeScale ZkCameraTrajectoryFrame_getMotionTypeTimeScale;
		public static ZkCameraTrajectoryFrame_getTension ZkCameraTrajectoryFrame_getTension;
		public static ZkCameraTrajectoryFrame_getCamBias ZkCameraTrajectoryFrame_getCamBias;
		public static ZkCameraTrajectoryFrame_getContinuity ZkCameraTrajectoryFrame_getContinuity;
		public static ZkCameraTrajectoryFrame_getTimeScale ZkCameraTrajectoryFrame_getTimeScale;
		public static ZkCameraTrajectoryFrame_getTimeFixed ZkCameraTrajectoryFrame_getTimeFixed;
		public static ZkCameraTrajectoryFrame_getOriginalPose ZkCameraTrajectoryFrame_getOriginalPose;
		public static ZkLightPreset_load ZkLightPreset_load;
		public static ZkLightPreset_loadPath ZkLightPreset_loadPath;
		public static ZkLightPreset_del ZkLightPreset_del;
		public static ZkLight_load ZkLight_load;
		public static ZkLight_loadPath ZkLight_loadPath;
		public static ZkLight_del ZkLight_del;
		public static ZkLightPreset_getPreset ZkLightPreset_getPreset;
		public static ZkLightPreset_getLightType ZkLightPreset_getLightType;
		public static ZkLightPreset_getRange ZkLightPreset_getRange;
		public static ZkLightPreset_getColor ZkLightPreset_getColor;
		public static ZkLightPreset_getConeAngle ZkLightPreset_getConeAngle;
		public static ZkLightPreset_getIsStatic ZkLightPreset_getIsStatic;
		public static ZkLightPreset_getQuality ZkLightPreset_getQuality;
		public static ZkLightPreset_getLensflareFx ZkLightPreset_getLensflareFx;
		public static ZkLightPreset_getOn ZkLightPreset_getOn;
		public static ZkLightPreset_getRangeAnimationScale ZkLightPreset_getRangeAnimationScale;
		public static ZkLightPreset_getRangeAnimationFps ZkLightPreset_getRangeAnimationFps;
		public static ZkLightPreset_getRangeAnimationSmooth ZkLightPreset_getRangeAnimationSmooth;
		public static ZkLightPreset_getColorAnimationList ZkLightPreset_getColorAnimationList;
		public static ZkLightPreset_getColorAnimationFps ZkLightPreset_getColorAnimationFps;
		public static ZkLightPreset_getColorAnimationSmooth ZkLightPreset_getColorAnimationSmooth;
		public static ZkLightPreset_getCanMove ZkLightPreset_getCanMove;
		public static ZkLight_getPreset ZkLight_getPreset;
		public static ZkLight_getLightType ZkLight_getLightType;
		public static ZkLight_getRange ZkLight_getRange;
		public static ZkLight_getColor ZkLight_getColor;
		public static ZkLight_getConeAngle ZkLight_getConeAngle;
		public static ZkLight_getIsStatic ZkLight_getIsStatic;
		public static ZkLight_getQuality ZkLight_getQuality;
		public static ZkLight_getLensflareFx ZkLight_getLensflareFx;
		public static ZkLight_getOn ZkLight_getOn;
		public static ZkLight_getRangeAnimationScale ZkLight_getRangeAnimationScale;
		public static ZkLight_getRangeAnimationFps ZkLight_getRangeAnimationFps;
		public static ZkLight_getRangeAnimationSmooth ZkLight_getRangeAnimationSmooth;
		public static ZkLight_getColorAnimationList ZkLight_getColorAnimationList;
		public static ZkLight_getColorAnimationFps ZkLight_getColorAnimationFps;
		public static ZkLight_getColorAnimationSmooth ZkLight_getColorAnimationSmooth;
		public static ZkLight_getCanMove ZkLight_getCanMove;
		public static ZkAnimate_load ZkAnimate_load;
		public static ZkAnimate_loadPath ZkAnimate_loadPath;
		public static ZkAnimate_del ZkAnimate_del;
		public static ZkAnimate_getStartOn ZkAnimate_getStartOn;
		public static ZkItem_load ZkItem_load;
		public static ZkItem_loadPath ZkItem_loadPath;
		public static ZkItem_del ZkItem_del;
		public static ZkItem_getInstance ZkItem_getInstance;
		public static ZkLensFlare_load ZkLensFlare_load;
		public static ZkLensFlare_loadPath ZkLensFlare_loadPath;
		public static ZkLensFlare_del ZkLensFlare_del;
		public static ZkLensFlare_getEffect ZkLensFlare_getEffect;
		public static ZkParticleEffectController_load ZkParticleEffectController_load;
		public static ZkParticleEffectController_loadPath ZkParticleEffectController_loadPath;
		public static ZkParticleEffectController_del ZkParticleEffectController_del;
		public static ZkParticleEffectController_getEffectName ZkParticleEffectController_getEffectName;
		public static ZkParticleEffectController_getKillWhenDone ZkParticleEffectController_getKillWhenDone;
		public static ZkParticleEffectController_getInitiallyRunning ZkParticleEffectController_getInitiallyRunning;
		public static ZkMessageFilter_load ZkMessageFilter_load;
		public static ZkMessageFilter_loadPath ZkMessageFilter_loadPath;
		public static ZkMessageFilter_del ZkMessageFilter_del;
		public static ZkMessageFilter_getTarget ZkMessageFilter_getTarget;
		public static ZkMessageFilter_getOnTrigger ZkMessageFilter_getOnTrigger;
		public static ZkMessageFilter_getOnUntrigger ZkMessageFilter_getOnUntrigger;
		public static ZkCodeMaster_load ZkCodeMaster_load;
		public static ZkCodeMaster_loadPath ZkCodeMaster_loadPath;
		public static ZkCodeMaster_del ZkCodeMaster_del;
		public static ZkCodeMaster_getTarget ZkCodeMaster_getTarget;
		public static ZkCodeMaster_getOrdered ZkCodeMaster_getOrdered;
		public static ZkCodeMaster_getFirstFalseIsFailure ZkCodeMaster_getFirstFalseIsFailure;
		public static ZkCodeMaster_getFailureTarget ZkCodeMaster_getFailureTarget;
		public static ZkCodeMaster_getUntriggeredCancels ZkCodeMaster_getUntriggeredCancels;
		public static ZkCodeMaster_getSlaveCount ZkCodeMaster_getSlaveCount;
		public static ZkCodeMaster_getSlave ZkCodeMaster_getSlave;
		public static ZkCodeMaster_enumerateSlaves ZkCodeMaster_enumerateSlaves;
		public static ZkMoverController_load ZkMoverController_load;
		public static ZkMoverController_loadPath ZkMoverController_loadPath;
		public static ZkMoverController_del ZkMoverController_del;
		public static ZkMoverController_getTarget ZkMoverController_getTarget;
		public static ZkMoverController_getMessage ZkMoverController_getMessage;
		public static ZkMoverController_getKey ZkMoverController_getKey;
		public static ZkTouchDamage_load ZkTouchDamage_load;
		public static ZkTouchDamage_loadPath ZkTouchDamage_loadPath;
		public static ZkTouchDamage_del ZkTouchDamage_del;
		public static ZkTouchDamage_getDamage ZkTouchDamage_getDamage;
		public static ZkTouchDamage_getIsBarrier ZkTouchDamage_getIsBarrier;
		public static ZkTouchDamage_getIsBlunt ZkTouchDamage_getIsBlunt;
		public static ZkTouchDamage_getIsEdge ZkTouchDamage_getIsEdge;
		public static ZkTouchDamage_getIsFire ZkTouchDamage_getIsFire;
		public static ZkTouchDamage_getIsFly ZkTouchDamage_getIsFly;
		public static ZkTouchDamage_getIsMagic ZkTouchDamage_getIsMagic;
		public static ZkTouchDamage_getIsPoint ZkTouchDamage_getIsPoint;
		public static ZkTouchDamage_getIsFall ZkTouchDamage_getIsFall;
		public static ZkTouchDamage_getRepeatDelaySeconds ZkTouchDamage_getRepeatDelaySeconds;
		public static ZkTouchDamage_getVolumeScale ZkTouchDamage_getVolumeScale;
		public static ZkTouchDamage_getCollisionType ZkTouchDamage_getCollisionType;
		public static ZkEarthquake_load ZkEarthquake_load;
		public static ZkEarthquake_loadPath ZkEarthquake_loadPath;
		public static ZkEarthquake_del ZkEarthquake_del;
		public static ZkEarthquake_getRadius ZkEarthquake_getRadius;
		public static ZkEarthquake_getDuration ZkEarthquake_getDuration;
		public static ZkEarthquake_getAmplitude ZkEarthquake_getAmplitude;
		public static ZkMovableObject_load ZkMovableObject_load;
		public static ZkMovableObject_loadPath ZkMovableObject_loadPath;
		public static ZkMovableObject_del ZkMovableObject_del;
		public static ZkMovableObject_getName ZkMovableObject_getName;
		public static ZkMovableObject_getHp ZkMovableObject_getHp;
		public static ZkMovableObject_getDamage ZkMovableObject_getDamage;
		public static ZkMovableObject_getMovable ZkMovableObject_getMovable;
		public static ZkMovableObject_getTakable ZkMovableObject_getTakable;
		public static ZkMovableObject_getFocusOverride ZkMovableObject_getFocusOverride;
		public static ZkMovableObject_getMaterial ZkMovableObject_getMaterial;
		public static ZkMovableObject_getVisualDestroyed ZkMovableObject_getVisualDestroyed;
		public static ZkMovableObject_getOwner ZkMovableObject_getOwner;
		public static ZkMovableObject_getOwnerGuild ZkMovableObject_getOwnerGuild;
		public static ZkMovableObject_getDestroyed ZkMovableObject_getDestroyed;
		public static ZkInteractiveObject_load ZkInteractiveObject_load;
		public static ZkInteractiveObject_loadPath ZkInteractiveObject_loadPath;
		public static ZkInteractiveObject_del ZkInteractiveObject_del;
		public static ZkInteractiveObject_getState ZkInteractiveObject_getState;
		public static ZkInteractiveObject_getTarget ZkInteractiveObject_getTarget;
		public static ZkInteractiveObject_getItem ZkInteractiveObject_getItem;
		public static ZkInteractiveObject_getConditionFunction ZkInteractiveObject_getConditionFunction;
		public static ZkInteractiveObject_getOnStateChangeFunction ZkInteractiveObject_getOnStateChangeFunction;
		public static ZkInteractiveObject_getRewind ZkInteractiveObject_getRewind;
		public static ZkFire_load ZkFire_load;
		public static ZkFire_loadPath ZkFire_loadPath;
		public static ZkFire_del ZkFire_del;
		public static ZkFire_getSlot ZkFire_getSlot;
		public static ZkFire_getVobTree ZkFire_getVobTree;
		public static ZkContainer_load ZkContainer_load;
		public static ZkContainer_loadPath ZkContainer_loadPath;
		public static ZkContainer_del ZkContainer_del;
		public static ZkContainer_getIsLocked ZkContainer_getIsLocked;
		public static ZkContainer_getKey ZkContainer_getKey;
		public static ZkContainer_getPickString ZkContainer_getPickString;
		public static ZkContainer_getContents ZkContainer_getContents;
		public static ZkDoor_load ZkDoor_load;
		public static ZkDoor_loadPath ZkDoor_loadPath;
		public static ZkDoor_del ZkDoor_del;
		public static ZkDoor_getIsLocked ZkDoor_getIsLocked;
		public static ZkDoor_getKey ZkDoor_getKey;
		public static ZkDoor_getPickString ZkDoor_getPickString;
		public static ZkSound_load ZkSound_load;
		public static ZkSound_loadPath ZkSound_loadPath;
		public static ZkSound_del ZkSound_del;
		public static ZkSound_getVolume ZkSound_getVolume;
		public static ZkSound_getMode ZkSound_getMode;
		public static ZkSound_getRandomDelay ZkSound_getRandomDelay;
		public static ZkSound_getRandomDelayVar ZkSound_getRandomDelayVar;
		public static ZkSound_getInitiallyPlaying ZkSound_getInitiallyPlaying;
		public static ZkSound_getAmbient3d ZkSound_getAmbient3d;
		public static ZkSound_getObstruction ZkSound_getObstruction;
		public static ZkSound_getConeAngle ZkSound_getConeAngle;
		public static ZkSound_getVolumeType ZkSound_getVolumeType;
		public static ZkSound_getRadius ZkSound_getRadius;
		public static ZkSound_getSoundName ZkSound_getSoundName;
		public static ZkSoundDaytime_load ZkSoundDaytime_load;
		public static ZkSoundDaytime_loadPath ZkSoundDaytime_loadPath;
		public static ZkSoundDaytime_del ZkSoundDaytime_del;
		public static ZkSoundDaytime_getStartTime ZkSoundDaytime_getStartTime;
		public static ZkSoundDaytime_getEndTime ZkSoundDaytime_getEndTime;
		public static ZkSoundDaytime_getSoundNameDaytime ZkSoundDaytime_getSoundNameDaytime;
		public static ZkTrigger_load ZkTrigger_load;
		public static ZkTrigger_loadPath ZkTrigger_loadPath;
		public static ZkTrigger_del ZkTrigger_del;
		public static ZkTrigger_getTarget ZkTrigger_getTarget;
		public static ZkTrigger_getFlags ZkTrigger_getFlags;
		public static ZkTrigger_getFilterFlags ZkTrigger_getFilterFlags;
		public static ZkTrigger_getVobTarget ZkTrigger_getVobTarget;
		public static ZkTrigger_getMaxActivationCount ZkTrigger_getMaxActivationCount;
		public static ZkTrigger_getRetriggerDelaySeconds ZkTrigger_getRetriggerDelaySeconds;
		public static ZkTrigger_getDamageThreshold ZkTrigger_getDamageThreshold;
		public static ZkTrigger_getFireDelaySeconds ZkTrigger_getFireDelaySeconds;
		public static ZkMover_load ZkMover_load;
		public static ZkMover_loadPath ZkMover_loadPath;
		public static ZkMover_del ZkMover_del;
		public static ZkMover_getBehavior ZkMover_getBehavior;
		public static ZkMover_getTouchBlockerDamage ZkMover_getTouchBlockerDamage;
		public static ZkMover_getStayOpenTimeSeconds ZkMover_getStayOpenTimeSeconds;
		public static ZkMover_getIsLocked ZkMover_getIsLocked;
		public static ZkMover_getAutoLink ZkMover_getAutoLink;
		public static ZkMover_getAutoRotate ZkMover_getAutoRotate;
		public static ZkMover_getSpeed ZkMover_getSpeed;
		public static ZkMover_getLerpType ZkMover_getLerpType;
		public static ZkMover_getSpeedType ZkMover_getSpeedType;
		public static ZkMover_getKeyframes ZkMover_getKeyframes;
		public static ZkMover_getSfxOpenStart ZkMover_getSfxOpenStart;
		public static ZkMover_getSfxOpenEnd ZkMover_getSfxOpenEnd;
		public static ZkMover_getSfxTransitioning ZkMover_getSfxTransitioning;
		public static ZkMover_getSfxCloseStart ZkMover_getSfxCloseStart;
		public static ZkMover_getSfxCloseEnd ZkMover_getSfxCloseEnd;
		public static ZkMover_getSfxLock ZkMover_getSfxLock;
		public static ZkMover_getSfxUnlock ZkMover_getSfxUnlock;
		public static ZkMover_getSfxUseLocked ZkMover_getSfxUseLocked;
		public static ZkTriggerList_load ZkTriggerList_load;
		public static ZkTriggerList_loadPath ZkTriggerList_loadPath;
		public static ZkTriggerList_del ZkTriggerList_del;
		public static ZkTriggerList_getMode ZkTriggerList_getMode;
		public static ZkTriggerList_getTargetCount ZkTriggerList_getTargetCount;
		public static ZkTriggerList_getTarget ZkTriggerList_getTarget;
		public static ZkTriggerList_enumerateTargets ZkTriggerList_enumerateTargets;
		public static ZkTriggerListTarget_getName ZkTriggerListTarget_getName;
		public static ZkTriggerListTarget_getDelaySeconds ZkTriggerListTarget_getDelaySeconds;
		public static ZkTriggerScript_load ZkTriggerScript_load;
		public static ZkTriggerScript_loadPath ZkTriggerScript_loadPath;
		public static ZkTriggerScript_del ZkTriggerScript_del;
		public static ZkTriggerScript_getFunction ZkTriggerScript_getFunction;
		public static ZkTriggerChangeLevel_load ZkTriggerChangeLevel_load;
		public static ZkTriggerChangeLevel_loadPath ZkTriggerChangeLevel_loadPath;
		public static ZkTriggerChangeLevel_del ZkTriggerChangeLevel_del;
		public static ZkTriggerChangeLevel_getLevelName ZkTriggerChangeLevel_getLevelName;
		public static ZkTriggerChangeLevel_getStartVob ZkTriggerChangeLevel_getStartVob;
		public static ZkTriggerWorldStart_load ZkTriggerWorldStart_load;
		public static ZkTriggerWorldStart_loadPath ZkTriggerWorldStart_loadPath;
		public static ZkTriggerWorldStart_del ZkTriggerWorldStart_del;
		public static ZkTriggerWorldStart_getTarget ZkTriggerWorldStart_getTarget;
		public static ZkTriggerWorldStart_getFireOnce ZkTriggerWorldStart_getFireOnce;
		public static ZkTriggerUntouch_load ZkTriggerUntouch_load;
		public static ZkTriggerUntouch_loadPath ZkTriggerUntouch_loadPath;
		public static ZkTriggerUntouch_del ZkTriggerUntouch_del;
		public static ZkTriggerUntouch_getTarget ZkTriggerUntouch_getTarget;
		public static ZkZoneMusic_load ZkZoneMusic_load;
		public static ZkZoneMusic_loadPath ZkZoneMusic_loadPath;
		public static ZkZoneMusic_del ZkZoneMusic_del;
		public static ZkZoneMusic_getIsEnabled ZkZoneMusic_getIsEnabled;
		public static ZkZoneMusic_getPriority ZkZoneMusic_getPriority;
		public static ZkZoneMusic_getIsEllipsoid ZkZoneMusic_getIsEllipsoid;
		public static ZkZoneMusic_getReverb ZkZoneMusic_getReverb;
		public static ZkZoneMusic_getVolume ZkZoneMusic_getVolume;
		public static ZkZoneMusic_getIsLoop ZkZoneMusic_getIsLoop;
		public static ZkZoneFarPlane_load ZkZoneFarPlane_load;
		public static ZkZoneFarPlane_loadPath ZkZoneFarPlane_loadPath;
		public static ZkZoneFarPlane_del ZkZoneFarPlane_del;
		public static ZkZoneFarPlane_getVobFarPlaneZ ZkZoneFarPlane_getVobFarPlaneZ;
		public static ZkZoneFarPlane_getInnerRangePercentage ZkZoneFarPlane_getInnerRangePercentage;
		public static ZkZoneFog_load ZkZoneFog_load;
		public static ZkZoneFog_loadPath ZkZoneFog_loadPath;
		public static ZkZoneFog_del ZkZoneFog_del;
		public static ZkZoneFog_getRangeCenter ZkZoneFog_getRangeCenter;
		public static ZkZoneFog_getInnerRangePercentage ZkZoneFog_getInnerRangePercentage;
		public static ZkZoneFog_getColor ZkZoneFog_getColor;
		public static ZkZoneFog_getFadeOutSky ZkZoneFog_getFadeOutSky;
		public static ZkZoneFog_getOverrideColor ZkZoneFog_getOverrideColor;
		public static ZkDaedalusScript_load ZkDaedalusScript_load;
		public static ZkDaedalusScript_loadPath ZkDaedalusScript_loadPath;
		public static ZkDaedalusScript_loadVfs ZkDaedalusScript_loadVfs;
		public static ZkDaedalusScript_del ZkDaedalusScript_del;
		public static ZkDaedalusScript_getSymbolCount ZkDaedalusScript_getSymbolCount;
		public static ZkDaedalusScript_enumerateSymbols ZkDaedalusScript_enumerateSymbols;
		public static ZkDaedalusScript_enumerateInstanceSymbols ZkDaedalusScript_enumerateInstanceSymbols;
		public static ZkDaedalusScript_getInstruction ZkDaedalusScript_getInstruction;
		public static ZkDaedalusScript_getSymbolByIndex ZkDaedalusScript_getSymbolByIndex;
		public static ZkDaedalusScript_getSymbolByAddress ZkDaedalusScript_getSymbolByAddress;
		public static ZkDaedalusScript_getSymbolByName ZkDaedalusScript_getSymbolByName;
		public static ZkDaedalusSymbol_getString ZkDaedalusSymbol_getString;
		public static ZkDaedalusSymbol_getFloat ZkDaedalusSymbol_getFloat;
		public static ZkDaedalusSymbol_getInt ZkDaedalusSymbol_getInt;
		public static ZkDaedalusSymbol_setString ZkDaedalusSymbol_setString;
		public static ZkDaedalusSymbol_setFloat ZkDaedalusSymbol_setFloat;
		public static ZkDaedalusSymbol_setInt ZkDaedalusSymbol_setInt;
		public static ZkDaedalusSymbol_getIsConst ZkDaedalusSymbol_getIsConst;
		public static ZkDaedalusSymbol_getIsMember ZkDaedalusSymbol_getIsMember;
		public static ZkDaedalusSymbol_getIsExternal ZkDaedalusSymbol_getIsExternal;
		public static ZkDaedalusSymbol_getIsMerged ZkDaedalusSymbol_getIsMerged;
		public static ZkDaedalusSymbol_getIsGenerated ZkDaedalusSymbol_getIsGenerated;
		public static ZkDaedalusSymbol_getHasReturn ZkDaedalusSymbol_getHasReturn;
		public static ZkDaedalusSymbol_getName ZkDaedalusSymbol_getName;
		public static ZkDaedalusSymbol_getAddress ZkDaedalusSymbol_getAddress;
		public static ZkDaedalusSymbol_getParent ZkDaedalusSymbol_getParent;
		public static ZkDaedalusSymbol_getSize ZkDaedalusSymbol_getSize;
		public static ZkDaedalusSymbol_getType ZkDaedalusSymbol_getType;
		public static ZkDaedalusSymbol_getIndex ZkDaedalusSymbol_getIndex;
		public static ZkDaedalusSymbol_getReturnType ZkDaedalusSymbol_getReturnType;
		public static ZkDaedalusInstance_getType ZkDaedalusInstance_getType;
		public static ZkDaedalusInstance_getIndex ZkDaedalusInstance_getIndex;
		public static ZkDaedalusVm_load ZkDaedalusVm_load;
		public static ZkDaedalusVm_loadPath ZkDaedalusVm_loadPath;
		public static ZkDaedalusVm_loadVfs ZkDaedalusVm_loadVfs;
		public static ZkDaedalusVm_del ZkDaedalusVm_del;
		public static ZkDaedalusVm_pushInt ZkDaedalusVm_pushInt;
		public static ZkDaedalusVm_pushFloat ZkDaedalusVm_pushFloat;
		public static ZkDaedalusVm_pushString ZkDaedalusVm_pushString;
		public static ZkDaedalusVm_pushInstance ZkDaedalusVm_pushInstance;
		public static ZkDaedalusVm_popInt ZkDaedalusVm_popInt;
		public static ZkDaedalusVm_popFloat ZkDaedalusVm_popFloat;
		public static ZkDaedalusVm_popString ZkDaedalusVm_popString;
		public static ZkDaedalusVm_popInstance ZkDaedalusVm_popInstance;
		public static ZkDaedalusVm_getGlobalSelf ZkDaedalusVm_getGlobalSelf;
		public static ZkDaedalusVm_getGlobalOther ZkDaedalusVm_getGlobalOther;
		public static ZkDaedalusVm_getGlobalVictim ZkDaedalusVm_getGlobalVictim;
		public static ZkDaedalusVm_getGlobalHero ZkDaedalusVm_getGlobalHero;
		public static ZkDaedalusVm_getGlobalItem ZkDaedalusVm_getGlobalItem;
		public static ZkDaedalusVm_setGlobalSelf ZkDaedalusVm_setGlobalSelf;
		public static ZkDaedalusVm_setGlobalOther ZkDaedalusVm_setGlobalOther;
		public static ZkDaedalusVm_setGlobalVictim ZkDaedalusVm_setGlobalVictim;
		public static ZkDaedalusVm_setGlobalHero ZkDaedalusVm_setGlobalHero;
		public static ZkDaedalusVm_setGlobalItem ZkDaedalusVm_setGlobalItem;
		public static ZkDaedalusVm_callFunction ZkDaedalusVm_callFunction;
		public static ZkDaedalusVm_initInstance ZkDaedalusVm_initInstance;
		public static ZkDaedalusVm_registerExternal ZkDaedalusVm_registerExternal;
		public static ZkDaedalusVm_registerExternalDefault ZkDaedalusVm_registerExternalDefault;
		public static ZkDaedalusVm_printStackTrace ZkDaedalusVm_printStackTrace;
		public static ZkGuildValuesInstance_getWaterDepthKnee ZkGuildValuesInstance_getWaterDepthKnee;
		public static ZkGuildValuesInstance_getWaterDepthChest ZkGuildValuesInstance_getWaterDepthChest;
		public static ZkGuildValuesInstance_getJumpUpHeight ZkGuildValuesInstance_getJumpUpHeight;
		public static ZkGuildValuesInstance_getSwimTime ZkGuildValuesInstance_getSwimTime;
		public static ZkGuildValuesInstance_getDiveTime ZkGuildValuesInstance_getDiveTime;
		public static ZkGuildValuesInstance_getStepHeight ZkGuildValuesInstance_getStepHeight;
		public static ZkGuildValuesInstance_getJumpLowHeight ZkGuildValuesInstance_getJumpLowHeight;
		public static ZkGuildValuesInstance_getJumpMidHeight ZkGuildValuesInstance_getJumpMidHeight;
		public static ZkGuildValuesInstance_getSlideAngle ZkGuildValuesInstance_getSlideAngle;
		public static ZkGuildValuesInstance_getSlideAngle2 ZkGuildValuesInstance_getSlideAngle2;
		public static ZkGuildValuesInstance_getDisableAutoRoll ZkGuildValuesInstance_getDisableAutoRoll;
		public static ZkGuildValuesInstance_getSurfaceAlign ZkGuildValuesInstance_getSurfaceAlign;
		public static ZkGuildValuesInstance_getClimbHeadingAngle ZkGuildValuesInstance_getClimbHeadingAngle;
		public static ZkGuildValuesInstance_getClimbHorizAngle ZkGuildValuesInstance_getClimbHorizAngle;
		public static ZkGuildValuesInstance_getClimbGroundAngle ZkGuildValuesInstance_getClimbGroundAngle;
		public static ZkGuildValuesInstance_getFightRangeBase ZkGuildValuesInstance_getFightRangeBase;
		public static ZkGuildValuesInstance_getFightRangeFist ZkGuildValuesInstance_getFightRangeFist;
		public static ZkGuildValuesInstance_getFightRangeG ZkGuildValuesInstance_getFightRangeG;
		public static ZkGuildValuesInstance_getFightRange1Hs ZkGuildValuesInstance_getFightRange1Hs;
		public static ZkGuildValuesInstance_getFightRange1Ha ZkGuildValuesInstance_getFightRange1Ha;
		public static ZkGuildValuesInstance_getFightRange2Hs ZkGuildValuesInstance_getFightRange2Hs;
		public static ZkGuildValuesInstance_getFightRange2Ha ZkGuildValuesInstance_getFightRange2Ha;
		public static ZkGuildValuesInstance_getFallDownHeight ZkGuildValuesInstance_getFallDownHeight;
		public static ZkGuildValuesInstance_getFallDownDamage ZkGuildValuesInstance_getFallDownDamage;
		public static ZkGuildValuesInstance_getBloodDisabled ZkGuildValuesInstance_getBloodDisabled;
		public static ZkGuildValuesInstance_getBloodMaxDistance ZkGuildValuesInstance_getBloodMaxDistance;
		public static ZkGuildValuesInstance_getBloodAmount ZkGuildValuesInstance_getBloodAmount;
		public static ZkGuildValuesInstance_getBloodFlow ZkGuildValuesInstance_getBloodFlow;
		public static ZkGuildValuesInstance_getTurnSpeed ZkGuildValuesInstance_getTurnSpeed;
		public static ZkGuildValuesInstance_getBloodEmitter ZkGuildValuesInstance_getBloodEmitter;
		public static ZkGuildValuesInstance_getBloodTexture ZkGuildValuesInstance_getBloodTexture;
		public static ZkNpcInstance_getId ZkNpcInstance_getId;
		public static ZkNpcInstance_getSlot ZkNpcInstance_getSlot;
		public static ZkNpcInstance_getEffect ZkNpcInstance_getEffect;
		public static ZkNpcInstance_getType ZkNpcInstance_getType;
		public static ZkNpcInstance_getFlags ZkNpcInstance_getFlags;
		public static ZkNpcInstance_getDamageType ZkNpcInstance_getDamageType;
		public static ZkNpcInstance_getGuild ZkNpcInstance_getGuild;
		public static ZkNpcInstance_getLevel ZkNpcInstance_getLevel;
		public static ZkNpcInstance_getFightTactic ZkNpcInstance_getFightTactic;
		public static ZkNpcInstance_getWeapon ZkNpcInstance_getWeapon;
		public static ZkNpcInstance_getVoice ZkNpcInstance_getVoice;
		public static ZkNpcInstance_getVoicePitch ZkNpcInstance_getVoicePitch;
		public static ZkNpcInstance_getBodyMass ZkNpcInstance_getBodyMass;
		public static ZkNpcInstance_getDailyRoutine ZkNpcInstance_getDailyRoutine;
		public static ZkNpcInstance_getStartAiState ZkNpcInstance_getStartAiState;
		public static ZkNpcInstance_getSpawnPoint ZkNpcInstance_getSpawnPoint;
		public static ZkNpcInstance_getSpawnDelay ZkNpcInstance_getSpawnDelay;
		public static ZkNpcInstance_getSenses ZkNpcInstance_getSenses;
		public static ZkNpcInstance_getSensesRange ZkNpcInstance_getSensesRange;
		public static ZkNpcInstance_getWp ZkNpcInstance_getWp;
		public static ZkNpcInstance_getExp ZkNpcInstance_getExp;
		public static ZkNpcInstance_getExpNext ZkNpcInstance_getExpNext;
		public static ZkNpcInstance_getLp ZkNpcInstance_getLp;
		public static ZkNpcInstance_getBodyStateInterruptableOverride ZkNpcInstance_getBodyStateInterruptableOverride;
		public static ZkNpcInstance_getNoFocus ZkNpcInstance_getNoFocus;
		public static ZkNpcInstance_getName ZkNpcInstance_getName;
		public static ZkNpcInstance_getMission ZkNpcInstance_getMission;
		public static ZkNpcInstance_getAttribute ZkNpcInstance_getAttribute;
		public static ZkNpcInstance_getHitChance ZkNpcInstance_getHitChance;
		public static ZkNpcInstance_getProtection ZkNpcInstance_getProtection;
		public static ZkNpcInstance_getDamage ZkNpcInstance_getDamage;
		public static ZkNpcInstance_getAiVar ZkNpcInstance_getAiVar;
		public static ZkMissionInstance_getName ZkMissionInstance_getName;
		public static ZkMissionInstance_getDescription ZkMissionInstance_getDescription;
		public static ZkMissionInstance_getDuration ZkMissionInstance_getDuration;
		public static ZkMissionInstance_getImportant ZkMissionInstance_getImportant;
		public static ZkMissionInstance_getOfferConditions ZkMissionInstance_getOfferConditions;
		public static ZkMissionInstance_getOffer ZkMissionInstance_getOffer;
		public static ZkMissionInstance_getSuccessConditions ZkMissionInstance_getSuccessConditions;
		public static ZkMissionInstance_getSuccess ZkMissionInstance_getSuccess;
		public static ZkMissionInstance_getFailureConditions ZkMissionInstance_getFailureConditions;
		public static ZkMissionInstance_getFailure ZkMissionInstance_getFailure;
		public static ZkMissionInstance_getObsoleteConditions ZkMissionInstance_getObsoleteConditions;
		public static ZkMissionInstance_getObsolete ZkMissionInstance_getObsolete;
		public static ZkMissionInstance_getRunning ZkMissionInstance_getRunning;
		public static ZkItemInstance_getId ZkItemInstance_getId;
		public static ZkItemInstance_getName ZkItemInstance_getName;
		public static ZkItemInstance_getNameId ZkItemInstance_getNameId;
		public static ZkItemInstance_getHp ZkItemInstance_getHp;
		public static ZkItemInstance_getHpMax ZkItemInstance_getHpMax;
		public static ZkItemInstance_getMainFlag ZkItemInstance_getMainFlag;
		public static ZkItemInstance_getFlags ZkItemInstance_getFlags;
		public static ZkItemInstance_getWeight ZkItemInstance_getWeight;
		public static ZkItemInstance_getValue ZkItemInstance_getValue;
		public static ZkItemInstance_getDamageType ZkItemInstance_getDamageType;
		public static ZkItemInstance_getDamageTotal ZkItemInstance_getDamageTotal;
		public static ZkItemInstance_getWear ZkItemInstance_getWear;
		public static ZkItemInstance_getNutrition ZkItemInstance_getNutrition;
		public static ZkItemInstance_getMagic ZkItemInstance_getMagic;
		public static ZkItemInstance_getOnEquip ZkItemInstance_getOnEquip;
		public static ZkItemInstance_getOnUnequip ZkItemInstance_getOnUnequip;
		public static ZkItemInstance_getOwner ZkItemInstance_getOwner;
		public static ZkItemInstance_getOwnerGuild ZkItemInstance_getOwnerGuild;
		public static ZkItemInstance_getDisguiseGuild ZkItemInstance_getDisguiseGuild;
		public static ZkItemInstance_getVisual ZkItemInstance_getVisual;
		public static ZkItemInstance_getVisualChange ZkItemInstance_getVisualChange;
		public static ZkItemInstance_getEffect ZkItemInstance_getEffect;
		public static ZkItemInstance_getVisualSkin ZkItemInstance_getVisualSkin;
		public static ZkItemInstance_getSchemeName ZkItemInstance_getSchemeName;
		public static ZkItemInstance_getMaterial ZkItemInstance_getMaterial;
		public static ZkItemInstance_getMunition ZkItemInstance_getMunition;
		public static ZkItemInstance_getSpell ZkItemInstance_getSpell;
		public static ZkItemInstance_getRange ZkItemInstance_getRange;
		public static ZkItemInstance_getMagCircle ZkItemInstance_getMagCircle;
		public static ZkItemInstance_getDescription ZkItemInstance_getDescription;
		public static ZkItemInstance_getInvZBias ZkItemInstance_getInvZBias;
		public static ZkItemInstance_getInvRotX ZkItemInstance_getInvRotX;
		public static ZkItemInstance_getInvRotY ZkItemInstance_getInvRotY;
		public static ZkItemInstance_getInvRotZ ZkItemInstance_getInvRotZ;
		public static ZkItemInstance_getInvAnimate ZkItemInstance_getInvAnimate;
		public static ZkItemInstance_getDamage ZkItemInstance_getDamage;
		public static ZkItemInstance_getProtection ZkItemInstance_getProtection;
		public static ZkItemInstance_getCondAtr ZkItemInstance_getCondAtr;
		public static ZkItemInstance_getCondValue ZkItemInstance_getCondValue;
		public static ZkItemInstance_getChangeAtr ZkItemInstance_getChangeAtr;
		public static ZkItemInstance_getChangeValue ZkItemInstance_getChangeValue;
		public static ZkItemInstance_getOnState ZkItemInstance_getOnState;
		public static ZkItemInstance_getText ZkItemInstance_getText;
		public static ZkItemInstance_getCount ZkItemInstance_getCount;
		public static ZkFocusInstance_getNpcLongrange ZkFocusInstance_getNpcLongrange;
		public static ZkFocusInstance_getNpcRange1 ZkFocusInstance_getNpcRange1;
		public static ZkFocusInstance_getNpcRange2 ZkFocusInstance_getNpcRange2;
		public static ZkFocusInstance_getNpcAzi ZkFocusInstance_getNpcAzi;
		public static ZkFocusInstance_getNpcElevdo ZkFocusInstance_getNpcElevdo;
		public static ZkFocusInstance_getNpcElevup ZkFocusInstance_getNpcElevup;
		public static ZkFocusInstance_getNpcPrio ZkFocusInstance_getNpcPrio;
		public static ZkFocusInstance_getItemRange1 ZkFocusInstance_getItemRange1;
		public static ZkFocusInstance_getItemRange2 ZkFocusInstance_getItemRange2;
		public static ZkFocusInstance_getItemAzi ZkFocusInstance_getItemAzi;
		public static ZkFocusInstance_getItemElevdo ZkFocusInstance_getItemElevdo;
		public static ZkFocusInstance_getItemElevup ZkFocusInstance_getItemElevup;
		public static ZkFocusInstance_getItemPrio ZkFocusInstance_getItemPrio;
		public static ZkFocusInstance_getMobRange1 ZkFocusInstance_getMobRange1;
		public static ZkFocusInstance_getMobRange2 ZkFocusInstance_getMobRange2;
		public static ZkFocusInstance_getMobAzi ZkFocusInstance_getMobAzi;
		public static ZkFocusInstance_getMobElevdo ZkFocusInstance_getMobElevdo;
		public static ZkFocusInstance_getMobElevup ZkFocusInstance_getMobElevup;
		public static ZkFocusInstance_getMobPrio ZkFocusInstance_getMobPrio;
		public static ZkInfoInstance_getNpc ZkInfoInstance_getNpc;
		public static ZkInfoInstance_getNr ZkInfoInstance_getNr;
		public static ZkInfoInstance_getImportant ZkInfoInstance_getImportant;
		public static ZkInfoInstance_getCondition ZkInfoInstance_getCondition;
		public static ZkInfoInstance_getInformation ZkInfoInstance_getInformation;
		public static ZkInfoInstance_getDescription ZkInfoInstance_getDescription;
		public static ZkInfoInstance_getTrade ZkInfoInstance_getTrade;
		public static ZkInfoInstance_getPermanent ZkInfoInstance_getPermanent;
		public static ZkItemReactInstance_getNpc ZkItemReactInstance_getNpc;
		public static ZkItemReactInstance_getTradeItem ZkItemReactInstance_getTradeItem;
		public static ZkItemReactInstance_getTradeAmount ZkItemReactInstance_getTradeAmount;
		public static ZkItemReactInstance_getRequestedCategory ZkItemReactInstance_getRequestedCategory;
		public static ZkItemReactInstance_getRequestedItem ZkItemReactInstance_getRequestedItem;
		public static ZkItemReactInstance_getRequestedAmount ZkItemReactInstance_getRequestedAmount;
		public static ZkItemReactInstance_getReaction ZkItemReactInstance_getReaction;
		public static ZkSpellInstance_getTimePerMana ZkSpellInstance_getTimePerMana;
		public static ZkSpellInstance_getDamagePerLevel ZkSpellInstance_getDamagePerLevel;
		public static ZkSpellInstance_getDamageType ZkSpellInstance_getDamageType;
		public static ZkSpellInstance_getSpellType ZkSpellInstance_getSpellType;
		public static ZkSpellInstance_getCanTurnDuringInvest ZkSpellInstance_getCanTurnDuringInvest;
		public static ZkSpellInstance_getCanChangeTargetDuringInvest ZkSpellInstance_getCanChangeTargetDuringInvest;
		public static ZkSpellInstance_getIsMultiEffect ZkSpellInstance_getIsMultiEffect;
		public static ZkSpellInstance_getTargetCollectAlgo ZkSpellInstance_getTargetCollectAlgo;
		public static ZkSpellInstance_getTargetCollectType ZkSpellInstance_getTargetCollectType;
		public static ZkSpellInstance_getTargetCollectRange ZkSpellInstance_getTargetCollectRange;
		public static ZkSpellInstance_getTargetCollectAzi ZkSpellInstance_getTargetCollectAzi;
		public static ZkSpellInstance_getTargetCollectElevation ZkSpellInstance_getTargetCollectElevation;
		public static ZkMenuInstance_getBackPic ZkMenuInstance_getBackPic;
		public static ZkMenuInstance_getBackWorld ZkMenuInstance_getBackWorld;
		public static ZkMenuInstance_getPosX ZkMenuInstance_getPosX;
		public static ZkMenuInstance_getPosY ZkMenuInstance_getPosY;
		public static ZkMenuInstance_getDimX ZkMenuInstance_getDimX;
		public static ZkMenuInstance_getDimY ZkMenuInstance_getDimY;
		public static ZkMenuInstance_getAlpha ZkMenuInstance_getAlpha;
		public static ZkMenuInstance_getMusicTheme ZkMenuInstance_getMusicTheme;
		public static ZkMenuInstance_getEventTimerMsec ZkMenuInstance_getEventTimerMsec;
		public static ZkMenuInstance_getFlags ZkMenuInstance_getFlags;
		public static ZkMenuInstance_getDefaultOutgame ZkMenuInstance_getDefaultOutgame;
		public static ZkMenuInstance_getDefaultIngame ZkMenuInstance_getDefaultIngame;
		public static ZkMenuInstance_getItem ZkMenuInstance_getItem;
		public static ZkMenuItemInstance_getFontName ZkMenuItemInstance_getFontName;
		public static ZkMenuItemInstance_getBackpic ZkMenuItemInstance_getBackpic;
		public static ZkMenuItemInstance_getAlphaMode ZkMenuItemInstance_getAlphaMode;
		public static ZkMenuItemInstance_getAlpha ZkMenuItemInstance_getAlpha;
		public static ZkMenuItemInstance_getType ZkMenuItemInstance_getType;
		public static ZkMenuItemInstance_getOnChgSetOption ZkMenuItemInstance_getOnChgSetOption;
		public static ZkMenuItemInstance_getOnChgSetOptionSection ZkMenuItemInstance_getOnChgSetOptionSection;
		public static ZkMenuItemInstance_getPosX ZkMenuItemInstance_getPosX;
		public static ZkMenuItemInstance_getPosY ZkMenuItemInstance_getPosY;
		public static ZkMenuItemInstance_getDimX ZkMenuItemInstance_getDimX;
		public static ZkMenuItemInstance_getDimY ZkMenuItemInstance_getDimY;
		public static ZkMenuItemInstance_getSizeStartScale ZkMenuItemInstance_getSizeStartScale;
		public static ZkMenuItemInstance_getFlags ZkMenuItemInstance_getFlags;
		public static ZkMenuItemInstance_getOpenDelayTime ZkMenuItemInstance_getOpenDelayTime;
		public static ZkMenuItemInstance_getOpenDuration ZkMenuItemInstance_getOpenDuration;
		public static ZkMenuItemInstance_getFramePosX ZkMenuItemInstance_getFramePosX;
		public static ZkMenuItemInstance_getFramePosY ZkMenuItemInstance_getFramePosY;
		public static ZkMenuItemInstance_getFrameSizeX ZkMenuItemInstance_getFrameSizeX;
		public static ZkMenuItemInstance_getFrameSizeY ZkMenuItemInstance_getFrameSizeY;
		public static ZkMenuItemInstance_getHideIfOptionSectionSet ZkMenuItemInstance_getHideIfOptionSectionSet;
		public static ZkMenuItemInstance_getHideIfOptionSet ZkMenuItemInstance_getHideIfOptionSet;
		public static ZkMenuItemInstance_getHideOnValue ZkMenuItemInstance_getHideOnValue;
		public static ZkMenuItemInstance_getText ZkMenuItemInstance_getText;
		public static ZkMenuItemInstance_getOnSelAction ZkMenuItemInstance_getOnSelAction;
		public static ZkMenuItemInstance_getOnSelActionS ZkMenuItemInstance_getOnSelActionS;
		public static ZkMenuItemInstance_getOnEventAction ZkMenuItemInstance_getOnEventAction;
		public static ZkMenuItemInstance_getUserFloat ZkMenuItemInstance_getUserFloat;
		public static ZkMenuItemInstance_getUserString ZkMenuItemInstance_getUserString;
		public static ZkCameraInstance_getBestRange ZkCameraInstance_getBestRange;
		public static ZkCameraInstance_getMinRange ZkCameraInstance_getMinRange;
		public static ZkCameraInstance_getMaxRange ZkCameraInstance_getMaxRange;
		public static ZkCameraInstance_getBestElevation ZkCameraInstance_getBestElevation;
		public static ZkCameraInstance_getMinElevation ZkCameraInstance_getMinElevation;
		public static ZkCameraInstance_getMaxElevation ZkCameraInstance_getMaxElevation;
		public static ZkCameraInstance_getBestAzimuth ZkCameraInstance_getBestAzimuth;
		public static ZkCameraInstance_getMinAzimuth ZkCameraInstance_getMinAzimuth;
		public static ZkCameraInstance_getMaxAzimuth ZkCameraInstance_getMaxAzimuth;
		public static ZkCameraInstance_getBestRotZ ZkCameraInstance_getBestRotZ;
		public static ZkCameraInstance_getMinRotZ ZkCameraInstance_getMinRotZ;
		public static ZkCameraInstance_getMaxRotZ ZkCameraInstance_getMaxRotZ;
		public static ZkCameraInstance_getRotOffsetX ZkCameraInstance_getRotOffsetX;
		public static ZkCameraInstance_getRotOffsetY ZkCameraInstance_getRotOffsetY;
		public static ZkCameraInstance_getRotOffsetZ ZkCameraInstance_getRotOffsetZ;
		public static ZkCameraInstance_getTargetOffsetX ZkCameraInstance_getTargetOffsetX;
		public static ZkCameraInstance_getTargetOffsetY ZkCameraInstance_getTargetOffsetY;
		public static ZkCameraInstance_getTargetOffsetZ ZkCameraInstance_getTargetOffsetZ;
		public static ZkCameraInstance_getVeloTrans ZkCameraInstance_getVeloTrans;
		public static ZkCameraInstance_getVeloRot ZkCameraInstance_getVeloRot;
		public static ZkCameraInstance_getTranslate ZkCameraInstance_getTranslate;
		public static ZkCameraInstance_getRotate ZkCameraInstance_getRotate;
		public static ZkCameraInstance_getCollision ZkCameraInstance_getCollision;
		public static ZkMusicSystemInstance_getVolume ZkMusicSystemInstance_getVolume;
		public static ZkMusicSystemInstance_getBitResolution ZkMusicSystemInstance_getBitResolution;
		public static ZkMusicSystemInstance_getGlobalReverbEnabled ZkMusicSystemInstance_getGlobalReverbEnabled;
		public static ZkMusicSystemInstance_getSampleRate ZkMusicSystemInstance_getSampleRate;
		public static ZkMusicSystemInstance_getNumChannels ZkMusicSystemInstance_getNumChannels;
		public static ZkMusicSystemInstance_getReverbBufferSize ZkMusicSystemInstance_getReverbBufferSize;
		public static ZkMusicThemeInstance_getFile ZkMusicThemeInstance_getFile;
		public static ZkMusicThemeInstance_getVol ZkMusicThemeInstance_getVol;
		public static ZkMusicThemeInstance_getLoop ZkMusicThemeInstance_getLoop;
		public static ZkMusicThemeInstance_getReverbmix ZkMusicThemeInstance_getReverbmix;
		public static ZkMusicThemeInstance_getReverbtime ZkMusicThemeInstance_getReverbtime;
		public static ZkMusicThemeInstance_getTranstype ZkMusicThemeInstance_getTranstype;
		public static ZkMusicThemeInstance_getTranssubtype ZkMusicThemeInstance_getTranssubtype;
		public static ZkMusicJingleInstance_getName ZkMusicJingleInstance_getName;
		public static ZkMusicJingleInstance_getLoop ZkMusicJingleInstance_getLoop;
		public static ZkMusicJingleInstance_getVol ZkMusicJingleInstance_getVol;
		public static ZkMusicJingleInstance_getTranssubtype ZkMusicJingleInstance_getTranssubtype;
		public static ZkParticleEffectInstance_getPpsValue ZkParticleEffectInstance_getPpsValue;
		public static ZkParticleEffectInstance_getPpsScaleKeysS ZkParticleEffectInstance_getPpsScaleKeysS;
		public static ZkParticleEffectInstance_getPpsIsLooping ZkParticleEffectInstance_getPpsIsLooping;
		public static ZkParticleEffectInstance_getPpsIsSmooth ZkParticleEffectInstance_getPpsIsSmooth;
		public static ZkParticleEffectInstance_getPpsFps ZkParticleEffectInstance_getPpsFps;
		public static ZkParticleEffectInstance_getPpsCreateEmS ZkParticleEffectInstance_getPpsCreateEmS;
		public static ZkParticleEffectInstance_getPpsCreateEmDelay ZkParticleEffectInstance_getPpsCreateEmDelay;
		public static ZkParticleEffectInstance_getShpTypeS ZkParticleEffectInstance_getShpTypeS;
		public static ZkParticleEffectInstance_getShpForS ZkParticleEffectInstance_getShpForS;
		public static ZkParticleEffectInstance_getShpOffsetVecS ZkParticleEffectInstance_getShpOffsetVecS;
		public static ZkParticleEffectInstance_getShpDistribTypeS ZkParticleEffectInstance_getShpDistribTypeS;
		public static ZkParticleEffectInstance_getShpDistribWalkSpeed ZkParticleEffectInstance_getShpDistribWalkSpeed;
		public static ZkParticleEffectInstance_getShpIsVolume ZkParticleEffectInstance_getShpIsVolume;
		public static ZkParticleEffectInstance_getShpDimS ZkParticleEffectInstance_getShpDimS;
		public static ZkParticleEffectInstance_getShpMeshS ZkParticleEffectInstance_getShpMeshS;
		public static ZkParticleEffectInstance_getShpMeshRenderB ZkParticleEffectInstance_getShpMeshRenderB;
		public static ZkParticleEffectInstance_getShpScaleKeysS ZkParticleEffectInstance_getShpScaleKeysS;
		public static ZkParticleEffectInstance_getShpScaleIsLooping ZkParticleEffectInstance_getShpScaleIsLooping;
		public static ZkParticleEffectInstance_getShpScaleIsSmooth ZkParticleEffectInstance_getShpScaleIsSmooth;
		public static ZkParticleEffectInstance_getShpScaleFps ZkParticleEffectInstance_getShpScaleFps;
		public static ZkParticleEffectInstance_getDirModeS ZkParticleEffectInstance_getDirModeS;
		public static ZkParticleEffectInstance_getDirForS ZkParticleEffectInstance_getDirForS;
		public static ZkParticleEffectInstance_getDirModeTargetForS ZkParticleEffectInstance_getDirModeTargetForS;
		public static ZkParticleEffectInstance_getDirModeTargetPosS ZkParticleEffectInstance_getDirModeTargetPosS;
		public static ZkParticleEffectInstance_getDirAngleHead ZkParticleEffectInstance_getDirAngleHead;
		public static ZkParticleEffectInstance_getDirAngleHeadVar ZkParticleEffectInstance_getDirAngleHeadVar;
		public static ZkParticleEffectInstance_getDirAngleElev ZkParticleEffectInstance_getDirAngleElev;
		public static ZkParticleEffectInstance_getDirAngleElevVar ZkParticleEffectInstance_getDirAngleElevVar;
		public static ZkParticleEffectInstance_getVelAvg ZkParticleEffectInstance_getVelAvg;
		public static ZkParticleEffectInstance_getVelVar ZkParticleEffectInstance_getVelVar;
		public static ZkParticleEffectInstance_getLspPartAvg ZkParticleEffectInstance_getLspPartAvg;
		public static ZkParticleEffectInstance_getLspPartVar ZkParticleEffectInstance_getLspPartVar;
		public static ZkParticleEffectInstance_getFlyGravityS ZkParticleEffectInstance_getFlyGravityS;
		public static ZkParticleEffectInstance_getFlyColldetB ZkParticleEffectInstance_getFlyColldetB;
		public static ZkParticleEffectInstance_getVisNameS ZkParticleEffectInstance_getVisNameS;
		public static ZkParticleEffectInstance_getVisOrientationS ZkParticleEffectInstance_getVisOrientationS;
		public static ZkParticleEffectInstance_getVisTexIsQuadpoly ZkParticleEffectInstance_getVisTexIsQuadpoly;
		public static ZkParticleEffectInstance_getVisTexAniFps ZkParticleEffectInstance_getVisTexAniFps;
		public static ZkParticleEffectInstance_getVisTexAniIsLooping ZkParticleEffectInstance_getVisTexAniIsLooping;
		public static ZkParticleEffectInstance_getVisTexColorStartS ZkParticleEffectInstance_getVisTexColorStartS;
		public static ZkParticleEffectInstance_getVisTexColorEndS ZkParticleEffectInstance_getVisTexColorEndS;
		public static ZkParticleEffectInstance_getVisSizeStartS ZkParticleEffectInstance_getVisSizeStartS;
		public static ZkParticleEffectInstance_getVisSizeEndScale ZkParticleEffectInstance_getVisSizeEndScale;
		public static ZkParticleEffectInstance_getVisAlphaFuncS ZkParticleEffectInstance_getVisAlphaFuncS;
		public static ZkParticleEffectInstance_getVisAlphaStart ZkParticleEffectInstance_getVisAlphaStart;
		public static ZkParticleEffectInstance_getVisAlphaEnd ZkParticleEffectInstance_getVisAlphaEnd;
		public static ZkParticleEffectInstance_getTrlFadeSpeed ZkParticleEffectInstance_getTrlFadeSpeed;
		public static ZkParticleEffectInstance_getTrlTextureS ZkParticleEffectInstance_getTrlTextureS;
		public static ZkParticleEffectInstance_getTrlWidth ZkParticleEffectInstance_getTrlWidth;
		public static ZkParticleEffectInstance_getMrkFadesPeed ZkParticleEffectInstance_getMrkFadesPeed;
		public static ZkParticleEffectInstance_getMrktExtureS ZkParticleEffectInstance_getMrktExtureS;
		public static ZkParticleEffectInstance_getMrkSize ZkParticleEffectInstance_getMrkSize;
		public static ZkParticleEffectInstance_getFlockMode ZkParticleEffectInstance_getFlockMode;
		public static ZkParticleEffectInstance_getFlockStrength ZkParticleEffectInstance_getFlockStrength;
		public static ZkParticleEffectInstance_getUseEmittersFor ZkParticleEffectInstance_getUseEmittersFor;
		public static ZkParticleEffectInstance_getTimeStartEndS ZkParticleEffectInstance_getTimeStartEndS;
		public static ZkParticleEffectInstance_getMBiasAmbientPfx ZkParticleEffectInstance_getMBiasAmbientPfx;
		public static ZkEffectBaseInstance_getVisNameS ZkEffectBaseInstance_getVisNameS;
		public static ZkEffectBaseInstance_getVisSizeS ZkEffectBaseInstance_getVisSizeS;
		public static ZkEffectBaseInstance_getVisAlpha ZkEffectBaseInstance_getVisAlpha;
		public static ZkEffectBaseInstance_getVisAlphaBlendFuncS ZkEffectBaseInstance_getVisAlphaBlendFuncS;
		public static ZkEffectBaseInstance_getVisTexAniFps ZkEffectBaseInstance_getVisTexAniFps;
		public static ZkEffectBaseInstance_getVisTexAniIsLooping ZkEffectBaseInstance_getVisTexAniIsLooping;
		public static ZkEffectBaseInstance_getEmTrjModeS ZkEffectBaseInstance_getEmTrjModeS;
		public static ZkEffectBaseInstance_getEmTrjOriginNode ZkEffectBaseInstance_getEmTrjOriginNode;
		public static ZkEffectBaseInstance_getEmTrjTargetNode ZkEffectBaseInstance_getEmTrjTargetNode;
		public static ZkEffectBaseInstance_getEmTrjTargetRange ZkEffectBaseInstance_getEmTrjTargetRange;
		public static ZkEffectBaseInstance_getEmTrjTargetAzi ZkEffectBaseInstance_getEmTrjTargetAzi;
		public static ZkEffectBaseInstance_getEmTrjTargetElev ZkEffectBaseInstance_getEmTrjTargetElev;
		public static ZkEffectBaseInstance_getEmTrjNumKeys ZkEffectBaseInstance_getEmTrjNumKeys;
		public static ZkEffectBaseInstance_getEmTrjNumKeysVar ZkEffectBaseInstance_getEmTrjNumKeysVar;
		public static ZkEffectBaseInstance_getEmTrjAngleElevVar ZkEffectBaseInstance_getEmTrjAngleElevVar;
		public static ZkEffectBaseInstance_getEmTrjAngleHeadVar ZkEffectBaseInstance_getEmTrjAngleHeadVar;
		public static ZkEffectBaseInstance_getEmTrjKeyDistVar ZkEffectBaseInstance_getEmTrjKeyDistVar;
		public static ZkEffectBaseInstance_getEmTrjLoopModeS ZkEffectBaseInstance_getEmTrjLoopModeS;
		public static ZkEffectBaseInstance_getEmTrjEaseFuncS ZkEffectBaseInstance_getEmTrjEaseFuncS;
		public static ZkEffectBaseInstance_getEmTrjEaseVel ZkEffectBaseInstance_getEmTrjEaseVel;
		public static ZkEffectBaseInstance_getEmTrjDynUpdateDelay ZkEffectBaseInstance_getEmTrjDynUpdateDelay;
		public static ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly;
		public static ZkEffectBaseInstance_getEmFxCreateS ZkEffectBaseInstance_getEmFxCreateS;
		public static ZkEffectBaseInstance_getEmFxInvestOriginS ZkEffectBaseInstance_getEmFxInvestOriginS;
		public static ZkEffectBaseInstance_getEmFxInvestTargetS ZkEffectBaseInstance_getEmFxInvestTargetS;
		public static ZkEffectBaseInstance_getEmFxTriggerDelay ZkEffectBaseInstance_getEmFxTriggerDelay;
		public static ZkEffectBaseInstance_getEmFxCreateDownTrj ZkEffectBaseInstance_getEmFxCreateDownTrj;
		public static ZkEffectBaseInstance_getEmActionCollDynS ZkEffectBaseInstance_getEmActionCollDynS;
		public static ZkEffectBaseInstance_getEmActionCollStatS ZkEffectBaseInstance_getEmActionCollStatS;
		public static ZkEffectBaseInstance_getEmFxCollStatS ZkEffectBaseInstance_getEmFxCollStatS;
		public static ZkEffectBaseInstance_getEmFxCollDynS ZkEffectBaseInstance_getEmFxCollDynS;
		public static ZkEffectBaseInstance_getEmFxCollStatAlignS ZkEffectBaseInstance_getEmFxCollStatAlignS;
		public static ZkEffectBaseInstance_getEmFxCollDynAlignS ZkEffectBaseInstance_getEmFxCollDynAlignS;
		public static ZkEffectBaseInstance_getEmFxLifespan ZkEffectBaseInstance_getEmFxLifespan;
		public static ZkEffectBaseInstance_getEmCheckCollision ZkEffectBaseInstance_getEmCheckCollision;
		public static ZkEffectBaseInstance_getEmAdjustShpToOrigin ZkEffectBaseInstance_getEmAdjustShpToOrigin;
		public static ZkEffectBaseInstance_getEmInvestNextKeyDuration ZkEffectBaseInstance_getEmInvestNextKeyDuration;
		public static ZkEffectBaseInstance_getEmFlyGravity ZkEffectBaseInstance_getEmFlyGravity;
		public static ZkEffectBaseInstance_getEmSelfRotVelS ZkEffectBaseInstance_getEmSelfRotVelS;
		public static ZkEffectBaseInstance_getLightPresetName ZkEffectBaseInstance_getLightPresetName;
		public static ZkEffectBaseInstance_getSfxId ZkEffectBaseInstance_getSfxId;
		public static ZkEffectBaseInstance_getSfxIsAmbient ZkEffectBaseInstance_getSfxIsAmbient;
		public static ZkEffectBaseInstance_getSendAssessMagic ZkEffectBaseInstance_getSendAssessMagic;
		public static ZkEffectBaseInstance_getSecsPerDamage ZkEffectBaseInstance_getSecsPerDamage;
		public static ZkEffectBaseInstance_getEmFxCollDynPercS ZkEffectBaseInstance_getEmFxCollDynPercS;
		public static ZkEffectBaseInstance_getUserString ZkEffectBaseInstance_getUserString;
		public static ZkParticleEffectEmitKeyInstance_getVisNameS ZkParticleEffectEmitKeyInstance_getVisNameS;
		public static ZkParticleEffectEmitKeyInstance_getVisSizeScale ZkParticleEffectEmitKeyInstance_getVisSizeScale;
		public static ZkParticleEffectEmitKeyInstance_getScaleDuration ZkParticleEffectEmitKeyInstance_getScaleDuration;
		public static ZkParticleEffectEmitKeyInstance_getPfxPpsValue ZkParticleEffectEmitKeyInstance_getPfxPpsValue;

		public static ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg
			ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg;

		public static ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg
			ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg;

		public static ZkParticleEffectEmitKeyInstance_getPfxScTime ZkParticleEffectEmitKeyInstance_getPfxScTime;

		public static ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS
			ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS;

		public static ZkParticleEffectEmitKeyInstance_getPfxShpDimS ZkParticleEffectEmitKeyInstance_getPfxShpDimS;

		public static ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg
			ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg;

		public static ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps
			ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps;

		public static ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed
			ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed;

		public static ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS
			ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS;

		public static ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS
			ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS;

		public static ZkParticleEffectEmitKeyInstance_getPfxDirModeS ZkParticleEffectEmitKeyInstance_getPfxDirModeS;
		public static ZkParticleEffectEmitKeyInstance_getPfxDirForS ZkParticleEffectEmitKeyInstance_getPfxDirForS;

		public static ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS
			ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS;

		public static ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS
			ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS;

		public static ZkParticleEffectEmitKeyInstance_getPfxVelAvg ZkParticleEffectEmitKeyInstance_getPfxVelAvg;
		public static ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg;

		public static ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart
			ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart;

		public static ZkParticleEffectEmitKeyInstance_getLightPresetName
			ZkParticleEffectEmitKeyInstance_getLightPresetName;

		public static ZkParticleEffectEmitKeyInstance_getLightRange ZkParticleEffectEmitKeyInstance_getLightRange;
		public static ZkParticleEffectEmitKeyInstance_getSfxId ZkParticleEffectEmitKeyInstance_getSfxId;
		public static ZkParticleEffectEmitKeyInstance_getSfxIsAmbient ZkParticleEffectEmitKeyInstance_getSfxIsAmbient;
		public static ZkParticleEffectEmitKeyInstance_getEmCreateFxId ZkParticleEffectEmitKeyInstance_getEmCreateFxId;
		public static ZkParticleEffectEmitKeyInstance_getEmFlyGravity ZkParticleEffectEmitKeyInstance_getEmFlyGravity;
		public static ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS;
		public static ZkParticleEffectEmitKeyInstance_getEmTrjModeS ZkParticleEffectEmitKeyInstance_getEmTrjModeS;
		public static ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel;

		public static ZkParticleEffectEmitKeyInstance_getEmCheckCollision
			ZkParticleEffectEmitKeyInstance_getEmCheckCollision;

		public static ZkParticleEffectEmitKeyInstance_getEmFxLifespan ZkParticleEffectEmitKeyInstance_getEmFxLifespan;
		public static ZkFightAiInstance_getMove ZkFightAiInstance_getMove;
		public static ZkSoundEffectInstance_getFile ZkSoundEffectInstance_getFile;
		public static ZkSoundEffectInstance_getPitchOff ZkSoundEffectInstance_getPitchOff;
		public static ZkSoundEffectInstance_getPitchVar ZkSoundEffectInstance_getPitchVar;
		public static ZkSoundEffectInstance_getVolume ZkSoundEffectInstance_getVolume;
		public static ZkSoundEffectInstance_getLoop ZkSoundEffectInstance_getLoop;
		public static ZkSoundEffectInstance_getLoopStartOffset ZkSoundEffectInstance_getLoopStartOffset;
		public static ZkSoundEffectInstance_getLoopEndOffset ZkSoundEffectInstance_getLoopEndOffset;
		public static ZkSoundEffectInstance_getReverbLevel ZkSoundEffectInstance_getReverbLevel;
		public static ZkSoundEffectInstance_getPfxName ZkSoundEffectInstance_getPfxName;
		public static ZkSoundSystemInstance_getVolume ZkSoundSystemInstance_getVolume;
		public static ZkSoundSystemInstance_getBitResolution ZkSoundSystemInstance_getBitResolution;
		public static ZkSoundSystemInstance_getSampleRate ZkSoundSystemInstance_getSampleRate;
		public static ZkSoundSystemInstance_getUseStereo ZkSoundSystemInstance_getUseStereo;
		public static ZkSoundSystemInstance_getNumSfxChannels ZkSoundSystemInstance_getNumSfxChannels;
		public static ZkSoundSystemInstance_getUsed3DProviderName ZkSoundSystemInstance_getUsed3DProviderName;

		static Native()
		{
			var loader = new NativeLibrary("czenkit", LibraryLoader.GetPlatformDefaultLoader(),
				new NativePathResolver());

			ZkAnimation_getFps = loader.LoadFunction<ZkAnimation_getFps>("ZkAnimation_getFps");
			ZkAnimation_getSpeed = loader.LoadFunction<ZkAnimation_getSpeed>("ZkAnimation_getSpeed");
			ZkLogger_set = loader.LoadFunction<ZkLogger_set>("ZkLogger_set");
			ZkLogger_setDefault = loader.LoadFunction<ZkLogger_setDefault>("ZkLogger_setDefault");
			ZkLogger_log = loader.LoadFunction<ZkLogger_log>("ZkLogger_log");
			ZkRead_newFile = loader.LoadFunction<ZkRead_newFile>("ZkRead_newFile");
			ZkRead_newMem = loader.LoadFunction<ZkRead_newMem>("ZkRead_newMem");
			ZkRead_newPath = loader.LoadFunction<ZkRead_newPath>("ZkRead_newPath");
			ZkRead_newExt = loader.LoadFunction<ZkRead_newExt>("ZkRead_newExt");
			ZkRead_del = loader.LoadFunction<ZkRead_del>("ZkRead_del");
			ZkVfs_new = loader.LoadFunction<ZkVfs_new>("ZkVfs_new");
			ZkVfs_del = loader.LoadFunction<ZkVfs_del>("ZkVfs_del");
			ZkVfs_getRoot = loader.LoadFunction<ZkVfs_getRoot>("ZkVfs_getRoot");
			ZkVfs_mkdir = loader.LoadFunction<ZkVfs_mkdir>("ZkVfs_mkdir");
			ZkVfs_remove = loader.LoadFunction<ZkVfs_remove>("ZkVfs_remove");
			ZkVfs_mount = loader.LoadFunction<ZkVfs_mount>("ZkVfs_mount");
			ZkVfs_mountHost = loader.LoadFunction<ZkVfs_mountHost>("ZkVfs_mountHost");
			ZkVfs_mountDisk = loader.LoadFunction<ZkVfs_mountDisk>("ZkVfs_mountDisk");
			ZkVfs_mountDiskHost = loader.LoadFunction<ZkVfs_mountDiskHost>("ZkVfs_mountDiskHost");
			ZkVfs_resolvePath = loader.LoadFunction<ZkVfs_resolvePath>("ZkVfs_resolvePath");
			ZkVfs_findNode = loader.LoadFunction<ZkVfs_findNode>("ZkVfs_findNode");
			ZkVfsNode_newFile = loader.LoadFunction<ZkVfsNode_newFile>("ZkVfsNode_newFile");
			ZkVfsNode_newDir = loader.LoadFunction<ZkVfsNode_newDir>("ZkVfsNode_newDir");
			ZkVfsNode_del = loader.LoadFunction<ZkVfsNode_del>("ZkVfsNode_del");
			ZkVfsNode_isFile = loader.LoadFunction<ZkVfsNode_isFile>("ZkVfsNode_isFile");
			ZkVfsNode_isDir = loader.LoadFunction<ZkVfsNode_isDir>("ZkVfsNode_isDir");
			ZkVfsNode_getTime = loader.LoadFunction<ZkVfsNode_getTime>("ZkVfsNode_getTime");
			ZkVfsNode_getName = loader.LoadFunction<ZkVfsNode_getName>("ZkVfsNode_getName");
			ZkVfsNode_getChild = loader.LoadFunction<ZkVfsNode_getChild>("ZkVfsNode_getChild");
			ZkVfsNode_create = loader.LoadFunction<ZkVfsNode_create>("ZkVfsNode_create");
			ZkVfsNode_remove = loader.LoadFunction<ZkVfsNode_remove>("ZkVfsNode_remove");
			ZkVfsNode_open = loader.LoadFunction<ZkVfsNode_open>("ZkVfsNode_open");
			ZkVfsNode_enumerateChildren =
				loader.LoadFunction<ZkVfsNode_enumerateChildren>("ZkVfsNode_enumerateChildren");
			ZkCutsceneLibrary_load = loader.LoadFunction<ZkCutsceneLibrary_load>("ZkCutsceneLibrary_load");
			ZkCutsceneLibrary_loadPath = loader.LoadFunction<ZkCutsceneLibrary_loadPath>("ZkCutsceneLibrary_loadPath");
			ZkCutsceneLibrary_loadVfs = loader.LoadFunction<ZkCutsceneLibrary_loadVfs>("ZkCutsceneLibrary_loadVfs");
			ZkCutsceneLibrary_del = loader.LoadFunction<ZkCutsceneLibrary_del>("ZkCutsceneLibrary_del");
			ZkCutsceneLibrary_getBlock = loader.LoadFunction<ZkCutsceneLibrary_getBlock>("ZkCutsceneLibrary_getBlock");
			ZkCutsceneLibrary_enumerateBlocks =
				loader.LoadFunction<ZkCutsceneLibrary_enumerateBlocks>("ZkCutsceneLibrary_enumerateBlocks");
			ZkCutsceneBlock_getName = loader.LoadFunction<ZkCutsceneBlock_getName>("ZkCutsceneBlock_getName");
			ZkCutsceneBlock_getMessage = loader.LoadFunction<ZkCutsceneBlock_getMessage>("ZkCutsceneBlock_getMessage");
			ZkCutsceneMessage_getType = loader.LoadFunction<ZkCutsceneMessage_getType>("ZkCutsceneMessage_getType");
			ZkCutsceneMessage_getText = loader.LoadFunction<ZkCutsceneMessage_getText>("ZkCutsceneMessage_getText");
			ZkCutsceneMessage_getName = loader.LoadFunction<ZkCutsceneMessage_getName>("ZkCutsceneMessage_getName");
			ZkFont_load = loader.LoadFunction<ZkFont_load>("ZkFont_load");
			ZkFont_loadPath = loader.LoadFunction<ZkFont_loadPath>("ZkFont_loadPath");
			ZkFont_loadVfs = loader.LoadFunction<ZkFont_loadVfs>("ZkFont_loadVfs");
			ZkFont_del = loader.LoadFunction<ZkFont_del>("ZkFont_del");
			ZkFont_getName = loader.LoadFunction<ZkFont_getName>("ZkFont_getName");
			ZkFont_getHeight = loader.LoadFunction<ZkFont_getHeight>("ZkFont_getHeight");
			ZkFont_getGlyphCount = loader.LoadFunction<ZkFont_getGlyphCount>("ZkFont_getGlyphCount");
			ZkFont_getGlyph = loader.LoadFunction<ZkFont_getGlyph>("ZkFont_getGlyph");
			ZkFont_enumerateGlyphs = loader.LoadFunction<ZkFont_enumerateGlyphs>("ZkFont_enumerateGlyphs");
			ZkModelAnimation_load = loader.LoadFunction<ZkModelAnimation_load>("ZkModelAnimation_load");
			ZkModelAnimation_loadPath = loader.LoadFunction<ZkModelAnimation_loadPath>("ZkModelAnimation_loadPath");
			ZkModelAnimation_loadVfs = loader.LoadFunction<ZkModelAnimation_loadVfs>("ZkModelAnimation_loadVfs");
			ZkModelAnimation_del = loader.LoadFunction<ZkModelAnimation_del>("ZkModelAnimation_del");
			ZkModelAnimation_getName = loader.LoadFunction<ZkModelAnimation_getName>("ZkModelAnimation_getName");
			ZkModelAnimation_getNext = loader.LoadFunction<ZkModelAnimation_getNext>("ZkModelAnimation_getNext");
			ZkModelAnimation_getLayer = loader.LoadFunction<ZkModelAnimation_getLayer>("ZkModelAnimation_getLayer");
			ZkModelAnimation_getFrameCount =
				loader.LoadFunction<ZkModelAnimation_getFrameCount>("ZkModelAnimation_getFrameCount");
			ZkModelAnimation_getNodeCount =
				loader.LoadFunction<ZkModelAnimation_getNodeCount>("ZkModelAnimation_getNodeCount");
			ZkModelAnimation_getFps = loader.LoadFunction<ZkModelAnimation_getFps>("ZkModelAnimation_getFps");
			ZkModelAnimation_getFpsSource =
				loader.LoadFunction<ZkModelAnimation_getFpsSource>("ZkModelAnimation_getFpsSource");
			ZkModelAnimation_getBbox = loader.LoadFunction<ZkModelAnimation_getBbox>("ZkModelAnimation_getBbox");
			ZkModelAnimation_getChecksum =
				loader.LoadFunction<ZkModelAnimation_getChecksum>("ZkModelAnimation_getChecksum");
			ZkModelAnimation_getSourcePath =
				loader.LoadFunction<ZkModelAnimation_getSourcePath>("ZkModelAnimation_getSourcePath");
			ZkModelAnimation_getSourceDate =
				loader.LoadFunction<ZkModelAnimation_getSourceDate>("ZkModelAnimation_getSourceDate");
			ZkModelAnimation_getSourceScript =
				loader.LoadFunction<ZkModelAnimation_getSourceScript>("ZkModelAnimation_getSourceScript");
			ZkModelAnimation_getSampleCount =
				loader.LoadFunction<ZkModelAnimation_getSampleCount>("ZkModelAnimation_getSampleCount");
			ZkModelAnimation_getSample = loader.LoadFunction<ZkModelAnimation_getSample>("ZkModelAnimation_getSample");
			ZkModelAnimation_enumerateSamples =
				loader.LoadFunction<ZkModelAnimation_enumerateSamples>("ZkModelAnimation_enumerateSamples");
			ZkModelAnimation_getNodeIndices =
				loader.LoadFunction<ZkModelAnimation_getNodeIndices>("ZkModelAnimation_getNodeIndices");
			ZkModelHierarchy_load = loader.LoadFunction<ZkModelHierarchy_load>("ZkModelHierarchy_load");
			ZkModelHierarchy_loadPath = loader.LoadFunction<ZkModelHierarchy_loadPath>("ZkModelHierarchy_loadPath");
			ZkModelHierarchy_loadVfs = loader.LoadFunction<ZkModelHierarchy_loadVfs>("ZkModelHierarchy_loadVfs");
			ZkModelHierarchy_del = loader.LoadFunction<ZkModelHierarchy_del>("ZkModelHierarchy_del");
			ZkModelHierarchy_getNodeCount =
				loader.LoadFunction<ZkModelHierarchy_getNodeCount>("ZkModelHierarchy_getNodeCount");
			ZkModelHierarchy_getNode = loader.LoadFunction<ZkModelHierarchy_getNode>("ZkModelHierarchy_getNode");
			ZkModelHierarchy_getBbox = loader.LoadFunction<ZkModelHierarchy_getBbox>("ZkModelHierarchy_getBbox");
			ZkModelHierarchy_getCollisionBbox =
				loader.LoadFunction<ZkModelHierarchy_getCollisionBbox>("ZkModelHierarchy_getCollisionBbox");
			ZkModelHierarchy_getRootTranslation =
				loader.LoadFunction<ZkModelHierarchy_getRootTranslation>("ZkModelHierarchy_getRootTranslation");
			ZkModelHierarchy_getChecksum =
				loader.LoadFunction<ZkModelHierarchy_getChecksum>("ZkModelHierarchy_getChecksum");
			ZkModelHierarchy_getSourceDate =
				loader.LoadFunction<ZkModelHierarchy_getSourceDate>("ZkModelHierarchy_getSourceDate");
			ZkModelHierarchy_getSourcePath =
				loader.LoadFunction<ZkModelHierarchy_getSourcePath>("ZkModelHierarchy_getSourcePath");
			ZkModelHierarchy_enumerateNodes =
				loader.LoadFunction<ZkModelHierarchy_enumerateNodes>("ZkModelHierarchy_enumerateNodes");
			ZkOrientedBoundingBox_getCenter =
				loader.LoadFunction<ZkOrientedBoundingBox_getCenter>("ZkOrientedBoundingBox_getCenter");
			ZkOrientedBoundingBox_getAxis =
				loader.LoadFunction<ZkOrientedBoundingBox_getAxis>("ZkOrientedBoundingBox_getAxis");
			ZkOrientedBoundingBox_getHalfWidth =
				loader.LoadFunction<ZkOrientedBoundingBox_getHalfWidth>("ZkOrientedBoundingBox_getHalfWidth");
			ZkOrientedBoundingBox_getChildCount =
				loader.LoadFunction<ZkOrientedBoundingBox_getChildCount>("ZkOrientedBoundingBox_getChildCount");
			ZkOrientedBoundingBox_getChild =
				loader.LoadFunction<ZkOrientedBoundingBox_getChild>("ZkOrientedBoundingBox_getChild");
			ZkOrientedBoundingBox_enumerateChildren =
				loader.LoadFunction<ZkOrientedBoundingBox_enumerateChildren>("ZkOrientedBoundingBox_enumerateChildren");
			ZkOrientedBoundingBox_toAabb =
				loader.LoadFunction<ZkOrientedBoundingBox_toAabb>("ZkOrientedBoundingBox_toAabb");
			ZkMaterial_load = loader.LoadFunction<ZkMaterial_load>("ZkMaterial_load");
			ZkMaterial_loadPath = loader.LoadFunction<ZkMaterial_loadPath>("ZkMaterial_loadPath");
			ZkMaterial_del = loader.LoadFunction<ZkMaterial_del>("ZkMaterial_del");
			ZkMaterial_getName = loader.LoadFunction<ZkMaterial_getName>("ZkMaterial_getName");
			ZkMaterial_getGroup = loader.LoadFunction<ZkMaterial_getGroup>("ZkMaterial_getGroup");
			ZkMaterial_getColor = loader.LoadFunction<ZkMaterial_getColor>("ZkMaterial_getColor");
			ZkMaterial_getSmoothAngle = loader.LoadFunction<ZkMaterial_getSmoothAngle>("ZkMaterial_getSmoothAngle");
			ZkMaterial_getTexture = loader.LoadFunction<ZkMaterial_getTexture>("ZkMaterial_getTexture");
			ZkMaterial_getTextureScale = loader.LoadFunction<ZkMaterial_getTextureScale>("ZkMaterial_getTextureScale");
			ZkMaterial_getTextureAnimationFps =
				loader.LoadFunction<ZkMaterial_getTextureAnimationFps>("ZkMaterial_getTextureAnimationFps");
			ZkMaterial_getTextureAnimationMapping =
				loader.LoadFunction<ZkMaterial_getTextureAnimationMapping>("ZkMaterial_getTextureAnimationMapping");
			ZkMaterial_getTextureAnimationMappingDirection =
				loader.LoadFunction<ZkMaterial_getTextureAnimationMappingDirection>(
					"ZkMaterial_getTextureAnimationMappingDirection");
			ZkMaterial_getDisableCollision =
				loader.LoadFunction<ZkMaterial_getDisableCollision>("ZkMaterial_getDisableCollision");
			ZkMaterial_getDisableLightmap =
				loader.LoadFunction<ZkMaterial_getDisableLightmap>("ZkMaterial_getDisableLightmap");
			ZkMaterial_getDontCollapse = loader.LoadFunction<ZkMaterial_getDontCollapse>("ZkMaterial_getDontCollapse");
			ZkMaterial_getDetailObject = loader.LoadFunction<ZkMaterial_getDetailObject>("ZkMaterial_getDetailObject");
			ZkMaterial_getDetailObjectScale =
				loader.LoadFunction<ZkMaterial_getDetailObjectScale>("ZkMaterial_getDetailObjectScale");
			ZkMaterial_getForceOccluder =
				loader.LoadFunction<ZkMaterial_getForceOccluder>("ZkMaterial_getForceOccluder");
			ZkMaterial_getEnvironmentMapping =
				loader.LoadFunction<ZkMaterial_getEnvironmentMapping>("ZkMaterial_getEnvironmentMapping");
			ZkMaterial_getEnvironmentMappingStrength =
				loader.LoadFunction<ZkMaterial_getEnvironmentMappingStrength>(
					"ZkMaterial_getEnvironmentMappingStrength");
			ZkMaterial_getWaveMode = loader.LoadFunction<ZkMaterial_getWaveMode>("ZkMaterial_getWaveMode");
			ZkMaterial_getWaveSpeed = loader.LoadFunction<ZkMaterial_getWaveSpeed>("ZkMaterial_getWaveSpeed");
			ZkMaterial_getWaveAmplitude =
				loader.LoadFunction<ZkMaterial_getWaveAmplitude>("ZkMaterial_getWaveAmplitude");
			ZkMaterial_getWaveGridSize = loader.LoadFunction<ZkMaterial_getWaveGridSize>("ZkMaterial_getWaveGridSize");
			ZkMaterial_getIgnoreSun = loader.LoadFunction<ZkMaterial_getIgnoreSun>("ZkMaterial_getIgnoreSun");
			ZkMaterial_getAlphaFunction =
				loader.LoadFunction<ZkMaterial_getAlphaFunction>("ZkMaterial_getAlphaFunction");
			ZkMaterial_getDefaultMapping =
				loader.LoadFunction<ZkMaterial_getDefaultMapping>("ZkMaterial_getDefaultMapping");
			ZkMultiResolutionMesh_load = loader.LoadFunction<ZkMultiResolutionMesh_load>("ZkMultiResolutionMesh_load");
			ZkMultiResolutionMesh_loadPath =
				loader.LoadFunction<ZkMultiResolutionMesh_loadPath>("ZkMultiResolutionMesh_loadPath");
			ZkMultiResolutionMesh_loadVfs =
				loader.LoadFunction<ZkMultiResolutionMesh_loadVfs>("ZkMultiResolutionMesh_loadVfs");
			ZkMultiResolutionMesh_del = loader.LoadFunction<ZkMultiResolutionMesh_del>("ZkMultiResolutionMesh_del");
			ZkMultiResolutionMesh_getPositions =
				loader.LoadFunction<ZkMultiResolutionMesh_getPositions>("ZkMultiResolutionMesh_getPositions");
			ZkMultiResolutionMesh_getNormals =
				loader.LoadFunction<ZkMultiResolutionMesh_getNormals>("ZkMultiResolutionMesh_getNormals");
			ZkMultiResolutionMesh_getSubMeshCount =
				loader.LoadFunction<ZkMultiResolutionMesh_getSubMeshCount>("ZkMultiResolutionMesh_getSubMeshCount");
			ZkMultiResolutionMesh_getSubMesh =
				loader.LoadFunction<ZkMultiResolutionMesh_getSubMesh>("ZkMultiResolutionMesh_getSubMesh");
			ZkMultiResolutionMesh_enumerateSubMeshes =
				loader.LoadFunction<ZkMultiResolutionMesh_enumerateSubMeshes>(
					"ZkMultiResolutionMesh_enumerateSubMeshes");
			ZkMultiResolutionMesh_getMaterialCount =
				loader.LoadFunction<ZkMultiResolutionMesh_getMaterialCount>("ZkMultiResolutionMesh_getMaterialCount");
			ZkMultiResolutionMesh_getMaterial =
				loader.LoadFunction<ZkMultiResolutionMesh_getMaterial>("ZkMultiResolutionMesh_getMaterial");
			ZkMultiResolutionMesh_enumerateMaterials =
				loader.LoadFunction<ZkMultiResolutionMesh_enumerateMaterials>(
					"ZkMultiResolutionMesh_enumerateMaterials");
			ZkMultiResolutionMesh_getAlphaTest =
				loader.LoadFunction<ZkMultiResolutionMesh_getAlphaTest>("ZkMultiResolutionMesh_getAlphaTest");
			ZkMultiResolutionMesh_getBbox =
				loader.LoadFunction<ZkMultiResolutionMesh_getBbox>("ZkMultiResolutionMesh_getBbox");
			ZkMultiResolutionMesh_getOrientedBbox =
				loader.LoadFunction<ZkMultiResolutionMesh_getOrientedBbox>("ZkMultiResolutionMesh_getOrientedBbox");
			ZkSubMesh_getMaterial = loader.LoadFunction<ZkSubMesh_getMaterial>("ZkSubMesh_getMaterial");
			ZkSubMesh_getTriangles = loader.LoadFunction<ZkSubMesh_getTriangles>("ZkSubMesh_getTriangles");
			ZkSubMesh_getWedges = loader.LoadFunction<ZkSubMesh_getWedges>("ZkSubMesh_getWedges");
			ZkSubMesh_getColors = loader.LoadFunction<ZkSubMesh_getColors>("ZkSubMesh_getColors");
			ZkSubMesh_getTrianglePlaneIndices =
				loader.LoadFunction<ZkSubMesh_getTrianglePlaneIndices>("ZkSubMesh_getTrianglePlaneIndices");
			ZkSubMesh_getTrianglePlanes =
				loader.LoadFunction<ZkSubMesh_getTrianglePlanes>("ZkSubMesh_getTrianglePlanes");
			ZkSubMesh_getTriangleEdges = loader.LoadFunction<ZkSubMesh_getTriangleEdges>("ZkSubMesh_getTriangleEdges");
			ZkSubMesh_getEdges = loader.LoadFunction<ZkSubMesh_getEdges>("ZkSubMesh_getEdges");
			ZkSubMesh_getEdgeScores = loader.LoadFunction<ZkSubMesh_getEdgeScores>("ZkSubMesh_getEdgeScores");
			ZkSubMesh_getWedgeMap = loader.LoadFunction<ZkSubMesh_getWedgeMap>("ZkSubMesh_getWedgeMap");
			ZkSoftSkinMesh_getNodeCount =
				loader.LoadFunction<ZkSoftSkinMesh_getNodeCount>("ZkSoftSkinMesh_getNodeCount");
			ZkSoftSkinMesh_getMesh = loader.LoadFunction<ZkSoftSkinMesh_getMesh>("ZkSoftSkinMesh_getMesh");
			ZkSoftSkinMesh_getBbox = loader.LoadFunction<ZkSoftSkinMesh_getBbox>("ZkSoftSkinMesh_getBbox");
			ZkSoftSkinMesh_enumerateBoundingBoxes =
				loader.LoadFunction<ZkSoftSkinMesh_enumerateBoundingBoxes>("ZkSoftSkinMesh_enumerateBoundingBoxes");
			ZkSoftSkinMesh_getWeights = loader.LoadFunction<ZkSoftSkinMesh_getWeights>("ZkSoftSkinMesh_getWeights");
			ZkSoftSkinMesh_enumerateWeights =
				loader.LoadFunction<ZkSoftSkinMesh_enumerateWeights>("ZkSoftSkinMesh_enumerateWeights");
			ZkSoftSkinMesh_getWedgeNormals =
				loader.LoadFunction<ZkSoftSkinMesh_getWedgeNormals>("ZkSoftSkinMesh_getWedgeNormals");
			ZkSoftSkinMesh_getNodes = loader.LoadFunction<ZkSoftSkinMesh_getNodes>("ZkSoftSkinMesh_getNodes");
			ZkModelMesh_load = loader.LoadFunction<ZkModelMesh_load>("ZkModelMesh_load");
			ZkModelMesh_loadPath = loader.LoadFunction<ZkModelMesh_loadPath>("ZkModelMesh_loadPath");
			ZkModelMesh_loadVfs = loader.LoadFunction<ZkModelMesh_loadVfs>("ZkModelMesh_loadVfs");
			ZkModelMesh_del = loader.LoadFunction<ZkModelMesh_del>("ZkModelMesh_del");
			ZkModelMesh_getMeshCount = loader.LoadFunction<ZkModelMesh_getMeshCount>("ZkModelMesh_getMeshCount");
			ZkModelMesh_getMesh = loader.LoadFunction<ZkModelMesh_getMesh>("ZkModelMesh_getMesh");
			ZkModelMesh_enumerateMeshes =
				loader.LoadFunction<ZkModelMesh_enumerateMeshes>("ZkModelMesh_enumerateMeshes");
			ZkModelMesh_getAttachmentCount =
				loader.LoadFunction<ZkModelMesh_getAttachmentCount>("ZkModelMesh_getAttachmentCount");
			ZkModelMesh_getAttachment = loader.LoadFunction<ZkModelMesh_getAttachment>("ZkModelMesh_getAttachment");
			ZkModelMesh_enumerateAttachments =
				loader.LoadFunction<ZkModelMesh_enumerateAttachments>("ZkModelMesh_enumerateAttachments");
			ZkModelMesh_getChecksum = loader.LoadFunction<ZkModelMesh_getChecksum>("ZkModelMesh_getChecksum");
			ZkModel_load = loader.LoadFunction<ZkModel_load>("ZkModel_load");
			ZkModel_loadPath = loader.LoadFunction<ZkModel_loadPath>("ZkModel_loadPath");
			ZkModel_loadVfs = loader.LoadFunction<ZkModel_loadVfs>("ZkModel_loadVfs");
			ZkModel_del = loader.LoadFunction<ZkModel_del>("ZkModel_del");
			ZkModel_getHierarchy = loader.LoadFunction<ZkModel_getHierarchy>("ZkModel_getHierarchy");
			ZkModel_getMesh = loader.LoadFunction<ZkModel_getMesh>("ZkModel_getMesh");
			ZkTexture_load = loader.LoadFunction<ZkTexture_load>("ZkTexture_load");
			ZkTexture_loadPath = loader.LoadFunction<ZkTexture_loadPath>("ZkTexture_loadPath");
			ZkTexture_loadVfs = loader.LoadFunction<ZkTexture_loadVfs>("ZkTexture_loadVfs");
			ZkTexture_del = loader.LoadFunction<ZkTexture_del>("ZkTexture_del");
			ZkTexture_getFormat = loader.LoadFunction<ZkTexture_getFormat>("ZkTexture_getFormat");
			ZkTexture_getWidth = loader.LoadFunction<ZkTexture_getWidth>("ZkTexture_getWidth");
			ZkTexture_getHeight = loader.LoadFunction<ZkTexture_getHeight>("ZkTexture_getHeight");
			ZkTexture_getWidthMipmap = loader.LoadFunction<ZkTexture_getWidthMipmap>("ZkTexture_getWidthMipmap");
			ZkTexture_getHeightMipmap = loader.LoadFunction<ZkTexture_getHeightMipmap>("ZkTexture_getHeightMipmap");
			ZkTexture_getWidthRef = loader.LoadFunction<ZkTexture_getWidthRef>("ZkTexture_getWidthRef");
			ZkTexture_getHeightRef = loader.LoadFunction<ZkTexture_getHeightRef>("ZkTexture_getHeightRef");
			ZkTexture_getMipmapCount = loader.LoadFunction<ZkTexture_getMipmapCount>("ZkTexture_getMipmapCount");
			ZkTexture_getAverageColor = loader.LoadFunction<ZkTexture_getAverageColor>("ZkTexture_getAverageColor");
			ZkTexture_getPalette = loader.LoadFunction<ZkTexture_getPalette>("ZkTexture_getPalette");
			ZkTexture_getMipmapRaw = loader.LoadFunction<ZkTexture_getMipmapRaw>("ZkTexture_getMipmapRaw");
			ZkTexture_getMipmapRgba = loader.LoadFunction<ZkTexture_getMipmapRgba>("ZkTexture_getMipmapRgba");
			ZkTexture_enumerateRawMipmaps =
				loader.LoadFunction<ZkTexture_enumerateRawMipmaps>("ZkTexture_enumerateRawMipmaps");
			ZkTexture_enumerateRgbaMipmaps =
				loader.LoadFunction<ZkTexture_enumerateRgbaMipmaps>("ZkTexture_enumerateRgbaMipmaps");
			ZkMorphMesh_load = loader.LoadFunction<ZkMorphMesh_load>("ZkMorphMesh_load");
			ZkMorphMesh_loadPath = loader.LoadFunction<ZkMorphMesh_loadPath>("ZkMorphMesh_loadPath");
			ZkMorphMesh_loadVfs = loader.LoadFunction<ZkMorphMesh_loadVfs>("ZkMorphMesh_loadVfs");
			ZkMorphMesh_del = loader.LoadFunction<ZkMorphMesh_del>("ZkMorphMesh_del");
			ZkMorphMesh_getName = loader.LoadFunction<ZkMorphMesh_getName>("ZkMorphMesh_getName");
			ZkMorphMesh_getMesh = loader.LoadFunction<ZkMorphMesh_getMesh>("ZkMorphMesh_getMesh");
			ZkMorphMesh_getMorphPositions =
				loader.LoadFunction<ZkMorphMesh_getMorphPositions>("ZkMorphMesh_getMorphPositions");
			ZkMorphMesh_getAnimationCount =
				loader.LoadFunction<ZkMorphMesh_getAnimationCount>("ZkMorphMesh_getAnimationCount");
			ZkMorphMesh_getAnimation = loader.LoadFunction<ZkMorphMesh_getAnimation>("ZkMorphMesh_getAnimation");
			ZkMorphMesh_enumerateAnimations =
				loader.LoadFunction<ZkMorphMesh_enumerateAnimations>("ZkMorphMesh_enumerateAnimations");
			ZkMorphMesh_getSourceCount = loader.LoadFunction<ZkMorphMesh_getSourceCount>("ZkMorphMesh_getSourceCount");
			ZkMorphMesh_getSource = loader.LoadFunction<ZkMorphMesh_getSource>("ZkMorphMesh_getSource");
			ZkMorphMesh_enumerateSources =
				loader.LoadFunction<ZkMorphMesh_enumerateSources>("ZkMorphMesh_enumerateSources");
			ZkMorphAnimation_getName = loader.LoadFunction<ZkMorphAnimation_getName>("ZkMorphAnimation_getName");
			ZkMorphAnimation_getLayer = loader.LoadFunction<ZkMorphAnimation_getLayer>("ZkMorphAnimation_getLayer");
			ZkMorphAnimation_getBlendIn =
				loader.LoadFunction<ZkMorphAnimation_getBlendIn>("ZkMorphAnimation_getBlendIn");
			ZkMorphAnimation_getBlendOut =
				loader.LoadFunction<ZkMorphAnimation_getBlendOut>("ZkMorphAnimation_getBlendOut");
			ZkMorphAnimation_getDuration =
				loader.LoadFunction<ZkMorphAnimation_getDuration>("ZkMorphAnimation_getDuration");
			ZkMorphAnimation_getSpeed = loader.LoadFunction<ZkMorphAnimation_getSpeed>("ZkMorphAnimation_getSpeed");
			ZkMorphAnimation_getFlags = loader.LoadFunction<ZkMorphAnimation_getFlags>("ZkMorphAnimation_getFlags");
			ZkMorphAnimation_getFrameCount =
				loader.LoadFunction<ZkMorphAnimation_getFrameCount>("ZkMorphAnimation_getFrameCount");
			ZkMorphAnimation_getVertices =
				loader.LoadFunction<ZkMorphAnimation_getVertices>("ZkMorphAnimation_getVertices");
			ZkMorphAnimation_getSamples =
				loader.LoadFunction<ZkMorphAnimation_getSamples>("ZkMorphAnimation_getSamples");
			ZkMorphSource_getFileName = loader.LoadFunction<ZkMorphSource_getFileName>("ZkMorphSource_getFileName");
			ZkMorphSource_getFileDate = loader.LoadFunction<ZkMorphSource_getFileDate>("ZkMorphSource_getFileDate");
			ZkMesh_load = loader.LoadFunction<ZkMesh_load>("ZkMesh_load");
			ZkMesh_loadPath = loader.LoadFunction<ZkMesh_loadPath>("ZkMesh_loadPath");
			ZkMesh_loadVfs = loader.LoadFunction<ZkMesh_loadVfs>("ZkMesh_loadVfs");
			ZkMesh_del = loader.LoadFunction<ZkMesh_del>("ZkMesh_del");
			ZkMesh_getSourceDate = loader.LoadFunction<ZkMesh_getSourceDate>("ZkMesh_getSourceDate");
			ZkMesh_getName = loader.LoadFunction<ZkMesh_getName>("ZkMesh_getName");
			ZkMesh_getBoundingBox = loader.LoadFunction<ZkMesh_getBoundingBox>("ZkMesh_getBoundingBox");
			ZkMesh_getOrientedBoundingBox =
				loader.LoadFunction<ZkMesh_getOrientedBoundingBox>("ZkMesh_getOrientedBoundingBox");
			ZkMesh_getMaterialCount = loader.LoadFunction<ZkMesh_getMaterialCount>("ZkMesh_getMaterialCount");
			ZkMesh_getMaterial = loader.LoadFunction<ZkMesh_getMaterial>("ZkMesh_getMaterial");
			ZkMesh_enumerateMaterials = loader.LoadFunction<ZkMesh_enumerateMaterials>("ZkMesh_enumerateMaterials");
			ZkMesh_getPositions = loader.LoadFunction<ZkMesh_getPositions>("ZkMesh_getPositions");
			ZkMesh_getVertices = loader.LoadFunction<ZkMesh_getVertices>("ZkMesh_getVertices");
			ZkMesh_getLightMapCount = loader.LoadFunction<ZkMesh_getLightMapCount>("ZkMesh_getLightMapCount");
			ZkMesh_getLightMap = loader.LoadFunction<ZkMesh_getLightMap>("ZkMesh_getLightMap");
			ZkMesh_enumerateLightMaps = loader.LoadFunction<ZkMesh_enumerateLightMaps>("ZkMesh_enumerateLightMaps");
			ZkMesh_getPolygonCount = loader.LoadFunction<ZkMesh_getPolygonCount>("ZkMesh_getPolygonCount");
			ZkMesh_getPolygon = loader.LoadFunction<ZkMesh_getPolygon>("ZkMesh_getPolygon");
			ZkMesh_enumeratePolygons = loader.LoadFunction<ZkMesh_enumeratePolygons>("ZkMesh_enumeratePolygons");
			ZkLightMap_getImage = loader.LoadFunction<ZkLightMap_getImage>("ZkLightMap_getImage");
			ZkLightMap_getOrigin = loader.LoadFunction<ZkLightMap_getOrigin>("ZkLightMap_getOrigin");
			ZkLightMap_getNormal = loader.LoadFunction<ZkLightMap_getNormal>("ZkLightMap_getNormal");
			ZkPolygon_getMaterialIndex = loader.LoadFunction<ZkPolygon_getMaterialIndex>("ZkPolygon_getMaterialIndex");
			ZkPolygon_getLightMapIndex = loader.LoadFunction<ZkPolygon_getLightMapIndex>("ZkPolygon_getLightMapIndex");
			ZkPolygon_getPositionIndices =
				loader.LoadFunction<ZkPolygon_getPositionIndices>("ZkPolygon_getPositionIndices");
			ZkPolygon_getPolygonIndices =
				loader.LoadFunction<ZkPolygon_getPolygonIndices>("ZkPolygon_getPolygonIndices");
			ZkPolygon_getIsPortal = loader.LoadFunction<ZkPolygon_getIsPortal>("ZkPolygon_getIsPortal");
			ZkPolygon_getIsOccluder = loader.LoadFunction<ZkPolygon_getIsOccluder>("ZkPolygon_getIsOccluder");
			ZkPolygon_getIsSector = loader.LoadFunction<ZkPolygon_getIsSector>("ZkPolygon_getIsSector");
			ZkPolygon_getShouldRelight = loader.LoadFunction<ZkPolygon_getShouldRelight>("ZkPolygon_getShouldRelight");
			ZkPolygon_getIsOutdoor = loader.LoadFunction<ZkPolygon_getIsOutdoor>("ZkPolygon_getIsOutdoor");
			ZkPolygon_getIsGhostOccluder =
				loader.LoadFunction<ZkPolygon_getIsGhostOccluder>("ZkPolygon_getIsGhostOccluder");
			ZkPolygon_getIsDynamicallyLit =
				loader.LoadFunction<ZkPolygon_getIsDynamicallyLit>("ZkPolygon_getIsDynamicallyLit");
			ZkPolygon_getIsLod = loader.LoadFunction<ZkPolygon_getIsLod>("ZkPolygon_getIsLod");
			ZkPolygon_getNormalAxis = loader.LoadFunction<ZkPolygon_getNormalAxis>("ZkPolygon_getNormalAxis");
			ZkPolygon_getSectorIndex = loader.LoadFunction<ZkPolygon_getSectorIndex>("ZkPolygon_getSectorIndex");
			ZkModelScript_load = loader.LoadFunction<ZkModelScript_load>("ZkModelScript_load");
			ZkModelScript_loadPath = loader.LoadFunction<ZkModelScript_loadPath>("ZkModelScript_loadPath");
			ZkModelScript_loadVfs = loader.LoadFunction<ZkModelScript_loadVfs>("ZkModelScript_loadVfs");
			ZkModelScript_del = loader.LoadFunction<ZkModelScript_del>("ZkModelScript_del");
			ZkModelScript_getSkeletonName =
				loader.LoadFunction<ZkModelScript_getSkeletonName>("ZkModelScript_getSkeletonName");
			ZkModelScript_getSkeletonMeshDisabled =
				loader.LoadFunction<ZkModelScript_getSkeletonMeshDisabled>("ZkModelScript_getSkeletonMeshDisabled");
			ZkModelScript_getMeshCount = loader.LoadFunction<ZkModelScript_getMeshCount>("ZkModelScript_getMeshCount");
			ZkModelScript_getDisabledAnimationsCount =
				loader.LoadFunction<ZkModelScript_getDisabledAnimationsCount>(
					"ZkModelScript_getDisabledAnimationsCount");
			ZkModelScript_getAnimationCombineCount =
				loader.LoadFunction<ZkModelScript_getAnimationCombineCount>("ZkModelScript_getAnimationCombineCount");
			ZkModelScript_getAnimationBlendCount =
				loader.LoadFunction<ZkModelScript_getAnimationBlendCount>("ZkModelScript_getAnimationBlendCount");
			ZkModelScript_getAnimationAliasCount =
				loader.LoadFunction<ZkModelScript_getAnimationAliasCount>("ZkModelScript_getAnimationAliasCount");
			ZkModelScript_getModelTagCount =
				loader.LoadFunction<ZkModelScript_getModelTagCount>("ZkModelScript_getModelTagCount");
			ZkModelScript_getAnimationCount =
				loader.LoadFunction<ZkModelScript_getAnimationCount>("ZkModelScript_getAnimationCount");
			ZkModelScript_getDisabledAnimation =
				loader.LoadFunction<ZkModelScript_getDisabledAnimation>("ZkModelScript_getDisabledAnimation");
			ZkModelScript_getMesh = loader.LoadFunction<ZkModelScript_getMesh>("ZkModelScript_getMesh");
			ZkModelScript_getAnimationCombine =
				loader.LoadFunction<ZkModelScript_getAnimationCombine>("ZkModelScript_getAnimationCombine");
			ZkModelScript_getAnimationBlend =
				loader.LoadFunction<ZkModelScript_getAnimationBlend>("ZkModelScript_getAnimationBlend");
			ZkModelScript_getAnimationAlias =
				loader.LoadFunction<ZkModelScript_getAnimationAlias>("ZkModelScript_getAnimationAlias");
			ZkModelScript_getModelTag = loader.LoadFunction<ZkModelScript_getModelTag>("ZkModelScript_getModelTag");
			ZkModelScript_getAnimation = loader.LoadFunction<ZkModelScript_getAnimation>("ZkModelScript_getAnimation");
			ZkModelScript_enumerateAnimationCombines =
				loader.LoadFunction<ZkModelScript_enumerateAnimationCombines>(
					"ZkModelScript_enumerateAnimationCombines");
			ZkModelScript_enumerateMeshes =
				loader.LoadFunction<ZkModelScript_enumerateMeshes>("ZkModelScript_enumerateMeshes");
			ZkModelScript_enumerateDisabledAnimations =
				loader.LoadFunction<ZkModelScript_enumerateDisabledAnimations>(
					"ZkModelScript_enumerateDisabledAnimations");
			ZkModelScript_enumerateAnimationBlends =
				loader.LoadFunction<ZkModelScript_enumerateAnimationBlends>("ZkModelScript_enumerateAnimationBlends");
			ZkModelScript_enumerateAnimationAliases =
				loader.LoadFunction<ZkModelScript_enumerateAnimationAliases>("ZkModelScript_enumerateAnimationAliases");
			ZkModelScript_enumerateModelTags =
				loader.LoadFunction<ZkModelScript_enumerateModelTags>("ZkModelScript_enumerateModelTags");
			ZkModelScript_enumerateAnimations =
				loader.LoadFunction<ZkModelScript_enumerateAnimations>("ZkModelScript_enumerateAnimations");
			ZkAnimation_getName = loader.LoadFunction<ZkAnimation_getName>("ZkAnimation_getName");
			ZkAnimation_getLayer = loader.LoadFunction<ZkAnimation_getLayer>("ZkAnimation_getLayer");
			ZkAnimation_getNext = loader.LoadFunction<ZkAnimation_getNext>("ZkAnimation_getNext");
			ZkAnimation_getBlendIn = loader.LoadFunction<ZkAnimation_getBlendIn>("ZkAnimation_getBlendIn");
			ZkAnimation_getBlendOut = loader.LoadFunction<ZkAnimation_getBlendOut>("ZkAnimation_getBlendOut");
			ZkAnimation_getFlags = loader.LoadFunction<ZkAnimation_getFlags>("ZkAnimation_getFlags");
			ZkAnimation_getModel = loader.LoadFunction<ZkAnimation_getModel>("ZkAnimation_getModel");
			ZkAnimation_getDirection = loader.LoadFunction<ZkAnimation_getDirection>("ZkAnimation_getDirection");
			ZkAnimation_getFirstFrame = loader.LoadFunction<ZkAnimation_getFirstFrame>("ZkAnimation_getFirstFrame");
			ZkAnimation_getLastFrame = loader.LoadFunction<ZkAnimation_getLastFrame>("ZkAnimation_getLastFrame");
			ZkAnimation_getCollisionVolumeScale =
				loader.LoadFunction<ZkAnimation_getCollisionVolumeScale>("ZkAnimation_getCollisionVolumeScale");
			ZkAnimation_getEventTagCount =
				loader.LoadFunction<ZkAnimation_getEventTagCount>("ZkAnimation_getEventTagCount");
			ZkAnimation_getParticleEffectCount =
				loader.LoadFunction<ZkAnimation_getParticleEffectCount>("ZkAnimation_getParticleEffectCount");
			ZkAnimation_getParticleEffectStopCount =
				loader.LoadFunction<ZkAnimation_getParticleEffectStopCount>("ZkAnimation_getParticleEffectStopCount");
			ZkAnimation_getSoundEffectCount =
				loader.LoadFunction<ZkAnimation_getSoundEffectCount>("ZkAnimation_getSoundEffectCount");
			ZkAnimation_getSoundEffectGroundCount =
				loader.LoadFunction<ZkAnimation_getSoundEffectGroundCount>("ZkAnimation_getSoundEffectGroundCount");
			ZkAnimation_getMorphAnimationCount =
				loader.LoadFunction<ZkAnimation_getMorphAnimationCount>("ZkAnimation_getMorphAnimationCount");
			ZkAnimation_getCameraTremorCount =
				loader.LoadFunction<ZkAnimation_getCameraTremorCount>("ZkAnimation_getCameraTremorCount");
			ZkAnimation_getEventTag = loader.LoadFunction<ZkAnimation_getEventTag>("ZkAnimation_getEventTag");
			ZkAnimation_getParticleEffect =
				loader.LoadFunction<ZkAnimation_getParticleEffect>("ZkAnimation_getParticleEffect");
			ZkAnimation_getParticleEffectStop =
				loader.LoadFunction<ZkAnimation_getParticleEffectStop>("ZkAnimation_getParticleEffectStop");
			ZkAnimation_getSoundEffect = loader.LoadFunction<ZkAnimation_getSoundEffect>("ZkAnimation_getSoundEffect");
			ZkAnimation_getSoundEffectGround =
				loader.LoadFunction<ZkAnimation_getSoundEffectGround>("ZkAnimation_getSoundEffectGround");
			ZkAnimation_getMorphAnimation =
				loader.LoadFunction<ZkAnimation_getMorphAnimation>("ZkAnimation_getMorphAnimation");
			ZkAnimation_getCameraTremor =
				loader.LoadFunction<ZkAnimation_getCameraTremor>("ZkAnimation_getCameraTremor");
			ZkAnimation_enumerateEventTags =
				loader.LoadFunction<ZkAnimation_enumerateEventTags>("ZkAnimation_enumerateEventTags");
			ZkAnimation_enumerateParticleEffects =
				loader.LoadFunction<ZkAnimation_enumerateParticleEffects>("ZkAnimation_enumerateParticleEffects");
			ZkAnimation_enumerateParticleEffectStops =
				loader.LoadFunction<ZkAnimation_enumerateParticleEffectStops>(
					"ZkAnimation_enumerateParticleEffectStops");
			ZkAnimation_enumerateSoundEffects =
				loader.LoadFunction<ZkAnimation_enumerateSoundEffects>("ZkAnimation_enumerateSoundEffects");
			ZkAnimation_enumerateSoundEffectGrounds =
				loader.LoadFunction<ZkAnimation_enumerateSoundEffectGrounds>("ZkAnimation_enumerateSoundEffectGrounds");
			ZkAnimation_enumerateMorphAnimations =
				loader.LoadFunction<ZkAnimation_enumerateMorphAnimations>("ZkAnimation_enumerateMorphAnimations");
			ZkAnimation_enumerateCameraTremors =
				loader.LoadFunction<ZkAnimation_enumerateCameraTremors>("ZkAnimation_enumerateCameraTremors");
			ZkEventTag_getFrame = loader.LoadFunction<ZkEventTag_getFrame>("ZkEventTag_getFrame");
			ZkEventTag_getType = loader.LoadFunction<ZkEventTag_getType>("ZkEventTag_getType");
			ZkEventTag_getSlot = loader.LoadFunction<ZkEventTag_getSlot>("ZkEventTag_getSlot");
			ZkEventTag_getItem = loader.LoadFunction<ZkEventTag_getItem>("ZkEventTag_getItem");
			ZkEventTag_getFrames = loader.LoadFunction<ZkEventTag_getFrames>("ZkEventTag_getFrames");
			ZkEventTag_getFightMode = loader.LoadFunction<ZkEventTag_getFightMode>("ZkEventTag_getFightMode");
			ZkEventTag_getIsAttached = loader.LoadFunction<ZkEventTag_getIsAttached>("ZkEventTag_getIsAttached");
			ZkEventParticleEffect_getFrame =
				loader.LoadFunction<ZkEventParticleEffect_getFrame>("ZkEventParticleEffect_getFrame");
			ZkEventParticleEffect_getIndex =
				loader.LoadFunction<ZkEventParticleEffect_getIndex>("ZkEventParticleEffect_getIndex");
			ZkEventParticleEffect_getName =
				loader.LoadFunction<ZkEventParticleEffect_getName>("ZkEventParticleEffect_getName");
			ZkEventParticleEffect_getPosition =
				loader.LoadFunction<ZkEventParticleEffect_getPosition>("ZkEventParticleEffect_getPosition");
			ZkEventParticleEffect_getIsAttached =
				loader.LoadFunction<ZkEventParticleEffect_getIsAttached>("ZkEventParticleEffect_getIsAttached");
			ZkEventParticleEffectStop_getFrame =
				loader.LoadFunction<ZkEventParticleEffectStop_getFrame>("ZkEventParticleEffectStop_getFrame");
			ZkEventParticleEffectStop_getIndex =
				loader.LoadFunction<ZkEventParticleEffectStop_getIndex>("ZkEventParticleEffectStop_getIndex");
			ZkEventCameraTremor_getFrame =
				loader.LoadFunction<ZkEventCameraTremor_getFrame>("ZkEventCameraTremor_getFrame");
			ZkEventCameraTremor_getField1 =
				loader.LoadFunction<ZkEventCameraTremor_getField1>("ZkEventCameraTremor_getField1");
			ZkEventCameraTremor_getField2 =
				loader.LoadFunction<ZkEventCameraTremor_getField2>("ZkEventCameraTremor_getField2");
			ZkEventCameraTremor_getField3 =
				loader.LoadFunction<ZkEventCameraTremor_getField3>("ZkEventCameraTremor_getField3");
			ZkEventCameraTremor_getField4 =
				loader.LoadFunction<ZkEventCameraTremor_getField4>("ZkEventCameraTremor_getField4");
			ZkEventSoundEffect_getFrame =
				loader.LoadFunction<ZkEventSoundEffect_getFrame>("ZkEventSoundEffect_getFrame");
			ZkEventSoundEffect_getName = loader.LoadFunction<ZkEventSoundEffect_getName>("ZkEventSoundEffect_getName");
			ZkEventSoundEffect_getRange =
				loader.LoadFunction<ZkEventSoundEffect_getRange>("ZkEventSoundEffect_getRange");
			ZkEventSoundEffect_getEmptySlot =
				loader.LoadFunction<ZkEventSoundEffect_getEmptySlot>("ZkEventSoundEffect_getEmptySlot");
			ZkEventSoundEffectGround_getFrame =
				loader.LoadFunction<ZkEventSoundEffectGround_getFrame>("ZkEventSoundEffectGround_getFrame");
			ZkEventSoundEffectGround_getName =
				loader.LoadFunction<ZkEventSoundEffectGround_getName>("ZkEventSoundEffectGround_getName");
			ZkEventSoundEffectGround_getRange =
				loader.LoadFunction<ZkEventSoundEffectGround_getRange>("ZkEventSoundEffectGround_getRange");
			ZkEventSoundEffectGround_getEmptySlot =
				loader.LoadFunction<ZkEventSoundEffectGround_getEmptySlot>("ZkEventSoundEffectGround_getEmptySlot");
			ZkMorphAnimation_getFrame = loader.LoadFunction<ZkMorphAnimation_getFrame>("ZkMorphAnimation_getFrame");
			ZkMorphAnimation_getAnimation =
				loader.LoadFunction<ZkMorphAnimation_getAnimation>("ZkMorphAnimation_getAnimation");
			ZkMorphAnimation_getNode = loader.LoadFunction<ZkMorphAnimation_getNode>("ZkMorphAnimation_getNode");
			ZkAnimationCombine_getName = loader.LoadFunction<ZkAnimationCombine_getName>("ZkAnimationCombine_getName");
			ZkAnimationCombine_getLayer =
				loader.LoadFunction<ZkAnimationCombine_getLayer>("ZkAnimationCombine_getLayer");
			ZkAnimationCombine_getNext = loader.LoadFunction<ZkAnimationCombine_getNext>("ZkAnimationCombine_getNext");
			ZkAnimationCombine_getBlendIn =
				loader.LoadFunction<ZkAnimationCombine_getBlendIn>("ZkAnimationCombine_getBlendIn");
			ZkAnimationCombine_getBlendOut =
				loader.LoadFunction<ZkAnimationCombine_getBlendOut>("ZkAnimationCombine_getBlendOut");
			ZkAnimationCombine_getFlags =
				loader.LoadFunction<ZkAnimationCombine_getFlags>("ZkAnimationCombine_getFlags");
			ZkAnimationCombine_getModel =
				loader.LoadFunction<ZkAnimationCombine_getModel>("ZkAnimationCombine_getModel");
			ZkAnimationCombine_getLastFrame =
				loader.LoadFunction<ZkAnimationCombine_getLastFrame>("ZkAnimationCombine_getLastFrame");
			ZkAnimationBlend_getName = loader.LoadFunction<ZkAnimationBlend_getName>("ZkAnimationBlend_getName");
			ZkAnimationBlend_getNext = loader.LoadFunction<ZkAnimationBlend_getNext>("ZkAnimationBlend_getNext");
			ZkAnimationBlend_getBlendIn =
				loader.LoadFunction<ZkAnimationBlend_getBlendIn>("ZkAnimationBlend_getBlendIn");
			ZkAnimationBlend_getBlendOut =
				loader.LoadFunction<ZkAnimationBlend_getBlendOut>("ZkAnimationBlend_getBlendOut");
			ZkAnimationAlias_getName = loader.LoadFunction<ZkAnimationAlias_getName>("ZkAnimationAlias_getName");
			ZkAnimationAlias_getLayer = loader.LoadFunction<ZkAnimationAlias_getLayer>("ZkAnimationAlias_getLayer");
			ZkAnimationAlias_getNext = loader.LoadFunction<ZkAnimationAlias_getNext>("ZkAnimationAlias_getNext");
			ZkAnimationAlias_getBlendIn =
				loader.LoadFunction<ZkAnimationAlias_getBlendIn>("ZkAnimationAlias_getBlendIn");
			ZkAnimationAlias_getBlendOut =
				loader.LoadFunction<ZkAnimationAlias_getBlendOut>("ZkAnimationAlias_getBlendOut");
			ZkAnimationAlias_getFlags = loader.LoadFunction<ZkAnimationAlias_getFlags>("ZkAnimationAlias_getFlags");
			ZkAnimationAlias_getAlias = loader.LoadFunction<ZkAnimationAlias_getAlias>("ZkAnimationAlias_getAlias");
			ZkAnimationAlias_getDirection =
				loader.LoadFunction<ZkAnimationAlias_getDirection>("ZkAnimationAlias_getDirection");
			ZkBspTree_getType = loader.LoadFunction<ZkBspTree_getType>("ZkBspTree_getType");
			ZkBspTree_getPolygonIndices =
				loader.LoadFunction<ZkBspTree_getPolygonIndices>("ZkBspTree_getPolygonIndices");
			ZkBspTree_getLeafPolygonIndices =
				loader.LoadFunction<ZkBspTree_getLeafPolygonIndices>("ZkBspTree_getLeafPolygonIndices");
			ZkBspTree_getPortalPolygonIndices =
				loader.LoadFunction<ZkBspTree_getPortalPolygonIndices>("ZkBspTree_getPortalPolygonIndices");
			ZkBspTree_getLightPoints = loader.LoadFunction<ZkBspTree_getLightPoints>("ZkBspTree_getLightPoints");
			ZkBspTree_getLeafNodeIndices =
				loader.LoadFunction<ZkBspTree_getLeafNodeIndices>("ZkBspTree_getLeafNodeIndices");
			ZkBspTree_getNodes = loader.LoadFunction<ZkBspTree_getNodes>("ZkBspTree_getNodes");
			ZkBspTree_getSectorCount = loader.LoadFunction<ZkBspTree_getSectorCount>("ZkBspTree_getSectorCount");
			ZkBspTree_getSector = loader.LoadFunction<ZkBspTree_getSector>("ZkBspTree_getSector");
			ZkBspTree_enumerateSectors = loader.LoadFunction<ZkBspTree_enumerateSectors>("ZkBspTree_enumerateSectors");
			ZkBspSector_getName = loader.LoadFunction<ZkBspSector_getName>("ZkBspSector_getName");
			ZkBspSector_getNodeIndices = loader.LoadFunction<ZkBspSector_getNodeIndices>("ZkBspSector_getNodeIndices");
			ZkBspSector_getPortalPolygonIndices =
				loader.LoadFunction<ZkBspSector_getPortalPolygonIndices>("ZkBspSector_getPortalPolygonIndices");
			ZkWayNet_getEdges = loader.LoadFunction<ZkWayNet_getEdges>("ZkWayNet_getEdges");
			ZkWayNet_getPointCount = loader.LoadFunction<ZkWayNet_getPointCount>("ZkWayNet_getPointCount");
			ZkWayNet_getPoint = loader.LoadFunction<ZkWayNet_getPoint>("ZkWayNet_getPoint");
			ZkWayNet_enumeratePoints = loader.LoadFunction<ZkWayNet_enumeratePoints>("ZkWayNet_enumeratePoints");
			ZkWayPoint_getName = loader.LoadFunction<ZkWayPoint_getName>("ZkWayPoint_getName");
			ZkWayPoint_getWaterDepth = loader.LoadFunction<ZkWayPoint_getWaterDepth>("ZkWayPoint_getWaterDepth");
			ZkWayPoint_getUnderWater = loader.LoadFunction<ZkWayPoint_getUnderWater>("ZkWayPoint_getUnderWater");
			ZkWayPoint_getPosition = loader.LoadFunction<ZkWayPoint_getPosition>("ZkWayPoint_getPosition");
			ZkWayPoint_getDirection = loader.LoadFunction<ZkWayPoint_getDirection>("ZkWayPoint_getDirection");
			ZkWayPoint_getFreePoint = loader.LoadFunction<ZkWayPoint_getFreePoint>("ZkWayPoint_getFreePoint");
			ZkWorld_load = loader.LoadFunction<ZkWorld_load>("ZkWorld_load");
			ZkWorld_loadPath = loader.LoadFunction<ZkWorld_loadPath>("ZkWorld_loadPath");
			ZkWorld_loadVfs = loader.LoadFunction<ZkWorld_loadVfs>("ZkWorld_loadVfs");
			ZkWorld_del = loader.LoadFunction<ZkWorld_del>("ZkWorld_del");
			ZkWorld_getMesh = loader.LoadFunction<ZkWorld_getMesh>("ZkWorld_getMesh");
			ZkWorld_getBspTree = loader.LoadFunction<ZkWorld_getBspTree>("ZkWorld_getBspTree");
			ZkWorld_getWayNet = loader.LoadFunction<ZkWorld_getWayNet>("ZkWorld_getWayNet");
			ZkWorld_getRootObjectCount = loader.LoadFunction<ZkWorld_getRootObjectCount>("ZkWorld_getRootObjectCount");
			ZkWorld_getRootObject = loader.LoadFunction<ZkWorld_getRootObject>("ZkWorld_getRootObject");
			ZkWorld_enumerateRootObjects =
				loader.LoadFunction<ZkWorld_enumerateRootObjects>("ZkWorld_enumerateRootObjects");
			ZkVirtualObject_load = loader.LoadFunction<ZkVirtualObject_load>("ZkVirtualObject_load");
			ZkVirtualObject_loadPath = loader.LoadFunction<ZkVirtualObject_loadPath>("ZkVirtualObject_loadPath");
			ZkVirtualObject_del = loader.LoadFunction<ZkVirtualObject_del>("ZkVirtualObject_del");
			ZkVirtualObject_getType = loader.LoadFunction<ZkVirtualObject_getType>("ZkVirtualObject_getType");
			ZkVirtualObject_getId = loader.LoadFunction<ZkVirtualObject_getId>("ZkVirtualObject_getId");
			ZkVirtualObject_getBbox = loader.LoadFunction<ZkVirtualObject_getBbox>("ZkVirtualObject_getBbox");
			ZkVirtualObject_getPosition =
				loader.LoadFunction<ZkVirtualObject_getPosition>("ZkVirtualObject_getPosition");
			ZkVirtualObject_getRotation =
				loader.LoadFunction<ZkVirtualObject_getRotation>("ZkVirtualObject_getRotation");
			ZkVirtualObject_getShowVisual =
				loader.LoadFunction<ZkVirtualObject_getShowVisual>("ZkVirtualObject_getShowVisual");
			ZkVirtualObject_getSpriteCameraFacingMode =
				loader.LoadFunction<ZkVirtualObject_getSpriteCameraFacingMode>(
					"ZkVirtualObject_getSpriteCameraFacingMode");
			ZkVirtualObject_getCdStatic =
				loader.LoadFunction<ZkVirtualObject_getCdStatic>("ZkVirtualObject_getCdStatic");
			ZkVirtualObject_getCdDynamic =
				loader.LoadFunction<ZkVirtualObject_getCdDynamic>("ZkVirtualObject_getCdDynamic");
			ZkVirtualObject_getVobStatic =
				loader.LoadFunction<ZkVirtualObject_getVobStatic>("ZkVirtualObject_getVobStatic");
			ZkVirtualObject_getDynamicShadows =
				loader.LoadFunction<ZkVirtualObject_getDynamicShadows>("ZkVirtualObject_getDynamicShadows");
			ZkVirtualObject_getPhysicsEnabled =
				loader.LoadFunction<ZkVirtualObject_getPhysicsEnabled>("ZkVirtualObject_getPhysicsEnabled");
			ZkVirtualObject_getAnimMode =
				loader.LoadFunction<ZkVirtualObject_getAnimMode>("ZkVirtualObject_getAnimMode");
			ZkVirtualObject_getBias = loader.LoadFunction<ZkVirtualObject_getBias>("ZkVirtualObject_getBias");
			ZkVirtualObject_getAmbient = loader.LoadFunction<ZkVirtualObject_getAmbient>("ZkVirtualObject_getAmbient");
			ZkVirtualObject_getAnimStrength =
				loader.LoadFunction<ZkVirtualObject_getAnimStrength>("ZkVirtualObject_getAnimStrength");
			ZkVirtualObject_getFarClipScale =
				loader.LoadFunction<ZkVirtualObject_getFarClipScale>("ZkVirtualObject_getFarClipScale");
			ZkVirtualObject_getPresetName =
				loader.LoadFunction<ZkVirtualObject_getPresetName>("ZkVirtualObject_getPresetName");
			ZkVirtualObject_getName = loader.LoadFunction<ZkVirtualObject_getName>("ZkVirtualObject_getName");
			ZkVirtualObject_getVisualName =
				loader.LoadFunction<ZkVirtualObject_getVisualName>("ZkVirtualObject_getVisualName");
			ZkVirtualObject_getVisualType =
				loader.LoadFunction<ZkVirtualObject_getVisualType>("ZkVirtualObject_getVisualType");
			ZkVirtualObject_getVisualDecal =
				loader.LoadFunction<ZkVirtualObject_getVisualDecal>("ZkVirtualObject_getVisualDecal");
			ZkVirtualObject_getChildCount =
				loader.LoadFunction<ZkVirtualObject_getChildCount>("ZkVirtualObject_getChildCount");
			ZkVirtualObject_getChild = loader.LoadFunction<ZkVirtualObject_getChild>("ZkVirtualObject_getChild");
			ZkVirtualObject_enumerateChildren =
				loader.LoadFunction<ZkVirtualObject_enumerateChildren>("ZkVirtualObject_enumerateChildren");
			ZkDecal_getName = loader.LoadFunction<ZkDecal_getName>("ZkDecal_getName");
			ZkDecal_getDimension = loader.LoadFunction<ZkDecal_getDimension>("ZkDecal_getDimension");
			ZkDecal_getOffset = loader.LoadFunction<ZkDecal_getOffset>("ZkDecal_getOffset");
			ZkDecal_getTwoSided = loader.LoadFunction<ZkDecal_getTwoSided>("ZkDecal_getTwoSided");
			ZkDecal_getAlphaFunc = loader.LoadFunction<ZkDecal_getAlphaFunc>("ZkDecal_getAlphaFunc");
			ZkDecal_getTextureAnimFps = loader.LoadFunction<ZkDecal_getTextureAnimFps>("ZkDecal_getTextureAnimFps");
			ZkDecal_getAlphaWeight = loader.LoadFunction<ZkDecal_getAlphaWeight>("ZkDecal_getAlphaWeight");
			ZkDecal_getIgnoreDaylight = loader.LoadFunction<ZkDecal_getIgnoreDaylight>("ZkDecal_getIgnoreDaylight");
			ZkCutsceneCamera_load = loader.LoadFunction<ZkCutsceneCamera_load>("ZkCutsceneCamera_load");
			ZkCutsceneCamera_loadPath = loader.LoadFunction<ZkCutsceneCamera_loadPath>("ZkCutsceneCamera_loadPath");
			ZkCutsceneCamera_del = loader.LoadFunction<ZkCutsceneCamera_del>("ZkCutsceneCamera_del");
			ZkCutsceneCamera_getTrajectoryFOR =
				loader.LoadFunction<ZkCutsceneCamera_getTrajectoryFOR>("ZkCutsceneCamera_getTrajectoryFOR");
			ZkCutsceneCamera_getTargetTrajectoryFOR =
				loader.LoadFunction<ZkCutsceneCamera_getTargetTrajectoryFOR>("ZkCutsceneCamera_getTargetTrajectoryFOR");
			ZkCutsceneCamera_getLoopMode =
				loader.LoadFunction<ZkCutsceneCamera_getLoopMode>("ZkCutsceneCamera_getLoopMode");
			ZkCutsceneCamera_getLerpMode =
				loader.LoadFunction<ZkCutsceneCamera_getLerpMode>("ZkCutsceneCamera_getLerpMode");
			ZkCutsceneCamera_getIgnoreFORVobRotation =
				loader.LoadFunction<ZkCutsceneCamera_getIgnoreFORVobRotation>(
					"ZkCutsceneCamera_getIgnoreFORVobRotation");
			ZkCutsceneCamera_getIgnoreFORVobRotationTarget =
				loader.LoadFunction<ZkCutsceneCamera_getIgnoreFORVobRotationTarget>(
					"ZkCutsceneCamera_getIgnoreFORVobRotationTarget");
			ZkCutsceneCamera_getAdapt = loader.LoadFunction<ZkCutsceneCamera_getAdapt>("ZkCutsceneCamera_getAdapt");
			ZkCutsceneCamera_getEaseFirst =
				loader.LoadFunction<ZkCutsceneCamera_getEaseFirst>("ZkCutsceneCamera_getEaseFirst");
			ZkCutsceneCamera_getEaseLast =
				loader.LoadFunction<ZkCutsceneCamera_getEaseLast>("ZkCutsceneCamera_getEaseLast");
			ZkCutsceneCamera_getTotalDuration =
				loader.LoadFunction<ZkCutsceneCamera_getTotalDuration>("ZkCutsceneCamera_getTotalDuration");
			ZkCutsceneCamera_getAutoFocusVob =
				loader.LoadFunction<ZkCutsceneCamera_getAutoFocusVob>("ZkCutsceneCamera_getAutoFocusVob");
			ZkCutsceneCamera_getAutoPlayerMovable =
				loader.LoadFunction<ZkCutsceneCamera_getAutoPlayerMovable>("ZkCutsceneCamera_getAutoPlayerMovable");
			ZkCutsceneCamera_getAutoUntriggerLast =
				loader.LoadFunction<ZkCutsceneCamera_getAutoUntriggerLast>("ZkCutsceneCamera_getAutoUntriggerLast");
			ZkCutsceneCamera_getAutoUntriggerLastDelay =
				loader.LoadFunction<ZkCutsceneCamera_getAutoUntriggerLastDelay>(
					"ZkCutsceneCamera_getAutoUntriggerLastDelay");
			ZkCutsceneCamera_getPositionCount =
				loader.LoadFunction<ZkCutsceneCamera_getPositionCount>("ZkCutsceneCamera_getPositionCount");
			ZkCutsceneCamera_getTargetCount =
				loader.LoadFunction<ZkCutsceneCamera_getTargetCount>("ZkCutsceneCamera_getTargetCount");
			ZkCutsceneCamera_getFrameCount =
				loader.LoadFunction<ZkCutsceneCamera_getFrameCount>("ZkCutsceneCamera_getFrameCount");
			ZkCutsceneCamera_getFrame = loader.LoadFunction<ZkCutsceneCamera_getFrame>("ZkCutsceneCamera_getFrame");
			ZkCutsceneCamera_enumerateFrames =
				loader.LoadFunction<ZkCutsceneCamera_enumerateFrames>("ZkCutsceneCamera_enumerateFrames");
			ZkCameraTrajectoryFrame_getTime =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getTime>("ZkCameraTrajectoryFrame_getTime");
			ZkCameraTrajectoryFrame_getRollAngle =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getRollAngle>("ZkCameraTrajectoryFrame_getRollAngle");
			ZkCameraTrajectoryFrame_getFovScale =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getFovScale>("ZkCameraTrajectoryFrame_getFovScale");
			ZkCameraTrajectoryFrame_getMotionType =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getMotionType>("ZkCameraTrajectoryFrame_getMotionType");
			ZkCameraTrajectoryFrame_getMotionTypeFov =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getMotionTypeFov>(
					"ZkCameraTrajectoryFrame_getMotionTypeFov");
			ZkCameraTrajectoryFrame_getMotionTypeRoll =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getMotionTypeRoll>(
					"ZkCameraTrajectoryFrame_getMotionTypeRoll");
			ZkCameraTrajectoryFrame_getMotionTypeTimeScale =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getMotionTypeTimeScale>(
					"ZkCameraTrajectoryFrame_getMotionTypeTimeScale");
			ZkCameraTrajectoryFrame_getTension =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getTension>("ZkCameraTrajectoryFrame_getTension");
			ZkCameraTrajectoryFrame_getCamBias =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getCamBias>("ZkCameraTrajectoryFrame_getCamBias");
			ZkCameraTrajectoryFrame_getContinuity =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getContinuity>("ZkCameraTrajectoryFrame_getContinuity");
			ZkCameraTrajectoryFrame_getTimeScale =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getTimeScale>("ZkCameraTrajectoryFrame_getTimeScale");
			ZkCameraTrajectoryFrame_getTimeFixed =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getTimeFixed>("ZkCameraTrajectoryFrame_getTimeFixed");
			ZkCameraTrajectoryFrame_getOriginalPose =
				loader.LoadFunction<ZkCameraTrajectoryFrame_getOriginalPose>("ZkCameraTrajectoryFrame_getOriginalPose");
			ZkLightPreset_load = loader.LoadFunction<ZkLightPreset_load>("ZkLightPreset_load");
			ZkLightPreset_loadPath = loader.LoadFunction<ZkLightPreset_loadPath>("ZkLightPreset_loadPath");
			ZkLightPreset_del = loader.LoadFunction<ZkLightPreset_del>("ZkLightPreset_del");
			ZkLight_load = loader.LoadFunction<ZkLight_load>("ZkLight_load");
			ZkLight_loadPath = loader.LoadFunction<ZkLight_loadPath>("ZkLight_loadPath");
			ZkLight_del = loader.LoadFunction<ZkLight_del>("ZkLight_del");
			ZkLightPreset_getPreset = loader.LoadFunction<ZkLightPreset_getPreset>("ZkLightPreset_getPreset");
			ZkLightPreset_getLightType = loader.LoadFunction<ZkLightPreset_getLightType>("ZkLightPreset_getLightType");
			ZkLightPreset_getRange = loader.LoadFunction<ZkLightPreset_getRange>("ZkLightPreset_getRange");
			ZkLightPreset_getColor = loader.LoadFunction<ZkLightPreset_getColor>("ZkLightPreset_getColor");
			ZkLightPreset_getConeAngle = loader.LoadFunction<ZkLightPreset_getConeAngle>("ZkLightPreset_getConeAngle");
			ZkLightPreset_getIsStatic = loader.LoadFunction<ZkLightPreset_getIsStatic>("ZkLightPreset_getIsStatic");
			ZkLightPreset_getQuality = loader.LoadFunction<ZkLightPreset_getQuality>("ZkLightPreset_getQuality");
			ZkLightPreset_getLensflareFx =
				loader.LoadFunction<ZkLightPreset_getLensflareFx>("ZkLightPreset_getLensflareFx");
			ZkLightPreset_getOn = loader.LoadFunction<ZkLightPreset_getOn>("ZkLightPreset_getOn");
			ZkLightPreset_getRangeAnimationScale =
				loader.LoadFunction<ZkLightPreset_getRangeAnimationScale>("ZkLightPreset_getRangeAnimationScale");
			ZkLightPreset_getRangeAnimationFps =
				loader.LoadFunction<ZkLightPreset_getRangeAnimationFps>("ZkLightPreset_getRangeAnimationFps");
			ZkLightPreset_getRangeAnimationSmooth =
				loader.LoadFunction<ZkLightPreset_getRangeAnimationSmooth>("ZkLightPreset_getRangeAnimationSmooth");
			ZkLightPreset_getColorAnimationList =
				loader.LoadFunction<ZkLightPreset_getColorAnimationList>("ZkLightPreset_getColorAnimationList");
			ZkLightPreset_getColorAnimationFps =
				loader.LoadFunction<ZkLightPreset_getColorAnimationFps>("ZkLightPreset_getColorAnimationFps");
			ZkLightPreset_getColorAnimationSmooth =
				loader.LoadFunction<ZkLightPreset_getColorAnimationSmooth>("ZkLightPreset_getColorAnimationSmooth");
			ZkLightPreset_getCanMove = loader.LoadFunction<ZkLightPreset_getCanMove>("ZkLightPreset_getCanMove");
			ZkLight_getPreset = loader.LoadFunction<ZkLight_getPreset>("ZkLight_getPreset");
			ZkLight_getLightType = loader.LoadFunction<ZkLight_getLightType>("ZkLight_getLightType");
			ZkLight_getRange = loader.LoadFunction<ZkLight_getRange>("ZkLight_getRange");
			ZkLight_getColor = loader.LoadFunction<ZkLight_getColor>("ZkLight_getColor");
			ZkLight_getConeAngle = loader.LoadFunction<ZkLight_getConeAngle>("ZkLight_getConeAngle");
			ZkLight_getIsStatic = loader.LoadFunction<ZkLight_getIsStatic>("ZkLight_getIsStatic");
			ZkLight_getQuality = loader.LoadFunction<ZkLight_getQuality>("ZkLight_getQuality");
			ZkLight_getLensflareFx = loader.LoadFunction<ZkLight_getLensflareFx>("ZkLight_getLensflareFx");
			ZkLight_getOn = loader.LoadFunction<ZkLight_getOn>("ZkLight_getOn");
			ZkLight_getRangeAnimationScale =
				loader.LoadFunction<ZkLight_getRangeAnimationScale>("ZkLight_getRangeAnimationScale");
			ZkLight_getRangeAnimationFps =
				loader.LoadFunction<ZkLight_getRangeAnimationFps>("ZkLight_getRangeAnimationFps");
			ZkLight_getRangeAnimationSmooth =
				loader.LoadFunction<ZkLight_getRangeAnimationSmooth>("ZkLight_getRangeAnimationSmooth");
			ZkLight_getColorAnimationList =
				loader.LoadFunction<ZkLight_getColorAnimationList>("ZkLight_getColorAnimationList");
			ZkLight_getColorAnimationFps =
				loader.LoadFunction<ZkLight_getColorAnimationFps>("ZkLight_getColorAnimationFps");
			ZkLight_getColorAnimationSmooth =
				loader.LoadFunction<ZkLight_getColorAnimationSmooth>("ZkLight_getColorAnimationSmooth");
			ZkLight_getCanMove = loader.LoadFunction<ZkLight_getCanMove>("ZkLight_getCanMove");
			ZkAnimate_load = loader.LoadFunction<ZkAnimate_load>("ZkAnimate_load");
			ZkAnimate_loadPath = loader.LoadFunction<ZkAnimate_loadPath>("ZkAnimate_loadPath");
			ZkAnimate_del = loader.LoadFunction<ZkAnimate_del>("ZkAnimate_del");
			ZkAnimate_getStartOn = loader.LoadFunction<ZkAnimate_getStartOn>("ZkAnimate_getStartOn");
			ZkItem_load = loader.LoadFunction<ZkItem_load>("ZkItem_load");
			ZkItem_loadPath = loader.LoadFunction<ZkItem_loadPath>("ZkItem_loadPath");
			ZkItem_del = loader.LoadFunction<ZkItem_del>("ZkItem_del");
			ZkItem_getInstance = loader.LoadFunction<ZkItem_getInstance>("ZkItem_getInstance");
			ZkLensFlare_load = loader.LoadFunction<ZkLensFlare_load>("ZkLensFlare_load");
			ZkLensFlare_loadPath = loader.LoadFunction<ZkLensFlare_loadPath>("ZkLensFlare_loadPath");
			ZkLensFlare_del = loader.LoadFunction<ZkLensFlare_del>("ZkLensFlare_del");
			ZkLensFlare_getEffect = loader.LoadFunction<ZkLensFlare_getEffect>("ZkLensFlare_getEffect");
			ZkParticleEffectController_load =
				loader.LoadFunction<ZkParticleEffectController_load>("ZkParticleEffectController_load");
			ZkParticleEffectController_loadPath =
				loader.LoadFunction<ZkParticleEffectController_loadPath>("ZkParticleEffectController_loadPath");
			ZkParticleEffectController_del =
				loader.LoadFunction<ZkParticleEffectController_del>("ZkParticleEffectController_del");
			ZkParticleEffectController_getEffectName =
				loader.LoadFunction<ZkParticleEffectController_getEffectName>(
					"ZkParticleEffectController_getEffectName");
			ZkParticleEffectController_getKillWhenDone =
				loader.LoadFunction<ZkParticleEffectController_getKillWhenDone>(
					"ZkParticleEffectController_getKillWhenDone");
			ZkParticleEffectController_getInitiallyRunning =
				loader.LoadFunction<ZkParticleEffectController_getInitiallyRunning>(
					"ZkParticleEffectController_getInitiallyRunning");
			ZkMessageFilter_load = loader.LoadFunction<ZkMessageFilter_load>("ZkMessageFilter_load");
			ZkMessageFilter_loadPath = loader.LoadFunction<ZkMessageFilter_loadPath>("ZkMessageFilter_loadPath");
			ZkMessageFilter_del = loader.LoadFunction<ZkMessageFilter_del>("ZkMessageFilter_del");
			ZkMessageFilter_getTarget = loader.LoadFunction<ZkMessageFilter_getTarget>("ZkMessageFilter_getTarget");
			ZkMessageFilter_getOnTrigger =
				loader.LoadFunction<ZkMessageFilter_getOnTrigger>("ZkMessageFilter_getOnTrigger");
			ZkMessageFilter_getOnUntrigger =
				loader.LoadFunction<ZkMessageFilter_getOnUntrigger>("ZkMessageFilter_getOnUntrigger");
			ZkCodeMaster_load = loader.LoadFunction<ZkCodeMaster_load>("ZkCodeMaster_load");
			ZkCodeMaster_loadPath = loader.LoadFunction<ZkCodeMaster_loadPath>("ZkCodeMaster_loadPath");
			ZkCodeMaster_del = loader.LoadFunction<ZkCodeMaster_del>("ZkCodeMaster_del");
			ZkCodeMaster_getTarget = loader.LoadFunction<ZkCodeMaster_getTarget>("ZkCodeMaster_getTarget");
			ZkCodeMaster_getOrdered = loader.LoadFunction<ZkCodeMaster_getOrdered>("ZkCodeMaster_getOrdered");
			ZkCodeMaster_getFirstFalseIsFailure =
				loader.LoadFunction<ZkCodeMaster_getFirstFalseIsFailure>("ZkCodeMaster_getFirstFalseIsFailure");
			ZkCodeMaster_getFailureTarget =
				loader.LoadFunction<ZkCodeMaster_getFailureTarget>("ZkCodeMaster_getFailureTarget");
			ZkCodeMaster_getUntriggeredCancels =
				loader.LoadFunction<ZkCodeMaster_getUntriggeredCancels>("ZkCodeMaster_getUntriggeredCancels");
			ZkCodeMaster_getSlaveCount = loader.LoadFunction<ZkCodeMaster_getSlaveCount>("ZkCodeMaster_getSlaveCount");
			ZkCodeMaster_getSlave = loader.LoadFunction<ZkCodeMaster_getSlave>("ZkCodeMaster_getSlave");
			ZkCodeMaster_enumerateSlaves =
				loader.LoadFunction<ZkCodeMaster_enumerateSlaves>("ZkCodeMaster_enumerateSlaves");
			ZkMoverController_load = loader.LoadFunction<ZkMoverController_load>("ZkMoverController_load");
			ZkMoverController_loadPath = loader.LoadFunction<ZkMoverController_loadPath>("ZkMoverController_loadPath");
			ZkMoverController_del = loader.LoadFunction<ZkMoverController_del>("ZkMoverController_del");
			ZkMoverController_getTarget =
				loader.LoadFunction<ZkMoverController_getTarget>("ZkMoverController_getTarget");
			ZkMoverController_getMessage =
				loader.LoadFunction<ZkMoverController_getMessage>("ZkMoverController_getMessage");
			ZkMoverController_getKey = loader.LoadFunction<ZkMoverController_getKey>("ZkMoverController_getKey");
			ZkTouchDamage_load = loader.LoadFunction<ZkTouchDamage_load>("ZkTouchDamage_load");
			ZkTouchDamage_loadPath = loader.LoadFunction<ZkTouchDamage_loadPath>("ZkTouchDamage_loadPath");
			ZkTouchDamage_del = loader.LoadFunction<ZkTouchDamage_del>("ZkTouchDamage_del");
			ZkTouchDamage_getDamage = loader.LoadFunction<ZkTouchDamage_getDamage>("ZkTouchDamage_getDamage");
			ZkTouchDamage_getIsBarrier = loader.LoadFunction<ZkTouchDamage_getIsBarrier>("ZkTouchDamage_getIsBarrier");
			ZkTouchDamage_getIsBlunt = loader.LoadFunction<ZkTouchDamage_getIsBlunt>("ZkTouchDamage_getIsBlunt");
			ZkTouchDamage_getIsEdge = loader.LoadFunction<ZkTouchDamage_getIsEdge>("ZkTouchDamage_getIsEdge");
			ZkTouchDamage_getIsFire = loader.LoadFunction<ZkTouchDamage_getIsFire>("ZkTouchDamage_getIsFire");
			ZkTouchDamage_getIsFly = loader.LoadFunction<ZkTouchDamage_getIsFly>("ZkTouchDamage_getIsFly");
			ZkTouchDamage_getIsMagic = loader.LoadFunction<ZkTouchDamage_getIsMagic>("ZkTouchDamage_getIsMagic");
			ZkTouchDamage_getIsPoint = loader.LoadFunction<ZkTouchDamage_getIsPoint>("ZkTouchDamage_getIsPoint");
			ZkTouchDamage_getIsFall = loader.LoadFunction<ZkTouchDamage_getIsFall>("ZkTouchDamage_getIsFall");
			ZkTouchDamage_getRepeatDelaySeconds =
				loader.LoadFunction<ZkTouchDamage_getRepeatDelaySeconds>("ZkTouchDamage_getRepeatDelaySeconds");
			ZkTouchDamage_getVolumeScale =
				loader.LoadFunction<ZkTouchDamage_getVolumeScale>("ZkTouchDamage_getVolumeScale");
			ZkTouchDamage_getCollisionType =
				loader.LoadFunction<ZkTouchDamage_getCollisionType>("ZkTouchDamage_getCollisionType");
			ZkEarthquake_load = loader.LoadFunction<ZkEarthquake_load>("ZkEarthquake_load");
			ZkEarthquake_loadPath = loader.LoadFunction<ZkEarthquake_loadPath>("ZkEarthquake_loadPath");
			ZkEarthquake_del = loader.LoadFunction<ZkEarthquake_del>("ZkEarthquake_del");
			ZkEarthquake_getRadius = loader.LoadFunction<ZkEarthquake_getRadius>("ZkEarthquake_getRadius");
			ZkEarthquake_getDuration = loader.LoadFunction<ZkEarthquake_getDuration>("ZkEarthquake_getDuration");
			ZkEarthquake_getAmplitude = loader.LoadFunction<ZkEarthquake_getAmplitude>("ZkEarthquake_getAmplitude");
			ZkMovableObject_load = loader.LoadFunction<ZkMovableObject_load>("ZkMovableObject_load");
			ZkMovableObject_loadPath = loader.LoadFunction<ZkMovableObject_loadPath>("ZkMovableObject_loadPath");
			ZkMovableObject_del = loader.LoadFunction<ZkMovableObject_del>("ZkMovableObject_del");
			ZkMovableObject_getName = loader.LoadFunction<ZkMovableObject_getName>("ZkMovableObject_getName");
			ZkMovableObject_getHp = loader.LoadFunction<ZkMovableObject_getHp>("ZkMovableObject_getHp");
			ZkMovableObject_getDamage = loader.LoadFunction<ZkMovableObject_getDamage>("ZkMovableObject_getDamage");
			ZkMovableObject_getMovable = loader.LoadFunction<ZkMovableObject_getMovable>("ZkMovableObject_getMovable");
			ZkMovableObject_getTakable = loader.LoadFunction<ZkMovableObject_getTakable>("ZkMovableObject_getTakable");
			ZkMovableObject_getFocusOverride =
				loader.LoadFunction<ZkMovableObject_getFocusOverride>("ZkMovableObject_getFocusOverride");
			ZkMovableObject_getMaterial =
				loader.LoadFunction<ZkMovableObject_getMaterial>("ZkMovableObject_getMaterial");
			ZkMovableObject_getVisualDestroyed =
				loader.LoadFunction<ZkMovableObject_getVisualDestroyed>("ZkMovableObject_getVisualDestroyed");
			ZkMovableObject_getOwner = loader.LoadFunction<ZkMovableObject_getOwner>("ZkMovableObject_getOwner");
			ZkMovableObject_getOwnerGuild =
				loader.LoadFunction<ZkMovableObject_getOwnerGuild>("ZkMovableObject_getOwnerGuild");
			ZkMovableObject_getDestroyed =
				loader.LoadFunction<ZkMovableObject_getDestroyed>("ZkMovableObject_getDestroyed");
			ZkInteractiveObject_load = loader.LoadFunction<ZkInteractiveObject_load>("ZkInteractiveObject_load");
			ZkInteractiveObject_loadPath =
				loader.LoadFunction<ZkInteractiveObject_loadPath>("ZkInteractiveObject_loadPath");
			ZkInteractiveObject_del = loader.LoadFunction<ZkInteractiveObject_del>("ZkInteractiveObject_del");
			ZkInteractiveObject_getState =
				loader.LoadFunction<ZkInteractiveObject_getState>("ZkInteractiveObject_getState");
			ZkInteractiveObject_getTarget =
				loader.LoadFunction<ZkInteractiveObject_getTarget>("ZkInteractiveObject_getTarget");
			ZkInteractiveObject_getItem =
				loader.LoadFunction<ZkInteractiveObject_getItem>("ZkInteractiveObject_getItem");
			ZkInteractiveObject_getConditionFunction =
				loader.LoadFunction<ZkInteractiveObject_getConditionFunction>(
					"ZkInteractiveObject_getConditionFunction");
			ZkInteractiveObject_getOnStateChangeFunction =
				loader.LoadFunction<ZkInteractiveObject_getOnStateChangeFunction>(
					"ZkInteractiveObject_getOnStateChangeFunction");
			ZkInteractiveObject_getRewind =
				loader.LoadFunction<ZkInteractiveObject_getRewind>("ZkInteractiveObject_getRewind");
			ZkFire_load = loader.LoadFunction<ZkFire_load>("ZkFire_load");
			ZkFire_loadPath = loader.LoadFunction<ZkFire_loadPath>("ZkFire_loadPath");
			ZkFire_del = loader.LoadFunction<ZkFire_del>("ZkFire_del");
			ZkFire_getSlot = loader.LoadFunction<ZkFire_getSlot>("ZkFire_getSlot");
			ZkFire_getVobTree = loader.LoadFunction<ZkFire_getVobTree>("ZkFire_getVobTree");
			ZkContainer_load = loader.LoadFunction<ZkContainer_load>("ZkContainer_load");
			ZkContainer_loadPath = loader.LoadFunction<ZkContainer_loadPath>("ZkContainer_loadPath");
			ZkContainer_del = loader.LoadFunction<ZkContainer_del>("ZkContainer_del");
			ZkContainer_getIsLocked = loader.LoadFunction<ZkContainer_getIsLocked>("ZkContainer_getIsLocked");
			ZkContainer_getKey = loader.LoadFunction<ZkContainer_getKey>("ZkContainer_getKey");
			ZkContainer_getPickString = loader.LoadFunction<ZkContainer_getPickString>("ZkContainer_getPickString");
			ZkContainer_getContents = loader.LoadFunction<ZkContainer_getContents>("ZkContainer_getContents");
			ZkDoor_load = loader.LoadFunction<ZkDoor_load>("ZkDoor_load");
			ZkDoor_loadPath = loader.LoadFunction<ZkDoor_loadPath>("ZkDoor_loadPath");
			ZkDoor_del = loader.LoadFunction<ZkDoor_del>("ZkDoor_del");
			ZkDoor_getIsLocked = loader.LoadFunction<ZkDoor_getIsLocked>("ZkDoor_getIsLocked");
			ZkDoor_getKey = loader.LoadFunction<ZkDoor_getKey>("ZkDoor_getKey");
			ZkDoor_getPickString = loader.LoadFunction<ZkDoor_getPickString>("ZkDoor_getPickString");
			ZkSound_load = loader.LoadFunction<ZkSound_load>("ZkSound_load");
			ZkSound_loadPath = loader.LoadFunction<ZkSound_loadPath>("ZkSound_loadPath");
			ZkSound_del = loader.LoadFunction<ZkSound_del>("ZkSound_del");
			ZkSound_getVolume = loader.LoadFunction<ZkSound_getVolume>("ZkSound_getVolume");
			ZkSound_getMode = loader.LoadFunction<ZkSound_getMode>("ZkSound_getMode");
			ZkSound_getRandomDelay = loader.LoadFunction<ZkSound_getRandomDelay>("ZkSound_getRandomDelay");
			ZkSound_getRandomDelayVar = loader.LoadFunction<ZkSound_getRandomDelayVar>("ZkSound_getRandomDelayVar");
			ZkSound_getInitiallyPlaying =
				loader.LoadFunction<ZkSound_getInitiallyPlaying>("ZkSound_getInitiallyPlaying");
			ZkSound_getAmbient3d = loader.LoadFunction<ZkSound_getAmbient3d>("ZkSound_getAmbient3d");
			ZkSound_getObstruction = loader.LoadFunction<ZkSound_getObstruction>("ZkSound_getObstruction");
			ZkSound_getConeAngle = loader.LoadFunction<ZkSound_getConeAngle>("ZkSound_getConeAngle");
			ZkSound_getVolumeType = loader.LoadFunction<ZkSound_getVolumeType>("ZkSound_getVolumeType");
			ZkSound_getRadius = loader.LoadFunction<ZkSound_getRadius>("ZkSound_getRadius");
			ZkSound_getSoundName = loader.LoadFunction<ZkSound_getSoundName>("ZkSound_getSoundName");
			ZkSoundDaytime_load = loader.LoadFunction<ZkSoundDaytime_load>("ZkSoundDaytime_load");
			ZkSoundDaytime_loadPath = loader.LoadFunction<ZkSoundDaytime_loadPath>("ZkSoundDaytime_loadPath");
			ZkSoundDaytime_del = loader.LoadFunction<ZkSoundDaytime_del>("ZkSoundDaytime_del");
			ZkSoundDaytime_getStartTime =
				loader.LoadFunction<ZkSoundDaytime_getStartTime>("ZkSoundDaytime_getStartTime");
			ZkSoundDaytime_getEndTime = loader.LoadFunction<ZkSoundDaytime_getEndTime>("ZkSoundDaytime_getEndTime");
			ZkSoundDaytime_getSoundNameDaytime =
				loader.LoadFunction<ZkSoundDaytime_getSoundNameDaytime>("ZkSoundDaytime_getSoundNameDaytime");
			ZkTrigger_load = loader.LoadFunction<ZkTrigger_load>("ZkTrigger_load");
			ZkTrigger_loadPath = loader.LoadFunction<ZkTrigger_loadPath>("ZkTrigger_loadPath");
			ZkTrigger_del = loader.LoadFunction<ZkTrigger_del>("ZkTrigger_del");
			ZkTrigger_getTarget = loader.LoadFunction<ZkTrigger_getTarget>("ZkTrigger_getTarget");
			ZkTrigger_getFlags = loader.LoadFunction<ZkTrigger_getFlags>("ZkTrigger_getFlags");
			ZkTrigger_getFilterFlags = loader.LoadFunction<ZkTrigger_getFilterFlags>("ZkTrigger_getFilterFlags");
			ZkTrigger_getVobTarget = loader.LoadFunction<ZkTrigger_getVobTarget>("ZkTrigger_getVobTarget");
			ZkTrigger_getMaxActivationCount =
				loader.LoadFunction<ZkTrigger_getMaxActivationCount>("ZkTrigger_getMaxActivationCount");
			ZkTrigger_getRetriggerDelaySeconds =
				loader.LoadFunction<ZkTrigger_getRetriggerDelaySeconds>("ZkTrigger_getRetriggerDelaySeconds");
			ZkTrigger_getDamageThreshold =
				loader.LoadFunction<ZkTrigger_getDamageThreshold>("ZkTrigger_getDamageThreshold");
			ZkTrigger_getFireDelaySeconds =
				loader.LoadFunction<ZkTrigger_getFireDelaySeconds>("ZkTrigger_getFireDelaySeconds");
			ZkMover_load = loader.LoadFunction<ZkMover_load>("ZkMover_load");
			ZkMover_loadPath = loader.LoadFunction<ZkMover_loadPath>("ZkMover_loadPath");
			ZkMover_del = loader.LoadFunction<ZkMover_del>("ZkMover_del");
			ZkMover_getBehavior = loader.LoadFunction<ZkMover_getBehavior>("ZkMover_getBehavior");
			ZkMover_getTouchBlockerDamage =
				loader.LoadFunction<ZkMover_getTouchBlockerDamage>("ZkMover_getTouchBlockerDamage");
			ZkMover_getStayOpenTimeSeconds =
				loader.LoadFunction<ZkMover_getStayOpenTimeSeconds>("ZkMover_getStayOpenTimeSeconds");
			ZkMover_getIsLocked = loader.LoadFunction<ZkMover_getIsLocked>("ZkMover_getIsLocked");
			ZkMover_getAutoLink = loader.LoadFunction<ZkMover_getAutoLink>("ZkMover_getAutoLink");
			ZkMover_getAutoRotate = loader.LoadFunction<ZkMover_getAutoRotate>("ZkMover_getAutoRotate");
			ZkMover_getSpeed = loader.LoadFunction<ZkMover_getSpeed>("ZkMover_getSpeed");
			ZkMover_getLerpType = loader.LoadFunction<ZkMover_getLerpType>("ZkMover_getLerpType");
			ZkMover_getSpeedType = loader.LoadFunction<ZkMover_getSpeedType>("ZkMover_getSpeedType");
			ZkMover_getKeyframes = loader.LoadFunction<ZkMover_getKeyframes>("ZkMover_getKeyframes");
			ZkMover_getSfxOpenStart = loader.LoadFunction<ZkMover_getSfxOpenStart>("ZkMover_getSfxOpenStart");
			ZkMover_getSfxOpenEnd = loader.LoadFunction<ZkMover_getSfxOpenEnd>("ZkMover_getSfxOpenEnd");
			ZkMover_getSfxTransitioning =
				loader.LoadFunction<ZkMover_getSfxTransitioning>("ZkMover_getSfxTransitioning");
			ZkMover_getSfxCloseStart = loader.LoadFunction<ZkMover_getSfxCloseStart>("ZkMover_getSfxCloseStart");
			ZkMover_getSfxCloseEnd = loader.LoadFunction<ZkMover_getSfxCloseEnd>("ZkMover_getSfxCloseEnd");
			ZkMover_getSfxLock = loader.LoadFunction<ZkMover_getSfxLock>("ZkMover_getSfxLock");
			ZkMover_getSfxUnlock = loader.LoadFunction<ZkMover_getSfxUnlock>("ZkMover_getSfxUnlock");
			ZkMover_getSfxUseLocked = loader.LoadFunction<ZkMover_getSfxUseLocked>("ZkMover_getSfxUseLocked");
			ZkTriggerList_load = loader.LoadFunction<ZkTriggerList_load>("ZkTriggerList_load");
			ZkTriggerList_loadPath = loader.LoadFunction<ZkTriggerList_loadPath>("ZkTriggerList_loadPath");
			ZkTriggerList_del = loader.LoadFunction<ZkTriggerList_del>("ZkTriggerList_del");
			ZkTriggerList_getMode = loader.LoadFunction<ZkTriggerList_getMode>("ZkTriggerList_getMode");
			ZkTriggerList_getTargetCount =
				loader.LoadFunction<ZkTriggerList_getTargetCount>("ZkTriggerList_getTargetCount");
			ZkTriggerList_getTarget = loader.LoadFunction<ZkTriggerList_getTarget>("ZkTriggerList_getTarget");
			ZkTriggerList_enumerateTargets =
				loader.LoadFunction<ZkTriggerList_enumerateTargets>("ZkTriggerList_enumerateTargets");
			ZkTriggerListTarget_getName =
				loader.LoadFunction<ZkTriggerListTarget_getName>("ZkTriggerListTarget_getName");
			ZkTriggerListTarget_getDelaySeconds =
				loader.LoadFunction<ZkTriggerListTarget_getDelaySeconds>("ZkTriggerListTarget_getDelaySeconds");
			ZkTriggerScript_load = loader.LoadFunction<ZkTriggerScript_load>("ZkTriggerScript_load");
			ZkTriggerScript_loadPath = loader.LoadFunction<ZkTriggerScript_loadPath>("ZkTriggerScript_loadPath");
			ZkTriggerScript_del = loader.LoadFunction<ZkTriggerScript_del>("ZkTriggerScript_del");
			ZkTriggerScript_getFunction =
				loader.LoadFunction<ZkTriggerScript_getFunction>("ZkTriggerScript_getFunction");
			ZkTriggerChangeLevel_load = loader.LoadFunction<ZkTriggerChangeLevel_load>("ZkTriggerChangeLevel_load");
			ZkTriggerChangeLevel_loadPath =
				loader.LoadFunction<ZkTriggerChangeLevel_loadPath>("ZkTriggerChangeLevel_loadPath");
			ZkTriggerChangeLevel_del = loader.LoadFunction<ZkTriggerChangeLevel_del>("ZkTriggerChangeLevel_del");
			ZkTriggerChangeLevel_getLevelName =
				loader.LoadFunction<ZkTriggerChangeLevel_getLevelName>("ZkTriggerChangeLevel_getLevelName");
			ZkTriggerChangeLevel_getStartVob =
				loader.LoadFunction<ZkTriggerChangeLevel_getStartVob>("ZkTriggerChangeLevel_getStartVob");
			ZkTriggerWorldStart_load = loader.LoadFunction<ZkTriggerWorldStart_load>("ZkTriggerWorldStart_load");
			ZkTriggerWorldStart_loadPath =
				loader.LoadFunction<ZkTriggerWorldStart_loadPath>("ZkTriggerWorldStart_loadPath");
			ZkTriggerWorldStart_del = loader.LoadFunction<ZkTriggerWorldStart_del>("ZkTriggerWorldStart_del");
			ZkTriggerWorldStart_getTarget =
				loader.LoadFunction<ZkTriggerWorldStart_getTarget>("ZkTriggerWorldStart_getTarget");
			ZkTriggerWorldStart_getFireOnce =
				loader.LoadFunction<ZkTriggerWorldStart_getFireOnce>("ZkTriggerWorldStart_getFireOnce");
			ZkTriggerUntouch_load = loader.LoadFunction<ZkTriggerUntouch_load>("ZkTriggerUntouch_load");
			ZkTriggerUntouch_loadPath = loader.LoadFunction<ZkTriggerUntouch_loadPath>("ZkTriggerUntouch_loadPath");
			ZkTriggerUntouch_del = loader.LoadFunction<ZkTriggerUntouch_del>("ZkTriggerUntouch_del");
			ZkTriggerUntouch_getTarget = loader.LoadFunction<ZkTriggerUntouch_getTarget>("ZkTriggerUntouch_getTarget");
			ZkZoneMusic_load = loader.LoadFunction<ZkZoneMusic_load>("ZkZoneMusic_load");
			ZkZoneMusic_loadPath = loader.LoadFunction<ZkZoneMusic_loadPath>("ZkZoneMusic_loadPath");
			ZkZoneMusic_del = loader.LoadFunction<ZkZoneMusic_del>("ZkZoneMusic_del");
			ZkZoneMusic_getIsEnabled = loader.LoadFunction<ZkZoneMusic_getIsEnabled>("ZkZoneMusic_getIsEnabled");
			ZkZoneMusic_getPriority = loader.LoadFunction<ZkZoneMusic_getPriority>("ZkZoneMusic_getPriority");
			ZkZoneMusic_getIsEllipsoid = loader.LoadFunction<ZkZoneMusic_getIsEllipsoid>("ZkZoneMusic_getIsEllipsoid");
			ZkZoneMusic_getReverb = loader.LoadFunction<ZkZoneMusic_getReverb>("ZkZoneMusic_getReverb");
			ZkZoneMusic_getVolume = loader.LoadFunction<ZkZoneMusic_getVolume>("ZkZoneMusic_getVolume");
			ZkZoneMusic_getIsLoop = loader.LoadFunction<ZkZoneMusic_getIsLoop>("ZkZoneMusic_getIsLoop");
			ZkZoneFarPlane_load = loader.LoadFunction<ZkZoneFarPlane_load>("ZkZoneFarPlane_load");
			ZkZoneFarPlane_loadPath = loader.LoadFunction<ZkZoneFarPlane_loadPath>("ZkZoneFarPlane_loadPath");
			ZkZoneFarPlane_del = loader.LoadFunction<ZkZoneFarPlane_del>("ZkZoneFarPlane_del");
			ZkZoneFarPlane_getVobFarPlaneZ =
				loader.LoadFunction<ZkZoneFarPlane_getVobFarPlaneZ>("ZkZoneFarPlane_getVobFarPlaneZ");
			ZkZoneFarPlane_getInnerRangePercentage =
				loader.LoadFunction<ZkZoneFarPlane_getInnerRangePercentage>("ZkZoneFarPlane_getInnerRangePercentage");
			ZkZoneFog_load = loader.LoadFunction<ZkZoneFog_load>("ZkZoneFog_load");
			ZkZoneFog_loadPath = loader.LoadFunction<ZkZoneFog_loadPath>("ZkZoneFog_loadPath");
			ZkZoneFog_del = loader.LoadFunction<ZkZoneFog_del>("ZkZoneFog_del");
			ZkZoneFog_getRangeCenter = loader.LoadFunction<ZkZoneFog_getRangeCenter>("ZkZoneFog_getRangeCenter");
			ZkZoneFog_getInnerRangePercentage =
				loader.LoadFunction<ZkZoneFog_getInnerRangePercentage>("ZkZoneFog_getInnerRangePercentage");
			ZkZoneFog_getColor = loader.LoadFunction<ZkZoneFog_getColor>("ZkZoneFog_getColor");
			ZkZoneFog_getFadeOutSky = loader.LoadFunction<ZkZoneFog_getFadeOutSky>("ZkZoneFog_getFadeOutSky");
			ZkZoneFog_getOverrideColor = loader.LoadFunction<ZkZoneFog_getOverrideColor>("ZkZoneFog_getOverrideColor");
			ZkDaedalusScript_load = loader.LoadFunction<ZkDaedalusScript_load>("ZkDaedalusScript_load");
			ZkDaedalusScript_loadPath = loader.LoadFunction<ZkDaedalusScript_loadPath>("ZkDaedalusScript_loadPath");
			ZkDaedalusScript_loadVfs = loader.LoadFunction<ZkDaedalusScript_loadVfs>("ZkDaedalusScript_loadVfs");
			ZkDaedalusScript_del = loader.LoadFunction<ZkDaedalusScript_del>("ZkDaedalusScript_del");
			ZkDaedalusScript_getSymbolCount =
				loader.LoadFunction<ZkDaedalusScript_getSymbolCount>("ZkDaedalusScript_getSymbolCount");
			ZkDaedalusScript_enumerateSymbols =
				loader.LoadFunction<ZkDaedalusScript_enumerateSymbols>("ZkDaedalusScript_enumerateSymbols");
			ZkDaedalusScript_enumerateInstanceSymbols =
				loader.LoadFunction<ZkDaedalusScript_enumerateInstanceSymbols>(
					"ZkDaedalusScript_enumerateInstanceSymbols");
			ZkDaedalusScript_getInstruction =
				loader.LoadFunction<ZkDaedalusScript_getInstruction>("ZkDaedalusScript_getInstruction");
			ZkDaedalusScript_getSymbolByIndex =
				loader.LoadFunction<ZkDaedalusScript_getSymbolByIndex>("ZkDaedalusScript_getSymbolByIndex");
			ZkDaedalusScript_getSymbolByAddress =
				loader.LoadFunction<ZkDaedalusScript_getSymbolByAddress>("ZkDaedalusScript_getSymbolByAddress");
			ZkDaedalusScript_getSymbolByName =
				loader.LoadFunction<ZkDaedalusScript_getSymbolByName>("ZkDaedalusScript_getSymbolByName");
			ZkDaedalusSymbol_getString = loader.LoadFunction<ZkDaedalusSymbol_getString>("ZkDaedalusSymbol_getString");
			ZkDaedalusSymbol_getFloat = loader.LoadFunction<ZkDaedalusSymbol_getFloat>("ZkDaedalusSymbol_getFloat");
			ZkDaedalusSymbol_getInt = loader.LoadFunction<ZkDaedalusSymbol_getInt>("ZkDaedalusSymbol_getInt");
			ZkDaedalusSymbol_setString = loader.LoadFunction<ZkDaedalusSymbol_setString>("ZkDaedalusSymbol_setString");
			ZkDaedalusSymbol_setFloat = loader.LoadFunction<ZkDaedalusSymbol_setFloat>("ZkDaedalusSymbol_setFloat");
			ZkDaedalusSymbol_setInt = loader.LoadFunction<ZkDaedalusSymbol_setInt>("ZkDaedalusSymbol_setInt");
			ZkDaedalusSymbol_getIsConst =
				loader.LoadFunction<ZkDaedalusSymbol_getIsConst>("ZkDaedalusSymbol_getIsConst");
			ZkDaedalusSymbol_getIsMember =
				loader.LoadFunction<ZkDaedalusSymbol_getIsMember>("ZkDaedalusSymbol_getIsMember");
			ZkDaedalusSymbol_getIsExternal =
				loader.LoadFunction<ZkDaedalusSymbol_getIsExternal>("ZkDaedalusSymbol_getIsExternal");
			ZkDaedalusSymbol_getIsMerged =
				loader.LoadFunction<ZkDaedalusSymbol_getIsMerged>("ZkDaedalusSymbol_getIsMerged");
			ZkDaedalusSymbol_getIsGenerated =
				loader.LoadFunction<ZkDaedalusSymbol_getIsGenerated>("ZkDaedalusSymbol_getIsGenerated");
			ZkDaedalusSymbol_getHasReturn =
				loader.LoadFunction<ZkDaedalusSymbol_getHasReturn>("ZkDaedalusSymbol_getHasReturn");
			ZkDaedalusSymbol_getName = loader.LoadFunction<ZkDaedalusSymbol_getName>("ZkDaedalusSymbol_getName");
			ZkDaedalusSymbol_getAddress =
				loader.LoadFunction<ZkDaedalusSymbol_getAddress>("ZkDaedalusSymbol_getAddress");
			ZkDaedalusSymbol_getParent = loader.LoadFunction<ZkDaedalusSymbol_getParent>("ZkDaedalusSymbol_getParent");
			ZkDaedalusSymbol_getSize = loader.LoadFunction<ZkDaedalusSymbol_getSize>("ZkDaedalusSymbol_getSize");
			ZkDaedalusSymbol_getType = loader.LoadFunction<ZkDaedalusSymbol_getType>("ZkDaedalusSymbol_getType");
			ZkDaedalusSymbol_getIndex = loader.LoadFunction<ZkDaedalusSymbol_getIndex>("ZkDaedalusSymbol_getIndex");
			ZkDaedalusSymbol_getReturnType =
				loader.LoadFunction<ZkDaedalusSymbol_getReturnType>("ZkDaedalusSymbol_getReturnType");
			ZkDaedalusInstance_getType = loader.LoadFunction<ZkDaedalusInstance_getType>("ZkDaedalusInstance_getType");
			ZkDaedalusInstance_getIndex =
				loader.LoadFunction<ZkDaedalusInstance_getIndex>("ZkDaedalusInstance_getIndex");
			ZkDaedalusVm_load = loader.LoadFunction<ZkDaedalusVm_load>("ZkDaedalusVm_load");
			ZkDaedalusVm_loadPath = loader.LoadFunction<ZkDaedalusVm_loadPath>("ZkDaedalusVm_loadPath");
			ZkDaedalusVm_loadVfs = loader.LoadFunction<ZkDaedalusVm_loadVfs>("ZkDaedalusVm_loadVfs");
			ZkDaedalusVm_del = loader.LoadFunction<ZkDaedalusVm_del>("ZkDaedalusVm_del");
			ZkDaedalusVm_pushInt = loader.LoadFunction<ZkDaedalusVm_pushInt>("ZkDaedalusVm_pushInt");
			ZkDaedalusVm_pushFloat = loader.LoadFunction<ZkDaedalusVm_pushFloat>("ZkDaedalusVm_pushFloat");
			ZkDaedalusVm_pushString = loader.LoadFunction<ZkDaedalusVm_pushString>("ZkDaedalusVm_pushString");
			ZkDaedalusVm_pushInstance = loader.LoadFunction<ZkDaedalusVm_pushInstance>("ZkDaedalusVm_pushInstance");
			ZkDaedalusVm_popInt = loader.LoadFunction<ZkDaedalusVm_popInt>("ZkDaedalusVm_popInt");
			ZkDaedalusVm_popFloat = loader.LoadFunction<ZkDaedalusVm_popFloat>("ZkDaedalusVm_popFloat");
			ZkDaedalusVm_popString = loader.LoadFunction<ZkDaedalusVm_popString>("ZkDaedalusVm_popString");
			ZkDaedalusVm_popInstance = loader.LoadFunction<ZkDaedalusVm_popInstance>("ZkDaedalusVm_popInstance");
			ZkDaedalusVm_getGlobalSelf = loader.LoadFunction<ZkDaedalusVm_getGlobalSelf>("ZkDaedalusVm_getGlobalSelf");
			ZkDaedalusVm_getGlobalOther =
				loader.LoadFunction<ZkDaedalusVm_getGlobalOther>("ZkDaedalusVm_getGlobalOther");
			ZkDaedalusVm_getGlobalVictim =
				loader.LoadFunction<ZkDaedalusVm_getGlobalVictim>("ZkDaedalusVm_getGlobalVictim");
			ZkDaedalusVm_getGlobalHero = loader.LoadFunction<ZkDaedalusVm_getGlobalHero>("ZkDaedalusVm_getGlobalHero");
			ZkDaedalusVm_getGlobalItem = loader.LoadFunction<ZkDaedalusVm_getGlobalItem>("ZkDaedalusVm_getGlobalItem");
			ZkDaedalusVm_setGlobalSelf = loader.LoadFunction<ZkDaedalusVm_setGlobalSelf>("ZkDaedalusVm_setGlobalSelf");
			ZkDaedalusVm_setGlobalOther =
				loader.LoadFunction<ZkDaedalusVm_setGlobalOther>("ZkDaedalusVm_setGlobalOther");
			ZkDaedalusVm_setGlobalVictim =
				loader.LoadFunction<ZkDaedalusVm_setGlobalVictim>("ZkDaedalusVm_setGlobalVictim");
			ZkDaedalusVm_setGlobalHero = loader.LoadFunction<ZkDaedalusVm_setGlobalHero>("ZkDaedalusVm_setGlobalHero");
			ZkDaedalusVm_setGlobalItem = loader.LoadFunction<ZkDaedalusVm_setGlobalItem>("ZkDaedalusVm_setGlobalItem");
			ZkDaedalusVm_callFunction = loader.LoadFunction<ZkDaedalusVm_callFunction>("ZkDaedalusVm_callFunction");
			ZkDaedalusVm_initInstance = loader.LoadFunction<ZkDaedalusVm_initInstance>("ZkDaedalusVm_initInstance");
			ZkDaedalusVm_registerExternal =
				loader.LoadFunction<ZkDaedalusVm_registerExternal>("ZkDaedalusVm_registerExternal");
			ZkDaedalusVm_registerExternalDefault =
				loader.LoadFunction<ZkDaedalusVm_registerExternalDefault>("ZkDaedalusVm_registerExternalDefault");
			ZkDaedalusVm_printStackTrace =
				loader.LoadFunction<ZkDaedalusVm_printStackTrace>("ZkDaedalusVm_printStackTrace");
			ZkGuildValuesInstance_getWaterDepthKnee =
				loader.LoadFunction<ZkGuildValuesInstance_getWaterDepthKnee>("ZkGuildValuesInstance_getWaterDepthKnee");
			ZkGuildValuesInstance_getWaterDepthChest =
				loader.LoadFunction<ZkGuildValuesInstance_getWaterDepthChest>(
					"ZkGuildValuesInstance_getWaterDepthChest");
			ZkGuildValuesInstance_getJumpUpHeight =
				loader.LoadFunction<ZkGuildValuesInstance_getJumpUpHeight>("ZkGuildValuesInstance_getJumpUpHeight");
			ZkGuildValuesInstance_getSwimTime =
				loader.LoadFunction<ZkGuildValuesInstance_getSwimTime>("ZkGuildValuesInstance_getSwimTime");
			ZkGuildValuesInstance_getDiveTime =
				loader.LoadFunction<ZkGuildValuesInstance_getDiveTime>("ZkGuildValuesInstance_getDiveTime");
			ZkGuildValuesInstance_getStepHeight =
				loader.LoadFunction<ZkGuildValuesInstance_getStepHeight>("ZkGuildValuesInstance_getStepHeight");
			ZkGuildValuesInstance_getJumpLowHeight =
				loader.LoadFunction<ZkGuildValuesInstance_getJumpLowHeight>("ZkGuildValuesInstance_getJumpLowHeight");
			ZkGuildValuesInstance_getJumpMidHeight =
				loader.LoadFunction<ZkGuildValuesInstance_getJumpMidHeight>("ZkGuildValuesInstance_getJumpMidHeight");
			ZkGuildValuesInstance_getSlideAngle =
				loader.LoadFunction<ZkGuildValuesInstance_getSlideAngle>("ZkGuildValuesInstance_getSlideAngle");
			ZkGuildValuesInstance_getSlideAngle2 =
				loader.LoadFunction<ZkGuildValuesInstance_getSlideAngle2>("ZkGuildValuesInstance_getSlideAngle2");
			ZkGuildValuesInstance_getDisableAutoRoll =
				loader.LoadFunction<ZkGuildValuesInstance_getDisableAutoRoll>(
					"ZkGuildValuesInstance_getDisableAutoRoll");
			ZkGuildValuesInstance_getSurfaceAlign =
				loader.LoadFunction<ZkGuildValuesInstance_getSurfaceAlign>("ZkGuildValuesInstance_getSurfaceAlign");
			ZkGuildValuesInstance_getClimbHeadingAngle =
				loader.LoadFunction<ZkGuildValuesInstance_getClimbHeadingAngle>(
					"ZkGuildValuesInstance_getClimbHeadingAngle");
			ZkGuildValuesInstance_getClimbHorizAngle =
				loader.LoadFunction<ZkGuildValuesInstance_getClimbHorizAngle>(
					"ZkGuildValuesInstance_getClimbHorizAngle");
			ZkGuildValuesInstance_getClimbGroundAngle =
				loader.LoadFunction<ZkGuildValuesInstance_getClimbGroundAngle>(
					"ZkGuildValuesInstance_getClimbGroundAngle");
			ZkGuildValuesInstance_getFightRangeBase =
				loader.LoadFunction<ZkGuildValuesInstance_getFightRangeBase>("ZkGuildValuesInstance_getFightRangeBase");
			ZkGuildValuesInstance_getFightRangeFist =
				loader.LoadFunction<ZkGuildValuesInstance_getFightRangeFist>("ZkGuildValuesInstance_getFightRangeFist");
			ZkGuildValuesInstance_getFightRangeG =
				loader.LoadFunction<ZkGuildValuesInstance_getFightRangeG>("ZkGuildValuesInstance_getFightRangeG");
			ZkGuildValuesInstance_getFightRange1Hs =
				loader.LoadFunction<ZkGuildValuesInstance_getFightRange1Hs>("ZkGuildValuesInstance_getFightRange1Hs");
			ZkGuildValuesInstance_getFightRange1Ha =
				loader.LoadFunction<ZkGuildValuesInstance_getFightRange1Ha>("ZkGuildValuesInstance_getFightRange1Ha");
			ZkGuildValuesInstance_getFightRange2Hs =
				loader.LoadFunction<ZkGuildValuesInstance_getFightRange2Hs>("ZkGuildValuesInstance_getFightRange2Hs");
			ZkGuildValuesInstance_getFightRange2Ha =
				loader.LoadFunction<ZkGuildValuesInstance_getFightRange2Ha>("ZkGuildValuesInstance_getFightRange2Ha");
			ZkGuildValuesInstance_getFallDownHeight =
				loader.LoadFunction<ZkGuildValuesInstance_getFallDownHeight>("ZkGuildValuesInstance_getFallDownHeight");
			ZkGuildValuesInstance_getFallDownDamage =
				loader.LoadFunction<ZkGuildValuesInstance_getFallDownDamage>("ZkGuildValuesInstance_getFallDownDamage");
			ZkGuildValuesInstance_getBloodDisabled =
				loader.LoadFunction<ZkGuildValuesInstance_getBloodDisabled>("ZkGuildValuesInstance_getBloodDisabled");
			ZkGuildValuesInstance_getBloodMaxDistance =
				loader.LoadFunction<ZkGuildValuesInstance_getBloodMaxDistance>(
					"ZkGuildValuesInstance_getBloodMaxDistance");
			ZkGuildValuesInstance_getBloodAmount =
				loader.LoadFunction<ZkGuildValuesInstance_getBloodAmount>("ZkGuildValuesInstance_getBloodAmount");
			ZkGuildValuesInstance_getBloodFlow =
				loader.LoadFunction<ZkGuildValuesInstance_getBloodFlow>("ZkGuildValuesInstance_getBloodFlow");
			ZkGuildValuesInstance_getTurnSpeed =
				loader.LoadFunction<ZkGuildValuesInstance_getTurnSpeed>("ZkGuildValuesInstance_getTurnSpeed");
			ZkGuildValuesInstance_getBloodEmitter =
				loader.LoadFunction<ZkGuildValuesInstance_getBloodEmitter>("ZkGuildValuesInstance_getBloodEmitter");
			ZkGuildValuesInstance_getBloodTexture =
				loader.LoadFunction<ZkGuildValuesInstance_getBloodTexture>("ZkGuildValuesInstance_getBloodTexture");
			ZkNpcInstance_getId = loader.LoadFunction<ZkNpcInstance_getId>("ZkNpcInstance_getId");
			ZkNpcInstance_getSlot = loader.LoadFunction<ZkNpcInstance_getSlot>("ZkNpcInstance_getSlot");
			ZkNpcInstance_getEffect = loader.LoadFunction<ZkNpcInstance_getEffect>("ZkNpcInstance_getEffect");
			ZkNpcInstance_getType = loader.LoadFunction<ZkNpcInstance_getType>("ZkNpcInstance_getType");
			ZkNpcInstance_getFlags = loader.LoadFunction<ZkNpcInstance_getFlags>("ZkNpcInstance_getFlags");
			ZkNpcInstance_getDamageType =
				loader.LoadFunction<ZkNpcInstance_getDamageType>("ZkNpcInstance_getDamageType");
			ZkNpcInstance_getGuild = loader.LoadFunction<ZkNpcInstance_getGuild>("ZkNpcInstance_getGuild");
			ZkNpcInstance_getLevel = loader.LoadFunction<ZkNpcInstance_getLevel>("ZkNpcInstance_getLevel");
			ZkNpcInstance_getFightTactic =
				loader.LoadFunction<ZkNpcInstance_getFightTactic>("ZkNpcInstance_getFightTactic");
			ZkNpcInstance_getWeapon = loader.LoadFunction<ZkNpcInstance_getWeapon>("ZkNpcInstance_getWeapon");
			ZkNpcInstance_getVoice = loader.LoadFunction<ZkNpcInstance_getVoice>("ZkNpcInstance_getVoice");
			ZkNpcInstance_getVoicePitch =
				loader.LoadFunction<ZkNpcInstance_getVoicePitch>("ZkNpcInstance_getVoicePitch");
			ZkNpcInstance_getBodyMass = loader.LoadFunction<ZkNpcInstance_getBodyMass>("ZkNpcInstance_getBodyMass");
			ZkNpcInstance_getDailyRoutine =
				loader.LoadFunction<ZkNpcInstance_getDailyRoutine>("ZkNpcInstance_getDailyRoutine");
			ZkNpcInstance_getStartAiState =
				loader.LoadFunction<ZkNpcInstance_getStartAiState>("ZkNpcInstance_getStartAiState");
			ZkNpcInstance_getSpawnPoint =
				loader.LoadFunction<ZkNpcInstance_getSpawnPoint>("ZkNpcInstance_getSpawnPoint");
			ZkNpcInstance_getSpawnDelay =
				loader.LoadFunction<ZkNpcInstance_getSpawnDelay>("ZkNpcInstance_getSpawnDelay");
			ZkNpcInstance_getSenses = loader.LoadFunction<ZkNpcInstance_getSenses>("ZkNpcInstance_getSenses");
			ZkNpcInstance_getSensesRange =
				loader.LoadFunction<ZkNpcInstance_getSensesRange>("ZkNpcInstance_getSensesRange");
			ZkNpcInstance_getWp = loader.LoadFunction<ZkNpcInstance_getWp>("ZkNpcInstance_getWp");
			ZkNpcInstance_getExp = loader.LoadFunction<ZkNpcInstance_getExp>("ZkNpcInstance_getExp");
			ZkNpcInstance_getExpNext = loader.LoadFunction<ZkNpcInstance_getExpNext>("ZkNpcInstance_getExpNext");
			ZkNpcInstance_getLp = loader.LoadFunction<ZkNpcInstance_getLp>("ZkNpcInstance_getLp");
			ZkNpcInstance_getBodyStateInterruptableOverride =
				loader.LoadFunction<ZkNpcInstance_getBodyStateInterruptableOverride>(
					"ZkNpcInstance_getBodyStateInterruptableOverride");
			ZkNpcInstance_getNoFocus = loader.LoadFunction<ZkNpcInstance_getNoFocus>("ZkNpcInstance_getNoFocus");
			ZkNpcInstance_getName = loader.LoadFunction<ZkNpcInstance_getName>("ZkNpcInstance_getName");
			ZkNpcInstance_getMission = loader.LoadFunction<ZkNpcInstance_getMission>("ZkNpcInstance_getMission");
			ZkNpcInstance_getAttribute = loader.LoadFunction<ZkNpcInstance_getAttribute>("ZkNpcInstance_getAttribute");
			ZkNpcInstance_getHitChance = loader.LoadFunction<ZkNpcInstance_getHitChance>("ZkNpcInstance_getHitChance");
			ZkNpcInstance_getProtection =
				loader.LoadFunction<ZkNpcInstance_getProtection>("ZkNpcInstance_getProtection");
			ZkNpcInstance_getDamage = loader.LoadFunction<ZkNpcInstance_getDamage>("ZkNpcInstance_getDamage");
			ZkNpcInstance_getAiVar = loader.LoadFunction<ZkNpcInstance_getAiVar>("ZkNpcInstance_getAiVar");
			ZkMissionInstance_getName = loader.LoadFunction<ZkMissionInstance_getName>("ZkMissionInstance_getName");
			ZkMissionInstance_getDescription =
				loader.LoadFunction<ZkMissionInstance_getDescription>("ZkMissionInstance_getDescription");
			ZkMissionInstance_getDuration =
				loader.LoadFunction<ZkMissionInstance_getDuration>("ZkMissionInstance_getDuration");
			ZkMissionInstance_getImportant =
				loader.LoadFunction<ZkMissionInstance_getImportant>("ZkMissionInstance_getImportant");
			ZkMissionInstance_getOfferConditions =
				loader.LoadFunction<ZkMissionInstance_getOfferConditions>("ZkMissionInstance_getOfferConditions");
			ZkMissionInstance_getOffer = loader.LoadFunction<ZkMissionInstance_getOffer>("ZkMissionInstance_getOffer");
			ZkMissionInstance_getSuccessConditions =
				loader.LoadFunction<ZkMissionInstance_getSuccessConditions>("ZkMissionInstance_getSuccessConditions");
			ZkMissionInstance_getSuccess =
				loader.LoadFunction<ZkMissionInstance_getSuccess>("ZkMissionInstance_getSuccess");
			ZkMissionInstance_getFailureConditions =
				loader.LoadFunction<ZkMissionInstance_getFailureConditions>("ZkMissionInstance_getFailureConditions");
			ZkMissionInstance_getFailure =
				loader.LoadFunction<ZkMissionInstance_getFailure>("ZkMissionInstance_getFailure");
			ZkMissionInstance_getObsoleteConditions =
				loader.LoadFunction<ZkMissionInstance_getObsoleteConditions>("ZkMissionInstance_getObsoleteConditions");
			ZkMissionInstance_getObsolete =
				loader.LoadFunction<ZkMissionInstance_getObsolete>("ZkMissionInstance_getObsolete");
			ZkMissionInstance_getRunning =
				loader.LoadFunction<ZkMissionInstance_getRunning>("ZkMissionInstance_getRunning");
			ZkItemInstance_getId = loader.LoadFunction<ZkItemInstance_getId>("ZkItemInstance_getId");
			ZkItemInstance_getName = loader.LoadFunction<ZkItemInstance_getName>("ZkItemInstance_getName");
			ZkItemInstance_getNameId = loader.LoadFunction<ZkItemInstance_getNameId>("ZkItemInstance_getNameId");
			ZkItemInstance_getHp = loader.LoadFunction<ZkItemInstance_getHp>("ZkItemInstance_getHp");
			ZkItemInstance_getHpMax = loader.LoadFunction<ZkItemInstance_getHpMax>("ZkItemInstance_getHpMax");
			ZkItemInstance_getMainFlag = loader.LoadFunction<ZkItemInstance_getMainFlag>("ZkItemInstance_getMainFlag");
			ZkItemInstance_getFlags = loader.LoadFunction<ZkItemInstance_getFlags>("ZkItemInstance_getFlags");
			ZkItemInstance_getWeight = loader.LoadFunction<ZkItemInstance_getWeight>("ZkItemInstance_getWeight");
			ZkItemInstance_getValue = loader.LoadFunction<ZkItemInstance_getValue>("ZkItemInstance_getValue");
			ZkItemInstance_getDamageType =
				loader.LoadFunction<ZkItemInstance_getDamageType>("ZkItemInstance_getDamageType");
			ZkItemInstance_getDamageTotal =
				loader.LoadFunction<ZkItemInstance_getDamageTotal>("ZkItemInstance_getDamageTotal");
			ZkItemInstance_getWear = loader.LoadFunction<ZkItemInstance_getWear>("ZkItemInstance_getWear");
			ZkItemInstance_getNutrition =
				loader.LoadFunction<ZkItemInstance_getNutrition>("ZkItemInstance_getNutrition");
			ZkItemInstance_getMagic = loader.LoadFunction<ZkItemInstance_getMagic>("ZkItemInstance_getMagic");
			ZkItemInstance_getOnEquip = loader.LoadFunction<ZkItemInstance_getOnEquip>("ZkItemInstance_getOnEquip");
			ZkItemInstance_getOnUnequip =
				loader.LoadFunction<ZkItemInstance_getOnUnequip>("ZkItemInstance_getOnUnequip");
			ZkItemInstance_getOwner = loader.LoadFunction<ZkItemInstance_getOwner>("ZkItemInstance_getOwner");
			ZkItemInstance_getOwnerGuild =
				loader.LoadFunction<ZkItemInstance_getOwnerGuild>("ZkItemInstance_getOwnerGuild");
			ZkItemInstance_getDisguiseGuild =
				loader.LoadFunction<ZkItemInstance_getDisguiseGuild>("ZkItemInstance_getDisguiseGuild");
			ZkItemInstance_getVisual = loader.LoadFunction<ZkItemInstance_getVisual>("ZkItemInstance_getVisual");
			ZkItemInstance_getVisualChange =
				loader.LoadFunction<ZkItemInstance_getVisualChange>("ZkItemInstance_getVisualChange");
			ZkItemInstance_getEffect = loader.LoadFunction<ZkItemInstance_getEffect>("ZkItemInstance_getEffect");
			ZkItemInstance_getVisualSkin =
				loader.LoadFunction<ZkItemInstance_getVisualSkin>("ZkItemInstance_getVisualSkin");
			ZkItemInstance_getSchemeName =
				loader.LoadFunction<ZkItemInstance_getSchemeName>("ZkItemInstance_getSchemeName");
			ZkItemInstance_getMaterial = loader.LoadFunction<ZkItemInstance_getMaterial>("ZkItemInstance_getMaterial");
			ZkItemInstance_getMunition = loader.LoadFunction<ZkItemInstance_getMunition>("ZkItemInstance_getMunition");
			ZkItemInstance_getSpell = loader.LoadFunction<ZkItemInstance_getSpell>("ZkItemInstance_getSpell");
			ZkItemInstance_getRange = loader.LoadFunction<ZkItemInstance_getRange>("ZkItemInstance_getRange");
			ZkItemInstance_getMagCircle =
				loader.LoadFunction<ZkItemInstance_getMagCircle>("ZkItemInstance_getMagCircle");
			ZkItemInstance_getDescription =
				loader.LoadFunction<ZkItemInstance_getDescription>("ZkItemInstance_getDescription");
			ZkItemInstance_getInvZBias = loader.LoadFunction<ZkItemInstance_getInvZBias>("ZkItemInstance_getInvZBias");
			ZkItemInstance_getInvRotX = loader.LoadFunction<ZkItemInstance_getInvRotX>("ZkItemInstance_getInvRotX");
			ZkItemInstance_getInvRotY = loader.LoadFunction<ZkItemInstance_getInvRotY>("ZkItemInstance_getInvRotY");
			ZkItemInstance_getInvRotZ = loader.LoadFunction<ZkItemInstance_getInvRotZ>("ZkItemInstance_getInvRotZ");
			ZkItemInstance_getInvAnimate =
				loader.LoadFunction<ZkItemInstance_getInvAnimate>("ZkItemInstance_getInvAnimate");
			ZkItemInstance_getDamage = loader.LoadFunction<ZkItemInstance_getDamage>("ZkItemInstance_getDamage");
			ZkItemInstance_getProtection =
				loader.LoadFunction<ZkItemInstance_getProtection>("ZkItemInstance_getProtection");
			ZkItemInstance_getCondAtr = loader.LoadFunction<ZkItemInstance_getCondAtr>("ZkItemInstance_getCondAtr");
			ZkItemInstance_getCondValue =
				loader.LoadFunction<ZkItemInstance_getCondValue>("ZkItemInstance_getCondValue");
			ZkItemInstance_getChangeAtr =
				loader.LoadFunction<ZkItemInstance_getChangeAtr>("ZkItemInstance_getChangeAtr");
			ZkItemInstance_getChangeValue =
				loader.LoadFunction<ZkItemInstance_getChangeValue>("ZkItemInstance_getChangeValue");
			ZkItemInstance_getOnState = loader.LoadFunction<ZkItemInstance_getOnState>("ZkItemInstance_getOnState");
			ZkItemInstance_getText = loader.LoadFunction<ZkItemInstance_getText>("ZkItemInstance_getText");
			ZkItemInstance_getCount = loader.LoadFunction<ZkItemInstance_getCount>("ZkItemInstance_getCount");
			ZkFocusInstance_getNpcLongrange =
				loader.LoadFunction<ZkFocusInstance_getNpcLongrange>("ZkFocusInstance_getNpcLongrange");
			ZkFocusInstance_getNpcRange1 =
				loader.LoadFunction<ZkFocusInstance_getNpcRange1>("ZkFocusInstance_getNpcRange1");
			ZkFocusInstance_getNpcRange2 =
				loader.LoadFunction<ZkFocusInstance_getNpcRange2>("ZkFocusInstance_getNpcRange2");
			ZkFocusInstance_getNpcAzi = loader.LoadFunction<ZkFocusInstance_getNpcAzi>("ZkFocusInstance_getNpcAzi");
			ZkFocusInstance_getNpcElevdo =
				loader.LoadFunction<ZkFocusInstance_getNpcElevdo>("ZkFocusInstance_getNpcElevdo");
			ZkFocusInstance_getNpcElevup =
				loader.LoadFunction<ZkFocusInstance_getNpcElevup>("ZkFocusInstance_getNpcElevup");
			ZkFocusInstance_getNpcPrio = loader.LoadFunction<ZkFocusInstance_getNpcPrio>("ZkFocusInstance_getNpcPrio");
			ZkFocusInstance_getItemRange1 =
				loader.LoadFunction<ZkFocusInstance_getItemRange1>("ZkFocusInstance_getItemRange1");
			ZkFocusInstance_getItemRange2 =
				loader.LoadFunction<ZkFocusInstance_getItemRange2>("ZkFocusInstance_getItemRange2");
			ZkFocusInstance_getItemAzi = loader.LoadFunction<ZkFocusInstance_getItemAzi>("ZkFocusInstance_getItemAzi");
			ZkFocusInstance_getItemElevdo =
				loader.LoadFunction<ZkFocusInstance_getItemElevdo>("ZkFocusInstance_getItemElevdo");
			ZkFocusInstance_getItemElevup =
				loader.LoadFunction<ZkFocusInstance_getItemElevup>("ZkFocusInstance_getItemElevup");
			ZkFocusInstance_getItemPrio =
				loader.LoadFunction<ZkFocusInstance_getItemPrio>("ZkFocusInstance_getItemPrio");
			ZkFocusInstance_getMobRange1 =
				loader.LoadFunction<ZkFocusInstance_getMobRange1>("ZkFocusInstance_getMobRange1");
			ZkFocusInstance_getMobRange2 =
				loader.LoadFunction<ZkFocusInstance_getMobRange2>("ZkFocusInstance_getMobRange2");
			ZkFocusInstance_getMobAzi = loader.LoadFunction<ZkFocusInstance_getMobAzi>("ZkFocusInstance_getMobAzi");
			ZkFocusInstance_getMobElevdo =
				loader.LoadFunction<ZkFocusInstance_getMobElevdo>("ZkFocusInstance_getMobElevdo");
			ZkFocusInstance_getMobElevup =
				loader.LoadFunction<ZkFocusInstance_getMobElevup>("ZkFocusInstance_getMobElevup");
			ZkFocusInstance_getMobPrio = loader.LoadFunction<ZkFocusInstance_getMobPrio>("ZkFocusInstance_getMobPrio");
			ZkInfoInstance_getNpc = loader.LoadFunction<ZkInfoInstance_getNpc>("ZkInfoInstance_getNpc");
			ZkInfoInstance_getNr = loader.LoadFunction<ZkInfoInstance_getNr>("ZkInfoInstance_getNr");
			ZkInfoInstance_getImportant =
				loader.LoadFunction<ZkInfoInstance_getImportant>("ZkInfoInstance_getImportant");
			ZkInfoInstance_getCondition =
				loader.LoadFunction<ZkInfoInstance_getCondition>("ZkInfoInstance_getCondition");
			ZkInfoInstance_getInformation =
				loader.LoadFunction<ZkInfoInstance_getInformation>("ZkInfoInstance_getInformation");
			ZkInfoInstance_getDescription =
				loader.LoadFunction<ZkInfoInstance_getDescription>("ZkInfoInstance_getDescription");
			ZkInfoInstance_getTrade = loader.LoadFunction<ZkInfoInstance_getTrade>("ZkInfoInstance_getTrade");
			ZkInfoInstance_getPermanent =
				loader.LoadFunction<ZkInfoInstance_getPermanent>("ZkInfoInstance_getPermanent");
			ZkItemReactInstance_getNpc = loader.LoadFunction<ZkItemReactInstance_getNpc>("ZkItemReactInstance_getNpc");
			ZkItemReactInstance_getTradeItem =
				loader.LoadFunction<ZkItemReactInstance_getTradeItem>("ZkItemReactInstance_getTradeItem");
			ZkItemReactInstance_getTradeAmount =
				loader.LoadFunction<ZkItemReactInstance_getTradeAmount>("ZkItemReactInstance_getTradeAmount");
			ZkItemReactInstance_getRequestedCategory =
				loader.LoadFunction<ZkItemReactInstance_getRequestedCategory>(
					"ZkItemReactInstance_getRequestedCategory");
			ZkItemReactInstance_getRequestedItem =
				loader.LoadFunction<ZkItemReactInstance_getRequestedItem>("ZkItemReactInstance_getRequestedItem");
			ZkItemReactInstance_getRequestedAmount =
				loader.LoadFunction<ZkItemReactInstance_getRequestedAmount>("ZkItemReactInstance_getRequestedAmount");
			ZkItemReactInstance_getReaction =
				loader.LoadFunction<ZkItemReactInstance_getReaction>("ZkItemReactInstance_getReaction");
			ZkSpellInstance_getTimePerMana =
				loader.LoadFunction<ZkSpellInstance_getTimePerMana>("ZkSpellInstance_getTimePerMana");
			ZkSpellInstance_getDamagePerLevel =
				loader.LoadFunction<ZkSpellInstance_getDamagePerLevel>("ZkSpellInstance_getDamagePerLevel");
			ZkSpellInstance_getDamageType =
				loader.LoadFunction<ZkSpellInstance_getDamageType>("ZkSpellInstance_getDamageType");
			ZkSpellInstance_getSpellType =
				loader.LoadFunction<ZkSpellInstance_getSpellType>("ZkSpellInstance_getSpellType");
			ZkSpellInstance_getCanTurnDuringInvest =
				loader.LoadFunction<ZkSpellInstance_getCanTurnDuringInvest>("ZkSpellInstance_getCanTurnDuringInvest");
			ZkSpellInstance_getCanChangeTargetDuringInvest =
				loader.LoadFunction<ZkSpellInstance_getCanChangeTargetDuringInvest>(
					"ZkSpellInstance_getCanChangeTargetDuringInvest");
			ZkSpellInstance_getIsMultiEffect =
				loader.LoadFunction<ZkSpellInstance_getIsMultiEffect>("ZkSpellInstance_getIsMultiEffect");
			ZkSpellInstance_getTargetCollectAlgo =
				loader.LoadFunction<ZkSpellInstance_getTargetCollectAlgo>("ZkSpellInstance_getTargetCollectAlgo");
			ZkSpellInstance_getTargetCollectType =
				loader.LoadFunction<ZkSpellInstance_getTargetCollectType>("ZkSpellInstance_getTargetCollectType");
			ZkSpellInstance_getTargetCollectRange =
				loader.LoadFunction<ZkSpellInstance_getTargetCollectRange>("ZkSpellInstance_getTargetCollectRange");
			ZkSpellInstance_getTargetCollectAzi =
				loader.LoadFunction<ZkSpellInstance_getTargetCollectAzi>("ZkSpellInstance_getTargetCollectAzi");
			ZkSpellInstance_getTargetCollectElevation =
				loader.LoadFunction<ZkSpellInstance_getTargetCollectElevation>(
					"ZkSpellInstance_getTargetCollectElevation");
			ZkMenuInstance_getBackPic = loader.LoadFunction<ZkMenuInstance_getBackPic>("ZkMenuInstance_getBackPic");
			ZkMenuInstance_getBackWorld =
				loader.LoadFunction<ZkMenuInstance_getBackWorld>("ZkMenuInstance_getBackWorld");
			ZkMenuInstance_getPosX = loader.LoadFunction<ZkMenuInstance_getPosX>("ZkMenuInstance_getPosX");
			ZkMenuInstance_getPosY = loader.LoadFunction<ZkMenuInstance_getPosY>("ZkMenuInstance_getPosY");
			ZkMenuInstance_getDimX = loader.LoadFunction<ZkMenuInstance_getDimX>("ZkMenuInstance_getDimX");
			ZkMenuInstance_getDimY = loader.LoadFunction<ZkMenuInstance_getDimY>("ZkMenuInstance_getDimY");
			ZkMenuInstance_getAlpha = loader.LoadFunction<ZkMenuInstance_getAlpha>("ZkMenuInstance_getAlpha");
			ZkMenuInstance_getMusicTheme =
				loader.LoadFunction<ZkMenuInstance_getMusicTheme>("ZkMenuInstance_getMusicTheme");
			ZkMenuInstance_getEventTimerMsec =
				loader.LoadFunction<ZkMenuInstance_getEventTimerMsec>("ZkMenuInstance_getEventTimerMsec");
			ZkMenuInstance_getFlags = loader.LoadFunction<ZkMenuInstance_getFlags>("ZkMenuInstance_getFlags");
			ZkMenuInstance_getDefaultOutgame =
				loader.LoadFunction<ZkMenuInstance_getDefaultOutgame>("ZkMenuInstance_getDefaultOutgame");
			ZkMenuInstance_getDefaultIngame =
				loader.LoadFunction<ZkMenuInstance_getDefaultIngame>("ZkMenuInstance_getDefaultIngame");
			ZkMenuInstance_getItem = loader.LoadFunction<ZkMenuInstance_getItem>("ZkMenuInstance_getItem");
			ZkMenuItemInstance_getFontName =
				loader.LoadFunction<ZkMenuItemInstance_getFontName>("ZkMenuItemInstance_getFontName");
			ZkMenuItemInstance_getBackpic =
				loader.LoadFunction<ZkMenuItemInstance_getBackpic>("ZkMenuItemInstance_getBackpic");
			ZkMenuItemInstance_getAlphaMode =
				loader.LoadFunction<ZkMenuItemInstance_getAlphaMode>("ZkMenuItemInstance_getAlphaMode");
			ZkMenuItemInstance_getAlpha =
				loader.LoadFunction<ZkMenuItemInstance_getAlpha>("ZkMenuItemInstance_getAlpha");
			ZkMenuItemInstance_getType = loader.LoadFunction<ZkMenuItemInstance_getType>("ZkMenuItemInstance_getType");
			ZkMenuItemInstance_getOnChgSetOption =
				loader.LoadFunction<ZkMenuItemInstance_getOnChgSetOption>("ZkMenuItemInstance_getOnChgSetOption");
			ZkMenuItemInstance_getOnChgSetOptionSection =
				loader.LoadFunction<ZkMenuItemInstance_getOnChgSetOptionSection>(
					"ZkMenuItemInstance_getOnChgSetOptionSection");
			ZkMenuItemInstance_getPosX = loader.LoadFunction<ZkMenuItemInstance_getPosX>("ZkMenuItemInstance_getPosX");
			ZkMenuItemInstance_getPosY = loader.LoadFunction<ZkMenuItemInstance_getPosY>("ZkMenuItemInstance_getPosY");
			ZkMenuItemInstance_getDimX = loader.LoadFunction<ZkMenuItemInstance_getDimX>("ZkMenuItemInstance_getDimX");
			ZkMenuItemInstance_getDimY = loader.LoadFunction<ZkMenuItemInstance_getDimY>("ZkMenuItemInstance_getDimY");
			ZkMenuItemInstance_getSizeStartScale =
				loader.LoadFunction<ZkMenuItemInstance_getSizeStartScale>("ZkMenuItemInstance_getSizeStartScale");
			ZkMenuItemInstance_getFlags =
				loader.LoadFunction<ZkMenuItemInstance_getFlags>("ZkMenuItemInstance_getFlags");
			ZkMenuItemInstance_getOpenDelayTime =
				loader.LoadFunction<ZkMenuItemInstance_getOpenDelayTime>("ZkMenuItemInstance_getOpenDelayTime");
			ZkMenuItemInstance_getOpenDuration =
				loader.LoadFunction<ZkMenuItemInstance_getOpenDuration>("ZkMenuItemInstance_getOpenDuration");
			ZkMenuItemInstance_getFramePosX =
				loader.LoadFunction<ZkMenuItemInstance_getFramePosX>("ZkMenuItemInstance_getFramePosX");
			ZkMenuItemInstance_getFramePosY =
				loader.LoadFunction<ZkMenuItemInstance_getFramePosY>("ZkMenuItemInstance_getFramePosY");
			ZkMenuItemInstance_getFrameSizeX =
				loader.LoadFunction<ZkMenuItemInstance_getFrameSizeX>("ZkMenuItemInstance_getFrameSizeX");
			ZkMenuItemInstance_getFrameSizeY =
				loader.LoadFunction<ZkMenuItemInstance_getFrameSizeY>("ZkMenuItemInstance_getFrameSizeY");
			ZkMenuItemInstance_getHideIfOptionSectionSet =
				loader.LoadFunction<ZkMenuItemInstance_getHideIfOptionSectionSet>(
					"ZkMenuItemInstance_getHideIfOptionSectionSet");
			ZkMenuItemInstance_getHideIfOptionSet =
				loader.LoadFunction<ZkMenuItemInstance_getHideIfOptionSet>("ZkMenuItemInstance_getHideIfOptionSet");
			ZkMenuItemInstance_getHideOnValue =
				loader.LoadFunction<ZkMenuItemInstance_getHideOnValue>("ZkMenuItemInstance_getHideOnValue");
			ZkMenuItemInstance_getText = loader.LoadFunction<ZkMenuItemInstance_getText>("ZkMenuItemInstance_getText");
			ZkMenuItemInstance_getOnSelAction =
				loader.LoadFunction<ZkMenuItemInstance_getOnSelAction>("ZkMenuItemInstance_getOnSelAction");
			ZkMenuItemInstance_getOnSelActionS =
				loader.LoadFunction<ZkMenuItemInstance_getOnSelActionS>("ZkMenuItemInstance_getOnSelActionS");
			ZkMenuItemInstance_getOnEventAction =
				loader.LoadFunction<ZkMenuItemInstance_getOnEventAction>("ZkMenuItemInstance_getOnEventAction");
			ZkMenuItemInstance_getUserFloat =
				loader.LoadFunction<ZkMenuItemInstance_getUserFloat>("ZkMenuItemInstance_getUserFloat");
			ZkMenuItemInstance_getUserString =
				loader.LoadFunction<ZkMenuItemInstance_getUserString>("ZkMenuItemInstance_getUserString");
			ZkCameraInstance_getBestRange =
				loader.LoadFunction<ZkCameraInstance_getBestRange>("ZkCameraInstance_getBestRange");
			ZkCameraInstance_getMinRange =
				loader.LoadFunction<ZkCameraInstance_getMinRange>("ZkCameraInstance_getMinRange");
			ZkCameraInstance_getMaxRange =
				loader.LoadFunction<ZkCameraInstance_getMaxRange>("ZkCameraInstance_getMaxRange");
			ZkCameraInstance_getBestElevation =
				loader.LoadFunction<ZkCameraInstance_getBestElevation>("ZkCameraInstance_getBestElevation");
			ZkCameraInstance_getMinElevation =
				loader.LoadFunction<ZkCameraInstance_getMinElevation>("ZkCameraInstance_getMinElevation");
			ZkCameraInstance_getMaxElevation =
				loader.LoadFunction<ZkCameraInstance_getMaxElevation>("ZkCameraInstance_getMaxElevation");
			ZkCameraInstance_getBestAzimuth =
				loader.LoadFunction<ZkCameraInstance_getBestAzimuth>("ZkCameraInstance_getBestAzimuth");
			ZkCameraInstance_getMinAzimuth =
				loader.LoadFunction<ZkCameraInstance_getMinAzimuth>("ZkCameraInstance_getMinAzimuth");
			ZkCameraInstance_getMaxAzimuth =
				loader.LoadFunction<ZkCameraInstance_getMaxAzimuth>("ZkCameraInstance_getMaxAzimuth");
			ZkCameraInstance_getBestRotZ =
				loader.LoadFunction<ZkCameraInstance_getBestRotZ>("ZkCameraInstance_getBestRotZ");
			ZkCameraInstance_getMinRotZ =
				loader.LoadFunction<ZkCameraInstance_getMinRotZ>("ZkCameraInstance_getMinRotZ");
			ZkCameraInstance_getMaxRotZ =
				loader.LoadFunction<ZkCameraInstance_getMaxRotZ>("ZkCameraInstance_getMaxRotZ");
			ZkCameraInstance_getRotOffsetX =
				loader.LoadFunction<ZkCameraInstance_getRotOffsetX>("ZkCameraInstance_getRotOffsetX");
			ZkCameraInstance_getRotOffsetY =
				loader.LoadFunction<ZkCameraInstance_getRotOffsetY>("ZkCameraInstance_getRotOffsetY");
			ZkCameraInstance_getRotOffsetZ =
				loader.LoadFunction<ZkCameraInstance_getRotOffsetZ>("ZkCameraInstance_getRotOffsetZ");
			ZkCameraInstance_getTargetOffsetX =
				loader.LoadFunction<ZkCameraInstance_getTargetOffsetX>("ZkCameraInstance_getTargetOffsetX");
			ZkCameraInstance_getTargetOffsetY =
				loader.LoadFunction<ZkCameraInstance_getTargetOffsetY>("ZkCameraInstance_getTargetOffsetY");
			ZkCameraInstance_getTargetOffsetZ =
				loader.LoadFunction<ZkCameraInstance_getTargetOffsetZ>("ZkCameraInstance_getTargetOffsetZ");
			ZkCameraInstance_getVeloTrans =
				loader.LoadFunction<ZkCameraInstance_getVeloTrans>("ZkCameraInstance_getVeloTrans");
			ZkCameraInstance_getVeloRot =
				loader.LoadFunction<ZkCameraInstance_getVeloRot>("ZkCameraInstance_getVeloRot");
			ZkCameraInstance_getTranslate =
				loader.LoadFunction<ZkCameraInstance_getTranslate>("ZkCameraInstance_getTranslate");
			ZkCameraInstance_getRotate = loader.LoadFunction<ZkCameraInstance_getRotate>("ZkCameraInstance_getRotate");
			ZkCameraInstance_getCollision =
				loader.LoadFunction<ZkCameraInstance_getCollision>("ZkCameraInstance_getCollision");
			ZkMusicSystemInstance_getVolume =
				loader.LoadFunction<ZkMusicSystemInstance_getVolume>("ZkMusicSystemInstance_getVolume");
			ZkMusicSystemInstance_getBitResolution =
				loader.LoadFunction<ZkMusicSystemInstance_getBitResolution>("ZkMusicSystemInstance_getBitResolution");
			ZkMusicSystemInstance_getGlobalReverbEnabled =
				loader.LoadFunction<ZkMusicSystemInstance_getGlobalReverbEnabled>(
					"ZkMusicSystemInstance_getGlobalReverbEnabled");
			ZkMusicSystemInstance_getSampleRate =
				loader.LoadFunction<ZkMusicSystemInstance_getSampleRate>("ZkMusicSystemInstance_getSampleRate");
			ZkMusicSystemInstance_getNumChannels =
				loader.LoadFunction<ZkMusicSystemInstance_getNumChannels>("ZkMusicSystemInstance_getNumChannels");
			ZkMusicSystemInstance_getReverbBufferSize =
				loader.LoadFunction<ZkMusicSystemInstance_getReverbBufferSize>(
					"ZkMusicSystemInstance_getReverbBufferSize");
			ZkMusicThemeInstance_getFile =
				loader.LoadFunction<ZkMusicThemeInstance_getFile>("ZkMusicThemeInstance_getFile");
			ZkMusicThemeInstance_getVol =
				loader.LoadFunction<ZkMusicThemeInstance_getVol>("ZkMusicThemeInstance_getVol");
			ZkMusicThemeInstance_getLoop =
				loader.LoadFunction<ZkMusicThemeInstance_getLoop>("ZkMusicThemeInstance_getLoop");
			ZkMusicThemeInstance_getReverbmix =
				loader.LoadFunction<ZkMusicThemeInstance_getReverbmix>("ZkMusicThemeInstance_getReverbmix");
			ZkMusicThemeInstance_getReverbtime =
				loader.LoadFunction<ZkMusicThemeInstance_getReverbtime>("ZkMusicThemeInstance_getReverbtime");
			ZkMusicThemeInstance_getTranstype =
				loader.LoadFunction<ZkMusicThemeInstance_getTranstype>("ZkMusicThemeInstance_getTranstype");
			ZkMusicThemeInstance_getTranssubtype =
				loader.LoadFunction<ZkMusicThemeInstance_getTranssubtype>("ZkMusicThemeInstance_getTranssubtype");
			ZkMusicJingleInstance_getName =
				loader.LoadFunction<ZkMusicJingleInstance_getName>("ZkMusicJingleInstance_getName");
			ZkMusicJingleInstance_getLoop =
				loader.LoadFunction<ZkMusicJingleInstance_getLoop>("ZkMusicJingleInstance_getLoop");
			ZkMusicJingleInstance_getVol =
				loader.LoadFunction<ZkMusicJingleInstance_getVol>("ZkMusicJingleInstance_getVol");
			ZkMusicJingleInstance_getTranssubtype =
				loader.LoadFunction<ZkMusicJingleInstance_getTranssubtype>("ZkMusicJingleInstance_getTranssubtype");
			ZkParticleEffectInstance_getPpsValue =
				loader.LoadFunction<ZkParticleEffectInstance_getPpsValue>("ZkParticleEffectInstance_getPpsValue");
			ZkParticleEffectInstance_getPpsScaleKeysS =
				loader.LoadFunction<ZkParticleEffectInstance_getPpsScaleKeysS>(
					"ZkParticleEffectInstance_getPpsScaleKeysS");
			ZkParticleEffectInstance_getPpsIsLooping =
				loader.LoadFunction<ZkParticleEffectInstance_getPpsIsLooping>(
					"ZkParticleEffectInstance_getPpsIsLooping");
			ZkParticleEffectInstance_getPpsIsSmooth =
				loader.LoadFunction<ZkParticleEffectInstance_getPpsIsSmooth>("ZkParticleEffectInstance_getPpsIsSmooth");
			ZkParticleEffectInstance_getPpsFps =
				loader.LoadFunction<ZkParticleEffectInstance_getPpsFps>("ZkParticleEffectInstance_getPpsFps");
			ZkParticleEffectInstance_getPpsCreateEmS =
				loader.LoadFunction<ZkParticleEffectInstance_getPpsCreateEmS>(
					"ZkParticleEffectInstance_getPpsCreateEmS");
			ZkParticleEffectInstance_getPpsCreateEmDelay =
				loader.LoadFunction<ZkParticleEffectInstance_getPpsCreateEmDelay>(
					"ZkParticleEffectInstance_getPpsCreateEmDelay");
			ZkParticleEffectInstance_getShpTypeS =
				loader.LoadFunction<ZkParticleEffectInstance_getShpTypeS>("ZkParticleEffectInstance_getShpTypeS");
			ZkParticleEffectInstance_getShpForS =
				loader.LoadFunction<ZkParticleEffectInstance_getShpForS>("ZkParticleEffectInstance_getShpForS");
			ZkParticleEffectInstance_getShpOffsetVecS =
				loader.LoadFunction<ZkParticleEffectInstance_getShpOffsetVecS>(
					"ZkParticleEffectInstance_getShpOffsetVecS");
			ZkParticleEffectInstance_getShpDistribTypeS =
				loader.LoadFunction<ZkParticleEffectInstance_getShpDistribTypeS>(
					"ZkParticleEffectInstance_getShpDistribTypeS");
			ZkParticleEffectInstance_getShpDistribWalkSpeed =
				loader.LoadFunction<ZkParticleEffectInstance_getShpDistribWalkSpeed>(
					"ZkParticleEffectInstance_getShpDistribWalkSpeed");
			ZkParticleEffectInstance_getShpIsVolume =
				loader.LoadFunction<ZkParticleEffectInstance_getShpIsVolume>("ZkParticleEffectInstance_getShpIsVolume");
			ZkParticleEffectInstance_getShpDimS =
				loader.LoadFunction<ZkParticleEffectInstance_getShpDimS>("ZkParticleEffectInstance_getShpDimS");
			ZkParticleEffectInstance_getShpMeshS =
				loader.LoadFunction<ZkParticleEffectInstance_getShpMeshS>("ZkParticleEffectInstance_getShpMeshS");
			ZkParticleEffectInstance_getShpMeshRenderB =
				loader.LoadFunction<ZkParticleEffectInstance_getShpMeshRenderB>(
					"ZkParticleEffectInstance_getShpMeshRenderB");
			ZkParticleEffectInstance_getShpScaleKeysS =
				loader.LoadFunction<ZkParticleEffectInstance_getShpScaleKeysS>(
					"ZkParticleEffectInstance_getShpScaleKeysS");
			ZkParticleEffectInstance_getShpScaleIsLooping =
				loader.LoadFunction<ZkParticleEffectInstance_getShpScaleIsLooping>(
					"ZkParticleEffectInstance_getShpScaleIsLooping");
			ZkParticleEffectInstance_getShpScaleIsSmooth =
				loader.LoadFunction<ZkParticleEffectInstance_getShpScaleIsSmooth>(
					"ZkParticleEffectInstance_getShpScaleIsSmooth");
			ZkParticleEffectInstance_getShpScaleFps =
				loader.LoadFunction<ZkParticleEffectInstance_getShpScaleFps>("ZkParticleEffectInstance_getShpScaleFps");
			ZkParticleEffectInstance_getDirModeS =
				loader.LoadFunction<ZkParticleEffectInstance_getDirModeS>("ZkParticleEffectInstance_getDirModeS");
			ZkParticleEffectInstance_getDirForS =
				loader.LoadFunction<ZkParticleEffectInstance_getDirForS>("ZkParticleEffectInstance_getDirForS");
			ZkParticleEffectInstance_getDirModeTargetForS =
				loader.LoadFunction<ZkParticleEffectInstance_getDirModeTargetForS>(
					"ZkParticleEffectInstance_getDirModeTargetForS");
			ZkParticleEffectInstance_getDirModeTargetPosS =
				loader.LoadFunction<ZkParticleEffectInstance_getDirModeTargetPosS>(
					"ZkParticleEffectInstance_getDirModeTargetPosS");
			ZkParticleEffectInstance_getDirAngleHead =
				loader.LoadFunction<ZkParticleEffectInstance_getDirAngleHead>(
					"ZkParticleEffectInstance_getDirAngleHead");
			ZkParticleEffectInstance_getDirAngleHeadVar =
				loader.LoadFunction<ZkParticleEffectInstance_getDirAngleHeadVar>(
					"ZkParticleEffectInstance_getDirAngleHeadVar");
			ZkParticleEffectInstance_getDirAngleElev =
				loader.LoadFunction<ZkParticleEffectInstance_getDirAngleElev>(
					"ZkParticleEffectInstance_getDirAngleElev");
			ZkParticleEffectInstance_getDirAngleElevVar =
				loader.LoadFunction<ZkParticleEffectInstance_getDirAngleElevVar>(
					"ZkParticleEffectInstance_getDirAngleElevVar");
			ZkParticleEffectInstance_getVelAvg =
				loader.LoadFunction<ZkParticleEffectInstance_getVelAvg>("ZkParticleEffectInstance_getVelAvg");
			ZkParticleEffectInstance_getVelVar =
				loader.LoadFunction<ZkParticleEffectInstance_getVelVar>("ZkParticleEffectInstance_getVelVar");
			ZkParticleEffectInstance_getLspPartAvg =
				loader.LoadFunction<ZkParticleEffectInstance_getLspPartAvg>("ZkParticleEffectInstance_getLspPartAvg");
			ZkParticleEffectInstance_getLspPartVar =
				loader.LoadFunction<ZkParticleEffectInstance_getLspPartVar>("ZkParticleEffectInstance_getLspPartVar");
			ZkParticleEffectInstance_getFlyGravityS =
				loader.LoadFunction<ZkParticleEffectInstance_getFlyGravityS>("ZkParticleEffectInstance_getFlyGravityS");
			ZkParticleEffectInstance_getFlyColldetB =
				loader.LoadFunction<ZkParticleEffectInstance_getFlyColldetB>("ZkParticleEffectInstance_getFlyColldetB");
			ZkParticleEffectInstance_getVisNameS =
				loader.LoadFunction<ZkParticleEffectInstance_getVisNameS>("ZkParticleEffectInstance_getVisNameS");
			ZkParticleEffectInstance_getVisOrientationS =
				loader.LoadFunction<ZkParticleEffectInstance_getVisOrientationS>(
					"ZkParticleEffectInstance_getVisOrientationS");
			ZkParticleEffectInstance_getVisTexIsQuadpoly =
				loader.LoadFunction<ZkParticleEffectInstance_getVisTexIsQuadpoly>(
					"ZkParticleEffectInstance_getVisTexIsQuadpoly");
			ZkParticleEffectInstance_getVisTexAniFps =
				loader.LoadFunction<ZkParticleEffectInstance_getVisTexAniFps>(
					"ZkParticleEffectInstance_getVisTexAniFps");
			ZkParticleEffectInstance_getVisTexAniIsLooping =
				loader.LoadFunction<ZkParticleEffectInstance_getVisTexAniIsLooping>(
					"ZkParticleEffectInstance_getVisTexAniIsLooping");
			ZkParticleEffectInstance_getVisTexColorStartS =
				loader.LoadFunction<ZkParticleEffectInstance_getVisTexColorStartS>(
					"ZkParticleEffectInstance_getVisTexColorStartS");
			ZkParticleEffectInstance_getVisTexColorEndS =
				loader.LoadFunction<ZkParticleEffectInstance_getVisTexColorEndS>(
					"ZkParticleEffectInstance_getVisTexColorEndS");
			ZkParticleEffectInstance_getVisSizeStartS =
				loader.LoadFunction<ZkParticleEffectInstance_getVisSizeStartS>(
					"ZkParticleEffectInstance_getVisSizeStartS");
			ZkParticleEffectInstance_getVisSizeEndScale =
				loader.LoadFunction<ZkParticleEffectInstance_getVisSizeEndScale>(
					"ZkParticleEffectInstance_getVisSizeEndScale");
			ZkParticleEffectInstance_getVisAlphaFuncS =
				loader.LoadFunction<ZkParticleEffectInstance_getVisAlphaFuncS>(
					"ZkParticleEffectInstance_getVisAlphaFuncS");
			ZkParticleEffectInstance_getVisAlphaStart =
				loader.LoadFunction<ZkParticleEffectInstance_getVisAlphaStart>(
					"ZkParticleEffectInstance_getVisAlphaStart");
			ZkParticleEffectInstance_getVisAlphaEnd =
				loader.LoadFunction<ZkParticleEffectInstance_getVisAlphaEnd>("ZkParticleEffectInstance_getVisAlphaEnd");
			ZkParticleEffectInstance_getTrlFadeSpeed =
				loader.LoadFunction<ZkParticleEffectInstance_getTrlFadeSpeed>(
					"ZkParticleEffectInstance_getTrlFadeSpeed");
			ZkParticleEffectInstance_getTrlTextureS =
				loader.LoadFunction<ZkParticleEffectInstance_getTrlTextureS>("ZkParticleEffectInstance_getTrlTextureS");
			ZkParticleEffectInstance_getTrlWidth =
				loader.LoadFunction<ZkParticleEffectInstance_getTrlWidth>("ZkParticleEffectInstance_getTrlWidth");
			ZkParticleEffectInstance_getMrkFadesPeed =
				loader.LoadFunction<ZkParticleEffectInstance_getMrkFadesPeed>(
					"ZkParticleEffectInstance_getMrkFadesPeed");
			ZkParticleEffectInstance_getMrktExtureS =
				loader.LoadFunction<ZkParticleEffectInstance_getMrktExtureS>("ZkParticleEffectInstance_getMrktExtureS");
			ZkParticleEffectInstance_getMrkSize =
				loader.LoadFunction<ZkParticleEffectInstance_getMrkSize>("ZkParticleEffectInstance_getMrkSize");
			ZkParticleEffectInstance_getFlockMode =
				loader.LoadFunction<ZkParticleEffectInstance_getFlockMode>("ZkParticleEffectInstance_getFlockMode");
			ZkParticleEffectInstance_getFlockStrength =
				loader.LoadFunction<ZkParticleEffectInstance_getFlockStrength>(
					"ZkParticleEffectInstance_getFlockStrength");
			ZkParticleEffectInstance_getUseEmittersFor =
				loader.LoadFunction<ZkParticleEffectInstance_getUseEmittersFor>(
					"ZkParticleEffectInstance_getUseEmittersFor");
			ZkParticleEffectInstance_getTimeStartEndS =
				loader.LoadFunction<ZkParticleEffectInstance_getTimeStartEndS>(
					"ZkParticleEffectInstance_getTimeStartEndS");
			ZkParticleEffectInstance_getMBiasAmbientPfx =
				loader.LoadFunction<ZkParticleEffectInstance_getMBiasAmbientPfx>(
					"ZkParticleEffectInstance_getMBiasAmbientPfx");
			ZkEffectBaseInstance_getVisNameS =
				loader.LoadFunction<ZkEffectBaseInstance_getVisNameS>("ZkEffectBaseInstance_getVisNameS");
			ZkEffectBaseInstance_getVisSizeS =
				loader.LoadFunction<ZkEffectBaseInstance_getVisSizeS>("ZkEffectBaseInstance_getVisSizeS");
			ZkEffectBaseInstance_getVisAlpha =
				loader.LoadFunction<ZkEffectBaseInstance_getVisAlpha>("ZkEffectBaseInstance_getVisAlpha");
			ZkEffectBaseInstance_getVisAlphaBlendFuncS =
				loader.LoadFunction<ZkEffectBaseInstance_getVisAlphaBlendFuncS>(
					"ZkEffectBaseInstance_getVisAlphaBlendFuncS");
			ZkEffectBaseInstance_getVisTexAniFps =
				loader.LoadFunction<ZkEffectBaseInstance_getVisTexAniFps>("ZkEffectBaseInstance_getVisTexAniFps");
			ZkEffectBaseInstance_getVisTexAniIsLooping =
				loader.LoadFunction<ZkEffectBaseInstance_getVisTexAniIsLooping>(
					"ZkEffectBaseInstance_getVisTexAniIsLooping");
			ZkEffectBaseInstance_getEmTrjModeS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjModeS>("ZkEffectBaseInstance_getEmTrjModeS");
			ZkEffectBaseInstance_getEmTrjOriginNode =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjOriginNode>("ZkEffectBaseInstance_getEmTrjOriginNode");
			ZkEffectBaseInstance_getEmTrjTargetNode =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjTargetNode>("ZkEffectBaseInstance_getEmTrjTargetNode");
			ZkEffectBaseInstance_getEmTrjTargetRange =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjTargetRange>(
					"ZkEffectBaseInstance_getEmTrjTargetRange");
			ZkEffectBaseInstance_getEmTrjTargetAzi =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjTargetAzi>("ZkEffectBaseInstance_getEmTrjTargetAzi");
			ZkEffectBaseInstance_getEmTrjTargetElev =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjTargetElev>("ZkEffectBaseInstance_getEmTrjTargetElev");
			ZkEffectBaseInstance_getEmTrjNumKeys =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjNumKeys>("ZkEffectBaseInstance_getEmTrjNumKeys");
			ZkEffectBaseInstance_getEmTrjNumKeysVar =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjNumKeysVar>("ZkEffectBaseInstance_getEmTrjNumKeysVar");
			ZkEffectBaseInstance_getEmTrjAngleElevVar =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjAngleElevVar>(
					"ZkEffectBaseInstance_getEmTrjAngleElevVar");
			ZkEffectBaseInstance_getEmTrjAngleHeadVar =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjAngleHeadVar>(
					"ZkEffectBaseInstance_getEmTrjAngleHeadVar");
			ZkEffectBaseInstance_getEmTrjKeyDistVar =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjKeyDistVar>("ZkEffectBaseInstance_getEmTrjKeyDistVar");
			ZkEffectBaseInstance_getEmTrjLoopModeS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjLoopModeS>("ZkEffectBaseInstance_getEmTrjLoopModeS");
			ZkEffectBaseInstance_getEmTrjEaseFuncS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjEaseFuncS>("ZkEffectBaseInstance_getEmTrjEaseFuncS");
			ZkEffectBaseInstance_getEmTrjEaseVel =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjEaseVel>("ZkEffectBaseInstance_getEmTrjEaseVel");
			ZkEffectBaseInstance_getEmTrjDynUpdateDelay =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjDynUpdateDelay>(
					"ZkEffectBaseInstance_getEmTrjDynUpdateDelay");
			ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly =
				loader.LoadFunction<ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly>(
					"ZkEffectBaseInstance_getEmTrjDynUpdateTargetOnly");
			ZkEffectBaseInstance_getEmFxCreateS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxCreateS>("ZkEffectBaseInstance_getEmFxCreateS");
			ZkEffectBaseInstance_getEmFxInvestOriginS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxInvestOriginS>(
					"ZkEffectBaseInstance_getEmFxInvestOriginS");
			ZkEffectBaseInstance_getEmFxInvestTargetS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxInvestTargetS>(
					"ZkEffectBaseInstance_getEmFxInvestTargetS");
			ZkEffectBaseInstance_getEmFxTriggerDelay =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxTriggerDelay>(
					"ZkEffectBaseInstance_getEmFxTriggerDelay");
			ZkEffectBaseInstance_getEmFxCreateDownTrj =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxCreateDownTrj>(
					"ZkEffectBaseInstance_getEmFxCreateDownTrj");
			ZkEffectBaseInstance_getEmActionCollDynS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmActionCollDynS>(
					"ZkEffectBaseInstance_getEmActionCollDynS");
			ZkEffectBaseInstance_getEmActionCollStatS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmActionCollStatS>(
					"ZkEffectBaseInstance_getEmActionCollStatS");
			ZkEffectBaseInstance_getEmFxCollStatS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxCollStatS>("ZkEffectBaseInstance_getEmFxCollStatS");
			ZkEffectBaseInstance_getEmFxCollDynS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxCollDynS>("ZkEffectBaseInstance_getEmFxCollDynS");
			ZkEffectBaseInstance_getEmFxCollStatAlignS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxCollStatAlignS>(
					"ZkEffectBaseInstance_getEmFxCollStatAlignS");
			ZkEffectBaseInstance_getEmFxCollDynAlignS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxCollDynAlignS>(
					"ZkEffectBaseInstance_getEmFxCollDynAlignS");
			ZkEffectBaseInstance_getEmFxLifespan =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxLifespan>("ZkEffectBaseInstance_getEmFxLifespan");
			ZkEffectBaseInstance_getEmCheckCollision =
				loader.LoadFunction<ZkEffectBaseInstance_getEmCheckCollision>(
					"ZkEffectBaseInstance_getEmCheckCollision");
			ZkEffectBaseInstance_getEmAdjustShpToOrigin =
				loader.LoadFunction<ZkEffectBaseInstance_getEmAdjustShpToOrigin>(
					"ZkEffectBaseInstance_getEmAdjustShpToOrigin");
			ZkEffectBaseInstance_getEmInvestNextKeyDuration =
				loader.LoadFunction<ZkEffectBaseInstance_getEmInvestNextKeyDuration>(
					"ZkEffectBaseInstance_getEmInvestNextKeyDuration");
			ZkEffectBaseInstance_getEmFlyGravity =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFlyGravity>("ZkEffectBaseInstance_getEmFlyGravity");
			ZkEffectBaseInstance_getEmSelfRotVelS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmSelfRotVelS>("ZkEffectBaseInstance_getEmSelfRotVelS");
			ZkEffectBaseInstance_getLightPresetName =
				loader.LoadFunction<ZkEffectBaseInstance_getLightPresetName>("ZkEffectBaseInstance_getLightPresetName");
			ZkEffectBaseInstance_getSfxId =
				loader.LoadFunction<ZkEffectBaseInstance_getSfxId>("ZkEffectBaseInstance_getSfxId");
			ZkEffectBaseInstance_getSfxIsAmbient =
				loader.LoadFunction<ZkEffectBaseInstance_getSfxIsAmbient>("ZkEffectBaseInstance_getSfxIsAmbient");
			ZkEffectBaseInstance_getSendAssessMagic =
				loader.LoadFunction<ZkEffectBaseInstance_getSendAssessMagic>("ZkEffectBaseInstance_getSendAssessMagic");
			ZkEffectBaseInstance_getSecsPerDamage =
				loader.LoadFunction<ZkEffectBaseInstance_getSecsPerDamage>("ZkEffectBaseInstance_getSecsPerDamage");
			ZkEffectBaseInstance_getEmFxCollDynPercS =
				loader.LoadFunction<ZkEffectBaseInstance_getEmFxCollDynPercS>(
					"ZkEffectBaseInstance_getEmFxCollDynPercS");
			ZkEffectBaseInstance_getUserString =
				loader.LoadFunction<ZkEffectBaseInstance_getUserString>("ZkEffectBaseInstance_getUserString");
			ZkParticleEffectEmitKeyInstance_getVisNameS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getVisNameS>(
					"ZkParticleEffectEmitKeyInstance_getVisNameS");
			ZkParticleEffectEmitKeyInstance_getVisSizeScale =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getVisSizeScale>(
					"ZkParticleEffectEmitKeyInstance_getVisSizeScale");
			ZkParticleEffectEmitKeyInstance_getScaleDuration =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getScaleDuration>(
					"ZkParticleEffectEmitKeyInstance_getScaleDuration");
			ZkParticleEffectEmitKeyInstance_getPfxPpsValue =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxPpsValue>(
					"ZkParticleEffectEmitKeyInstance_getPfxPpsValue");
			ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg>(
					"ZkParticleEffectEmitKeyInstance_getPfxPpsIsSmoothChg");
			ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg>(
					"ZkParticleEffectEmitKeyInstance_getPfxPpsIsLoopingChg");
			ZkParticleEffectEmitKeyInstance_getPfxScTime =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxScTime>(
					"ZkParticleEffectEmitKeyInstance_getPfxScTime");
			ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS>(
					"ZkParticleEffectEmitKeyInstance_getPfxFlyGravityS");
			ZkParticleEffectEmitKeyInstance_getPfxShpDimS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxShpDimS>(
					"ZkParticleEffectEmitKeyInstance_getPfxShpDimS");
			ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg>(
					"ZkParticleEffectEmitKeyInstance_getPfxShpIsVolumeChg");
			ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps>(
					"ZkParticleEffectEmitKeyInstance_getPfxShpScaleFps");
			ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed>(
					"ZkParticleEffectEmitKeyInstance_getPfxShpDistribWalksPeed");
			ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS>(
					"ZkParticleEffectEmitKeyInstance_getPfxShpOffsetVecS");
			ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS>(
					"ZkParticleEffectEmitKeyInstance_getPfxShpDistribTypeS");
			ZkParticleEffectEmitKeyInstance_getPfxDirModeS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxDirModeS>(
					"ZkParticleEffectEmitKeyInstance_getPfxDirModeS");
			ZkParticleEffectEmitKeyInstance_getPfxDirForS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxDirForS>(
					"ZkParticleEffectEmitKeyInstance_getPfxDirForS");
			ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS>(
					"ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetForS");
			ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS>(
					"ZkParticleEffectEmitKeyInstance_getPfxDirModeTargetPosS");
			ZkParticleEffectEmitKeyInstance_getPfxVelAvg =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxVelAvg>(
					"ZkParticleEffectEmitKeyInstance_getPfxVelAvg");
			ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg>(
					"ZkParticleEffectEmitKeyInstance_getPfxLspPartAvg");
			ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart>(
					"ZkParticleEffectEmitKeyInstance_getPfxVisAlphaStart");
			ZkParticleEffectEmitKeyInstance_getLightPresetName =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getLightPresetName>(
					"ZkParticleEffectEmitKeyInstance_getLightPresetName");
			ZkParticleEffectEmitKeyInstance_getLightRange =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getLightRange>(
					"ZkParticleEffectEmitKeyInstance_getLightRange");
			ZkParticleEffectEmitKeyInstance_getSfxId =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getSfxId>(
					"ZkParticleEffectEmitKeyInstance_getSfxId");
			ZkParticleEffectEmitKeyInstance_getSfxIsAmbient =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getSfxIsAmbient>(
					"ZkParticleEffectEmitKeyInstance_getSfxIsAmbient");
			ZkParticleEffectEmitKeyInstance_getEmCreateFxId =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getEmCreateFxId>(
					"ZkParticleEffectEmitKeyInstance_getEmCreateFxId");
			ZkParticleEffectEmitKeyInstance_getEmFlyGravity =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getEmFlyGravity>(
					"ZkParticleEffectEmitKeyInstance_getEmFlyGravity");
			ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS>(
					"ZkParticleEffectEmitKeyInstance_getEmSelfRotVelS");
			ZkParticleEffectEmitKeyInstance_getEmTrjModeS =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getEmTrjModeS>(
					"ZkParticleEffectEmitKeyInstance_getEmTrjModeS");
			ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel>(
					"ZkParticleEffectEmitKeyInstance_getEmTrjEaseVel");
			ZkParticleEffectEmitKeyInstance_getEmCheckCollision =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getEmCheckCollision>(
					"ZkParticleEffectEmitKeyInstance_getEmCheckCollision");
			ZkParticleEffectEmitKeyInstance_getEmFxLifespan =
				loader.LoadFunction<ZkParticleEffectEmitKeyInstance_getEmFxLifespan>(
					"ZkParticleEffectEmitKeyInstance_getEmFxLifespan");
			ZkFightAiInstance_getMove = loader.LoadFunction<ZkFightAiInstance_getMove>("ZkFightAiInstance_getMove");
			ZkSoundEffectInstance_getFile =
				loader.LoadFunction<ZkSoundEffectInstance_getFile>("ZkSoundEffectInstance_getFile");
			ZkSoundEffectInstance_getPitchOff =
				loader.LoadFunction<ZkSoundEffectInstance_getPitchOff>("ZkSoundEffectInstance_getPitchOff");
			ZkSoundEffectInstance_getPitchVar =
				loader.LoadFunction<ZkSoundEffectInstance_getPitchVar>("ZkSoundEffectInstance_getPitchVar");
			ZkSoundEffectInstance_getVolume =
				loader.LoadFunction<ZkSoundEffectInstance_getVolume>("ZkSoundEffectInstance_getVolume");
			ZkSoundEffectInstance_getLoop =
				loader.LoadFunction<ZkSoundEffectInstance_getLoop>("ZkSoundEffectInstance_getLoop");
			ZkSoundEffectInstance_getLoopStartOffset =
				loader.LoadFunction<ZkSoundEffectInstance_getLoopStartOffset>(
					"ZkSoundEffectInstance_getLoopStartOffset");
			ZkSoundEffectInstance_getLoopEndOffset =
				loader.LoadFunction<ZkSoundEffectInstance_getLoopEndOffset>("ZkSoundEffectInstance_getLoopEndOffset");
			ZkSoundEffectInstance_getReverbLevel =
				loader.LoadFunction<ZkSoundEffectInstance_getReverbLevel>("ZkSoundEffectInstance_getReverbLevel");
			ZkSoundEffectInstance_getPfxName =
				loader.LoadFunction<ZkSoundEffectInstance_getPfxName>("ZkSoundEffectInstance_getPfxName");
			ZkSoundSystemInstance_getVolume =
				loader.LoadFunction<ZkSoundSystemInstance_getVolume>("ZkSoundSystemInstance_getVolume");
			ZkSoundSystemInstance_getBitResolution =
				loader.LoadFunction<ZkSoundSystemInstance_getBitResolution>("ZkSoundSystemInstance_getBitResolution");
			ZkSoundSystemInstance_getSampleRate =
				loader.LoadFunction<ZkSoundSystemInstance_getSampleRate>("ZkSoundSystemInstance_getSampleRate");
			ZkSoundSystemInstance_getUseStereo =
				loader.LoadFunction<ZkSoundSystemInstance_getUseStereo>("ZkSoundSystemInstance_getUseStereo");
			ZkSoundSystemInstance_getNumSfxChannels =
				loader.LoadFunction<ZkSoundSystemInstance_getNumSfxChannels>("ZkSoundSystemInstance_getNumSfxChannels");
			ZkSoundSystemInstance_getUsed3DProviderName =
				loader.LoadFunction<ZkSoundSystemInstance_getUsed3DProviderName>(
					"ZkSoundSystemInstance_getUsed3DProviderName");
		}
	}
}