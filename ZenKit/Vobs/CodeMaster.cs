using System;
using System.Collections.Generic;

namespace ZenKit.Vobs
{
	public class CodeMaster : VirtualObject
	{
		public CodeMaster(Read buf, GameVersion version) : base(Native.ZkCodeMaster_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load code master vob");
		}

		public CodeMaster(string path, GameVersion version) : base(Native.ZkCodeMaster_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load code master vob");
		}

		internal CodeMaster(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public string Target
		{
			get =>
				Native.ZkCodeMaster_getTarget(Handle).MarshalAsString() ??
				throw new Exception("Failed to load code master vob target");
			set => Native.ZkCodeMaster_setTarget(Handle, value);
		}

		public bool Ordered
		{
			get => Native.ZkCodeMaster_getOrdered(Handle);
			set => Native.ZkCodeMaster_setOrdered(Handle, value);
		}

		public bool FirstFalseIsFailure
		{
			get => Native.ZkCodeMaster_getFirstFalseIsFailure(Handle);
			set => Native.ZkCodeMaster_setFirstFalseIsFailure(Handle, value);
		}


		public string FailureTarget
		{
			get =>
				Native.ZkCodeMaster_getFailureTarget(Handle).MarshalAsString() ??
				throw new Exception("Failed to load code master vob failure target");
			set => Native.ZkCodeMaster_setFailureTarget(Handle, value);
		}


		public bool UntriggeredCancels
		{
			get => Native.ZkCodeMaster_getUntriggeredCancels(Handle);
			set => Native.ZkCodeMaster_setUntriggeredCancels(Handle, value);
		}

		public ulong SlaveCount => Native.ZkCodeMaster_getSlaveCount(Handle);

		public List<string> Slaves
		{
			get
			{
				var slaves = new List<string>();

				Native.ZkCodeMaster_enumerateSlaves(Handle, (_, v) =>
				{
					slaves.Add(v.MarshalAsString() ?? string.Empty);
					return false;
				}, UIntPtr.Zero);

				return slaves;
			}
		}

		public string GetSlave(ulong i)
		{
			return Native.ZkCodeMaster_getSlave(Handle, i).MarshalAsString() ??
			       throw new Exception("Failed to load code master vob slave");
		}

		public void AddSlave(string slave)
		{
			Native.ZkCodeMaster_addSlave(Handle, slave);
		}

		public void RemoveSlave(ulong i)
		{
			Native.ZkCodeMaster_removeSlave(Handle, i);
		}

		public void RemoveSlaves(Predicate<string> pred)
		{
			Native.ZkCodeMaster_removeSlaves(Handle, (_, ptr) => pred(ptr.MarshalAsString()!), UIntPtr.Zero);
		}

		protected override void Delete()
		{
			Native.ZkCodeMaster_del(Handle);
		}
	}
}