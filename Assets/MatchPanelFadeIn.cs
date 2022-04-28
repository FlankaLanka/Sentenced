using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanelFadeIn : MonoBehaviour
{
    private Image mainPanelBorder;
    private Transform matchImageStarting;
    private Transform matchImage;
    private Vector2 endingPos;
    private Vector2 originalScale;
    private Color originalColor;

    // Start is called before the first frame update
    private void Awake()
    {
        mainPanelBorder = transform.Find("MainPanelBorder").GetComponent<Image>();
        matchImage = transform.Find("JustinImage");
        matchImageStarting = transform.Find("ImageStartingPosition");
        endingPos = matchImage.position;
        originalScale = matchImageStarting.localScale;
        originalColor = mainPanelBorder.color;
    }

    private void OnEnable()
    {
        matchImage.position = matchImageStarting.position;
        matchImage.localScale = matchImageStarting.localScale;
        mainPanelBorder.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        StartCoroutine(MatchPanelTranslateCharacter());
    }

    private IEnumerator MatchPanelTranslateCharacter()
    {
        //scale goes from 0.75 to 1

        float timer = 0f;
        float fadeTime = 1f;
        while(timer < fadeTime)
        {
            mainPanelBorder.color = new Color(originalColor.r, originalColor.g, originalColor.b, timer / fadeTime);
            matchImage.position = Vector2.Lerp(matchImageStarting.position, endingPos, timer / fadeTime);
            matchImage.localScale = new Vector2(originalScale.x + 0.25f * timer / fadeTime, originalScale.y + 0.25f * timer / fadeTime);
            timer += 0.025f;

            yield return new WaitForSeconds(0.01f);
        }


    }
}
