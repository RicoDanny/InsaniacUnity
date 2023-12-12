using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static HomeScreenStatics;
using static ActionStatics;

public class HomeScreen : MonoBehaviour
{
    public GameObject StartButtonObject;

    void Start() 
    {
        ChosenCharacters = new List<GameObject>();
    }
    public void CallSetChosenScene(string ProxyDesiredScene)
    {
        SetChosenScene(ProxyDesiredScene);
    }

    public void CallMoveToScene()
    {
        MoveToScene();
    }
    
    public void ToggleCharacter(GameObject CharacterButton)
    {
        if(ChosenCharacters.Contains(CharacterButton))
        {
            ChosenCharacters.Remove(CharacterButton);

            CharacterButton.GetComponent<Image>().color = new Color(255, 255, 255);
        }
        else
        {
            ChosenCharacters.Add(CharacterButton);

            CharacterButton.GetComponent<Image>().color = new Color(0, 255, 0);
        }
    }
    
    void Update()
    {
        StartButtonObject.SetActive(ChosenCharacters.Count >= 3);
    }
}
