using System;

namespace ZenKit
{
	public class Model
	{
		private readonly UIntPtr _handle;


		public Model(string path)
		{
			_handle = Native.ZkModel_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model");
		}

		public Model(Read buf)
		{
			_handle = Native.ZkModel_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model");
		}

		public Model(Vfs vfs, string name)
		{
			_handle = Native.ZkModel_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model");
		}

		public ModelHierarchy Hierarchy => new ModelHierarchy(Native.ZkModel_getHierarchy(_handle));
		public ModelMesh Mesh => new ModelMesh(Native.ZkModel_getMesh(_handle));

		~Model()
		{
			Native.ZkModel_del(_handle);
		}
	}
}