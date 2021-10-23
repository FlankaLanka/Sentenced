using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{

    private bool alreadyFaded = false;

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount != 0 && !alreadyFaded)
        {
            StartCoroutine(Fadeout());
            alreadyFaded = true;
        }
    }

    private IEnumerator Fadeout()
    {
        float fader = 1f;
        Color SlotColor = gameObject.GetComponent<Image>().color;
        while (SlotColor.a > 0)
        {
            yield return new WaitForSeconds(0.002f);
            gameObject.GetComponent<Image>().color = new Color(SlotColor.r, SlotColor.g, SlotColor.b, fader);
            fader -= 0.01f;
        }
        
    }

}
