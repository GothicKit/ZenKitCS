using System;

namespace ZenKit.Daedalus
{
	public class ItemReactInstance : DaedalusInstance
	{
		public ItemReactInstance(UIntPtr handle) : base(handle)
		{
		}

		public int Npc
		{
			get => Native.ZkItemReactInstance_getNpc(Handle);
			set => Native.ZkItemReactInstance_setNpc(Handle, value);
		}

		public int TradeItem
		{
			get => Native.ZkItemReactInstance_getTradeItem(Handle);
			set => Native.ZkItemReactInstance_setTradeItem(Handle, value);
		}

		public int TradeAmount
		{
			get => Native.ZkItemReactInstance_getTradeAmount(Handle);
			set => Native.ZkItemReactInstance_setTradeAmount(Handle, value);
		}

		public int RequestedCategory
		{
			get => Native.ZkItemReactInstance_getRequestedCategory(Handle);
			set => Native.ZkItemReactInstance_setRequestedCategory(Handle, value);
		}

		public int RequestedItem
		{
			get => Native.ZkItemReactInstance_getRequestedItem(Handle);
			set => Native.ZkItemReactInstance_setRequestedItem(Handle, value);
		}

		public int RequestedAmount
		{
			get => Native.ZkItemReactInstance_getRequestedAmount(Handle);
			set => Native.ZkItemReactInstance_setRequestedAmount(Handle, value);
		}

		public int Reaction
		{
			get => Native.ZkItemReactInstance_getReaction(Handle);
			set => Native.ZkItemReactInstance_setReaction(Handle, value);
		}
	}
}