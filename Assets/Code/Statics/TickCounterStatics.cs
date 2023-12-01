using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static QuirkStatics;

public static class  TickCounterStatics
{
    public static void UpdateTick(TickCounter CallingTickCounter)
    {
        CallingTickCounter.TimeDiff += Time.deltaTime;

        if(CallingTickCounter.NewTick == true && CallingTickCounter.Active.Count == 0 && CallingTickCounter.TimeDiff > 1)
        {
            CallingTickCounter.TimeDiff = 0;

            CallingTickCounter.NewTick = false;

            CallingTickCounter.Tickfunction();
        }
    }

    public static void LoopThroughUnits(TickCounter CallingTickCounter)
    {
        foreach (GameObject character in CallingTickCounter.Units)
        {
            CharacterBehaviour characterScript = character.GetComponent<CharacterBehaviour>();

            if (characterScript.CharacterEntity.energy >= 60) {
                CallingTickCounter.Active.Add(characterScript);

                //Do the quirk scripts
                LoopThroughQuirks(characterScript);

                characterScript.CharacterEntity.energy -= 60;
            }

            characterScript.CharacterEntity.energy += characterScript.CharacterEntity.spd;
        }
    }

    public static void DefineMenusChildren(TickCounter CallingTickCounter)
    {
        Transform parentTransform = GameObject.Find("BattleUI").transform;

        // Loop through each child and add it to the list
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);
            CallingTickCounter.MenusChildren.Add(child);
        }
    }

    public static void DefineTickSelectBanners(TickCounter CallingTickCounter)
    {
        CallingTickCounter.SelectTargetBanner = GameObject.Find("SelectTargets");

        CallingTickCounter.SelectUnitBanner = GameObject.Find("SelectUnit");
    }
}