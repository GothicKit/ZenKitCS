using System;

namespace ZenKit.Daedalus
{
	public class CameraInstance : DaedalusInstance
	{
		public CameraInstance(UIntPtr handle) : base(handle)
		{
		}
	
		public float BestRange => Native.ZkCameraInstance_getBestRange(Handle);
		public float MinRange => Native.ZkCameraInstance_getMinRange(Handle);
		public float MaxRange => Native.ZkCameraInstance_getMaxRange(Handle);
		public float BestElevation => Native.ZkCameraInstance_getBestElevation(Handle);
		public float MinElevation => Native.ZkCameraInstance_getMinElevation(Handle);
		public float MaxElevation => Native.ZkCameraInstance_getMaxElevation(Handle);
		public float BestAzimuth => Native.ZkCameraInstance_getBestAzimuth(Handle);
		public float MinAzimuth => Native.ZkCameraInstance_getMinAzimuth(Handle);
		public float MaxAzimuth => Native.ZkCameraInstance_getMaxAzimuth(Handle);
		public float BestRotZ => Native.ZkCameraInstance_getBestRotZ(Handle);
		public float MinRotZ => Native.ZkCameraInstance_getMinRotZ(Handle);
		public float MaxRotZ => Native.ZkCameraInstance_getMaxRotZ(Handle);
		public float RotOffsetX => Native.ZkCameraInstance_getRotOffsetX(Handle);
		public float RotOffsetY => Native.ZkCameraInstance_getRotOffsetY(Handle);
		public float RotOffsetZ => Native.ZkCameraInstance_getRotOffsetZ(Handle);
		public float TargetOffsetX => Native.ZkCameraInstance_getTargetOffsetX(Handle);
		public float TargetOffsetY => Native.ZkCameraInstance_getTargetOffsetY(Handle);
		public float TargetOffsetZ => Native.ZkCameraInstance_getTargetOffsetZ(Handle);
		public float VelocityTrans => Native.ZkCameraInstance_getVeloTrans(Handle);
		public float VelocityRot => Native.ZkCameraInstance_getVeloRot(Handle);
		public int Translate => Native.ZkCameraInstance_getTranslate(Handle);
		public int Rotate => Native.ZkCameraInstance_getRotate(Handle);
		public int Collision => Native.ZkCameraInstance_getCollision(Handle);
	}
}