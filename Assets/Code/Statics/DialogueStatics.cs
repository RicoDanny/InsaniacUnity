using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        CallingDialogueScript.ForegroundObject = Canvas.transform.GetChild(1).gameObject;  //Volgorde van children maakt dus uit!
        CallingDialogueScript.DialogueBox = Canvas.transform.GetChild(2).gameObject;       //Volgorde van children maakt dus uit!!!!!
    }



    public static void DisplayDialogue(DialogueScript CallingDialogueScript)
    {
        CallingDialogueScript.TextBoxComponent.text = (string) CallingDialogueScript.myDialogueList.dialogue[CallingDialogueScript.DialogueNumber].DialogueString;
        CallingDialogueScript.NameBoxComponent.text = (string) CallingDialogueScript.myDialogueList.dialogue[CallingDialogueScript.DialogueNumber].CharacterName;
    }

    public static void CheckNextDialogue(DialogueScript CallingDialogueScript)
    {
        if (Input.GetKeyDown(KeyCode.Space) && CallingDialogueScript.DialogueNumber < CallingDialogueScript.myDialogueList.dialogue.Length - 1)
        {
            CallingDialogueScript.DialogueNumber++;
        }
    }

}
