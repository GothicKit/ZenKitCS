using System;

namespace ZenKit.Vobs
{
	public interface ILensFlare : IVirtualObject
	{
		string Effect { get; set; }
	}

	public class LensFlare : VirtualObject, ILensFlare
	{
		public LensFlare() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCVobLensFlare))
		{
		}

		public LensFlare(Read buf, GameVersion version) : base(Native.ZkLensFlare_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load lens flare vob");
		}

		public LensFlare(string path, GameVersion version) : base(Native.ZkLensFlare_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load lens flare vob");
		}

		internal LensFlare(UIntPtr handle) : base(handle)
		{
		}

		public string Effect
		{
			get => Native.ZkLensFlare_getEffect(Handle).MarshalAsString() ??
			       throw new Exception("Failed to load lens flare vob effect");
			set => Native.ZkLensFlare_setEffect(Handle, value);
		}

		protected override void Delete()
		{
			Native.ZkLensFlare_del(Handle);
		}
	}
}
