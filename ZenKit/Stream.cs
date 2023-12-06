using System;

namespace ZenKit
{
	public enum Whence
	{
		Begin = 0,
		Current = 1,
		End = 2
	}

	public interface IRead
	{
		ulong Read(IntPtr buf, ulong length);
		ulong Seek(long off, Whence whence);
		ulong Tell();
		bool Eof();
	}

	public class Read
	{
		private readonly byte[]? _data;

		public Read(byte[] data)
		{
			Handle = Native.ZkRead_newMem(data, (ulong)data.Length);
			_data = data;
		}

		public Read(string path)
		{
			Handle = Native.ZkRead_newPath(path);
		}

		public Read(IRead impl)
		{
			var ext = new Native.Structs.ZkReadExt();
			ext.read = (_, buf, len) => impl.Read(buf, len);
			ext.seek = (_, off, whence) => impl.Seek(off, Whence.Begin);
			ext.tell = _ => impl.Tell();
			ext.eof = _ => impl.Eof();
			Handle = Native.ZkRead_newExt(ext, UIntPtr.Zero);
		}

		internal Read(UIntPtr handle)
		{
			Handle = handle;
		}

		internal UIntPtr Handle { get; }

		public byte[] Bytes
		{
			get
			{
				var buf = new byte[Native.ZkRead_getSize(Handle)];
				Native.ZkRead_getBytes(Handle, buf, (ulong)buf.Length);
				return buf;
			}
		}

		~Read()
		{
			Native.ZkRead_del(Handle);
		}
	}
}