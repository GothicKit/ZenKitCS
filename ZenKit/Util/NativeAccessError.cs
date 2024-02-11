using System;

namespace ZenKit.Util
{
	public class NativeAccessError : Exception
	{
		public NativeAccessError() : base("Access to a native property failed")
		{
		}

		public NativeAccessError(string message) : base("Access to a native property failed: " + message)
		{
		}

		public NativeAccessError(string message, Exception innerException) : base(
			"Access to a native property failed: " + message, innerException)
		{
		}
	}
}