using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

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
    public GameObject SkillList;

    // Start is called before the first frame update
    void Start()
    {
        DefineCharacter(this); //Definieerd de character, gebruikt de json file

        if(IsGoodGuy(this)){
            SpawnSkillList(this);
        }

        DefineAI(this); //Voor primitive AI behaviour eventjes dit (15x basicattack tegen min voor alle npcs)

        CharacterEntity.status = "thrilled"; //Tests
        object[] QuirkArray = {"cursed", 3, 1}; //syntax van zo'n quirk array
        CharacterEntity.quirks.Add(QuirkArray); //In de lijst

        Debug.Log( StatusChart[ MatchupNum[CharacterEntity.status], MatchupNum["vexed"] ] );
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
            CallStaticFunction(Actions[0], this, "Action");

            //Loop through quirks and do their respective methods
            string[] QuirkMethods = LoopThroughQuirks(this);
            foreach (string MethodName in QuirkMethods) {
                CallStaticFunction(MethodName, this, "Quirk");
            }
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

    public void CallStaticFunction(string functionName, CharacterBehaviour character, string functionType)
    {
        object[] parameters = new object[] { character }; // Parameters for the function call

        Type staticClassType = null;


        if(functionType == "Action"){
            if(functionName != "BasicAttack"){
                switch(name)
                {
                    case "Min":
                        staticClassType = typeof(Min); 
                        break;
                    case "Pygor":
                        staticClassType = typeof(Pygor); 
                        break;
                    case "Grungo":
                        staticClassType = typeof(Pygor); 
                        break;
                    default:
                        staticClassType = typeof(ActionStatics); 
                        break;
                }
            }
            else
            {
                staticClassType = typeof(ActionStatics); 
            }
        }
        else if (functionType == "Quirk")
        {
            staticClassType = typeof(QuirkStatics); 
        }
        else //dus hier is het geen quirk of action, dan is het kansloos
        {
            staticClassType = typeof(CharacterBehaviour);
        }

        MethodInfo method = staticClassType.GetMethod(functionName, BindingFlags.Public | BindingFlags.Static);

        if (method != null)
        {
            method.Invoke(null, parameters);
        }
        else
        {
            Debug.LogError("Static method " + functionName + " not found in " + staticClassType.Name);
        }
    }
}