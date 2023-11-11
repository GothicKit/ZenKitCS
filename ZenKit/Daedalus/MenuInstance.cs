using System;

namespace ZenKit.Daedalus
{
	public enum MenuItemType
	{
		Unknown = 0,
		Text = 1,
		Slider = 2,
		Input = 3,
		Cursor = 4,
		ChoiceBox = 5,
		Button = 6,
		ListBox = 7,
	}

	public enum MenuItemFlag
	{
		ChromaKeyed = 1 << 0,
		Transparent = 1 << 1,
		Selectable = 1 << 2,
		Movable = 1 << 3,
		Centered = 1 << 4,
		Disabled = 1 << 5,
		Fade = 1 << 6,
		Effects = 1 << 7,
		OnlyOutGame = 1 << 8,
		OnlyInGame = 1 << 9,
		PerfOption = 1 << 10,
		Multiline = 1 << 11,
		NeedsApply = 1 << 12,
		NeedsRestart = 1 << 13,
		ExtendedMenu = 1 << 14,
		HorSelectable = 1 << 15,
	}

	public class MenuInstance : DaedalusInstance
	{
		public MenuInstance(UIntPtr handle) : base(handle)
		{
		}

		public string BackPic => Native.ZkMenuInstance_getBackPic(Handle).MarshalAsString() ?? string.Empty;
		public string BackWorld => Native.ZkMenuInstance_getBackWorld(Handle).MarshalAsString() ?? string.Empty;
		public int PosX => Native.ZkMenuInstance_getPosX(Handle);
		public int PosY => Native.ZkMenuInstance_getPosY(Handle);
		public int DimX => Native.ZkMenuInstance_getDimX(Handle);
		public int DimY => Native.ZkMenuInstance_getDimY(Handle);
		public int Alpha => Native.ZkMenuInstance_getAlpha(Handle);
		public string MusicTheme => Native.ZkMenuInstance_getMusicTheme(Handle).MarshalAsString() ?? string.Empty;
		public int EventTimerMsec => Native.ZkMenuInstance_getEventTimerMsec(Handle);
		public int Flags => Native.ZkMenuInstance_getFlags(Handle);
		public int DefaultOutGame => Native.ZkMenuInstance_getDefaultOutgame(Handle);
		public int DefaultInGame => Native.ZkMenuInstance_getDefaultIngame(Handle);
		public string GetItem(ulong i) => Native.ZkMenuInstance_getItem(Handle, i).MarshalAsString() ?? string.Empty;
	}

	public class MenuItemInstance : DaedalusInstance
	{
		public MenuItemInstance(UIntPtr handle) : base(handle)
		{
		}

		public string FontName => Native.ZkMenuItemInstance_getFontName(Handle).MarshalAsString() ?? string.Empty;
		public string BackPic => Native.ZkMenuItemInstance_getBackpic(Handle).MarshalAsString() ?? string.Empty;
		public string AlphaMode => Native.ZkMenuItemInstance_getAlphaMode(Handle).MarshalAsString() ?? string.Empty;
		public int Alpha => Native.ZkMenuItemInstance_getAlpha(Handle);
		public MenuItemType MenuItemType => Native.ZkMenuItemInstance_getType(Handle);
		public string OnChgSetOption => Native.ZkMenuItemInstance_getOnChgSetOption(Handle).MarshalAsString() ?? string.Empty;

		public string OnChgSetOptionSection =>
			Native.ZkMenuItemInstance_getOnChgSetOptionSection(Handle).MarshalAsString() ?? string.Empty;

		public int PosX => Native.ZkMenuItemInstance_getPosX(Handle);
		public int PosY => Native.ZkMenuItemInstance_getPosY(Handle);
		public int DimX => Native.ZkMenuItemInstance_getDimX(Handle);
		public int DimY => Native.ZkMenuItemInstance_getDimY(Handle);
		public float SizeStartScale => Native.ZkMenuItemInstance_getSizeStartScale(Handle);
		public int Flags => Native.ZkMenuItemInstance_getFlags(Handle);
		public float OpenDelayTime => Native.ZkMenuItemInstance_getOpenDelayTime(Handle);
		public float OpenDuration => Native.ZkMenuItemInstance_getOpenDuration(Handle);
		public int FramePosX => Native.ZkMenuItemInstance_getFramePosX(Handle);
		public int FramePosY => Native.ZkMenuItemInstance_getFramePosY(Handle);
		public int FrameSizeX => Native.ZkMenuItemInstance_getFrameSizeX(Handle);
		public int FrameSizeY => Native.ZkMenuItemInstance_getFrameSizeY(Handle);

		public string HideIfOptionSectionSet =>
			Native.ZkMenuItemInstance_getHideIfOptionSectionSet(Handle).MarshalAsString() ?? string.Empty;

		public string HideIfOptionSet => Native.ZkMenuItemInstance_getHideIfOptionSet(Handle).MarshalAsString() ?? string.Empty;
		public int HideOnValue => Native.ZkMenuItemInstance_getHideOnValue(Handle);

		public string GetText(ulong i)
		{
			return Native.ZkMenuItemInstance_getText(Handle, i).MarshalAsString() ?? string.Empty;
		}

		public int GetOnSelAction(ulong i)
		{
			return Native.ZkMenuItemInstance_getOnSelAction(Handle, i);
		}

		public string GetOnSelActionS(ulong i)
		{
			return Native.ZkMenuItemInstance_getOnSelActionS(Handle, i).MarshalAsString() ?? string.Empty;
		}

		public int GetOnEventAction(ulong i)
		{
			return Native.ZkMenuItemInstance_getOnEventAction(Handle, i);
		}

		public float GetUserFloat(ulong i)
		{
			return Native.ZkMenuItemInstance_getUserFloat(Handle, i);
		}

		public string GetUserString(ulong i)
		{
			return Native.ZkMenuItemInstance_getUserString(Handle, i).MarshalAsString() ?? string.Empty;
		}
	}
}
