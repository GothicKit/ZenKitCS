using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace ZenKit.Vobs;

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

public enum SpriteAlignment
{
	None = 0,
	Yaw = 1,
	Full = 2
}

public enum ShadowType
{
	None = 0,
	Blob = 1
}

public enum AnimationType
{
	None = 0,
	Wind = 1,
	WindAlt = 2
}

public enum VisualType
{
	Decal = 0,
	Mesh = 1,
	MultiResolutionMesh = 2,
	ParticleEffect = 3,
	AiCamera = 4,
	Model = 5,
	MorphMesh = 6,
	Unknown = 7
}

public class Decal
{
	private readonly UIntPtr _handle;

	internal Decal(UIntPtr handle)
	{
		_handle = handle;
	}

	public string Name => Native.ZkDecal_getName(_handle).MarshalAsString() ??
	                      throw new Exception("Failed to load decal name");

	public Vector2 Dimension => Native.ZkDecal_getDimension(_handle);
	public Vector2 Offset => Native.ZkDecal_getOffset(_handle);
	public bool TwoSided => Native.ZkDecal_getTwoSided(_handle);
	public AlphaFunction AlphaFunction => Native.ZkDecal_getAlphaFunc(_handle);
	public float TextureAnimationFps => Native.ZkDecal_getTextureAnimFps(_handle);
	public byte AlphaWeight => Native.ZkDecal_getAlphaWeight(_handle);
	public bool IgnoreDaylight => Native.ZkDecal_getIgnoreDaylight(_handle);
}

public class VirtualObject
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

	public string VisualName => Native.ZkVirtualObject_getVisualName(Handle).MarshalAsString() ??
	                            throw new Exception("Failed to load virtual object visual name");

	public VisualType VisualType => Native.ZkVirtualObject_getVisualType(Handle);

	public Decal? VisualDecal
	{
		get
		{
			var val = Native.ZkVirtualObject_getVisualDecal(Handle);
			return val == UIntPtr.Zero ? null : new Decal(val);
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
			_ => new VirtualObject(ptr, false)
		};
	}
}