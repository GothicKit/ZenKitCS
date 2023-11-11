using System;
using System.Drawing;

namespace ZenKit.Vobs
{
	public enum LightType
	{
		Point = 0,
		Spot = 1,
		Reserved0 = 2,
		Reserved1 = 3
	}

	public enum LightQuality
	{
		High = 0,
		Medium = 1,
		Low = 2
	}

	public class LightPreset
	{
		private readonly bool _delete = true;
		private readonly UIntPtr _handle;

		public LightPreset(Read buf, GameVersion version)
		{
			_handle = Native.ZkLightPreset_load(buf.Handle, version);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load light preset");
		}

		public LightPreset(string path, GameVersion version)
		{
			_handle = Native.ZkLightPreset_loadPath(path, version);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load light preset");
		}

		internal LightPreset(UIntPtr handle)
		{
			_handle = handle;
			_delete = false;
		}

		public string Preset => Native.ZkLightPreset_getPreset(_handle).MarshalAsString() ??
		                        throw new Exception("Failed to load light preset name");

		public LightType LightType => Native.ZkLightPreset_getLightType(_handle);
		public float Range => Native.ZkLightPreset_getRange(_handle);
		public Color Color => Native.ZkLightPreset_getColor(_handle).ToColor();
		public float ConeAngle => Native.ZkLightPreset_getConeAngle(_handle);
		public bool LightStatic => Native.ZkLightPreset_getIsStatic(_handle);
		public LightQuality Quality => Native.ZkLightPreset_getQuality(_handle);

		public string LensflareFx => Native.ZkLightPreset_getLensflareFx(_handle).MarshalAsString() ??
		                             throw new Exception("Failed to load light preset lensflare fx");

		public bool On => Native.ZkLightPreset_getOn(_handle);

		public float[] RangeAnimationScale => Native.ZkLightPreset_getRangeAnimationScale(_handle, out var count)
			.MarshalAsArray<float>(count);

		public float RangeAnimationFps => Native.ZkLightPreset_getRangeAnimationFps(_handle);
		public bool RangeAnimationSmooth => Native.ZkLightPreset_getRangeAnimationSmooth(_handle);

		public Color[] ColorAnimationList => Array.ConvertAll(Native
			.ZkLightPreset_getColorAnimationList(_handle, out var count)
			.MarshalAsArray<Native.ZkColor>(count), i => i.ToColor());

		public float ColorAnimationFps => Native.ZkLightPreset_getColorAnimationFps(_handle);
		public bool ColorAnimationSmooth => Native.ZkLightPreset_getColorAnimationSmooth(_handle);
		public bool CanMove => Native.ZkLightPreset_getCanMove(_handle);

		~LightPreset()
		{
			if (_delete) Native.ZkLightPreset_del(_handle);
		}
	}

	public class Light : VirtualObject
	{
		public Light(Read buf, GameVersion version) : base(Native.ZkLight_load(buf.Handle, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load light vob");
		}

		public Light(string path, GameVersion version) : base(Native.ZkLight_loadPath(path, version), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load light vob");
		}

		internal Light(UIntPtr handle, bool delete) : base(handle, delete)
		{
		}

		public string Preset => Native.ZkLight_getPreset(Handle).MarshalAsString() ??
		                        throw new Exception("Failed to load light name");

		public LightType LightType => Native.ZkLight_getLightType(Handle);
		public float Range => Native.ZkLight_getRange(Handle);
		public Color Color => Native.ZkLight_getColor(Handle).ToColor();
		public float ConeAngle => Native.ZkLight_getConeAngle(Handle);
		public bool LightStatic => Native.ZkLight_getIsStatic(Handle);
		public LightQuality Quality => Native.ZkLight_getQuality(Handle);

		public string LensflareFx => Native.ZkLight_getLensflareFx(Handle).MarshalAsString() ??
		                             throw new Exception("Failed to load light lensflare fx");

		public bool On => Native.ZkLight_getOn(Handle);

		public float[] RangeAnimationScale => Native.ZkLight_getRangeAnimationScale(Handle, out var count)
			.MarshalAsArray<float>(count);

		public float RangeAnimationFps => Native.ZkLight_getRangeAnimationFps(Handle);
		public bool RangeAnimationSmooth => Native.ZkLight_getRangeAnimationSmooth(Handle);

		public Color[] ColorAnimationList => Array.ConvertAll(Native
			.ZkLight_getColorAnimationList(Handle, out var count)
			.MarshalAsArray<Native.ZkColor>(count), i => i.ToColor());

		public float ColorAnimationFps => Native.ZkLight_getColorAnimationFps(Handle);
		public bool ColorAnimationSmooth => Native.ZkLight_getColorAnimationSmooth(Handle);
		public bool CanMove => Native.ZkLight_getCanMove(Handle);

		protected override void Delete()
		{
			Native.ZkLight_del(Handle);
		}
	}
}