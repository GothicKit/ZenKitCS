using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ZenKit.Util;
using ZenKit.Vobs;

namespace ZenKit
{
	public enum LogLevel
	{
		Error = 0,
		Warning = 1,
		Info = 2,
		Debug = 3,
		Trace = 4
	}

	public static class Logger
	{
		public delegate void Callback(LogLevel level, string name, string message);

		private static GCHandle? _handler = null;
		private static readonly Native.Callbacks.ZkLogger NativeHandler = _nativeCallbackHandler;

		public static void Set(LogLevel lvl, Callback callback)
		{
			var handler = GCHandle.Alloc(callback);
			Native.ZkLogger_set(lvl, NativeHandler, GCHandle.ToIntPtr(handler));
			
			_handler?.Free();
			_handler = handler;
		}

		public static void SetDefault(LogLevel level)
		{
			Native.ZkLogger_setDefault(level);
		}

		public static void Log(LogLevel level, string name, string message)
		{
			Native.ZkLogger_log(level, name, message);
		}

		[MonoPInvokeCallback]
		private static void _nativeCallbackHandler(IntPtr ctx, LogLevel level, string name, string message)
		{
			var gcHandle = GCHandle.FromIntPtr(ctx);
			var cb = (Callback)gcHandle.Target;
			cb(level, name, message);
		}
	}
}