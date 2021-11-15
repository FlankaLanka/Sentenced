using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvoFadeInOnEnable : MonoBehaviour
{
    private Image background;
    void Awake()
    {
        background = GetComponent<Image>();
    }

    private void OnEnable()
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float albedo = 0f;
        while(background.color.a < 1f)
        {
            yield return new WaitForSeconds(0.01f);
            background.color = new Color(background.color.r, background.color.g, background.color.b, albedo += 0.025f);

        }
    }
}
