using System;
using System.Collections.Generic;
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

	public interface ILightPreset
	{
		public string Preset { get; set; }
		public LightType LightType { get; set; }
		public float Range { get; set; }
		public Color Color { get; set; }
		public float ConeAngle { get; set; }
		public bool LightStatic { get; set; }
		public LightQuality Quality { get; set; }
		public string LensflareFx { get; set; }
		public bool On { get; set; }
		public List<float> RangeAnimationScale { get; set; }
		public float RangeAnimationFps { get; set; }
		public bool RangeAnimationSmooth { get; set; }
		public List<Color> ColorAnimationList { get; set; }
		public float ColorAnimationFps { get; set; }
		public bool ColorAnimationSmooth { get; set; }
		public bool CanMove { get; set; }
	}

	public class LightPreset : ILightPreset
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

		public string Preset
		{
			get =>
				Native.ZkLightPreset_getPreset(_handle).MarshalAsString() ??
				throw new Exception("Failed to load light preset name");
			set => Native.ZkLightPreset_setPreset(_handle, value);
		}


		public LightType LightType
		{
			get => Native.ZkLightPreset_getLightType(_handle);
			set => Native.ZkLightPreset_setLightType(_handle, value);
		}

		public float Range
		{
			get => Native.ZkLightPreset_getRange(_handle);
			set => Native.ZkLightPreset_setRange(_handle, value);
		}

		public Color Color
		{
			get => Native.ZkLightPreset_getColor(_handle).ToColor();
			set => Native.ZkLightPreset_setColor(_handle, new Native.Structs.ZkColor(value));
		}

		public float ConeAngle
		{
			get => Native.ZkLightPreset_getConeAngle(_handle);
			set => Native.ZkLightPreset_setConeAngle(_handle, value);
		}

		public bool LightStatic
		{
			get => Native.ZkLightPreset_getIsStatic(_handle);
			set => Native.ZkLightPreset_setIsStatic(_handle, value);
		}

		public LightQuality Quality
		{
			get => Native.ZkLightPreset_getQuality(_handle);
			set => Native.ZkLightPreset_setQuality(_handle, value);
		}

		public string LensflareFx
		{
			get => Native.ZkLightPreset_getLensflareFx(_handle).MarshalAsString() ??
			       throw new Exception("Failed to load light preset lensflare fx");
			set => Native.ZkLightPreset_setLensflareFx(_handle, value);
		}

		public bool On
		{
			get => Native.ZkLightPreset_getOn(_handle);
			set => Native.ZkLightPreset_setOn(_handle, value);
		}

		public List<float> RangeAnimationScale
		{
			get =>
				Native.ZkLightPreset_getRangeAnimationScale(_handle, out var count)
					.MarshalAsList<float>(count);
			set => Native.ZkLightPreset_setRangeAnimationScale(_handle, value.ToArray(), (ulong)value.Count);
		}

		public float RangeAnimationFps
		{
			get => Native.ZkLightPreset_getRangeAnimationFps(_handle);
			set => Native.ZkLightPreset_setRangeAnimationFps(_handle, value);
		}

		public bool RangeAnimationSmooth
		{
			get => Native.ZkLightPreset_getRangeAnimationSmooth(_handle);
			set => Native.ZkLightPreset_setRangeAnimationSmooth(_handle, value);
		}

		public List<Color> ColorAnimationList
		{
			get
			{
				var colors = new List<Color>();
				var count = (int)Native.ZkLightPreset_getColorAnimationCount(_handle);
				for (var i = 0; i < count; ++i)
					colors.Add(Native.ZkLightPreset_getColorAnimationItem(_handle, (ulong)i).ToColor());
				return colors;
			}
			set => Native.ZkLightPreset_setColorAnimationList(_handle,
				Array.ConvertAll(value.ToArray(), c => new Native.Structs.ZkColor(c)), (ulong)value.Count);
		}


		public float ColorAnimationFps
		{
			get => Native.ZkLightPreset_getColorAnimationFps(_handle);
			set => Native.ZkLightPreset_setColorAnimationFps(_handle, value);
		}

		public bool ColorAnimationSmooth
		{
			get => Native.ZkLightPreset_getColorAnimationSmooth(_handle);
			set => Native.ZkLightPreset_setColorAnimationSmooth(_handle, value);
		}

		public bool CanMove
		{
			get => Native.ZkLightPreset_getCanMove(_handle);
			set => Native.ZkLightPreset_setCanMove(_handle, value);
		}


		~LightPreset()
		{
			if (_delete) Native.ZkLightPreset_del(_handle);
		}
	}

	public interface ILight : ILightPreset, IVirtualObject
	{
		
	}

	public class Light : VirtualObject, ILight
	{
		public Light() : base(Native.ZkVirtualObject_new(VirtualObjectType.zCVobLight))
		{
		}

		public Light(Read buf, GameVersion version) : base(Native.ZkLight_load(buf.Handle, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load light vob");
		}

		public Light(string path, GameVersion version) : base(Native.ZkLight_loadPath(path, version))
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load light vob");
		}

		internal Light(UIntPtr handle) : base(handle)
		{
		}

		public string Preset
		{
			get =>
				Native.ZkLight_getPreset(Handle).MarshalAsString() ??
				throw new Exception("Failed to load light name");
			set => Native.ZkLight_setPreset(Handle, value);
		}


		public LightType LightType
		{
			get => Native.ZkLight_getLightType(Handle);
			set => Native.ZkLight_setLightType(Handle, value);
		}

		public float Range
		{
			get => Native.ZkLight_getRange(Handle);
			set => Native.ZkLight_setRange(Handle, value);
		}

		public Color Color
		{
			get => Native.ZkLight_getColor(Handle).ToColor();
			set => Native.ZkLight_setColor(Handle, new Native.Structs.ZkColor(value));
		}

		public float ConeAngle
		{
			get => Native.ZkLight_getConeAngle(Handle);
			set => Native.ZkLight_setConeAngle(Handle, value);
		}

		public bool LightStatic
		{
			get => Native.ZkLight_getIsStatic(Handle);
			set => Native.ZkLight_setIsStatic(Handle, value);
		}

		public LightQuality Quality
		{
			get => Native.ZkLight_getQuality(Handle);
			set => Native.ZkLight_setQuality(Handle, value);
		}


		public string LensflareFx
		{
			get => Native.ZkLight_getLensflareFx(Handle).MarshalAsString() ??
			       throw new Exception("Failed to load light lensflare fx");
			set => Native.ZkLight_setLensflareFx(Handle, value);
		}


		public bool On
		{
			get => Native.ZkLight_getOn(Handle);
			set => Native.ZkLight_setOn(Handle, value);
		}


		public List<float> RangeAnimationScale
		{
			get =>
				Native.ZkLight_getRangeAnimationScale(Handle, out var count)
					.MarshalAsList<float>(count);
			set => Native.ZkLight_setRangeAnimationScale(Handle, value.ToArray(), (ulong)value.Count);
		}


		public float RangeAnimationFps
		{
			get => Native.ZkLight_getRangeAnimationFps(Handle);
			set => Native.ZkLight_setRangeAnimationFps(Handle, value);
		}

		public bool RangeAnimationSmooth
		{
			get => Native.ZkLight_getRangeAnimationSmooth(Handle);
			set => Native.ZkLight_setRangeAnimationSmooth(Handle, value);
		}

		public List<Color> ColorAnimationList
		{
			get
			{
				var colors = new List<Color>();
				var count = (int)Native.ZkLight_getColorAnimationCount(Handle);
				for (var i = 0; i < count; ++i)
					colors.Add(Native.ZkLight_getColorAnimationItem(Handle, (ulong)i).ToColor());
				return colors;
			}
			set => Native.ZkLight_setColorAnimationList(Handle,
				Array.ConvertAll(value.ToArray(), c => new Native.Structs.ZkColor(c)), (ulong)value.Count);
		}


		public float ColorAnimationFps
		{
			get => Native.ZkLight_getColorAnimationFps(Handle);
			set => Native.ZkLight_setColorAnimationFps(Handle, value);
		}

		public bool ColorAnimationSmooth
		{
			get => Native.ZkLight_getColorAnimationSmooth(Handle);
			set => Native.ZkLight_setColorAnimationSmooth(Handle, value);
		}

		public bool CanMove
		{
			get => Native.ZkLight_getCanMove(Handle);
			set => Native.ZkLight_setCanMove(Handle, value);
		}


		protected override void Delete()
		{
			Native.ZkLight_del(Handle);
		}
	}
}
