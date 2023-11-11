using System;

namespace ZenKit.Vobs
{
	public class Fire : InteractiveObject
	{
		public Fire(Read buf, GameVersion version) : base(Native.ZkFire_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Fire vob");
		}

		public Fire(string path, GameVersion version) : base(Native.ZkFire_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Fire vob");
		}

		internal Fire(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public string Slot => Native.ZkFire_getSlot(Handle).MarshalAsString() ?? string.Empty;
		public string VobTree => Native.ZkFire_getVobTree(Handle).MarshalAsString() ?? string.Empty;

		protected override void Delete()
		{
			Native.ZkFire_del(Handle);
		}
	}
}