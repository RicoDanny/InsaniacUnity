using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;
using static UiStatics;
using static QuirkStatics;
using static ModifierStatics;

public static class  EmoteStatics
{
    public static void Emote1(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.status = "Manic";

        EndAction(CallingCharacterBehaviour);
    }
}