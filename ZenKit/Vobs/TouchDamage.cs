using System;

namespace ZenKit.Vobs
{
	public enum TouchCollisionType
	{
		None = 0,
		Box = 1,
		Point = 2
	}

	public interface ITouchDamage : IVirtualObject
	{
		float Damage { get; set; }
		bool IsBarrier { get; set; }
		bool IsBlunt { get; set; }
		bool IsEdge { get; set; }
		bool IsFire { get; set; }
		bool IsFly { get; set; }
		bool IsMagic { get; set; }
		bool IsPoint { get; set; }
		bool IsFall { get; set; }
		TimeSpan RepeatDelay { get; set; }
		float VolumeScale { get; set; }
		TouchCollisionType CollisionType { get; set; }
	}

	public class TouchDamage : VirtualObject, ITouchDamage
	{
		public TouchDamage() : base(Native.ZkVirtualObject_new(VirtualObjectType.oCTouchDamage))
		{
		}

		public TouchDamage(Read buf, GameVersion version) : base(Native.ZkTouchDamage_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load touch damage vob");
		}

		public TouchDamage(string path, GameVersion version) : base(Native.ZkTouchDamage_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load touch damage vob");
		}

		internal TouchDamage(UIntPtr handle) : base(handle)
		{
		}

		public float Damage
		{
			get => Native.ZkTouchDamage_getDamage(Handle);
			set => Native.ZkTouchDamage_setDamage(Handle, value);
		}

		public bool IsBarrier
		{
			get => Native.ZkTouchDamage_getIsBarrier(Handle);
			set => Native.ZkTouchDamage_setIsBarrier(Handle, value);
		}

		public bool IsBlunt
		{
			get => Native.ZkTouchDamage_getIsBlunt(Handle);
			set => Native.ZkTouchDamage_setIsBlunt(Handle, value);
		}

		public bool IsEdge
		{
			get => Native.ZkTouchDamage_getIsEdge(Handle);
			set => Native.ZkTouchDamage_setIsEdge(Handle, value);
		}

		public bool IsFire
		{
			get => Native.ZkTouchDamage_getIsFire(Handle);
			set => Native.ZkTouchDamage_setIsFire(Handle, value);
		}

		public bool IsFly
		{
			get => Native.ZkTouchDamage_getIsFly(Handle);
			set => Native.ZkTouchDamage_setIsFly(Handle, value);
		}

		public bool IsMagic
		{
			get => Native.ZkTouchDamage_getIsMagic(Handle);
			set => Native.ZkTouchDamage_setIsMagic(Handle, value);
		}

		public bool IsPoint
		{
			get => Native.ZkTouchDamage_getIsPoint(Handle);
			set => Native.ZkTouchDamage_setIsPoint(Handle, value);
		}

		public bool IsFall
		{
			get => Native.ZkTouchDamage_getIsFall(Handle);
			set => Native.ZkTouchDamage_setIsFall(Handle, value);
		}

		public TimeSpan RepeatDelay
		{
			get => TimeSpan.FromSeconds(Native.ZkTouchDamage_getRepeatDelaySeconds(Handle));
			set => Native.ZkTouchDamage_setRepeatDelaySeconds(Handle, (float)value.TotalSeconds);
		}

		public float VolumeScale
		{
			get => Native.ZkTouchDamage_getVolumeScale(Handle);
			set => Native.ZkTouchDamage_setVolumeScale(Handle, value);
		}

		public TouchCollisionType CollisionType
		{
			get => Native.ZkTouchDamage_getCollisionType(Handle);
			set => Native.ZkTouchDamage_setCollisionType(Handle, value);
		}


		protected override void Delete()
		{
			Native.ZkTouchDamage_del(Handle);
		}
	}
}
