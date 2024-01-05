using System;

namespace ZenKit.Vobs
{
	public class Container : InteractiveObject
	{
		public Container(Read buf, GameVersion version) : base(Native.ZkContainer_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Container vob");
		}

		public Container(string path, GameVersion version) : base(Native.ZkContainer_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Container vob");
		}

		internal Container(UIntPtr handle) : base(handle)
		{
		}

		public bool IsLocked
		{
			get => Native.ZkContainer_getIsLocked(Handle);
			set => Native.ZkContainer_setIsLocked(Handle, value);
		}

		public string Key
		{
			get => Native.ZkContainer_getKey(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkContainer_setKey(Handle, value);
		}

		public string PickString
		{
			get => Native.ZkContainer_getPickString(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkContainer_setPickString(Handle, value);
		}

		public string Contents
		{
			get => Native.ZkContainer_getContents(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkContainer_setContents(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkContainer_del(Handle);
		}
	}
}