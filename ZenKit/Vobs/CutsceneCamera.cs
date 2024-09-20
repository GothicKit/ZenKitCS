using System;
using System.Collections.Generic;
using System.Numerics;

namespace ZenKit.Vobs
{
	public enum CameraTrajectory
	{
		World = 0,
		Object = 1
	}

	public enum CameraLoopType
	{
		None = 0,
		Restart = 1,
		PingPong = 2
	}

	public enum CameraLerpType
	{
		Undefined = 0,
		Path = 1,
		PathIgnoreRoll = 2,
		PathRotationSamples = 3
	}

	public enum CameraMotion
	{
		Undefined = 0,
		Smooth = 1,
		Linear = 2,
		Step = 3,
		Slow = 4,
		Fast = 5,
		Custom = 6
	}

	public interface ICameraTrajectoryFrame : IVirtualObject
	{
		float Time { get; set; }
		float RollAngle { get; set; }
		float FovScale { get; set; }
		CameraMotion MotionType { get; set; }
		CameraMotion MotionTypeFov { get; set; }
		CameraMotion MotionTypeRoll { get; set; }
		CameraMotion MotionTypeTimeScale { get; set; }
		float Tension { get; set; }
		float CamBias { get; set; }
		float Continuity { get; set; }
		float TimeScale { get; set; }
		bool TimeFixed { get; set; }
		Matrix4x4 OriginalPose { get; set; }
	}

	public class CameraTrajectoryFrame : VirtualObject, ICameraTrajectoryFrame
	{
		public CameraTrajectoryFrame() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCCamTrj_KeyFrame))
		{
		}

		internal CameraTrajectoryFrame(UIntPtr handle) : base(handle)
		{
		}

		public float Time
		{
			get => Native.ZkCameraTrajectoryFrame_getTime(Handle);
			set => Native.ZkCameraTrajectoryFrame_setTime(Handle, value);
		}

		public float RollAngle
		{
			get => Native.ZkCameraTrajectoryFrame_getRollAngle(Handle);
			set => Native.ZkCameraTrajectoryFrame_setRollAngle(Handle, value);
		}

		public float FovScale
		{
			get => Native.ZkCameraTrajectoryFrame_getFovScale(Handle);
			set => Native.ZkCameraTrajectoryFrame_setFovScale(Handle, value);
		}

		public CameraMotion MotionType
		{
			get => Native.ZkCameraTrajectoryFrame_getMotionType(Handle);
			set => Native.ZkCameraTrajectoryFrame_setMotionType(Handle, value);
		}

		public CameraMotion MotionTypeFov
		{
			get => Native.ZkCameraTrajectoryFrame_getMotionTypeFov(Handle);
			set => Native.ZkCameraTrajectoryFrame_setMotionTypeFov(Handle, value);
		}

		public CameraMotion MotionTypeRoll
		{
			get => Native.ZkCameraTrajectoryFrame_getMotionTypeRoll(Handle);
			set => Native.ZkCameraTrajectoryFrame_setMotionTypeRoll(Handle, value);
		}

		public CameraMotion MotionTypeTimeScale
		{
			get => Native.ZkCameraTrajectoryFrame_getMotionTypeTimeScale(Handle);
			set => Native.ZkCameraTrajectoryFrame_setMotionTypeTimeScale(Handle, value);
		}

		public float Tension
		{
			get => Native.ZkCameraTrajectoryFrame_getTension(Handle);
			set => Native.ZkCameraTrajectoryFrame_setTension(Handle, value);
		}

		public float CamBias
		{
			get => Native.ZkCameraTrajectoryFrame_getCamBias(Handle);
			set => Native.ZkCameraTrajectoryFrame_setCamBias(Handle, value);
		}

		public float Continuity
		{
			get => Native.ZkCameraTrajectoryFrame_getContinuity(Handle);
			set => Native.ZkCameraTrajectoryFrame_setContinuity(Handle, value);
		}

		public float TimeScale
		{
			get => Native.ZkCameraTrajectoryFrame_getTimeScale(Handle);
			set => Native.ZkCameraTrajectoryFrame_setTimeScale(Handle, value);
		}

		public bool TimeFixed
		{
			get => Native.ZkCameraTrajectoryFrame_getTimeFixed(Handle);
			set => Native.ZkCameraTrajectoryFrame_setTimeFixed(Handle, value);
		}

		public Matrix4x4 OriginalPose
		{
			get => Native.ZkCameraTrajectoryFrame_getOriginalPose(Handle).ToCSharp();
			set => Native.ZkCameraTrajectoryFrame_setOriginalPose(Handle, new Native.Structs.ZkMat4x4(value));
		}

		protected override void Delete()
		{
		}
	}

	public interface ICutsceneCamera : IVirtualObject
	{
		CameraTrajectory TrajectoryFOR { get; set; }
		CameraTrajectory TargetTrajectoryFOR { get; set; }
		CameraLoopType LoopMode { get; set; }
		CameraLerpType LerpMode { get; set; }
		bool IgnoreFORVobRotation { get; set; }
		bool IgnoreFORVobRotationTarget { get; set; }
		bool Adapt { get; set; }
		bool EaseFirst { get; set; }
		bool EaseLast { get; set; }
		float TotalDuration { get; set; }
		string AutoFocusVob { get; set; }
		bool AutoPlayerMovable { get; set; }
		bool AutoUntriggerLast { get; set; }
		float AutoUntriggerLastDelay { get; set; }
		int PositionCount { get; }
		int TargetCount { get; }
		int FrameCount { get; }
		List<ICameraTrajectoryFrame> Frames { get; }
		ICameraTrajectoryFrame GetFrame(int i);
	}

	public class CutsceneCamera : VirtualObject, ICutsceneCamera
	{
		public CutsceneCamera() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCCSCamera))
		{
		}

		public CutsceneCamera(Read buf, GameVersion version) : base(Native.ZkCutsceneCamera_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load camera vob");
		}

		public CutsceneCamera(string path, GameVersion version) : base(Native.ZkCutsceneCamera_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load camera vob");
		}

		internal CutsceneCamera(UIntPtr handle) : base(handle)
		{
		}

		public CameraTrajectory TrajectoryFOR
		{
			get => Native.ZkCutsceneCamera_getTrajectoryFOR(Handle);
			set => Native.ZkCutsceneCamera_setTrajectoryFOR(Handle, value);
		}

		public CameraTrajectory TargetTrajectoryFOR
		{
			get => Native.ZkCutsceneCamera_getTargetTrajectoryFOR(Handle);
			set => Native.ZkCutsceneCamera_setTargetTrajectoryFOR(Handle, value);
		}

		public CameraLoopType LoopMode
		{
			get => Native.ZkCutsceneCamera_getLoopMode(Handle);
			set => Native.ZkCutsceneCamera_setLoopMode(Handle, value);
		}

		public CameraLerpType LerpMode
		{
			get => Native.ZkCutsceneCamera_getLerpMode(Handle);
			set => Native.ZkCutsceneCamera_setLerpMode(Handle, value);
		}

		public bool IgnoreFORVobRotation
		{
			get => Native.ZkCutsceneCamera_getIgnoreFORVobRotation(Handle);
			set => Native.ZkCutsceneCamera_setIgnoreFORVobRotation(Handle, value);
		}

		public bool IgnoreFORVobRotationTarget
		{
			get => Native.ZkCutsceneCamera_getIgnoreFORVobRotationTarget(Handle);
			set => Native.ZkCutsceneCamera_setIgnoreFORVobRotationTarget(Handle, value);
		}

		public bool Adapt
		{
			get => Native.ZkCutsceneCamera_getAdapt(Handle);
			set => Native.ZkCutsceneCamera_setAdapt(Handle, value);
		}

		public bool EaseFirst
		{
			get => Native.ZkCutsceneCamera_getEaseFirst(Handle);
			set => Native.ZkCutsceneCamera_setEaseFirst(Handle, value);
		}

		public bool EaseLast
		{
			get => Native.ZkCutsceneCamera_getEaseLast(Handle);
			set => Native.ZkCutsceneCamera_setEaseLast(Handle, value);
		}

		public float TotalDuration
		{
			get => Native.ZkCutsceneCamera_getTotalDuration(Handle);
			set => Native.ZkCutsceneCamera_setTotalDuration(Handle, value);
		}

		public string AutoFocusVob
		{
			get =>
				Native.ZkCutsceneCamera_getAutoFocusVob(Handle).MarshalAsString() ??
				throw new Exception("Failed to load cutscene camera auto focus vob");
			set => Native.ZkCutsceneCamera_setAutoFocusVob(Handle, value);
		}

		public bool AutoPlayerMovable
		{
			get => Native.ZkCutsceneCamera_getAutoPlayerMovable(Handle);
			set => Native.ZkCutsceneCamera_setAutoPlayerMovable(Handle, value);
		}

		public bool AutoUntriggerLast
		{
			get => Native.ZkCutsceneCamera_getAutoUntriggerLast(Handle);
			set => Native.ZkCutsceneCamera_setAutoUntriggerLast(Handle, value);
		}

		public float AutoUntriggerLastDelay
		{
			get => Native.ZkCutsceneCamera_getAutoUntriggerLastDelay(Handle);
			set => Native.ZkCutsceneCamera_setAutoUntriggerLastDelay(Handle, value);
		}

		public int PositionCount => Native.ZkCutsceneCamera_getPositionCount(Handle);
		public int TargetCount => Native.ZkCutsceneCamera_getTargetCount(Handle);
		public int FrameCount => (int)Native.ZkCutsceneCamera_getFrameCount(Handle);

		public List<ICameraTrajectoryFrame> Frames
		{
			get
			{
				var frames = new List<ICameraTrajectoryFrame>();
				var count = FrameCount;
				for (var i = 0; i < count; ++i) frames.Add(GetFrame(i));
				return frames;
			}
		}

		protected override void Delete()
		{
			Native.ZkCutsceneCamera_del(Handle);
		}

		public ICameraTrajectoryFrame GetFrame(int i)
		{
			var handle = Native.ZkCutsceneCamera_getFrame(Handle, (ulong)i);
			return new CameraTrajectoryFrame(Native.ZkObject_takeRef(handle));
		}
	}
}
