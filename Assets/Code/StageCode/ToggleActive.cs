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
        TickCounterObject = GameObject.Find("Canvas").GetComponent<TickCounter>();

        SelectUnitBanner = GameObject.Find("SelectUnit");

        SelectTargetBanner = GameObject.Find("SelectTargets");

        SelectTargetBanner.SetActive(false);

        
    }

    public void ToggleObjectActive(GameObject a)
    {
        if (!TickCounterObject.Targeting)
        {
            Transform parentTransform = GameObject.Find("BattleUI").transform;

            // Create a list to store children
            List<Transform> children = new List<Transform>();

            // Loop through each child and add it to the list
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                Transform child = parentTransform.GetChild(i);
                children.Add(child);
            }

            a.SetActive(!a.activeSelf);

            children.Remove(a.transform);

            children.Remove(TargetingButtonsObject.transform);

            children.ForEach(p => p.gameObject.SetActive(false));

            TickCounterObject.Selected = a.activeSelf;

            SelectUnitBanner.SetActive(!SelectUnitBanner.activeSelf);
        }
    }
}
