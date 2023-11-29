using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ToggleActive : MonoBehaviour
{
    public bool active = false;

    public GameObject TargetingButtonsObject;

    private TickCounter TickCounterObject;

    public GameObject SelectUnitBanner;

    public GameObject SelectTargetBanner;

    void Start()
    {
        TickCounterObject = GameObject.Find("MainCanvas").GetComponent<TickCounter>();

        SelectUnitBanner = GameObject.Find("SelectUnit");

        SelectTargetBanner = GameObject.Find("SelectTargets");

        SelectTargetBanner.SetActive(false);

        
    }

    public void ToggleObjectActive(GameObject a)
    {
        if (!TickCounterObject.Targeting)
        {
            Transform parentTransform = GameObject.Find("Menus").transform;

            // Create a list to store children
            List<Transform> children = new List<Transform>();

            // Loop through each child and add it to the list
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                Transform child = parentTransform.GetChild(i);
                children.Add(child);
            }

            a.SetActive(!a.activeSelf);

            if(a.activeSelf){ TickCounterObject.Selected = true; }

            children.Remove(a.transform);

            children.Remove(TargetingButtonsObject.transform);

            children.ForEach(p => p.gameObject.SetActive(false));

            SelectUnitBanner.SetActive(!SelectUnitBanner.activeSelf);
        }
    }
}
