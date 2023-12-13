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
        List<Modifier> ReturnList = new List<Modifier>();

        for (int i = 0; i < CallingCharacterBehaviour.CharacterEntity.modifiers.Count; i++)
        {
            Modifier CharacterModifier = CallingCharacterBehaviour.CharacterEntity.modifiers[i];

            ReturnList.Add(CharacterModifier);

            CallingCharacterBehaviour.CharacterEntity.modifiers[i].duration -= 1;
        
        }

        return ReturnList.ToArray();
    }
}