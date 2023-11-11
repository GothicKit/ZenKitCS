using System;

namespace ZenKit.Daedalus
{
	public class InfoInstance : DaedalusInstance
	{
		public InfoInstance(UIntPtr handle) : base(handle)
		{
		}
	
		public int Npc => Native.ZkInfoInstance_getNpc(Handle);
		public int Nr => Native.ZkInfoInstance_getNr(Handle);
		public int Important => Native.ZkInfoInstance_getImportant(Handle);
		public int Condition => Native.ZkInfoInstance_getCondition(Handle);
		public int Information => Native.ZkInfoInstance_getInformation(Handle);
		public string Description => Native.ZkInfoInstance_getDescription(Handle).MarshalAsString() ?? string.Empty;
		public int Trade => Native.ZkInfoInstance_getTrade(Handle);
		public int Permanent => Native.ZkInfoInstance_getPermanent(Handle);
	}
}