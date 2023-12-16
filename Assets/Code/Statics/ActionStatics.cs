using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Main statics
using static CharacterBehaviourStatics;
using static QuirkStatics;
using static ModifierStatics;

public static class ActionStatics
{
    public static string ChosenScene;
    public static string ChosenLetter;
    public static string ChosenStageType;
    public static int ChosenLetterAmount;
    public static int ChosenEXAmount = 0;
    public static List<GameObject> ChosenCharacters;
    public static List<string> ChosenCharacterStrings;

    [System.Serializable]
    public class Skill
    {
        public string name;
        public string displayName;
        public int targetNumber;
        public int requiredSP;
        public string description;
    }

    //-3: Target Self
    //-2: Target Enemy Team
    //-1: Target Ally Team
    //0: Target Everyone
    public static Dictionary<string, Skill> Skills = new Dictionary<string, Skill>
        {
            //ALLY SKILLS
            { "BasicAttack", new Skill {name = "BasicAttack", targetNumber = 1, requiredSP = 0} },

            //Min
            { "LookOverThere", new Skill {name = "LookOverThere", targetNumber = 1, requiredSP = 5, displayName = "Look over there!"} },
            { "HypeUp", new Skill {name = "HypeUp", targetNumber = 1, requiredSP = 3, displayName = "Hype up!"} },
            { "Speech", new Skill {name = "Speech", targetNumber = -1, requiredSP = 6, displayName = "Speech"} },
            { "Overwork", new Skill {name = "Overwork", targetNumber = 1, requiredSP = 5, displayName = "Overwork"} },

            //Grungo
            { "BodyCheck", new Skill {name = "BodyCheck", targetNumber = 1, requiredSP = 4, displayName = "Body check"} },
            { "Break", new Skill {name = "Break", targetNumber = 1, requiredSP = 5, displayName = "Break"} },
            { "ChainAttack", new Skill {name = "ChainAttack", targetNumber = -1, requiredSP = 10, displayName = "Chain-attack"} },
            { "Suffer", new Skill {name = "Suffer", targetNumber = 1, requiredSP = 3, displayName = "Suffer"} },

            //Pygor
            { "Matchsticks", new Skill {name = "Matchsticks", targetNumber = 1, requiredSP = 1, displayName = "Matchsticks"} },
            { "LateForWork", new Skill {name = "LateForWork", targetNumber = 1, requiredSP = 8, displayName = "Late for work"} },
            { "RiskyManeuver", new Skill {name = "RiskyManeuver", targetNumber = 2, requiredSP = 6, displayName = "Risky maneuver"} },
            { "LitOnFire", new Skill {name = "LitOnFire", targetNumber = -2, requiredSP = 6, displayName = "Lit on fire"} },
            { "FeastFlail", new Skill {name = "FeastFlail", targetNumber = -1, requiredSP = 4, displayName = "Feast flail"} },

            //Freddy
            { "RainOfPain", new Skill {name = "RainOfPain", targetNumber = 1, requiredSP = 7, displayName = "Rain of pain"} },
            { "FinancialThreat", new Skill {name = "FinancialThreat", targetNumber = 1, requiredSP = 2, displayName = "Financial threat"} },
            { "ArtificialDeflation", new Skill {name = "ArtificialDeflation", targetNumber = -2, requiredSP = 8, displayName = "Artificial deflation"} },
            { "TakeALoan", new Skill {name = "TakeALoan", targetNumber = -3, requiredSP = 0, displayName = "Take a loan"} },

            //ENEMY SKILLS
            //LaVigneSuspecte
            { "Grapeshot", new Skill {name = "Grapeshot", targetNumber = -2, requiredSP = 3} },

            //Fantolectrique
            { "ShockingNews", new Skill {name = "ShockingNews", targetNumber = 1, requiredSP = 4} },
        };

    public static Dictionary<string, List<Skill>> ChosenSkills = new Dictionary<string, List<Skill>>
        {
            { "Min",      new List<Skill>() {Skills["LookOverThere"], Skills["HypeUp"], Skills["Speech"], Skills["Overwork"]} },
            { "Grungo",   new List<Skill>() {Skills["BodyCheck"], Skills["Break"], Skills["ChainAttack"], Skills["Suffer"]} },
            { "Guinn",    new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Capri",    new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Freckle",  new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Freddy",   new List<Skill>() {Skills["RainOfPain"], Skills["FinancialThreat"], Skills["ArtificialDeflation"], Skills["TakeALoan"]} },
            { "Orami",    new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "RosyMary", new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Dough",    new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Tan",      new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Pygor",    new List<Skill>() {Skills["Matchsticks"], Skills["LateForWork"], Skills["RiskyManeuver"], Skills["LitOnFire"], Skills["FeastFlail"]} },
            { "Frogor",   new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Jazzy",    new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Cequeba",  new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Mick",     new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Poky",     new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} },
            { "Risleigh", new List<Skill>() {Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"], Skills["BasicAttack"]} }
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

    public static Dictionary<string, List<Skill>> EnemySkills = new Dictionary<string, List<Skill>> //Hier moet basicattack wel bij want hier boeien we niet om een of ander skilllist die apart staat van basicattack
        {
            { "LaVigneSuspecte",     new List<Skill>() {Skills["BasicAttack"], Skills["Grapeshot"]} },
            { "Fantolectrique",      new List<Skill>() {Skills["BasicAttack"], Skills["ShockingNews"]} },
            { "AngryOnlooker",      new List<Skill>() {Skills["BasicAttack"]} }
        };

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

        CallingCharacterBehaviour.TargetingType = Skills[ActionString].targetNumber;

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

        UserBattler.luckymultiplier = Mathf.Pow(1.5, ((int) (UserBattler.luckp/60)));
        
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

        UserBattler.luckp -= ((int) (UserBattler.luckp/60))*60;

        return DMG;
    }

    public static double StatusCalculation(Character AttickingCharacter, Character DefendingCharacter)
    {
        if(!(AttickingCharacter.status != null && DefendingCharacter.status != null)){return 1.0;} //If a status doesn't exist, it just returns 1 as multiplier

        return StatusChart[MatchupNum[AttickingCharacter.status], MatchupNum[DefendingCharacter.status]];
    }

    public static void Death(CharacterBehaviour CallingCharacterBehaviour) 
    {
        foreach(KeyValuePair<string, List<Quirk>> QuirkEntry in CallingCharacterBehaviour.CharacterEntity.quirks)
        {
            if( QuirkEntry.Key == "AdrenalineKick" && QuirkEntry.Value.Count > 0) 
            {
                CallingCharacterBehaviour.CharacterEntity.hp = 1; 
                QuirkEntry.Value.RemoveAt(0);
            }
        }

        if(CallingCharacterBehaviour.CharacterEntity.hp == 0)
        {
            if(CallingCharacterBehaviour.transform.parent.name == "Goodguys")
            {
                CallingCharacterBehaviour.TickCounterObject.AllyCount--;
            }
            else
            {
                CallingCharacterBehaviour.TickCounterObject.EnemyCount--;
            }

            CallingCharacterBehaviour.gameObject.SetActive(false);
            CallingCharacterBehaviour.TickCounterObject.Units.Remove(CallingCharacterBehaviour.gameObject);
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

            if (iq <= 50)
            {
                CallingCharacterBehaviour.Actions.Add("BasicAttack");
                
                CallingCharacterBehaviour.TargetsPerAction.Add( new CharacterBehaviour[] {CallingCharacterBehaviour.TickCounterObject.GoodGuysObject.transform.Cast<Transform>().Where(child => child.gameObject.activeSelf).Skip(Random.Range(0,ChosenCharacterStrings.Count)).FirstOrDefault()?.gameObject.GetComponent<CharacterBehaviour>()} );
            }
            if (iq > 50 && iq <= 100)
            {
                Skill ChosenSkill = EnemySkills[CallingCharacterBehaviour.name][Random.Range(0,EnemySkills[CallingCharacterBehaviour.name].Count)]; //not minus one because rondom range int is max-exclusive

                if(ChosenSkill.requiredSP > CallingCharacterBehaviour.CharacterEntity.sp)
                {
                    CallingCharacterBehaviour.Actions.Add("BasicAttack");
                }
                else
                {
                    CallingCharacterBehaviour.Actions.Add(ChosenSkill.name);
                }

                
                //-3: Target Self
                //-2: Target Enemy Team
                //-1: Target Ally Team
                //0: Target Everyone

                List<CharacterBehaviour> AITargets = new List<CharacterBehaviour>();

                if(ChosenSkill.targetNumber == -3)
                {
                    CallingCharacterBehaviour.TickCounterObject.Targets.Add(CallingCharacterBehaviour);
                }
                else if(ChosenSkill.targetNumber == -2)
                {
                    Transform parentTransform = GameObject.Find("GoodGuys").transform; //Enemy team van enemies is goodguys

                    for (int i = 0; i < parentTransform.childCount; i++)
                    {
                        CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                        AITargets.Add(child);
                    }
                }
                else if(ChosenSkill.targetNumber == -1)
                {
                    Transform parentTransform = GameObject.Find("BadGuys").transform; //Ally team van enemies is badguys

                    for (int i = 0; i < parentTransform.childCount; i++)
                    {
                        CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                        AITargets.Add(child);
                    }
                }
                else if(ChosenSkill.targetNumber == 0)
                {
                    Transform parentTransform = GameObject.Find("BadGuys").transform;

                    for (int i = 0; i < parentTransform.childCount; i++)
                    {
                        CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                        AITargets.Add(child);
                    }

                    parentTransform = GameObject.Find("GoodGuys").transform;

                    for (int i = 0; i < parentTransform.childCount; i++)
                    {
                        CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                        AITargets.Add(child);
                    }
                }
                else if(ChosenSkill.targetNumber == 1)
                {
                    AITargets.Add( CallingCharacterBehaviour.TickCounterObject.GoodGuysObject.transform.Cast<Transform>().Where(child => child.gameObject.activeSelf).Skip(Random.Range(0,ChosenCharacterStrings.Count)).FirstOrDefault()?.gameObject.GetComponent<CharacterBehaviour>() );
                }
                else if(ChosenSkill.targetNumber == 2)
                {
                    
                }
                //Als targetNumber 3 is, dan is het hele enemy team, dus -2, dus gebeurd niet bij enemy skills
            

                CallingCharacterBehaviour.TargetsPerAction.Add(AITargets.ToArray());
            }
        }
    }

    public static void InflictQuirk(Character InflictedCharacter, Quirk InflictingQuirk)
    {
        InflictedCharacter.quirks[InflictingQuirk.name].Add(InflictingQuirk);
    }

    public static void InflictModifier(Character InflictedCharacter, Modifier InflictingModifier)
    {
        InflictedCharacter.modifiers.Add(InflictingModifier);
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