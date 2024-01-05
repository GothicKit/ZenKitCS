using System;

namespace ZenKit.Vobs
{
	public class Door : InteractiveObject
	{
		public Door(Read buf, GameVersion version) : base(Native.ZkDoor_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Door vob");
		}

		public Door(string path, GameVersion version) : base(Native.ZkDoor_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Door vob");
		}

		internal Door(UIntPtr handle) : base(handle)
		{
		}


		public bool IsLocked
		{
			get => Native.ZkDoor_getIsLocked(Handle);
			set => Native.ZkDoor_setIsLocked(Handle, value);
		}

		public string Key
		{
			get => Native.ZkDoor_getKey(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkDoor_setKey(Handle, value);
		}

		public string PickString
		{
			get => Native.ZkDoor_getPickString(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkDoor_setPickString(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkDoor_del(Handle);
		}
	}
}