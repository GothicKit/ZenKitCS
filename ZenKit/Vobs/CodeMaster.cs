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

		public string Target => Native.ZkCodeMaster_getTarget(Handle).MarshalAsString() ??
		                        throw new Exception("Failed to load code master vob target");

		public bool Ordered => Native.ZkCodeMaster_getOrdered(Handle);
		public bool FirstFalseIsFailure => Native.ZkCodeMaster_getFirstFalseIsFailure(Handle);

		public string FailureTarget => Native.ZkCodeMaster_getFailureTarget(Handle).MarshalAsString() ??
		                               throw new Exception("Failed to load code master vob failure target");

		public bool UntriggeredCancels => Native.ZkCodeMaster_getUntriggeredCancels(Handle);
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

		public string Slave(ulong i)
		{
			return Native.ZkCodeMaster_getSlave(Handle, i).MarshalAsString() ??
			       throw new Exception("Failed to load code master vob slave");
		}

		protected override void Delete()
		{
			Native.ZkCodeMaster_del(Handle);
		}
	}
}