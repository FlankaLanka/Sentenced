using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoPanelTransitionIn : MonoBehaviour
{
    private Transform leftCenter;
    private Transform leftOut;
    private Transform rightOut;
    private Transform leftImage;
    private Transform rightImage;
    private Vector2 finalLeftPos;
    private Vector2 finalRightPos;

    public bool chooseToFade;
    public bool comingInFromMatchPanel;

    private void Awake()
    {
        leftCenter = transform.Find("LeftImageCenterPosition");
        leftOut = transform.Find("LeftImageOutPosition");
        rightOut = transform.Find("RightImageOutPosition");
        leftImage = transform.Find("LeftPersonImage");
        rightImage = transform.Find("RightPersonImage");

        finalRightPos = rightImage.position;
        finalLeftPos = leftImage.position;
    }

    private void OnEnable()
    {
        if(chooseToFade)
        {
            leftImage.position = leftOut.position;
            rightImage.position = rightOut.position;
            // if (comingInFromMatchPanel)
                // leftImage.position = leftCenter.position;
            StartCoroutine(ConvoCharsFadeIn());
        }
        else if(comingInFromMatchPanel)
        {
            rightImage.position = finalRightPos;
            leftImage.position = leftCenter.position;
            StartCoroutine(LeftFadeInOnly());
        }
    }

    private IEnumerator ConvoCharsFadeIn()
    {
        float fadeTime = 1f;
        float timer = 0f;

        while (timer < fadeTime)
        {
            leftImage.position = Vector2.Lerp(leftOut.position, finalLeftPos, timer / fadeTime);
            rightImage.position = Vector2.Lerp(rightOut.position, finalRightPos, timer / fadeTime);
            timer += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }

    }

    private IEnumerator LeftFadeInOnly()
    {
        float fadeTime = 1f;
        float timer = 0f;

        while (timer < fadeTime)
        {
            leftImage.position = Vector2.Lerp(leftCenter.position, finalLeftPos, timer / fadeTime);
            timer += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
