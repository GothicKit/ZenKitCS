using System;

namespace ZenKit.Daedalus
{
	public class FocusInstance : DaedalusInstance
	{
		public FocusInstance(UIntPtr handle) : base(handle)
		{
		}

		public float NpcLongRange => Native.ZkFocusInstance_getNpcLongrange(Handle);
		public float NpcRange1 => Native.ZkFocusInstance_getNpcRange1(Handle);
		public float NpcRange2 => Native.ZkFocusInstance_getNpcRange2(Handle);
		public float NpcAzi => Native.ZkFocusInstance_getNpcAzi(Handle);
		public float NpcElevationDown => Native.ZkFocusInstance_getNpcElevdo(Handle);
		public float NpcElevationUp => Native.ZkFocusInstance_getNpcElevup(Handle);
		public int NpcPriority => Native.ZkFocusInstance_getNpcPrio(Handle);
		public float ItemRange1 => Native.ZkFocusInstance_getItemRange1(Handle);
		public float ItemRange2 => Native.ZkFocusInstance_getItemRange2(Handle);
		public float ItemAzi => Native.ZkFocusInstance_getItemAzi(Handle);
		public float ItemElevationDown => Native.ZkFocusInstance_getItemElevdo(Handle);
		public float ItemElevationUp => Native.ZkFocusInstance_getItemElevup(Handle);
		public int ItemPriority => Native.ZkFocusInstance_getItemPrio(Handle);
		public float MobRange1 => Native.ZkFocusInstance_getMobRange1(Handle);
		public float MobRange2 => Native.ZkFocusInstance_getMobRange2(Handle);
		public float MobAzi => Native.ZkFocusInstance_getMobAzi(Handle);
		public float MobElevationDown => Native.ZkFocusInstance_getMobElevdo(Handle);
		public float MobElevationUp => Native.ZkFocusInstance_getMobElevup(Handle);
		public int MobPriority => Native.ZkFocusInstance_getMobPrio(Handle);
	}
}