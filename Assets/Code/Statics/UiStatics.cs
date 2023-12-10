using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main statics
using static ActionStatics;

public static class  UiStatics
{
    public static void UpdateHighlight(CharacterBehaviour CallingCharacterBehaviour)
    {
        CallingCharacterBehaviour.HighlightRenderer.color = new Color(255, 255, 255, 255 * (CallingCharacterBehaviour.TickCounterObject.Targets.Contains(CallingCharacterBehaviour) ? 1 : 0) * (CallingCharacterBehaviour.TickCounterObject.Targeting ? 1 : 0));
    }

    public static void DefineSelectBanners(CharacterBehaviour CallingCharacterBehaviour)
    {
        if(CallingCharacterBehaviour.SelectTargetBanner == null)
        {
            CallingCharacterBehaviour.SelectTargetBanner = CallingCharacterBehaviour.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ToggleActive>().SelectTargetBanner;

            CallingCharacterBehaviour.SelectUnitBanner = CallingCharacterBehaviour.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ToggleActive>().SelectUnitBanner;
        }
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
        CallingCharacterBehaviour.CharacterHPText.GetComponent<TMPro.TextMeshProUGUI>().text = CallingCharacterBehaviour.CharacterEntity.hp.ToString();
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