using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  ModifierStatics
{
    [System.Serializable]
    public class Modifier
    {
        public int totalduration;
        public int duration;
        public int atkboost = 0;
        public int defboost = 0;
        public int dmgboost = 0;
        public float atkmultiplier = 1.0f;
        public float defmultiplier = 1.0f;
        public float luckymultiplier = 1.0f;
        public float critmultiplier = 1.0f;
        public float guardmultiplier = 1.0f;
    }

    public static void LoopThroughModifiers(CharacterBehaviour CallingCharacterBehaviour)
    {
        foreach(Modifier modifier in CallingCharacterBehaviour.CharacterEntity.modifiers)
        {
            if(modifier.duration == modifier.totalduration)
            {
                CallingCharacterBehaviour.CharacterEntity.atkboost += modifier.atkboost;
                CallingCharacterBehaviour.CharacterEntity.defboost += modifier.defboost;
                CallingCharacterBehaviour.CharacterEntity.dmgboost += modifier.dmgboost;

                CallingCharacterBehaviour.CharacterEntity.atkmultiplier *= modifier.atkmultiplier;
                CallingCharacterBehaviour.CharacterEntity.defmultiplier *= modifier.defmultiplier;
                CallingCharacterBehaviour.CharacterEntity.luckymultiplier *= modifier.luckymultiplier;
                CallingCharacterBehaviour.CharacterEntity.critmultiplier *= modifier.critmultiplier;
                CallingCharacterBehaviour.CharacterEntity.guardmultiplier *= modifier.guardmultiplier;
            }

            if(modifier.duration == 0)
            {
                CallingCharacterBehaviour.CharacterEntity.atkboost -= modifier.atkboost;
                CallingCharacterBehaviour.CharacterEntity.defboost -= modifier.defboost;
                CallingCharacterBehaviour.CharacterEntity.dmgboost -= modifier.dmgboost;

                CallingCharacterBehaviour.CharacterEntity.atkmultiplier /= modifier.atkmultiplier;
                CallingCharacterBehaviour.CharacterEntity.defmultiplier /= modifier.defmultiplier;
                CallingCharacterBehaviour.CharacterEntity.luckymultiplier /= modifier.luckymultiplier;
                CallingCharacterBehaviour.CharacterEntity.critmultiplier /= modifier.critmultiplier;
                CallingCharacterBehaviour.CharacterEntity.guardmultiplier /= modifier.guardmultiplier;

                CallingCharacterBehaviour.CharacterEntity.modifiers.Remove(modifier);
            }
            else
            {
                modifier.duration -= 1;
            }
        }
    }
}