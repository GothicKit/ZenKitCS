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

	public interface ITriggerListTarget
	{
		string Name { get; set; }
		TimeSpan Delay { get; set; }
	}

	public class TriggerListTarget : ITriggerListTarget
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

	public interface ITriggerList : ITrigger
	{
		TriggerBatchMode Mode { get; set; }
		byte ActTarget { get; set; }
		bool SendOnTrigger { get; set; }
		int TargetCount { get; }
		List<ITriggerListTarget> Targets { get; }
		ITriggerListTarget GetTarget(int i);
		ITriggerListTarget AddTarget();
		void RemoveTarget(int i);
		void RemoveTargets(Predicate<ITriggerListTarget> pred);
	}

	public class TriggerList : Trigger, ITriggerList
	{
		public TriggerList() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCTriggerList))
		{
		}

		public TriggerList(Read buf, GameVersion version) : base(Native.ZkTriggerList_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerList vob");
		}

		public TriggerList(string path, GameVersion version) : base(Native.ZkTriggerList_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerList vob");
		}

		internal TriggerList(UIntPtr handle) : base(handle)
		{
		}

		public TriggerBatchMode Mode
		{
			get => Native.ZkTriggerList_getMode(Handle);
			set => Native.ZkTriggerList_setMode(Handle, value);
		}

		public byte ActTarget
		{
			get => Native.ZkTriggerList_getActTarget(Handle);
			set => Native.ZkTriggerList_setActTarget(Handle, value);
		}

		public bool SendOnTrigger
		{
			get => Native.ZkTriggerList_getSendOnTrigger(Handle);
			set => Native.ZkTriggerList_setSendOnTrigger(Handle, value);
		}

		public int TargetCount => (int)Native.ZkTriggerList_getTargetCount(Handle);

		public List<ITriggerListTarget> Targets
		{
			get
			{
				var targets = new List<ITriggerListTarget>();
				var count = TargetCount;
				for (var i = 0; i < count; ++i) targets.Add(GetTarget(i));
				return targets;
			}
		}

		public ITriggerListTarget GetTarget(int i)
		{
			return new TriggerListTarget(Native.ZkTriggerList_getTarget(Handle, (ulong)i));
		}

		public ITriggerListTarget AddTarget()
		{
			return new TriggerListTarget(Native.ZkTriggerList_addTarget(Handle));
		}

		public void RemoveTarget(int i)
		{
			Native.ZkTriggerList_removeTarget(Handle, (ulong)i);
		}

		public void RemoveTargets(Predicate<ITriggerListTarget> pred)
		{
			Native.ZkTriggerList_removeTargets(Handle, (_, ptr) => pred(new TriggerListTarget(ptr)), UIntPtr.Zero);
		}

		protected override void Delete()
		{
			Native.ZkTriggerList_del(Handle);
		}
	}
}
