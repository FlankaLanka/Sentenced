using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoPanelTransitionIn : MonoBehaviour
{
    private Transform leftOut;
    private Transform rightOut;
    private Transform leftImage;
    private Transform rightImage;
    private Vector2 finalLeftPos;
    private Vector2 finalRightPos;

    public bool chooseToFade;

    private void Awake()
    {
        leftOut = transform.Find("LeftImageOutPosition");
        rightOut = transform.Find("RightImageOutPosition");
        leftImage = transform.Find("LeftPersonImage");
        rightImage = transform.Find("RightPersonImage");

        finalRightPos = rightImage.position;
        finalLeftPos = leftImage.position;
        chooseToFade = true;
    }

    private void OnEnable()
    {
        if(chooseToFade)
        {
            leftImage.position = leftOut.position;
            rightImage.position = rightOut.position;
            StartCoroutine(ConvoCharsFadeIn());
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
}
