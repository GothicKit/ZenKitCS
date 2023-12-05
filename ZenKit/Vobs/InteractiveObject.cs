using System;

namespace ZenKit.Vobs
{
	public class InteractiveObject : MovableObject
	{
		public InteractiveObject(Read buf, GameVersion version) : base(
			Native.ZkInteractiveObject_load(buf.Handle, version),
			true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load InteractiveObject vob");
		}

		public InteractiveObject(string path, GameVersion version) : base(
			Native.ZkInteractiveObject_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load InteractiveObject vob");
		}

		internal InteractiveObject(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public int State => Native.ZkInteractiveObject_getState(Handle);
		public string Target => Native.ZkInteractiveObject_getTarget(Handle).MarshalAsString() ?? string.Empty;
		public string Item => Native.ZkInteractiveObject_getItem(Handle).MarshalAsString() ?? string.Empty;

		public string ConditionFunction =>
			Native.ZkInteractiveObject_getConditionFunction(Handle).MarshalAsString() ?? string.Empty;

		public string OnStateChangeFunction =>
			Native.ZkInteractiveObject_getOnStateChangeFunction(Handle).MarshalAsString() ?? string.Empty;

		public bool Rewind => Native.ZkInteractiveObject_getRewind(Handle);

		protected override void Delete()
		{
			Native.ZkInteractiveObject_del(Handle);
		}
	}
	
	public class Bed : InteractiveObject
	{
		public Bed(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public Bed(string path, GameVersion version) : base(path, version)
		{
		}

		internal Bed(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
	
	public class Ladder : InteractiveObject
	{
		public Ladder(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public Ladder(string path, GameVersion version) : base(path, version)
		{
		}

		internal Ladder(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
	
	public class Switch : InteractiveObject
	{
		public Switch(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public Switch(string path, GameVersion version) : base(path, version)
		{
		}

		internal Switch(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
	
	public class Wheel : InteractiveObject
	{
		public Wheel(Read buf, GameVersion version) : base(buf, version)
		{
		}

		public Wheel(string path, GameVersion version) : base(path, version)
		{
		}

		internal Wheel(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}
	}
}