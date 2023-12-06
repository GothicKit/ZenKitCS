using System;

namespace ZenKit.Daedalus
{
	public class CameraInstance : DaedalusInstance
	{
		public CameraInstance(UIntPtr handle) : base(handle)
		{
		}

		public float BestRange
		{
			get => Native.ZkCameraInstance_getBestRange(Handle);
			set => Native.ZkCameraInstance_setBestRange(Handle, value);
		}

		public float MinRange
		{
			get => Native.ZkCameraInstance_getMinRange(Handle);
			set => Native.ZkCameraInstance_setMinRange(Handle, value);
		}

		public float MaxRange
		{
			get => Native.ZkCameraInstance_getMaxRange(Handle);
			set => Native.ZkCameraInstance_setMaxRange(Handle, value);
		}

		public float BestElevation
		{
			get => Native.ZkCameraInstance_getBestElevation(Handle);
			set => Native.ZkCameraInstance_setBestElevation(Handle, value);
		}

		public float MinElevation
		{
			get => Native.ZkCameraInstance_getMinElevation(Handle);
			set => Native.ZkCameraInstance_setMinElevation(Handle, value);
		}

		public float MaxElevation
		{
			get => Native.ZkCameraInstance_getMaxElevation(Handle);
			set => Native.ZkCameraInstance_setMaxElevation(Handle, value);
		}

		public float BestAzimuth
		{
			get => Native.ZkCameraInstance_getBestAzimuth(Handle);
			set => Native.ZkCameraInstance_setBestAzimuth(Handle, value);
		}

		public float MinAzimuth
		{
			get => Native.ZkCameraInstance_getMinAzimuth(Handle);
			set => Native.ZkCameraInstance_setMinAzimuth(Handle, value);
		}

		public float MaxAzimuth
		{
			get => Native.ZkCameraInstance_getMaxAzimuth(Handle);
			set => Native.ZkCameraInstance_setMaxAzimuth(Handle, value);
		}

		public float BestRotZ
		{
			get => Native.ZkCameraInstance_getBestRotZ(Handle);
			set => Native.ZkCameraInstance_setBestRotZ(Handle, value);
		}

		public float MinRotZ
		{
			get => Native.ZkCameraInstance_getMinRotZ(Handle);
			set => Native.ZkCameraInstance_setMinRotZ(Handle, value);
		}

		public float MaxRotZ
		{
			get => Native.ZkCameraInstance_getMaxRotZ(Handle);
			set => Native.ZkCameraInstance_setMaxRotZ(Handle, value);
		}

		public float RotOffsetX
		{
			get => Native.ZkCameraInstance_getRotOffsetX(Handle);
			set => Native.ZkCameraInstance_setRotOffsetX(Handle, value);
		}

		public float RotOffsetY
		{
			get => Native.ZkCameraInstance_getRotOffsetY(Handle);
			set => Native.ZkCameraInstance_setRotOffsetY(Handle, value);
		}

		public float RotOffsetZ
		{
			get => Native.ZkCameraInstance_getRotOffsetZ(Handle);
			set => Native.ZkCameraInstance_setRotOffsetZ(Handle, value);
		}

		public float TargetOffsetX
		{
			get => Native.ZkCameraInstance_getTargetOffsetX(Handle);
			set => Native.ZkCameraInstance_setTargetOffsetX(Handle, value);
		}

		public float TargetOffsetY
		{
			get => Native.ZkCameraInstance_getTargetOffsetY(Handle);
			set => Native.ZkCameraInstance_setTargetOffsetY(Handle, value);
		}

		public float TargetOffsetZ
		{
			get => Native.ZkCameraInstance_getTargetOffsetZ(Handle);
			set => Native.ZkCameraInstance_setTargetOffsetZ(Handle, value);
		}

		public float VelocityTrans
		{
			get => Native.ZkCameraInstance_getVeloTrans(Handle);
			set => Native.ZkCameraInstance_setVeloTrans(Handle, value);
		}

		public float VelocityRot
		{
			get => Native.ZkCameraInstance_getVeloRot(Handle);
			set => Native.ZkCameraInstance_setVeloRot(Handle, value);
		}

		public int Translate
		{
			get => Native.ZkCameraInstance_getTranslate(Handle);
			set => Native.ZkCameraInstance_setTranslate(Handle, value);
		}

		public int Rotate
		{
			get => Native.ZkCameraInstance_getRotate(Handle);
			set => Native.ZkCameraInstance_setRotate(Handle, value);
		}

		public int Collision
		{
			get => Native.ZkCameraInstance_getCollision(Handle);
			set => Native.ZkCameraInstance_setCollision(Handle, value);
		}
	}
}