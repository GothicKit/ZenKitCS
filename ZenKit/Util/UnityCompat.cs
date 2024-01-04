using System;

namespace ZenKit.Util
{
	/// <summary>
	/// Attribute for Unity compatibility. When calling native functions using delegates, Unity's AOT compiler expects every unmanaged callback function to be static and have this attribute, otherwise compilation will fail.
	/// </summary>
	public class MonoPInvokeCallbackAttribute : Attribute
	{
	}
}