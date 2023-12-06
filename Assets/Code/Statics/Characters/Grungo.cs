using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;

public static class Grungo
{
    public static void BodyCheck(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;


        //Dit per target (met meerdere targets gebruik foreach)
        int DMG = (int)(UserBattler.atk - TargetBattler.def * 0.5);
        int spcost = 4 - UserBattler.loweredspcost;
        int BaseDMG = 1;

        if (TargetBattler.hp > TargetBattler.maxhp * 0.5)
        {
            //double CRIT = 1.5;
        }

        if (DMG < 1)
        {
            DMG = BaseDMG;
        }

        if (TargetBattler.hp - DMG < 0)
        {
            DMG = TargetBattler.hp;
        }

        TargetBattler.hp -= DMG;

        //Dit onder elke action
        CallingCharacterBehaviour.Actions.RemoveAt(0);

        CallingCharacterBehaviour.TargetsPerAction.RemoveAt(0);
    }
}