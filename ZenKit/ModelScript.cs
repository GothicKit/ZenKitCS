using System;
using System.Collections.Generic;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	[Flags]
	public enum AnimationFlags
	{
		None = 0,
		Move = 1,
		Rotate = 2,
		Queue = 4,
		Fly = 8,
		Idle = 16,
		InPlace = 32,
	}

	[Serializable]
	public enum AnimationDirection
	{
		Forward = 0,
		Backward = 1
	}

	[Serializable]
	public enum EventType
	{
		Unknown = 0,
		ItemCreate = 1,
		ItemInsert = 2,
		ItemRemove = 3,
		ItemDestroy = 4,
		ItemPlace = 5,
		ItemExchange = 6,
		SetFightMode = 7,
		MunitionPlace = 8,
		MunitionRemove = 9,
		SoundDraw = 10,
		SoundUndraw = 11,
		MeshSwap = 12,
		TorchDraw = 13,
		TorchInventory = 14,
		TorchDrop = 15,
		HitLimb = 16,
		HitDirection = 17,
		DamageMultiplier = 18,
		ParryFrame = 19,
		OptimalFrame = 20,
		HitEnd = 21,
		ComboWindow = 22
	}

	[Serializable]
	public enum FightMode
	{
		Fist = 0,
		SingleHanded = 1,
		DualHanded = 2,
		Bow = 3,
		Crossbow = 4,
		Magic = 5,
		None = 6,
		Invalid = 0xFF
	}

	public interface IModelScript : ICacheable<IModelScript>
	{
		string SkeletonName { get; }
		bool SkeletonMeshDisabled { get; }
		int MeshCount { get; }
		int DisabledAnimationsCount { get; }
		int AnimationCombineCount { get; }
		int AnimationBlendCount { get; }
		int AnimationAliasCount { get; }
		int ModelTagCount { get; }
		int AnimationCount { get; }
		List<IAnimationCombine> AnimationCombines { get; }
		List<string> Meshes { get; }
		List<string> DisabledAnimations { get; }
		List<IAnimationBlend> AnimationBlends { get; }
		List<IAnimationAlias> AnimationAliases { get; }
		List<string> ModelTags { get; }
		List<IAnimation> Animations { get; }
		string GetDisabledAnimation(int i);
		string GetMesh(int i);
		IAnimationCombine GetAnimationCombine(int i);
		IAnimationBlend GetAnimationBlend(int i);
		IAnimationAlias GetAnimationAlias(int i);
		string GetModelTag(int i);
		IAnimation GetAnimation(int i);
	}

	[Serializable]
	public class CachedModelScript : IModelScript
	{
		public string SkeletonName { get; set; }
		public bool SkeletonMeshDisabled { get; set; }
		public int MeshCount => Meshes.Count;
		public int DisabledAnimationsCount => DisabledAnimations.Count;
		public int AnimationCombineCount => AnimationCombines.Count;
		public int AnimationBlendCount => AnimationBlends.Count;
		public int AnimationAliasCount => AnimationAliases.Count;
		public int ModelTagCount => ModelTags.Count;
		public int AnimationCount => Animations.Count;
		public List<IAnimationCombine> AnimationCombines { get; set; }
		public List<string> Meshes { get; set; }
		public List<string> DisabledAnimations { get; set; }
		public List<IAnimationBlend> AnimationBlends { get; set; }
		public List<IAnimationAlias> AnimationAliases { get; set; }
		public List<string> ModelTags { get; set; }
		public List<IAnimation> Animations { get; set; }

		public string GetDisabledAnimation(int i)
		{
			return DisabledAnimations[i];
		}

		public string GetMesh(int i)
		{
			return Meshes[i];
		}

		public IAnimationCombine GetAnimationCombine(int i)
		{
			return AnimationCombines[i];
		}

		public IAnimationBlend GetAnimationBlend(int i)
		{
			return AnimationBlends[i];
		}

		public IAnimationAlias GetAnimationAlias(int i)
		{
			return AnimationAliases[i];
		}

		public string GetModelTag(int i)
		{
			return ModelTags[i];
		}

		public IAnimation GetAnimation(int i)
		{
			return Animations[i];
		}

		public IModelScript Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}


	public class ModelScript : IModelScript
	{
		private readonly UIntPtr _handle;

		public ModelScript(string path)
		{
			_handle = Native.ZkModelScript_loadPath(path);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model script");
		}

		public ModelScript(Read buf)
		{
			_handle = Native.ZkModelScript_load(buf.Handle);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model script");
		}

		public ModelScript(Vfs vfs, string name)
		{
			_handle = Native.ZkModelScript_loadVfs(vfs.Handle, name);
			if (_handle == UIntPtr.Zero) throw new Exception("Failed to load model script");
		}

		public string SkeletonName => Native.ZkModelScript_getSkeletonName(_handle).MarshalAsString() ??
		                              throw new Exception("Failed to load model script name");

		public bool SkeletonMeshDisabled => Native.ZkModelScript_getSkeletonMeshDisabled(_handle);

		public int MeshCount => (int)Native.ZkModelScript_getMeshCount(_handle);

		public int DisabledAnimationsCount => (int)Native.ZkModelScript_getDisabledAnimationsCount(_handle);

		public int AnimationCombineCount => (int)Native.ZkModelScript_getAnimationCombineCount(_handle);

		public int AnimationBlendCount => (int)Native.ZkModelScript_getAnimationBlendCount(_handle);

		public int AnimationAliasCount => (int)Native.ZkModelScript_getAnimationAliasCount(_handle);

		public int ModelTagCount => (int)Native.ZkModelScript_getModelTagCount(_handle);

		public int AnimationCount => (int)Native.ZkModelScript_getAnimationCount(_handle);

		public List<IAnimationCombine> AnimationCombines
		{
			get
			{
				var arr = new List<IAnimationCombine>();
				var count = AnimationCombineCount;
				for (var i = 0; i < count; ++i) arr.Add(GetAnimationCombine(i));
				return arr;
			}
		}

		public List<string> Meshes
		{
			get
			{
				var arr = new List<string>();
				var count = MeshCount;
				for (var i = 0; i < count; ++i) arr.Add(GetMesh(i));
				return arr;
			}
		}

		public List<string> DisabledAnimations
		{
			get
			{
				var arr = new List<string>();
				var count = DisabledAnimationsCount;
				for (var i = 0; i < count; ++i) arr.Add(GetDisabledAnimation(i));
				return arr;
			}
		}

		public List<IAnimationBlend> AnimationBlends
		{
			get
			{
				var arr = new List<IAnimationBlend>();
				var count = AnimationBlendCount;
				for (var i = 0; i < count; ++i) arr.Add(GetAnimationBlend(i));
				return arr;
			}
		}

		public List<IAnimationAlias> AnimationAliases
		{
			get
			{
				var arr = new List<IAnimationAlias>();
				var count = AnimationAliasCount;
				for (var i = 0; i < count; ++i) arr.Add(GetAnimationAlias(i));
				return arr;
			}
		}

		public List<string> ModelTags
		{
			get
			{
				var arr = new List<string>();
				var count = ModelTagCount;
				for (var i = 0; i < count; ++i) arr.Add(GetModelTag(i));
				return arr;
			}
		}

		public List<IAnimation> Animations
		{
			get
			{
				var arr = new List<IAnimation>();
				var count = AnimationCount;
				for (var i = 0; i < count; ++i) arr.Add(GetAnimation(i));
				return arr;
			}
		}


		public IModelScript Cache()
		{
			return new CachedModelScript
			{
				SkeletonName = SkeletonName,
				SkeletonMeshDisabled = SkeletonMeshDisabled,
				AnimationCombines = AnimationCombines.ConvertAll(obj => obj.Cache()),
				Meshes = Meshes,
				DisabledAnimations = DisabledAnimations,
				AnimationBlends = AnimationBlends.ConvertAll(obj => obj.Cache()),
				AnimationAliases = AnimationAliases.ConvertAll(obj => obj.Cache()),
				ModelTags = ModelTags,
				Animations = Animations.ConvertAll(obj => obj.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public string GetDisabledAnimation(int i)
		{
			return Native.ZkModelScript_getDisabledAnimation(_handle, i).MarshalAsString() ??
			       throw new Exception("Failed to load model script disabled animation");
		}

		public string GetMesh(int i)
		{
			return Native.ZkModelScript_getMesh(_handle, i).MarshalAsString() ??
			       throw new Exception("Failed to load model script mesh");
		}

		public IAnimationCombine GetAnimationCombine(int i)
		{
			return new AnimationCombine(Native.ZkModelScript_getAnimationCombine(_handle, i));
		}

		public IAnimationBlend GetAnimationBlend(int i)
		{
			return new AnimationBlend(Native.ZkModelScript_getAnimationBlend(_handle, i));
		}

		public IAnimationAlias GetAnimationAlias(int i)
		{
			return new AnimationAlias(Native.ZkModelScript_getAnimationAlias(_handle, i));
		}

		public string GetModelTag(int i)
		{
			return Native.ZkModelScript_getModelTag(_handle, i).MarshalAsString() ??
			       throw new Exception("Failed to load model script model tag");
		}

		public IAnimation GetAnimation(int i)
		{
			return new Animation(Native.ZkModelScript_getAnimation(_handle, i));
		}

		~ModelScript()
		{
			Native.ZkModelScript_del(_handle);
		}
	}

	public interface IAnimation : ICacheable<IAnimation>
	{
		string Name { get; }
		int Layer { get; }
		string Next { get; }
		float BlendIn { get; }
		float BlendOut { get; }
		AnimationFlags Flags { get; }
		string Model { get; }
		AnimationDirection Direction { get; }
		int FirstFrame { get; }
		int LastFrame { get; }
		float Fps { get; }
		float Speed { get; }
		float CollisionVolumeScale { get; }
		int EventTagCount { get; }
		int ParticleEffectCount { get; }
		int ParticleEffectStopCount { get; }
		int SoundEffectCount { get; }
		int SoundEffectGroundCount { get; }
		int MorphAnimationCount { get; }
		int CameraTremorCount { get; }
		List<IEventTag> EventTags { get; }
		List<IEventParticleEffect> ParticleEffects { get; }
		List<IEventParticleEffectStop> ParticleEffectsStop { get; }
		List<IEventSoundEffect> SoundEffects { get; }
		List<IEventSoundEffectGround> SoundEffectsGround { get; }
		List<IEventMorphAnimation> MorphAnimations { get; }
		List<IEventCameraTremor> CameraTremors { get; }
		IEventTag GetEventTag(int i);
		IEventParticleEffect GetParticleEffect(int i);
		IEventParticleEffectStop GetParticleEffectStop(int i);
		IEventSoundEffect GetSoundEffect(int i);
		IEventSoundEffectGround GetSoundEffectGround(int i);
		IEventMorphAnimation GetMorphAnimation(int i);
		IEventCameraTremor GetCameraTremor(int i);
	}

	[Serializable]
	public class CachedAnimation : IAnimation
	{
		public string Name { get; set; }
		public int Layer { get; set; }
		public string Next { get; set; }
		public float BlendIn { get; set; }
		public float BlendOut { get; set; }
		public AnimationFlags Flags { get; set; }
		public string Model { get; set; }
		public AnimationDirection Direction { get; set; }
		public int FirstFrame { get; set; }
		public int LastFrame { get; set; }
		public float Fps { get; set; }
		public float Speed { get; set; }
		public float CollisionVolumeScale { get; set; }

		public int EventTagCount => EventTags.Count;

		public int ParticleEffectCount => ParticleEffects.Count;

		public int ParticleEffectStopCount => ParticleEffectsStop.Count;

		public int SoundEffectCount => SoundEffects.Count;

		public int SoundEffectGroundCount => SoundEffectsGround.Count;

		public int MorphAnimationCount => MorphAnimations.Count;

		public int CameraTremorCount => CameraTremors.Count;

		public List<IEventTag> EventTags { get; set; }
		public List<IEventParticleEffect> ParticleEffects { get; set; }
		public List<IEventParticleEffectStop> ParticleEffectsStop { get; set; }
		public List<IEventSoundEffect> SoundEffects { get; set; }
		public List<IEventSoundEffectGround> SoundEffectsGround { get; set; }
		public List<IEventMorphAnimation> MorphAnimations { get; set; }
		public List<IEventCameraTremor> CameraTremors { get; set; }

		public IEventTag GetEventTag(int i)
		{
			return EventTags[i];
		}

		public IEventParticleEffect GetParticleEffect(int i)
		{
			return ParticleEffects[i];
		}

		public IEventParticleEffectStop GetParticleEffectStop(int i)
		{
			return ParticleEffectsStop[i];
		}

		public IEventSoundEffect GetSoundEffect(int i)
		{
			return SoundEffects[i];
		}

		public IEventSoundEffectGround GetSoundEffectGround(int i)
		{
			return SoundEffectsGround[i];
		}

		public IEventMorphAnimation GetMorphAnimation(int i)
		{
			return MorphAnimations[i];
		}

		public IEventCameraTremor GetCameraTremor(int i)
		{
			return CameraTremors[i];
		}

		public IAnimation Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}


	public class Animation : IAnimation
	{
		private readonly UIntPtr _handle;

		public Animation(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkAnimation_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation name");

		public int Layer => (int)Native.ZkAnimation_getLayer(_handle);

		public string Next => Native.ZkAnimation_getNext(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation next");

		public float BlendIn => Native.ZkAnimation_getBlendIn(_handle);

		public float BlendOut => Native.ZkAnimation_getBlendOut(_handle);

		public AnimationFlags Flags => Native.ZkAnimation_getFlags(_handle);

		public string Model => Native.ZkAnimation_getModel(_handle).MarshalAsString() ??
		                       throw new Exception("Failed to load animation model");

		public AnimationDirection Direction => Native.ZkAnimation_getDirection(_handle);

		public int FirstFrame => Native.ZkAnimation_getFirstFrame(_handle);

		public int LastFrame => Native.ZkAnimation_getLastFrame(_handle);

		public float Fps => Native.ZkAnimation_getFps(_handle);

		public float Speed => Native.ZkAnimation_getSpeed(_handle);

		public float CollisionVolumeScale => Native.ZkAnimation_getCollisionVolumeScale(_handle);

		public int EventTagCount => (int)Native.ZkAnimation_getEventTagCount(_handle);

		public int ParticleEffectCount => (int)Native.ZkAnimation_getParticleEffectCount(_handle);

		public int ParticleEffectStopCount => (int)Native.ZkAnimation_getParticleEffectStopCount(_handle);

		public int SoundEffectCount => (int)Native.ZkAnimation_getSoundEffectCount(_handle);

		public int SoundEffectGroundCount => (int)Native.ZkAnimation_getSoundEffectGroundCount(_handle);

		public int MorphAnimationCount => (int)Native.ZkAnimation_getMorphAnimationCount(_handle);

		public int CameraTremorCount => (int)Native.ZkAnimation_getCameraTremorCount(_handle);

		public List<IEventTag> EventTags
		{
			get
			{
				var arr = new List<IEventTag>();
				var count = EventTagCount;
				for (var i = 0; i < count; ++i) arr.Add(GetEventTag(i));
				return arr;
			}
		}

		public List<IEventParticleEffect> ParticleEffects
		{
			get
			{
				var arr = new List<IEventParticleEffect>();
				var count = ParticleEffectCount;
				for (var i = 0; i < count; ++i) arr.Add(GetParticleEffect(i));
				return arr;
			}
		}

		public List<IEventParticleEffectStop> ParticleEffectsStop
		{
			get
			{
				var arr = new List<IEventParticleEffectStop>();
				var count = ParticleEffectStopCount;
				for (var i = 0; i < count; ++i) arr.Add(GetParticleEffectStop(i));
				return arr;
			}
		}

		public List<IEventSoundEffect> SoundEffects
		{
			get
			{
				var arr = new List<IEventSoundEffect>();
				var count = SoundEffectCount;
				for (var i = 0; i < count; ++i) arr.Add(GetSoundEffect(i));
				return arr;
			}
		}

		public List<IEventSoundEffectGround> SoundEffectsGround
		{
			get
			{
				var arr = new List<IEventSoundEffectGround>();
				var count = SoundEffectGroundCount;
				for (var i = 0; i < count; ++i) arr.Add(GetSoundEffectGround(i));
				return arr;
			}
		}

		public List<IEventMorphAnimation> MorphAnimations
		{
			get
			{
				var arr = new List<IEventMorphAnimation>();
				var count = MorphAnimationCount;
				for (var i = 0; i < count; ++i) arr.Add(GetMorphAnimation(i));
				return arr;
			}
		}

		public List<IEventCameraTremor> CameraTremors
		{
			get
			{
				var arr = new List<IEventCameraTremor>();
				var count = CameraTremorCount;
				for (var i = 0; i < count; ++i) arr.Add(GetCameraTremor(i));
				return arr;
			}
		}

		public IAnimation Cache()
		{
			return new CachedAnimation
			{
				Name = Name,
				Layer = Layer,
				Next = Next,
				BlendIn = BlendIn,
				BlendOut = BlendOut,
				Flags = Flags,
				Model = Model,
				Direction = Direction,
				FirstFrame = FirstFrame,
				LastFrame = LastFrame,
				Fps = Fps,
				Speed = Speed,
				CollisionVolumeScale = CollisionVolumeScale,
				EventTags = EventTags.ConvertAll(evt => evt.Cache()),
				ParticleEffects = ParticleEffects.ConvertAll(evt => evt.Cache()),
				ParticleEffectsStop = ParticleEffectsStop.ConvertAll(evt => evt.Cache()),
				SoundEffects = SoundEffects.ConvertAll(evt => evt.Cache()),
				SoundEffectsGround = SoundEffectsGround.ConvertAll(evt => evt.Cache()),
				MorphAnimations = MorphAnimations.ConvertAll(evt => evt.Cache()),
				CameraTremors = CameraTremors.ConvertAll(evt => evt.Cache())
			};
		}

		public bool IsCached()
		{
			return false;
		}

		public IEventTag GetEventTag(int i)
		{
			return new EventTag(Native.ZkAnimation_getEventTag(_handle, (ulong)i));
		}

		public IEventParticleEffect GetParticleEffect(int i)
		{
			return new EventParticleEffect(Native.ZkAnimation_getParticleEffect(_handle, (ulong)i));
		}

		public IEventParticleEffectStop GetParticleEffectStop(int i)
		{
			return new EventParticleEffectStop(Native.ZkAnimation_getParticleEffectStop(_handle, (ulong)i));
		}

		public IEventSoundEffect GetSoundEffect(int i)
		{
			return new EventSoundEffect(Native.ZkAnimation_getSoundEffect(_handle, (ulong)i));
		}

		public IEventSoundEffectGround GetSoundEffectGround(int i)
		{
			return new EventSoundEffectGround(Native.ZkAnimation_getSoundEffectGround(_handle, (ulong)i));
		}

		public IEventMorphAnimation GetMorphAnimation(int i)
		{
			return new EventMorphAnimation(Native.ZkAnimation_getMorphAnimation(_handle, (ulong)i));
		}

		public IEventCameraTremor GetCameraTremor(int i)
		{
			return new EventCameraTremor(Native.ZkAnimation_getCameraTremor(_handle, (ulong)i));
		}
	}

	public interface IEventMorphAnimation : ICacheable<IEventMorphAnimation>
	{
		int Frame { get; }
		string Animation { get; }
		string Node { get; }
	}

	[Serializable]
	public class CachedEventMorphAnimation : IEventMorphAnimation
	{
		public int Frame { get; set; }
		public string Animation { get; set; }
		public string Node { get; set; }

		public IEventMorphAnimation Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class EventMorphAnimation : IEventMorphAnimation
	{
		private readonly UIntPtr _handle;

		public EventMorphAnimation(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Frame => Native.ZkMorphAnimation_getFrame(_handle);

		public string Animation => Native.ZkMorphAnimation_getAnimation(_handle).MarshalAsString() ??
		                           throw new Exception("Failed to load event morph animation name");

		public string Node => Native.ZkMorphAnimation_getNode(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load event morph animation node");

		public IEventMorphAnimation Cache()
		{
			return new CachedEventMorphAnimation
			{
				Frame = Frame,
				Animation = Animation,
				Node = Node
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IEventCameraTremor : ICacheable<IEventCameraTremor>
	{
		int Frame { get; }
		int Field1 { get; }
		int Field2 { get; }
		int Field3 { get; }
		int Field4 { get; }
	}

	[Serializable]
	public class CachedEventCameraTremor : IEventCameraTremor
	{
		public int Frame { get; set; }
		public int Field1 { get; set; }
		public int Field2 { get; set; }
		public int Field3 { get; set; }
		public int Field4 { get; set; }

		public IEventCameraTremor Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class EventCameraTremor : IEventCameraTremor
	{
		private readonly UIntPtr _handle;

		public EventCameraTremor(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Frame => Native.ZkEventCameraTremor_getFrame(_handle);

		public int Field1 => Native.ZkEventCameraTremor_getField1(_handle);

		public int Field2 => Native.ZkEventCameraTremor_getField2(_handle);

		public int Field3 => Native.ZkEventCameraTremor_getField3(_handle);

		public int Field4 => Native.ZkEventCameraTremor_getField4(_handle);

		public IEventCameraTremor Cache()
		{
			return new CachedEventCameraTremor
			{
				Frame = Frame,
				Field1 = Field1,
				Field2 = Field2,
				Field3 = Field3,
				Field4 = Field4
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IEventSoundEffectGround : ICacheable<IEventSoundEffectGround>
	{
		int Frame { get; }
		string Name { get; }
		float Range { get; }
		bool EmptySlot { get; }
	}

	[Serializable]
	public class CachedEventSoundEffectGround : IEventSoundEffectGround
	{
		public int Frame { get; set; }
		public string Name { get; set; }
		public float Range { get; set; }
		public bool EmptySlot { get; set; }

		public IEventSoundEffectGround Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class EventSoundEffectGround : IEventSoundEffectGround
	{
		private readonly UIntPtr _handle;

		public EventSoundEffectGround(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Frame => Native.ZkEventSoundEffectGround_getFrame(_handle);

		public string Name => Native.ZkEventSoundEffectGround_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load event sound effect ground name");

		public float Range => Native.ZkEventSoundEffectGround_getRange(_handle);

		public bool EmptySlot => Native.ZkEventSoundEffectGround_getEmptySlot(_handle);

		public IEventSoundEffectGround Cache()
		{
			return new CachedEventSoundEffectGround
			{
				Frame = Frame,
				Name = Name,
				Range = Range,
				EmptySlot = EmptySlot
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IEventSoundEffect : ICacheable<IEventSoundEffect>
	{
		int Frame { get; }
		string Name { get; }
		float Range { get; }
		bool EmptySlot { get; }
	}


	[Serializable]
	public class CachedEventSoundEffect : IEventSoundEffect
	{
		public int Frame { get; set; }
		public string Name { get; set; }
		public float Range { get; set; }
		public bool EmptySlot { get; set; }

		public IEventSoundEffect Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class EventSoundEffect : IEventSoundEffect
	{
		private readonly UIntPtr _handle;

		public EventSoundEffect(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Frame => Native.ZkEventSoundEffect_getFrame(_handle);

		public string Name => Native.ZkEventSoundEffect_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load event sound effect name");

		public float Range => Native.ZkEventSoundEffect_getRange(_handle);

		public bool EmptySlot => Native.ZkEventSoundEffect_getEmptySlot(_handle);

		public IEventSoundEffect Cache()
		{
			return new CachedEventSoundEffect
			{
				Frame = Frame,
				Name = Name,
				Range = Range,
				EmptySlot = EmptySlot
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IEventParticleEffectStop : ICacheable<IEventParticleEffectStop>
	{
		int Frame { get; }
		int Index { get; }
	}

	[Serializable]
	public class CachedEventParticleEffectStop : IEventParticleEffectStop
	{
		public int Frame { get; set; }
		public int Index { get; set; }

		public IEventParticleEffectStop Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class EventParticleEffectStop : IEventParticleEffectStop
	{
		private readonly UIntPtr _handle;

		public EventParticleEffectStop(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Frame => Native.ZkEventParticleEffectStop_getFrame(_handle);

		public int Index => Native.ZkEventParticleEffectStop_getIndex(_handle);

		public IEventParticleEffectStop Cache()
		{
			return new CachedEventParticleEffectStop
			{
				Frame = Frame,
				Index = Index
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IEventParticleEffect : ICacheable<IEventParticleEffect>
	{
		int Frame { get; }
		int Index { get; }
		string Name { get; }
		string Position { get; }
		bool Attached { get; }
	}

	[Serializable]
	public class CachedEventParticleEffect : IEventParticleEffect
	{
		public int Frame { get; set; }
		public int Index { get; set; }
		public string Name { get; set; }
		public string Position { get; set; }
		public bool Attached { get; set; }

		public IEventParticleEffect Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class EventParticleEffect : IEventParticleEffect
	{
		private readonly UIntPtr _handle;

		public EventParticleEffect(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Frame => Native.ZkEventParticleEffect_getFrame(_handle);

		public int Index => Native.ZkEventParticleEffect_getIndex(_handle);

		public string Name => Native.ZkEventParticleEffect_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load event particle effect name");

		public string Position => Native.ZkEventParticleEffect_getPosition(_handle).MarshalAsString() ??
		                          throw new Exception("Failed to load event particle effect position");

		public bool Attached => Native.ZkEventParticleEffect_getIsAttached(_handle);

		public IEventParticleEffect Cache()
		{
			return new CachedEventParticleEffect
			{
				Frame = Frame,
				Index = Index,
				Name = Name,
				Position = Position,
				Attached = Attached
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IEventTag : ICacheable<IEventTag>
	{
		int Frame { get; }
		EventType Type { get; }
		Tuple<string, string> Slots { get; }
		string Item { get; }
		List<int> Frames { get; }
		FightMode FightMode { get; }
		bool Attached { get; }
	}

	[Serializable]
	public class CachedEventTag : IEventTag
	{
		public int Frame { get; set; }
		public EventType Type { get; set; }
		public Tuple<string, string> Slots { get; set; }
		public string Item { get; set; }
		public List<int> Frames { get; set; }
		public FightMode FightMode { get; set; }
		public bool Attached { get; set; }

		public IEventTag Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class EventTag : IEventTag
	{
		private readonly UIntPtr _handle;

		public EventTag(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Frame => Native.ZkEventTag_getFrame(_handle);

		public EventType Type => Native.ZkEventTag_getType(_handle);

		public Tuple<string, string> Slots =>
			new Tuple<string, string>(
				Native.ZkEventTag_getSlot(_handle, 0).MarshalAsString() ??
				throw new Exception("Failed to load event tag slot"),
				Native.ZkEventTag_getSlot(_handle, 1).MarshalAsString() ??
				throw new Exception("Failed to load event tag slot")
			);

		public string Item => Native.ZkEventTag_getItem(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load event tag item");

		public List<int> Frames => Native.ZkEventTag_getFrames(_handle, out var count).MarshalAsList<int>(count);

		public FightMode FightMode => Native.ZkEventTag_getFightMode(_handle);

		public bool Attached => Native.ZkEventTag_getIsAttached(_handle);

		public IEventTag Cache()
		{
			return new CachedEventTag
			{
				Frame = Frame,
				Type = Type,
				Slots = Slots,
				Item = Item,
				Frames = Frames,
				FightMode = FightMode,
				Attached = Attached
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IAnimationAlias : ICacheable<IAnimationAlias>
	{
		string Name { get; }
		int Layer { get; }
		string Next { get; }
		float BlendIn { get; }
		float BlendOut { get; }
		AnimationFlags Flags { get; }
		string Alias { get; }
		AnimationDirection Direction { get; }
	}

	[Serializable]
	public class CachedAnimationAlias : IAnimationAlias
	{
		public string Name { get; set; }
		public int Layer { get; set; }
		public string Next { get; set; }
		public float BlendIn { get; set; }
		public float BlendOut { get; set; }
		public AnimationFlags Flags { get; set; }
		public string Alias { get; set; }
		public AnimationDirection Direction { get; set; }

		public IAnimationAlias Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class AnimationAlias : IAnimationAlias
	{
		private readonly UIntPtr _handle;

		public AnimationAlias(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkAnimationAlias_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation alias name");

		public int Layer => (int)Native.ZkAnimationAlias_getLayer(_handle);

		public string Next => Native.ZkAnimationAlias_getNext(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation alias next");

		public float BlendIn => Native.ZkAnimationAlias_getBlendIn(_handle);

		public float BlendOut => Native.ZkAnimationAlias_getBlendOut(_handle);

		public AnimationFlags Flags => Native.ZkAnimationAlias_getFlags(_handle);

		public string Alias => Native.ZkAnimationAlias_getAlias(_handle).MarshalAsString() ??
		                       throw new Exception("Failed to load animation alias alias");

		public AnimationDirection Direction => Native.ZkAnimationAlias_getDirection(_handle);

		public IAnimationAlias Cache()
		{
			return new CachedAnimationAlias
			{
				Name = Name,
				Layer = Layer,
				Next = Next,
				BlendIn = BlendIn,
				BlendOut = BlendOut,
				Flags = Flags,
				Alias = Alias,
				Direction = Direction
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IAnimationBlend : ICacheable<IAnimationBlend>
	{
		string Name { get; }
		string Next { get; }
		float BlendIn { get; }
		float BlendOut { get; }
	}

	[Serializable]
	public class CachedAnimationBlend : IAnimationBlend
	{
		public string Name { get; set; }
		public string Next { get; set; }
		public float BlendIn { get; set; }
		public float BlendOut { get; set; }

		public IAnimationBlend Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}

	public class AnimationBlend : IAnimationBlend
	{
		private readonly UIntPtr _handle;

		public AnimationBlend(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkAnimationBlend_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation blend name");

		public string Next => Native.ZkAnimationBlend_getNext(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation blend next");

		public float BlendIn => Native.ZkAnimationBlend_getBlendIn(_handle);

		public float BlendOut => Native.ZkAnimationBlend_getBlendOut(_handle);

		public IAnimationBlend Cache()
		{
			return new CachedAnimationBlend
			{
				Name = Name,
				Next = Next,
				BlendIn = BlendIn,
				BlendOut = BlendOut
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}

	public interface IAnimationCombine : ICacheable<IAnimationCombine>
	{
		string Name { get; }
		int Layer { get; }
		string Next { get; }
		float BlendIn { get; }
		float BlendOut { get; }
		AnimationFlags Flags { get; }
		string Model { get; }
		int LastFrame { get; }
	}

	[Serializable]
	public class CachedAnimationCombine : IAnimationCombine
	{
		public string Name { get; set; }
		public int Layer { get; set; }
		public string Next { get; set; }
		public float BlendIn { get; set; }
		public float BlendOut { get; set; }
		public AnimationFlags Flags { get; set; }
		public string Model { get; set; }
		public int LastFrame { get; set; }

		public IAnimationCombine Cache()
		{
			return this;
		}

		public bool IsCached()
		{
			return true;
		}
	}


	public class AnimationCombine : IAnimationCombine
	{
		private readonly UIntPtr _handle;

		public AnimationCombine(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkAnimationCombine_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation combine name");

		public int Layer => (int)Native.ZkAnimationCombine_getLayer(_handle);

		public string Next => Native.ZkAnimationCombine_getNext(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation combine next");

		public float BlendIn => Native.ZkAnimationCombine_getBlendIn(_handle);

		public float BlendOut => Native.ZkAnimationCombine_getBlendOut(_handle);

		public AnimationFlags Flags => Native.ZkAnimationCombine_getFlags(_handle);

		public string Model => Native.ZkAnimationCombine_getModel(_handle).MarshalAsString() ??
		                       throw new Exception("Failed to load animation combine model");

		public int LastFrame => Native.ZkAnimationCombine_getLastFrame(_handle);

		public IAnimationCombine Cache()
		{
			return new CachedAnimationCombine
			{
				Name = Name,
				Layer = Layer,
				Next = Next,
				BlendIn = BlendIn,
				BlendOut = BlendOut,
				Flags = Flags,
				Model = Model,
				LastFrame = LastFrame
			};
		}

		public bool IsCached()
		{
			return false;
		}
	}
}