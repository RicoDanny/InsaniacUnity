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
using static ModifierStatics;
using static EmoteStatics;

//Character statics
using static Min;
using static Pygor;
using static Grungo;
using static Freddy;
    //enz.

//Enemy statics
using static LaVigneSuspecte;
using static Fantolectrique;

public class CharacterBehaviour : MonoBehaviour
{
    public GameObject CharacterHPText;
    public GameObject CharacterSPText;
    public GameObject CharacterATKText;
    public GameObject CharacterDEFText;
    public GameObject CharacterSPDText;
    public GameObject CharacterHITText;
    public GameObject CharacterAVOText;
    public GameObject CharacterLUCKText;
    public List<string> Actions; //List of premoves of Abilities/Actions
    public List<CharacterBehaviour[]> TargetsPerAction; //List of Targets that are on the same Indexes as the Actions that they are Targets for in the Actions list
    public GameObject HighlightObject;
    public SpriteRenderer HighlightRenderer;
    public SpriteRenderer ActiveHighlightRenderer;
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
    public GameObject EmoteList;

    // Start is called before the first frame update
    void Start()
    {
        DefineCharacter(this); //Definieerd de character, gebruikt de json file

        if(IsGoodGuy(this))
        {
            SpawnSkillList(this);
            SpawnEmoteList(this);
        }
    }

    void Update()
    {
        DefineSelectBanners(this); //Definieer de banners die zeggen wat je moet selecten

        UpdateHighlight(this); //Elke frame wordt er gekeken naar of de character getarget is, zo ja, highlight aan
        UpdateActiveHighlight(this);

        EnableActionSubmit(this); //Elke frame wanneer je target wordt er gekeken of je het juiste aantal targets hebt, dan pas kan je submit klikken

        //Update de stats van de characters
        UpdateHP(this);
        
        if(IsGoodGuy(this))
        {
            UpdateSP(this);
            UpdateATK(this);
            UpdateDEF(this);
            UpdateSPD(this);
            UpdateHIT(this);
            UpdateAVO(this);
            UpdateLUCK(this);
        }
        else
        {
            AImove(this);
        }

        ActionAccepted(this); //Als de action submit dan is ingedrukt, wordt de stuff gestuurd naar HandleAction

        if(CharacterActive(this)) //Does char have turn right now?
        {
            //Loop through quirks and do their respective methods
            string[] QuirkMethods = LoopThroughQuirks(this);
            foreach (string CharacterQuirk in QuirkMethods) 
            {
                CallStaticQuirk(CharacterQuirk, this);
            }

            //Loop through modifiers and inflict em
            LoopThroughModifiers(this);

            //Do your move
            CallStaticAction(Actions[0], this); //Psst, Ga in de toekomst [turns] als je niet wilt dat de turns worden verwijderd! (Non-Removal-Based NRM voor nerds (Moet ook in EndAction veranderen))
        }

        Death(this);
    }

    //Dit moet OnClick bij een skill guys!!!
    public void HandleTargeting(string ActionString)
    {
        InitiateTargeting(this, ActionString); //Initiation of targeting (defining vars)

        FreezeTargeting(this); //Only freezes target selection if action requires so, else it's free reign
    }

    public void HandleAction()
    {
        Actions.Add(TempActionString); //De geselecteerde actie dan na het hele gebeuren in de premove list zetten,

        TargetsPerAction.Add(TickCounterObject.Targets.ToArray()); // met daarbij de bijbehorende targets
    }

    public void CallStaticAction(string functionName, CharacterBehaviour character)
    {
        object[] parameters;
        
        // Parameters for the function call

        Type staticClassType = null;

        parameters = new object[] { character };

        if(functionName != "BasicAttack"){

            bool IsEmote = false;

            foreach(Skill Emote in Emotes)
            {
                if(functionName == Emote.name)
                {
                    IsEmote = true;
                }
            }

            if(IsEmote)
            {
                staticClassType = typeof(EmoteStatics);
            }
            else
            {
                staticClassType = Type.GetType(name);
            }
        }
        else
        {
            staticClassType = typeof(ActionStatics); 
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

    public void CallStaticQuirk(string CharacterQuirk, CharacterBehaviour character)
    {
        object[] parameters;
        
        // Parameters for the function call

        Type staticClassType = null;

        parameters = new object[] { character };
        staticClassType = typeof(QuirkStatics); 

        MethodInfo method = staticClassType.GetMethod(CharacterQuirk, BindingFlags.Public | BindingFlags.Static);

        if (method != null)
        {
            method.Invoke(null, parameters);
        }
        else
        {
            Debug.LogError("Static method " + CharacterQuirk + " not found in " + staticClassType.Name);
        }
    }
}