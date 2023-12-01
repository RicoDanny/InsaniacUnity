using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;

public static class ActionStatics
{
    public static int GetNumberOfTargets(string Action)
    {
        Dictionary<string, int> NumberOfTargets = new Dictionary<string, int>
        {
            { "BasicAttack", 1 },
            { "WorkHarder", -3 },
            { "BodyCheck", 1 }
        };

        return NumberOfTargets[Action];
    }

    public static void ActionAccepted(CharacterBehaviour CallingCharacterBehaviour)
    {
        if(CallingCharacterBehaviour.User && CallingCharacterBehaviour.TickCounterObject.IsActionAccepted)
        {
            CallingCharacterBehaviour.User = false;

            CallingCharacterBehaviour.TickCounterObject.IsActionAccepted = false;

            CallingCharacterBehaviour.HandleAction();
        }
    }

    public static void DefineAI(CharacterBehaviour CallingCharacterBehaviour)
    {
        if (CallingCharacterBehaviour.gameObject.name != "Min")
        {
            for (int i = 0; i < 15; i++){
                CallingCharacterBehaviour.Actions.Add("BasicAttack");
                CallingCharacterBehaviour.TargetsPerAction.Add( new CharacterBehaviour[] {GameObject.Find("Min").GetComponent<CharacterBehaviour>()} );
            }
        }
    }

    public static bool CharacterActive(CharacterBehaviour CallingCharacterBehaviour)
    {
        bool IsCharacterActive = (CallingCharacterBehaviour.TickCounterObject.Active.Contains(CallingCharacterBehaviour) && CallingCharacterBehaviour.Actions.Count > 0);

        if(IsCharacterActive)
        {
            CallingCharacterBehaviour.CharacterEntity.turn++;
            CallingCharacterBehaviour.TickCounterObject.Active.Remove(CallingCharacterBehaviour);
        }

        return IsCharacterActive;
    }

    public static void InitiateTargeting(CharacterBehaviour CallingCharacterBehaviour, string ActionString)
    {
        CallingCharacterBehaviour.SelectTargetBanner.SetActive(!CallingCharacterBehaviour.SelectTargetBanner.activeSelf);

        CallingCharacterBehaviour.TargetingType = GetNumberOfTargets(ActionString);

        CallingCharacterBehaviour.TickCounterObject.Targets.Clear();

        CallingCharacterBehaviour.TickCounterObject.Targeting = true;

        CallingCharacterBehaviour.User = true;

        CallingCharacterBehaviour.TempActionString = ActionString;

        CallingCharacterBehaviour.TickCounterObject.MenusChildren.ForEach(p => p.gameObject.SetActive(false));

        CallingCharacterBehaviour.TickCounterObject.TargetAccept.SetActive(true);
    }

    public static void FreezeTargeting(CharacterBehaviour CallingCharacterBehaviour)
    {
        if(CallingCharacterBehaviour.TargetingType == -3) //Target jezelf
        {
            CallingCharacterBehaviour.TickCounterObject.Frozen = true;
            CallingCharacterBehaviour.TickCounterObject.Targets.Add(CallingCharacterBehaviour);
        }
        else if(CallingCharacterBehaviour.TargetingType == -2) //Target hele enemy team
        {
            CallingCharacterBehaviour.TickCounterObject.Frozen = true;

            Transform parentTransform = GameObject.Find("BadGuys").transform;

            for (int i = 0; i < parentTransform.childCount; i++)
            {
                CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                CallingCharacterBehaviour.TickCounterObject.Targets.Add(child);
            }
        }
        else if(CallingCharacterBehaviour.TargetingType == -1) // target hele friendly team
        {
            CallingCharacterBehaviour.TickCounterObject.Frozen = true;

            Transform parentTransform = GameObject.Find("GoodGuys").transform;

            for (int i = 0; i < parentTransform.childCount; i++)
            {
                CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                CallingCharacterBehaviour.TickCounterObject.Targets.Add(child);
            }
        }
        else if(CallingCharacterBehaviour.TargetingType == 0) // target iedereen cuz why not!!
        {
            CallingCharacterBehaviour.TickCounterObject.Frozen = true;

            Transform parentTransform = GameObject.Find("BadGuys").transform;

            for (int i = 0; i < parentTransform.childCount; i++)
            {
                CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                CallingCharacterBehaviour.TickCounterObject.Targets.Add(child);
            }

            parentTransform = GameObject.Find("GoodGuys").transform;

            for (int i = 0; i < parentTransform.childCount; i++)
            {
                CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                CallingCharacterBehaviour.TickCounterObject.Targets.Add(child);
            }
        }
    }

    public static bool TargetingChecker(CharacterBehaviour CallingCharacterBehaviour, int TargetingInt) 
    {
        if(TargetingInt > 0 && CallingCharacterBehaviour.TickCounterObject.Targets.Count == TargetingInt) // alles 1 of hoger is amount of targets 
        {
            return true;
        }

        return false;
    }

    public static void BasicAttack(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;


        //Dit per target (met meerdere targets gebruik foreach)
        int DMG = UserBattler.atk - TargetBattler.def;

        int BaseDMG = 1;

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