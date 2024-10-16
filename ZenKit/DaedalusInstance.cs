using System;
using System.Runtime.InteropServices;
using ZenKit.Daedalus;

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
	}
}