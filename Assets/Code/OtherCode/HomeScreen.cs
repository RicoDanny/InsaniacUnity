using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static HomeScreenStatics;
using static ActionStatics;

public class HomeScreen : MonoBehaviour
{
    public GameObject StartButtonObject;
    public GameObject StoryStartButtonObject;

    public GameObject LetterA;
    public GameObject LetterB;
    public GameObject LetterC;
    public GameObject LetterEXA;
    public GameObject LetterEXB;

    public GameObject StoryLetterA;
    public GameObject StoryLetterB;
    public GameObject StoryLetterC;
    public GameObject StoryLetterD;

    void Start() 
    {
        ChosenCharacters = new List<GameObject>();
        ChosenCharacterStrings = new List<string>();

        ChosenLetter = "0";

        if(!PlayerPrefs.HasKey("ChapterProgress"))
        {
            PlayerPrefs.SetInt("ChapterProgress", 0);
            PlayerPrefs.SetInt("StageProgress", 0);
        }

        int ChapterProgress = PlayerPrefs.GetInt("ChapterProgress");
        int StageProgress = PlayerPrefs.GetInt("StageProgress");

        Debug.Log(ChapterProgress);
        Debug.Log(StageProgress);

        bool unlocked = true;

        foreach (Transform ChapterTransform in GameObject.Find("LevelSelection").transform)
        {
            foreach (Transform StageSelectTransform in ChapterTransform)
            {
                StageSelectTransform.gameObject.SetActive(unlocked);

                if(ChapterTransform.name[7].ToString() == ChapterProgress.ToString() && StageSelectTransform.name[2].ToString() == StageProgress.ToString())
                {
                    unlocked = false;
                }
            }
        }

        unlocked = true;

        foreach (Transform ChapterClip in GameObject.Find("ChapterClips").transform)
        {
            ChapterClip.gameObject.SetActive(unlocked);

            if (ChapterClip.name[7].ToString() == ChapterProgress.ToString())
            {
                unlocked = false;
            }
        }
    }

    public void CallSetChosenScene(string ProxyDesiredScene)
    {
        SetChosenScene(ProxyDesiredScene);
    }

    public void CallSetChosenLetter(string ProxyDesiredLetter)
    {
        SetChosenLetter(ProxyDesiredLetter);
    }

    public void CallSetStageType(string ProxyDesiredStageType)
    {
        SetStageType(ProxyDesiredStageType);

        Debug.Log(ProxyDesiredStageType);
    }

    public void CallSetLetterAmount(int ProxyDesiredLetterAmount)
    {
        SetLetterAmount(ProxyDesiredLetterAmount);

        Debug.Log(ProxyDesiredLetterAmount);
    }
    public void CallSetEXAmount(int ProxyDesiredEXAmount)
    {
        SetEXAmount(ProxyDesiredEXAmount);
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
            ChosenCharacterStrings.Remove(CharacterButton.name.Remove(CharacterButton.name.Length-6, 6));

            CharacterButton.GetComponent<Image>().color = new Color(255, 255, 255);
        }
        else
        {
            ChosenCharacters.Add(CharacterButton);
            ChosenCharacterStrings.Add(CharacterButton.name.Remove(CharacterButton.name.Length-6, 6));

            CharacterButton.GetComponent<Image>().color = new Color(0, 255, 0);
        }
    }
    
    void Update()
    {
        StartButtonObject.SetActive(ChosenCharacters.Count == 3 && ChosenLetter != "0");
        StoryStartButtonObject.SetActive(ChosenLetter != "0");

        LetterA.SetActive(ChosenStageType == "Battle" && ChosenLetterAmount >= 1);
        LetterB.SetActive(ChosenStageType == "Battle" && ChosenLetterAmount >= 2);
        LetterC.SetActive(ChosenStageType == "Battle" && ChosenLetterAmount >= 3);
        LetterEXA.SetActive(ChosenStageType == "Battle" && ChosenEXAmount >= 1);
        LetterEXB.SetActive(ChosenStageType == "Battle" && ChosenEXAmount >= 2);

        StoryLetterA.SetActive(ChosenStageType == "Story" && ChosenLetterAmount >= 1);
        StoryLetterB.SetActive(ChosenStageType == "Story" && ChosenLetterAmount >= 2);
        StoryLetterC.SetActive(ChosenStageType == "Story" && ChosenLetterAmount >= 3);
        StoryLetterD.SetActive(ChosenStageType == "Story" && ChosenLetterAmount >= 4);
    }
}
