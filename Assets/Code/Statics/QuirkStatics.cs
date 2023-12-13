using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  QuirkStatics
{
    [System.Serializable]
    public class Quirk
    {
        public string name;
        public int totalduration;
        public int duration;
    }

    public static Quirk[] LoopThroughQuirks(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<Quirk> ReturnList = new List<Quirk>();

        for (int i = 0; i < CallingCharacterBehaviour.CharacterEntity.quirks.Count; i++)
        {
            Quirk CharacterQuirk = CallingCharacterBehaviour.CharacterEntity.quirks[i];

            ReturnList.Add(CharacterQuirk);

            CallingCharacterBehaviour.CharacterEntity.quirks[i].duration -= 1;
            
        }

        return ReturnList.ToArray();
    }

    //Methods for Individual quirks under here with first capital letter cause yeah reasons
    public static void Cursed(CharacterBehaviour CallingCharacterBehaviour, Quirk CharacterQuirk)
    {
        if(CharacterQuirk.duration == CharacterQuirk.totalduration)
        {
            //Do stuff that should be done one time when quirk is recieved like atk*0.9
        }
        else if (CharacterQuirk.duration == 0)
        {
            CallingCharacterBehaviour.CharacterEntity.quirks.Remove(CharacterQuirk);

            //undo stuff from first turn like atk/0.9 (restoring atk to its former glory)

            return;
        }
        
        //Do stuff that needs to be done every move, including the first.
    }

    public static void Ablaze(CharacterBehaviour CallingCharacterBehaviour, Quirk CharacterQuirk)
    {
        if(CharacterQuirk.duration == CharacterQuirk.totalduration)
        {
            //Do stuff that should be done one time when quirk is recieved like atk*0.9
        }
        else if (CharacterQuirk.duration == 0)
        {
            CallingCharacterBehaviour.CharacterEntity.quirks.Remove(CharacterQuirk);

            //undo stuff from first turn like atk/0.9 (restoring atk to its former glory)

            return;
        }
        
        //Do stuff that needs to be done every move, including the first.
    }
}