using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using ZenKit.Util;

namespace ZenKit.Vobs
{
	[Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public enum VirtualObjectType
	{
		zCVob = 0,
		zCVobLevelCompo = 1,
		oCItem = 2,
		oCNpc = 3,
		zCMoverController = 4,
		zCVobScreenFX = 5,
		zCVobStair = 6,
		zCPFXController = 7,
		zCVobAnimate = 8,
		zCVobLensFlare = 9,
		zCVobLight = 10,
		zCVobSpot = 11,
		zCVobStartpoint = 12,
		zCMessageFilter = 13,
		zCCodeMaster = 14,
		zCTriggerWorldStart = 15,
		zCCSCamera = 16,
		zCCamTrj_KeyFrame = 17,
		oCTouchDamage = 18,
		zCTriggerUntouch = 19,
		zCEarthquake = 20,
		oCMOB = 21,
		oCMobInter = 22,
		oCMobBed = 23,
		oCMobFire = 24,
		oCMobLadder = 25,
		oCMobSwitch = 26,
		oCMobWheel = 27,
		oCMobContainer = 28,
		oCMobDoor = 29,
		zCTrigger = 30,
		zCTriggerList = 31,
		oCTriggerScript = 32,
		oCTriggerChangeLevel = 33,
		oCCSTrigger = 34,
		zCMover = 35,
		zCVobSound = 36,
		zCVobSoundDaytime = 37,
		oCZoneMusic = 38,
		oCZoneMusicDefault = 39,
		zCZoneZFog = 40,
		zCZoneZFogDefault = 41,
		zCZoneVobFarPlane = 42,
		zCZoneVobFarPlaneDefault = 43,
		Ignored = 44,
		Unknown = 45
	}

	[Serializable]
	public enum SpriteAlignment
	{
		None = 0,
		Yaw = 1,
		Full = 2
	}

	[Serializable]
	public enum ShadowType
	{
		None = 0,
		Blob = 1
	}

	[Serializable]
	public enum AnimationType
	{
		None = 0,
		Wind = 1,
		WindAlt = 2
	}

	[Serializable]
	public enum VisualType
	{
		Decal = 0,
		Mesh = 1,
		MultiResolutionMesh = 2,
		ParticleEffect = 3,
		Camera = 4,
		Model = 5,
		MorphMesh = 6,
		Unknown = 7
	}

	namespace Materialized
	{
		[Serializable]
		public class Visual
		{
			public string Name;
			public VisualType Type;
		}
		
		[Serializable]
		public class VisualDecal : Visual
		{
			public string DecalName;
			public Vector2 Dimension;
			public Vector2 Offset;
			public bool TwoSided;
			public AlphaFunction AlphaFunction;
			public float TextureAnimationFps;
			public byte AlphaWeight;
			public bool IgnoreDaylight;
		}

		[Serializable]
		public class VirtualObject
		{
			public bool Ambient;
			public float AnimationStrength;
			public AnimationType AnimationType;
			public int Bias;
			public AxisAlignedBoundingBox BoundingBox;
			public bool CdDynamic;
			public bool CdStatic;
			public List<VirtualObject> Children;
			public ShadowType DynamicShadows;
			public float FarClipScale;
			public uint Id;
			public string Name;
			public bool PhysicsEnabled;
			public Vector3 Position;
			public string PresetName;
			public Quaternion Rotation;
			public bool ShowVisual;
			public SpriteAlignment SpriteCameraFacingMode;
			public bool Static;
			public VirtualObjectType Type;
			public Visual? Visual;
			public string VisualName;
			public VisualType VisualType;

			internal virtual VirtualObject MaterializeFrom(Vobs.VirtualObject orig)
			{
				Type = orig.Type;
				Id = orig.Id;
				BoundingBox = orig.BoundingBox;
				Position = orig.Position;
				Rotation = orig.Rotation;
				ShowVisual = orig.ShowVisual;
				SpriteCameraFacingMode = orig.SpriteCameraFacingMode;
				CdStatic = orig.CdStatic;
				CdDynamic = orig.CdDynamic;
				Static = orig.Static;
				DynamicShadows = orig.DynamicShadows;
				PhysicsEnabled = orig.PhysicsEnabled;
				AnimationType = orig.AnimationType;
				Bias = orig.Bias;
				Ambient = orig.Ambient;
				AnimationStrength = orig.AnimationStrength;
				FarClipScale = orig.FarClipScale;
				PresetName = orig.PresetName;
				Name = orig.Name;
				Visual = orig.Visual?.Materialize();
				Children = orig.Children.ConvertAll(child => child.Materialize());
				return this;
			}
		}
	}


	public class Visual : IMaterializing<Materialized.Visual>
	{
		protected readonly UIntPtr Handle;

		internal Visual(UIntPtr handle)
		{
			Handle = handle;
		}

		public string Name => Native.ZkVisual_getName(Handle).MarshalAsString() ??
		                      throw new Exception("Failed to load visual name");

		public VisualType Type => Native.ZkVisual_getType(Handle);

		public static Visual? FromNative(UIntPtr ptr)
		{
			if (ptr == UIntPtr.Zero) return null;

			switch (Native.ZkVisual_getType(ptr))
			{
				case VisualType.Decal:
					return new VisualDecal(ptr);
				case VisualType.Mesh:
					return new VisualMesh(ptr);
				case VisualType.MultiResolutionMesh:
					return new VisualMultiResolutionMesh(ptr);
				case VisualType.ParticleEffect:
					return new VisualParticleEffect(ptr);
				case VisualType.Camera:
					return new VisualCamera(ptr);
				case VisualType.Model:
					return new VisualModel(ptr);
				case VisualType.MorphMesh:
					return new VisualMorphMesh(ptr);
				default:
					return new Visual(ptr);
			}
		}
		
		public Materialized.Visual Materialize()
		{
			return new Materialized.Visual
			{
				Name = Name,
				Type = Type,
			};
		}
	}
	
	public class VisualMesh : Visual
	{
		internal VisualMesh(UIntPtr handle) : base(handle)
		{
		}
	}
	
	public class VisualMultiResolutionMesh : Visual
	{
		internal VisualMultiResolutionMesh(UIntPtr handle) : base(handle)
		{
		}
	}
	
	public class VisualParticleEffect : Visual
	{
		internal VisualParticleEffect(UIntPtr handle) : base(handle)
		{
		}
	}
	
	public class VisualCamera : Visual
	{
		internal VisualCamera(UIntPtr handle) : base(handle)
		{
		}
	}
	
	public class VisualModel : Visual
	{
		internal VisualModel(UIntPtr handle) : base(handle)
		{
		}
	}
	
	public class VisualMorphMesh : Visual
	{
		internal VisualMorphMesh(UIntPtr handle) : base(handle)
		{
		}
	}
	
	public class VisualDecal : Visual
	{
		internal VisualDecal(UIntPtr handle) : base(handle)
		{
		}

		public string DecalName => Native.ZkVisualDecal_getName(Handle).MarshalAsString() ??
		                      throw new Exception("Failed to load decal name");

		public Vector2 Dimension => Native.ZkVisualDecal_getDimension(Handle);
		public Vector2 Offset => Native.ZkVisualDecal_getOffset(Handle);
		public bool TwoSided => Native.ZkVisualDecal_getTwoSided(Handle);
		public AlphaFunction AlphaFunction => Native.ZkVisualDecal_getAlphaFunc(Handle);
		public float TextureAnimationFps => Native.ZkVisualDecal_getTextureAnimFps(Handle);
		public byte AlphaWeight => Native.ZkVisualDecal_getAlphaWeight(Handle);
		public bool IgnoreDaylight => Native.ZkVisualDecal_getIgnoreDaylight(Handle);

		public Materialized.VisualDecal Materialize()
		{
			return new Materialized.VisualDecal
			{
				Name = Name,
				Type = Type,
				DecalName = DecalName,
				Dimension = Dimension,
				Offset = Offset,
				TwoSided = TwoSided,
				AlphaFunction = AlphaFunction,
				TextureAnimationFps = TextureAnimationFps,
				AlphaWeight = AlphaWeight,
				IgnoreDaylight = IgnoreDaylight
			};
		}
	}

	public class VirtualObject : IMaterializing<Materialized.VirtualObject>
	{
		private readonly bool _delete;
		protected readonly UIntPtr Handle;

		public VirtualObject(Read buf, GameVersion version)
		{
			Handle = Native.ZkVirtualObject_load(buf.Handle, version);
			_delete = true;
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load virtual object");
		}

		public VirtualObject(string path, GameVersion version)
		{
			Handle = Native.ZkVirtualObject_loadPath(path, version);
			_delete = true;
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load virtual object");
		}

		internal VirtualObject(UIntPtr handle, bool delete)
		{
			Handle = handle;
			_delete = delete;
		}

		public VirtualObjectType Type => Native.ZkVirtualObject_getType(Handle);
		public uint Id => Native.ZkVirtualObject_getId(Handle);
		public AxisAlignedBoundingBox BoundingBox => Native.ZkVirtualObject_getBbox(Handle);
		public Vector3 Position => Native.ZkVirtualObject_getPosition(Handle);
		public Quaternion Rotation => Native.ZkVirtualObject_getRotation(Handle).ToQuaternion();
		public bool ShowVisual => Native.ZkVirtualObject_getShowVisual(Handle);
		public SpriteAlignment SpriteCameraFacingMode => Native.ZkVirtualObject_getSpriteCameraFacingMode(Handle);
		public bool CdStatic => Native.ZkVirtualObject_getCdStatic(Handle);
		public bool CdDynamic => Native.ZkVirtualObject_getCdDynamic(Handle);
		public bool Static => Native.ZkVirtualObject_getVobStatic(Handle);
		public ShadowType DynamicShadows => Native.ZkVirtualObject_getDynamicShadows(Handle);
		public bool PhysicsEnabled => Native.ZkVirtualObject_getPhysicsEnabled(Handle);
		public AnimationType AnimationType => Native.ZkVirtualObject_getAnimMode(Handle);
		public int Bias => Native.ZkVirtualObject_getBias(Handle);
		public bool Ambient => Native.ZkVirtualObject_getAmbient(Handle);
		public float AnimationStrength => Native.ZkVirtualObject_getAnimStrength(Handle);
		public float FarClipScale => Native.ZkVirtualObject_getFarClipScale(Handle);

		public string PresetName => Native.ZkVirtualObject_getPresetName(Handle).MarshalAsString() ??
		                            throw new Exception("Failed to load virtual object preset name");

		public string Name => Native.ZkVirtualObject_getName(Handle).MarshalAsString() ??
		                      throw new Exception("Failed to load virtual object name");

		public Visual? Visual
		{
			get
			{
				var val = Native.ZkVirtualObject_getVisual(Handle);
				return Visual.FromNative(val);
			}
		}

		public ulong ChildCount => Native.ZkVirtualObject_getChildCount(Handle);

		public List<VirtualObject> Children
		{
			get
			{
				var children = new List<VirtualObject>();

				Native.ZkVirtualObject_enumerateChildren(Handle, (_, vob) =>
				{
					children.Add(FromNative(vob));
					return false;
				}, UIntPtr.Zero);

				return children;
			}
		}

		public virtual Materialized.VirtualObject Materialize()
		{
			return new Materialized.VirtualObject().MaterializeFrom(this);
		}

		protected virtual void Delete()
		{
			Native.ZkVirtualObject_del(Handle);
		}

		~VirtualObject()
		{
			if (_delete) Delete();
		}

		public VirtualObject GetChild(ulong i)
		{
			return FromNative(Native.ZkVirtualObject_getChild(Handle, i));
		}

		public static VirtualObject FromNative(UIntPtr ptr)
		{
			return Native.ZkVirtualObject_getType(ptr) switch
			{
				VirtualObjectType.zCCSCamera => new CutsceneCamera(ptr, false),
				VirtualObjectType.zCVobLight => new Light(ptr, false),
				VirtualObjectType.zCVobAnimate => new Animate(ptr, false),
				VirtualObjectType.zCCodeMaster => new CodeMaster(ptr, false),
				VirtualObjectType.zCEarthquake => new Earthquake(ptr, false),
				VirtualObjectType.oCItem => new Item(ptr, false),
				VirtualObjectType.zCVobLensFlare => new LensFlare(ptr, false),
				VirtualObjectType.zCMessageFilter => new MessageFilter(ptr, false),
				VirtualObjectType.zCMoverController => new MoverController(ptr, false),
				VirtualObjectType.zCPFXController => new ParticleEffectController(ptr, false),
				VirtualObjectType.oCTouchDamage => new TouchDamage(ptr, false),
				VirtualObjectType.oCMobContainer => new Container(ptr, false),
				VirtualObjectType.oCMobDoor => new Door(ptr, false),
				VirtualObjectType.oCMobFire => new Fire(ptr, false),
				VirtualObjectType.oCMobInter => new InteractiveObject(ptr, false),
				VirtualObjectType.oCMobLadder => new Ladder(ptr, false),
				VirtualObjectType.oCMobSwitch => new Switch(ptr, false),
				VirtualObjectType.oCMobWheel => new Wheel(ptr, false),
				VirtualObjectType.oCMobBed => new Bed(ptr, false),
				VirtualObjectType.oCMOB => new MovableObject(ptr, false),
				VirtualObjectType.zCVobSound => new Sound(ptr, false),
				VirtualObjectType.zCVobSoundDaytime => new SoundDaytime(ptr, false),
				VirtualObjectType.zCTrigger => new Trigger(ptr, false),
				VirtualObjectType.oCCSTrigger => new CutsceneTrigger(ptr, false),
				VirtualObjectType.zCTriggerList => new TriggerList(ptr, false),
				VirtualObjectType.oCTriggerScript => new TriggerScript(ptr, false),
				VirtualObjectType.zCMover => new Mover(ptr, false),
				VirtualObjectType.oCTriggerChangeLevel => new TriggerChangeLevel(ptr, false),
				VirtualObjectType.zCTriggerWorldStart => new TriggerWorldStart(ptr, false),
				VirtualObjectType.zCTriggerUntouch => new TriggerUntouch(ptr, false),
				VirtualObjectType.oCZoneMusic => new ZoneMusic(ptr, false),
				VirtualObjectType.oCZoneMusicDefault => new ZoneMusicDefault(ptr, false),
				VirtualObjectType.zCZoneZFog => new ZoneFog(ptr, false),
				VirtualObjectType.zCZoneZFogDefault => new ZoneFogDefault(ptr, false),
				VirtualObjectType.zCZoneVobFarPlane => new ZoneFarPlane(ptr, false),
				VirtualObjectType.zCZoneVobFarPlaneDefault => new ZoneFarPlaneDefault(ptr, false),
				_ => new VirtualObject(ptr, false)
			};
		}
	}
	
	public class VSpot : VirtualObject
	{
		public VSpot(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public VSpot(string path, GameVersion version) : base(path, version)
		{
		}

		internal VSpot(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
	
	public class VStair : VirtualObject
	{
		public VStair(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public VStair(string path, GameVersion version) : base(path, version)
		{
		}

		internal VStair(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
	
	public class VStartPoint : VirtualObject
	{
		public VStartPoint(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public VStartPoint(string path, GameVersion version) : base(path, version)
		{
		}

		internal VStartPoint(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
	
	public class VLevel : VirtualObject
	{
		public VLevel(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public VLevel(string path, GameVersion version) : base(path, version)
		{
		}

		internal VLevel(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
}