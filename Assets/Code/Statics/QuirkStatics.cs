using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  QuirkStatics
{
    public static string[] LoopThroughQuirks(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<string> ReturnList = new List<string>();

        for (int i = 0; i < CallingCharacterBehaviour.CharacterEntity.quirks.Count; i++)
        {
            object[] QuirkArray = CallingCharacterBehaviour.CharacterEntity.quirks[i];
            string QuirkName = (string) QuirkArray[0];

            string MethodName = char.ToUpper(( (string) QuirkName)[0]) + ( (string) QuirkName).Substring(1); //Capatilezed eerste letter voor redenen (Function readability)

            ReturnList.Add(MethodName);

            if (((int) CallingCharacterBehaviour.CharacterEntity.quirks[i][1]) > 0)
            {
                CallingCharacterBehaviour.CharacterEntity.quirks[i][1] = (int) CallingCharacterBehaviour.CharacterEntity.quirks[i][1] - 1;
            }
            else
            {
                CallingCharacterBehaviour.CharacterEntity.quirks.RemoveAt(i);
            }
        }

        return ReturnList.ToArray();
    }

    //Methods for Individual quirks under here with first capital letter cause yeah reasons
    public static void Cursed(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Doe iets leuks
    }

    public static void Ablaze(CharacterBehaviour CallingCharacterBehaviour)
    {
        //Doe iets leuks
    }
}