using System;
using System.Drawing;
using System.Numerics;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	public enum MaterialGroup
	{
		Undefined = 0,
		Metal = 1,
		Stone = 2,
		Wood = 3,
		Earth = 4,
		Water = 5,
		Snow = 6
	}

	[Serializable]
	public enum AnimationMapping
	{
		None = 0,
		Linear = 1
	}

	[Serializable]
	public enum WaveMode
	{
		None = 0,
		GroundAmbient = 1,
		Ground = 2,
		WallAmbient = 3,
		Wall = 4,
		Environment = 5,
		WindAmbient = 6,
		Wind = 7
	}

	[Serializable]
	public enum WaveSpeed
	{
		None = 0,
		Slow = 1,
		Normal = 2,
		Fast = 3
	}

	[Serializable]
	public enum AlphaFunction
	{
		Default = 0,
		None = 1,
		Blend = 2,
		Add = 3,
		Subtract = 4,
		Multiply = 5,
		MultiplyAlt = 6
	}

	namespace Materialized
	{
		[Serializable]
		public struct Material
		{
			public string Name;
			public MaterialGroup Group;
			public Color Color;
			public float SmoothAngle;
			public string Texture;
			public Vector2 TextureScale;
			public float TextureAnimationFps;
			public AnimationMapping TextureAnimationMapping;
			public Vector2 TextureAnimationMappingDirection;
			public bool DisableCollision;
			public bool DisableLightmap;
			public bool DontCollapse;
			public string DetailObject;
			public float DetailObjectScale;
			public bool ForceOccluder;
			public bool EnvironmentMapping;
			public float EnvironmentMappingStrength;
			public WaveMode WaveMode;
			public WaveSpeed WaveSpeed;
			public float WaveAmplitude;
			public float WaveGridSize;
			public bool IgnoreSun;
			public AlphaFunction AlphaFunction;
			public Vector2 DefaultMapping;
		}
	}

	public class Material : IMaterializing<Materialized.Material>
	{
		private readonly bool _delete;
		private readonly UIntPtr _handle;

		public Material(Read buf)
		{
			_handle = Native.ZkMaterial_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load material");
			_delete = true;
		}

		public Material(string path)
		{
			_handle = Native.ZkMaterial_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load material");
			_delete = true;
		}

		internal Material(UIntPtr handle)
		{
			_handle = handle;
			_delete = false;
		}

		public string Name => Native.ZkMaterial_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load material name");

		public MaterialGroup Group => Native.ZkMaterial_getGroup(_handle);
		public Color Color => Native.ZkMaterial_getColor(_handle).ToColor();
		public float SmoothAngle => Native.ZkMaterial_getSmoothAngle(_handle);

		public string Texture => Native.ZkMaterial_getTexture(_handle).MarshalAsString() ??
		                         throw new Exception("Failed to load material texture");

		public Vector2 TextureScale => Native.ZkMaterial_getTextureScale(_handle);
		public float TextureAnimationFps => Native.ZkMaterial_getTextureAnimationFps(_handle);
		public AnimationMapping TextureAnimationMapping => Native.ZkMaterial_getTextureAnimationMapping(_handle);

		public Vector2 TextureAnimationMappingDirection =>
			Native.ZkMaterial_getTextureAnimationMappingDirection(_handle);

		public bool DisableCollision => Native.ZkMaterial_getDisableCollision(_handle);
		public bool DisableLightmap => Native.ZkMaterial_getDisableLightmap(_handle);
		public bool DontCollapse => Native.ZkMaterial_getDontCollapse(_handle);

		public string DetailObject => Native.ZkMaterial_getDetailObject(_handle).MarshalAsString() ??
		                              throw new Exception("Failed to load material detail object");

		public float DetailObjectScale => Native.ZkMaterial_getDetailObjectScale(_handle);
		public bool ForceOccluder => Native.ZkMaterial_getForceOccluder(_handle);
		public bool EnvironmentMapping => Native.ZkMaterial_getEnvironmentMapping(_handle);
		public float EnvironmentMappingStrength => Native.ZkMaterial_getEnvironmentMappingStrength(_handle);
		public WaveMode WaveMode => Native.ZkMaterial_getWaveMode(_handle);
		public WaveSpeed WaveSpeed => Native.ZkMaterial_getWaveSpeed(_handle);
		public float WaveAmplitude => Native.ZkMaterial_getWaveAmplitude(_handle);
		public float WaveGridSize => Native.ZkMaterial_getWaveGridSize(_handle);
		public bool IgnoreSun => Native.ZkMaterial_getIgnoreSun(_handle);
		public AlphaFunction AlphaFunction => Native.ZkMaterial_getAlphaFunction(_handle);
		public Vector2 DefaultMapping => Native.ZkMaterial_getDefaultMapping(_handle);

		public Materialized.Material Materialize()
		{
			return new Materialized.Material
			{
				Name = Name,
				Group = Group,
				Color = Color,
				SmoothAngle = SmoothAngle,
				Texture = Texture,
				TextureScale = TextureScale,
				TextureAnimationFps = TextureAnimationFps,
				TextureAnimationMapping = TextureAnimationMapping,
				TextureAnimationMappingDirection = TextureAnimationMappingDirection,
				DisableCollision = DisableCollision,
				DisableLightmap = DisableLightmap,
				DontCollapse = DontCollapse,
				DetailObject = DetailObject,
				DetailObjectScale = DetailObjectScale,
				ForceOccluder = ForceOccluder,
				EnvironmentMapping = EnvironmentMapping,
				EnvironmentMappingStrength = EnvironmentMappingStrength,
				WaveMode = WaveMode,
				WaveSpeed = WaveSpeed,
				WaveAmplitude = WaveAmplitude,
				WaveGridSize = WaveGridSize,
				IgnoreSun = IgnoreSun,
				AlphaFunction = AlphaFunction,
				DefaultMapping = DefaultMapping
			};
		}

		~Material()
		{
			if (_delete) Native.ZkMaterial_del(_handle);
		}
	}
}