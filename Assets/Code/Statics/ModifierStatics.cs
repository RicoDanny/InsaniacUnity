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

    public static string[] LoopThroughModifiers(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<string> ReturnList = new List<string>();

        foreach(Modifier modifier in CallingCharacterBehaviour.CharacterEntity.modifiers)
        {
            modifier.duration -= 1;

            ReturnList.Add(modifier.name);
        }

        return ReturnList.ToArray();
    }
}