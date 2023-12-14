using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSkillSP : MonoBehaviour
{
    public GameObject CharacterObject;
    public int RequiredSP;
    public Button SkillButton;

    // Update is called once per frame
    void Update()
    {
        SkillButton.interactable = (CharacterObject.GetComponent<CharacterBehaviour>().CharacterEntity.sp >= RequiredSP);
    }
}
