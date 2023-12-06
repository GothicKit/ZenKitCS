using System;

namespace ZenKit.Daedalus
{
	public class SvmInstance : DaedalusInstance
	{
		internal SvmInstance(UIntPtr handle) : base(handle)
		{
		}

		public string MilGreetings => Native.ZkSvmInstance_getMilGreetings(Handle).MarshalAsString() ?? string.Empty;
		public string PalGreetings => Native.ZkSvmInstance_getPalGreetings(Handle).MarshalAsString() ?? string.Empty;
		public string Weather => Native.ZkSvmInstance_getWeather(Handle).MarshalAsString() ?? string.Empty;
		public string IGetYouStill => Native.ZkSvmInstance_getIGetYouStill(Handle).MarshalAsString() ?? string.Empty;
		public string DieEnemy => Native.ZkSvmInstance_getDieEnemy(Handle).MarshalAsString() ?? string.Empty;
		public string DieMonster => Native.ZkSvmInstance_getDieMonster(Handle).MarshalAsString() ?? string.Empty;

		public string AddonDieMonster =>
			Native.ZkSvmInstance_getAddonDieMonster(Handle).MarshalAsString() ?? string.Empty;

		public string AddonDieMonster2 =>
			Native.ZkSvmInstance_getAddonDieMonster2(Handle).MarshalAsString() ?? string.Empty;

		public string DirtyThief => Native.ZkSvmInstance_getDirtyThief(Handle).MarshalAsString() ?? string.Empty;
		public string HandsOff => Native.ZkSvmInstance_getHandsOff(Handle).MarshalAsString() ?? string.Empty;
		public string SheepKiller => Native.ZkSvmInstance_getSheepKiller(Handle).MarshalAsString() ?? string.Empty;

		public string SheepKillerMonster =>
			Native.ZkSvmInstance_getSheepKillerMonster(Handle).MarshalAsString() ?? string.Empty;

		public string YouMurderer => Native.ZkSvmInstance_getYouMurderer(Handle).MarshalAsString() ?? string.Empty;

		public string DieStupidBeast =>
			Native.ZkSvmInstance_getDieStupidBeast(Handle).MarshalAsString() ?? string.Empty;

		public string YouDareHitMe => Native.ZkSvmInstance_getYouDareHitMe(Handle).MarshalAsString() ?? string.Empty;
		public string YouAskedForIt => Native.ZkSvmInstance_getYouAskedForIt(Handle).MarshalAsString() ?? string.Empty;

		public string ThenIBeatYouOutOfHere =>
			Native.ZkSvmInstance_getThenIBeatYouOutOfHere(Handle).MarshalAsString() ?? string.Empty;

		public string WhatDidYouDoInThere =>
			Native.ZkSvmInstance_getWhatDidYouDoInThere(Handle).MarshalAsString() ?? string.Empty;

		public string WillYouStopFighting =>
			Native.ZkSvmInstance_getWillYouStopFighting(Handle).MarshalAsString() ?? string.Empty;

		public string KillEnemy => Native.ZkSvmInstance_getKillEnemy(Handle).MarshalAsString() ?? string.Empty;
		public string EnemyKilled => Native.ZkSvmInstance_getEnemyKilled(Handle).MarshalAsString() ?? string.Empty;
		public string MonsterKilled => Native.ZkSvmInstance_getMonsterKilled(Handle).MarshalAsString() ?? string.Empty;

		public string AddonMonsterKilled =>
			Native.ZkSvmInstance_getAddonMonsterKilled(Handle).MarshalAsString() ?? string.Empty;

		public string AddonMonsterKilled2 =>
			Native.ZkSvmInstance_getAddonMonsterKilled2(Handle).MarshalAsString() ?? string.Empty;

		public string ThiefDown => Native.ZkSvmInstance_getThiefDown(Handle).MarshalAsString() ?? string.Empty;

		public string RumfummlerDown =>
			Native.ZkSvmInstance_getRumfummlerDown(Handle).MarshalAsString() ?? string.Empty;

		public string SheepAttackerDown =>
			Native.ZkSvmInstance_getSheepAttackerDown(Handle).MarshalAsString() ?? string.Empty;

		public string KillMurderer => Native.ZkSvmInstance_getKillMurderer(Handle).MarshalAsString() ?? string.Empty;

		public string StupidBeastKilled =>
			Native.ZkSvmInstance_getStupidBeastKilled(Handle).MarshalAsString() ?? string.Empty;

		public string NeverHitMeAgain =>
			Native.ZkSvmInstance_getNeverHitMeAgain(Handle).MarshalAsString() ?? string.Empty;

		public string YouBetterShouldHaveListened =>
			Native.ZkSvmInstance_getYouBetterShouldHaveListened(Handle).MarshalAsString() ?? string.Empty;

		public string GetUpAndBegone =>
			Native.ZkSvmInstance_getGetUpAndBegone(Handle).MarshalAsString() ?? string.Empty;

		public string NeverEnterRoomAgain =>
			Native.ZkSvmInstance_getNeverEnterRoomAgain(Handle).MarshalAsString() ?? string.Empty;

		public string ThereIsNoFightingHere =>
			Native.ZkSvmInstance_getThereIsNoFightingHere(Handle).MarshalAsString() ?? string.Empty;

		public string SpareMe => Native.ZkSvmInstance_getSpareMe(Handle).MarshalAsString() ?? string.Empty;
		public string RunAway => Native.ZkSvmInstance_getRunAway(Handle).MarshalAsString() ?? string.Empty;
		public string Alarm => Native.ZkSvmInstance_getAlarm(Handle).MarshalAsString() ?? string.Empty;
		public string Guards => Native.ZkSvmInstance_getGuards(Handle).MarshalAsString() ?? string.Empty;
		public string Help => Native.ZkSvmInstance_getHelp(Handle).MarshalAsString() ?? string.Empty;

		public string GoodMonsterKill =>
			Native.ZkSvmInstance_getGoodMonsterKill(Handle).MarshalAsString() ?? string.Empty;

		public string GoodKill => Native.ZkSvmInstance_getGoodKill(Handle).MarshalAsString() ?? string.Empty;
		public string NotNow => Native.ZkSvmInstance_getNotNow(Handle).MarshalAsString() ?? string.Empty;
		public string RunCoward => Native.ZkSvmInstance_getRunCoward(Handle).MarshalAsString() ?? string.Empty;
		public string GetOutOfHere => Native.ZkSvmInstance_getGetOutOfHere(Handle).MarshalAsString() ?? string.Empty;

		public string WhyAreYouInHere =>
			Native.ZkSvmInstance_getWhyAreYouInHere(Handle).MarshalAsString() ?? string.Empty;

		public string YesGoOutOfHere =>
			Native.ZkSvmInstance_getYesGoOutOfHere(Handle).MarshalAsString() ?? string.Empty;

		public string WhatsThisSupposedToBe =>
			Native.ZkSvmInstance_getWhatsThisSupposedToBe(Handle).MarshalAsString() ?? string.Empty;

		public string YouDisturbedMySlumber =>
			Native.ZkSvmInstance_getYouDisturbedMySlumber(Handle).MarshalAsString() ?? string.Empty;

		public string ITookYourGold => Native.ZkSvmInstance_getITookYourGold(Handle).MarshalAsString() ?? string.Empty;
		public string ShitNoGold => Native.ZkSvmInstance_getShitNoGold(Handle).MarshalAsString() ?? string.Empty;

		public string ITakeYourWeapon =>
			Native.ZkSvmInstance_getITakeYourWeapon(Handle).MarshalAsString() ?? string.Empty;

		public string WhatAreYouDoing =>
			Native.ZkSvmInstance_getWhatAreYouDoing(Handle).MarshalAsString() ?? string.Empty;

		public string LookingForTroubleAgain =>
			Native.ZkSvmInstance_getLookingForTroubleAgain(Handle).MarshalAsString() ?? string.Empty;

		public string StopMagic => Native.ZkSvmInstance_getStopMagic(Handle).MarshalAsString() ?? string.Empty;

		public string ISaidStopMagic =>
			Native.ZkSvmInstance_getISaidStopMagic(Handle).MarshalAsString() ?? string.Empty;

		public string WeaponDown => Native.ZkSvmInstance_getWeaponDown(Handle).MarshalAsString() ?? string.Empty;

		public string ISaidWeaponDown =>
			Native.ZkSvmInstance_getISaidWeaponDown(Handle).MarshalAsString() ?? string.Empty;

		public string WiseMove => Native.ZkSvmInstance_getWiseMove(Handle).MarshalAsString() ?? string.Empty;

		public string NextTimeYoureInForIt =>
			Native.ZkSvmInstance_getNextTimeYoureInForIt(Handle).MarshalAsString() ?? string.Empty;

		public string OhMyHead => Native.ZkSvmInstance_getOhMyHead(Handle).MarshalAsString() ?? string.Empty;
		public string TheresAFight => Native.ZkSvmInstance_getTheresAFight(Handle).MarshalAsString() ?? string.Empty;

		public string OhMyGodItsAFight =>
			Native.ZkSvmInstance_getOhMyGodItsAFight(Handle).MarshalAsString() ?? string.Empty;

		public string GoodVictory => Native.ZkSvmInstance_getGoodVictory(Handle).MarshalAsString() ?? string.Empty;
		public string NotBad => Native.ZkSvmInstance_getNotBad(Handle).MarshalAsString() ?? string.Empty;

		public string OhMyGodHesDown =>
			Native.ZkSvmInstance_getOhMyGodHesDown(Handle).MarshalAsString() ?? string.Empty;

		public string CheerFriend01 => Native.ZkSvmInstance_getCheerFriend01(Handle).MarshalAsString() ?? string.Empty;
		public string CheerFriend02 => Native.ZkSvmInstance_getCheerFriend02(Handle).MarshalAsString() ?? string.Empty;
		public string CheerFriend03 => Native.ZkSvmInstance_getCheerFriend03(Handle).MarshalAsString() ?? string.Empty;
		public string Ooh01 => Native.ZkSvmInstance_getOoh01(Handle).MarshalAsString() ?? string.Empty;
		public string Ooh02 => Native.ZkSvmInstance_getOoh02(Handle).MarshalAsString() ?? string.Empty;
		public string Ooh03 => Native.ZkSvmInstance_getOoh03(Handle).MarshalAsString() ?? string.Empty;
		public string WhatWasThat => Native.ZkSvmInstance_getWhatWasThat(Handle).MarshalAsString() ?? string.Empty;
		public string GetOutOfMyBed => Native.ZkSvmInstance_getGetOutOfMyBed(Handle).MarshalAsString() ?? string.Empty;
		public string Awake => Native.ZkSvmInstance_getAwake(Handle).MarshalAsString() ?? string.Empty;
		public string AbsCommander => Native.ZkSvmInstance_getAbsCommander(Handle).MarshalAsString() ?? string.Empty;
		public string AbsMonastery => Native.ZkSvmInstance_getAbsMonastery(Handle).MarshalAsString() ?? string.Empty;
		public string AbsFarm => Native.ZkSvmInstance_getAbsFarm(Handle).MarshalAsString() ?? string.Empty;
		public string AbsGood => Native.ZkSvmInstance_getAbsGood(Handle).MarshalAsString() ?? string.Empty;

		public string SheepKillerCrime =>
			Native.ZkSvmInstance_getSheepKillerCrime(Handle).MarshalAsString() ?? string.Empty;

		public string AttackCrime => Native.ZkSvmInstance_getAttackCrime(Handle).MarshalAsString() ?? string.Empty;
		public string TheftCrime => Native.ZkSvmInstance_getTheftCrime(Handle).MarshalAsString() ?? string.Empty;
		public string MurderCrime => Native.ZkSvmInstance_getMurderCrime(Handle).MarshalAsString() ?? string.Empty;
		public string PalCityCrime => Native.ZkSvmInstance_getPalCityCrime(Handle).MarshalAsString() ?? string.Empty;
		public string MilCityCrime => Native.ZkSvmInstance_getMilCityCrime(Handle).MarshalAsString() ?? string.Empty;
		public string CityCrime => Native.ZkSvmInstance_getCityCrime(Handle).MarshalAsString() ?? string.Empty;
		public string MonaCrime => Native.ZkSvmInstance_getMonaCrime(Handle).MarshalAsString() ?? string.Empty;
		public string FarmCrime => Native.ZkSvmInstance_getFarmCrime(Handle).MarshalAsString() ?? string.Empty;
		public string OcCrime => Native.ZkSvmInstance_getOcCrime(Handle).MarshalAsString() ?? string.Empty;

		public string ToughguyAttackLost =>
			Native.ZkSvmInstance_getToughguyAttackLost(Handle).MarshalAsString() ?? string.Empty;

		public string ToughguyAttackWon =>
			Native.ZkSvmInstance_getToughguyAttackWon(Handle).MarshalAsString() ?? string.Empty;

		public string ToughguyPlayerAttack =>
			Native.ZkSvmInstance_getToughguyPlayerAttack(Handle).MarshalAsString() ?? string.Empty;

		public string Gold1000 => Native.ZkSvmInstance_getGold1000(Handle).MarshalAsString() ?? string.Empty;
		public string Gold950 => Native.ZkSvmInstance_getGold950(Handle).MarshalAsString() ?? string.Empty;
		public string Gold900 => Native.ZkSvmInstance_getGold900(Handle).MarshalAsString() ?? string.Empty;
		public string Gold850 => Native.ZkSvmInstance_getGold850(Handle).MarshalAsString() ?? string.Empty;
		public string Gold800 => Native.ZkSvmInstance_getGold800(Handle).MarshalAsString() ?? string.Empty;
		public string Gold750 => Native.ZkSvmInstance_getGold750(Handle).MarshalAsString() ?? string.Empty;
		public string Gold700 => Native.ZkSvmInstance_getGold700(Handle).MarshalAsString() ?? string.Empty;
		public string Gold650 => Native.ZkSvmInstance_getGold650(Handle).MarshalAsString() ?? string.Empty;
		public string Gold600 => Native.ZkSvmInstance_getGold600(Handle).MarshalAsString() ?? string.Empty;
		public string Gold550 => Native.ZkSvmInstance_getGold550(Handle).MarshalAsString() ?? string.Empty;
		public string Gold500 => Native.ZkSvmInstance_getGold500(Handle).MarshalAsString() ?? string.Empty;
		public string Gold450 => Native.ZkSvmInstance_getGold450(Handle).MarshalAsString() ?? string.Empty;
		public string Gold400 => Native.ZkSvmInstance_getGold400(Handle).MarshalAsString() ?? string.Empty;
		public string Gold350 => Native.ZkSvmInstance_getGold350(Handle).MarshalAsString() ?? string.Empty;
		public string Gold300 => Native.ZkSvmInstance_getGold300(Handle).MarshalAsString() ?? string.Empty;
		public string Gold250 => Native.ZkSvmInstance_getGold250(Handle).MarshalAsString() ?? string.Empty;
		public string Gold200 => Native.ZkSvmInstance_getGold200(Handle).MarshalAsString() ?? string.Empty;
		public string Gold150 => Native.ZkSvmInstance_getGold150(Handle).MarshalAsString() ?? string.Empty;
		public string Gold100 => Native.ZkSvmInstance_getGold100(Handle).MarshalAsString() ?? string.Empty;
		public string Gold90 => Native.ZkSvmInstance_getGold90(Handle).MarshalAsString() ?? string.Empty;
		public string Gold80 => Native.ZkSvmInstance_getGold80(Handle).MarshalAsString() ?? string.Empty;
		public string Gold70 => Native.ZkSvmInstance_getGold70(Handle).MarshalAsString() ?? string.Empty;
		public string Gold60 => Native.ZkSvmInstance_getGold60(Handle).MarshalAsString() ?? string.Empty;
		public string Gold50 => Native.ZkSvmInstance_getGold50(Handle).MarshalAsString() ?? string.Empty;
		public string Gold40 => Native.ZkSvmInstance_getGold40(Handle).MarshalAsString() ?? string.Empty;
		public string Gold30 => Native.ZkSvmInstance_getGold30(Handle).MarshalAsString() ?? string.Empty;
		public string Gold20 => Native.ZkSvmInstance_getGold20(Handle).MarshalAsString() ?? string.Empty;
		public string Gold10 => Native.ZkSvmInstance_getGold10(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk01 => Native.ZkSvmInstance_getSmalltalk01(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk02 => Native.ZkSvmInstance_getSmalltalk02(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk03 => Native.ZkSvmInstance_getSmalltalk03(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk04 => Native.ZkSvmInstance_getSmalltalk04(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk05 => Native.ZkSvmInstance_getSmalltalk05(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk06 => Native.ZkSvmInstance_getSmalltalk06(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk07 => Native.ZkSvmInstance_getSmalltalk07(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk08 => Native.ZkSvmInstance_getSmalltalk08(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk09 => Native.ZkSvmInstance_getSmalltalk09(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk10 => Native.ZkSvmInstance_getSmalltalk10(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk11 => Native.ZkSvmInstance_getSmalltalk11(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk12 => Native.ZkSvmInstance_getSmalltalk12(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk13 => Native.ZkSvmInstance_getSmalltalk13(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk14 => Native.ZkSvmInstance_getSmalltalk14(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk15 => Native.ZkSvmInstance_getSmalltalk15(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk16 => Native.ZkSvmInstance_getSmalltalk16(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk17 => Native.ZkSvmInstance_getSmalltalk17(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk18 => Native.ZkSvmInstance_getSmalltalk18(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk19 => Native.ZkSvmInstance_getSmalltalk19(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk20 => Native.ZkSvmInstance_getSmalltalk20(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk21 => Native.ZkSvmInstance_getSmalltalk21(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk22 => Native.ZkSvmInstance_getSmalltalk22(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk23 => Native.ZkSvmInstance_getSmalltalk23(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk24 => Native.ZkSvmInstance_getSmalltalk24(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk25 => Native.ZkSvmInstance_getSmalltalk25(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk26 => Native.ZkSvmInstance_getSmalltalk26(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk27 => Native.ZkSvmInstance_getSmalltalk27(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk28 => Native.ZkSvmInstance_getSmalltalk28(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk29 => Native.ZkSvmInstance_getSmalltalk29(Handle).MarshalAsString() ?? string.Empty;
		public string Smalltalk30 => Native.ZkSvmInstance_getSmalltalk30(Handle).MarshalAsString() ?? string.Empty;

		public string NoLearnNoPoints =>
			Native.ZkSvmInstance_getNoLearnNoPoints(Handle).MarshalAsString() ?? string.Empty;

		public string NoLearnOverPersonalMax =>
			Native.ZkSvmInstance_getNoLearnOverPersonalMax(Handle).MarshalAsString() ?? string.Empty;

		public string NoLearnYoureBetter =>
			Native.ZkSvmInstance_getNoLearnYoureBetter(Handle).MarshalAsString() ?? string.Empty;

		public string YouLearnedSomething =>
			Native.ZkSvmInstance_getYouLearnedSomething(Handle).MarshalAsString() ?? string.Empty;

		public string Unterstadt => Native.ZkSvmInstance_getUnterstadt(Handle).MarshalAsString() ?? string.Empty;
		public string Oberstadt => Native.ZkSvmInstance_getOberstadt(Handle).MarshalAsString() ?? string.Empty;
		public string Tempel => Native.ZkSvmInstance_getTempel(Handle).MarshalAsString() ?? string.Empty;
		public string Markt => Native.ZkSvmInstance_getMarkt(Handle).MarshalAsString() ?? string.Empty;
		public string Galgen => Native.ZkSvmInstance_getGalgen(Handle).MarshalAsString() ?? string.Empty;
		public string Kaserne => Native.ZkSvmInstance_getKaserne(Handle).MarshalAsString() ?? string.Empty;
		public string Hafen => Native.ZkSvmInstance_getHafen(Handle).MarshalAsString() ?? string.Empty;
		public string Whereto => Native.ZkSvmInstance_getWhereto(Handle).MarshalAsString() ?? string.Empty;

		public string Oberstadt2Unterstadt =>
			Native.ZkSvmInstance_getOberstadt2Unterstadt(Handle).MarshalAsString() ?? string.Empty;

		public string Unterstadt2Oberstadt =>
			Native.ZkSvmInstance_getUnterstadt2Oberstadt(Handle).MarshalAsString() ?? string.Empty;

		public string Unterstadt2Tempel =>
			Native.ZkSvmInstance_getUnterstadt2Tempel(Handle).MarshalAsString() ?? string.Empty;

		public string Unterstadt2Hafen =>
			Native.ZkSvmInstance_getUnterstadt2Hafen(Handle).MarshalAsString() ?? string.Empty;

		public string Tempel2Unterstadt =>
			Native.ZkSvmInstance_getTempel2Unterstadt(Handle).MarshalAsString() ?? string.Empty;

		public string Tempel2Markt => Native.ZkSvmInstance_getTempel2Markt(Handle).MarshalAsString() ?? string.Empty;
		public string Tempel2Galgen => Native.ZkSvmInstance_getTempel2Galgen(Handle).MarshalAsString() ?? string.Empty;
		public string Markt2Tempel => Native.ZkSvmInstance_getMarkt2Tempel(Handle).MarshalAsString() ?? string.Empty;
		public string Markt2Kaserne => Native.ZkSvmInstance_getMarkt2Kaserne(Handle).MarshalAsString() ?? string.Empty;
		public string Markt2Galgen => Native.ZkSvmInstance_getMarkt2Galgen(Handle).MarshalAsString() ?? string.Empty;
		public string Galgen2Tempel => Native.ZkSvmInstance_getGalgen2Tempel(Handle).MarshalAsString() ?? string.Empty;
		public string Galgen2Markt => Native.ZkSvmInstance_getGalgen2Markt(Handle).MarshalAsString() ?? string.Empty;

		public string Galgen2Kaserne =>
			Native.ZkSvmInstance_getGalgen2Kaserne(Handle).MarshalAsString() ?? string.Empty;

		public string Kaserne2Markt => Native.ZkSvmInstance_getKaserne2Markt(Handle).MarshalAsString() ?? string.Empty;

		public string Kaserne2Galgen =>
			Native.ZkSvmInstance_getKaserne2Galgen(Handle).MarshalAsString() ?? string.Empty;

		public string Hafen2Unterstadt =>
			Native.ZkSvmInstance_getHafen2Unterstadt(Handle).MarshalAsString() ?? string.Empty;

		public string Dead => Native.ZkSvmInstance_getDead(Handle).MarshalAsString() ?? string.Empty;
		public string Aargh1 => Native.ZkSvmInstance_getAargh1(Handle).MarshalAsString() ?? string.Empty;
		public string Aargh2 => Native.ZkSvmInstance_getAargh2(Handle).MarshalAsString() ?? string.Empty;
		public string Aargh3 => Native.ZkSvmInstance_getAargh3(Handle).MarshalAsString() ?? string.Empty;

		public string AddonWrongArmor =>
			Native.ZkSvmInstance_getAddonWrongArmor(Handle).MarshalAsString() ?? string.Empty;

		public string AddonWrongArmorSld =>
			Native.ZkSvmInstance_getAddonWrongArmorSld(Handle).MarshalAsString() ?? string.Empty;

		public string AddonWrongArmorMil =>
			Native.ZkSvmInstance_getAddonWrongArmorMil(Handle).MarshalAsString() ?? string.Empty;

		public string AddonWrongArmorKdf =>
			Native.ZkSvmInstance_getAddonWrongArmorKdf(Handle).MarshalAsString() ?? string.Empty;

		public string AddonNoArmorBdt =>
			Native.ZkSvmInstance_getAddonNoArmorBdt(Handle).MarshalAsString() ?? string.Empty;

		public string AddonDieBandit =>
			Native.ZkSvmInstance_getAddonDieBandit(Handle).MarshalAsString() ?? string.Empty;

		public string AddonDirtyPirate =>
			Native.ZkSvmInstance_getAddonDirtyPirate(Handle).MarshalAsString() ?? string.Empty;

		public string ScHeyTurnAround =>
			Native.ZkSvmInstance_getScHeyTurnAround(Handle).MarshalAsString() ?? string.Empty;

		public string ScHeyTurnAround02 =>
			Native.ZkSvmInstance_getScHeyTurnAround02(Handle).MarshalAsString() ?? string.Empty;

		public string ScHeyTurnAround03 =>
			Native.ZkSvmInstance_getScHeyTurnAround03(Handle).MarshalAsString() ?? string.Empty;

		public string ScHeyTurnAround04 =>
			Native.ZkSvmInstance_getScHeyTurnAround04(Handle).MarshalAsString() ?? string.Empty;

		public string ScHeyWaitASecond =>
			Native.ZkSvmInstance_getScHeyWaitASecond(Handle).MarshalAsString() ?? string.Empty;

		public string DoesntMork => Native.ZkSvmInstance_getDoesntMork(Handle).MarshalAsString() ?? string.Empty;
		public string PickBroke => Native.ZkSvmInstance_getPickBroke(Handle).MarshalAsString() ?? string.Empty;
		public string NeedKey => Native.ZkSvmInstance_getNeedKey(Handle).MarshalAsString() ?? string.Empty;
		public string NoMorePicks => Native.ZkSvmInstance_getNoMorePicks(Handle).MarshalAsString() ?? string.Empty;

		public string NoPickLockTalent =>
			Native.ZkSvmInstance_getNoPickLockTalent(Handle).MarshalAsString() ?? string.Empty;

		public string NoSweeping => Native.ZkSvmInstance_getNoSweeping(Handle).MarshalAsString() ?? string.Empty;

		public string PickLockOrKeyMissing =>
			Native.ZkSvmInstance_getPickLockOrKeyMissing(Handle).MarshalAsString() ?? string.Empty;

		public string KeyMissing => Native.ZkSvmInstance_getKeyMissing(Handle).MarshalAsString() ?? string.Empty;

		public string PickLockMissing =>
			Native.ZkSvmInstance_getPickLockMissing(Handle).MarshalAsString() ?? string.Empty;

		public string NeverOpen => Native.ZkSvmInstance_getNeverOpen(Handle).MarshalAsString() ?? string.Empty;
		public string MissingItem => Native.ZkSvmInstance_getMissingItem(Handle).MarshalAsString() ?? string.Empty;
		public string DontKnow => Native.ZkSvmInstance_getDontKnow(Handle).MarshalAsString() ?? string.Empty;
		public string NothingToGet => Native.ZkSvmInstance_getNothingToGet(Handle).MarshalAsString() ?? string.Empty;

		public string NothingToGet02 =>
			Native.ZkSvmInstance_getNothingToGet02(Handle).MarshalAsString() ?? string.Empty;

		public string NothingToGet03 =>
			Native.ZkSvmInstance_getNothingToGet03(Handle).MarshalAsString() ?? string.Empty;

		public string HealShrine => Native.ZkSvmInstance_getHealShrine(Handle).MarshalAsString() ?? string.Empty;

		public string HealLastShrine =>
			Native.ZkSvmInstance_getHealLastShrine(Handle).MarshalAsString() ?? string.Empty;

		public string IrdorathThereYouAre =>
			Native.ZkSvmInstance_getIrdorathThereYouAre(Handle).MarshalAsString() ?? string.Empty;

		public string ScOpensIrdorathBook =>
			Native.ZkSvmInstance_getScOpensIrdorathBook(Handle).MarshalAsString() ?? string.Empty;

		public string ScOpensLastDoor =>
			Native.ZkSvmInstance_getScOpensLastDoor(Handle).MarshalAsString() ?? string.Empty;

		public string Trade1 => Native.ZkSvmInstance_getTrade1(Handle).MarshalAsString() ?? string.Empty;
		public string Trade2 => Native.ZkSvmInstance_getTrade2(Handle).MarshalAsString() ?? string.Empty;
		public string Trade3 => Native.ZkSvmInstance_getTrade3(Handle).MarshalAsString() ?? string.Empty;
		public string Verstehe => Native.ZkSvmInstance_getVerstehe(Handle).MarshalAsString() ?? string.Empty;
		public string FoundTreasure => Native.ZkSvmInstance_getFoundTreasure(Handle).MarshalAsString() ?? string.Empty;

		public string CantUnderstandThis =>
			Native.ZkSvmInstance_getCantUnderstandThis(Handle).MarshalAsString() ?? string.Empty;

		public string CantReadThis => Native.ZkSvmInstance_getCantReadThis(Handle).MarshalAsString() ?? string.Empty;
		public string Stoneplate1 => Native.ZkSvmInstance_getStoneplate1(Handle).MarshalAsString() ?? string.Empty;
		public string Stoneplate2 => Native.ZkSvmInstance_getStoneplate2(Handle).MarshalAsString() ?? string.Empty;
		public string Stoneplate3 => Native.ZkSvmInstance_getStoneplate3(Handle).MarshalAsString() ?? string.Empty;
		public string Cough => Native.ZkSvmInstance_getCough(Handle).MarshalAsString() ?? string.Empty;
		public string Hui => Native.ZkSvmInstance_getHui(Handle).MarshalAsString() ?? string.Empty;

		public string AddonThisLittleBastard =>
			Native.ZkSvmInstance_getAddonThisLittleBastard(Handle).MarshalAsString() ?? string.Empty;

		public string AddonOpenAdanosTemple =>
			Native.ZkSvmInstance_getAddonOpenAdanosTemple(Handle).MarshalAsString() ?? string.Empty;

		public string AttentatAddonDescription =>
			Native.ZkSvmInstance_getAttentatAddonDescription(Handle).MarshalAsString() ?? string.Empty;

		public string AttentatAddonDescription2 =>
			Native.ZkSvmInstance_getAttentatAddonDescription2(Handle).MarshalAsString() ?? string.Empty;

		public string AttentatAddonPro =>
			Native.ZkSvmInstance_getAttentatAddonPro(Handle).MarshalAsString() ?? string.Empty;

		public string AttentatAddonContra =>
			Native.ZkSvmInstance_getAttentatAddonContra(Handle).MarshalAsString() ?? string.Empty;

		public string MineAddonDescription =>
			Native.ZkSvmInstance_getMineAddonDescription(Handle).MarshalAsString() ?? string.Empty;

		public string AddonSummonAncientGhost =>
			Native.ZkSvmInstance_getAddonSummonAncientGhost(Handle).MarshalAsString() ?? string.Empty;

		public string AddonAncientGhostNotNear =>
			Native.ZkSvmInstance_getAddonAncientGhostNotNear(Handle).MarshalAsString() ?? string.Empty;

		public string AddonGoldDescription =>
			Native.ZkSvmInstance_getAddonGoldDescription(Handle).MarshalAsString() ?? string.Empty;

		public string WatchYourAim => Native.ZkSvmInstance_getWatchYourAim(Handle).MarshalAsString() ?? string.Empty;

		public string WatchYourAimAngry =>
			Native.ZkSvmInstance_getWatchYourAimAngry(Handle).MarshalAsString() ?? string.Empty;

		public string LetsForgetOurLittleFight =>
			Native.ZkSvmInstance_getLetsForgetOurLittleFight(Handle).MarshalAsString() ?? string.Empty;

		public string Strange => Native.ZkSvmInstance_getStrange(Handle).MarshalAsString() ?? string.Empty;

		public string DieMortalEnemy =>
			Native.ZkSvmInstance_getDieMortalEnemy(Handle).MarshalAsString() ?? string.Empty;

		public string NowWait => Native.ZkSvmInstance_getNowWait(Handle).MarshalAsString() ?? string.Empty;

		public string NowWaitIntruder =>
			Native.ZkSvmInstance_getNowWaitIntruder(Handle).MarshalAsString() ?? string.Empty;

		public string YouStillNotHaveEnough =>
			Native.ZkSvmInstance_getYouStillNotHaveEnough(Handle).MarshalAsString() ?? string.Empty;

		public string YouAttackedMyCharge =>
			Native.ZkSvmInstance_getYouAttackedMyCharge(Handle).MarshalAsString() ?? string.Empty;

		public string IWillTeachYouRespectForForeignProperty =>
			Native.ZkSvmInstance_getIWillTeachYouRespectForForeignProperty(Handle).MarshalAsString() ?? string.Empty;

		public string YouKilledOneOfUs =>
			Native.ZkSvmInstance_getYouKilledOneOfUs(Handle).MarshalAsString() ?? string.Empty;

		public string Berzerk => Native.ZkSvmInstance_getBerzerk(Handle).MarshalAsString() ?? string.Empty;

		public string YoullBeSorryForThis =>
			Native.ZkSvmInstance_getYoullBeSorryForThis(Handle).MarshalAsString() ?? string.Empty;

		public string YesYes => Native.ZkSvmInstance_getYesYes(Handle).MarshalAsString() ?? string.Empty;

		public string ShitWhatAMonster =>
			Native.ZkSvmInstance_getShitWhatAMonster(Handle).MarshalAsString() ?? string.Empty;

		public string WeWillMeetAgain =>
			Native.ZkSvmInstance_getWeWillMeetAgain(Handle).MarshalAsString() ?? string.Empty;

		public string NeverTryThatAgain =>
			Native.ZkSvmInstance_getNeverTryThatAgain(Handle).MarshalAsString() ?? string.Empty;

		public string ITookYourOre => Native.ZkSvmInstance_getITookYourOre(Handle).MarshalAsString() ?? string.Empty;
		public string ShitNoOre => Native.ZkSvmInstance_getShitNoOre(Handle).MarshalAsString() ?? string.Empty;

		public string YouViolatedForbiddenTerritory =>
			Native.ZkSvmInstance_getYouViolatedForbiddenTerritory(Handle).MarshalAsString() ?? string.Empty;

		public string YouWannaFoolMe =>
			Native.ZkSvmInstance_getYouWannaFoolMe(Handle).MarshalAsString() ?? string.Empty;

		public string WhatDidYouInThere =>
			Native.ZkSvmInstance_getWhatDidYouInThere(Handle).MarshalAsString() ?? string.Empty;

		public string IntruderAlert => Native.ZkSvmInstance_getIntruderAlert(Handle).MarshalAsString() ?? string.Empty;
		public string BehindYou => Native.ZkSvmInstance_getBehindYou(Handle).MarshalAsString() ?? string.Empty;
		public string HeyHeyHey => Native.ZkSvmInstance_getHeyHeyHey(Handle).MarshalAsString() ?? string.Empty;
		public string CheerFight => Native.ZkSvmInstance_getCheerFight(Handle).MarshalAsString() ?? string.Empty;
		public string CheerFriend => Native.ZkSvmInstance_getCheerFriend(Handle).MarshalAsString() ?? string.Empty;
		public string Ooh => Native.ZkSvmInstance_getOoh(Handle).MarshalAsString() ?? string.Empty;
		public string YeahWellDone => Native.ZkSvmInstance_getYeahWellDone(Handle).MarshalAsString() ?? string.Empty;
		public string HeDefeatedhim => Native.ZkSvmInstance_getHeDefeatedhim(Handle).MarshalAsString() ?? string.Empty;
		public string HeDeservEdit => Native.ZkSvmInstance_getHeDeservEdit(Handle).MarshalAsString() ?? string.Empty;
		public string HeKilledHim => Native.ZkSvmInstance_getHeKilledHim(Handle).MarshalAsString() ?? string.Empty;

		public string ItWasAGoodFight =>
			Native.ZkSvmInstance_getItWasAGoodFight(Handle).MarshalAsString() ?? string.Empty;

		public string FriendlyGreetings =>
			Native.ZkSvmInstance_getFriendlyGreetings(Handle).MarshalAsString() ?? string.Empty;

		public string AlGreetings => Native.ZkSvmInstance_getAlGreetings(Handle).MarshalAsString() ?? string.Empty;
		public string MageGreetings => Native.ZkSvmInstance_getMageGreetings(Handle).MarshalAsString() ?? string.Empty;
		public string SectGreetings => Native.ZkSvmInstance_getSectGreetings(Handle).MarshalAsString() ?? string.Empty;
		public string ThereHeIs => Native.ZkSvmInstance_getThereHeIs(Handle).MarshalAsString() ?? string.Empty;

		public string NoLearnOverMax =>
			Native.ZkSvmInstance_getNoLearnOverMax(Handle).MarshalAsString() ?? string.Empty;

		public string NoLearnYouAlreadyKnow =>
			Native.ZkSvmInstance_getNoLearnYouAlreadyKnow(Handle).MarshalAsString() ?? string.Empty;

		public string HeyYou => Native.ZkSvmInstance_getHeyYou(Handle).MarshalAsString() ?? string.Empty;
		public string WhatDoYouWant => Native.ZkSvmInstance_getWhatDoYouWant(Handle).MarshalAsString() ?? string.Empty;

		public string ISaidWhatDoYouWant =>
			Native.ZkSvmInstance_getISaidWhatDoYouWant(Handle).MarshalAsString() ?? string.Empty;

		public string MakeWay => Native.ZkSvmInstance_getMakeWay(Handle).MarshalAsString() ?? string.Empty;
		public string OutOfMyWay => Native.ZkSvmInstance_getOutOfMyWay(Handle).MarshalAsString() ?? string.Empty;
		public string YouDeafOrWhat => Native.ZkSvmInstance_getYouDeafOrWhat(Handle).MarshalAsString() ?? string.Empty;
		public string LookAway => Native.ZkSvmInstance_getLookAway(Handle).MarshalAsString() ?? string.Empty;
		public string OkayKeepIt => Native.ZkSvmInstance_getOkayKeepIt(Handle).MarshalAsString() ?? string.Empty;
		public string WhatsThat => Native.ZkSvmInstance_getWhatsThat(Handle).MarshalAsString() ?? string.Empty;
		public string ThatsMyWeapon => Native.ZkSvmInstance_getThatsMyWeapon(Handle).MarshalAsString() ?? string.Empty;
		public string GiveItTome => Native.ZkSvmInstance_getGiveItTome(Handle).MarshalAsString() ?? string.Empty;

		public string YouCanKeepTheCrap =>
			Native.ZkSvmInstance_getYouCanKeepTheCrap(Handle).MarshalAsString() ?? string.Empty;

		public string TheyKilledMyFriend =>
			Native.ZkSvmInstance_getTheyKilledMyFriend(Handle).MarshalAsString() ?? string.Empty;

		public string SuckerGotSome => Native.ZkSvmInstance_getSuckerGotSome(Handle).MarshalAsString() ?? string.Empty;

		public string SuckerDefeatedEbr =>
			Native.ZkSvmInstance_getSuckerDefeatedEbr(Handle).MarshalAsString() ?? string.Empty;

		public string SuckerDefeatedGur =>
			Native.ZkSvmInstance_getSuckerDefeatedGur(Handle).MarshalAsString() ?? string.Empty;

		public string SuckerDefeatedMage =>
			Native.ZkSvmInstance_getSuckerDefeatedMage(Handle).MarshalAsString() ?? string.Empty;

		public string SuckerDefeatedNovGuard =>
			Native.ZkSvmInstance_getSuckerDefeatedNovGuard(Handle).MarshalAsString() ?? string.Empty;

		public string SuckerDefeatedVlkGuard =>
			Native.ZkSvmInstance_getSuckerDefeatedVlkGuard(Handle).MarshalAsString() ?? string.Empty;

		public string YouDefeatedMyComrade =>
			Native.ZkSvmInstance_getYouDefeatedMyComrade(Handle).MarshalAsString() ?? string.Empty;

		public string YouDefeatedNovGuard =>
			Native.ZkSvmInstance_getYouDefeatedNovGuard(Handle).MarshalAsString() ?? string.Empty;

		public string YouDefeatedVlkGuard =>
			Native.ZkSvmInstance_getYouDefeatedVlkGuard(Handle).MarshalAsString() ?? string.Empty;

		public string YouStoleFromMe =>
			Native.ZkSvmInstance_getYouStoleFromMe(Handle).MarshalAsString() ?? string.Empty;

		public string YouStoleFromUs =>
			Native.ZkSvmInstance_getYouStoleFromUs(Handle).MarshalAsString() ?? string.Empty;

		public string YouStoleFromEbr =>
			Native.ZkSvmInstance_getYouStoleFromEbr(Handle).MarshalAsString() ?? string.Empty;

		public string YouStoleFromGur =>
			Native.ZkSvmInstance_getYouStoleFromGur(Handle).MarshalAsString() ?? string.Empty;

		public string StoleUromMage => Native.ZkSvmInstance_getStoleUromMage(Handle).MarshalAsString() ?? string.Empty;

		public string YouKilledmyfriend =>
			Native.ZkSvmInstance_getYouKilledmyfriend(Handle).MarshalAsString() ?? string.Empty;

		public string YouKilledEbr => Native.ZkSvmInstance_getYouKilledEbr(Handle).MarshalAsString() ?? string.Empty;
		public string YouKilledGur => Native.ZkSvmInstance_getYouKilledGur(Handle).MarshalAsString() ?? string.Empty;
		public string YouKilledMage => Native.ZkSvmInstance_getYouKilledMage(Handle).MarshalAsString() ?? string.Empty;

		public string YouKilledOcFolk =>
			Native.ZkSvmInstance_getYouKilledOcFolk(Handle).MarshalAsString() ?? string.Empty;

		public string YouKilledNcFolk =>
			Native.ZkSvmInstance_getYouKilledNcFolk(Handle).MarshalAsString() ?? string.Empty;

		public string YouKilledPsiFolk =>
			Native.ZkSvmInstance_getYouKilledPsiFolk(Handle).MarshalAsString() ?? string.Empty;

		public string GetThingsRight =>
			Native.ZkSvmInstance_getGetThingsRight(Handle).MarshalAsString() ?? string.Empty;

		public string YouDefeatedMeWell =>
			Native.ZkSvmInstance_getYouDefeatedMeWell(Handle).MarshalAsString() ?? string.Empty;

		public string Om => Native.ZkSvmInstance_getOm(Handle).MarshalAsString() ?? string.Empty;
	}
}