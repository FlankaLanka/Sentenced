using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnlargeOnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 originalSize;
    public Sprite whiteDialogue;
    public Sprite blackDialogue;
    //public int originalFontSize;

    private AudioSource a;

    private void Start()
    {
        a = GetComponent<AudioSource>();

        originalSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        whiteDialogue = gameObject.GetComponent<Image>().sprite;
        if(transform.parent.name == "Content1")
        {
            blackDialogue = Resources.Load<Sprite>("dialogue_rev_3x_black");
        }
        else
        {
            blackDialogue = Resources.Load<Sprite>("dialogue_quote_black");
        }
        //originalFontSize = gameObject.GetComponentInChildren<Text>().fontSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.parent.name == "Content1" || transform.parent.name == "Content2")
        {
            if(!GameObject.Find("MatchCanvas").GetComponent<CardPositions>().curDragging)
            {
                gameObject.GetComponent<RectTransform>().sizeDelta = originalSize * 1.5f;
                a.Play();
                
                //this part should only work if draggable
                if(gameObject.GetComponent<Image>().color != Color.gray)
                {
                    gameObject.GetComponent<Image>().sprite = blackDialogue;
                    gameObject.GetComponentInChildren<Text>().color = Color.white;
                }
            }
        }
        }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.parent.name == "Content1" || transform.parent.name == "Content2")
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = originalSize;
            gameObject.GetComponent<Image>().sprite = whiteDialogue;
            gameObject.GetComponentInChildren<Text>().color = Color.black;
        }
    }
}
