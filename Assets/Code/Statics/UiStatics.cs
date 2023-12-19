using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics 
using static ActionStatics;

public static class  UiStatics
{
    public static void UpdateHighlight(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.HighlightRenderer.color = new Color(0, 255, 255, 255 * (CallingCharacterBehaviour.TickCounterObject.Targets.Contains(CallingCharacterBehaviour) ? 1 : 0) * (CallingCharacterBehaviour.TickCounterObject.Targeting ? 1 : 0));
    }

    public static void UpdateActiveHighlight(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.ActiveHighlightRenderer.color = new Color(255, 255, 0, 255 * (CallingCharacterBehaviour.TickCounterObject.Active.Contains(CallingCharacterBehaviour) ? 1 : 0));
    }

    public static void EnableActionSubmit(CharacterBehaviour CallingCharacterBehaviour)
    {
        if(CallingCharacterBehaviour.TickCounterObject.Targeting && CallingCharacterBehaviour.User) 
        {
            CallingCharacterBehaviour.TickCounterObject.SubmitButton.SetActive(TargetingChecker(CallingCharacterBehaviour, CallingCharacterBehaviour.TargetingType));
        }
    }

    public static void UpdateHP(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterHPText.GetComponent<TMPro.TextMeshProUGUI>().text = "HP: " + (CallingCharacterBehaviour.CharacterEntity.hp).ToString();
    }

    public static void UpdateSP(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterSPText.GetComponent<TMPro.TextMeshProUGUI>().text = "SP: " + (CallingCharacterBehaviour.CharacterEntity.sp).ToString();
    }

    public static void UpdateATK(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterATKText.GetComponent<TMPro.TextMeshProUGUI>().text = "ATK: " + (CallingCharacterBehaviour.CharacterEntity.atk + CallingCharacterBehaviour.CharacterEntity.atkboost).ToString();
    }

    public static void UpdateDEF(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterDEFText.GetComponent<TMPro.TextMeshProUGUI>().text = "DEF: " + (CallingCharacterBehaviour.CharacterEntity.def+CallingCharacterBehaviour.CharacterEntity.defboost).ToString();
    }

    public static void UpdateSPD(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterSPDText.GetComponent<TMPro.TextMeshProUGUI>().text = "SPD: " + (CallingCharacterBehaviour.CharacterEntity.spd).ToString();
    }

    public static void UpdateHIT(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterHITText.GetComponent<TMPro.TextMeshProUGUI>().text = "HIT: " + CallingCharacterBehaviour.CharacterEntity.hit.ToString();
    }

    public static void UpdateAVO(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterAVOText.GetComponent<TMPro.TextMeshProUGUI>().text = "AVO: " + CallingCharacterBehaviour.CharacterEntity.avo.ToString();
    }

    public static void UpdateLUCK(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.CharacterLUCKText.GetComponent<TMPro.TextMeshProUGUI>().text = "LUCK: " + CallingCharacterBehaviour.CharacterEntity.luck.ToString();
    }

    public static void UpdateSelectUnitBanner(TickCounter CallingTickCounter)
    {
        CallingTickCounter.SelectUnitBanner.SetActive(!CallingTickCounter.Selected);
    }

    public static void UpdateBanners(TickCounter CallingTickCounter)
    {
        CallingTickCounter.TargetAccept.SetActive(false);

        CallingTickCounter.SelectTargetBanner.SetActive(false);
    }
}