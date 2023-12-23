using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct AnimationSample
	{
		public Vector3 Position;
		public Quaternion Rotation;
	}

	public interface IModelAnimation : ICacheable<IModelAnimation>
	{
		string Name { get; }
		string Next { get; }
		uint Layer { get; }
		uint FrameCount { get; }
		uint NodeCount { get; }
		float Fps { get; }
		float FpsSource { get; }
		AxisAlignedBoundingBox BoundingBox { get; }
		uint Checksum { get; }
		string SourcePath { get; }
		DateTime? SourceDate { get; }
		string SourceScript { get; }
		ulong SampleCount { get; }
		List<AnimationSample> Samples { get; }
		uint[] NodeIndices { get; }
		AnimationSample GetSample(ulong i);
	}

	[Serializable]
	public class CachedModelAnimation : IModelAnimation
	{
		public string Name { get; set; }
		public string Next { get; set; }
		public uint Layer { get; set; }
		public uint FrameCount { get; set; }
		public uint NodeCount { get; set; }
		public float Fps { get; set; }
		public float FpsSource { get; set; }
		public AxisAlignedBoundingBox BoundingBox { get; set; }
		public uint Checksum { get; set; }
		public string SourcePath { get; set; }
		public DateTime? SourceDate { get; set; }
		public string SourceScript { get; set; }
		public ulong SampleCount => (ulong)Samples.LongCount();
		public List<AnimationSample> Samples { get; set; }
		public uint[] NodeIndices { get; set; }

		public AnimationSample GetSample(ulong i)
		{
			return Samples[(int)i];
		}

		public IModelAnimation Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class ModelAnimation : IModelAnimation
	{
		private readonly UIntPtr _handle;

		public ModelAnimation(string path)
		{
			_handle = Native.ZkModelAnimation_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model animation");
		}

		public ModelAnimation(Read r)
		{
			_handle = Native.ZkModelAnimation_load(r.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model animation");
		}

		public ModelAnimation(Vfs vfs, string name)
		{
			_handle = Native.ZkModelAnimation_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model animation");
		}

		public string Name => Native.ZkModelAnimation_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load model animation name");

		public string Next => Native.ZkModelAnimation_getNext(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load model animation next");

		public uint Layer => Native.ZkModelAnimation_getLayer(_handle);
		public uint FrameCount => Native.ZkModelAnimation_getFrameCount(_handle);
		public uint NodeCount => Native.ZkModelAnimation_getNodeCount(_handle);
		public float Fps => Native.ZkModelAnimation_getFps(_handle);
		public float FpsSource => Native.ZkModelAnimation_getFpsSource(_handle);
		public AxisAlignedBoundingBox BoundingBox => Native.ZkModelAnimation_getBbox(_handle);
		public uint Checksum => Native.ZkModelAnimation_getChecksum(_handle);

		public string SourcePath => Native.ZkModelAnimation_getSourcePath(_handle).MarshalAsString() ??
		                            throw new Exception("Failed to load model animation source path");

		public DateTime? SourceDate => Native.ZkModelAnimation_getSourceDate(_handle).AsDateTime();

		public string SourceScript => Native.ZkModelAnimation_getSourceScript(_handle).MarshalAsString() ??
		                              throw new Exception("Failed to load model animation source script");

		public ulong SampleCount => Native.ZkModelAnimation_getSampleCount(_handle);

		public List<AnimationSample> Samples
		{
			get
			{
				var samples = new List<AnimationSample>();

				Native.ZkModelAnimation_enumerateSamples(_handle, (_, sample) =>
				{
					samples.Add(Marshal.PtrToStructure<AnimationSample>(sample));
					return false;
				}, UIntPtr.Zero);

				return samples;
			}
		}

		public uint[] NodeIndices =>
			Native.ZkModelAnimation_getNodeIndices(_handle, out var size).MarshalAsArray<uint>(size);

		public IModelAnimation Cache()
		{
			return new CachedModelAnimation
			{
				Name = Name,
				Next = Next,
				Layer = Layer,
				FrameCount = FrameCount,
				NodeCount = NodeCount,
				Fps = Fps,
				FpsSource = FpsSource,
				BoundingBox = BoundingBox,
				Checksum = Checksum,
				SourcePath = SourcePath,
				SourceDate = SourceDate,
				SourceScript = SourceScript,
				Samples = Samples,
				NodeIndices = NodeIndices
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public AnimationSample GetSample(ulong i)
		{
			return Native.ZkModelAnimation_getSample(_handle, i);
		}

		~ModelAnimation()
		{
			Native.ZkModelAnimation_del(_handle);
		}
	}
}