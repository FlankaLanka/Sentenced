using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ConvoChangeOnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite blackButton;
    private Sprite whiteButton;
    private Vector2 regSize;

    // Start is called before the first frame update
    void Awake()
    {
        whiteButton = GetComponent<Image>().sprite;
        regSize = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        changeToBlack();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        changeToOriginal();
    }

    public void changeToBlack()
    {
        gameObject.GetComponent<Image>().sprite = blackButton;
        transform.localScale = regSize * 1.5f;
    }

    public void changeToOriginal()
    {
        gameObject.GetComponent<Image>().sprite = whiteButton;
        transform.localScale = regSize;
    }
}
