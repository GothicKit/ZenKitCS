using System;
using System.Collections.Generic;
using System.Numerics;

namespace ZenKit
{
	public class MorphAnimation
	{
		private readonly UIntPtr _handle;

		public MorphAnimation(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkMorphAnimation_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load morph animation name");

		public int Layer => Native.ZkMorphAnimation_getLayer(_handle);
		public float BlendIn => Native.ZkMorphAnimation_getBlendIn(_handle);
		public float BlendOut => Native.ZkMorphAnimation_getBlendOut(_handle);
		public float Duration => Native.ZkMorphAnimation_getDuration(_handle);
		public float Speed => Native.ZkMorphAnimation_getSpeed(_handle);
		public byte Flags => Native.ZkMorphAnimation_getFlags(_handle);
		public uint FrameCount => Native.ZkMorphAnimation_getFrameCount(_handle);
		public uint[] Vertices => Native.ZkMorphAnimation_getVertices(_handle, out var count).MarshalAsArray<uint>(count);

		public Vector3[] Samples =>
			Native.ZkMorphAnimation_getSamples(_handle, out var count).MarshalAsArray<Vector3>(count);
	}

	public class MorphSource
	{
		private readonly UIntPtr _handle;

		public MorphSource(UIntPtr handle)
		{
			_handle = handle;
		}

		public string FileName => Native.ZkMorphSource_getFileName(_handle).MarshalAsString() ??
		                          throw new Exception("Failed to load morph source file name");

		public DateTime FileDate => Native.ZkMorphSource_getFileDate(_handle).AsDateTime();
	}

	public class MorphMesh
	{
		private readonly UIntPtr _handle;

		public MorphMesh(string path)
		{
			_handle = Native.ZkMorphMesh_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load morph mesh");
		}

		public MorphMesh(Read buf)
		{
			_handle = Native.ZkMorphMesh_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load morph mesh");
		}

		public MorphMesh(Vfs vfs, string name)
		{
			_handle = Native.ZkMorphMesh_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load morph mesh");
		}

		public string Name => Native.ZkMorphMesh_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load morph mesh name");

		public MultiResolutionMesh Mesh => new MultiResolutionMesh(Native.ZkMorphMesh_getMesh(_handle));

		public Vector3[] MorphPositions =>
			Native.ZkMorphMesh_getMorphPositions(_handle, out var count).MarshalAsArray<Vector3>(count);

		public ulong AnimationCount => Native.ZkMorphMesh_getAnimationCount(_handle);

		public List<MorphAnimation> Animations
		{
			get
			{
				var animation = new List<MorphAnimation>();

				Native.ZkMorphMesh_enumerateAnimations(_handle, (_, anim) =>
				{
					animation.Add(new MorphAnimation(anim));
					return false;
				}, UIntPtr.Zero);

				return animation;
			}
		}

		public ulong SourceCount => Native.ZkMorphMesh_getSourceCount(_handle);

		public List<MorphSource> Sources
		{
			get
			{
				var sources = new List<MorphSource>();

				Native.ZkMorphMesh_enumerateSources(_handle, (_, src) =>
				{
					sources.Add(new MorphSource(src));
					return false;
				}, UIntPtr.Zero);

				return sources;
			}
		}

		~MorphMesh()
		{
			Native.ZkMorphMesh_del(_handle);
		}

		public MorphAnimation GetAnimation(ulong i)
		{
			return new MorphAnimation(Native.ZkMorphMesh_getAnimation(_handle, i));
		}

		public MorphSource GetSource(ulong i)
		{
			return new MorphSource(Native.ZkMorphMesh_getSource(_handle, i));
		}
	}
}