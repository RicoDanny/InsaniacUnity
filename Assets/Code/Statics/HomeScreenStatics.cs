using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;

public static class HomeScreenStatics
{
    public static void SetChosenScene(string DesiredScene)
    {
        ChosenScene = DesiredScene;
    }

    public static void SetChosenLetter(string LevelLetter)
    {
        ChosenLetter = LevelLetter;
    }
    
    public static void SetStageType(string StageType)
    {
        ChosenStageType = StageType;
    }

    public static void SetLetterAmount(int LetterAmount)
    {
        ChosenLetterAmount = LetterAmount;
    }

    public static void SetEXAmount(int EXAmount)
    {
        ChosenEXAmount = EXAmount;
    }

    public static void MoveToScene()
    {
        SceneManager.LoadScene(ChosenScene + ChosenLetter);
    }
}
