using System;
using System.Collections.Generic;

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

		private static readonly List<Native.Callbacks.ZkLogger> _callbacks = new List<Native.Callbacks.ZkLogger>();

		public static void Set(LogLevel lvl, Callback callback)
		{
			Native.Callbacks.ZkLogger cb = (_, level, name, message) => callback(level, name, message);
			_callbacks.Add(cb);
			Native.ZkLogger_set(lvl, cb, UIntPtr.Zero);
		}

		public static void SetDefault(LogLevel level)
		{
			Native.ZkLogger_setDefault(level);
		}

		public static void Log(LogLevel level, string name, string message)
		{
			Native.ZkLogger_log(level, name, message);
		}
	}
}