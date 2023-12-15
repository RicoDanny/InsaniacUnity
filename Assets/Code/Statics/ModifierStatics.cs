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

        foreach(KeyValuePair<string, List<Modifier>> ModifierEntry in CallingCharacterBehaviour.CharacterEntity.modifiers)
        {
            if (ModifierEntry.Value.Count > 0)
            {
                ReturnList.Add(ModifierEntry.Key);

                foreach( Modifier modifier in ModifierEntry.Value)
                {
                    modifier.duration -= 1;
                }
            }
        }

        return ReturnList.ToArray();
    }
}