namespace ZenKit.Vobs;

public enum SoundMaterialType
{
	Wood = 0,
	Stone = 1,
	Metal = 2,
	Leather = 3,
	Clay = 4,
	Glass = 5,
}

public class MovableObject : VirtualObject
{
	public MovableObject(Read buf, GameVersion version) : base(Native.ZkMovableObject_load(buf.Handle, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load MovableObject vob");
	}

	public MovableObject(string path, GameVersion version) : base(Native.ZkMovableObject_loadPath(path, version), true)
	{
		if (Handle == UIntPtr.Zero) throw new Exception("Failed to load MovableObject vob");
	}

	internal MovableObject(UIntPtr handle, bool delete) : base(handle, delete)
	{
	}
	
	public string FocusName => Native.ZkMovableObject_getName(Handle).MarshalAsString() ?? string.Empty;
	public int Hp => Native.ZkMovableObject_getHp(Handle);
	public int Damage => Native.ZkMovableObject_getDamage(Handle);
	public bool Movable => Native.ZkMovableObject_getMovable(Handle);
	public bool Takable => Native.ZkMovableObject_getTakable(Handle);
	public bool FocusOverride => Native.ZkMovableObject_getFocusOverride(Handle);
	public SoundMaterialType Material => Native.ZkMovableObject_getMaterial(Handle);
	public string VisualDestroyed => Native.ZkMovableObject_getVisualDestroyed(Handle).MarshalAsString() ?? string.Empty;
	public string Owner => Native.ZkMovableObject_getOwner(Handle).MarshalAsString() ?? string.Empty;
	public string OwnerGuild => Native.ZkMovableObject_getOwnerGuild(Handle).MarshalAsString() ?? string.Empty;
	public bool Destroyed => Native.ZkMovableObject_getDestroyed(Handle);


	protected override void Delete()
	{
		Native.ZkMovableObject_del(Handle);
	}
}