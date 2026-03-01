using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ZenKit.Daedalus;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	public enum DaedalusInstanceType
	{
		GuildValues = 0,
		Npc = 1,
		Mission = 2,
		Item = 3,
		Focus = 4,
		Info = 5,
		ItemReact = 6,
		Spell = 7,
		Svm = 8,
		Menu = 9,
		MenuItem = 10,
		Camera = 11,
		MusicSystem = 12,
		MusicTheme = 13,
		MusicJingle = 14,
		ParticleEffect = 15,
		EffectBase = 16,
		ParticleEffectEmitKey = 17,
		FightAi = 18,
		SoundEffect = 19,
		SoundSystem = 20,
		Invalid = 21
	}

	public class DaedalusInstance
	{
		protected DaedalusInstance(UIntPtr handle)
		{
			Handle = handle;
		}

		~DaedalusInstance()
		{
			Native.ZkDaedalusInstance_release(Handle);
		}

		internal UIntPtr Handle { get; }

		public DaedalusInstanceType Type => Native.ZkDaedalusInstance_getType(Handle);
		public int Index => (int)Native.ZkDaedalusInstance_getIndex(Handle);

		public object? UserData
		{
			get
			{
				var userPtr = Native.ZkDaedalusInstance_getUserPointer(Handle);
				return userPtr == IntPtr.Zero ? null : GCHandle.FromIntPtr(userPtr).Target;
			}
			set
			{
				// TODO: This leaks a handle.
				var handle = GCHandle.Alloc(value, GCHandleType.Weak);
				Native.ZkDaedalusInstance_setUserPointer(Handle, GCHandle.ToIntPtr(handle));
			}
		}

		public static DaedalusInstance? FromNative(UIntPtr handle)
		{
			if (handle == UIntPtr.Zero) return null;

			return Native.ZkDaedalusInstance_getType(handle) switch
			{
				DaedalusInstanceType.GuildValues => new GuildValuesInstance(handle),
				DaedalusInstanceType.Npc => new NpcInstance(handle),
				DaedalusInstanceType.Mission => new MissionInstance(handle),
				DaedalusInstanceType.Item => new ItemInstance(handle),
				DaedalusInstanceType.Focus => new FocusInstance(handle),
				DaedalusInstanceType.Info => new InfoInstance(handle),
				DaedalusInstanceType.ItemReact => new ItemReactInstance(handle),
				DaedalusInstanceType.Spell => new SpellInstance(handle),
				DaedalusInstanceType.Menu => new MenuInstance(handle),
				DaedalusInstanceType.MenuItem => new MenuItemInstance(handle),
				DaedalusInstanceType.Camera => new CameraInstance(handle),
				DaedalusInstanceType.MusicSystem => new MusicSystemInstance(handle),
				DaedalusInstanceType.MusicTheme => new MusicThemeInstance(handle),
				DaedalusInstanceType.MusicJingle => new MusicJingleInstance(handle),
				DaedalusInstanceType.ParticleEffect => new ParticleEffectInstance(handle),
				DaedalusInstanceType.EffectBase => new EffectBaseInstance(handle),
				DaedalusInstanceType.ParticleEffectEmitKey => new ParticleEffectEmitKeyInstance(handle),
				DaedalusInstanceType.FightAi => new FightAiInstance(handle),
				DaedalusInstanceType.SoundEffect => new SoundEffectInstance(handle),
				DaedalusInstanceType.SoundSystem => new SoundSystemInstance(handle),
				DaedalusInstanceType.Svm => new SvmInstance(handle),
				_ => new DaedalusInstance(handle)
			};
		}

		public static DaedalusInstance CreateTransient(IDaedalusTransientInstance impl)
		{
			return new DaedalusInstance(IDaedalusTransientInstance.Create(impl));
		}
	}

	public interface IDaedalusTransientInstance
	{
		private static readonly List<GCHandle> KeepaliveList = new List<GCHandle>();
		private static readonly Native.Callbacks.ZkDaedalusTransientInstanceIntGetter NativeIntGetterCallback =
			_nativeIntGetterCallbackHandler;
		
		private static readonly Native.Callbacks.ZkDaedalusTransientInstanceIntSetter NativeIntSetterCallback =
			_nativeIntSetterCallbackHandler;
		
		private static readonly Native.Callbacks.ZkDaedalusTransientInstanceFloatGetter NativeFloatGetterCallback =
			_nativeFloatGetterCallbackHandler;
		
		private static readonly Native.Callbacks.ZkDaedalusTransientInstanceFloatSetter NativeFloatSetterCallback =
			_nativeFloatSetterCallbackHandler;
		
		private static readonly Native.Callbacks.ZkDaedalusTransientInstanceStringGetter NativeStringGetterCallback =
			_nativeStringGetterCallbackHandler;
		
		private static readonly Native.Callbacks.ZkDaedalusTransientInstanceStringSetter NativeStringSetterCallback =
			_nativeStringSetterCallbackHandler;

		internal static UIntPtr Create(IDaedalusTransientInstance impl)
		{
			
			var ptr = GCHandle.Alloc(impl);
			KeepaliveList.Add(ptr);
			return Native.ZkDaedalusInstance_newTransient(
				GCHandle.ToIntPtr(ptr),
				NativeIntGetterCallback,
				NativeIntSetterCallback,
				NativeFloatGetterCallback,
				NativeFloatSetterCallback,
				NativeStringGetterCallback,
				NativeStringSetterCallback
			);
		}
		
		public static void ReleaseAll()
		{
			KeepaliveList.ForEach(handle => handle.Free());
			KeepaliveList.Clear();
		}

		public void SetInt(DaedalusSymbol sym, ushort idx, int val);
		public void SetFloat(DaedalusSymbol sym, ushort idx, float val);
		public void SetString(DaedalusSymbol sym, ushort idx, string val);

		public int GetInt(DaedalusSymbol sym, ushort idx);
		public float GetFloat(DaedalusSymbol sym, ushort idx);
		public string GetString(DaedalusSymbol sym, ushort idx);
		
		[MonoPInvokeCallback]
		private static int _nativeIntGetterCallbackHandler(IntPtr ctx, UIntPtr sym, ushort idx)
		{
			var gcHandle = GCHandle.FromIntPtr(ctx);
			var impl = (IDaedalusTransientInstance)gcHandle.Target;
			return impl.GetInt(new DaedalusSymbol(sym), idx);
		}
		
		[MonoPInvokeCallback]
		private static void _nativeIntSetterCallbackHandler(IntPtr ctx, UIntPtr sym, ushort idx, int val)
		{
			var gcHandle = GCHandle.FromIntPtr(ctx);
			var impl = (IDaedalusTransientInstance)gcHandle.Target;
			impl.SetInt(new DaedalusSymbol(sym), idx, val);
		}
		
		[MonoPInvokeCallback]
		private static float _nativeFloatGetterCallbackHandler(IntPtr ctx, UIntPtr sym, ushort idx)
		{
			var gcHandle = GCHandle.FromIntPtr(ctx);
			var impl = (IDaedalusTransientInstance)gcHandle.Target;
			return impl.GetFloat(new DaedalusSymbol(sym), idx);
		}
		
		[MonoPInvokeCallback]
		private static void _nativeFloatSetterCallbackHandler(IntPtr ctx, UIntPtr sym, ushort idx, float val)
		{
			var gcHandle = GCHandle.FromIntPtr(ctx);
			var impl = (IDaedalusTransientInstance)gcHandle.Target;
			impl.SetFloat(new DaedalusSymbol(sym), idx, val);
		}
		
		private static IntPtr _stringCache = IntPtr.Zero;
		
		[MonoPInvokeCallback]
		private static IntPtr _nativeStringGetterCallbackHandler(IntPtr ctx, UIntPtr sym, ushort idx)
		{
			var gcHandle = GCHandle.FromIntPtr(ctx);
			var impl = (IDaedalusTransientInstance)gcHandle.Target;

			// Need to avoid memory leaks
			if (_stringCache != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(_stringCache);
				_stringCache = IntPtr.Zero;
			}
			
			_stringCache = GothicStringMarshaller.Instance.MarshalManagedToNative(impl.GetString(new DaedalusSymbol(sym), idx));
			return _stringCache;
		}
		
		[MonoPInvokeCallback]
		private static void _nativeStringSetterCallbackHandler(IntPtr ctx, UIntPtr sym, ushort idx, string val)
		{
			var gcHandle = GCHandle.FromIntPtr(ctx);
			var impl = (IDaedalusTransientInstance)gcHandle.Target;
			impl.SetString(new DaedalusSymbol(sym), idx, val);
		}
	}
}