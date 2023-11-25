using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class Character
    {
        public string Name;
        public int hp;
        public int sp;
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
    }

    [System.Serializable]
    public class CharacterList
    {
        public Character[] character;
    }

    public CharacterList myCharacterList = new CharacterList();
    
    // Start is called before the first frame update
    void Start()
    {
        myCharacterList = JsonUtility.FromJson<CharacterList>(textJSON.text);
    }
}
