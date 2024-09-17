using System;
using System.Collections.Generic;
using System.Numerics;

namespace ZenKit.Vobs
{
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

	public interface IMover : ITrigger
	{
		MoverBehavior Behavior { get; set; }
		float TouchBlockerDamage { get; set; }
		TimeSpan StayOpenTime { get; set; }
		bool IsLocked { get; set; }
		bool AutoLink { get; set; }
		bool AutoRotate { get; set; }
		float Speed { get; set; }
		MoverLerpType LerpType { get; set; }
		MoverSpeedType SpeedType { get; set; }
		Vector3 ActKeyPosDelta { get; set; }
		float ActKeyframeF { get; set; }
		int ActKeyframe { get; set; }
		int NextKeyframe { get; set; }
		float MoveSpeedUnit { get; set; }
		float AdvanceDir { get; set; }
		int MoverState { get; set; }
		int TriggerEventCount { get; set; }
		float StayOpenTimeDest { get; set; }
		int KeyframeCount { get; }
		List<AnimationSample> Keyframes { get; }
		string SfxOpenStart { get; set; }
		string SfxOpenEnd { get; set; }
		string SfxTransitioning { get; set; }
		string SfxCloseStart { get; set; }
		string SfxCloseEnd { get; set; }
		string SfxLock { get; set; }
		string SfxUnlock { get; set; }
		string SfxUseLocked { get; set; }
	}

	public class Mover : Trigger, IMover
	{
		public Mover() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCMover))
		{
		}

		public Mover(Read buf, GameVersion version) : base(Native.ZkMover_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Mover vob");
		}

		public Mover(string path, GameVersion version) : base(Native.ZkMover_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Mover vob");
		}

		internal Mover(UIntPtr handle) : base(handle)
		{
		}

		public MoverBehavior Behavior
		{
			get => Native.ZkMover_getBehavior(Handle);
			set => Native.ZkMover_setBehavior(Handle, value);
		}

		public float TouchBlockerDamage
		{
			get => Native.ZkMover_getTouchBlockerDamage(Handle);
			set => Native.ZkMover_setTouchBlockerDamage(Handle, value);
		}

		public TimeSpan StayOpenTime
		{
			get => TimeSpan.FromSeconds(Native.ZkMover_getStayOpenTimeSeconds(Handle));
			set => Native.ZkMover_setStayOpenTimeSeconds(Handle, (float)value.TotalSeconds);
		}

		public bool IsLocked
		{
			get => Native.ZkMover_getIsLocked(Handle);
			set => Native.ZkMover_setIsLocked(Handle, value);
		}

		public bool AutoLink
		{
			get => Native.ZkMover_getAutoLink(Handle);
			set => Native.ZkMover_setAutoLink(Handle, value);
		}

		public bool AutoRotate
		{
			get => Native.ZkMover_getAutoRotate(Handle);
			set => Native.ZkMover_setAutoRotate(Handle, value);
		}

		public float Speed
		{
			get => Native.ZkMover_getSpeed(Handle);
			set => Native.ZkMover_setSpeed(Handle, value);
		}

		public MoverLerpType LerpType
		{
			get => Native.ZkMover_getLerpType(Handle);
			set => Native.ZkMover_setLerpType(Handle, value);
		}

		public MoverSpeedType SpeedType
		{
			get => Native.ZkMover_getSpeedType(Handle);
			set => Native.ZkMover_setSpeedType(Handle, value);
		}

		public Vector3 ActKeyPosDelta
		{
			get => Native.ZkMover_getActKeyPosDelta(Handle);
			set => Native.ZkMover_setActKeyPosDelta(Handle, value);
		}

		public float ActKeyframeF
		{
			get => Native.ZkMover_getActKeyframeF(Handle);
			set => Native.ZkMover_setActKeyframeF(Handle, value);
		}

		public int ActKeyframe
		{
			get => Native.ZkMover_getActKeyframe(Handle);
			set => Native.ZkMover_setActKeyframe(Handle, value);
		}

		public int NextKeyframe
		{
			get => Native.ZkMover_getNextKeyframe(Handle);
			set => Native.ZkMover_setNextKeyframe(Handle, value);
		}

		public float MoveSpeedUnit
		{
			get => Native.ZkMover_getMoveSpeedUnit(Handle);
			set => Native.ZkMover_setMoveSpeedUnit(Handle, value);
		}

		public float AdvanceDir
		{
			get => Native.ZkMover_getAdvanceDir(Handle);
			set => Native.ZkMover_setAdvanceDir(Handle, value);
		}

		public int MoverState
		{
			get => (int)Native.ZkMover_getMoverState(Handle);
			set => Native.ZkMover_setMoverState(Handle, (uint)value);
		}

		public int TriggerEventCount
		{
			get => Native.ZkMover_getTriggerEventCount(Handle);
			set => Native.ZkMover_setTriggerEventCount(Handle, value);
		}

		public float StayOpenTimeDest
		{
			get => Native.ZkMover_getStayOpenTimeDest(Handle);
			set => Native.ZkMover_setStayOpenTimeDest(Handle, value);
		}


		public int KeyframeCount => (int)Native.ZkMover_getKeyframeCount(Handle);

		public List<AnimationSample> Keyframes
		{
			get
			{
				var samples = new List<AnimationSample>();
				var count = KeyframeCount;
				for (var i = 0; i < count; ++i) samples.Add(GetKeyframe(i));
				return samples;
			}
		}


		public string SfxOpenStart
		{
			get => Native.ZkMover_getSfxOpenStart(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMover_setSfxOpenStart(Handle, value);
		}

		public string SfxOpenEnd
		{
			get => Native.ZkMover_getSfxOpenEnd(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMover_setSfxOpenEnd(Handle, value);
		}

		public string SfxTransitioning
		{
			get => Native.ZkMover_getSfxTransitioning(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMover_setSfxTransitioning(Handle, value);
		}

		public string SfxCloseStart
		{
			get => Native.ZkMover_getSfxCloseStart(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMover_setSfxCloseStart(Handle, value);
		}

		public string SfxCloseEnd
		{
			get => Native.ZkMover_getSfxCloseEnd(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMover_setSfxCloseEnd(Handle, value);
		}

		public string SfxLock
		{
			get => Native.ZkMover_getSfxLock(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMover_setSfxLock(Handle, value);
		}

		public string SfxUnlock
		{
			get => Native.ZkMover_getSfxUnlock(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMover_setSfxUnlock(Handle, value);
		}

		public string SfxUseLocked
		{
			get => Native.ZkMover_getSfxUseLocked(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMover_setSfxUseLocked(Handle, value);
		}


		protected override void Delete()
		{
			Native.ZkMover_del(Handle);
		}

		public AnimationSample GetKeyframe(int i)
		{
			return Native.ZkMover_getKeyframe(Handle, (ulong)i);
		}
	}
}
