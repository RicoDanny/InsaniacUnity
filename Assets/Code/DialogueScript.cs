using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueStatics;

public class DialogueScript : MonoBehaviour
{
    public TextAsset textJSON;
    public DialogueList myDialogueList = new DialogueList();
    public Dictionary<string, int> StageIndeces = new Dictionary<string, int>();
    public GameObject BackgroundObject;
    public GameObject ForegroundObject;
    public GameObject DialogueBox;
    public Dialogue[] CurrentDialogue;
    public int DialogueNumber = 0;
    
    void Start()
    {
        DefineDialogue(this);

        SetScene(this);
    }

    void Update()
    {
        DisplayDialogue(this);

        CheckNextDialogue(this);
    }
}
