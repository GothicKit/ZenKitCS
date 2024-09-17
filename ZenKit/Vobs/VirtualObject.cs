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
		internal readonly UIntPtr Handle;

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

		~Visual()
		{
			Native.ZkVisual_del(Handle);
		}

		public static IVisual? FromNative(UIntPtr ptr)
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

	public interface IVisualMesh : IVisual
	{
	}

	public class VisualMesh : Visual, IVisualMesh
	{
		public VisualMesh() : base(Native.ZkVisual_new(VisualType.Mesh))
		{
		}

		internal VisualMesh(UIntPtr handle) : base(handle)
		{
		}
	}

	public interface IVisualMultiResolutionMesh : IVisual
	{
	}

	public class VisualMultiResolutionMesh : Visual, IVisualMultiResolutionMesh
	{
		public VisualMultiResolutionMesh() : base(Native.ZkVisual_new(VisualType.MultiResolutionMesh))
		{
		}

		internal VisualMultiResolutionMesh(UIntPtr handle) : base(handle)
		{
		}
	}

	public interface IVisualParticleEffect : IVisual
	{
	}

	public class VisualParticleEffect : Visual, IVisualParticleEffect
	{
		public VisualParticleEffect() : base(Native.ZkVisual_new(VisualType.ParticleEffect))
		{
		}

		internal VisualParticleEffect(UIntPtr handle) : base(handle)
		{
		}
	}

	public interface IVisualCamera : IVisual
	{
	}

	public class VisualCamera : Visual, IVisualCamera
	{
		public VisualCamera() : base(Native.ZkVisual_new(VisualType.Camera))
		{
		}

		internal VisualCamera(UIntPtr handle) : base(handle)
		{
		}
	}

	public interface IVisualModel : IVisual
	{
	}

	public class VisualModel : Visual, IVisualModel
	{
		public VisualModel() : base(Native.ZkVisual_new(VisualType.Model))
		{
		}

		internal VisualModel(UIntPtr handle) : base(handle)
		{
		}
	}

	public interface IVisualMorphMesh : IVisual
	{
	}

	public class VisualMorphMesh : Visual, IVisualMorphMesh
	{
		public VisualMorphMesh() : base(Native.ZkVisual_new(VisualType.MorphMesh))
		{
		}

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
		public VisualDecal() : base(Native.ZkVisual_new(VisualType.Decal))
		{
		}

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
		public Matrix3x3 Rotation { get; set; }
		public bool ShowVisual { get; set; }
		public SpriteAlignment SpriteCameraFacingMode { get; set; }
		public bool Static { get; set; }
		public VirtualObjectType Type { get; }
		public IVisual? Visual { get; set; }

		public IVirtualObject GetChild(int i);

		public void AddChild(IVirtualObject obj);

		public void RemoveChild(int i);

		public void RemoveChildren(Predicate<IVirtualObject> pred);
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
		public Matrix3x3 Rotation { get; set; }
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

		public void AddChild(IVirtualObject obj)
		{
			Children.Add(obj);
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

	public enum AiType
	{
		Human = 0,
		Move = 1,
	}

	public class Ai
	{
		internal readonly UIntPtr Handle;

		internal Ai(UIntPtr handle)
		{
			Handle = handle;
		}

		~Ai()
		{
			Native.ZkAi_del(Handle);
		}

		public AiType Type => Native.ZkAi_getType(Handle);

		internal static Ai? FromNative(UIntPtr handle)
		{
			if (handle == UIntPtr.Zero) return null;

			return Native.ZkAi_getType(handle) switch
			{
				AiType.Human => new AiHuman(handle),
				AiType.Move => new AiMove(handle),
				_ => null
			};
		}
	}

	public class AiHuman : Ai
	{
		public AiHuman() : base(Native.ZkAi_new(AiType.Human))
		{
		}

		internal AiHuman(UIntPtr handle) : base(handle)
		{
		}

		public int WaterLevel
		{
			get => Native.ZkAiHuman_getWaterLevel(Handle);
			set => Native.ZkAiHuman_setWaterLevel(Handle, value);
		}

		public float FloorY
		{
			get => Native.ZkAiHuman_getFloorY(Handle);
			set => Native.ZkAiHuman_setFloorY(Handle, value);
		}

		public float WaterY
		{
			get => Native.ZkAiHuman_getWaterY(Handle);
			set => Native.ZkAiHuman_setWaterY(Handle, value);
		}

		public float CeilY
		{
			get => Native.ZkAiHuman_getCeilY(Handle);
			set => Native.ZkAiHuman_setCeilY(Handle, value);
		}

		public float FeetY
		{
			get => Native.ZkAiHuman_getFeetY(Handle);
			set => Native.ZkAiHuman_setFeetY(Handle, value);
		}

		public float HeadY
		{
			get => Native.ZkAiHuman_getHeadY(Handle);
			set => Native.ZkAiHuman_setHeadY(Handle, value);
		}

		public float FallDistY
		{
			get => Native.ZkAiHuman_getFallDistY(Handle);
			set => Native.ZkAiHuman_setFallDistY(Handle, value);
		}

		public float FallStartY
		{
			get => Native.ZkAiHuman_getFallStartY(Handle);
			set => Native.ZkAiHuman_setFallStartY(Handle, value);
		}

		public Npc? Npc
		{
			get
			{
				var val = Native.ZkAiHuman_getNpc(Handle);
				if (val == UIntPtr.Zero) return null;
				return new Npc(Native.ZkObject_takeRef(val));
			}
			set => Native.ZkAiHuman_setNpc(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public int WalkMode
		{
			get => Native.ZkAiHuman_getWalkMode(Handle);
			set => Native.ZkAiHuman_setWalkMode(Handle, value);
		}

		public int WeaponMode
		{
			get => Native.ZkAiHuman_getWeaponMode(Handle);
			set => Native.ZkAiHuman_setWeaponMode(Handle, value);
		}

		public int WModeAst
		{
			get => Native.ZkAiHuman_getWmodeAst(Handle);
			set => Native.ZkAiHuman_setWmodeAst(Handle, value);
		}

		public int WModeSelect
		{
			get => Native.ZkAiHuman_getWmodeSelect(Handle);
			set => Native.ZkAiHuman_setWmodeSelect(Handle, value);
		}

		public bool ChangeWeapon
		{
			get => Native.ZkAiHuman_getChangeWeapon(Handle);
			set => Native.ZkAiHuman_setChangeWeapon(Handle, value);
		}

		public int ActionMode
		{
			get => Native.ZkAiHuman_getActionMode(Handle);
			set => Native.ZkAiHuman_setActionMode(Handle, value);
		}
	}

	public class AiMove : Ai
	{
		public AiMove() : base(Native.ZkAi_new(AiType.Move))
		{
		}

		internal AiMove(UIntPtr handle) : base(handle)
		{
		}

		public VirtualObject? Vob
		{
			get
			{
				var val = Native.ZkAiMove_getVob(Handle);
				return VirtualObject.FromNative(Native.ZkObject_takeRef(val));
			}
			set => Native.ZkAiMove_setVob(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public Npc? Owner
		{
			get
			{
				var val = Native.ZkAiMove_getOwner(Handle);
				return new Npc(Native.ZkObject_takeRef(val));
			}
			set => Native.ZkAiMove_setOwner(Handle, value?.Handle ?? UIntPtr.Zero);
		}
	}

	public class EventManager
	{
		internal readonly UIntPtr Handle;

		public EventManager()
		{
			Handle = Native.ZkEventManager_new();
		}

		internal EventManager(UIntPtr handle)
		{
			Handle = handle;
		}

		~EventManager()
		{
			Native.ZkEventManager_del(Handle);
		}

		public bool Cleared
		{
			get => Native.ZkEventManager_getCleared(Handle);
			set => Native.ZkEventManager_setCleared(Handle, value);
		}

		public bool Active
		{
			get => Native.ZkEventManager_getActive(Handle);
			set => Native.ZkEventManager_setActive(Handle, value);
		}
	}

	public class VirtualObject : IVirtualObject
	{
		internal readonly UIntPtr Handle;

		public VirtualObject()
		{
			Handle = Native.ZkVirtualObject_new(VirtualObjectType.zCVob);
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load virtual object");
		}

		public VirtualObject(Read buf, GameVersion version)
		{
			Handle = Native.ZkVirtualObject_load(buf.Handle, version);
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load virtual object");
		}

		public VirtualObject(string path, GameVersion version)
		{
			Handle = Native.ZkVirtualObject_loadPath(path, version);
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load virtual object");
		}

		internal VirtualObject(UIntPtr handle)
		{
			Handle = handle;
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load virtual object");
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

		public Matrix3x3 Rotation
		{
			get => Native.ZkVirtualObject_getRotation(Handle).ToPublicMatrix();
			set => Native.ZkVirtualObject_setRotation(Handle, Native.Structs.ZkMat3x3.FromPublicMatrix(value));
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
				return Vobs.Visual.FromNative(Native.ZkObject_takeRef(val));
			}
			set
			{
				if (value != null && value.IsCached())
					throw new ArgumentException("Only non-cached visuals are allowed!");

				var v = (Visual?)value;
				Native.ZkVirtualObject_setVisual(Handle, v?.Handle ?? UIntPtr.Zero);
			}
		}

		public byte SleepMode
		{
			get => Native.ZkVirtualObject_getSleepMode(Handle);
			set => Native.ZkVirtualObject_setSleepMode(Handle, value);
		}

		public float NextOnTimer
		{
			get => Native.ZkVirtualObject_getNextOnTimer(Handle);
			set => Native.ZkVirtualObject_setNextOnTimer(Handle, value);
		}

		public Ai? Ai
		{
			get
			{
				var val = Native.ZkVirtualObject_getAi(Handle);
				return Vobs.Ai.FromNative(Native.ZkObject_takeRef(val));
			}
			set => Native.ZkVirtualObject_setAi(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public EventManager? EventManager
		{
			get
			{
				var val = Native.ZkVirtualObject_getEventManager(Handle);
				if (val == UIntPtr.Zero) return null;
				return new EventManager(Native.ZkObject_takeRef(val));
			}
			set => Native.ZkVirtualObject_setEventManager(Handle, value?.Handle ?? UIntPtr.Zero);
		}


		public int ChildCount => (int)Native.ZkVirtualObject_getChildCount(Handle);

		public List<IVirtualObject> Children
		{
			get
			{
				var children = new List<IVirtualObject>();
				var count = ChildCount;
				for (var i = 0; i < count; ++i) children.Add(GetChild(i));
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
			var handle = Native.ZkVirtualObject_getChild(Handle, (ulong)i);
			return FromNative(Native.ZkObject_takeRef(handle));
		}

		public void AddChild(IVirtualObject obj)
		{
			if (obj.IsCached()) throw new ArgumentException("Only non-cached objects are supported!");

			var val = (VirtualObject)obj;
			Native.ZkVirtualObject_addChild(Handle, val.Handle);
		}

		public void RemoveChild(int i)
		{
			Native.ZkVirtualObject_removeChild(Handle, (ulong)i);
		}

		public void RemoveChildren(Predicate<IVirtualObject> pred)
		{
			Native.ZkVirtualObject_removeChildren(Handle, (_, vob) => pred(FromNative(vob)), UIntPtr.Zero);
		}

		protected virtual void Delete()
		{
			Native.ZkVirtualObject_del(Handle);
		}

		~VirtualObject()
		{
			Delete();
		}

		public static VirtualObject? FromNative(UIntPtr ptr)
		{
			if (ptr == UIntPtr.Zero) return null;
			return Native.ZkVirtualObject_getType(ptr) switch
			{
				VirtualObjectType.zCVobLevelCompo => new Level(ptr),
				VirtualObjectType.zCVobSpot => new Spot(ptr),
				VirtualObjectType.zCVobStair => new Stair(ptr),
				VirtualObjectType.zCVobStartpoint => new StartPoint(ptr),
				VirtualObjectType.zCCSCamera => new CutsceneCamera(ptr),
				VirtualObjectType.zCVobLight => new Light(ptr),
				VirtualObjectType.zCVobAnimate => new Animate(ptr),
				VirtualObjectType.zCCodeMaster => new CodeMaster(ptr),
				VirtualObjectType.zCEarthquake => new Earthquake(ptr),
				VirtualObjectType.oCItem => new Item(ptr),
				VirtualObjectType.zCVobLensFlare => new LensFlare(ptr),
				VirtualObjectType.zCMessageFilter => new MessageFilter(ptr),
				VirtualObjectType.zCMoverController => new MoverController(ptr),
				VirtualObjectType.zCPFXController => new ParticleEffectController(ptr),
				VirtualObjectType.oCTouchDamage => new TouchDamage(ptr),
				VirtualObjectType.oCMobContainer => new Container(ptr),
				VirtualObjectType.oCMobDoor => new Door(ptr),
				VirtualObjectType.oCMobFire => new Fire(ptr),
				VirtualObjectType.oCMobInter => new InteractiveObject(ptr),
				VirtualObjectType.oCMobLadder => new Ladder(ptr),
				VirtualObjectType.oCMobSwitch => new Switch(ptr),
				VirtualObjectType.oCMobWheel => new Wheel(ptr),
				VirtualObjectType.oCMobBed => new Bed(ptr),
				VirtualObjectType.oCMOB => new MovableObject(ptr),
				VirtualObjectType.zCVobSound => new Sound(ptr),
				VirtualObjectType.zCVobSoundDaytime => new SoundDaytime(ptr),
				VirtualObjectType.zCTrigger => new Trigger(ptr),
				VirtualObjectType.oCCSTrigger => new CutsceneTrigger(ptr),
				VirtualObjectType.zCTriggerList => new TriggerList(ptr),
				VirtualObjectType.oCTriggerScript => new TriggerScript(ptr),
				VirtualObjectType.zCMover => new Mover(ptr),
				VirtualObjectType.oCTriggerChangeLevel => new TriggerChangeLevel(ptr),
				VirtualObjectType.zCTriggerWorldStart => new TriggerWorldStart(ptr),
				VirtualObjectType.zCTriggerUntouch => new TriggerUntouch(ptr),
				VirtualObjectType.oCZoneMusic => new ZoneMusic(ptr),
				VirtualObjectType.oCZoneMusicDefault => new ZoneMusicDefault(ptr),
				VirtualObjectType.zCZoneZFog => new ZoneFog(ptr),
				VirtualObjectType.zCZoneZFogDefault => new ZoneFogDefault(ptr),
				VirtualObjectType.zCZoneVobFarPlane => new ZoneFarPlane(ptr),
				VirtualObjectType.zCZoneVobFarPlaneDefault => new ZoneFarPlaneDefault(ptr),
				VirtualObjectType.oCNpc => new Npc(ptr),
				_ => new VirtualObject(ptr)
			};
		}
	}

	public interface ISpot : IVirtualObject
	{
	}

	public class Spot : VirtualObject, ISpot
	{
		internal Spot(UIntPtr handle) : base(handle)
		{
		}
	}

	public interface IStair : IVirtualObject
	{
	}

	public class Stair : VirtualObject, IStair
	{
		internal Stair(UIntPtr handle) : base(handle)
		{
		}
	}

	public interface IStartPoint : IVirtualObject
	{
	}

	public class StartPoint : VirtualObject, IStartPoint
	{
		internal StartPoint(UIntPtr handle) : base(handle)
		{
		}
	}

	public interface ILevel : IVirtualObject
	{
	}

	public class Level : VirtualObject, ILevel
	{
		internal Level(UIntPtr handle) : base(handle)
		{
		}
	}
}
