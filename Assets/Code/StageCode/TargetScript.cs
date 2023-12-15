using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] private GameObject TickCounterObject;

    private bool Enabled;

    private TickCounter TickCounterScript;

    private CharacterBehaviour CurrentCharacterBehaviour;
    public void Enable(GameObject FunctionInput)
    {
        CurrentCharacterBehaviour = FunctionInput.GetComponent<CharacterBehaviour>();

            TickCounterScript = TickCounterObject.GetComponent<TickCounter>();
            
            Enabled = TickCounterScript.Targets.Contains(CurrentCharacterBehaviour);
            
        if(!TickCounterScript.Frozen) 
        {
            if (!Enabled)
            {
                TickCounterScript.Targets.Add(CurrentCharacterBehaviour);

                Enabled = true;
            }
            else
            {
                TickCounterScript.Targets.Remove(CurrentCharacterBehaviour);

                Enabled = false;
            }
        }
    }
}