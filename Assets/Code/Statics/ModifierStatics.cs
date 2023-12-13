using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  ModifierStatics
{
    [System.Serializable]
    public class Modifier
    {
        public string name;
        public int totalduration;
        public int duration;
    }

    public static Modifier[] LoopThroughModifiers(CharacterBehaviour CallingCharacterBehaviour)
    {
        // List<Quirk> ReturnList = new List<Quirk>();

        // for (int i = 0; i < CallingCharacterBehaviour.CharacterEntity.quirks.Count; i++)
        // {
        //     Quirk CharacterQuirk = CallingCharacterBehaviour.CharacterEntity.quirks[i];

        //     ReturnList.Add(CharacterQuirk);

        //     CallingCharacterBehaviour.CharacterEntity.quirks[i].duration -= 1;
            
        // }

        // return ReturnList.ToArray();
        return null;
    }
}