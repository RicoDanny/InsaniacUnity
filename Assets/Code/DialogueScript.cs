using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueStatics;

public class DialogueScript : MonoBehaviour
{
    public TextAsset textJSON;

    public DialogueList myDialogueList = new DialogueList();
    
    // Start is called before the first frame update
    void Start()
    {
        DefineDialogue(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
