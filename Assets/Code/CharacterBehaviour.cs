using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;
using static UiStatics;
using static QuirkStatics;

//Character statics
using static Min;
using static Pygor;
using static Grungo;
    //enz.

public class CharacterBehaviour : MonoBehaviour
{
    public GameObject CharacterHPText;
    public List<string> Actions; //List of premoves of Abilities/Actions
    public List<CharacterBehaviour[]> TargetsPerAction; //List of Targets that are on the same Indexes as the Actions that they are Targets for in the Actions list
    public GameObject HighlightObject;
    public SpriteRenderer HighlightRenderer;
    private CharacterBehaviour ThisCharacterBehaviour;
    public TickCounter TickCounterObject;
    public Character CharacterEntity;
    public int TargetingType;
    public bool IsTarget;
    public bool User;
    public string TempActionString;
    public TextAsset textJSON;
    public GameObject SelectTargetBanner;
    public GameObject SelectUnitBanner;
    public CharacterList myCharacterList = new CharacterList();

    // Start is called before the first frame update
    void Start()
    {
        DefineCharacter(this); //Definieerd de character, gebruikt de json file

        DefineAI(this); //Voor primitive AI behaviour eventjes dit (15x basicattack tegen min voor alle npcs)




        CharacterEntity.status = "thrilled"; //Tests
        object[] QuirkArray = {"cursed", 3}; //syntax van zo'n quirk array
        CharacterEntity.quirks.Add(QuirkArray); //In de lijst
    }

    void Update()
    {
        DefineSelectBanners(this); //Definieer de banners die zeggen wat je moet selecten

        UpdateHighlight(this); //Elke frame wordt er gekeken naar of de character getarget is, zo ja, highlight aan

        EnableActionSubmit(this); //Elke frame wanneer je target wordt er gekeken of je het juiste aantal targets hebt, dan pas kan je submit klikken

        UpdateHP(this); //Update het hp van de characters

        ActionAccepted(this); //Als de action submit dan is ingedrukt, wordt de stuff gestuurd naar HandleAction

        if(CharacterActive(this)) //Does char have turn right now?
        {
            //Do your move
            RedirectAction(Actions[0]);

            //Loop through quirks and do their respective methods
            string[] QuirkMethods = LoopThroughQuirks(this);
            foreach (string MethodName in QuirkMethods) {
                RedirectQuirk(MethodName);
            }  //Invoke werk niet met args dus nu redirecten ;-; unity whyy (╯°□°)╯︵ ┻━┻
        }

        Death(this);
    }

    //Dit moet OnClick bij een skill guys!!!
    public void HandleTargeting(string ActionString)
    {
        InitiateTargeting(this, ActionString); //Initiation of targeting (defining vars)

        FreezeTargeting(this); //Only freezes target selection if action requires so, else it's free reign (FREEZE TARGETING IS NOG NIET GETEST BTW!!)
    }

    public void HandleAction()
    {
        Actions.Add(TempActionString); //De geselecteerde actie dan na het hele gebeuren in de premove list zetten,

        TargetsPerAction.Add(TickCounterObject.Targets.ToArray()); // met daarbij de bijbehorende targets
    }

    //Quirks
    public void RedirectQuirk(string QuirkMethodName)
    {
        switch(QuirkMethodName) //Hier van quirk naar method van quirk om quirk effecten te doen enz
        {
            case "Cursed":
                Cursed(this);
                break;
            case "Ablaze":
                Ablaze(this);
                break;
            //case enzovoort (succes hiermee haha)
            default:
                Debug.Log("Couldn't redirect quirk becasue of invalid QuirkMethodName");
                break;
        }
    }

    public void RedirectAction(string ActionName)
    {
        switch(ActionName) //Hier redirect naar actions, in de respectievelijke static scripts van de characters in Assets/Code/Statics/Characters
        {
            case "BasicAttack":
                BasicAttack(this);
                break;
            case "WorkHarder":
                WorkHarder(this);
                break;
            case "BodyCheck":
                BodyCheck(this);
                break;
            //case enzovoort (succes hiermee haha x2 want ctrl c ctrl v de goats)
            default:
                Debug.Log("Couldn't redirect action becasue of invalid ActionName");
                break;
        }
    }
}