using System;
using System.Collections.Generic;

namespace ZenKit.Vobs
{
	public enum TriggerBatchMode
	{
		All = 0,
		Next = 1,
		Random = 2
	}

	public class TriggerListTarget
	{
		private readonly UIntPtr _handle;

		public TriggerListTarget(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name
		{
			get => Native.ZkTriggerListTarget_getName(_handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkTriggerListTarget_setName(_handle, value);
		}

		public TimeSpan Delay
		{
			get => TimeSpan.FromSeconds(Native.ZkTriggerListTarget_getDelaySeconds(_handle));
			set => Native.ZkTriggerListTarget_setDelaySeconds(_handle, (float)value.TotalSeconds);
		}
	}

	public class TriggerList : Trigger
	{
		public TriggerList(Read buf, GameVersion version) : base(Native.ZkTriggerList_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerList vob");
		}

		public TriggerList(string path, GameVersion version) : base(Native.ZkTriggerList_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerList vob");
		}

		internal TriggerList(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public TriggerBatchMode Mode
		{
			get => Native.ZkTriggerList_getMode(Handle);
			set => Native.ZkTriggerList_setMode(Handle, value);
		}

		public int TargetCount => (int)Native.ZkTriggerList_getTargetCount(Handle);

		public List<TriggerListTarget> Targets
		{
			get
			{
				var targets = new List<TriggerListTarget>();
				var count = TargetCount;
				for (var i = 0;i < count; ++i) targets.Add(GetTarget(i));
				return targets;
			}
		}

		public TriggerListTarget GetTarget(int i)
		{
			return new TriggerListTarget(Native.ZkTriggerList_getTarget(Handle, (ulong)i));
		}

		public TriggerListTarget AddTarget()
		{
			return new TriggerListTarget(Native.ZkTriggerList_addTarget(Handle));
		}

		public void RemoveTarget(int i)
		{
			Native.ZkTriggerList_removeTarget(Handle, (ulong)i);
		}

		public void RemoveTargets(Predicate<TriggerListTarget> pred)
		{
			Native.ZkTriggerList_removeTargets(Handle, (_, ptr) => pred(new TriggerListTarget(ptr)), UIntPtr.Zero);
		}

		protected override void Delete()
		{
			Native.ZkTriggerList_del(Handle);
		}
	}
}