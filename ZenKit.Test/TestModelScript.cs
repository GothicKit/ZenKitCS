using System;
using NUnit.Framework;

namespace ZenKit.Test;

public class TestModelScript
{
	[OneTimeSetUp]
	public void SetUp()
	{
		Logger.Set(LogLevel.Trace,
			(level, name, message) =>
				Console.WriteLine(new DateTime() + " [ZenKit] (" + level + ") > " + name + ": " + message));
	}

	[Test]
	public void TestLoad()
	{
		var script = new ModelScript("Samples/waran.mds");

		Assert.That(script.SkeletonMeshDisabled, Is.True);
		Assert.That(script.SkeletonName, Is.EqualTo("TestModelMesh.asc"));

		var anims = script.Animations;
		Assert.That(anims, Has.Count.EqualTo(2));
		Assert.That(anims[0].Name, Is.EqualTo("aniName1"));
		Assert.That(anims[0].Layer, Is.EqualTo(111));
		Assert.That(anims[0].Next, Is.EqualTo("aniNext1"));
		Assert.That(anims[0].BlendIn, Is.EqualTo(4.2f));
		Assert.That(anims[0].BlendOut, Is.EqualTo(0.5f));
		Assert.That(anims[0].Flags, Is.EqualTo(AnimationFlag.Move | AnimationFlag.Idle));
		Assert.That(anims[0].Model, Is.EqualTo("aniModel1"));
		Assert.That(anims[0].Direction, Is.EqualTo(AnimationDirection.Forward));
		Assert.That(anims[0].FirstFrame, Is.EqualTo(221));
		Assert.That(anims[0].LastFrame, Is.EqualTo(-331));
		Assert.That(anims[0].Fps, Is.EqualTo(25.0f));
		Assert.That(anims[0].Speed, Is.EqualTo(0.0f));
		Assert.That(anims[0].CollisionVolumeScale, Is.EqualTo(0.2f));
		Assert.That(anims[0].ParticleEffects, Has.Count.EqualTo(0));
		Assert.That(anims[0].ParticleEffectsStop, Has.Count.EqualTo(0));
		Assert.That(anims[0].SoundEffects, Has.Count.EqualTo(0));
		Assert.That(anims[0].SoundEffectsGround, Has.Count.EqualTo(0));
		Assert.That(anims[0].EventTags, Has.Count.EqualTo(0));
		Assert.That(anims[0].MorphAnimations, Has.Count.EqualTo(0));
		Assert.That(anims[0].CameraTremors, Has.Count.EqualTo(0));
		Assert.That(anims[0].ParticleEffectCount, Is.EqualTo(0));
		Assert.That(anims[0].ParticleEffectStopCount, Is.EqualTo(0));
		Assert.That(anims[0].SoundEffectCount, Is.EqualTo(0));
		Assert.That(anims[0].SoundEffectGroundCount, Is.EqualTo(0));
		Assert.That(anims[0].EventTagCount, Is.EqualTo(0));
		Assert.That(anims[0].MorphAnimationCount, Is.EqualTo(0));
		Assert.That(anims[0].CameraTremorCount, Is.EqualTo(0));

		Assert.That(anims[1].Name, Is.EqualTo("aniName2"));
		Assert.That(anims[1].Layer, Is.EqualTo(112));
		Assert.That(anims[1].Next, Is.EqualTo("aniNext2"));
		Assert.That(anims[1].BlendIn, Is.EqualTo(9.0f));
		Assert.That(anims[1].BlendOut, Is.EqualTo(0.0f));
		Assert.That(anims[1].Flags, Is.EqualTo(AnimationFlag.Move));
		Assert.That(anims[1].Model, Is.EqualTo("aniModel2"));
		Assert.That(anims[1].Direction, Is.EqualTo(AnimationDirection.Backward));
		Assert.That(anims[1].FirstFrame, Is.EqualTo(222));
		Assert.That(anims[1].LastFrame, Is.EqualTo(332));
		Assert.That(anims[1].Fps, Is.EqualTo(25.0f));
		Assert.That(anims[1].Speed, Is.EqualTo(0.0f));
		Assert.That(anims[1].CollisionVolumeScale, Is.EqualTo(1.0f));

		var events = anims[1].EventTags;
		Assert.That(events, Has.Count.EqualTo(3));
		Assert.That(events[0].Frame, Is.EqualTo(0));
		Assert.That(events[0].Type, Is.EqualTo(EventType.TorchDrop));
		Assert.That(events[0].Attached, Is.EqualTo(true));
		Assert.That(events[1].Frame, Is.EqualTo(1));
		Assert.That(events[1].Type, Is.EqualTo(EventType.ComboWindow));
		Assert.That(events[1].Attached, Is.EqualTo(false));
		Assert.That(events[2].Frame, Is.EqualTo(0));
		Assert.That(events[2].Type, Is.EqualTo(EventType.ItemCreate));
		Assert.That(events[2].Slots.Item1, Is.EqualTo("eventSlot"));
		Assert.That(events[2].Item, Is.EqualTo("eventItem"));
		Assert.That(events[2].Attached, Is.EqualTo(false));
		Assert.That(events[1].Frames[0], Is.EqualTo(1));
		Assert.That(events[1].Frames[1], Is.EqualTo(2));
		Assert.That(events[1].Frames[2], Is.EqualTo(3));
		Assert.That(events[1].Frames[3], Is.EqualTo(4));
		Assert.That(events[1].Frames[4], Is.EqualTo(5));

		var sfx = anims[1].SoundEffects;
		Assert.That(sfx, Has.Count.EqualTo(3));
		Assert.That(sfx[0].Frame, Is.EqualTo(3));
		Assert.That(sfx[0].Name, Is.EqualTo("sfxName1"));
		Assert.That(sfx[0].EmptySlot, Is.EqualTo(true));
		Assert.That(sfx[1].Frame, Is.EqualTo(4));
		Assert.That(sfx[1].Name, Is.EqualTo("sfxName2"));
		Assert.That(sfx[1].Range, Is.EqualTo(67.4f));
		Assert.That(sfx[1].EmptySlot, Is.EqualTo(false));

		var sfxGround = anims[1].SoundEffectsGround;
		Assert.That(sfxGround, Has.Count.EqualTo(1));
		Assert.That(sfxGround[0].Frame, Is.EqualTo(5));
		Assert.That(sfxGround[0].Name, Is.EqualTo("sfxGrndName"));
		Assert.That(sfxGround[0].EmptySlot, Is.EqualTo(false));

		var pfx = anims[1].ParticleEffects;
		Assert.That(pfx, Has.Count.EqualTo(3));
		Assert.That(pfx[0].Frame, Is.EqualTo(6));
		Assert.That(pfx[0].Index, Is.EqualTo(0));
		Assert.That(pfx[0].Name, Is.EqualTo("pfxName1"));
		Assert.That(pfx[0].Position, Is.EqualTo("pfxPosition1"));
		Assert.That(pfx[0].Attached, Is.EqualTo(true));
		Assert.That(pfx[1].Frame, Is.EqualTo(7));
		Assert.That(pfx[1].Index, Is.EqualTo(991));
		Assert.That(pfx[1].Name, Is.EqualTo("pfxName2"));
		Assert.That(pfx[1].Position, Is.EqualTo("pfxPosition2"));
		Assert.That(pfx[1].Attached, Is.EqualTo(false));
		Assert.That(pfx[2].Frame, Is.EqualTo(9));
		Assert.That(pfx[2].Index, Is.EqualTo(991));
		Assert.That(pfx[2].Name, Is.EqualTo("pfxName3"));
		Assert.That(pfx[2].Position, Is.EqualTo("pfxPosition3"));
		Assert.That(pfx[2].Attached, Is.EqualTo(true));

		var pfxStop = anims[1].ParticleEffectsStop;
		Assert.That(pfxStop, Has.Count.EqualTo(1));
		Assert.That(pfxStop[0].Frame, Is.EqualTo(8));
		Assert.That(pfxStop[0].Index, Is.EqualTo(992));

		var morph = anims[1].MorphAnimations;
		Assert.That(morph, Has.Count.EqualTo(2));
		Assert.That(morph[0].Frame, Is.EqualTo(9));
		Assert.That(morph[0].Animation, Is.EqualTo("mmAni1"));
		Assert.That(morph[0].Node, Is.EqualTo(""));
		Assert.That(morph[1].Frame, Is.EqualTo(10));
		Assert.That(morph[1].Animation, Is.EqualTo("mmAni2"));
		Assert.That(morph[1].Node, Is.EqualTo("mmNode"));

		var tremors = anims[1].CameraTremors;
		Assert.That(tremors, Has.Count.EqualTo(1));
		Assert.That(tremors[0].Frame, Is.EqualTo(11));
		Assert.That(tremors[0].Field1, Is.EqualTo(881));
		Assert.That(tremors[0].Field2, Is.EqualTo(882));
		Assert.That(tremors[0].Field3, Is.EqualTo(883));
		Assert.That(tremors[0].Field4, Is.EqualTo(884));

		var blends = script.AnimationBlends;
		Assert.That(blends, Has.Count.EqualTo(3));
		Assert.That(blends[0].Name, Is.EqualTo("blendName1"));
		Assert.That(blends[0].Next, Is.EqualTo("blendNext1"));
		Assert.That(blends[1].Name, Is.EqualTo("blendName2"));
		Assert.That(blends[1].Next, Is.EqualTo("blendNext2"));
		Assert.That(blends[2].Name, Is.EqualTo("blendName3"));
		Assert.That(blends[2].Next, Is.EqualTo("blendNext3"));
		Assert.That(blends[2].BlendIn, Is.EqualTo(223.1f));
		Assert.That(blends[2].BlendOut, Is.EqualTo(333.1f));

		var aliases = script.AnimationAliases;
		Assert.That(aliases, Has.Count.EqualTo(2));
		Assert.That(aliases[0].Name, Is.EqualTo("aliasName1"));
		Assert.That(aliases[0].Layer, Is.EqualTo(114));
		Assert.That(aliases[0].Next, Is.EqualTo("aliasNext1"));
		Assert.That(aliases[0].BlendIn, Is.EqualTo(100.1f));
		Assert.That(aliases[0].BlendOut, Is.EqualTo(200.2f));
		Assert.That(aliases[0].Flags, Is.EqualTo(AnimationFlag.Rotate | AnimationFlag.Queue));
		Assert.That(aliases[0].Alias, Is.EqualTo("aliasAlias1"));
		Assert.That(aliases[0].Direction, Is.EqualTo(AnimationDirection.Forward));
		Assert.That(aliases[1].Name, Is.EqualTo("aliasName2"));
		Assert.That(aliases[1].Layer, Is.EqualTo(115));
		Assert.That(aliases[1].Next, Is.EqualTo("aliasNext2"));
		Assert.That(aliases[1].BlendIn, Is.EqualTo(101.1f));
		Assert.That(aliases[1].BlendOut, Is.EqualTo(201.2f));
		Assert.That(aliases[1].Flags, Is.EqualTo(AnimationFlag.Fly));
		Assert.That(aliases[1].Alias, Is.EqualTo("aliasAlias2"));
		Assert.That(aliases[1].Direction, Is.EqualTo(AnimationDirection.Backward));

		var combines = script.AnimationCombines;
		Assert.That(combines, Has.Count.EqualTo(2));
		Assert.That(combines[0].Name, Is.EqualTo("combName1"));
		Assert.That(combines[0].Layer, Is.EqualTo(116));
		Assert.That(combines[0].Next, Is.EqualTo("combNext1"));
		Assert.That(combines[0].BlendIn, Is.EqualTo(102.1f));
		Assert.That(combines[0].BlendOut, Is.EqualTo(202.2f));
		Assert.That(combines[0].Flags, Is.EqualTo(AnimationFlag.Move));
		Assert.That(combines[0].Model, Is.EqualTo("combModel1"));
		Assert.That(combines[0].LastFrame, Is.EqualTo(226));
		Assert.That(combines[1].Name, Is.EqualTo("combName2"));
		Assert.That(combines[1].Layer, Is.EqualTo(117));
		Assert.That(combines[1].Next, Is.EqualTo("combNext2"));
		Assert.That(combines[1].BlendIn, Is.EqualTo(103.1f));
		Assert.That(combines[1].BlendOut, Is.EqualTo(203.2f));
		Assert.That(combines[1].Flags, Is.EqualTo(AnimationFlag.Idle));
		Assert.That(combines[1].Model, Is.EqualTo("combModel2"));
		Assert.That(combines[1].LastFrame, Is.EqualTo(227));

		var disabled = script.DisabledAnimations;
		Assert.That(disabled, Has.Count.EqualTo(2));
		Assert.That(disabled[0], Is.EqualTo("disable1"));
		Assert.That(disabled[1], Is.EqualTo("disable2"));

		var tags = script.ModelTags;
		Assert.That(tags, Has.Count.EqualTo(2));
		Assert.That(tags[0], Is.EqualTo("tag1"));
		Assert.That(tags[1], Is.EqualTo("tag2"));
	}
}