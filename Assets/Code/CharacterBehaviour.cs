using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBehaviour : MonoBehaviour
{
    private string battlerName;

    [SerializeField] private GameObject CharacterHPText;

    public Battler battlerEntity;

    private List<string> Actions; //List of premoves of Abilities/Actions

    private List<CharacterBehaviour[]> TargetsPerAction; //List of Targets that are on the same Indexes as the Actions that they are Targets for in the Actions list

    private GameObject HighlightObject;

    private SpriteRenderer HighlightRenderer;

    private TickCounter TickCounterObject;

    private bool IsTarget;

    private bool User;

    private string TempActionString;

    private CharacterBehaviour ThisCharacterBehaviour;

    public struct Battler
    {
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

        //parameterized constructor
        public Battler(int healthStat, int skillStat, int attackStat, int defenseStat, int speedStat, int energyStat, int hitStat, int avoidStat, int avoidPoints, int luckStat, int luckPoints, int critStat, int critPoints)
        {

            hp = healthStat;
            sp = skillStat;
            atk = attackStat;
            def = defenseStat;
            spd = speedStat;
            energy = energyStat;
            hit = hitStat;
            avo = avoidStat;
            avop = avoidPoints;
            luck = luckStat;
            luckp = luckPoints;
            crit = critStat;
            critp = critPoints;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        battlerName = transform.name;

        switch (battlerName)
        {
            case "Pygor":
                battlerEntity = new Battler(27, 15, 9, 6, 14, 0, 6, 18, 0, 8, 0, 0, 0);
                break;
            case "Min":
                battlerEntity = new Battler(22, 20, 7, 5, 13, 0, 18, 36, 0, 9, 0, 0, 0);
                break;
            case "Grungo":
                battlerEntity = new Battler(30, 18, 8, 7, 11, 0, 24, 18, 0, 0, 0, 0, 0);
                break;
            case "Freddy":
                battlerEntity = new Battler(22, 15, 9, 7, 14, 0, 12, 24, 0, 9, 0, 0, 0);
                break;
            case "Dough":
                battlerEntity = new Battler(25, 20, 11, 5, 12, 0, 0, 18, 0, 10, 0, 0, 0);
                break;
            case "Tan":
                battlerEntity = new Battler(23, 17, 10, 6, 15, 0, 6, 0, 0, 9, 0, 0, 0);
                break;
            case "Capri":
                battlerEntity = new Battler(20, 25, 9, 6, 13, 0, 12, 24, 0, 9, 0, 0, 0);
                break;
            case "Freckle":
                battlerEntity = new Battler(24, 17, 11, 6, 12, 0, 12, 30, 0, 7, 0, 0, 0);
                break;
            case "LaVigneSuspecte":
                battlerEntity = new Battler(25, 6, 8, 2, 18, 0, 9, 0, 0, 0, 0, 0, 0);
                break;
            case "Fantolectrique":
                battlerEntity = new Battler(12, 15, 10, 15, 22, 0, 12, 0, 0, 0, 0, 0, 0);
                break;
        }

        //Debug.Log(battlerEntity.energy); //Legacy Item

        HighlightObject = this.gameObject.transform.Find("Highlight").gameObject;

        ThisCharacterBehaviour = transform.gameObject.GetComponent<CharacterBehaviour>();

        TickCounterObject = GameObject.Find("MainCanvas").GetComponent<TickCounter>();

        HighlightRenderer = HighlightObject.GetComponent<SpriteRenderer>();

        Actions = new List<string>();

        TargetsPerAction = new List<CharacterBehaviour[]>();
    }

    public Battler GetBattler()
    {
        return battlerEntity;
    }

    void Update()
    {
        IsTarget = TickCounterObject.Targets.Contains(ThisCharacterBehaviour);

        CharacterHPText.GetComponent<TMPro.TextMeshProUGUI>().text = battlerEntity.hp.ToString();

        HighlightRenderer.color = new Color(255, 255, 255, 255 * (IsTarget ? 1 : 0) * (TickCounterObject.Targeting ? 1 : 0));

        if(User && TickCounterObject.IsActionAccepted)
        {
            User = false;

            TickCounterObject.IsActionAccepted = false;

            HandleAction();
        }

        if(TickCounterObject.Active.Contains(this) && Actions.Count > 0)
        {
            Invoke(Actions[0], 0);

            TickCounterObject.Active.Remove(this);

            Actions.RemoveAt(0);

            TargetsPerAction.RemoveAt(0);
        }
    }

    public void HandleTargeting(string ActionString)
    {
        //-2: Ally team

        //-1: Enemy team

        //0: All

        // alles 1 of hoger is amount of targets

        System.Reflection.MethodInfo ActionMethod = this.GetType().GetMethod("Targets" + ActionString);

        int TargetingType = (int) ActionMethod.Invoke(this, null);

        TickCounterObject.Targets.Clear();

        TickCounterObject.Targeting = true;

        User = true;

        TempActionString = ActionString;

        TickCounterObject.MenusChildren.ForEach(p => p.gameObject.SetActive(false));

        TickCounterObject.TargetAccept.SetActive(true);
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