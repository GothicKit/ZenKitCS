using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

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

		public ulong KeyframeCount => Native.ZkMover_getKeyframeCount(Handle);

		public List<AnimationSample> Keyframes
		{
			get
			{
				var samples = new List<AnimationSample>();

				Native.ZkMover_enumerateKeyframes(Handle, (_, sample) =>
				{
					samples.Add(Marshal.PtrToStructure<AnimationSample>(sample));
					return false;
				}, UIntPtr.Zero);

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

		public AnimationSample GetKeyframe(ulong i)
		{
			return Native.ZkMover_getKeyframe(Handle, i);
		}
	}
}