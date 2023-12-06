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
        string path = "Assets/Code/JsonStuff/StageDialogues/" + SceneManager.GetActiveScene().name + ".json"; //Pak de goede json file
        StreamReader reader = new StreamReader(path); 
        CallingDialogueScript.myDialogueList = JsonUtility.FromJson<DialogueList>(reader.ReadToEnd());

        GameObject Canvas = GameObject.Find("Canvas");

        CallingDialogueScript.BackgroundObject = Canvas.transform.GetChild(0).gameObject;  //Volgorde van children maakt dus uit
        CallingDialogueScript.ForegroundObject = Canvas.transform.GetChild(1).gameObject;
        CallingDialogueScript.DialogueBox = Canvas.transform.GetChild(2).gameObject;
    }

    public static void DisplayDialogue(DialogueScript CallingDialogueScript)
    {
        CallingDialogueScript.TextBoxComponent.text = (string) CallingDialogueScript.myDialogueList.dialogue[CallingDialogueScript.DialogueNumber].DialogueString;

        string CharacterNameToDisplay = (string) CallingDialogueScript.myDialogueList.dialogue[CallingDialogueScript.DialogueNumber].CharacterName;
        CallingDialogueScript.NameBoxComponent.text = CharacterNameToDisplay;
        CallingDialogueScript.NameBoxSprite.SetActive(CharacterNameToDisplay != ""); //Als het de scene is die dingen zegt, dan moet de sprite achter de naam weg, want er is geen naam :0! 
    }

    public static void CheckNextDialogue(DialogueScript CallingDialogueScript)
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && CallingDialogueScript.DialogueNumber < CallingDialogueScript.myDialogueList.dialogue.Length - 1)
        {
            CallingDialogueScript.DialogueNumber++; 
        }
    }

}
