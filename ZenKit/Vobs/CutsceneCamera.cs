using System.Numerics;

namespace ZenKit.Vobs;

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

public class CameraTrajectoryFrame : VirtualObject
{
	internal CameraTrajectoryFrame(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public float Time => Native.ZkCameraTrajectoryFrame_getTime(Handle);
	public float RollAngle => Native.ZkCameraTrajectoryFrame_getRollAngle(Handle);
	public float FovScale => Native.ZkCameraTrajectoryFrame_getFovScale(Handle);
	public CameraMotion MotionType => Native.ZkCameraTrajectoryFrame_getMotionType(Handle);
	public CameraMotion MotionTypeFov => Native.ZkCameraTrajectoryFrame_getMotionTypeFov(Handle);
	public CameraMotion MotionTypeRoll => Native.ZkCameraTrajectoryFrame_getMotionTypeRoll(Handle);
	public CameraMotion MotionTypeTimeScale => Native.ZkCameraTrajectoryFrame_getMotionTypeTimeScale(Handle);
	public float Tension => Native.ZkCameraTrajectoryFrame_getTension(Handle);
	public float CamBias => Native.ZkCameraTrajectoryFrame_getCamBias(Handle);
	public float Continuity => Native.ZkCameraTrajectoryFrame_getContinuity(Handle);
	public float TimeScale => Native.ZkCameraTrajectoryFrame_getTimeScale(Handle);
	public bool TimeFixed => Native.ZkCameraTrajectoryFrame_getTimeFixed(Handle);
	public Matrix4x4 OriginalPose => Native.ZkCameraTrajectoryFrame_getOriginalPose(Handle).ToCSharp();

	protected override void Delete()
	{
	}
}

public class CutsceneCamera : VirtualObject
{
	public CutsceneCamera(Read buf, GameVersion version) : base(Native.ZkCutsceneCamera_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load camera vob");
	}

	public CutsceneCamera(string path, GameVersion version) : base(Native.ZkCutsceneCamera_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load camera vob");
	}

	internal CutsceneCamera(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}

	public CameraTrajectory TrajectoryFOR => Native.ZkCutsceneCamera_getTrajectoryFOR(Handle);
	public CameraTrajectory TargetTrajectoryFOR => Native.ZkCutsceneCamera_getTargetTrajectoryFOR(Handle);
	public CameraLoopType LoopMode => Native.ZkCutsceneCamera_getLoopMode(Handle);
	public CameraLerpType LerpMode => Native.ZkCutsceneCamera_getLerpMode(Handle);
	public bool IgnoreFORVobRotation => Native.ZkCutsceneCamera_getIgnoreFORVobRotation(Handle);
	public bool IgnoreFORVobRotationTarget => Native.ZkCutsceneCamera_getIgnoreFORVobRotationTarget(Handle);
	public bool Adapt => Native.ZkCutsceneCamera_getAdapt(Handle);
	public bool EaseFirst => Native.ZkCutsceneCamera_getEaseFirst(Handle);
	public bool EaseLast => Native.ZkCutsceneCamera_getEaseLast(Handle);
	public float TotalDuration => Native.ZkCutsceneCamera_getTotalDuration(Handle);

	public string AutoFocusVob => Native.ZkCutsceneCamera_getAutoFocusVob(Handle).MarshalAsString() ??
	                              throw new Exception("Failed to load cutscene camera auto focus vob");

	public bool AutoPlayerMovable => Native.ZkCutsceneCamera_getAutoPlayerMovable(Handle);
	public bool AutoUntriggerLast => Native.ZkCutsceneCamera_getAutoUntriggerLast(Handle);
	public float AutoUntriggerLastDelay => Native.ZkCutsceneCamera_getAutoUntriggerLastDelay(Handle);
	public int PositionCount => Native.ZkCutsceneCamera_getPositionCount(Handle);
	public int TargetCount => Native.ZkCutsceneCamera_getTargetCount(Handle);
	public ulong FrameCount => Native.ZkCutsceneCamera_getFrameCount(Handle);

	public List<CameraTrajectoryFrame> Frames
	{
		get
		{
			var frames = new List<CameraTrajectoryFrame>();

			Native.ZkCutsceneCamera_enumerateFrames(Handle, (_, frame) =>
			{
				frames.Add(new CameraTrajectoryFrame(frame, false));
				return false;
			}, UIntPtr.Zero);

			return frames;
		}
	}

	protected override void Delete()
	{
		Native.ZkCutsceneCamera_del(Handle);
	}

	public CameraTrajectoryFrame Frame(ulong i)
	{
		return new CameraTrajectoryFrame(Native.ZkCutsceneCamera_getFrame(Handle, i), false);
	}
}