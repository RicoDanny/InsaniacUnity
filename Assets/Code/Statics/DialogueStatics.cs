using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DialogueStatics 
{
    [System.Serializable]
    public class Dialogue
    {
        public string DialogueString;
        public string CharacterName;
        public string CharacterEmotion;
        public string BackgroundImage;
    }

    [System.Serializable]
    public class DialogueList
    {
        public Dialogue[][] dialogue;
    }

    // Start is called before the first frame update
    public static void DefineDialogue(DialogueScript CallingDialogueScript)
    {
        CallingDialogueScript.myDialogueList = JsonUtility.FromJson<DialogueList>(CallingDialogueScript.textJSON.text);

        GameObject Canvas = GameObject.Find("Canvas");

        CallingDialogueScript.BackgroundObject = Canvas.transform.GetChild(0).gameObject;  //Volgorde van children maakt dus uit
        CallingDialogueScript.ForegroundObject = Canvas.transform.GetChild(1).gameObject;  //Volgorde van children maakt dus uit!
        CallingDialogueScript.DialogueBox = Canvas.transform.GetChild(2).gameObject;       //Volgorde van children maakt dus uit!!!!!

        CallingDialogueScript.StageIndeces = new Dictionary<string, int>(){
            {"0-0", 0},
            {"0-1", 1}
        };
    }

    public static void SetScene(DialogueScript CallingDialogueScript)
    {
        CallingDialogueScript.CurrentDialogue = (Dialogue[]) CallingDialogueScript.myDialogueList.GetType().GetProperty(SceneManager.GetActiveScene().name).GetValue(CallingDialogueScript.myDialogueList);
    }

    public static void DisplayDialogue(DialogueScript CallingDialogueScript)
    {
        GameObject.Find("TextBox").GetComponent<TMPro.TextMeshProUGUI>().text = CallingDialogueScript.CurrentDialogue[CallingDialogueScript.DialogueNumber].DialogueString;
    }

    public static void CheckNextDialogue(DialogueScript CallingDialogueScript)
    {

    }

}
