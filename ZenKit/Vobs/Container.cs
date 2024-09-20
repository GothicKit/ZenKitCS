using System;
using System.Collections.Generic;

namespace ZenKit.Vobs
{
	public interface IContainer : IInteractiveObject
	{
		bool IsLocked { get; set; }
		string Key { get; set; }
		string PickString { get; set; }
		string Contents { get; set; }
		int ItemCount { get; }
		List<IItem> Items { get; }
		void AddItem(Item item);
		void RemoveItem(int i);
	}

	public class Container : InteractiveObject, IContainer
	{
		public Container() : base(Native.ZkVirtualObject_new(VirtualObjectType.oCMobContainer))
		{
		}

		public Container(Read buf, GameVersion version) : base(Native.ZkContainer_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Container vob");
		}

		public Container(string path, GameVersion version) : base(Native.ZkContainer_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load Container vob");
		}

		internal Container(UIntPtr handle) : base(handle)
		{
		}

		public bool IsLocked
		{
			get => Native.ZkContainer_getIsLocked(Handle);
			set => Native.ZkContainer_setIsLocked(Handle, value);
		}

		public string Key
		{
			get => Native.ZkContainer_getKey(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkContainer_setKey(Handle, value);
		}

		public string PickString
		{
			get => Native.ZkContainer_getPickString(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkContainer_setPickString(Handle, value);
		}

		public string Contents
		{
			get => Native.ZkContainer_getContents(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkContainer_setContents(Handle, value);
		}

		public int ItemCount => (int)Native.ZkContainer_getItemCount(Handle);

		public List<IItem> Items
		{
			get
			{
				var items = new List<IItem>();

				for (var i = 0; i < ItemCount; ++i)
				{
					var val = Native.ZkContainer_getItem(Handle, (ulong)i);
					items.Add(new Item(Native.ZkObject_takeRef(val)));
				}

				return items;
			}
		}

		public void AddItem(Item item)
		{
			Native.ZkContainer_addItem(Handle, item.Handle);
		}

		public void RemoveItem(int i)
		{
			Native.ZkContainer_removeItem(Handle, (ulong)i);
		}

		protected override void Delete()
		{
			Native.ZkContainer_del(Handle);
		}
	}
}
