using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;

public static class Pygor
{
    public static void Matchsticks(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        EndAction(CallingCharacterBehaviour);
    }

    public static void LateForWork(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        EndAction(CallingCharacterBehaviour);
    }

    public static void RiskyManeuver(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        EndAction(CallingCharacterBehaviour);
    }

    public static void LitOnFire(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        EndAction(CallingCharacterBehaviour);
    }

    public static void FeastFlail(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        EndAction(CallingCharacterBehaviour);
    }
}