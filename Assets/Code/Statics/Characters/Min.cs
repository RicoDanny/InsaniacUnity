using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;

public static class Min
{
    public static void MinSpecificAbility(CharacterBehaviour CallingCharacterBehaviour)
    {
        Debug.Log("Thank god!", CallingCharacterBehaviour.gameObject);
    }
}