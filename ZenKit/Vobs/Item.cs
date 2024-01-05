using System;

namespace ZenKit.Vobs
{
	public class Item : VirtualObject
	{
		public Item(Read buf, GameVersion version) : base(Native.ZkItem_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load item vob");
		}

		public Item(string path, GameVersion version) : base(Native.ZkItem_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load item vob");
		}

		internal Item(UIntPtr handle) : base(handle)
		{
		}

		public string Instance
		{
			get => Native.ZkItem_getInstance(Handle).MarshalAsString() ??
			       throw new Exception("Failed to load item instance");
			set => Native.ZkItem_setInstance(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkItem_del(Handle);
		}
	}
}