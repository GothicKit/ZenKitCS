using System;

namespace ZenKit.Daedalus
{
	public class InfoInstance : DaedalusInstance
	{
		public InfoInstance(UIntPtr handle) : base(handle)
		{
		}

		public int Npc
		{
			get => Native.ZkInfoInstance_getNpc(Handle);
			set => Native.ZkInfoInstance_setNpc(Handle, value);
		}

		public int Nr
		{
			get => Native.ZkInfoInstance_getNr(Handle);
			set => Native.ZkInfoInstance_setNr(Handle, value);
		}

		public int Important
		{
			get => Native.ZkInfoInstance_getImportant(Handle);
			set => Native.ZkInfoInstance_setImportant(Handle, value);
		}

		public int Condition
		{
			get => Native.ZkInfoInstance_getCondition(Handle);
			set => Native.ZkInfoInstance_setCondition(Handle, value);
		}

		public int Information
		{
			get => Native.ZkInfoInstance_getInformation(Handle);
			set => Native.ZkInfoInstance_setInformation(Handle, value);
		}

		public string Description
		{
			get => Native.ZkInfoInstance_getDescription(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkInfoInstance_setDescription(Handle, value);
		}

		public int Trade
		{
			get => Native.ZkInfoInstance_getTrade(Handle);
			set => Native.ZkInfoInstance_setTrade(Handle, value);
		}

		public int Permanent
		{
			get => Native.ZkInfoInstance_getPermanent(Handle);
			set => Native.ZkInfoInstance_setPermanent(Handle, value);
		}
	}
}