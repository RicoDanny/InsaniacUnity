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

//Character statics
using static Min;
using static Pygor;
using static Grungo;
    //enz.

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

        if(IsGoodGuy(this))
        {
            SpawnSkillList(this);
        }

        DefineAI(this); //Voor primitive AI behaviour eventjes dit (15x basicattack tegen min voor alle npcs)

        CharacterEntity.status = "thrilled"; //Tests

        Debug.Log( StatusChart[ MatchupNum[CharacterEntity.status], MatchupNum["vexed"] ] );
    }

    void Update()
    {
        DefineSelectBanners(this); //Definieer de banners die zeggen wat je moet selecten

        UpdateHighlight(this); //Elke frame wordt er gekeken naar of de character getarget is, zo ja, highlight aan

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

        ActionAccepted(this); //Als de action submit dan is ingedrukt, wordt de stuff gestuurd naar HandleAction

        if(CharacterActive(this)) //Does char have turn right now?
        {
            //Loop through quirks and do their respective methods
            Quirk[] QuirkMethods = LoopThroughQuirks(this);
            foreach (Quirk CharacterQuirk in QuirkMethods) 
            {
                CallStaticQuirk(char.ToUpper(( (string) CharacterQuirk.name)[0]) + ( (string) CharacterQuirk.name).Substring(1), this, CharacterQuirk);
            }

            //Loop through modifiers and do their respective methods
            Modifier[] ModifierMethods = LoopThroughModifiers(this);
            foreach (Modifier CharacterModifier in ModifierMethods) 
            {
                CallStaticModifier(char.ToUpper(( (string) CharacterModifier.name)[0]) + ( (string) CharacterModifier.name).Substring(1), this, CharacterModifier);
            }

            //Do your move
            CallStaticAction(Actions[0], this);
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

    public void CallStaticAction(string functionName, CharacterBehaviour character)
    {
        object[] parameters;
        
        // Parameters for the function call

        Type staticClassType = null;

        parameters = new object[] { character };

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

    public void CallStaticQuirk(string functionName, CharacterBehaviour character, Quirk CharacterQuirk)
    {
        object[] parameters;
        
        // Parameters for the function call

        Type staticClassType = null;

        parameters = new object[] { character, CharacterQuirk };
        staticClassType = typeof(QuirkStatics); 

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

    public void CallStaticModifier(string functionName, CharacterBehaviour character, Modifier CharacterModifier)
    {
        object[] parameters;
        
        // Parameters for the function call

        Type staticClassType = null;

        parameters = new object[] { character, CharacterModifier };
        staticClassType = typeof(ModifierStatics); 

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