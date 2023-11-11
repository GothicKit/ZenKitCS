using System;
using ZenKit.Util;

namespace ZenKit
{
	namespace Materialized
	{
		[Serializable]
		public struct Model
		{
			public ModelHierarchy Hierarchy;
			public ModelMesh Mesh;
		}
	}

	public class Model : IMaterializing<Materialized.Model>
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

		public Materialized.Model Materialize()
		{
			return new Materialized.Model
			{
				Hierarchy = Hierarchy.Materialize(),
				Mesh = Mesh.Materialize()
			};
		}

		~Model()
		{
			Native.ZkModel_del(_handle);
		}
	}
}