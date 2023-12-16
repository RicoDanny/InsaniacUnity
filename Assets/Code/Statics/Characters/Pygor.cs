using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;
using static QuirkStatics;
using static ModifierStatics;

public static class Pygor
{
    public static void Matchsticks(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Basicattakc alleen atk +1, inflict 1 ablaze voor 3 turns, if user = thrilled of manic, 2 ablaze
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        //inflict ablaze
        InflictQuirk(TargetBattler, new Quirk{name = "Ablaze", duration = 3, totalduration = 3});

        //extraatje voor de leuk als je thrilled of manic bent
        if(UserBattler.status == "Thrilled" || UserBattler.status == "Manic")
        {
            InflictQuirk(TargetBattler, new Quirk{name = "Ablaze", duration = 3, totalduration = 3});
        }

        EndAction(CallingCharacterBehaviour);
    }

    public static void LateForWork(CharacterBehaviour CallingCharacterBehaviour)
    {
        //basicattack alleen dan spd ipv atk, if user is thrilled oder manic
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        int tempatk = UserBattler.atk;

        UserBattler.atk = UserBattler.spd;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        //Reset
        UserBattler.atk = tempatk;

        //thrilled of manic shenanigans
        if(UserBattler.status == "Thrilled" || UserBattler.status == "Manic")
        {
            InflictQuirk(TargetBattler, new Quirk{name = "Ablaze", duration = 3, totalduration = 3});
        }

        EndAction(CallingCharacterBehaviour);
    }

    public static void RiskyManeuver(CharacterBehaviour CallingCharacterBehaviour)
    {
        //apply ablaze
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Meerdere targets dus foreach (voor elke target doe dit)
        foreach(CharacterBehaviour TargetBattlerCharacterBehaviour in TargetBattlerList)
        {
            Character TargetBattler = TargetBattlerCharacterBehaviour.CharacterEntity;

            TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);
        }

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