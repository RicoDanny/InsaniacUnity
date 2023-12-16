using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBattleMinSpeech : MonoBehaviour
{
    public TMPro.TextMeshProUGUI  MinSpeechTextBox;
    public GameObject MinNameTag;
    public List<string> MinSpeech = new List<string>()
        {
            "“Grrrreetings my loyal supporters! Excuse me for my brief departure, I needed to <i>heighten my chances of survival</i>.”",
            "“But anyways, while we deal with these meanieheads. It's high time we return to our main topic: the silly ol' water crisis.”",
            "“Water prices are skyrocketing and the lower classes are failing to pay for clean drinking water!”",
            "“The regions hit hardest by this tomfoolery are Tumbletree, Dusty and a large orphanage near the recently uhh c-consumed Yonder with a lack of better words.”",
            "“Luckily, these regions are near Midburgh, unluckily, Midburgh is also struggling. If we want to act, we need to act <i>fast</i>!”",
            "“But do not worry friends! I've already devised a quick plan that will solve this petty crisis in a blink of only a single eye!”",
            "“So here's the plan, we collect all the rainwater we can find and allocate it to the poor!”",
            "“But you might be asking: 'How do we do that Mr. Handsome?' Well contain your excitement peeps;”",
            "“<b>WE RAISE TAXES</b>”",
            "<i>Murmurs run through the crowd</i>"
        };

    public int MinSpeechIndex = 0;

    void Update()
    {
        MinSpeechTextBox.text = MinSpeech[MinSpeechIndex];

        if(MinSpeechIndex == 9)
        {
            MinNameTag.SetActive(false);
        }
    }

    public void NextScentence()
    {
        if(MinSpeechIndex < 9)
        {
            MinSpeechIndex++;
        }
    }
}
