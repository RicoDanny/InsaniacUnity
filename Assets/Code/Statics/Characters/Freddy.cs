using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;

public static class Freddy
{
    public static void RainOfPain(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        //Dit per target (met meerdere targets gebruik foreach)
        UserBattler.atk += 1;
        UserBattler.luck += 2;
        UserBattler.maxsp += 10;
        int spcost = 6 - UserBattler.loweredspcost;

        //Dit onder elke action
        CallingCharacterBehaviour.Actions.RemoveAt(0);

        CallingCharacterBehaviour.TargetsPerAction.RemoveAt(0);
    }

    public static void FinancialThreat(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        //Dit per target (met meerdere targets gebruik foreach)
        UserBattler.atk += 1;
        UserBattler.luck += 2;
        UserBattler.maxsp += 10;
        int spcost = 6 - UserBattler.loweredspcost;

        //Dit onder elke action
        CallingCharacterBehaviour.Actions.RemoveAt(0);

        CallingCharacterBehaviour.TargetsPerAction.RemoveAt(0);
    }

    public static void ArtificialDeflation(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        //Dit per target (met meerdere targets gebruik foreach)
        UserBattler.atk += 1;
        UserBattler.luck += 2;
        UserBattler.maxsp += 10;
        int spcost = 6 - UserBattler.loweredspcost;

        //Dit onder elke action
        CallingCharacterBehaviour.Actions.RemoveAt(0);

        CallingCharacterBehaviour.TargetsPerAction.RemoveAt(0);
    }

    public static void TakeALoan(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        //Dit per target (met meerdere targets gebruik foreach)
        UserBattler.atk += 1;
        UserBattler.luck += 2;
        UserBattler.maxsp += 10;
        int spcost = 6 - UserBattler.loweredspcost;

        //Dit onder elke action
        CallingCharacterBehaviour.Actions.RemoveAt(0);

        CallingCharacterBehaviour.TargetsPerAction.RemoveAt(0);
    }
}