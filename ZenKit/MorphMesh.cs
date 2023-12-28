using System;
using System.Collections.Generic;
using System.Numerics;
using ZenKit.Util;

namespace ZenKit
{
	public interface IMorphAnimation : ICacheable<IMorphAnimation>
	{
		string Name { get; }
		int Layer { get; }
		float BlendIn { get; }
		float BlendOut { get; }
		TimeSpan Duration { get; }
		float Speed { get; }
		byte Flags { get; }
		int FrameCount { get; }
		List<int> Vertices { get; }
		public int SampleCount { get; }
		List<Vector3> Samples { get; }

		Vector3 GetSample(int i);
	}

	[Serializable]
	public class CachedMorphAnimation : IMorphAnimation
	{
		public string Name { get; set; }
		public int Layer { get; set; }
		public float BlendIn { get; set; }
		public float BlendOut { get; set; }
		public TimeSpan Duration { get; set; }
		public float Speed { get; set; }
		public byte Flags { get; set; }
		public int FrameCount { get; set; }
		public List<int> Vertices { get; set; }

		public int SampleCount => Samples.Count;

		public List<Vector3> Samples { get; set; }

		public Vector3 GetSample(int i)
		{
			return Samples[i];
		}

		public IMorphAnimation Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class MorphAnimation : IMorphAnimation
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
		public int FrameCount => (int)Native.ZkMorphAnimation_getFrameCount(_handle);

		public List<int> Vertices =>
			Native.ZkMorphAnimation_getVertices(_handle, out var count).MarshalAsList<int>(count);

		public int SampleCount => (int)Native.ZkMorphAnimation_getSampleCount(_handle);

		public List<Vector3> Samples
		{
			get
			{
				var samples = new List<Vector3>();

				Native.ZkMorphAnimation_enumerateSamples(_handle, (_, v) =>
				{
					samples.Add(v);
					return false;
				}, UIntPtr.Zero);

				return samples;
			}
		}

		public IMorphAnimation Cache()
		{
			return new CachedMorphAnimation
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

		public bool IsCached()
		{
			return false;
		}

		public Vector3 GetSample(int i)
		{
			return Native.ZkMorphAnimation_getSample(_handle, (ulong)i);
		}
	}

	public interface IMorphSource : ICacheable<IMorphSource>
	{
		string FileName { get; }
		DateTime? FileDate { get; }
	}

	[Serializable]
	public class CachedMorphSource : IMorphSource
	{
		public string FileName { get; set; }
		public DateTime? FileDate { get; set; }

		public IMorphSource Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class MorphSource : IMorphSource
	{
		private readonly UIntPtr _handle;

		internal MorphSource(UIntPtr handle)
		{
			_handle = handle;
		}

		public string FileName => Native.ZkMorphSource_getFileName(_handle).MarshalAsString() ??
		                          throw new Exception("Failed to load morph source file name");

		public DateTime? FileDate => Native.ZkMorphSource_getFileDate(_handle).AsDateTime();

		public IMorphSource Cache()
		{
			return new CachedMorphSource
			{
				FileDate = FileDate,
				FileName = FileName
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IMorphMesh : ICacheable<IMorphMesh>
	{
		string Name { get; }
		IMultiResolutionMesh Mesh { get; }
		public int MorphPositionCount { get; }
		List<Vector3> MorphPositions { get; }
		int AnimationCount { get; }
		List<IMorphAnimation> Animations { get; }
		int SourceCount { get; }
		List<IMorphSource> Sources { get; }
		Vector3 GetMorphPosition(int i);
		IMorphAnimation GetAnimation(int i);
		IMorphSource GetSource(int i);
	}

	[Serializable]
	public struct CachedMorphMesh : IMorphMesh
	{
		public string Name { get; set; }
		public IMultiResolutionMesh Mesh { get; set; }

		public int MorphPositionCount => MorphPositions.Count;

		public List<Vector3> MorphPositions { get; set; }
		public int AnimationCount => Animations.Count;
		public List<IMorphAnimation> Animations { get; set; }
		public int SourceCount => Sources.Count;
		public List<IMorphSource> Sources { get; set; }

		public Vector3 GetMorphPosition(int i)
		{
			return MorphPositions[i];
		}

		public IMorphAnimation GetAnimation(int i)
		{
			return Animations[i];
		}

		public IMorphSource GetSource(int i)
		{
			return Sources[i];
		}

		public IMorphMesh Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class MorphMesh : IMorphMesh
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

		public IMultiResolutionMesh Mesh => new MultiResolutionMesh(Native.ZkMorphMesh_getMesh(_handle));

		public int MorphPositionCount => (int)Native.ZkMorphMesh_getMorphPositionCount(_handle);

		public List<Vector3> MorphPositions
		{
			get
			{
				var positions = new List<Vector3>();

				Native.ZkMorphMesh_enumerateMorphPositions(_handle, (_, v) =>
				{
					positions.Add(v);
					return false;
				}, UIntPtr.Zero);

				return positions;
			}
		}

		public int AnimationCount => (int)Native.ZkMorphMesh_getAnimationCount(_handle);

		public List<IMorphAnimation> Animations
		{
			get
			{
				var animation = new List<IMorphAnimation>();

				Native.ZkMorphMesh_enumerateAnimations(_handle, (_, anim) =>
				{
					animation.Add(new MorphAnimation(anim));
					return false;
				}, UIntPtr.Zero);

				return animation;
			}
		}

		public int SourceCount => (int)Native.ZkMorphMesh_getSourceCount(_handle);

		public List<IMorphSource> Sources
		{
			get
			{
				var sources = new List<IMorphSource>();

				Native.ZkMorphMesh_enumerateSources(_handle, (_, src) =>
				{
					sources.Add(new MorphSource(src));
					return false;
				}, UIntPtr.Zero);

				return sources;
			}
		}

		public IMorphMesh Cache()
		{
			return new CachedMorphMesh
			{
				Name = Name,
				Mesh = Mesh.Cache(),
				MorphPositions = MorphPositions,
				Animations = Animations.ConvertAll(ani => ani.Cache()),
				Sources = Sources.ConvertAll(src => src.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public Vector3 GetMorphPosition(int i)
		{
			return Native.ZkMorphMesh_getMorphPosition(_handle, (ulong)i);
		}

		public IMorphAnimation GetAnimation(int i)
		{
			return new MorphAnimation(Native.ZkMorphMesh_getAnimation(_handle, (ulong)i));
		}

		public IMorphSource GetSource(int i)
		{
			return new MorphSource(Native.ZkMorphMesh_getSource(_handle, (ulong)i));
		}

		~MorphMesh()
		{
			Native.ZkMorphMesh_del(_handle);
		}
	}
}