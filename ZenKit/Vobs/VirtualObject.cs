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

	public interface IVisual : ICacheable<IVisual>
	{
		public string Name { get; set; }
		public VisualType Type { get; }
	}

	[Serializable]
	public class CachedVisual : IVisual
	{
		public string Name { get; set; }
		public VisualType Type { get; set; }

		public IVisual Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class Visual : IVisual
	{
		protected readonly UIntPtr Handle;

		internal Visual(UIntPtr handle)
		{
			Handle = handle;
		}

		public string Name
		{
			get =>
				Native.ZkVisual_getName(Handle).MarshalAsString() ??
				throw new Exception("Failed to load visual name");
			set => Native.ZkVisual_setName(Handle, value);
		}

		public VisualType Type => Native.ZkVisual_getType(Handle);

		public virtual IVisual Cache()
		{
			return new CachedVisual
			{
				Name = Name,
				Type = Type
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public static Visual? FromNative(UIntPtr ptr)
		{
			if (ptr == UIntPtr.Zero) return null;

			return Native.ZkVisual_getType(ptr) switch
			{
				VisualType.Decal => new VisualDecal(ptr),
				VisualType.Mesh => new VisualMesh(ptr),
				VisualType.MultiResolutionMesh => new VisualMultiResolutionMesh(ptr),
				VisualType.ParticleEffect => new VisualParticleEffect(ptr),
				VisualType.Camera => new VisualCamera(ptr),
				VisualType.Model => new VisualModel(ptr),
				VisualType.MorphMesh => new VisualMorphMesh(ptr),
				_ => new Visual(ptr)
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

	public interface IVisualDecal : IVisual
	{
		public AlphaFunction AlphaFunction { get; set; }
		public byte AlphaWeight { get; set; }
		public string DecalName { get; set; }
		public Vector2 Dimension { get; set; }
		public bool IgnoreDaylight { get; set; }
		public Vector2 Offset { get; set; }
		public float TextureAnimationFps { get; set; }
		public bool TwoSided { get; set; }
	}

	[Serializable]
	public class CachedVisualDecal : CachedVisual, IVisualDecal
	{
		public AlphaFunction AlphaFunction { get; set; }
		public byte AlphaWeight { get; set; }
		public string DecalName { get; set; }
		public Vector2 Dimension { get; set; }
		public bool IgnoreDaylight { get; set; }
		public Vector2 Offset { get; set; }
		public float TextureAnimationFps { get; set; }
		public bool TwoSided { get; set; }
	}

	public class VisualDecal : Visual, IVisualDecal
	{
		internal VisualDecal(UIntPtr handle) : base(handle)
		{
		}

		public string DecalName
		{
			get =>
				Native.ZkVisualDecal_getName(Handle).MarshalAsString() ??
				throw new Exception("Failed to load decal name");
			set => Native.ZkVisualDecal_setName(Handle, value);
		}

		public Vector2 Dimension
		{
			get => Native.ZkVisualDecal_getDimension(Handle);
			set => Native.ZkVisualDecal_setDimension(Handle, value);
		}

		public Vector2 Offset
		{
			get => Native.ZkVisualDecal_getOffset(Handle);
			set => Native.ZkVisualDecal_setOffset(Handle, value);
		}

		public bool TwoSided
		{
			get => Native.ZkVisualDecal_getTwoSided(Handle);
			set => Native.ZkVisualDecal_setTwoSided(Handle, value);
		}

		public AlphaFunction AlphaFunction
		{
			get => Native.ZkVisualDecal_getAlphaFunc(Handle);
			set => Native.ZkVisualDecal_setAlphaFunc(Handle, value);
		}

		public float TextureAnimationFps
		{
			get => Native.ZkVisualDecal_getTextureAnimFps(Handle);
			set => Native.ZkVisualDecal_setTextureAnimFps(Handle, value);
		}

		public byte AlphaWeight
		{
			get => Native.ZkVisualDecal_getAlphaWeight(Handle);
			set => Native.ZkVisualDecal_setAlphaWeight(Handle, value);
		}

		public bool IgnoreDaylight
		{
			get => Native.ZkVisualDecal_getIgnoreDaylight(Handle);
			set => Native.ZkVisualDecal_setIgnoreDaylight(Handle, value);
		}

		public IVisual Cache()
		{
			return new CachedVisualDecal
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

	public interface IVirtualObject : ICacheable<IVirtualObject>
	{
		public bool Ambient { get; set; }
		public float AnimationStrength { get; set; }
		public AnimationType AnimationType { get; set; }
		public int Bias { get; set; }
		public AxisAlignedBoundingBox BoundingBox { get; set; }
		public bool CdDynamic { get; set; }
		public bool CdStatic { get; set; }
		public List<IVirtualObject> Children { get; }
		public int ChildCount { get; }
		public ShadowType DynamicShadows { get; set; }
		public float FarClipScale { get; set; }
		public int Id { get; }
		public string Name { get; set; }
		public bool PhysicsEnabled { get; set; }
		public Vector3 Position { get; set; }
		public string PresetName { get; set; }
		public Quaternion Rotation { get; set; }
		public bool ShowVisual { get; set; }
		public SpriteAlignment SpriteCameraFacingMode { get; set; }
		public bool Static { get; set; }
		public VirtualObjectType Type { get; }
		public IVisual? Visual { get; }

		public IVirtualObject GetChild(int i);

		public T AddChild<T>() where T : IVirtualObject;

		public void RemoveChild(int i);

		public void RemoveChildren(Predicate<IVirtualObject> pred);

		public void ResetVisual();
		public T ResetVisual<T>() where T : IVisual;
	}


	[Serializable]
	public class CachedVirtualObject : IVirtualObject
	{
		public bool Ambient { get; set; }
		public float AnimationStrength { get; set; }
		public AnimationType AnimationType { get; set; }
		public int Bias { get; set; }
		public AxisAlignedBoundingBox BoundingBox { get; set; }
		public bool CdDynamic { get; set; }
		public bool CdStatic { get; set; }
		public List<IVirtualObject> Children { get; set; }
		public int ChildCount => Children.Count;
		public ShadowType DynamicShadows { get; set; }
		public float FarClipScale { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public bool PhysicsEnabled { get; set; }
		public Vector3 Position { get; set; }
		public string PresetName { get; set; }
		public Quaternion Rotation { get; set; }
		public bool ShowVisual { get; set; }
		public SpriteAlignment SpriteCameraFacingMode { get; set; }
		public bool Static { get; set; }
		public VirtualObjectType Type { get; set; }
		public IVisual? Visual { get; set; }

		public IVirtualObject Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}

		public IVirtualObject GetChild(int i)
		{
			return Children[i];
		}

		public T AddChild<T>() where T : IVirtualObject
		{
			throw new NotImplementedException();
		}

		public void RemoveChild(int i)
		{
			Children.RemoveAt(i);
		}

		public void RemoveChildren(Predicate<IVirtualObject> pred)
		{
			Children.RemoveAll(pred);
		}

		public void ResetVisual()
		{
		}

		public T ResetVisual<T>() where T : IVisual
		{
			throw new NotImplementedException();
		}

		internal virtual CachedVirtualObject CacheFrom(IVirtualObject orig)
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
			Visual = orig.Visual?.Cache();
			Children = orig.Children.ConvertAll(child => child.Cache());
			return this;
		}
	}

	public class VirtualObject : IVirtualObject
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
		public int Id => (int)Native.ZkVirtualObject_getId(Handle);

		public AxisAlignedBoundingBox BoundingBox
		{
			get => Native.ZkVirtualObject_getBbox(Handle);
			set => Native.ZkVirtualObject_setBbox(Handle, value);
		}

		public Vector3 Position
		{
			get => Native.ZkVirtualObject_getPosition(Handle);
			set => Native.ZkVirtualObject_setPosition(Handle, value);
		}

		public Quaternion Rotation
		{
			get => Native.ZkVirtualObject_getRotation(Handle).ToQuaternion();
			set => Native.ZkVirtualObject_setRotation(Handle, new Native.Structs.ZkMat3x3(value));
		}

		public bool ShowVisual
		{
			get => Native.ZkVirtualObject_getShowVisual(Handle);
			set => Native.ZkVirtualObject_setShowVisual(Handle, value);
		}

		public SpriteAlignment SpriteCameraFacingMode
		{
			get => Native.ZkVirtualObject_getSpriteCameraFacingMode(Handle);
			set => Native.ZkVirtualObject_setSpriteCameraFacingMode(Handle, value);
		}

		public bool CdStatic
		{
			get => Native.ZkVirtualObject_getCdStatic(Handle);
			set => Native.ZkVirtualObject_setCdStatic(Handle, value);
		}

		public bool CdDynamic
		{
			get => Native.ZkVirtualObject_getCdDynamic(Handle);
			set => Native.ZkVirtualObject_setCdDynamic(Handle, value);
		}

		public bool Static
		{
			get => Native.ZkVirtualObject_getVobStatic(Handle);
			set => Native.ZkVirtualObject_setVobStatic(Handle, value);
		}

		public ShadowType DynamicShadows
		{
			get => Native.ZkVirtualObject_getDynamicShadows(Handle);
			set => Native.ZkVirtualObject_setDynamicShadows(Handle, value);
		}

		public bool PhysicsEnabled
		{
			get => Native.ZkVirtualObject_getPhysicsEnabled(Handle);
			set => Native.ZkVirtualObject_setPhysicsEnabled(Handle, value);
		}

		public AnimationType AnimationType
		{
			get => Native.ZkVirtualObject_getAnimMode(Handle);
			set => Native.ZkVirtualObject_setAnimMode(Handle, value);
		}

		public int Bias
		{
			get => Native.ZkVirtualObject_getBias(Handle);
			set => Native.ZkVirtualObject_setBias(Handle, value);
		}

		public bool Ambient
		{
			get => Native.ZkVirtualObject_getAmbient(Handle);
			set => Native.ZkVirtualObject_setAmbient(Handle, value);
		}

		public float AnimationStrength
		{
			get => Native.ZkVirtualObject_getAnimStrength(Handle);
			set => Native.ZkVirtualObject_setAnimStrength(Handle, value);
		}

		public float FarClipScale
		{
			get => Native.ZkVirtualObject_getFarClipScale(Handle);
			set => Native.ZkVirtualObject_setFarClipScale(Handle, value);
		}

		public string PresetName
		{
			get =>
				Native.ZkVirtualObject_getPresetName(Handle).MarshalAsString() ??
				throw new Exception("Failed to load virtual object preset name");
			set => Native.ZkVirtualObject_setPresetName(Handle, value);
		}


		public string Name
		{
			get =>
				Native.ZkVirtualObject_getName(Handle).MarshalAsString() ??
				throw new Exception("Failed to load virtual object name");
			set => Native.ZkVirtualObject_setName(Handle, value);
		}


		public IVisual? Visual
		{
			get
			{
				var val = Native.ZkVirtualObject_getVisual(Handle);
				return Vobs.Visual.FromNative(val);
			}
		}

		public int ChildCount => (int)Native.ZkVirtualObject_getChildCount(Handle);

		public List<IVirtualObject> Children
		{
			get
			{
				var children = new List<IVirtualObject>();

				Native.ZkVirtualObject_enumerateChildren(Handle, (_, vob) =>
				{
					children.Add(FromNative(vob));
					return false;
				}, UIntPtr.Zero);

				return children;
			}
		}

		public virtual IVirtualObject Cache()
		{
			return new CachedVirtualObject().CacheFrom(this);
		}

		public bool IsCached()
		{
			return false;
		}

		public IVirtualObject GetChild(int i)
		{
			return FromNative(Native.ZkVirtualObject_getChild(Handle, (ulong)i));
		}

		public T AddChild<T>() where T : IVirtualObject
		{
			VirtualObjectType type;
			if (typeof(T) == typeof(VirtualObject)) type = VirtualObjectType.zCVob;
			else if (typeof(T) == typeof(Stair)) type = VirtualObjectType.zCVobStair;
			else if (typeof(T) == typeof(Level)) type = VirtualObjectType.zCVobLevelCompo;
			else if (typeof(T) == typeof(Spot)) type = VirtualObjectType.zCVobSpot;
			else if (typeof(T) == typeof(StartPoint)) type = VirtualObjectType.zCVobStartpoint;
			else if (typeof(T) == typeof(CutsceneCamera)) type = VirtualObjectType.zCCSCamera;
			else if (typeof(T) == typeof(Light)) type = VirtualObjectType.zCVobLight;
			else if (typeof(T) == typeof(Animate)) type = VirtualObjectType.zCVobAnimate;
			else if (typeof(T) == typeof(CodeMaster)) type = VirtualObjectType.zCCodeMaster;
			else if (typeof(T) == typeof(Earthquake)) type = VirtualObjectType.zCEarthquake;
			else if (typeof(T) == typeof(Item)) type = VirtualObjectType.oCItem;
			else if (typeof(T) == typeof(LensFlare)) type = VirtualObjectType.zCVobLensFlare;
			else if (typeof(T) == typeof(MessageFilter)) type = VirtualObjectType.zCMessageFilter;
			else if (typeof(T) == typeof(MoverController)) type = VirtualObjectType.zCMoverController;
			else if (typeof(T) == typeof(ParticleEffectController)) type = VirtualObjectType.zCPFXController;
			else if (typeof(T) == typeof(TouchDamage)) type = VirtualObjectType.oCTouchDamage;
			else if (typeof(T) == typeof(Container)) type = VirtualObjectType.oCMobContainer;
			else if (typeof(T) == typeof(Door)) type = VirtualObjectType.oCMobDoor;
			else if (typeof(T) == typeof(Fire)) type = VirtualObjectType.oCMobFire;
			else if (typeof(T) == typeof(InteractiveObject)) type = VirtualObjectType.oCMobInter;
			else if (typeof(T) == typeof(Ladder)) type = VirtualObjectType.oCMobLadder;
			else if (typeof(T) == typeof(Switch)) type = VirtualObjectType.oCMobSwitch;
			else if (typeof(T) == typeof(Wheel)) type = VirtualObjectType.oCMobWheel;
			else if (typeof(T) == typeof(Bed)) type = VirtualObjectType.oCMobBed;
			else if (typeof(T) == typeof(MovableObject)) type = VirtualObjectType.oCMOB;
			else if (typeof(T) == typeof(Sound)) type = VirtualObjectType.zCVobSound;
			else if (typeof(T) == typeof(SoundDaytime)) type = VirtualObjectType.zCVobSoundDaytime;
			else if (typeof(T) == typeof(Trigger)) type = VirtualObjectType.zCTrigger;
			else if (typeof(T) == typeof(CutsceneTrigger)) type = VirtualObjectType.oCCSTrigger;
			else if (typeof(T) == typeof(TriggerList)) type = VirtualObjectType.zCTriggerList;
			else if (typeof(T) == typeof(TriggerScript)) type = VirtualObjectType.oCTriggerScript;
			else if (typeof(T) == typeof(Mover)) type = VirtualObjectType.zCMover;
			else if (typeof(T) == typeof(TriggerChangeLevel)) type = VirtualObjectType.oCTriggerChangeLevel;
			else if (typeof(T) == typeof(TriggerWorldStart)) type = VirtualObjectType.zCTriggerWorldStart;
			else if (typeof(T) == typeof(TriggerUntouch)) type = VirtualObjectType.zCTriggerUntouch;
			else if (typeof(T) == typeof(ZoneMusic)) type = VirtualObjectType.oCZoneMusic;
			else if (typeof(T) == typeof(ZoneMusicDefault)) type = VirtualObjectType.oCZoneMusicDefault;
			else if (typeof(T) == typeof(ZoneFog)) type = VirtualObjectType.zCZoneZFog;
			else if (typeof(T) == typeof(ZoneFogDefault)) type = VirtualObjectType.zCZoneZFogDefault;
			else if (typeof(T) == typeof(ZoneFarPlane)) type = VirtualObjectType.zCZoneVobFarPlane;
			else if (typeof(T) == typeof(ZoneFarPlaneDefault)) type = VirtualObjectType.zCZoneVobFarPlaneDefault;
			else throw new NotSupportedException("Only VObjects are supported");

			return (T)(object)FromNative(Native.ZkVirtualObject_addChild(Handle, type));
		}

		public void RemoveChild(int i)
		{
			Native.ZkVirtualObject_removeChild(Handle, (ulong)i);
		}

		public void RemoveChildren(Predicate<IVirtualObject> pred)
		{
			Native.ZkVirtualObject_removeChildren(Handle, (_, vob) => pred(FromNative(vob)), UIntPtr.Zero);
		}

		public void ResetVisual()
		{
			Native.ZkVirtualObject_setVisual(Handle, VisualType.Unknown);
		}

		public T ResetVisual<T>() where T : IVisual
		{
			VisualType type;
			if (typeof(T) == typeof(VisualCamera)) type = VisualType.Camera;
			else if (typeof(T) == typeof(VisualDecal)) type = VisualType.Decal;
			else if (typeof(T) == typeof(VisualDecal)) type = VisualType.Mesh;
			else if (typeof(T) == typeof(VisualDecal)) type = VisualType.MorphMesh;
			else if (typeof(T) == typeof(VisualDecal)) type = VisualType.Model;
			else if (typeof(T) == typeof(VisualDecal)) type = VisualType.MultiResolutionMesh;
			else if (typeof(T) == typeof(VisualDecal)) type = VisualType.ParticleEffect;
			else throw new NotSupportedException("Only Visuals are supported");

			return (T)(object)Vobs.Visual.FromNative(Native.ZkVirtualObject_setVisual(Handle, type))!;
		}

		protected virtual void Delete()
		{
			Native.ZkVirtualObject_del(Handle);
		}

		~VirtualObject()
		{
			if (_delete) Delete();
		}

		public static VirtualObject FromNative(UIntPtr ptr)
		{
			return Native.ZkVirtualObject_getType(ptr) switch
			{
				VirtualObjectType.zCVobLevelCompo => new Level(ptr, false),
				VirtualObjectType.zCVobSpot => new Spot(ptr, false),
				VirtualObjectType.zCVobStair => new Stair(ptr, false),
				VirtualObjectType.zCVobStartpoint => new StartPoint(ptr, false),
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

	public class Spot : VirtualObject
	{
		internal Spot(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}

	public class Stair : VirtualObject
	{
		internal Stair(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}

	public class StartPoint : VirtualObject
	{
		internal StartPoint(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}

	public class Level : VirtualObject
	{
		internal Level(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
}