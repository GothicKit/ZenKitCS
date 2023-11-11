using System;

namespace ZenKit.Vobs
{
	public enum TouchCollisionType
	{
		None = 0,
		Box = 1,
		Point = 2
	}

	public class TouchDamage : VirtualObject
	{
		public TouchDamage(Read buf, GameVersion version) : base(Native.ZkTouchDamage_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load touch damage vob");
		}

		public TouchDamage(string path, GameVersion version) : base(Native.ZkTouchDamage_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load touch damage vob");
		}

		internal TouchDamage(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public float Damage => Native.ZkTouchDamage_getDamage(Handle);
		public bool IsBarrier => Native.ZkTouchDamage_getIsBarrier(Handle);
		public bool IsBlunt => Native.ZkTouchDamage_getIsBlunt(Handle);
		public bool IsEdge => Native.ZkTouchDamage_getIsEdge(Handle);
		public bool IsFire => Native.ZkTouchDamage_getIsFire(Handle);
		public bool IsFly => Native.ZkTouchDamage_getIsFly(Handle);
		public bool IsMagic => Native.ZkTouchDamage_getIsMagic(Handle);
		public bool IsPoint => Native.ZkTouchDamage_getIsPoint(Handle);
		public bool IsFall => Native.ZkTouchDamage_getIsFall(Handle);
		public TimeSpan RepeatDelay => TimeSpan.FromSeconds(Native.ZkTouchDamage_getRepeatDelaySeconds(Handle));
		public float VolumeScale => Native.ZkTouchDamage_getVolumeScale(Handle);
		public TouchCollisionType CollisionType => Native.ZkTouchDamage_getCollisionType(Handle);


		protected override void Delete()
		{
			Native.ZkTouchDamage_del(Handle);
		}
	}
}