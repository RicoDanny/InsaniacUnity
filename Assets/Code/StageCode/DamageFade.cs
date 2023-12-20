using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFade : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textMeshPro;
    private float fadeSpeed = 4.0f;

    private void Start()
    {
        textMeshPro = this.GetComponent<TMPro.TextMeshProUGUI>();
        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        Color startColor = textMeshPro.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        while (true)
        {
            textMeshPro.color = Color.Lerp(textMeshPro.color, targetColor, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
