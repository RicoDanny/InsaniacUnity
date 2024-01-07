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
        public string type;
        public int cooldown = 0;
        public int startup = 0;
        public string description;
        int hitcount = 1; //multihit basically
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
            { "LookOverThere", new Skill {name = "LookOverThere", targetNumber = 1, requiredSP = 5, type = "Rushdown", displayName = "Look over there!"} },
            { "HypeUp", new Skill {name = "HypeUp", targetNumber = 1, requiredSP = 3, type = "Rushdown", displayName = "Hype up!"} },
            { "Speech", new Skill {name = "Speech", targetNumber = -1, requiredSP = 6, type = "Rushdown", displayName = "Speech"} },
            { "Overwork", new Skill {name = "Overwork", targetNumber = 1, requiredSP = 5, type = "Rushdown", displayName = "Overwork"} },

            //Grungo
            { "BodyCheck", new Skill {name = "BodyCheck", targetNumber = 1, requiredSP = 4, type = "Rushdown", displayName = "Body check"} },
            { "Break", new Skill {name = "Break", targetNumber = 1, requiredSP = 5, type = "Rushdown", displayName = "Break"} },
            { "ChainAttack", new Skill {name = "ChainAttack", targetNumber = -1, requiredSP = 10, type = "Rushdown", displayName = "Chain-attack"} },
            { "Suffer", new Skill {name = "Suffer", targetNumber = 1, requiredSP = 3, type = "Rushdown", displayName = "Suffer"} },

            //Pygor
            { "Matchsticks", new Skill {name = "Matchsticks", targetNumber = 1, requiredSP = 1, type = "Rushdown", displayName = "Matchsticks"} },
            { "LateForWork", new Skill {name = "LateForWork", targetNumber = 1, requiredSP = 8, type = "Rushdown", displayName = "Late for work"} },
            { "RiskyManeuver", new Skill {name = "RiskyManeuver", targetNumber = 2, requiredSP = 6, type = "Rushdown", displayName = "Risky maneuver"} },
            { "LitOnFire", new Skill {name = "LitOnFire", targetNumber = -2, requiredSP = 6, type = "Rushdown", displayName = "Lit on fire"} },
            { "FeastFlail", new Skill {name = "FeastFlail", targetNumber = -1, requiredSP = 4, type = "Rushdown", displayName = "Feast flail"} },

            //Freddy
            { "RainOfPain", new Skill {name = "RainOfPain", targetNumber = 1, requiredSP = 7, type = "Rushdown", displayName = "Rain of pain"} },
            { "FinancialThreat", new Skill {name = "FinancialThreat", targetNumber = 1, requiredSP = 2, type = "Rushdown", displayName = "Financial threat"} },
            { "ArtificialDeflation", new Skill {name = "ArtificialDeflation", targetNumber = -2, requiredSP = 8, type = "Rushdown", displayName = "Artificial deflation"} },
            { "TakeALoan", new Skill {name = "TakeALoan", targetNumber = -3, requiredSP = 0, type = "Rushdown", displayName = "Take a loan"} },

            //ENEMY SKILLS
            //LaVigneSuspecte
            { "Grapeshot", new Skill {name = "Grapeshot", targetNumber = -2, requiredSP = 3, type = "Rushdown"} },

            //Fantolectrique
            { "ShockingNews", new Skill {name = "ShockingNews", targetNumber = 1, requiredSP = 4, type = "Rushdown"} },

            //EMOTES
            { "Giggle", new Skill {name = "Giggle", targetNumber = 1, requiredSP = 3, type = "Emote", displayName = "Giggle"} },
            { "Complain", new Skill {name = "Complain", targetNumber = 1, requiredSP = 3, type = "Emote", displayName = "Complain"} },
            { "Whisper", new Skill {name = "Whisper", targetNumber = 1, requiredSP = 3, type = "Emote", displayName = "Whisper"} },
            { "Wallow", new Skill {name = "Wallow", targetNumber = 1, requiredSP = 3, type = "Emote", displayName = "Wallow"} },
            { "Delude", new Skill {name = "Delude", targetNumber = 1, requiredSP = 3, type = "Emote", displayName = "Delude"} },
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

    public static List<Skill> Emotes = new List<Skill>
        {
            Skills["Giggle"],
            Skills["Complain"],
            Skills["Whisper"],
            Skills["Wallow"],
            Skills["Delude"],
        };

    public static Dictionary<string, int> MatchupNum = new Dictionary<string, int>
        {
            { "Empty",      0 },
            { "Thrilled",   1 },
            { "Paranoid",   2 },
            { "Delusional", 3 },
            { "Vexed",      4 },
            { "Depressed",  5 },
            { "Manic",      6 },
            { "Hysterical", 7 },
            { "Psycho",     8 },
            { "Throthing",  9 },
            { "Bored",      10 }
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
            CallingCharacterBehaviour.TickCounterObject.Active.Remove(CallingCharacterBehaviour);
        }

        return IsCharacterActive;
    }

    public static void InitiateTargeting(CharacterBehaviour CallingCharacterBehaviour, string ActionString)
    {
        CallingCharacterBehaviour.TickCounterObject.SelectTargetBanner.SetActive(!CallingCharacterBehaviour.TickCounterObject.SelectTargetBanner.activeSelf);

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

    public static int DmgCalculation(Character UserBattler, Character TargetBattler, Skill UsingSkill) 
    {
        UserBattler.statusmultiplier = StatusCalculation(UserBattler, TargetBattler);

        UserBattler.luckymultiplier = (float) Mathf.Pow(1.5f, (float) ((int) (UserBattler.luckp/60)));

        UserBattler.critmultiplier = (float) Mathf.Pow(1.5f, (float) ((int) (UserBattler.critp/60)));
        
        //Damage = (((ATK + ATK BOOST) x ATK MULTIPLIER) x LUCKY - DEF + DMG Buffs) x CRIT x STATUS MULTIPLIER x GUARD MULTIPLIER + FLAT DAMAGE/MINIMUM DAMAGE
        int DMG = (int) Mathf.Round((float) ((((UserBattler.atk + UserBattler.atkboost) * UserBattler.atkmultiplier) * UserBattler.luckymultiplier - ((TargetBattler.def + TargetBattler.defboost) * TargetBattler.defmultiplier) + UserBattler.dmgboost) * UserBattler.critmultiplier * UserBattler.statusmultiplier * TargetBattler.guardmultiplier));

        if(DMG > 99)
        {
            DMG = 99;
        }

        if(DMG < TargetBattler.basedmg)
        {
            DMG = TargetBattler.basedmg;
        }

        if(TargetBattler.hp - DMG < 0)
        {
            DMG = TargetBattler.hp;
        }

        if(TargetBattler.avop >= 60)
        {
            DMG = 0;
        }

        UserBattler.luckp -= ((int) (UserBattler.luckp/60))*60;

        UserBattler.critp -= ((int) (UserBattler.critp/60))*60;

        if(DMG > 0)
        {
            UserBattler.luckp += UserBattler.luck * UserBattler.luckpmultiplier;

            UserBattler.critp += UserBattler.crit * UserBattler.critpmultiplier;

            TargetBattler.avop += TargetBattler.avo * TargetBattler.avopmultiplier;
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
            if(CallingCharacterBehaviour.transform.parent.name == "GoodGuys")
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

        CallingCharacterBehaviour.CharacterEntity.sp -= Skills[CallingCharacterBehaviour.Actions[0]].requiredSP;
    }

    public static void DisplayDamage(Character TargetCharacter, int DamageDone)
    {
        TargetCharacter.ParentCharacterBehaviour.DamageTakenText.text = "-" + DamageDone.ToString();
        TargetCharacter.ParentCharacterBehaviour.DamageTakenText.color = new Color(TargetCharacter.ParentCharacterBehaviour.DamageTakenText.color.r, TargetCharacter.ParentCharacterBehaviour.DamageTakenText.color.g, TargetCharacter.ParentCharacterBehaviour.DamageTakenText.color.b, 255);
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
                    AITargets.Add( CallingCharacterBehaviour.TickCounterObject.GoodGuysObject.transform.Cast<Transform>().Where(child => child.gameObject.activeSelf).Skip(Random.Range(0,ChosenCharacterStrings.Count)).FirstOrDefault()?.gameObject.GetComponent<CharacterBehaviour>() );
                    
                    AITargets.Add( CallingCharacterBehaviour.TickCounterObject.GoodGuysObject.transform.Cast<Transform>().Where(child => child.gameObject.activeSelf && !AITargets.Contains(child.GetComponent<CharacterBehaviour>())).Skip(Random.Range(0,ChosenCharacterStrings.Count)).FirstOrDefault()?.gameObject.GetComponent<CharacterBehaviour>() );
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
        Character UserBattler = CallingCharacterBehaviour.CharacterEntity;
        CharacterBehaviour[] TargetBattlerList = CallingCharacterBehaviour.TargetsPerAction[UserBattler.turn];

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;

        int DMG = DmgCalculation(UserBattler, TargetBattler, Skills["BasicAttack"]);

        TargetBattler.hp -= DMG;

        DisplayDamage(TargetBattler, DMG);

        EndAction(CallingCharacterBehaviour);
    }
}