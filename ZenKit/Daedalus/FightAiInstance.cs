using System;

namespace ZenKit.Daedalus
{
	public enum FightAiMove
	{
		Nop = 0,
		Run = 1,
		RunBack = 2,
		JumpBack = 3,
		Turn = 4,
		Strafe = 5,
		Attack = 6,
		AttackSide = 7,
		AttackFront = 8,
		AttackTriple = 9,
		AttackWhirl = 10,
		AttackMaster = 11,
		TurnToHit = 15,
		Parry = 17,
		StandUp = 18,
		Wait = 19,
		WaitLonger = 23,
		WaitExt = 24
	}

	public class FightAiInstance : DaedalusInstance
	{
		public FightAiInstance(UIntPtr handle) : base(handle)
		{
		}

		public FightAiMove GetMove(ulong i)
		{
			return Native.ZkFightAiInstance_getMove(Handle, i);
		}
	}
}