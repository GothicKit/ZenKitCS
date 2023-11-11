using System;
using System.Collections.Generic;
using ZenKit.Util;

namespace ZenKit
{
	[Serializable]
	public class AnimationFlag
	{
		public static int None = 0;
		public static int Move = 1;
		public static int Rotate = 2;
		public static int Queue = 4;
		public static int Fly = 8;
		public static int Idle = 16;
		public static int InPlace = 32;
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

	namespace Materialized
	{
		[Serializable]
		public struct ModelScript
		{
			public string SkeletonName;
			public bool SkeletonMeshDisabled;
			public List<AnimationCombine> AnimationCombines;
			public List<string> Meshes;
			public List<string> DisabledAnimations;
			public List<AnimationBlend> AnimationBlends;
			public List<AnimationAlias> AnimationAliases;
			public List<string> ModelTags;
			public List<Animation> Animations;
		}

		[Serializable]
		public struct Animation
		{
			public string Name;
			public uint Layer;
			public string Next;
			public float BlendIn;
			public float BlendOut;
			public int Flags;
			public string Model;
			public AnimationDirection Direction;
			public int FirstFrame;
			public int LastFrame;
			public float Fps;
			public float Speed;
			public float CollisionVolumeScale;
			public List<EventTag> EventTags;
			public List<EventParticleEffect> ParticleEffects;
			public List<EventParticleEffectStop> ParticleEffectsStop;
			public List<EventSoundEffect> SoundEffects;
			public List<EventSoundEffectGround> SoundEffectsGround;
			public List<EventMorphAnimation> MorphAnimations;
			public List<EventCameraTremor> CameraTremors;
		}

		[Serializable]
		public struct EventMorphAnimation
		{
			public int Frame;
			public string Animation;
			public string Node;
		}

		[Serializable]
		public struct EventCameraTremor
		{
			public int Frame;
			public int Field1;
			public int Field2;
			public int Field3;
			public int Field4;
		}

		[Serializable]
		public struct EventSoundEffectGround
		{
			public int Frame;
			public string Name;
			public float Range;
			public bool EmptySlot;
		}

		[Serializable]
		public struct EventSoundEffect
		{
			public int Frame;
			public string Name;
			public float Range;
			public bool EmptySlot;
		}

		[Serializable]
		public struct EventParticleEffectStop
		{
			public int Frame;
			public int Index;
		}

		[Serializable]
		public struct EventParticleEffect
		{
			public int Frame;
			public int Index;
			public string Name;
			public string Position;
			public bool Attached;
		}

		[Serializable]
		public struct EventTag
		{
			public int Frame;
			public EventType Type;
			public Tuple<string, string> Slots;
			public string Item;
			public uint[] Frames;
			public FightMode FightMode;
			public bool Attached;
		}

		[Serializable]
		public struct AnimationAlias
		{
			public string Name;
			public uint Layer;
			public string Next;
			public float BlendIn;
			public float BlendOut;
			public uint Flags;
			public string Alias;
			public AnimationDirection Direction;
		}

		[Serializable]
		public struct AnimationBlend
		{
			public string Name;
			public string Next;
			public float BlendIn;
			public float BlendOut;
		}

		[Serializable]
		public struct AnimationCombine
		{
			public string Name;
			public uint Layer;
			public string Next;
			public float BlendIn;
			public float BlendOut;
			public uint Flags;
			public string Model;
			public int LastFrame;
		}
	}

	public class ModelScript : IMaterializing<Materialized.ModelScript>
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

		public ulong MeshCount => Native.ZkModelScript_getMeshCount(_handle);

		public ulong DisabledAnimationsCount => Native.ZkModelScript_getDisabledAnimationsCount(_handle);

		public ulong AnimationCombineCount => Native.ZkModelScript_getAnimationCombineCount(_handle);

		public ulong AnimationBlendCount => Native.ZkModelScript_getAnimationBlendCount(_handle);

		public ulong AnimationAliasCount => Native.ZkModelScript_getAnimationAliasCount(_handle);

		public ulong ModelTagCount => Native.ZkModelScript_getModelTagCount(_handle);

		public ulong AnimationCount => Native.ZkModelScript_getAnimationCount(_handle);

		public List<AnimationCombine> AnimationCombines
		{
			get
			{
				var arr = new List<AnimationCombine>();

				Native.ZkModelScript_enumerateAnimationCombines(_handle, (_, v) =>
				{
					arr.Add(new AnimationCombine(v));
					return false;
				}, UIntPtr.Zero);

				return arr;
			}
		}

		public List<string> Meshes
		{
			get
			{
				var arr = new List<string>();

				Native.ZkModelScript_enumerateMeshes(_handle, (_, v) =>
				{
					arr.Add(v.MarshalAsString() ?? throw new Exception("Failed to load model script mesh"));
					return false;
				}, UIntPtr.Zero);

				return arr;
			}
		}

		public List<string> DisabledAnimations
		{
			get
			{
				var arr = new List<string>();

				Native.ZkModelScript_enumerateDisabledAnimations(_handle, (_, v) =>
				{
					arr.Add(
						v.MarshalAsString() ?? throw new Exception("Failed to load model script disabled animation"));
					return false;
				}, UIntPtr.Zero);

				return arr;
			}
		}

		public List<AnimationBlend> AnimationBlends
		{
			get
			{
				var arr = new List<AnimationBlend>();

				Native.ZkModelScript_enumerateAnimationBlends(_handle, (_, v) =>
				{
					arr.Add(new AnimationBlend(v));
					return false;
				}, UIntPtr.Zero);

				return arr;
			}
		}

		public List<AnimationAlias> AnimationAliases
		{
			get
			{
				var arr = new List<AnimationAlias>();

				Native.ZkModelScript_enumerateAnimationAliases(_handle, (_, v) =>
				{
					arr.Add(new AnimationAlias(v));
					return false;
				}, UIntPtr.Zero);

				return arr;
			}
		}

		public List<string> ModelTags
		{
			get
			{
				var arr = new List<string>();

				Native.ZkModelScript_enumerateModelTags(_handle, (_, v) =>
				{
					arr.Add(v.MarshalAsString() ?? throw new Exception("Failed to load model script model tag"));
					return false;
				}, UIntPtr.Zero);

				return arr;
			}
		}

		public List<Animation> Animations
		{
			get
			{
				var arr = new List<Animation>();

				Native.ZkModelScript_enumerateAnimations(_handle, (_, v) =>
				{
					arr.Add(new Animation(v));
					return false;
				}, UIntPtr.Zero);

				return arr;
			}
		}


		public Materialized.ModelScript Materialize()
		{
			return new Materialized.ModelScript
			{
				SkeletonName = SkeletonName,
				SkeletonMeshDisabled = SkeletonMeshDisabled,
				AnimationCombines = AnimationCombines.ConvertAll(obj => obj.Materialize()),
				Meshes = Meshes,
				DisabledAnimations = DisabledAnimations,
				AnimationBlends = AnimationBlends.ConvertAll(obj => obj.Materialize()),
				AnimationAliases = AnimationAliases.ConvertAll(obj => obj.Materialize()),
				ModelTags = ModelTags,
				Animations = Animations.ConvertAll(obj => obj.Materialize())
			};
		}

		~ModelScript()
		{
			Native.ZkModelScript_del(_handle);
		}

		public string GetDisabledAnimation(long i)
		{
			return Native.ZkModelScript_getDisabledAnimation(_handle, i).MarshalAsString() ??
			       throw new Exception("Failed to load model script disabled animation");
		}

		public string GetMesh(long i)
		{
			return Native.ZkModelScript_getMesh(_handle, i).MarshalAsString() ??
			       throw new Exception("Failed to load model script mesh");
		}

		public AnimationCombine GetAnimationCombine(long i)
		{
			return new AnimationCombine(Native.ZkModelScript_getAnimationCombine(_handle, i));
		}

		public AnimationBlend GetAnimationBlend(long i)
		{
			return new AnimationBlend(Native.ZkModelScript_getAnimationBlend(_handle, i));
		}

		public AnimationAlias GetAnimationAlias(long i)
		{
			return new AnimationAlias(Native.ZkModelScript_getAnimationAlias(_handle, i));
		}

		public string GetModelTag(long i)
		{
			return Native.ZkModelScript_getModelTag(_handle, i).MarshalAsString() ??
			       throw new Exception("Failed to load model script model tag");
		}

		public Animation GetAnimation(long i)
		{
			return new Animation(Native.ZkModelScript_getAnimation(_handle, i));
		}
	}

	public class Animation : IMaterializing<Materialized.Animation>
	{
		private readonly UIntPtr _handle;

		public Animation(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkAnimation_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation name");

		public uint Layer => Native.ZkAnimation_getLayer(_handle);

		public string Next => Native.ZkAnimation_getNext(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation next");

		public float BlendIn => Native.ZkAnimation_getBlendIn(_handle);

		public float BlendOut => Native.ZkAnimation_getBlendOut(_handle);

		public int Flags => Native.ZkAnimation_getFlags(_handle);

		public string Model => Native.ZkAnimation_getModel(_handle).MarshalAsString() ??
		                       throw new Exception("Failed to load animation model");

		public AnimationDirection Direction => Native.ZkAnimation_getDirection(_handle);

		public int FirstFrame => Native.ZkAnimation_getFirstFrame(_handle);

		public int LastFrame => Native.ZkAnimation_getLastFrame(_handle);

		public float Fps => Native.ZkAnimation_getFps(_handle);

		public float Speed => Native.ZkAnimation_getSpeed(_handle);

		public float CollisionVolumeScale => Native.ZkAnimation_getCollisionVolumeScale(_handle);

		public ulong EventTagCount => Native.ZkAnimation_getEventTagCount(_handle);

		public ulong ParticleEffectCount => Native.ZkAnimation_getParticleEffectCount(_handle);

		public ulong ParticleEffectStopCount => Native.ZkAnimation_getParticleEffectStopCount(_handle);

		public ulong SoundEffectCount => Native.ZkAnimation_getSoundEffectCount(_handle);

		public ulong SoundEffectGroundCount => Native.ZkAnimation_getSoundEffectGroundCount(_handle);

		public ulong MorphAnimationCount => Native.ZkAnimation_getMorphAnimationCount(_handle);

		public ulong CameraTremorCount => Native.ZkAnimation_getCameraTremorCount(_handle);

		public List<EventTag> EventTags
		{
			get
			{
				var arr = new List<EventTag>();

				Native.ZkAnimation_enumerateEventTags(_handle, (_, evt) =>
					{
						arr.Add(new EventTag(evt));
						return false;
					},
					UIntPtr.Zero);

				return arr;
			}
		}

		public List<EventParticleEffect> ParticleEffects
		{
			get
			{
				var arr = new List<EventParticleEffect>();

				Native.ZkAnimation_enumerateParticleEffects(_handle, (_, evt) =>
					{
						arr.Add(new EventParticleEffect(evt));
						return false;
					},
					UIntPtr.Zero);

				return arr;
			}
		}

		public List<EventParticleEffectStop> ParticleEffectsStop
		{
			get
			{
				var arr = new List<EventParticleEffectStop>();

				Native.ZkAnimation_enumerateParticleEffectStops(_handle, (_, evt) =>
					{
						arr.Add(new EventParticleEffectStop(evt));
						return false;
					},
					UIntPtr.Zero);

				return arr;
			}
		}

		public List<EventSoundEffect> SoundEffects
		{
			get
			{
				var arr = new List<EventSoundEffect>();

				Native.ZkAnimation_enumerateSoundEffects(_handle, (_, evt) =>
					{
						arr.Add(new EventSoundEffect(evt));
						return false;
					},
					UIntPtr.Zero);

				return arr;
			}
		}

		public List<EventSoundEffectGround> SoundEffectsGround
		{
			get
			{
				var arr = new List<EventSoundEffectGround>();

				Native.ZkAnimation_enumerateSoundEffectGrounds(_handle, (_, evt) =>
					{
						arr.Add(new EventSoundEffectGround(evt));
						return false;
					},
					UIntPtr.Zero);

				return arr;
			}
		}

		public List<EventMorphAnimation> MorphAnimations
		{
			get
			{
				var arr = new List<EventMorphAnimation>();

				Native.ZkAnimation_enumerateMorphAnimations(_handle, (_, evt) =>
					{
						arr.Add(new EventMorphAnimation(evt));
						return false;
					},
					UIntPtr.Zero);

				return arr;
			}
		}

		public List<EventCameraTremor> CameraTremors
		{
			get
			{
				var arr = new List<EventCameraTremor>();

				Native.ZkAnimation_enumerateCameraTremors(_handle, (_, evt) =>
					{
						arr.Add(new EventCameraTremor(evt));
						return false;
					},
					UIntPtr.Zero);

				return arr;
			}
		}

		public Materialized.Animation Materialize()
		{
			return new Materialized.Animation
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
				EventTags = EventTags.ConvertAll(evt => evt.Materialize()),
				ParticleEffects = ParticleEffects.ConvertAll(evt => evt.Materialize()),
				ParticleEffectsStop = ParticleEffectsStop.ConvertAll(evt => evt.Materialize()),
				SoundEffects = SoundEffects.ConvertAll(evt => evt.Materialize()),
				SoundEffectsGround = SoundEffectsGround.ConvertAll(evt => evt.Materialize()),
				MorphAnimations = MorphAnimations.ConvertAll(evt => evt.Materialize()),
				CameraTremors = CameraTremors.ConvertAll(evt => evt.Materialize())
			};
		}


		public EventTag GetEventTag(ulong i)
		{
			return new EventTag(Native.ZkAnimation_getEventTag(_handle, i));
		}

		public EventParticleEffect GetParticleEffect(ulong i)
		{
			return new EventParticleEffect(Native.ZkAnimation_getParticleEffect(_handle, i));
		}

		public EventParticleEffectStop GetParticleEffectStop(ulong i)
		{
			return new EventParticleEffectStop(Native.ZkAnimation_getParticleEffectStop(_handle, i));
		}

		public EventSoundEffect GetSoundEffect(ulong i)
		{
			return new EventSoundEffect(Native.ZkAnimation_getSoundEffect(_handle, i));
		}

		public EventSoundEffectGround GetSoundEffectGround(ulong i)
		{
			return new EventSoundEffectGround(Native.ZkAnimation_getSoundEffectGround(_handle, i));
		}

		public EventMorphAnimation GetMorphAnimation(ulong i)
		{
			return new EventMorphAnimation(Native.ZkAnimation_getMorphAnimation(_handle, i));
		}

		public EventCameraTremor GetCameraTremor(ulong i)
		{
			return new EventCameraTremor(Native.ZkAnimation_getCameraTremor(_handle, i));
		}
	}

	public class EventMorphAnimation : IMaterializing<Materialized.EventMorphAnimation>
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

		public Materialized.EventMorphAnimation Materialize()
		{
			return new Materialized.EventMorphAnimation
			{
				Frame = Frame,
				Animation = Animation,
				Node = Node
			};
		}
	}

	public class EventCameraTremor : IMaterializing<Materialized.EventCameraTremor>
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

		public Materialized.EventCameraTremor Materialize()
		{
			return new Materialized.EventCameraTremor
			{
				Frame = Frame,
				Field1 = Field1,
				Field2 = Field2,
				Field3 = Field3,
				Field4 = Field4
			};
		}
	}

	public class EventSoundEffectGround : IMaterializing<Materialized.EventSoundEffectGround>
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

		public Materialized.EventSoundEffectGround Materialize()
		{
			return new Materialized.EventSoundEffectGround
			{
				Frame = Frame,
				Name = Name,
				Range = Range,
				EmptySlot = EmptySlot
			};
		}
	}

	public class EventSoundEffect : IMaterializing<Materialized.EventSoundEffect>
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

		public Materialized.EventSoundEffect Materialize()
		{
			return new Materialized.EventSoundEffect
			{
				Frame = Frame,
				Name = Name,
				Range = Range,
				EmptySlot = EmptySlot
			};
		}
	}

	public class EventParticleEffectStop : IMaterializing<Materialized.EventParticleEffectStop>
	{
		private readonly UIntPtr _handle;

		public EventParticleEffectStop(UIntPtr handle)
		{
			_handle = handle;
		}

		public int Frame => Native.ZkEventParticleEffectStop_getFrame(_handle);

		public int Index => Native.ZkEventParticleEffectStop_getIndex(_handle);

		public Materialized.EventParticleEffectStop Materialize()
		{
			return new Materialized.EventParticleEffectStop
			{
				Frame = Frame,
				Index = Index
			};
		}
	}

	public class EventParticleEffect : IMaterializing<Materialized.EventParticleEffect>
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

		public Materialized.EventParticleEffect Materialize()
		{
			return new Materialized.EventParticleEffect
			{
				Frame = Frame,
				Index = Index,
				Name = Name,
				Position = Position,
				Attached = Attached
			};
		}
	}

	public class EventTag : IMaterializing<Materialized.EventTag>
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

		public uint[] Frames => Native.ZkEventTag_getFrames(_handle, out var count).MarshalAsArray<uint>(count);

		public FightMode FightMode => Native.ZkEventTag_getFightMode(_handle);

		public bool Attached => Native.ZkEventTag_getIsAttached(_handle);

		public Materialized.EventTag Materialize()
		{
			return new Materialized.EventTag
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
	}

	public class AnimationAlias : IMaterializing<Materialized.AnimationAlias>
	{
		private readonly UIntPtr _handle;

		public AnimationAlias(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkAnimationAlias_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation alias name");

		public uint Layer => Native.ZkAnimationAlias_getLayer(_handle);

		public string Next => Native.ZkAnimationAlias_getNext(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation alias next");

		public float BlendIn => Native.ZkAnimationAlias_getBlendIn(_handle);

		public float BlendOut => Native.ZkAnimationAlias_getBlendOut(_handle);

		public uint Flags => Native.ZkAnimationAlias_getFlags(_handle);

		public string Alias => Native.ZkAnimationAlias_getAlias(_handle).MarshalAsString() ??
		                       throw new Exception("Failed to load animation alias alias");

		public AnimationDirection Direction => Native.ZkAnimationAlias_getDirection(_handle);

		public Materialized.AnimationAlias Materialize()
		{
			return new Materialized.AnimationAlias
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
	}

	public class AnimationBlend : IMaterializing<Materialized.AnimationBlend>
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

		public Materialized.AnimationBlend Materialize()
		{
			return new Materialized.AnimationBlend
			{
				Name = Name,
				Next = Next,
				BlendIn = BlendIn,
				BlendOut = BlendOut
			};
		}
	}

	public class AnimationCombine : IMaterializing<Materialized.AnimationCombine>
	{
		private readonly UIntPtr _handle;

		public AnimationCombine(UIntPtr handle)
		{
			_handle = handle;
		}

		public string Name => Native.ZkAnimationCombine_getName(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation combine name");

		public uint Layer => Native.ZkAnimationCombine_getLayer(_handle);

		public string Next => Native.ZkAnimationCombine_getNext(_handle).MarshalAsString() ??
		                      throw new Exception("Failed to load animation combine next");

		public float BlendIn => Native.ZkAnimationCombine_getBlendIn(_handle);

		public float BlendOut => Native.ZkAnimationCombine_getBlendOut(_handle);

		public uint Flags => Native.ZkAnimationCombine_getFlags(_handle);

		public string Model => Native.ZkAnimationCombine_getModel(_handle).MarshalAsString() ??
		                       throw new Exception("Failed to load animation combine model");

		public int LastFrame => Native.ZkAnimationCombine_getLastFrame(_handle);

		public Materialized.AnimationCombine Materialize()
		{
			return new Materialized.AnimationCombine
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
	}
}