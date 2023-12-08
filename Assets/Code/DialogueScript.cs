using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueStatics;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public TextAsset textJSON;
    public DialogueList myDialogueList = new DialogueList();
    public GameObject BackgroundObject;
    public Image BackgroundImage;
    public GameObject ForegroundObject;
    public GameObject DialogueBox;
    public GameObject NameBoxSprite;
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
        DisplayBackground(this);

        CheckNextDialogue(this);
    }
}
