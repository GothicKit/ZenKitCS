using System;
using System.Collections.Generic;
using System.Numerics;
using ZenKit.Util;

namespace ZenKit
{
	namespace Materialized
	{
		[Serializable]
		public struct MorphAnimation
		{
			public int Layer;
			public float BlendIn;
			public float BlendOut;
			public TimeSpan Duration;
			public float Speed;
			public byte Flags;
			public uint FrameCount;
			public uint[] Vertices;
			public Vector3[] Samples;
		}

		[Serializable]
		public struct MorphSource
		{
			public string FileName;
			public DateTime? FileDate;
		}

		[Serializable]
		public struct MorphMesh
		{
			public string Name;
			public MultiResolutionMesh Mesh;
			public Vector3[] MorphPositions;
			public List<MorphAnimation> Animations;
			public List<MorphSource> Sources;
		}
	}

	public class MorphAnimation : IMaterializing<Materialized.MorphAnimation>
	{
		private readonly UIntPtr _handle;

		internal MorphAnimation(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkMorphAnimation_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load morph animation name");

		public int Layer => Native.ZkMorphAnimation_getLayer(_handle);
		public float BlendIn => Native.ZkMorphAnimation_getBlendIn(_handle);
		public float BlendOut => Native.ZkMorphAnimation_getBlendOut(_handle);
		public TimeSpan Duration => TimeSpan.FromMilliseconds(Native.ZkMorphAnimation_getDuration(_handle));
		public float Speed => Native.ZkMorphAnimation_getSpeed(_handle);
		public byte Flags => Native.ZkMorphAnimation_getFlags(_handle);
		public uint FrameCount => Native.ZkMorphAnimation_getFrameCount(_handle);

		public uint[] Vertices =>
			Native.ZkMorphAnimation_getVertices(_handle, out var count).MarshalAsArray<uint>(count);

		public Vector3[] Samples =>
			Native.ZkMorphAnimation_getSamples(_handle, out var count).MarshalAsArray<Vector3>(count);

		public Materialized.MorphAnimation Materialize()
		{
			return new Materialized.MorphAnimation
			{
				Layer = Layer,
				BlendIn = BlendIn,
				BlendOut = BlendOut,
				Duration = Duration,
				Speed = Speed,
				Flags = Flags,
				FrameCount = FrameCount,
				Vertices = Vertices,
				Samples = Samples
			};
		}
	}

	public class MorphSource : IMaterializing<Materialized.MorphSource>
	{
		private readonly UIntPtr _handle;

		internal MorphSource(UIntPtr handle)
		{
			_handle = handle;
		}

		public string FileName => Native.ZkMorphSource_getFileName(_handle).MarshalAsString() ??
		                          throw new Exception("Failed to load morph source file name");

		public DateTime? FileDate => Native.ZkMorphSource_getFileDate(_handle).AsDateTime();

		public Materialized.MorphSource Materialize()
		{
			return new Materialized.MorphSource
			{
				FileDate = FileDate,
				FileName = FileName
			};
		}
	}

	public class MorphMesh : IMaterializing<Materialized.MorphMesh>
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

		public Materialized.MorphMesh Materialize()
		{
			return new Materialized.MorphMesh
			{
				Name = Name,
				Mesh = Mesh.Materialize(),
				MorphPositions = MorphPositions,
				Animations = Animations.ConvertAll(ani => ani.Materialize()),
				Sources = Sources.ConvertAll(src => src.Materialize())
			};
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