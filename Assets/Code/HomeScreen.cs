using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HomeScreenStatics;

public class HomeScreen : MonoBehaviour
{
    public void CallSetChosenScene(string ProxyDesiredScene)
    {
        SetChosenScene(ProxyDesiredScene);
    }

    public void CallMoveToScene()
    {
        MoveToScene();
    }
    
    public void ToggleCharacter(string CharacterName)
    {
        if(ChosenCharacters.Contains(CharacterName))
        {
            ChosenCharacters.Remove(CharacterName);

            GameObject.Find(CharacterName + "Button").GetComponent<Sprite>().color = new Color(255, 255, 255);
        }
        else
        {
            ChosenCharacters.Add(CharacterName);

            GameObject.Find(CharacterName + "Button").GetComponent<Sprite>().color = new Color(0, 255, 0);
        }
    }
}
