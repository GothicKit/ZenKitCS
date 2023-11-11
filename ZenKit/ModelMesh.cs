using System;
using System.Collections.Generic;
using System.Linq;
using ZenKit.Util;

namespace ZenKit
{
	namespace Materialized
	{
		[Serializable]
		public struct ModelMesh
		{
			public List<SoftSkinMesh> Meshes;
			public Dictionary<string, MultiResolutionMesh> Attachments;
			public uint Checksum;
		}
	}

	public class ModelMesh : IMaterializing<Materialized.ModelMesh>
	{
		private readonly bool _delete = true;
		private readonly UIntPtr _handle;

		public ModelMesh(string path)
		{
			_handle = Native.ZkModelMesh_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model mesh");
		}

		public ModelMesh(Read buf)
		{
			_handle = Native.ZkModelMesh_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model mesh");
		}

		public ModelMesh(Vfs vfs, string name)
		{
			_handle = Native.ZkModelMesh_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model mesh");
		}

		internal ModelMesh(UIntPtr handle)
		{
			_handle = handle;
			_delete = false;
		}

		public ulong MeshCount => Native.ZkModelMesh_getMeshCount(_handle);

		public List<SoftSkinMesh> Meshes
		{
			get
			{
				var meshes = new List<SoftSkinMesh>();

				Native.ZkModelMesh_enumerateMeshes(_handle, (_, mesh) =>
				{
					meshes.Add(new SoftSkinMesh(mesh));
					return false;
				}, UIntPtr.Zero);

				return meshes;
			}
		}

		public ulong AttachmentCount => Native.ZkModelMesh_getAttachmentCount(_handle);

		public Dictionary<string, MultiResolutionMesh> Attachments
		{
			get
			{
				var attachments = new Dictionary<string, MultiResolutionMesh>();

				Native.ZkModelMesh_enumerateAttachments(_handle, (_, namePtr, mesh) =>
				{
					var name = namePtr.MarshalAsString();
					if (name == null) return false;

					attachments.Add(name, new MultiResolutionMesh(mesh));
					return false;
				}, UIntPtr.Zero);

				return attachments;
			}
		}

		public uint Checksum => Native.ZkModelMesh_getChecksum(_handle);

		public Materialized.ModelMesh Materialize()
		{
			return new Materialized.ModelMesh
			{
				Meshes = Meshes.ConvertAll(mesh => mesh.Materialize()),
				Attachments = Attachments.ToDictionary(p => p.Key, p => p.Value.Materialize())
			};
		}

		~ModelMesh()
		{
			if (_delete) Native.ZkModelMesh_del(_handle);
		}

		public SoftSkinMesh GetMesh(ulong i)
		{
			return new SoftSkinMesh(Native.ZkModelMesh_getMesh(_handle, i));
		}

		public MultiResolutionMesh? GetAttachment(string name)
		{
			var attachment = Native.ZkModelMesh_getAttachment(_handle, name);
			return attachment == UIntPtr.Zero ? null : new MultiResolutionMesh(attachment);
		}
	}
}