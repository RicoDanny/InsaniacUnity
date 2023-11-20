using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] private GameObject TickCounterObject;

    private bool Enabled;

    public void Enable(GameObject FunctionInput)
    {
        Enabled = TickCounterObject.GetComponent<TickCounter>().Targets.Contains(FunctionInput.GetComponent<CharacterBehaviour>());


        if (!Enabled)
        {
            TickCounterObject.GetComponent<TickCounter>().Targets.Add(FunctionInput.GetComponent<CharacterBehaviour>());

            Enabled = true;
        }
        else
        {
            TickCounterObject.GetComponent<TickCounter>().Targets.Remove(FunctionInput.GetComponent<CharacterBehaviour>());

            Enabled = false;
        }
    }
}