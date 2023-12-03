using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public List<object[]> quirks = new List<object[]>(); //Dus voor elke character een lijst met daarin arrays van quirk naam en quirk effect duration to go: {["quirkname", int], ["quirkname", int]}
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
            if(JsonCharacter.Name == CallingCharacterBehaviour.transform.name) 
            {
                CallingCharacterBehaviour.CharacterEntity = JsonCharacter;
            }
        }
    }
}