using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject CharacterHPText;

    private List<string> Actions; //List of premoves of Abilities/Actions

    private List<CharacterBehaviour[]> TargetsPerAction; //List of Targets that are on the same Indexes as the Actions that they are Targets for in the Actions list

    private GameObject HighlightObject;

    private SpriteRenderer HighlightRenderer;

    private CharacterBehaviour ThisCharacterBehaviour;

    private TickCounter TickCounterObject;

    public Character CharacterEntity;

    private int TargetingType;

    private bool IsTarget;

    private bool User;

    private string TempActionString;

    public TextAsset textJSON;

    [System.Serializable]
    public class Character
    {
        public string Name;
        public int hp;
        public int sp;
        public int atk;
        public int def;
        public int spd;
        public int energy;
        public int hit;
        public int avo;
        public int avop;
        public int luck;
        public int luckp;
        public int crit;
        public int critp;
        public int prime;
    }

    [System.Serializable]
    public class CharacterList
    {
        public Character[] character;
    }

    public CharacterList myCharacterList = new CharacterList();

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(battlerEntity.energy); //Legacy Item

        myCharacterList = JsonUtility.FromJson<CharacterList>(textJSON.text);
        
        HighlightObject = this.gameObject.transform.Find("Highlight").gameObject;

        ThisCharacterBehaviour = transform.gameObject.GetComponent<CharacterBehaviour>();

        TickCounterObject = GameObject.Find("MainCanvas").GetComponent<TickCounter>();

        HighlightRenderer = HighlightObject.GetComponent<SpriteRenderer>();

        Actions = new List<string>();

        TargetsPerAction = new List<CharacterBehaviour[]>();

        foreach(Character JsonCharacter in myCharacterList.character) 
        {
            if(JsonCharacter.Name == transform.name) 
            {
                CharacterEntity = JsonCharacter;
            }
        }
    }

    void Update()
    {
        IsTarget = TickCounterObject.Targets.Contains(ThisCharacterBehaviour);

        //CharacterHPText.GetComponent<TMPro.TextMeshProUGUI>().text = StatText.hp.ToString();

        HighlightRenderer.color = new Color(255, 255, 255, 255 * (IsTarget ? 1 : 0) * (TickCounterObject.Targeting ? 1 : 0));

        if(User && TickCounterObject.IsActionAccepted)
        {
            User = false;

            TickCounterObject.IsActionAccepted = false;

            HandleAction();
        }

        if(TickCounterObject.Targeting) 
        {
            TickCounterObject.SubmitButton.SetActive(TargetingChecker(TargetingType));
        }

        if(TickCounterObject.Active.Contains(this) && Actions.Count > 0)
        {
            Invoke(Actions[0], 0);

            TickCounterObject.Active.Remove(this);

            Actions.RemoveAt(0);

            TargetsPerAction.RemoveAt(0);
        }

        CharacterHPText.GetComponent<TMPro.TextMeshProUGUI>().text = CharacterEntity.hp.ToString();
    }

    public void HandleTargeting(string ActionString)
    {
        System.Reflection.MethodInfo ActionMethod = this.GetType().GetMethod("Targets" + ActionString);

        TargetingType = (int) ActionMethod.Invoke(this, null);

        TickCounterObject.Targets.Clear();

        TickCounterObject.TargetsFactor = 1;

        TickCounterObject.Targeting = true;

        User = true;

        TempActionString = ActionString;

        TickCounterObject.MenusChildren.ForEach(p => p.gameObject.SetActive(false));

        TickCounterObject.TargetAccept.SetActive(true);
    }

    public bool TargetingChecker(int TargetingInt) 
    {
        if(TargetingInt == -2) //-2: Ally team
        {
            if(TickCounterObject.TargetsFactor == TickCounterObject.GoodGuysFactor) 
            {
                return true;
            }

            return false;
        }
        if(TargetingInt == -1) //-1: Enemy team
        {
            if(TickCounterObject.TargetsFactor == TickCounterObject.BadGuysFactor) 
            {
                return true;
            }

            return false;
        }
        if(TargetingInt == 0) //0: All
        {
            if(TickCounterObject.TargetsFactor == TickCounterObject.GoodGuysFactor * TickCounterObject.BadGuysFactor) 
            {
                return true;
            }

            return false;
        }

        if(TickCounterObject.Targets.Count == TargetingInt) // alles 1 of hoger is amount of targets 
        {
            return true;
        }

        return false;
    }

    public void HandleAction()
    {
        Actions.Add(TempActionString);

        TargetsPerAction.Add(TickCounterObject.Targets.ToArray());
    }

    //Attacks

    public int TargetsBasicAttack()
    {
        return 1;
    }

    public void BasicAttack()
    {
        //UserScript userScript = UnitCanvas.GetComponent<UserScript>();

        //TargetScript targetScript = UnitCanvas.GetComponent<TargetScript>();

        //CharacterBehaviour TargetBattler = targetScript.Target.GetComponent<CharacterBehaviour>();
        //CharacterBehaviour UserBattler = userScript.User.GetComponent<CharacterBehaviour>();

        //GameObject TargetBattler = TargetsPerAction[0][0];
        
        //int DMG = battlerEntity.atk - TargetBattler.battlerEntity.def;

        //int BaseDMG = 1;

        //if (DMG < 1)
        //{
        //    DMG = BaseDMG;
        //}

        //if (TargetBattler.battlerEntity.hp - DMG < 0)
        //{
        //    DMG = TargetBattler.battlerEntity.hp;
        //}

        //TargetBattler.battlerEntity.hp -= DMG;
    }
}