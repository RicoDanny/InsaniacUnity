using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ActionStatics;
using static QuirkStatics;
using static ModifierStatics;

public static class CharacterBehaviourStatics
{
    [System.Serializable]
    public class Character
    {
        public string Name;
        public int hp;
        public int maxhp;
        public int sp;
        public int maxsp;
        public int atk;
        public int def;
        public int spd;
        public int energy;
        public int hit;
        public int avo;
        public int avop;
        public int luck;
        public int luckp;
        public int crit;
        public int critp;
        public int iq = 100;
        public int basedmg = 1;
        public int atkboost = 0;
        public int defboost = 0;
        public int dmgboost = 0;
        public float atkmultiplier = 1.0f;
        public float defmultiplier = 1.0f;
        public float luckymultiplier = 1.0f;
        public float critmultiplier = 1.0f;
        public float guardmultiplier = 1.0f;
        public int loweredspcost = 0;
        public int turn = 0;
        public string status;
        public double statusmultiplier = 1.0f;

        //quirk dict because of that fcking stack bs (calmest flint comment)
        public Dictionary<string, List<Quirk>> quirks = new Dictionary<string, List<Quirk>>
        {
            { "Bleeding",    new List<Quirk>() },
            { "Hyperthermic",    new List<Quirk>() },
            { "Hypothermic",    new List<Quirk>() },
            { "Poisoned",    new List<Quirk>() },
            { "Acidified",    new List<Quirk>() },
            { "Decay",    new List<Quirk>() },
            { "Brittle",    new List<Quirk>() },
            { "Fried",    new List<Quirk>() },
            { "Starving",    new List<Quirk>() },
            { "Haunted",    new List<Quirk>() },
            { "Frostbite",    new List<Quirk>() },
            { "Malaise",    new List<Quirk>() },
            { "Virus",    new List<Quirk>() },
            { "Vulnerable",    new List<Quirk>() },
            { "Pierced",    new List<Quirk>() },

            { "Nauseated",    new List<Quirk>() },
            { "Paralyzed",    new List<Quirk>() },
            { "Plasma",    new List<Quirk>() },
            { "Confused",    new List<Quirk>() },
            { "Trembling",    new List<Quirk>() },
            { "TuckeredOut",    new List<Quirk>() },
            { "Tetanus",    new List<Quirk>() },
            { "Dizzy",    new List<Quirk>() },
            { "DeathListed",    new List<Quirk>() },
            { "Soaked",    new List<Quirk>() },
            { "Pacified",    new List<Quirk>() },
            { "Ablaze",    new List<Quirk>() },
            { "BlueFlame",    new List<Quirk>() },
            { "Reported",    new List<Quirk>() },
            { "Analyzed",    new List<Quirk>() },
            { "Defenseless",    new List<Quirk>() },
            { "Weakened",    new List<Quirk>() },
            { "Static",    new List<Quirk>() },
            { "Toppled",    new List<Quirk>() },
            { "Shattered",    new List<Quirk>() },
            { "Concussed",    new List<Quirk>() },

            { "PowerHungry",    new List<Quirk>() },
            { "Ballistic",    new List<Quirk>() },
            { "Forgetful",    new List<Quirk>() },
            { "Damaged",    new List<Quirk>() },
            { "Blind",    new List<Quirk>() },
            { "Tinnitus",    new List<Quirk>() },
            { "Intoxicated",    new List<Quirk>() },
            { "Frenzy",    new List<Quirk>() },
            { "Horrified",    new List<Quirk>() },

            { "Vampiric",    new List<Quirk>() },
            { "Sapper",    new List<Quirk>() },
            { "Hotheaded",    new List<Quirk>() },
            { "ColdFingers",    new List<Quirk>() },
            { "HPRegen",    new List<Quirk>() },
            { "SPRegen",    new List<Quirk>() },
            { "Munchies",    new List<Quirk>() },
            { "Armored",    new List<Quirk>() },
            { "Protected",    new List<Quirk>() },
            { "Invincible",    new List<Quirk>() },
            { "AdrenalineKick",    new List<Quirk>() },
            { "Invisible",    new List<Quirk>() },
            { "Invigorated",    new List<Quirk>() },
            { "Ambrosia",    new List<Quirk>() },
            { "SlushedUp",    new List<Quirk>() },
            { "Reckless",    new List<Quirk>() },
            { "VitalPower",    new List<Quirk>() },
            { "VitalWall",    new List<Quirk>() },
            { "VitalSpeed",    new List<Quirk>() },
            { "VitalSenses",    new List<Quirk>() },
            { "ChillingFigure",    new List<Quirk>() },
            { "CuttingPresence",    new List<Quirk>() },
            { "Pumped",    new List<Quirk>() },
            { "SleepWalking",    new List<Quirk>() },
            { "SturdyMuscle",    new List<Quirk>() },
            { "SturdyNerves",    new List<Quirk>() },
            { "SturdyWits",    new List<Quirk>() },
            { "SturdyReflex",    new List<Quirk>() },
            { "StrikeReady",    new List<Quirk>() },
            { "Hype",    new List<Quirk>() },
            { "Blessed",    new List<Quirk>() },
            { "Hallowed",    new List<Quirk>() },
            { "Oblivious",    new List<Quirk>() },
            { "Intensity",    new List<Quirk>() },

            { "Aggravated",    new List<Quirk>() },
            { "Shareholder",    new List<Quirk>() },
            { "Manipulable",    new List<Quirk>() },
            { "Succubus",    new List<Quirk>() },
            { "Linked",    new List<Quirk>() },

            { "SubstanceReliant",    new List<Quirk>() },
            { "Insomniac",    new List<Quirk>() },
            { "Hoarding",    new List<Quirk>() },
            { "FlippedOut",    new List<Quirk>() },
            { "Dazed",    new List<Quirk>() },

            { "CoStar",    new List<Quirk>() },
            { "Star",    new List<Quirk>() },
            { "Cursed",    new List<Quirk>() },
            { "Watt",    new List<Quirk>() },
            { "Data",    new List<Quirk>() },
            { "TechSavvy",    new List<Quirk>() },
            { "Trojanned",    new List<Quirk>() },
            { "Transfer",    new List<Quirk>() },
            { "Split",    new List<Quirk>() },
            { "Hooked",    new List<Quirk>() },
            { "Lined",    new List<Quirk>() },
            { "Charged",    new List<Quirk>() },
            { "Boned",    new List<Quirk>() },

            { "Sandswept",    new List<Quirk>() },
            { "LightningRod",    new List<Quirk>() },
            { "Avalanched",    new List<Quirk>() },
            { "Decompose",    new List<Quirk>() },
            { "Dehydrated",    new List<Quirk>() }
        };

        public List<Modifier> modifiers = new List<Modifier>();
    }

    [System.Serializable]
    public class CharacterList
    {
        public Character[] character;
    }


    public static void DefineCharacter(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterEntity.turn = 0;

        CallingCharacterBehaviour.myCharacterList = JsonUtility.FromJson<CharacterList>(CallingCharacterBehaviour.CharactersJSON.text);
        
        CallingCharacterBehaviour.HighlightObject = CallingCharacterBehaviour.gameObject.transform.Find("Highlight").gameObject;

        CallingCharacterBehaviour.TickCounterObject = GameObject.Find("MainCanvas").GetComponent<TickCounter>();

        CallingCharacterBehaviour.HighlightRenderer = CallingCharacterBehaviour.HighlightObject.GetComponent<SpriteRenderer>();

        CallingCharacterBehaviour.Actions = new List<string>();

        CallingCharacterBehaviour.TargetsPerAction = new List<CharacterBehaviour[]>();
        
        foreach(Character JsonCharacter in CallingCharacterBehaviour.myCharacterList.character) 
        {
            if(JsonCharacter.Name == CallingCharacterBehaviour.name) 
            {
                CallingCharacterBehaviour.CharacterEntity = JsonCharacter;
            }
        }
    }

    public static void DefineHighlights(CharacterBehaviour CallingCharacterBehaviour)
    {
        foreach(Transform CharacterChild in CallingCharacterBehaviour.transform)
        {
            if(CharacterChild.name == CallingCharacterBehaviour.name + "HP"){CallingCharacterBehaviour.CharacterHPText = CharacterChild.gameObject;}
            if(CharacterChild.name == "Highlight"){CallingCharacterBehaviour.HighlightObject = CharacterChild.gameObject; CallingCharacterBehaviour.HighlightRenderer = CharacterChild.GetComponent<SpriteRenderer>();}
            if(CharacterChild.name == "ActiveHighlight"){CallingCharacterBehaviour.ActiveHighlightRenderer = CharacterChild.GetComponent<SpriteRenderer>();}
        }
    }

    public static void SpawnGoodGuyMenu(CharacterBehaviour CallingCharacterBehaviour)
    {
        GameObject Menu = GameObject.Instantiate(CallingCharacterBehaviour.MenuPrefab, new Vector3(0,0,0), Quaternion.identity);

        Menu.name = CallingCharacterBehaviour.name + "Menu";

        Menu.transform.SetParent(CallingCharacterBehaviour.TickCounterObject.BattleUIObject.transform, false);

        foreach(Transform ThingToAssign in Menu.transform)
        {
            if(ThingToAssign.name == "StatInformation")
            {
                foreach (Transform StatToAssign in ThingToAssign)
                {
                    if(StatToAssign.name == "SP"){CallingCharacterBehaviour.CharacterSPText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "ATK"){CallingCharacterBehaviour.CharacterATKText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "DEF"){CallingCharacterBehaviour.CharacterDEFText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "SPD"){CallingCharacterBehaviour.CharacterSPDText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "HIT"){CallingCharacterBehaviour.CharacterHITText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "AVO"){CallingCharacterBehaviour.CharacterAVOText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "LUCK"){CallingCharacterBehaviour.CharacterLUCKText = StatToAssign.gameObject;}
                }
            }
            else if(ThingToAssign.name == "ClockHand")
            {
                CallingCharacterBehaviour.ClockHand = ThingToAssign.gameObject;
            }

            if(ThingToAssign.name == "Weapon"){ThingToAssign.GetComponent<Button>().onClick.AddListener(() => CallingCharacterBehaviour.HandleTargeting("BasicAttack"));}

            if(ThingToAssign.name == "EmoteList"){CallingCharacterBehaviour.EmoteList = ThingToAssign.gameObject;}
            if(ThingToAssign.name == "SkillList"){CallingCharacterBehaviour.SkillList = ThingToAssign.gameObject;}
        }

        CallingCharacterBehaviour.GetComponent<Button>().onClick.AddListener(() => CallingCharacterBehaviour.transform.parent.parent.GetComponent<ToggleActive>().ToggleObjectActive(Menu));
    }

    public static void SpawnBadGuyMenu(CharacterBehaviour CallingCharacterBehaviour)
    {
        GameObject Menu = GameObject.Instantiate(CallingCharacterBehaviour.MenuPrefab, new Vector3(0,0,0), Quaternion.identity);

        Menu.name = CallingCharacterBehaviour.name + "Menu";

        Menu.transform.SetParent(CallingCharacterBehaviour.TickCounterObject.BattleUIObject.transform, false);

        foreach(Transform ThingToAssign in Menu.transform)
        {
            if(ThingToAssign.name == "StatInformation")
            {
                foreach (Transform StatToAssign in ThingToAssign)
                {
                    if(StatToAssign.name == "SP"){CallingCharacterBehaviour.CharacterSPText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "ATK"){CallingCharacterBehaviour.CharacterATKText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "DEF"){CallingCharacterBehaviour.CharacterDEFText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "SPD"){CallingCharacterBehaviour.CharacterSPDText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "HIT"){CallingCharacterBehaviour.CharacterHITText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "AVO"){CallingCharacterBehaviour.CharacterAVOText = StatToAssign.gameObject;}
                    if(StatToAssign.name == "LUCK"){CallingCharacterBehaviour.CharacterLUCKText = StatToAssign.gameObject;}
                }
            }
            else if(ThingToAssign.name == "ClockHand")
            {
                CallingCharacterBehaviour.ClockHand = ThingToAssign.gameObject;
            }
            else
            {
                ThingToAssign.gameObject.SetActive(false);
            }
           
        }

        CallingCharacterBehaviour.GetComponent<Button>().onClick.AddListener(() => CallingCharacterBehaviour.transform.parent.parent.GetComponent<ToggleActive>().ToggleObjectActive(Menu));
    }

    public static void SpawnSkillList(CharacterBehaviour CallingCharacterBehaviour)
    {
        if(!(CallingCharacterBehaviour.SkillList)){return;}

        float buttonHeight = 50f;
        float buttonWidth = 150f;

        float buttonMargin = 10f;

        float buttonScaler = 0.05f;

        float yOffset = (buttonScaler*(((buttonHeight+buttonMargin)/2) + (buttonHeight + buttonMargin)*(ChosenSkills[CallingCharacterBehaviour.name].Count-1)))/2;
        float yOffsetDelta = buttonScaler*(buttonHeight + buttonMargin);

        foreach (Skill SkillSkill in ChosenSkills[CallingCharacterBehaviour.name])
        {
            string SkillString = SkillSkill.name;

            GameObject Skill = new GameObject();
            Skill.layer = LayerMask.NameToLayer("UI");
            Skill.transform.parent = CallingCharacterBehaviour.SkillList.transform;

            GameObject SkillText = new GameObject();
            SkillText.layer = LayerMask.NameToLayer("UI");
            SkillText.transform.parent = Skill.transform;

            //Skill gameobject definitions
            Skill.name = char.ToUpper(( (string) SkillString)[0]) + ( (string) SkillString).Substring(1);

            RectTransform SkillRectTransform = Skill.AddComponent<RectTransform>();
            SkillRectTransform.anchoredPosition = new Vector2(0.5f, 0.5f + yOffset);
            yOffset -= yOffsetDelta;
            SkillRectTransform.sizeDelta = new Vector2(buttonWidth*( (float) Screen.width)/960f, buttonHeight*( (float) Screen.height)/540f);

            CanvasRenderer SkillCanvasRenderer = Skill.AddComponent<CanvasRenderer>();

            Image SkillImage = Skill.AddComponent<Image>();
            SkillImage.sprite = CallingCharacterBehaviour.TickCounterObject.SkillSprite;

            Button SkillButton = Skill.AddComponent<Button>();
            SkillButton.targetGraphic = SkillImage;
            SkillButton.onClick.AddListener(() => CallingCharacterBehaviour.HandleTargeting(Skill.name));
            SkillButton.onClick.AddListener(() => CallingCharacterBehaviour.SkillList.SetActive(false));

            CheckSkillSP SkillCheckSkillSP = Skill.AddComponent<CheckSkillSP>();
            SkillCheckSkillSP.CharacterObject = CallingCharacterBehaviour.gameObject;
            SkillCheckSkillSP.RequiredSP = SkillSkill.requiredSP;
            SkillCheckSkillSP.SkillButton = SkillButton;

            //Skilltext gameobject definitions
            SkillText.name = char.ToUpper(( (string) SkillString)[0]) + ( (string) SkillString).Substring(1) + "Text";

            TMPro.TextMeshProUGUI SkillTextTMP = SkillText.AddComponent<TMPro.TextMeshProUGUI>();
            SkillTextTMP.text = SkillSkill.displayName;
            SkillTextTMP.fontSize = 20f*( (float) Screen.width)/960f;
            SkillTextTMP.alignment = TMPro.TextAlignmentOptions.Center;
        }

        GameObject BackButton = new GameObject();
        BackButton.layer = LayerMask.NameToLayer("UI");
        BackButton.transform.parent = CallingCharacterBehaviour.SkillList.transform;

        BackButton.name = "BackButton";

        RectTransform BackButtonRectTransform = BackButton.AddComponent<RectTransform>();
        BackButtonRectTransform.anchoredPosition = new Vector2(4.20f, (buttonScaler*(((buttonHeight+buttonMargin)/2) + (buttonHeight + buttonMargin)*(ChosenSkills[CallingCharacterBehaviour.name].Count-1)))/2 + 40f*buttonScaler);
        BackButtonRectTransform.sizeDelta = new Vector2(50f*( (float) Screen.width)/960f, 50f*( (float) Screen.height)/540f);

        CanvasRenderer BackButtonCanvasRenderer = BackButton.AddComponent<CanvasRenderer>();

        Image BackButtonImage = BackButton.AddComponent<Image>();
        BackButtonImage.sprite = CallingCharacterBehaviour.TickCounterObject.BackButtonSprite;

        Button BackButtonButton = BackButton.AddComponent<Button>();
        BackButtonButton.targetGraphic = BackButtonImage;
        BackButtonButton.onClick.AddListener(() => CallingCharacterBehaviour.SkillList.SetActive(false));
    }

    public static void SpawnEmoteList(CharacterBehaviour CallingCharacterBehaviour)
    {

        float buttonHeight = 50f;
        float buttonWidth = 150f;

        float buttonMargin = 10f;

        float buttonScaler = 0.05f;

        float yOffset = (buttonScaler*(((buttonHeight+buttonMargin)/2) + (buttonHeight + buttonMargin)*(Emotes.Count-1)))/2;
        float yOffsetDelta = buttonScaler*(buttonHeight + buttonMargin);

        foreach (Skill EmoteEmote in Emotes)
        {
            string EmoteString = EmoteEmote.name;

            GameObject Emote = new GameObject();
            Emote.layer = LayerMask.NameToLayer("UI");
            Emote.transform.parent = CallingCharacterBehaviour.EmoteList.transform;

            GameObject EmoteText = new GameObject();
            EmoteText.layer = LayerMask.NameToLayer("UI");
            EmoteText.transform.parent = Emote.transform;

            //Emote gameobject definitions
            Emote.name = char.ToUpper(( (string) EmoteString)[0]) + ( (string) EmoteString).Substring(1);

            RectTransform EmoteRectTransform = Emote.AddComponent<RectTransform>();
            EmoteRectTransform.anchoredPosition = new Vector2(0.5f, 0.5f + yOffset);
            yOffset -= yOffsetDelta;
            EmoteRectTransform.sizeDelta = new Vector2(buttonWidth*( (float) Screen.width)/960f, buttonHeight*( (float) Screen.height)/540f);

            CanvasRenderer EmoteCanvasRenderer = Emote.AddComponent<CanvasRenderer>();

            Image EmoteImage = Emote.AddComponent<Image>();
            EmoteImage.sprite = CallingCharacterBehaviour.TickCounterObject.SkillSprite;

            Button EmoteButton = Emote.AddComponent<Button>();
            EmoteButton.targetGraphic = EmoteImage;
            EmoteButton.onClick.AddListener(() => CallingCharacterBehaviour.HandleTargeting(Emote.name));
            EmoteButton.onClick.AddListener(() => CallingCharacterBehaviour.EmoteList.SetActive(false));

            CheckSkillSP EmoteCheckEmoteSP = Emote.AddComponent<CheckSkillSP>();
            EmoteCheckEmoteSP.CharacterObject = CallingCharacterBehaviour.gameObject;
            EmoteCheckEmoteSP.RequiredSP = EmoteEmote.requiredSP;
            EmoteCheckEmoteSP.SkillButton = EmoteButton;

            //Emotetext gameobject definitions
            EmoteText.name = char.ToUpper(( (string) EmoteString)[0]) + ( (string) EmoteString).Substring(1) + "Text";

            TMPro.TextMeshProUGUI EmoteTextTMP = EmoteText.AddComponent<TMPro.TextMeshProUGUI>();
            EmoteTextTMP.text = EmoteEmote.displayName;
            EmoteTextTMP.fontSize = 20f*( (float) Screen.width)/960f;
            EmoteTextTMP.alignment = TMPro.TextAlignmentOptions.Center;
        }

        GameObject BackButton = new GameObject();
        BackButton.layer = LayerMask.NameToLayer("UI");
        BackButton.transform.parent = CallingCharacterBehaviour.EmoteList.transform;

        BackButton.name = "BackButton";

        RectTransform BackButtonRectTransform = BackButton.AddComponent<RectTransform>();
        BackButtonRectTransform.anchoredPosition = new Vector2(4.20f, (buttonScaler*(((buttonHeight+buttonMargin)/2) + (buttonHeight + buttonMargin)*(Emotes.Count-1)))/2 + 40f*buttonScaler);
        BackButtonRectTransform.sizeDelta = new Vector2(50f*( (float) Screen.width)/960f, 50f*( (float) Screen.height)/540f);

        CanvasRenderer BackButtonCanvasRenderer = BackButton.AddComponent<CanvasRenderer>();

        Image BackButtonImage = BackButton.AddComponent<Image>();
        BackButtonImage.sprite = CallingCharacterBehaviour.TickCounterObject.BackButtonSprite;

        Button BackButtonButton = BackButton.AddComponent<Button>();
        BackButtonButton.targetGraphic = BackButtonImage;
        BackButtonButton.onClick.AddListener(() => CallingCharacterBehaviour.EmoteList.SetActive(false));
    }

    public static bool IsGoodGuy(CharacterBehaviour CallingCharacterBehaviour)
    {
        return (CallingCharacterBehaviour.transform.parent.name == "GoodGuys");
    }
}