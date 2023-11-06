namespace ZenKit.Vobs;

public class VirtualObject
{
	private readonly UIntPtr _handle;

	internal VirtualObject(UIntPtr handle)
	{
		_handle = handle;
	}

	public static VirtualObject FromNative(UIntPtr ptr)
	{
		return new VirtualObject(ptr);
	}
}