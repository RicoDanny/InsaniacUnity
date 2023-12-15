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

    public static string[] LoopThroughQuirks(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<string> ReturnList = new List<string>();

        foreach(KeyValuePair<string, List<Quirk>> QuirkEntry in CallingCharacterBehaviour.CharacterEntity.quirks)
        {
            if (QuirkEntry.Value.Count > 0)
            {
                ReturnList.Add(QuirkEntry.Key);

                foreach( Quirk quirk in QuirkEntry.Value)
                {
                    quirk.duration -= 1;
                }
            }
        }

        return ReturnList.ToArray();
    }

    //Methods for Individual quirks under here
    public static void Ablaze(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<Quirk> AblazeList = CallingCharacterBehaviour.CharacterEntity.quirks["Ablaze"];

        int NumberOfStacksOfThree = (int) AblazeList.Count/3; //Hier truncate de cast van int de float de decimalen, oftewel de floor function alleen dan efficient

        foreach(Quirk AblazeQuirk in AblazeList)
        {
            if(AblazeQuirk.duration + 1 == AblazeQuirk.totalduration) //+1 want in loopthrough quirks wordt al de 1 van duration eraf gehaald
            {
                //Do stuff that should be done one time when quirk is recieved like atk*0.9
            }
            
            if (AblazeQuirk.duration + 1 == 0)
            {
                AblazeList.Remove(AblazeQuirk);

                //undo stuff from first turn like atk/0.9 (restoring atk to its former glory)
            }
            else
            {
                //Hier dingen elke turn
            }
        }
        
        if(NumberOfStacksOfThree == AblazeList.Count/3 && AblazeList[AblazeList.Count-1].duration + 1 == AblazeList[AblazeList.Count-1].totalduration)
        {
            //reset all countdowns
            foreach(Quirk AblazeQuirk in AblazeList)
            {
                AblazeQuirk.duration = AblazeQuirk.totalduration;
            }
        }
    }
}