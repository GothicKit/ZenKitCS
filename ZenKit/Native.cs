using System.Reflection;
using System.Runtime.InteropServices;

namespace ZenKit;

internal static class Marshalling
{
	public static string? MarshalAsString(this IntPtr ptr)
	{
		return Marshal.PtrToStringUTF8(ptr);
	}
}

internal static class Native
{
	public delegate bool ZkCutsceneBlockEnumerator(UIntPtr ctx, UIntPtr block);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate bool ZkFontGlyphEnumerator(UIntPtr ctx, IntPtr glyph);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ZkLogger(UIntPtr ctx, LogLevel lvl, string name, string message);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate bool ZkVfsNodeEnumerator(UIntPtr ctx, UIntPtr node);

	private const string DLLNAME = "zenkit";

	static Native()
	{
		NativeLibrary.SetDllImportResolver(typeof(Native).Assembly, ImportResolver);
	}

	private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
	{
		if (!Environment.Is64BitProcess) return IntPtr.Zero;
		if (libraryName != DLLNAME) return IntPtr.Zero;

		string libraryPath;

		if (OperatingSystem.IsWindows()) libraryPath = "./Runtimes/win-x64/libczenkit.dll";
		else if (OperatingSystem.IsAndroid()) libraryPath = "./Runtimes/android-x64/libczenkit.so";
		else if (OperatingSystem.IsLinux()) libraryPath = "./Runtimes/linux-x64/libczenkit.so";
		else if (OperatingSystem.IsMacOS()) libraryPath = "./Runtimes/osx-x64/libczenkit.so";
		else return IntPtr.Zero;

		if (!NativeLibrary.TryLoad(libraryPath, out var libraryHandle)) return IntPtr.Zero;
		return libraryHandle;
	}

	[DllImport(DLLNAME)]
	public static extern void ZkLogger_set(LogLevel lvl, ZkLogger logger, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkLogger_setDefault(LogLevel lvl);

	[DllImport(DLLNAME)]
	public static extern void ZkLogger_log(LogLevel lvl, string name, string message);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkRead_newFile(UIntPtr stream);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkRead_newMem([MarshalAs(UnmanagedType.LPArray)] byte[] bytes, ulong length);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkRead_newPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkRead_newExt(ZkReadExt ext, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern void ZkRead_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_new();

	[DllImport(DLLNAME)]
	public static extern void ZkVfs_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_getRoot(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_mkdir(UIntPtr slf, string path);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_remove(UIntPtr slf, string path);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_mount(UIntPtr slf, UIntPtr node, string parent, VfsOverwriteBehavior overwrite);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_mountHost(UIntPtr slf, string path, string parent, VfsOverwriteBehavior overwrite);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_mountDisk(UIntPtr slf, UIntPtr buf, VfsOverwriteBehavior overwrite);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfs_mountDiskHost(UIntPtr slf, string path, VfsOverwriteBehavior overwrite);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_resolvePath(UIntPtr slf, string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfs_findNode(UIntPtr slf, string name);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_newFile(string name, byte[] data, ulong size, ulong ts);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_newDir(string name, ulong ts);

	[DllImport(DLLNAME)]
	public static extern void ZkVfsNode_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfsNode_isFile(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfsNode_isDir(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern ulong ZkVfsNode_getTime(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkVfsNode_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_getChild(UIntPtr slf, string name);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_create(UIntPtr slf, UIntPtr node);

	[DllImport(DLLNAME)]
	public static extern bool ZkVfsNode_remove(UIntPtr slf, string name);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkVfsNode_open(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern void ZkVfsNode_enumerateChildren(UIntPtr slf, ZkVfsNodeEnumerator callback, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneLibrary_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneLibrary_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneLibrary_loadVfs(UIntPtr vfs, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkCutsceneLibrary_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneLibrary_getBlock(UIntPtr slf, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkCutsceneLibrary_enumerateBlocks(UIntPtr slf, ZkCutsceneBlockEnumerator cb, UIntPtr ctx);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkCutsceneBlock_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkCutsceneBlock_getMessage(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint ZkCutsceneMessage_getType(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkCutsceneMessage_getText(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkCutsceneMessage_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkFont_load(UIntPtr buf);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkFont_loadPath(string path);

	[DllImport(DLLNAME)]
	public static extern UIntPtr ZkFont_loadVfs(UIntPtr buf, string name);

	[DllImport(DLLNAME)]
	public static extern void ZkFont_del(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern IntPtr ZkFont_getName(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint ZkFont_getHeight(UIntPtr slf);

	[DllImport(DLLNAME)]
	[return: MarshalAs(UnmanagedType.U8)]
	public static extern ulong ZkFont_getGlyphCount(UIntPtr slf);

	[DllImport(DLLNAME)]
	public static extern FontGlyph ZkFont_getGlyph(UIntPtr slf, [MarshalAs(UnmanagedType.U8)] ulong i);

	[DllImport(DLLNAME)]
	public static extern void ZkFont_enumerateGlyphs(UIntPtr slf, ZkFontGlyphEnumerator cb, UIntPtr ctx);

	[StructLayout(LayoutKind.Sequential)]
	public struct ZkReadExt
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong ReadFn(UIntPtr ctx, IntPtr buf, ulong len);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong SeekFn(UIntPtr ctx, long off, Whence whence);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong TellFn(UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool EofFn(UIntPtr ctx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool DelFn(UIntPtr ctx);

		public ReadFn read;
		public SeekFn seek;
		public TellFn tell;
		public EofFn eof;
		public DelFn del;
	}
}