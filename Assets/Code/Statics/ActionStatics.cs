using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;

public static class ActionStatics
{
    public static string ChosenScene;
    public static List<GameObject> ChosenCharacters;
    public static List<string> ChosenCharacterStrings;

    public static Dictionary<string, string[]> ChosenSkills = new Dictionary<string, string[]>
        {
            { "Min",      new string[] {"Swivel","Investigate"} },
            { "Grungo",   new string[] {"Swivel","Investigate"} },
            { "Guinn",    new string[] {"Swivel","Investigate"} },
            { "Capri",    new string[] {"Swivel","Investigate"} },
            { "Freckle",  new string[] {"Swivel","Investigate"} },
            { "Freddy",   new string[] {"Swivel","Investigate"} },
            { "Orami",    new string[] {"Swivel","Investigate"} },
            { "RosyMary", new string[] {"Swivel","Investigate"} },
            { "Dough",    new string[] {"Swivel","Investigate"} },
            { "Tan",      new string[] {"Swivel","Investigate"} },
            { "Pygor",    new string[] {"Swivel","Investigate"} },
            { "Frogor",   new string[] {"Swivel","Investigate"} },
            { "Jazzy",    new string[] {"Swivel","Investigate"} },
            { "Cequeba",  new string[] {"Swivel","Investigate"} },
            { "Mick",     new string[] {"Swivel","Investigate"} },
            { "Poky",     new string[] {"Swivel","Investigate"} },
            { "Risleigh", new string[] {"Swivel","Investigate"} }
        };


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

    public static int DmgCalculation(Character UserBattler, Character TargetBattler) 
    {
        
        
        //Damage = (((ATK + ATK BOOST) x ATK MULTIPLIER) x LUCKY - DEF + DMG Buffs) x CRIT x STATUS MULTIPLIER x GUARD MULTIPLIER + FLAT DAMAGE/MINIMUM DAMAGE
        int DMG = (int)((((UserBattler.atk + UserBattler.atkboost) * UserBattler.atkmultiplier) * UserBattler.luckymultiplier - ((TargetBattler.def + TargetBattler.defboost) * TargetBattler.defmultiplier) + UserBattler.dmgboost) * UserBattler.critmultiplier * UserBattler.statusmultiplier * TargetBattler.guardmultiplier);

        if (DMG < TargetBattler.basedmg)
        {
            DMG = TargetBattler.basedmg;
        }

        if (TargetBattler.hp - DMG < 0)
        {
            DMG = TargetBattler.hp;
        }

        return DMG;
    }

    public static void Death(CharacterBehaviour CallingCharacterBehaviour) 
    {
        CallingCharacterBehaviour.CharacterEntity.quirks.ForEach(x => {if( (string) x[0] == "adrenalinekick") {CallingCharacterBehaviour.CharacterEntity.hp = 1; CallingCharacterBehaviour.CharacterEntity.quirks.Remove(x);}});

        if(CallingCharacterBehaviour.CharacterEntity.hp == 0)
        {
            CallingCharacterBehaviour.gameObject.SetActive(false);
        }
    }

    public static void BasicAttack(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        //Dit onder elke action
        CallingCharacterBehaviour.Actions.RemoveAt(0);

        CallingCharacterBehaviour.TargetsPerAction.RemoveAt(0);
    }
}