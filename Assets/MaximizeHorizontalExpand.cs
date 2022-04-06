using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaximizeHorizontalExpand : MonoBehaviour
{
    [SerializeField]
    private float horizontalMax;
    private RectTransform rectText;
    private Text curText;
    private Transform emotionIcon;
    private ContentSizeFitter fit;
    private bool alreadyUpdatedSize;
    private bool alreadySetActive;

    [SerializeField]
    private Sprite big;

    // Start is called before the first frame update
    void Start()
    {
        GameObject textTransform = transform.Find("Text").gameObject;
        rectText = textTransform.GetComponent<RectTransform>();
        curText = textTransform.GetComponent<Text>();
        

        //first set them disabled and only enable when ready (size is fixed) to prevent a flicker bug
        curText.enabled = false;
        GetComponent<Image>().enabled = false;

        
        emotionIcon = transform.Find("EmotionImageInText");
        if (emotionIcon != null)
            emotionIcon.gameObject.SetActive(false);

        fit = GetComponent<ContentSizeFitter>();
        alreadyUpdatedSize = false;
        alreadySetActive = false;

        StartCoroutine(EoFrameEnable());
    }

    // Update is called once per frame
    void Update()
    {
        if(rectText.sizeDelta.x > horizontalMax && !alreadyUpdatedSize)
        {
            string sentence = curText.text;
            curText.text = "";
            GetComponent<Image>().sprite = big;
            fit.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            StartCoroutine(EoFrame(sentence));
            alreadyUpdatedSize = true;
        }
    }


    private IEnumerator EoFrame(string s)
    {
        yield return new WaitForEndOfFrame();
        fit.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        curText.text = s;
    }

    private IEnumerator EoFrameEnable()
    {
        yield return new WaitForSeconds(0.2f);
        curText.enabled = true;
        GetComponent<Image>().enabled = true;
        if (emotionIcon != null)
            emotionIcon.gameObject.SetActive(true);
    }
}
