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

        float yOffset = 8;
        float yOffsetDelta = 16/(ChosenSkills[CallingCharacterBehaviour.name].Length-1);

        foreach (string SkillString in ChosenSkills[CallingCharacterBehaviour.name])
        {
            GameObject Skill = new GameObject();
            Skill.name = char.ToUpper(( (string) SkillString)[0]) + ( (string) SkillString).Substring(1);
            Skill.transform.parent = CallingCharacterBehaviour.SkillList.transform;

            RectTransform SkillRectTransform = Skill.AddComponent<RectTransform>();
            SkillRectTransform.anchoredPosition = new Vector2(0.5f, 0.5f + yOffset);
            yOffset -= yOffsetDelta;

            CanvasRenderer SkillCanvasRenderer = Skill.AddComponent<CanvasRenderer>();

            Image SkillImage = Skill.AddComponent<Image>();
            SkillImage.sprite = CallingCharacterBehaviour.TickCounterObject.SkillSprite;

            Button SkillButton = Skill.AddComponent<Button>();
        }
    }

    public static bool IsGoodGuy(CharacterBehaviour CallingCharacterBehaviour)
    {
        return (CallingCharacterBehaviour.transform.parent.name == "GoodGuys");
    }
}