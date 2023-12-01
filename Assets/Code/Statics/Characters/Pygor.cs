using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;

public static class Pygor
{
    public static void WorkHarder(CharacterBehaviour CallingCharacterBehaviour)
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