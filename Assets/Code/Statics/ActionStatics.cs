using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Main statics
using static CharacterBehaviourStatics;

public static class ActionStatics
{
    public static string ChosenScene;
    public static string ChosenLetter;
    public static string ChosenStageType;
    public static int ChosenLetterAmount;
    public static int ChosenEXAmount = 0;
    public static List<GameObject> ChosenCharacters;
    public static List<string> ChosenCharacterStrings;

    public static Dictionary<string, string[]> ChosenSkills = new Dictionary<string, string[]>
        {
            { "Min",      new string[] {"swivel","investigate"} },
            { "Grungo",   new string[] {"swivel","investigate"} },
            { "Guinn",    new string[] {"swivel","investigate"} },
            { "Capri",    new string[] {"swivel","investigate"} },
            { "Freckle",  new string[] {"swivel","investigate"} },
            { "Freddy",   new string[] {"swivel","investigate"} },
            { "Orami",    new string[] {"swivel","investigate"} },
            { "RosyMary", new string[] {"swivel","investigate"} },
            { "Dough",    new string[] {"swivel","investigate"} },
            { "Tan",      new string[] {"swivel","investigate"} },
            { "Pygor",    new string[] {"swivel","investigate"} },
            { "Frogor",   new string[] {"swivel","investigate"} },
            { "Jazzy",    new string[] {"swivel","investigate"} },
            { "Cequeba",  new string[] {"swivel","investigate"} },
            { "Mick",     new string[] {"swivel","investigate"} },
            { "Poky",     new string[] {"swivel","investigate"} },
            { "Risleigh", new string[] {"swivel","investigate"} }
        };

    public static Dictionary<string, int> MatchupNum = new Dictionary<string, int>
        {
            { "empty",      0 },
            { "thrilled",   1 },
            { "paranoid",   2 },
            { "delusional", 3 },
            { "vexed",      4 },
            { "depressed",  5 },
            { "manic",      6 },
            { "hysterical", 7 },
            { "psycho",     8 },
            { "throthing",  9 },
            { "bored",      10 }
        };

    public static double[,] StatusChart = 
        {
            {1.1, 0.9, 1.1, 1.0, 0.9,    1.2, 0.8, 1.2, 1.0, 0.8, 0.9},
            {1.1, 1.0, 0.9, 0.9, 1.1,    1.2, 1.0, 0.8, 0.8, 1.2, 0.9},
            {1.0, 1.1, 0.9, 0.9, 1.1,    1.0, 1.2, 0.8, 0.8, 1.2, 0.9},
            {0.9, 1.1, 1.9, 1.0, 1.1,    0.8, 1.2, 0.8, 1.0, 1.2, 0.9},
            {0.9, 0.9, 1.1, 1.1, 1.0,    0.8, 0.8, 1.2, 1.2, 1.0, 0.9},

            {1.2, 0.8, 1.2, 1.0, 0.8,    1.3, 0.7, 1.3, 1.0, 0.7, 0.9},
            {1.2, 1.0, 0.8, 0.8, 1.2,    1.3, 1.0, 0.7, 0.7, 1.3, 0.9},
            {1.0, 1.2, 0.8, 0.8, 1.2,    1.0, 1.3, 0.7, 0.7, 1.3, 0.9},
            {0.8, 1.2, 0.8, 1.0, 1.2,    0.7, 1.3, 0.7, 1.0, 1.3, 0.9},
            {0.8, 0.8, 1.2, 1.2, 1.0,    0.7, 0.7, 1.3, 1.3, 1.0, 0.9},
            {0.8, 0.8, 0.8, 0.8, 0.8,    0.8, 0.8, 0.8, 0.8, 0.8, 0.9}
        };

    public static int GetNumberOfTargets(string Action)
    {
        //-3 = Target jezelf
        //-2 = Target Enemy Team
        //-1 = Target Ally Team
        //0 = Target Iedereen
        //>0 = Aantal targets

        Dictionary<string, int> NumberOfTargets = new Dictionary<string, int>
        {
            { "BasicAttack", 1 },
            { "WorkHarder", -3 },
            { "BodyCheck", 1 },
            { "Swivel", -3 }
        };

        return NumberOfTargets.ContainsKey(Action) ? NumberOfTargets[Action] : -4;
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
        else
        {
            CallingCharacterBehaviour.TickCounterObject.Frozen = false;
        }
    }

    public static bool TargetingChecker(CharacterBehaviour CallingCharacterBehaviour, int TargetingInt) 
    {
        if(TargetingInt > 0 && CallingCharacterBehaviour.TickCounterObject.Targets.Count == TargetingInt) // alles 1 of hoger is amount of targets 
        {
            return true;
        }
        else if(TargetingInt <= 0)
        {
            return true;
        }

        return false;
    }

    public static int DmgCalculation(Character UserBattler, Character TargetBattler) 
    {
        UserBattler.statusmultiplier = StatusCalculation(UserBattler, TargetBattler);
        
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

    public static double StatusCalculation(Character AttickingCharacter, Character DefendingCharacter)
    {
        if(!(AttickingCharacter.status != null && DefendingCharacter.status != null)){return 1.0;} //If a status doesn't exist, it just returns 1 as multiplier

        return StatusChart[MatchupNum[AttickingCharacter.status], MatchupNum[DefendingCharacter.status]];
    }

    public static void Death(CharacterBehaviour CallingCharacterBehaviour) 
    {
        CallingCharacterBehaviour.CharacterEntity.quirks.ForEach(x => {if( (string) x.name == "adrenalinekick") {CallingCharacterBehaviour.CharacterEntity.hp = 1; CallingCharacterBehaviour.CharacterEntity.quirks.Remove(x);}});

        if(CallingCharacterBehaviour.CharacterEntity.hp == 0)
        {
            CallingCharacterBehaviour.gameObject.SetActive(false);
        }
    }

    public static void EndAction(CharacterBehaviour CallingCharacterBehaviour)
    {
        Debug.Log(CallingCharacterBehaviour.name + " performed " + CallingCharacterBehaviour.Actions[0] + " on " +  CallingCharacterBehaviour.TargetsPerAction[0][0].name);
        CallingCharacterBehaviour.Actions.RemoveAt(0);
        CallingCharacterBehaviour.TargetsPerAction.RemoveAt(0);
    }

    public static void AImove(CharacterBehaviour CallingCharacterBehaviour)
    {
        if(CallingCharacterBehaviour.TickCounterObject.Active.Contains(CallingCharacterBehaviour))
        {
            int iq = CallingCharacterBehaviour.CharacterEntity.iq;

            if (iq <= 100)
            {
                CallingCharacterBehaviour.Actions.Add("BasicAttack");
                
                CallingCharacterBehaviour.TargetsPerAction.Add( new CharacterBehaviour[] {CallingCharacterBehaviour.TickCounterObject.GoodGuysObject.transform.Cast<Transform>().Where(child => child.gameObject.activeSelf).Skip(Random.Range(0,ChosenCharacterStrings.Count)).FirstOrDefault()?.gameObject.GetComponent<CharacterBehaviour>()} );
            }
        }
    }

    public static void BasicAttack(CharacterBehaviour CallingCharacterBehaviour)
    {
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[0];
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        TargetBattler.hp -= DmgCalculation(UserBattler, TargetBattler);

        EndAction(CallingCharacterBehaviour);
    }
}