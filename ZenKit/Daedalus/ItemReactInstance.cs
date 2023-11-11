using System;

namespace ZenKit.Daedalus
{
	public class ItemReactInstance : DaedalusInstance
	{
		public ItemReactInstance(UIntPtr handle) : base(handle)
		{
		}

		public int Npc => Native.ZkItemReactInstance_getNpc(Handle);
		public int TradeItem => Native.ZkItemReactInstance_getTradeItem(Handle);
		public int TradeAmount => Native.ZkItemReactInstance_getTradeAmount(Handle);
		public int RequestedCategory => Native.ZkItemReactInstance_getRequestedCategory(Handle);
		public int RequestedItem => Native.ZkItemReactInstance_getRequestedItem(Handle);
		public int RequestedAmount => Native.ZkItemReactInstance_getRequestedAmount(Handle);
		public int Reaction => Native.ZkItemReactInstance_getReaction(Handle);
	}
}