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

    public bool Targeting = false;

    public List<Transform> MenusChildren;

    public GameObject TargetAccept;

    public bool IsActionAccepted;

    public bool NewTick;

    private JsonReader.myCharacterList.character CharacterInfo;

    void Start()
    {
        Transform parentTransform = GameObject.Find("Menus").transform;

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
    }

    void Update()
    {
        if(NewTick == true && Active.Count == 0)
        {
            NewTick = false;

            Tickfunction();
        }
    }

    private void Tickfunction()
    {
        // Loop through the array of characters and do something with them.
        foreach (GameObject character in Units)
        {
            JsonReader characterScript = character.GetComponent<JsonReader>();

            if (CharacterInfo.energy >= 60) {
                Active.Add(characterScript);

                CharacterInfo.energy -= 60;
            }

            CharacterInfo.energy += CharacterInfo.spd;
        }

        Tickcounter++;

        NewTick = true;
    }

    public void CancelAction()
    {
        Targeting = false;

        TargetAccept.SetActive(false);
    }

    public void AcceptAction()
    {
        IsActionAccepted = true;

        Targeting = false;

        TargetAccept.SetActive(false);
    }
}
