using System;

namespace ZenKit.Vobs
{
	public class Fire : InteractiveObject
	{
		public Fire() : base(Native.ZkVirtualObject_new(VirtualObjectType.oCMobFire))
		{
		}

		public Fire(Read buf, GameVersion version) : base(Native.ZkFire_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Fire vob");
		}

		public Fire(string path, GameVersion version) : base(Native.ZkFire_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Fire vob");
		}

		internal Fire(UIntPtr handle) : base(handle)
		{
		}

		public string Slot
		{
			get => Native.ZkFire_getSlot(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkFire_setSlot(Handle, value);
		}

		public string VobTree
		{
			get => Native.ZkFire_getVobTree(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkFire_setVobTree(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkFire_del(Handle);
		}
	}
}