using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueStatics;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public TextAsset textJSON;
    public DialogueList myDialogueList = new DialogueList();
    public string CharacterNameToDisplay;
    public string CharacterEmotionToDisplay;
    public string BackgroundToDisplay;
    public GameObject BackgroundObject;
    public Image BackgroundImage;
    public GameObject ForegroundObject;
    public GameObject DialogueBox;
    public GameObject NameBoxSprite;
    public TMPro.TextMeshProUGUI TextBoxComponent;
    public TMPro.TextMeshProUGUI NameBoxComponent;
    public int DialogueNumber = 0;
    public string NextScene = "HomeScreen";
    public int SetStageProgress;
    public int SetChapterProgress;
    
    void Start()
    {
        SetScene(this);

        DisplayDialogue(this);
        DisplayBackground(this);
        DisplayCharacter(this);
    }

    void Update()
    {
        if(CheckNextDialogue(this))
        {
            DisplayDialogue(this);
            DisplayBackground(this);
            DisplayCharacter(this);
        }
    }
}
