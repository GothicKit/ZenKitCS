using System;

namespace ZenKit.Vobs
{
	public interface ITriggerScript : IVirtualObject
	{
		string Function { get; set; }
	}

	public class TriggerScript : Trigger, ITriggerScript
	{
		public TriggerScript() : base(Native.ZkVirtualObject_new(VirtualObjectType.oCTriggerScript))
		{
		}

		public TriggerScript(Read buf, GameVersion version) : base(Native.ZkTriggerScript_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerScript vob");
		}

		public TriggerScript(string path, GameVersion version) : base(Native.ZkTriggerScript_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load TriggerScript vob");
		}

		internal TriggerScript(UIntPtr handle) : base(handle)
		{
		}

		public string Function
		{
			get => Native.ZkTriggerScript_getFunction(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkTriggerScript_setFunction(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkTriggerScript_del(Handle);
		}
	}
}
