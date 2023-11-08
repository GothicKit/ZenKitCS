namespace ZenKit.Vobs;

public enum MoverBehavior
{
	Toggle = 0,
	TriggerControl = 1,
	OpenTime = 2,
	Loop = 3,
	SingleKeys = 4
}

public enum MoverLerpType
{
	Curve = 0,
	Linear = 1
}

public enum MoverSpeedType
{
	Constant = 0,
	SlowStartEnd = 1,
	SlowStart = 2,
	SlowEnd = 3,
	SegmentSlowStartEnd = 4,
	SegmentSlowStart = 5,
	SegmentSlowEnd = 6
}

public class Mover : Trigger
{
	public Mover(Read buf, GameVersion version) : base(Native.ZkMover_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Mover vob");
	}

	public Mover(string path, GameVersion version) : base(Native.ZkMover_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Mover vob");
	}

	internal Mover(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public MoverBehavior Behavior => Native.ZkMover_getBehavior(Handle);
	public float TouchBlockerDamage => Native.ZkMover_getTouchBlockerDamage(Handle);
	public TimeSpan StayOpenTime => TimeSpan.FromSeconds(Native.ZkMover_getStayOpenTimeSeconds(Handle));
	public bool IsLocked => Native.ZkMover_getIsLocked(Handle);
	public bool AutoLink => Native.ZkMover_getAutoLink(Handle);
	public bool AutoRotate => Native.ZkMover_getAutoRotate(Handle);
	public float Speed => Native.ZkMover_getSpeed(Handle);
	public MoverLerpType LerpType => Native.ZkMover_getLerpType(Handle);
	public MoverSpeedType SpeedType => Native.ZkMover_getSpeedType(Handle);

	public AnimationSample[] Keyframes =>
		Native.ZkMover_getKeyframes(Handle, out var count).MarshalAsArray<AnimationSample>(count);

	public string SfxOpenStart => Native.ZkMover_getSfxOpenStart(Handle).MarshalAsString() ?? string.Empty;
	public string SfxOpenEnd => Native.ZkMover_getSfxOpenEnd(Handle).MarshalAsString() ?? string.Empty;
	public string SfxTransitioning => Native.ZkMover_getSfxTransitioning(Handle).MarshalAsString() ?? string.Empty;
	public string SfxCloseStart => Native.ZkMover_getSfxCloseStart(Handle).MarshalAsString() ?? string.Empty;
	public string SfxCloseEnd => Native.ZkMover_getSfxCloseEnd(Handle).MarshalAsString() ?? string.Empty;
	public string SfxLock => Native.ZkMover_getSfxLock(Handle).MarshalAsString() ?? string.Empty;
	public string SfxUnlock => Native.ZkMover_getSfxUnlock(Handle).MarshalAsString() ?? string.Empty;
	public string SfxUseLocked => Native.ZkMover_getSfxUseLocked(Handle).MarshalAsString() ?? string.Empty;

	protected override void Delete()
	{
		Native.ZkMover_del(Handle);
	}
}