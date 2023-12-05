using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueStatics;

public class DialogueScript : MonoBehaviour
{
    public TextAsset textJSON;
    public DialogueList myDialogueList = new DialogueList();
    public GameObject BackgroundObject;
    public GameObject ForegroundObject;
    public GameObject DialogueBox;
    public TMPro.TextMeshProUGUI TextBoxComponent;
    public TMPro.TextMeshProUGUI NameBoxComponent;
    public int DialogueNumber = 0;
    
    void Start()
    {
        SetScene(this);
    }

    void Update()
    {
        DisplayDialogue(this);

        CheckNextDialogue(this);
    }
}
