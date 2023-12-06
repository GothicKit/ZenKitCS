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
		ListBox = 7
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
		HorSelectable = 1 << 15
	}

	public class MenuInstance : DaedalusInstance
	{
		public MenuInstance(UIntPtr handle) : base(handle)
		{
		}

		public string BackPic
		{
			get => Native.ZkMenuInstance_getBackPic(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuInstance_setBackPic(Handle, value);
		}

		public string BackWorld
		{
			get => Native.ZkMenuInstance_getBackWorld(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuInstance_setBackWorld(Handle, value);
		}

		public int PosX
		{
			get => Native.ZkMenuInstance_getPosX(Handle);
			set => Native.ZkMenuInstance_setPosX(Handle, value);
		}

		public int PosY
		{
			get => Native.ZkMenuInstance_getPosY(Handle);
			set => Native.ZkMenuInstance_setPosY(Handle, value);
		}

		public int DimX
		{
			get => Native.ZkMenuInstance_getDimX(Handle);
			set => Native.ZkMenuInstance_setDimX(Handle, value);
		}

		public int DimY
		{
			get => Native.ZkMenuInstance_getDimY(Handle);
			set => Native.ZkMenuInstance_setDimY(Handle, value);
		}

		public int Alpha
		{
			get => Native.ZkMenuInstance_getAlpha(Handle);
			set => Native.ZkMenuInstance_setAlpha(Handle, value);
		}

		public string MusicTheme
		{
			get => Native.ZkMenuInstance_getMusicTheme(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuInstance_setMusicTheme(Handle, value);
		}

		public int EventTimerMsec
		{
			get => Native.ZkMenuInstance_getEventTimerMsec(Handle);
			set => Native.ZkMenuInstance_setEventTimerMsec(Handle, value);
		}

		public int Flags
		{
			get => Native.ZkMenuInstance_getFlags(Handle);
			set => Native.ZkMenuInstance_setFlags(Handle, value);
		}

		public int DefaultOutGame
		{
			get => Native.ZkMenuInstance_getDefaultOutgame(Handle);
			set => Native.ZkMenuInstance_setDefaultOutgame(Handle, value);
		}

		public int DefaultInGame
		{
			get => Native.ZkMenuInstance_getDefaultIngame(Handle);
			set => Native.ZkMenuInstance_setDefaultIngame(Handle, value);
		}


		public string GetItem(ulong i)
		{
			return Native.ZkMenuInstance_getItem(Handle, i).MarshalAsString() ?? string.Empty;
		}
	}

	public class MenuItemInstance : DaedalusInstance
	{
		public MenuItemInstance(UIntPtr handle) : base(handle)
		{
		}

		public string FontName
		{
			get => Native.ZkMenuItemInstance_getFontName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuItemInstance_setFontName(Handle, value);
		}

		public string BackPic
		{
			get => Native.ZkMenuItemInstance_getBackpic(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuItemInstance_setBackpic(Handle, value);
		}

		public string AlphaMode
		{
			get => Native.ZkMenuItemInstance_getAlphaMode(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuItemInstance_setAlphaMode(Handle, value);
		}

		public int Alpha
		{
			get => Native.ZkMenuItemInstance_getAlpha(Handle);
			set => Native.ZkMenuItemInstance_setAlpha(Handle, value);
		}

		public MenuItemType MenuItemType
		{
			get => Native.ZkMenuItemInstance_getType(Handle);
			set => Native.ZkMenuItemInstance_setType(Handle, value);
		}


		public string OnChgSetOption
		{
			get => Native.ZkMenuItemInstance_getOnChgSetOption(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuItemInstance_setOnChgSetOption(Handle, value);
		}


		public string OnChgSetOptionSection
		{
			get => Native.ZkMenuItemInstance_getOnChgSetOptionSection(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuItemInstance_setOnChgSetOptionSection(Handle, value);
		}


		public int PosX
		{
			get => Native.ZkMenuItemInstance_getPosX(Handle);
			set => Native.ZkMenuItemInstance_setPosX(Handle, value);
		}

		public int PosY
		{
			get => Native.ZkMenuItemInstance_getPosY(Handle);
			set => Native.ZkMenuItemInstance_setPosY(Handle, value);
		}

		public int DimX
		{
			get => Native.ZkMenuItemInstance_getDimX(Handle);
			set => Native.ZkMenuItemInstance_setDimX(Handle, value);
		}

		public int DimY
		{
			get => Native.ZkMenuItemInstance_getDimY(Handle);
			set => Native.ZkMenuItemInstance_setDimY(Handle, value);
		}

		public float SizeStartScale
		{
			get => Native.ZkMenuItemInstance_getSizeStartScale(Handle);
			set => Native.ZkMenuItemInstance_setSizeStartScale(Handle, value);
		}

		public int Flags
		{
			get => Native.ZkMenuItemInstance_getFlags(Handle);
			set => Native.ZkMenuItemInstance_setFlags(Handle, value);
		}

		public float OpenDelayTime
		{
			get => Native.ZkMenuItemInstance_getOpenDelayTime(Handle);
			set => Native.ZkMenuItemInstance_setOpenDelayTime(Handle, value);
		}

		public float OpenDuration
		{
			get => Native.ZkMenuItemInstance_getOpenDuration(Handle);
			set => Native.ZkMenuItemInstance_setOpenDuration(Handle, value);
		}

		public int FramePosX
		{
			get => Native.ZkMenuItemInstance_getFramePosX(Handle);
			set => Native.ZkMenuItemInstance_setFramePosX(Handle, value);
		}

		public int FramePosY
		{
			get => Native.ZkMenuItemInstance_getFramePosY(Handle);
			set => Native.ZkMenuItemInstance_setFramePosY(Handle, value);
		}

		public int FrameSizeX
		{
			get => Native.ZkMenuItemInstance_getFrameSizeX(Handle);
			set => Native.ZkMenuItemInstance_setFrameSizeX(Handle, value);
		}

		public int FrameSizeY
		{
			get => Native.ZkMenuItemInstance_getFrameSizeY(Handle);
			set => Native.ZkMenuItemInstance_setFrameSizeY(Handle, value);
		}


		public string HideIfOptionSectionSet
		{
			get => Native.ZkMenuItemInstance_getHideIfOptionSectionSet(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuItemInstance_setHideIfOptionSectionSet(Handle, value);
		}


		public string HideIfOptionSet
		{
			get => Native.ZkMenuItemInstance_getHideIfOptionSet(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMenuItemInstance_setHideIfOptionSet(Handle, value);
		}


		public int HideOnValue
		{
			get => Native.ZkMenuItemInstance_getHideOnValue(Handle);
			set => Native.ZkMenuItemInstance_setHideOnValue(Handle, value);
		}

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

		public void SetText(ulong i, string v)
		{
			Native.ZkMenuItemInstance_setText(Handle, i, v);
		}

		public void SetOnSelAction(ulong i, int v)
		{
			Native.ZkMenuItemInstance_setOnSelAction(Handle, i, v);
		}

		public void SetOnSelActionS(ulong i, string v)
		{
			Native.ZkMenuItemInstance_setOnSelActionS(Handle, i, v);
		}

		public void SetOnEventAction(ulong i, int v)
		{
			Native.ZkMenuItemInstance_setOnEventAction(Handle, i, v);
		}

		public void SetUserFloat(ulong i, float v)
		{
			Native.ZkMenuItemInstance_setUserFloat(Handle, i, v);
		}

		public void SetUserString(ulong i, string v)
		{
			Native.ZkMenuItemInstance_setUserString(Handle, i, v);
		}
	}
}