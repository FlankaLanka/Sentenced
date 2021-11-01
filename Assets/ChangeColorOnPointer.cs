using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColorOnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Sprite blackButton;
    private Sprite whiteButton;

    private void Start()
    {
        whiteButton = gameObject.GetComponent<Image>().sprite;
        blackButton = Resources.Load<Sprite>("confirm_button_3x_black");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //gameObject.GetComponent<Image>().color = Color.magenta;
        gameObject.GetComponent<Image>().sprite = blackButton;
        gameObject.GetComponentInChildren<Text>().color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //gameObject.GetComponent<Image>().color = Color.white;
        gameObject.GetComponent<Image>().sprite = whiteButton;
        gameObject.GetComponentInChildren<Text>().color = Color.black;
    }
}
