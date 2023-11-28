using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TickCounter : MonoBehaviour
{
    public int Tickcounter = 0;

    private GameObject[] Units;

    [SerializeField] private GameObject UnitCanvas;

    public List<CharacterBehaviour> Active; //Here are the characters that have an action on this turn

    public List<CharacterBehaviour> Targets; //List of characters that are targeted (Selected) on the UnitCanvas (Unfiltered)

    public bool Targeting;

    public List<Transform> MenusChildren;

    public GameObject TargetAccept;

    public bool IsActionAccepted;
    
    public GameObject SubmitButton;

    public bool NewTick;

    public bool Frozen = false;

    private float TimeDiff = 0;

    private GameObject SelectTargetBanner;

    private GameObject SelectUnitBanner;

    void Start()
    {
        Targeting = false;

        Transform parentTransform = GameObject.Find("BattleUI").transform;

        // Create a list to store MenusChildren
        MenusChildren = new List<Transform>();

        // Loop through each child and add it to the list
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);
            MenusChildren.Add(child);
        }
        
        // Find all game objects with the "Units" tag and store them in an array.
        Units = GameObject.FindGameObjectsWithTag("Units");

        Active = new List<CharacterBehaviour>();

        Targets = new List<CharacterBehaviour>();

        NewTick = true;

        SelectTargetBanner = GameObject.Find("SelectTargets");

        SelectUnitBanner = GameObject.Find("SelectUnit");
    }

    void Update()
    {
        TimeDiff += Time.deltaTime;

        if(NewTick == true && Active.Count == 0 && TimeDiff > 1)
        {
            TimeDiff = 0;

            NewTick = false;

            Tickfunction();
        }
    }

    private void Tickfunction()
    {
        // Loop through the array of characters and do something with them.
        foreach (GameObject character in Units)
        {
            CharacterBehaviour characterScript = character.GetComponent<CharacterBehaviour>();

            if (characterScript.CharacterEntity.energy >= 60) {
                Active.Add(characterScript);

                characterScript.CharacterEntity.energy -= 60;
            }

            characterScript.CharacterEntity.energy += characterScript.CharacterEntity.spd;
        }

        Tickcounter++;

        NewTick = true;
    }

    public void CancelAction()
    {
        Targeting = false;

        TargetAccept.SetActive(false);

        SelectTargetBanner.SetActive(false);

        SelectUnitBanner.SetActive(true);
    }

    public void AcceptAction()
    {
        IsActionAccepted = true;

        Targeting = false;

        TargetAccept.SetActive(false);

        SelectTargetBanner.SetActive(false);

        SelectUnitBanner.SetActive(true);
    }
}
