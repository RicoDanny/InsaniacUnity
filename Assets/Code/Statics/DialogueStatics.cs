using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class DialogueStatics 
{
    [System.Serializable]
    public class DialogueList
    {
        public Dialogue[] dialogue;
    }

    [System.Serializable]
    public class Dialogue
    {
        public string DialogueString;
        public string CharacterName;
        public string CharacterEmotion;
        public string BackgroundImage;
    }

    public static void SetScene(DialogueScript CallingDialogueScript)
    {
        CallingDialogueScript.myDialogueList = JsonUtility.FromJson<DialogueList>(CallingDialogueScript.textJSON.text);

        GameObject Canvas = GameObject.Find("Canvas");

        CallingDialogueScript.BackgroundObject = Canvas.transform.GetChild(0).gameObject;  //Volgorde van children maakt dus uit
        CallingDialogueScript.ForegroundObject = Canvas.transform.GetChild(1).gameObject;
        CallingDialogueScript.DialogueBox = Canvas.transform.GetChild(2).gameObject;
    }

    public static void DisplayDialogue(DialogueScript CallingDialogueScript)
    {
        if(CallingDialogueScript.DialogueNumber < CallingDialogueScript.myDialogueList.dialogue.Length)
        {
            CallingDialogueScript.CharacterNameToDisplay = (string) CallingDialogueScript.myDialogueList.dialogue[CallingDialogueScript.DialogueNumber].CharacterName;
            CallingDialogueScript.NameBoxComponent.text = CallingDialogueScript.CharacterNameToDisplay;

            if(CallingDialogueScript.CharacterNameToDisplay != "")
            {
                CallingDialogueScript.TextBoxComponent.text = "“" + (string) CallingDialogueScript.myDialogueList.dialogue[CallingDialogueScript.DialogueNumber].DialogueString + "”";
                CallingDialogueScript.NameBoxSprite.SetActive(true);
            }
            else
            {
                CallingDialogueScript.NameBoxSprite.SetActive(false); //Als het de scene is die dingen zegt, dan moet de sprite achter de naam weg, want er is geen naam :0! 
                CallingDialogueScript.TextBoxComponent.text = (string) CallingDialogueScript.myDialogueList.dialogue[CallingDialogueScript.DialogueNumber].DialogueString;
            }

            CallingDialogueScript.CharacterEmotionToDisplay = (string) CallingDialogueScript.myDialogueList.dialogue[CallingDialogueScript.DialogueNumber].CharacterEmotion;
            CallingDialogueScript.BackgroundToDisplay = (string) CallingDialogueScript.myDialogueList.dialogue[CallingDialogueScript.DialogueNumber].BackgroundImage;
        }
    }

    public static void DisplayCharacter(DialogueScript CallingDialogueScript)
    {
        if(CallingDialogueScript.DialogueNumber < CallingDialogueScript.myDialogueList.dialogue.Length)
        {
            foreach (Transform CharacterSpritesTransform in CallingDialogueScript.ForegroundObject.transform)
            {
                if(CharacterSpritesTransform.name == CallingDialogueScript.CharacterNameToDisplay)
                {
                    CharacterSpritesTransform.gameObject.SetActive(true);

                    foreach (Transform CharacterEmotionSpritesTransform in CharacterSpritesTransform)
                    {
                        if(CharacterEmotionSpritesTransform.name == CallingDialogueScript.CharacterEmotionToDisplay)
                        {
                            CharacterEmotionSpritesTransform.gameObject.SetActive(true);
                        }
                        else
                        {
                            CharacterEmotionSpritesTransform.gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    CharacterSpritesTransform.gameObject.SetActive(false);
                }
            }
        }
    }

    public static void DisplayBackground(DialogueScript CallingDialogueScript)
    {
        if(CallingDialogueScript.DialogueNumber < CallingDialogueScript.myDialogueList.dialogue.Length)
        {
            foreach (Transform BackgroundSpritesTransform in CallingDialogueScript.BackgroundObject.transform)
            {
                BackgroundSpritesTransform.gameObject.SetActive(BackgroundSpritesTransform.name == CallingDialogueScript.BackgroundToDisplay);
            }
        }
    }

    public static bool CheckNextDialogue(DialogueScript CallingDialogueScript)
    {
        if(CallingDialogueScript.DialogueNumber < CallingDialogueScript.myDialogueList.dialogue.Length)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                CallingDialogueScript.DialogueNumber++;
                return true; 
            }

            return false;
        }
        else
        {
            SceneManager.LoadScene(CallingDialogueScript.NextScene);
            return false;
        }
    }

}
