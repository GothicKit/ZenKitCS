using System;

namespace ZenKit.Vobs
{
	public class TriggerChangeLevel : Trigger
	{
		public TriggerChangeLevel(Read buf, GameVersion version) : base(
			Native.ZkTriggerChangeLevel_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerChangeLevel vob");
		}

		public TriggerChangeLevel(string path, GameVersion version) : base(
			Native.ZkTriggerChangeLevel_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerChangeLevel vob");
		}

		internal TriggerChangeLevel(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public string LevelName => Native.ZkTriggerChangeLevel_getLevelName(Handle).MarshalAsString() ?? string.Empty;
		public string StartVob => Native.ZkTriggerChangeLevel_getStartVob(Handle).MarshalAsString() ?? string.Empty;


		protected override void Delete()
		{
			Native.ZkTriggerChangeLevel_del(Handle);
		}
	}
}