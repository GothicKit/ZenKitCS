using System;
using ZenKit.Daedalus;

namespace ZenKit
{
	public enum DaedalusInstanceType {
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
		Invalid = 20,
	}

	public class DaedalusInstance
	{
		public DaedalusInstance(UIntPtr handle)
		{
			Handle = handle;
		}

		internal UIntPtr Handle { get; }

		public DaedalusInstanceType Type => Native.ZkDaedalusInstance_getType(Handle);
		public uint Index => Native.ZkDaedalusInstance_getIndex(Handle);

		public static DaedalusInstance FromNative(UIntPtr handle)
		{
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
				DaedalusInstanceType.Svm => throw new InvalidOperationException("Svm Instances are currently not supported"),
				_ => new DaedalusInstance(handle)
			};
		}
	}
}
