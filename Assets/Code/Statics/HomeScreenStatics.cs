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
    
    public static void MoveToScene()
    {
        SceneManager.LoadScene(ChosenScene);
    }
}