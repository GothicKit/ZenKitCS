using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ZenKit.Daedalus;
using ZenKit.Util;

namespace ZenKit
{
	public class DaedalusVm : DaedalusScript
	{
		public delegate void ExternalDefaultFunction(DaedalusVm vm, DaedalusSymbol sym);

		public delegate TR ExternalFunc<out TR>();

		public delegate TR ExternalFunc<out TR, in TP0>(TP0 p0);

		public delegate TR ExternalFunc<out TR, in TP0, in TP1>(TP0 p0, TP1 p1);

		public delegate TR ExternalFunc<out TR, in TP0, in TP1, in TP2>(TP0 p0, TP1 p1, TP2 p2);

		public delegate TR ExternalFunc<out TR, in TP0, in TP1, in TP2, in TP3>(TP0 p0, TP1 p1, TP2 p2, TP3 p3);

		public delegate TR ExternalFunc<out TR, in TP0, in TP1, in TP2, in TP3, in TP4>(TP0 p0, TP1 p1, TP2 p2, TP3 p3,
			TP4 p4);

		public delegate TR ExternalFunc<out TR, in TP0, in TP1, in TP2, in TP3, in TP4, in TP5>(TP0 p0, TP1 p1, TP2 p2,
			TP3 p3, TP4 p4, TP5 p5);

		public delegate TR ExternalFunc<out TR, in TP0, in TP1, in TP2, in TP3, in TP4, in TP5, in TP6>(TP0 p0, TP1 p1,
			TP2 p2, TP3 p3, TP4 p4, TP5 p5, TP6 p6);

		public delegate TR ExternalFunc<out TR, in TP0, in TP1, in TP2, in TP3, in TP4, in TP5, in TP6, in TP7>(TP0 p0,
			TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5, TP6 p6, TP7 p7);

		public delegate TR ExternalFunc<out TR, in TP0, in TP1, in TP2, in TP3, in TP4, in TP5, in TP6, in TP7, in TP8>(
			TP0 p0, TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5, TP6 p6, TP7 p7, TP8 p8);

		public delegate TR ExternalFunc<out TR, in TP0, in TP1, in TP2, in TP3, in TP4, in TP5, in TP6, in TP7, in TP8,
			in TP9>(TP0 p0, TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5, TP6 p6, TP7 p7, TP8 p8, TP9 p9);

		public delegate void ExternalFuncV();

		public delegate void ExternalFuncV<in TP0>(TP0 p0);

		public delegate void ExternalFuncV<in TP0, in TP1>(TP0 p0, TP1 p1);

		public delegate void ExternalFuncV<in TP0, in TP1, in TP2>(TP0 p0, TP1 p1, TP2 p2);

		public delegate void ExternalFuncV<in TP0, in TP1, in TP2, in TP3>(TP0 p0, TP1 p1, TP2 p2, TP3 p3);

		public delegate void ExternalFuncV<in TP0, in TP1, in TP2, in TP3, in TP4>(TP0 p0, TP1 p1, TP2 p2, TP3 p3,
			TP4 p4);

		public delegate void ExternalFuncV<in TP0, in TP1, in TP2, in TP3, in TP4, in TP5>(TP0 p0, TP1 p1, TP2 p2,
			TP3 p3, TP4 p4, TP5 p5);

		public delegate void ExternalFuncV<in TP0, in TP1, in TP2, in TP3, in TP4, in TP5, in TP6>(TP0 p0, TP1 p1,
			TP2 p2, TP3 p3, TP4 p4, TP5 p5, TP6 p6);

		public delegate void ExternalFuncV<in TP0, in TP1, in TP2, in TP3, in TP4, in TP5, in TP6, in TP7>(TP0 p0,
			TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5, TP6 p6, TP7 p7);

		public delegate void ExternalFuncV<in TP0, in TP1, in TP2, in TP3, in TP4, in TP5, in TP6, in TP7, in TP8>(
			TP0 p0, TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5, TP6 p6, TP7 p7, TP8 p8);

		public delegate void ExternalFuncV<in TP0, in TP1, in TP2, in TP3, in TP4, in TP5, in TP6, in TP7, in TP8,
			in TP9>(TP0 p0, TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5, TP6 p6, TP7 p7, TP8 p8, TP9 p9);

		private readonly List<GCHandle> _externalCallbacks = new List<GCHandle>();

		private readonly Native.Callbacks.ZkDaedalusVmExternalCallback _nativeExternalCallback =
			_nativeExternalCallbackHandler;

		private readonly Native.Callbacks.ZkDaedalusVmExternalDefaultCallback _nativeExternalCallbackDefault =
			_nativeExternalCallbackDefaultHandle;

		public DaedalusVm(string path) : base(Native.ZkDaedalusVm_loadPath(path), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load DaedalusVm");
		}

		public DaedalusVm(Read r) : base(Native.ZkDaedalusVm_load(r.Handle), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load DaedalusVm");
		}

		public DaedalusVm(Vfs vfs, string name) : base(Native.ZkDaedalusVm_loadVfs(vfs.Handle, name), true)
		{
			if (Handle == UIntPtr.Zero) throw new Exception("Failed to load DaedalusVm");
		}

		private DaedalusVm(UIntPtr handle) : base(handle, false)
		{
		}

		public DaedalusInstance? GlobalSelf
		{
			get => DaedalusInstance.FromNative(Native.ZkDaedalusVm_getGlobalSelf(Handle));
			set => Native.ZkDaedalusVm_setGlobalSelf(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public DaedalusInstance? GlobalOther
		{
			get => DaedalusInstance.FromNative(Native.ZkDaedalusVm_getGlobalOther(Handle));
			set => Native.ZkDaedalusVm_setGlobalOther(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public DaedalusInstance? GlobalVictim
		{
			get => DaedalusInstance.FromNative(Native.ZkDaedalusVm_getGlobalVictim(Handle));
			set => Native.ZkDaedalusVm_setGlobalVictim(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public DaedalusInstance? GlobalHero
		{
			get => DaedalusInstance.FromNative(Native.ZkDaedalusVm_getGlobalHero(Handle));
			set => Native.ZkDaedalusVm_setGlobalHero(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		public DaedalusInstance? GlobalItem
		{
			get => DaedalusInstance.FromNative(Native.ZkDaedalusVm_getGlobalItem(Handle));
			set => Native.ZkDaedalusVm_setGlobalItem(Handle, value?.Handle ?? UIntPtr.Zero);
		}

		protected override void Delete()
		{
			_externalCallbacks.ForEach(handle => handle.Free());
			Native.ZkDaedalusVm_del(Handle);
		}

		public void PrintStackTrace()
		{
			Native.ZkDaedalusVm_printStackTrace(Handle);
		}

		public T AllocInstance<T>(string symbolName)
		{
			var sym = GetSymbolByName(symbolName);
			if (sym == null) throw new Exception("Symbol not found");
			return AllocInstance<T>(sym);
		}

		public T AllocInstance<T>(DaedalusSymbol symbol)
		{
			DaedalusInstanceType type;

			if (typeof(T) == typeof(GuildValuesInstance)) type = DaedalusInstanceType.GuildValues;
			else if (typeof(T) == typeof(NpcInstance)) type = DaedalusInstanceType.Npc;
			else if (typeof(T) == typeof(MissionInstance)) type = DaedalusInstanceType.Mission;
			else if (typeof(T) == typeof(ItemInstance)) type = DaedalusInstanceType.Item;
			else if (typeof(T) == typeof(FocusInstance)) type = DaedalusInstanceType.Focus;
			else if (typeof(T) == typeof(InfoInstance)) type = DaedalusInstanceType.Info;
			else if (typeof(T) == typeof(ItemReactInstance)) type = DaedalusInstanceType.ItemReact;
			else if (typeof(T) == typeof(SpellInstance)) type = DaedalusInstanceType.Spell;
			else if (typeof(T) == typeof(MenuInstance)) type = DaedalusInstanceType.Menu;
			else if (typeof(T) == typeof(MenuItemInstance)) type = DaedalusInstanceType.MenuItem;
			else if (typeof(T) == typeof(CameraInstance)) type = DaedalusInstanceType.Camera;
			else if (typeof(T) == typeof(MusicSystemInstance)) type = DaedalusInstanceType.MusicSystem;
			else if (typeof(T) == typeof(MusicThemeInstance)) type = DaedalusInstanceType.MusicTheme;
			else if (typeof(T) == typeof(MusicJingleInstance)) type = DaedalusInstanceType.MusicJingle;
			else if (typeof(T) == typeof(ParticleEffectInstance)) type = DaedalusInstanceType.ParticleEffect;
			else if (typeof(T) == typeof(EffectBaseInstance)) type = DaedalusInstanceType.EffectBase;
			else if (typeof(T) == typeof(ParticleEffectEmitKeyInstance))
				type = DaedalusInstanceType.ParticleEffectEmitKey;
			else if (typeof(T) == typeof(FightAiInstance)) type = DaedalusInstanceType.FightAi;
			else if (typeof(T) == typeof(SoundEffectInstance)) type = DaedalusInstanceType.SoundEffect;
			else if (typeof(T) == typeof(SoundSystemInstance)) type = DaedalusInstanceType.SoundSystem;
			else if (typeof(T) == typeof(InvalidOperationException)) type = DaedalusInstanceType.Svm;
			else throw new NotSupportedException("Must be DaedalusInstance");

			var ptr = DaedalusInstance.FromNative(Native.ZkDaedalusVm_allocInstance(Handle, symbol.Handle, type)) ??
			          throw new InvalidOperationException();
			return (T)(object)ptr;
		}

		public void InitInstance(DaedalusInstance instance)
		{
			Native.ZkDaedalusVm_initInstanceDirect(Handle, instance.Handle);
		}

		public T InitInstance<T>(string symbolName)
		{
			var sym = GetSymbolByName(symbolName);
			if (sym == null) throw new Exception("Symbol not found");
			return InitInstance<T>(sym);
		}

		public T InitInstance<T>(DaedalusSymbol symbol)
		{
			DaedalusInstanceType type;

			if (typeof(T) == typeof(GuildValuesInstance)) type = DaedalusInstanceType.GuildValues;
			else if (typeof(T) == typeof(NpcInstance)) type = DaedalusInstanceType.Npc;
			else if (typeof(T) == typeof(MissionInstance)) type = DaedalusInstanceType.Mission;
			else if (typeof(T) == typeof(ItemInstance)) type = DaedalusInstanceType.Item;
			else if (typeof(T) == typeof(FocusInstance)) type = DaedalusInstanceType.Focus;
			else if (typeof(T) == typeof(InfoInstance)) type = DaedalusInstanceType.Info;
			else if (typeof(T) == typeof(ItemReactInstance)) type = DaedalusInstanceType.ItemReact;
			else if (typeof(T) == typeof(SpellInstance)) type = DaedalusInstanceType.Spell;
			else if (typeof(T) == typeof(MenuInstance)) type = DaedalusInstanceType.Menu;
			else if (typeof(T) == typeof(MenuItemInstance)) type = DaedalusInstanceType.MenuItem;
			else if (typeof(T) == typeof(CameraInstance)) type = DaedalusInstanceType.Camera;
			else if (typeof(T) == typeof(MusicSystemInstance)) type = DaedalusInstanceType.MusicSystem;
			else if (typeof(T) == typeof(MusicThemeInstance)) type = DaedalusInstanceType.MusicTheme;
			else if (typeof(T) == typeof(MusicJingleInstance)) type = DaedalusInstanceType.MusicJingle;
			else if (typeof(T) == typeof(ParticleEffectInstance)) type = DaedalusInstanceType.ParticleEffect;
			else if (typeof(T) == typeof(EffectBaseInstance)) type = DaedalusInstanceType.EffectBase;
			else if (typeof(T) == typeof(ParticleEffectEmitKeyInstance))
				type = DaedalusInstanceType.ParticleEffectEmitKey;
			else if (typeof(T) == typeof(FightAiInstance)) type = DaedalusInstanceType.FightAi;
			else if (typeof(T) == typeof(SoundEffectInstance)) type = DaedalusInstanceType.SoundEffect;
			else if (typeof(T) == typeof(SoundSystemInstance)) type = DaedalusInstanceType.SoundSystem;
			else if (typeof(T) == typeof(SvmInstance)) type = DaedalusInstanceType.Svm;
			else throw new NotSupportedException("Must be DaedalusInstance");

			var ptr = DaedalusInstance.FromNative(Native.ZkDaedalusVm_initInstance(Handle, symbol.Handle, type)) ??
			          throw new InvalidOperationException();
			return (T)(object)ptr;
		}

		public void Call(string name)
		{
			var sym = GetSymbolByName(name);
			if (!(sym is { Type: DaedalusDataType.Function })) throw new Exception("Symbol not found");
			Native.ZkDaedalusVm_callFunction(Handle, sym.Handle);
		}

		public void Call(int symId)
		{
			var sym = GetSymbolByIndex(symId);
			if (!(sym is { Type: DaedalusDataType.Function })) throw new Exception("Symbol not found");
			Native.ZkDaedalusVm_callFunction(Handle, sym.Handle);
		}

		public TR Call<TR>(string name)
		{
			var sym = GetSymbolByName(name);
			if (!(sym is { Type: DaedalusDataType.Function })) throw new Exception("Symbol not found");
			Native.ZkDaedalusVm_callFunction(Handle, sym.Handle);

			if (!sym.HasReturn) throw new InvalidOperationException("The function does not return anything!");
			return Pop<TR>();
		}

		public TR Call<TR>(int symId)
		{
			var sym = GetSymbolByIndex(symId);
			if (!(sym is { Type: DaedalusDataType.Function })) throw new Exception("Symbol not found");
			Native.ZkDaedalusVm_callFunction(Handle, sym.Handle);

			if (!sym.HasReturn) throw new InvalidOperationException("The function does not return anything!");
			return Pop<TR>();
		}

		public TR Call<TR, TP0>(string name, TP0 p0)
		{
			Push(p0);
			return Call<TR>(name);
		}

		public TR Call<TR, TP0, TP1>(string name, TP0 p0, TP1 p1)
		{
			Push(p0);
			Push(p1);
			return Call<TR>(name);
		}

		public TR Call<TR, TP0, TP1, TP2>(string name, TP0 p0, TP1 p1, TP2 p2)
		{
			Push(p0);
			Push(p1);
			Push(p2);
			return Call<TR>(name);
		}

		public TR Call<TR, TP0, TP1, TP2, TP3>(string name, TP0 p0, TP1 p1, TP2 p2, TP3 p3)
		{
			Push(p0);
			Push(p1);
			Push(p2);
			Push(p3);
			return Call<TR>(name);
		}

		public TR Call<TR, TP0>(int symId, TP0 p0)
		{
			Push(p0);
			return Call<TR>(symId);
		}

		public TR Call<TR, TP0, TP1>(int symId, TP0 p0, TP1 p1)
		{
			Push(p0);
			Push(p1);
			return Call<TR>(symId);
		}

		public TR Call<TR, TP0, TP1, TP2>(int symId, TP0 p0, TP1 p1, TP2 p2)
		{
			Push(p0);
			Push(p1);
			Push(p2);
			return Call<TR>(symId);
		}

		public TR Call<TR, TP0, TP1, TP2, TP3>(int symId, TP0 p0, TP1 p1, TP2 p2, TP3 p3)
		{
			Push(p0);
			Push(p1);
			Push(p2);
			Push(p3);
			return Call<TR>(symId);
		}

		public void Call<TP0>(int symId, TP0 p0)
		{
			Push(p0);
			Call(symId);
		}

		public void Call<TP0, TP1>(int symId, TP0 p0, TP1 p1)
		{
			Push(p0);
			Push(p1);
			Call(symId);
		}

		public void Call<TP0, TP1, TP2>(int symId, TP0 p0, TP1 p1, TP2 p2)
		{
			Push(p0);
			Push(p1);
			Push(p2);
			Call(symId);
		}

		public void Call<TP0, TP1, TP2, TP3>(int symId, TP0 p0, TP1 p1, TP2 p2, TP3 p3)
		{
			Push(p0);
			Push(p1);
			Push(p2);
			Push(p3);
			Call(symId);
		}

		public void RegisterExternalDefault(ExternalDefaultFunction cb)
		{
			var gcHandle = GCHandle.Alloc(cb);
			_externalCallbacks.Add(gcHandle);
			Native.ZkDaedalusVm_registerExternalDefault(Handle, _nativeExternalCallbackDefault,
				GCHandle.ToIntPtr(gcHandle));
		}

		public void RegisterExternal<TR>(string name, ExternalFunc<TR> cb)
		{
			RegisterExternalUnsafe(name, vm => vm.Push(cb()));
		}

		public void RegisterExternal<TR, TP0>(string name, ExternalFunc<TR, TP0> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0));
			});
		}

		public void RegisterExternal<TR, TP0, TP1>(string name, ExternalFunc<TR, TP0, TP1> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0, p1));
			});
		}

		public void RegisterExternal<TR, TP0, TP1, TP2>(string name, ExternalFunc<TR, TP0, TP1, TP2> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0, p1, p2));
			});
		}

		public void RegisterExternal<TR, TP0, TP1, TP2, TP3>(string name, ExternalFunc<TR, TP0, TP1, TP2, TP3> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0, p1, p2, p3));
			});
		}

		public void RegisterExternal<TR, TP0, TP1, TP2, TP3, TP4>(string name,
			ExternalFunc<TR, TP0, TP1, TP2, TP3, TP4> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0, p1, p2, p3, p4));
			});
		}

		public void RegisterExternal<TR, TP0, TP1, TP2, TP3, TP4, TP5>(string name,
			ExternalFunc<TR, TP0, TP1, TP2, TP3, TP4, TP5> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0, p1, p2, p3, p4, p5));
			});
		}

		public void RegisterExternal<TR, TP0, TP1, TP2, TP3, TP4, TP5, TP6>(string name,
			ExternalFunc<TR, TP0, TP1, TP2, TP3, TP4, TP5, TP6> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p6 = vm.Pop<TP6>();
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0, p1, p2, p3, p4, p5, p6));
			});
		}

		public void RegisterExternal<TR, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7>(string name,
			ExternalFunc<TR, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p7 = vm.Pop<TP7>();
				var p6 = vm.Pop<TP6>();
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0, p1, p2, p3, p4, p5, p6, p7));
			});
		}

		public void RegisterExternal<TR, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(string name,
			ExternalFunc<TR, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p8 = vm.Pop<TP8>();
				var p7 = vm.Pop<TP7>();
				var p6 = vm.Pop<TP6>();
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0, p1, p2, p3, p4, p5, p6, p7, p8));
			});
		}

		public void RegisterExternal<TR, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(string name,
			ExternalFunc<TR, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p9 = vm.Pop<TP9>();
				var p8 = vm.Pop<TP8>();
				var p7 = vm.Pop<TP7>();
				var p6 = vm.Pop<TP6>();
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				vm.Push(cb(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
			});
		}

		public void RegisterExternal(string name, ExternalFuncV cb)
		{
			RegisterExternalUnsafe(name, _ => cb());
		}

		public void RegisterExternal<TP0>(string name, ExternalFuncV<TP0> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p0 = vm.Pop<TP0>();
				cb(p0);
			});
		}

		public void RegisterExternal<TP0, TP1>(string name, ExternalFuncV<TP0, TP1> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				cb(p0, p1);
			});
		}

		public void RegisterExternal<TP0, TP1, TP2>(string name, ExternalFuncV<TP0, TP1, TP2> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				cb(p0, p1, p2);
			});
		}

		public void RegisterExternal<TP0, TP1, TP2, TP3>(string name, ExternalFuncV<TP0, TP1, TP2, TP3> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				cb(p0, p1, p2, p3);
			});
		}

		public void RegisterExternal<TP0, TP1, TP2, TP3, TP4>(string name, ExternalFuncV<TP0, TP1, TP2, TP3, TP4> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				cb(p0, p1, p2, p3, p4);
			});
		}

		public void RegisterExternal<TP0, TP1, TP2, TP3, TP4, TP5>(string name,
			ExternalFuncV<TP0, TP1, TP2, TP3, TP4, TP5> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				cb(p0, p1, p2, p3, p4, p5);
			});
		}

		public void RegisterExternal<TP0, TP1, TP2, TP3, TP4, TP5, TP6>(string name,
			ExternalFuncV<TP0, TP1, TP2, TP3, TP4, TP5, TP6> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p6 = vm.Pop<TP6>();
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				cb(p0, p1, p2, p3, p4, p5, p6);
			});
		}

		public void RegisterExternal<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7>(string name,
			ExternalFuncV<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p7 = vm.Pop<TP7>();
				var p6 = vm.Pop<TP6>();
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				cb(p0, p1, p2, p3, p4, p5, p6, p7);
			});
		}

		public void RegisterExternal<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(string name,
			ExternalFuncV<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p8 = vm.Pop<TP8>();
				var p7 = vm.Pop<TP7>();
				var p6 = vm.Pop<TP6>();
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				cb(p0, p1, p2, p3, p4, p5, p6, p7, p8);
			});
		}

		public void RegisterExternal<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(string name,
			ExternalFuncV<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9> cb)
		{
			RegisterExternalUnsafe(name, vm =>
			{
				var p9 = vm.Pop<TP9>();
				var p8 = vm.Pop<TP8>();
				var p7 = vm.Pop<TP7>();
				var p6 = vm.Pop<TP6>();
				var p5 = vm.Pop<TP5>();
				var p4 = vm.Pop<TP4>();
				var p3 = vm.Pop<TP3>();
				var p2 = vm.Pop<TP2>();
				var p1 = vm.Pop<TP1>();
				var p0 = vm.Pop<TP0>();
				cb(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);
			});
		}

		private void Push<T>(T value)
		{
			switch (value)
			{
				case string v:
					Native.ZkDaedalusVm_pushString(Handle, v);
					break;
				case int v:
					Native.ZkDaedalusVm_pushInt(Handle, v);
					break;
				case bool v:
					Native.ZkDaedalusVm_pushInt(Handle, v ? 1 : 0);
					break;
				case float v:
					Native.ZkDaedalusVm_pushFloat(Handle, v);
					break;
				case DaedalusInstance v:
					Native.ZkDaedalusVm_pushInstance(Handle, v.Handle);
					break;
				case null:
					Native.ZkDaedalusVm_pushInstance(Handle, UIntPtr.Zero);
					break;
				default:
					throw new InvalidOperationException("Unsupported type: " + value?.GetType());
			}
		}

		private T Pop<T>()
		{
			if (typeof(T) == typeof(string))
				return (T)(object)(Native.ZkDaedalusVm_popString(Handle).MarshalAsString() ?? string.Empty);
			if (typeof(T) == typeof(int) || typeof(T) == typeof(bool))
				return (T)(object)Native.ZkDaedalusVm_popInt(Handle);
			if (typeof(T) == typeof(float)) return (T)(object)Native.ZkDaedalusVm_popFloat(Handle);
			if (typeof(T) == typeof(DaedalusInstance) || typeof(T).IsSubclassOf(typeof(DaedalusInstance)))
			{
				var ptr = Native.ZkDaedalusVm_popInstance(Handle);
				if (ptr == UIntPtr.Zero)
				{
					return (T)(object) null;
				}
				return (T)(object)DaedalusInstance.FromNative(ptr);
			}
			if (typeof(T) == typeof(void)) return (T)new object();

			throw new InvalidOperationException("Unsupported type: " + typeof(T));
		}

		private void RegisterExternalUnsafe(string name, ExternalFunc cb)
		{
			var sym = GetSymbolByName(name);
			if (sym == null) throw new Exception("Symbol not found");
			RegisterExternalUnsafe(sym, cb);
		}

		private void RegisterExternalUnsafe(DaedalusSymbol sym, ExternalFunc cb)
		{
			var gcHandle = GCHandle.Alloc(cb);
			_externalCallbacks.Add(gcHandle);
			Native.ZkDaedalusVm_registerExternal(Handle, sym.Handle, _nativeExternalCallback,
				GCHandle.ToIntPtr(gcHandle));
		}

		[MonoPInvokeCallback]
		private static void _nativeExternalCallbackHandler(IntPtr ctx, UIntPtr vm)
		{
			var gcHandle = GCHandle.FromIntPtr(ctx);
			var cb = (ExternalFunc)gcHandle.Target;
			cb(new DaedalusVm(vm));
		}

		[MonoPInvokeCallback]
		private static void _nativeExternalCallbackDefaultHandle(IntPtr ctx, UIntPtr vm, UIntPtr sym)
		{
			var gcHandle = GCHandle.FromIntPtr(ctx);
			var cb = (ExternalDefaultFunction)gcHandle.Target;
			cb(new DaedalusVm(vm), new DaedalusSymbol(sym));
		}

		private delegate void ExternalFunc(DaedalusVm vm);
	}
}