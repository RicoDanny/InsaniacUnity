using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;
using static ModifierStatics;

public static class Min
{
    public static void LookOverThere(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Een basic die, defense van de enemy negeert

        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        int TargetBattlerDef = TargetBattler.def;

        TargetBattler.def = 0;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        TargetBattler.def += TargetBattlerDef;

        EndAction(CallingCharacterBehaviour);
    }

    public static void HypeUp(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Target thrilled maken en geef energy + 30

        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.energy += 30;
        TargetBattler.status = "Thrilled";

        EndAction(CallingCharacterBehaviour);
    }

    public static void Speech(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Iedereen krijgt dezelfde status alks min (user)

        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        foreach(CharacterBehaviour TargetBattlerCharacterBehaviour in TargetBattlerList)
        {
            Character TargetBattler = TargetBattlerCharacterBehaviour.CharacterEntity;

            TargetBattler.status = UserBattler.status;
        }

        EndAction(CallingCharacterBehaviour);
    }

    public static void Overwork(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Geef target attack + 2 voor 3 beurten en attack + 3 wanneer manic

        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        if(UserBattler.status == "Manic")
        {
            InflictModifier(TargetBattler, new Modifier{atkboost = 3});
        }
        else
        {
            InflictModifier(TargetBattler, new Modifier{atkboost = 2});
        }

        EndAction(CallingCharacterBehaviour);
    }
}