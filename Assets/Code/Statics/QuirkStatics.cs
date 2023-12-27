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
        public bool ispositive;
        public bool tickbased = false;
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

    public static string[] LoopThroughTickQuirks(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<string> ReturnList = new List<string>();

        foreach(KeyValuePair<string, List<Quirk>> QuirkEntry in CallingCharacterBehaviour.CharacterEntity.quirks)
        {
            if (QuirkEntry.Value.Count > 0)
            {
                if(QuirkEntry.Value[0].tickbased){
                    ReturnList.Add(QuirkEntry.Key);
                }
            }
        }

        return ReturnList.ToArray();
    }

    //Methods for Individual quirks under here
    public static void Ablaze(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<Quirk> SpecificQuirkList = CallingCharacterBehaviour.CharacterEntity.quirks["Ablaze"];

        int NumberOfStacksOfThree = (int) SpecificQuirkList.Count/3; //Hier truncate de cast van int de float de decimalen, oftewel de floor function alleen dan efficient

        foreach(Quirk SpecificQuirk in SpecificQuirkList)
        {
            if(SpecificQuirk.duration + 1 == SpecificQuirk.totalduration) //+1 want in loopthrough quirks wordt al de 1 van duration eraf gehaald
            {
                //Do stuff that should be done one time when quirk is recieved like atk*0.9
            }
            
            if (SpecificQuirk.duration + 1 == 0)
            {
                SpecificQuirkList.Remove(SpecificQuirk);

                //undo stuff from first turn like atk/0.9 (restoring atk to its former glory)
            }
            else
            {
                //Hier dingen elke turn
            }
        }
        
        if(NumberOfStacksOfThree == SpecificQuirkList.Count/3 && SpecificQuirkList[SpecificQuirkList.Count-1].duration + 1 == SpecificQuirkList[SpecificQuirkList.Count-1].totalduration)
        {
            //reset all countdowns
            foreach(Quirk SpecificQuirk in SpecificQuirkList)
            {
                SpecificQuirk.duration = SpecificQuirk.totalduration;
            }
        }
    }

    public static void Defenseless(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<Quirk> SpecificQuirkList = CallingCharacterBehaviour.CharacterEntity.quirks["Defenseless"];

        foreach(Quirk SpecificQuirk in SpecificQuirkList)
        {
            if(SpecificQuirk.duration + 1 == SpecificQuirk.totalduration) //+1 want in loopthrough quirks wordt al de 1 van duration eraf gehaald
            {
                //Do stuff that should be done one time when quirk is recieved like atk*0.9
            }
            
            if (SpecificQuirk.duration + 1 == 0)
            {
                SpecificQuirkList.Remove(SpecificQuirk);

                //undo stuff from first turn like atk/0.9 (restoring atk to its former glory)
            }
            else
            {
                //Hier dingen elke turn
            }
        }
    }

    public static void Paralyzed(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<Quirk> SpecificQuirkList = CallingCharacterBehaviour.CharacterEntity.quirks["Paralyzed"];

        foreach(Quirk SpecificQuirk in SpecificQuirkList)
        {
            if(SpecificQuirk.duration + 1 == SpecificQuirk.totalduration) //+1 want in loopthrough quirks wordt al de 1 van duration eraf gehaald
            {
                //Do stuff that should be done one time when quirk is recieved like atk*0.9
            }
            
            if (SpecificQuirk.duration + 1 == 0)
            {
                SpecificQuirkList.Remove(SpecificQuirk);

                //undo stuff from first turn like atk/0.9 (restoring atk to its former glory)
            }
            else
            {
                //Hier dingen elke turn
            }
        }
    }

    public static void Bleeding(CharacterBehaviour CallingCharacterBehaviour)
    {
        List<Quirk> SpecificQuirkList = CallingCharacterBehaviour.CharacterEntity.quirks["Bleeding"];

        foreach(Quirk SpecificQuirk in SpecificQuirkList)
        {
            if(SpecificQuirk.duration + 1 == SpecificQuirk.totalduration) //+1 want in loopthrough quirks wordt al de 1 van duration eraf gehaald
            {
                //Do stuff that should be done one time when quirk is recieved like atk*0.9
            }
            
            if (SpecificQuirk.duration + 1 == 0)
            {
                SpecificQuirkList.Remove(SpecificQuirk);

                //undo stuff from first turn like atk/0.9 (restoring atk to its former glory)
            }
            else
            {
                //Hier dingen elke turn
            }
        }
    }
}