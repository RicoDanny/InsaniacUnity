using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;
using static QuirkStatics;

public static class LaVigneSuspecte
{
    public static void Grapeshot(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        UserBattler.atk += 1;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler, Skills["Grapeshot"]);

        UserBattler.atk -= 1;

        InflictQuirk(TargetBattler, new Quirk {name = "Paralyzed", totalduration = 2, duration = 2});

        EndAction(CallingCharacterBehaviour);
    }
}