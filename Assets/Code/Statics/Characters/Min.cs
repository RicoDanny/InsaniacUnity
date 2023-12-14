using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static CharacterBehaviourStatics;
using static ActionStatics;

public static class Min
{
    public static void Swivel(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterEntity.hp += 10;
        
        EndAction(CallingCharacterBehaviour);
    }
}