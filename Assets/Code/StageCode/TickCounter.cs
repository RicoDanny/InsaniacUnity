using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main statics
using static TickCounterStatics;
using static UiStatics;
using static ActionStatics;

public class TickCounter : MonoBehaviour
{
    public int Tickcounter = 0;
    public List<GameObject> Units = new List<GameObject>();
    [SerializeField] private GameObject UnitCanvas;
    public List<CharacterBehaviour> Active = new List<CharacterBehaviour>(); //Here are the characters that have an action on this turn
    public List<CharacterBehaviour> Targets = new List<CharacterBehaviour>(); //List of characters that are targeted (Selected) on the UnitCanvas (Unfiltered)
    public bool Targeting;
    public List<Transform> MenusChildren  = new List<Transform>();
    public GameObject TargetAccept;
    public bool IsActionAccepted;
    public GameObject SubmitButton;
    public bool NewTick;
    public bool Frozen = false;
    public float TimeDiff = 0;
    public GameObject SelectTargetBanner;
    public GameObject SelectUnitBanner;
    public bool Selected = false;
    public GameObject GoodGuysObject;
    public GameObject BadGuysObject;
    public Sprite SkillSprite;
    public Sprite BackButtonSprite;
    public double TickInterval = 0.5;
    public int AllyCount = 0;
    public int EnemyCount = 0;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "0-2c")
        {
            ChosenCharacterStrings = new List<string>(){"Pygor", "Grungo"}; //Defealt team in case of bug ig
        }

        SpawnCharacters(this);

        Targeting = false;

        NewTick = true;

        DefineMenusChildren(this); //de menus van de characters even allemaal pakken zodat we die aan en uit kunnen zetten, denkend aan wie geselect is
        
        Units.AddRange(GameObject.FindGameObjectsWithTag("Units")); //Pakt elk character

        DefineTickSelectBanners(this); //Defined de UI banners dat zegt dat je dingetjes moet selecten
    }

    void Update()
    {
        UpdateSelectUnitBanner(this); //UI dingetjes updaten elke frame ("Select Unit"!!)

        UpdateTick(this); //Check of nieuwe tick en zo ja, Tickfunction

        if(PlayerWon(this))
        {
            SceneManager.LoadScene("WinScreen");
        }
        else if(PlayerLost(this))
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void Tickfunction()
    {
        LoopThroughUnits(this); //Op nieuwe tick gewoon door idereen heen loopen om te kijken wie aan de beurt is, mensjes energy te geven, mensen die aan de beurt zijn meteen te naaien met quirks, enz

        Tickcounter++; // verrassend

        NewTick = true; //NewTick is technisch gezien niet nodig vgm, je kan gwn if dingetjetime > 1 aanhouden, maarja
    }

    public void CancelAction()
    {
        Targeting = false; //Targeting uit want je wilt na cancellen weer gwn je units kunnen selecten

        Selected = false; // Deze is er om aan te duiden of de "select a unit!" banner aanmoet of niet, "Selected" komt voor op de raarste plekken maar het werkt

        UpdateBanners(this); //Zet de banners weer goed ("Select unit!")
    }

    public void AcceptAction()
    {
        IsActionAccepted = true; //Hetzelfde verhaal als hierboven alleen dan is isactionaccepted nu true want de action is geaccept :0!

        Targeting = false;

        Selected = false;
        
        UpdateBanners(this);
    }
}