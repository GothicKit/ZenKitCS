using System;

namespace ZenKit.Vobs
{
	public class InteractiveObject : MovableObject
	{
		public InteractiveObject(Read buf, GameVersion version) : base(
			Native.ZkInteractiveObject_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load InteractiveObject vob");
		}

		public InteractiveObject(string path, GameVersion version) : base(
			Native.ZkInteractiveObject_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load InteractiveObject vob");
		}

		internal InteractiveObject(UIntPtr handle) : base(handle)
		{
		}

		public int State
		{
			get => Native.ZkInteractiveObject_getState(Handle);
			set => Native.ZkInteractiveObject_setState(Handle, value);
		}

		public string Target
		{
			get => Native.ZkInteractiveObject_getTarget(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkInteractiveObject_setTarget(Handle, value);
		}

		public string Item
		{
			get => Native.ZkInteractiveObject_getItem(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkInteractiveObject_setItem(Handle, value);
		}


		public string ConditionFunction
		{
			get => Native.ZkInteractiveObject_getConditionFunction(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkInteractiveObject_setConditionFunction(Handle, value);
		}


		public string OnStateChangeFunction
		{
			get => Native.ZkInteractiveObject_getOnStateChangeFunction(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkInteractiveObject_setOnStateChangeFunction(Handle, value);
		}


		public bool Rewind
		{
			get => Native.ZkInteractiveObject_getRewind(Handle);
			set => Native.ZkInteractiveObject_setRewind(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkInteractiveObject_del(Handle);
		}
	}

	public class Bed : InteractiveObject
	{
		internal Bed(UIntPtr handle) : base(handle)
		{
		}
	}

	public class Ladder : InteractiveObject
	{
		internal Ladder(UIntPtr handle) : base(handle)
		{
		}
	}

	public class Switch : InteractiveObject
	{
		internal Switch(UIntPtr handle) : base(handle)
		{
		}
	}

	public class Wheel : InteractiveObject
	{
		internal Wheel(UIntPtr handle) : base(handle)
		{
		}
	}
}