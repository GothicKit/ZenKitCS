using System;
using System.Collections.Generic;
using System.Linq;
using ZenKit.Util;

namespace ZenKit
{
	public interface IModelMesh : ICacheable<IModelMesh>
	{
		int MeshCount { get; }
		List<ISoftSkinMesh> Meshes { get; }
		int AttachmentCount { get; }
		Dictionary<string, IMultiResolutionMesh> Attachments { get; }
		int Checksum { get; }
		ISoftSkinMesh GetMesh(int i);
		IMultiResolutionMesh? GetAttachment(string name);
	}

	[Serializable]
	public class CachedModelMesh : IModelMesh
	{
		public int MeshCount => Meshes.Count;
		public List<ISoftSkinMesh> Meshes { get; set; }
		public int AttachmentCount => Attachments.Count;
		public Dictionary<string, IMultiResolutionMesh> Attachments { get; set; }
		public int Checksum { get; set; }

		public ISoftSkinMesh GetMesh(int i)
		{
			return Meshes[i];
		}

		public IMultiResolutionMesh? GetAttachment(string name)
		{
			return Attachments[name];
		}

		public IModelMesh Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class ModelMesh : IModelMesh
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

		public int MeshCount => (int)Native.ZkModelMesh_getMeshCount(_handle);

		public List<ISoftSkinMesh> Meshes
		{
			get
			{
				var meshes = new List<ISoftSkinMesh>();

				Native.ZkModelMesh_enumerateMeshes(_handle, (_, mesh) =>
				{
					meshes.Add(new SoftSkinMesh(mesh));
					return false;
				}, UIntPtr.Zero);

				return meshes;
			}
		}

		public int AttachmentCount => (int)Native.ZkModelMesh_getAttachmentCount(_handle);

		public Dictionary<string, IMultiResolutionMesh> Attachments
		{
			get
			{
				var attachments = new Dictionary<string, IMultiResolutionMesh>();

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

		public int Checksum => (int)Native.ZkModelMesh_getChecksum(_handle);

		public IModelMesh Cache()
		{
			return new CachedModelMesh
			{
				Meshes = Meshes.ConvertAll(mesh => mesh.Cache()),
				Attachments = Attachments.ToDictionary(p => p.Key, p => p.Value.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public ISoftSkinMesh GetMesh(int i)
		{
			return new SoftSkinMesh(Native.ZkModelMesh_getMesh(_handle, (ulong)i));
		}

		public IMultiResolutionMesh? GetAttachment(string name)
		{
			var attachment = Native.ZkModelMesh_getAttachment(_handle, name);
			return attachment == UIntPtr.Zero ? null : new MultiResolutionMesh(attachment);
		}

		~ModelMesh()
		{
			if (_delete) Native.ZkModelMesh_del(_handle);
		}
	}
}