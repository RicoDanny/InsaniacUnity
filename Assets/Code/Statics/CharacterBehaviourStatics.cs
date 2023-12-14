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
        public int basedmg = 1;
        public int atkboost = 0;
        public int defboost = 0;
        public int dmgboost = 0;
        public double atkmultiplier = 1.0;
        public double defmultiplier = 1.0;
        public double luckymultiplier = 1.0;
        public double critmultiplier = 1.0;
        public double guardmultiplier = 1.0;
        public int loweredspcost = 0;
        public int turn = 0;
        public string status;
        public double statusmultiplier = 1.0;
        public List<Quirk> quirks = new List<Quirk>(); //Dus voor elke character een lijst met daarin arrays van quirk naam en quirk effect duration to go: {["quirkname", int], ["quirkname", int]}
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

        CallingCharacterBehaviour.myCharacterList = JsonUtility.FromJson<CharacterList>(CallingCharacterBehaviour.textJSON.text);
        
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

    public static void SpawnSkillList(CharacterBehaviour CallingCharacterBehaviour)
    {
        if(!(CallingCharacterBehaviour.SkillList)){return;}

        float buttonHeight = 50f;
        float buttonWidth = 150f;

        float buttonMargin = 10f;

        float buttonScaler = 0.05f;

        float yOffset = (buttonScaler*(((buttonHeight+buttonMargin)/2) + (buttonHeight + buttonMargin)*(ChosenSkills[CallingCharacterBehaviour.name].Length-1)))/2;
        float yOffsetDelta = buttonScaler*(buttonHeight + buttonMargin);

        foreach (string SkillString in ChosenSkills[CallingCharacterBehaviour.name])
        {
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
            SkillRectTransform.sizeDelta = new Vector2(buttonWidth, buttonHeight);

            CanvasRenderer SkillCanvasRenderer = Skill.AddComponent<CanvasRenderer>();

            Image SkillImage = Skill.AddComponent<Image>();
            SkillImage.sprite = CallingCharacterBehaviour.TickCounterObject.SkillSprite;

            Button SkillButton = Skill.AddComponent<Button>();
            SkillButton.targetGraphic = SkillImage;
            SkillButton.onClick.AddListener(() => CallingCharacterBehaviour.HandleTargeting(Skill.name));
            SkillButton.onClick.AddListener(() => CallingCharacterBehaviour.SkillList.SetActive(false));

            //Skilltext gameobject definitions
            SkillText.name = char.ToUpper(( (string) SkillString)[0]) + ( (string) SkillString).Substring(1) + "Text";

            TMPro.TextMeshProUGUI SkillTextTMP = SkillText.AddComponent<TMPro.TextMeshProUGUI>();
            SkillTextTMP.text = char.ToUpper(( (string) SkillString)[0]) + ( (string) SkillString).Substring(1);
            SkillTextTMP.fontSize = 20f;
            SkillTextTMP.alignment = TMPro.TextAlignmentOptions.Center;
        }

        GameObject BackButton = new GameObject();
        BackButton.layer = LayerMask.NameToLayer("UI");
        BackButton.transform.parent = CallingCharacterBehaviour.SkillList.transform;

        BackButton.name = "BackButton";

        RectTransform BackButtonRectTransform = BackButton.AddComponent<RectTransform>();
        BackButtonRectTransform.anchoredPosition = new Vector2(4.20f, (buttonScaler*(((buttonHeight+buttonMargin)/2) + (buttonHeight + buttonMargin)*(ChosenSkills[CallingCharacterBehaviour.name].Length-1)))/2 + 40f*buttonScaler);
        BackButtonRectTransform.sizeDelta = new Vector2(50f, 50f);

        CanvasRenderer BackButtonCanvasRenderer = BackButton.AddComponent<CanvasRenderer>();

        Image BackButtonImage = BackButton.AddComponent<Image>();
        BackButtonImage.sprite = CallingCharacterBehaviour.TickCounterObject.BackButtonSprite;

        Button BackButtonButton = BackButton.AddComponent<Button>();
        BackButtonButton.targetGraphic = BackButtonImage;
        BackButtonButton.onClick.AddListener(() => CallingCharacterBehaviour.SkillList.SetActive(false));
    }

    public static bool IsGoodGuy(CharacterBehaviour CallingCharacterBehaviour)
    {
        return (CallingCharacterBehaviour.transform.parent.name == "GoodGuys");
    }
}