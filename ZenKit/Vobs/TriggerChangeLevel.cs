using System;

namespace ZenKit.Vobs
{
	public class TriggerChangeLevel : Trigger
	{
		public TriggerChangeLevel() : base(Native.ZkVirtualObject_new(VirtualObjectType.oCTriggerChangeLevel))
		{
		}

		public TriggerChangeLevel(Read buf, GameVersion version) : base(
			Native.ZkTriggerChangeLevel_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerChangeLevel vob");
		}

		public TriggerChangeLevel(string path, GameVersion version) : base(
			Native.ZkTriggerChangeLevel_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerChangeLevel vob");
		}

		internal TriggerChangeLevel(UIntPtr handle) : base(handle)
		{
		}

		public string LevelName
		{
			get => Native.ZkTriggerChangeLevel_getLevelName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkTriggerChangeLevel_setLevelName(Handle, value);
		}

		public string StartVob
		{
			get => Native.ZkTriggerChangeLevel_getStartVob(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkTriggerChangeLevel_setStartVob(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkTriggerChangeLevel_del(Handle);
		}
	}
}