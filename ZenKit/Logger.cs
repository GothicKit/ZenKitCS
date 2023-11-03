namespace ZenKit;

public enum LogLevel : uint
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

	public static void Set(LogLevel lvl, Callback callback)
	{
		Native.ZkLogger_set(lvl, (_, level, name, message) => callback(level, name, message), UIntPtr.Zero);
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