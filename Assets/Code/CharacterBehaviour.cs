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

    private GameObject SelectTargetBanner;

    private GameObject SelectUnitBanner;

    [System.Serializable]
    public class Character
    {
        public string Name;
        public int hp;
        public int maxhp;
        public int sp;
        public int maxsp;
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
        public int loweredspcost;
        public int turn;
        public string status;
        public List<object[]> quirks = new List<object[]>(); //Dus voor elke character een lijst met daarin arrays van quirk naam en quirk effect duration to go: {["quirkname", int], ["quirkname", int]}
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

        CharacterEntity.turn = 0;

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

        //Voor primitive AI behaviour eventjes dit (15x basicattack tegen min voor alle npcs):
        if (this.gameObject.name != "Min")
        {
            for (int i = 0; i < 15; i++){
                Actions.Add("BasicAttack");
                TargetsPerAction.Add( new CharacterBehaviour[] {GameObject.Find("Min").GetComponent<CharacterBehaviour>()} );
            }
        }

        //Testen voor statuses en quirks. Voor de vibes:
        CharacterEntity.status = "thrilled";

        object[] QuirkArray = {"cursed", 3};

        CharacterEntity.quirks.Add(QuirkArray);

        Debug.Log(CharacterEntity.Name + ": " + CharacterEntity.quirks[0][0]);
        Debug.Log(CharacterEntity.Name + ": " + CharacterEntity.status);
    }

    void Update()
    {
        if(SelectTargetBanner == null)
        {
            SelectTargetBanner = transform.parent.gameObject.transform.parent.gameObject.GetComponent<ToggleActive>().SelectTargetBanner;

            SelectUnitBanner = transform.parent.gameObject.transform.parent.gameObject.GetComponent<ToggleActive>().SelectUnitBanner;
        }

        IsTarget = TickCounterObject.Targets.Contains(ThisCharacterBehaviour);

        //CharacterHPText.GetComponent<TMPro.TextMeshProUGUI>().text = StatText.hp.ToString();

        HighlightRenderer.color = new Color(255, 255, 255, 255 * (IsTarget ? 1 : 0) * (TickCounterObject.Targeting ? 1 : 0));

        if(User && TickCounterObject.IsActionAccepted)
        {
            User = false;

            TickCounterObject.IsActionAccepted = false;

            HandleAction();
        }

        if(TickCounterObject.Targeting && User) 
        {
            TickCounterObject.SubmitButton.SetActive(TargetingChecker(TargetingType));
        }

        if(TickCounterObject.Active.Contains(this) && Actions.Count > 0)
        {
            CharacterEntity.turn++;

            Invoke(Actions[0], 0);

            TickCounterObject.Active.Remove(this);
        }

        CharacterHPText.GetComponent<TMPro.TextMeshProUGUI>().text = CharacterEntity.hp.ToString();
    }

    //Quirks
    public void Quirks()
    {
        //Loop through quirks
        for (int i = 0; i < CharacterEntity.quirks.Count; i++)
        {
            object[] QuirkArray = CharacterEntity.quirks[i];
            string QuirkName = (string) QuirkArray[0];

            string MethodName = char.ToUpper(( (string) QuirkName)[0]) + ( (string) QuirkName).Substring(1); //Wrm eerste capitilized? ja leuk toch haha im losing my mind 
            
            Invoke(MethodName, 0);

            if (((int) CharacterEntity.quirks[i][1]) > 0)
            {
                CharacterEntity.quirks[i][1] = (int) CharacterEntity.quirks[i][1] - 1;
            }
            else
            {
                CharacterEntity.quirks.RemoveAt(i);
            }
        }
    }

    //Dit moet OnClick bij een skill guys!!!
    public void HandleTargeting(string ActionString)
    {
        SelectTargetBanner.SetActive(!SelectTargetBanner.activeSelf);

        TargetingType = NumberOfTargets.ContainsKey(ActionString) ? NumberOfTargets[ActionString] : -4; 

        TickCounterObject.Targets.Clear();

        TickCounterObject.Targeting = true;

        User = true;

        TempActionString = ActionString;

        TickCounterObject.MenusChildren.ForEach(p => p.gameObject.SetActive(false));

        TickCounterObject.TargetAccept.SetActive(true);

        if(TargetingType == -4) 
        {
            Debug.Log("You ain't cooking 💀");
        }
        else if(TargetingType == -3)
        {
            TickCounterObject.Frozen = true;
            TickCounterObject.Targets.Add(this);
        }
        else if(TargetingType == -2) 
        {
            TickCounterObject.Frozen = true;

            Transform parentTransform = GameObject.Find("BadGuys").transform;

            // Loop through each child and add it to the list
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                TickCounterObject.Targets.Add(child);
            }
        }
        else if(TargetingType == -1)
        {
            TickCounterObject.Frozen = true;

            Transform parentTransform = GameObject.Find("GoodGuys").transform;

            // Loop through each child and add it to the list
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                TickCounterObject.Targets.Add(child);
            }
        }
        else if(TargetingType == 0)
        {
            TickCounterObject.Frozen = true;

            Transform parentTransform = GameObject.Find("BadGuys").transform;

            // Loop through each child and add it to the list
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                TickCounterObject.Targets.Add(child);
            }

            parentTransform = GameObject.Find("GoodGuys").transform;

            // Loop through each child and add it to the list
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                CharacterBehaviour child = parentTransform.GetChild(i).gameObject.GetComponent<CharacterBehaviour>();
                TickCounterObject.Targets.Add(child);
            }
        }
    }

    public bool TargetingChecker(int TargetingInt) 
    {
        if(TargetingInt > 0 && TickCounterObject.Targets.Count == TargetingInt) // alles 1 of hoger is amount of targets 
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

    //Dictionary of targets per every action
    Dictionary<string, int> NumberOfTargets = new Dictionary<string, int>
    {
        { "BasicAttack", 1 },
        { "WorkHarder", -3 },
        { "BodyCheck", 1 }
    };

    public void BasicAttack()
    {
        CharacterBehaviour[] TargetBattlerList = TargetsPerAction[0];
        Character UserBattler = CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;


        //Dit per target (met meerdere targets gebruik foreach)
        int DMG = UserBattler.atk - TargetBattler.def;

        int BaseDMG = 1;

        if (DMG < 1)
        {
            DMG = BaseDMG;
        }

        if (TargetBattler.hp - DMG < 0)
        {
            DMG = TargetBattler.hp;
            Debug.Log("Breh dead", TargetBattlerList[0].gameObject);
        }

        TargetBattler.hp -= DMG;

        //Dit onder elke action
        Actions.RemoveAt(0);

        TargetsPerAction.RemoveAt(0);
    }

    public void WorkHarder()
    {
        CharacterBehaviour[] TargetBattlerList = TargetsPerAction[0];
        Character UserBattler = CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;


        //Dit per target (met meerdere targets gebruik foreach)
        UserBattler.atk += 1;
        UserBattler.luck += 2;
        UserBattler.maxsp += 10;
        int spcost = 6 - UserBattler.loweredspcost;

        //Dit onder elke action
        Actions.RemoveAt(0);

        TargetsPerAction.RemoveAt(0);
    }

    public void BodyCheck()
    {
        CharacterBehaviour[] TargetBattlerList = TargetsPerAction[0];
        Character UserBattler = CharacterEntity;

        //Je weet dat in basic attack er maar 1 target is dus
        Character TargetBattler = TargetBattlerList[0].CharacterEntity;


        //Dit per target (met meerdere targets gebruik foreach)
        int DMG = (int)(UserBattler.atk - TargetBattler.def * 0.5);
        int spcost = 4 - UserBattler.loweredspcost;
        int BaseDMG = 1;

        if (TargetBattler.hp > TargetBattler.maxhp * 0.5)
        {
            double CRIT = 1.5;
        }


        if (DMG < 1)
        {
            DMG = BaseDMG;
        }

        if (TargetBattler.hp - DMG < 0)
        {
            DMG = TargetBattler.hp;
            Debug.Log("Breh dead", TargetBattlerList[0].gameObject);
        }

        TargetBattler.hp -= DMG;

        //Dit onder elke action
        Actions.RemoveAt(0);

        TargetsPerAction.RemoveAt(0);
    }

    //Quirks
}