using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;
using static QuirkStatics;
using static ModifierStatics;

public static class Grungo
{
    public static void BodyCheck(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Helft van defense buffs negeren, 
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat het 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        //Negeer helft van def
        TargetBattler.defboost /= 2;

        //In case temp critmult.
        float tempcritmultiplier = UserBattler.critmultiplier;

        //Als enemy meer dan helft van hp dan critmult. keer 1.5
        if(TargetBattler.hp > TargetBattler.maxhp/2)
        {
            UserBattler.critmultiplier *= 1.5f;
        }

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler, Skills["BodyCheck"]);

        //reset stats
        UserBattler.critmultiplier = tempcritmultiplier;
        TargetBattler.defboost *= 2;

        EndAction(CallingCharacterBehaviour);
    }

    public static void Break(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Apply defenseless on one target for 3 turn, if status = vexed/throthing: attack

        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat het maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        InflictQuirk(TargetBattler, new Quirk{name = "Defenseless", duration = 3, totalduration = 3});

        if(TargetBattler.status == "Vexed" || TargetBattler.status == "Throthing")
        {
            //BasicAttack
            TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler, Skills["Break"]);
        }

        EndAction(CallingCharacterBehaviour);
    }

    public static void ChainAttack(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Geeft hele team 30 energy
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Meerdere targets dus foreach (voor elke target doe dit)
        foreach(CharacterBehaviour TargetBattlerCharacterBehaviour in TargetBattlerList)
        {
            Character TargetBattler = TargetBattlerCharacterBehaviour.CharacterEntity;

            TargetBattler.energy += 30;
        }

        EndAction(CallingCharacterBehaviour);
    }

    public static void Suffer(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Basicattack on target, daaarna give target bleeding, en sp cost omhoog met 2
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat het maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        //Basicattack
        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler, Skills["Suffer"]);

        //Inflict bleeding
        InflictQuirk(TargetBattler, new Quirk{name = "Bleeding", duration = -1, totalduration = -1});

        Skills["Suffer"].requiredSP += 2;

        EndAction(CallingCharacterBehaviour);
    }
}