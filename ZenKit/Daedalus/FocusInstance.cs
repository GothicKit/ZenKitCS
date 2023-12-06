using System;

namespace ZenKit.Daedalus
{
	public class FocusInstance : DaedalusInstance
	{
		public FocusInstance(UIntPtr handle) : base(handle)
		{
		}

		public float NpcLongRange
		{
			get => Native.ZkFocusInstance_getNpcLongrange(Handle);
			set => Native.ZkFocusInstance_setNpcLongrange(Handle, value);
		}

		public float NpcRange1
		{
			get => Native.ZkFocusInstance_getNpcRange1(Handle);
			set => Native.ZkFocusInstance_setNpcRange1(Handle, value);
		}

		public float NpcRange2
		{
			get => Native.ZkFocusInstance_getNpcRange2(Handle);
			set => Native.ZkFocusInstance_setNpcRange2(Handle, value);
		}

		public float NpcAzi
		{
			get => Native.ZkFocusInstance_getNpcAzi(Handle);
			set => Native.ZkFocusInstance_setNpcAzi(Handle, value);
		}

		public float NpcElevationDown
		{
			get => Native.ZkFocusInstance_getNpcElevdo(Handle);
			set => Native.ZkFocusInstance_setNpcElevdo(Handle, value);
		}

		public float NpcElevationUp
		{
			get => Native.ZkFocusInstance_getNpcElevup(Handle);
			set => Native.ZkFocusInstance_setNpcElevup(Handle, value);
		}

		public int NpcPriority
		{
			get => Native.ZkFocusInstance_getNpcPrio(Handle);
			set => Native.ZkFocusInstance_setNpcPrio(Handle, value);
		}

		public float ItemRange1
		{
			get => Native.ZkFocusInstance_getItemRange1(Handle);
			set => Native.ZkFocusInstance_setItemRange1(Handle, value);
		}

		public float ItemRange2
		{
			get => Native.ZkFocusInstance_getItemRange2(Handle);
			set => Native.ZkFocusInstance_setItemRange2(Handle, value);
		}

		public float ItemAzi
		{
			get => Native.ZkFocusInstance_getItemAzi(Handle);
			set => Native.ZkFocusInstance_setItemAzi(Handle, value);
		}

		public float ItemElevationDown
		{
			get => Native.ZkFocusInstance_getItemElevdo(Handle);
			set => Native.ZkFocusInstance_setItemElevdo(Handle, value);
		}

		public float ItemElevationUp
		{
			get => Native.ZkFocusInstance_getItemElevup(Handle);
			set => Native.ZkFocusInstance_setItemElevup(Handle, value);
		}

		public int ItemPriority
		{
			get => Native.ZkFocusInstance_getItemPrio(Handle);
			set => Native.ZkFocusInstance_setItemPrio(Handle, value);
		}

		public float MobRange1
		{
			get => Native.ZkFocusInstance_getMobRange1(Handle);
			set => Native.ZkFocusInstance_setMobRange1(Handle, value);
		}

		public float MobRange2
		{
			get => Native.ZkFocusInstance_getMobRange2(Handle);
			set => Native.ZkFocusInstance_setMobRange2(Handle, value);
		}

		public float MobAzi
		{
			get => Native.ZkFocusInstance_getMobAzi(Handle);
			set => Native.ZkFocusInstance_setMobAzi(Handle, value);
		}

		public float MobElevationDown
		{
			get => Native.ZkFocusInstance_getMobElevdo(Handle);
			set => Native.ZkFocusInstance_setMobElevdo(Handle, value);
		}

		public float MobElevationUp
		{
			get => Native.ZkFocusInstance_getMobElevup(Handle);
			set => Native.ZkFocusInstance_setMobElevup(Handle, value);
		}

		public int MobPriority
		{
			get => Native.ZkFocusInstance_getMobPrio(Handle);
			set => Native.ZkFocusInstance_setMobPrio(Handle, value);
		}
	}
}