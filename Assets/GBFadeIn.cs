using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GBFadeIn : MonoBehaviour
{
    public Vector2 originalImagePosition;
    public Vector2 originalScale;

    private GameObject PersonImage;
    private GameObject PersonImageEnd;

    private void Awake()
    {
        PersonImage = transform.Find("PersonImage").gameObject;
        PersonImageEnd = transform.Find("PersonImageEnd").gameObject;
        originalImagePosition = PersonImage.transform.position;
        originalScale = PersonImage.transform.localScale;
    }

    private void OnEnable()
    {
        StartCoroutine(GBFade());
    }

    private IEnumerator GBFade()
    {
        Vector2 FinalScale = new Vector2(0.9f, 0.9f);
        Vector2 FinalPosition = PersonImageEnd.transform.position;

        float timer = 0f;
        float fadeTime = 1f;
        //scale starts at 0.4 and ends at 0.9
        while(timer < fadeTime)
        {
            //change person image transition
            PersonImage.transform.position = Vector2.Lerp(originalImagePosition, FinalPosition, timer/fadeTime);
            PersonImage.transform.localScale = new Vector2(originalScale.x + 0.5f * timer / fadeTime,
                originalScale.y + 0.5f * timer / fadeTime);

            //change panel opacity transition
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, (timer / fadeTime) * (100f / 255f));

            timer += 0.025f;
            yield return new WaitForSeconds(0.01f);
            
        }

        
    }

}
