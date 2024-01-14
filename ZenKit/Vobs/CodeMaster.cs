using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit.Vobs
{
	public class CodeMaster : VirtualObject
	{
		private static readonly Native.Callbacks.ZkStringEnumerator RemoveSlavesEnumerator = _enumerateSlavesHandler;

		public CodeMaster() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCCodeMaster))
		{
		}

		public CodeMaster(Read buf, GameVersion version) : base(Native.ZkCodeMaster_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load code master vob");
		}

		public CodeMaster(string path, GameVersion version) : base(Native.ZkCodeMaster_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load code master vob");
		}

		internal CodeMaster(UIntPtr handle) : base(handle)
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

		public int SlaveCount => (int)Native.ZkCodeMaster_getSlaveCount(Handle);

		public List<string> Slaves
		{
			get
			{
				var slaves = new List<string>();
				var count = SlaveCount;
				for (var i = 0; i < count; ++i) slaves.Add(GetSlave(i));
				return slaves;
			}
		}

		public string GetSlave(int i)
		{
			return Native.ZkCodeMaster_getSlave(Handle, (ulong)i).MarshalAsString() ??
			       throw new Exception("Failed to load code master vob slave");
		}

		public void AddSlave(string slave)
		{
			Native.ZkCodeMaster_addSlave(Handle, slave);
		}

		public void RemoveSlave(int i)
		{
			Native.ZkCodeMaster_removeSlave(Handle, (ulong)i);
		}

		public void RemoveSlaves(Predicate<string> pred)
		{
			var gch = GCHandle.Alloc(pred);
			Native.ZkCodeMaster_removeSlaves(Handle, RemoveSlavesEnumerator, GCHandle.ToIntPtr(gch));
			gch.Free();
		}

		protected override void Delete()
		{
			Native.ZkCodeMaster_del(Handle);
		}

		[MonoPInvokeCallback]
		private static bool _enumerateSlavesHandler(IntPtr ctx, IntPtr ptr)
		{
			var cb = (Predicate<string>)GCHandle.FromIntPtr(ctx).Target;
			return cb(ptr.MarshalAsString()!);
		}
	}
}