using System;

namespace ZenKit.Vobs
{
	public enum SoundMaterialType
	{
		Wood = 0,
		Stone = 1,
		Metal = 2,
		Leather = 3,
		Clay = 4,
		Glass = 5
	}

	public class MovableObject : VirtualObject
	{
		public MovableObject(Read buf, GameVersion version) : base(Native.ZkMovableObject_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load MovableObject vob");
		}

		public MovableObject(string path, GameVersion version) : base(Native.ZkMovableObject_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load MovableObject vob");
		}

		internal MovableObject(UIntPtr handle) : base(handle)
		{
		}

		public string FocusName
		{
			get => Native.ZkMovableObject_getName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMovableObject_setName(Handle, value);
		}

		public int Hp
		{
			get => Native.ZkMovableObject_getHp(Handle);
			set => Native.ZkMovableObject_setHp(Handle, value);
		}

		public int Damage
		{
			get => Native.ZkMovableObject_getDamage(Handle);
			set => Native.ZkMovableObject_setDamage(Handle, value);
		}

		public bool Movable
		{
			get => Native.ZkMovableObject_getMovable(Handle);
			set => Native.ZkMovableObject_setMovable(Handle, value);
		}

		public bool Takable
		{
			get => Native.ZkMovableObject_getTakable(Handle);
			set => Native.ZkMovableObject_setTakable(Handle, value);
		}

		public bool FocusOverride
		{
			get => Native.ZkMovableObject_getFocusOverride(Handle);
			set => Native.ZkMovableObject_setFocusOverride(Handle, value);
		}

		public SoundMaterialType Material
		{
			get => Native.ZkMovableObject_getMaterial(Handle);
			set => Native.ZkMovableObject_setMaterial(Handle, value);
		}


		public string VisualDestroyed
		{
			get => Native.ZkMovableObject_getVisualDestroyed(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMovableObject_setVisualDestroyed(Handle, value);
		}


		public string Owner
		{
			get => Native.ZkMovableObject_getOwner(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMovableObject_setOwner(Handle, value);
		}

		public string OwnerGuild
		{
			get => Native.ZkMovableObject_getOwnerGuild(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMovableObject_setOwnerGuild(Handle, value);
		}

		public bool Destroyed
		{
			get => Native.ZkMovableObject_getDestroyed(Handle);
			set => Native.ZkMovableObject_setDestroyed(Handle, value);
		}


		protected override void Delete()
		{
			Native.ZkMovableObject_del(Handle);
		}
	}
}