using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using NativeLibraryLoader;

namespace ZenKit.NativeLoader
{
	public class NativePathResolver : PathResolver
	{
		public override IEnumerable<string> EnumeratePossibleLibraryLoadTargets(string name)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				yield return Path.Combine(AppContext.BaseDirectory, $"{name}.dll");
				yield return Path.Combine(AppContext.BaseDirectory, $"runtimes\\win-x64\\native\\{name}.dll");
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
				{
					yield return Path.Combine(AppContext.BaseDirectory, $"lib{name}.so");
					yield return Path.Combine(AppContext.BaseDirectory, $"runtimes/linux-x64/native/lib{name}.so");
				}
				else if (RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
				{
					yield return Path.Combine(AppContext.BaseDirectory, $"lib{name}.so");
					yield return Path.Combine(AppContext.BaseDirectory, $"runtimes/android-arm64/native/lib{name}.so");
				}
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				yield return Path.Combine(AppContext.BaseDirectory, $"lib{name}.dylib");
				yield return Path.Combine(AppContext.BaseDirectory, $"runtimes/osx-x64/native/lib{name}.dylib");
			}
		}
	}
}