using System;
using ZenKit.Util;

namespace ZenKit
{
	public interface IModel : ICacheable<IModel>
	{
		IModelHierarchy Hierarchy { get; }
		IModelMesh Mesh { get; }
	}

	[Serializable]
	public class CachedModel : IModel
	{
		public IModelHierarchy Hierarchy { get; set; }
		public IModelMesh Mesh { get; set; }

		public IModel Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class Model : IModel
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

		public IModelHierarchy Hierarchy => new ModelHierarchy(Native.ZkModel_getHierarchy(_handle));
		public IModelMesh Mesh => new ModelMesh(Native.ZkModel_getMesh(_handle));

		public IModel Cache()
		{
			return new CachedModel
			{
				Hierarchy = Hierarchy.Cache(),
				Mesh = Mesh.Cache()
			};
		}

		public bool IsCached()
		{
			return false;
		}

		~Model()
		{
			Native.ZkModel_del(_handle);
		}
	}
}