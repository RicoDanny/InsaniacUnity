using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        CallingDialogueScript.StageIndeces = new Dictionary<string, int>(){
            {"0-0", 0},
            {"0-1", 1}
        };
    }
}
