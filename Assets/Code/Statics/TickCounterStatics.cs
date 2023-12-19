using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main statics
using static QuirkStatics;
using static ActionStatics;
using static CharacterBehaviourStatics;

public static class  TickCounterStatics
{
    public static void SpawnCharacters(TickCounter CallingTickCounter)
    {
        int displace = 200*(ChosenCharacterStrings.Count-1);

        foreach (Transform GoodGuysTransform in CallingTickCounter.GoodGuysObject.transform)
        {
            bool contained = false;

            if(ChosenCharacterStrings == null){break;}
            
            foreach (string ChosenCharacter in ChosenCharacterStrings)
            {
                if ( ChosenCharacter ==  GoodGuysTransform.name){
                    contained = true;
                    CallingTickCounter.AllyCount++;
                    SpawnSkillList(GoodGuysTransform.gameObject.GetComponent<CharacterBehaviour>());
                    GoodGuysTransform.position = CallingTickCounter.GoodGuysObject.transform.position + new Vector3(displace*0.2f,0,displace*0.2f);
                    displace -= 200;
                }
            }
            GoodGuysTransform.gameObject.SetActive(contained);
        }

        foreach (Transform BadGuysTransform in CallingTickCounter.BadGuysObject.transform)
        {
            CallingTickCounter.EnemyCount++;
        }
    }

    public static void UpdateTick(TickCounter CallingTickCounter)
    {
        CallingTickCounter.TimeDiff += Time.deltaTime;

        if(CallingTickCounter.NewTick == true && CallingTickCounter.Active.Count == 0 && CallingTickCounter.TimeDiff > (float) CallingTickCounter.TickInterval)
        {
            CallingTickCounter.TimeDiff = 0;

            CallingTickCounter.NewTick = false;
            
            CallingTickCounter.Tickfunction();
        }
    }

    public static void LoopThroughUnits(TickCounter CallingTickCounter)
    {
        foreach (GameObject character in CallingTickCounter.Units)
        {
            CharacterBehaviour characterScript = character.GetComponent<CharacterBehaviour>();
            
            characterScript.CharacterEntity.energy += characterScript.CharacterEntity.spd;

            if (characterScript.CharacterEntity.energy >= 60) {
                CallingTickCounter.Active.Add(characterScript);

                characterScript.CharacterEntity.energy -= 60;
            }
        }
    }

    public static void DefineMenusChildren(TickCounter CallingTickCounter)
    {
        Transform parentTransform = GameObject.Find("BattleUI").transform;

        // Loop through each child and add it to the list
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);
            CallingTickCounter.MenusChildren.Add(child);
        }
    }

    public static void DefineTickSelectBanners(TickCounter CallingTickCounter)
    {
        CallingTickCounter.SelectTargetBanner = GameObject.Find("SelectTargets");

        CallingTickCounter.SelectUnitBanner = GameObject.Find("SelectUnit");
    }

    public static bool PlayerWon(TickCounter CallingTickCounter)
    {
        if(CallingTickCounter.EnemyCount == 0 && CallingTickCounter.TimeDiff > 0.5f)
        {
            if(SceneManager.GetActiveScene().name == "0-2c"){SceneManager.LoadScene("0-2d");return false;}

            return true;
        }

        return false;
    }
    public static bool PlayerLost(TickCounter CallingTickCounter)
    {
        if(CallingTickCounter.AllyCount == 0 && CallingTickCounter.TimeDiff > 0.5f)
        {
            if(SceneManager.GetActiveScene().name == "0-2c"){SceneManager.LoadScene("0-2d");return false;}

            return true;
        }

        return false;
    }
}