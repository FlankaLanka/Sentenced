using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnlargeOnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 originalSize;

    private void Start()
    {
        originalSize = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.parent.name == "Content1" || transform.parent.name == "Content2")
        {
            if(!GameObject.Find("MatchPanel").GetComponent<CardPositions>().curDragging)
            {
                gameObject.GetComponent<RectTransform>().sizeDelta = originalSize * 1.5f;
            }
        }
            /*
            Debug.Log("poiter enter");
            if (transform.parent.name == "Content1" || transform.parent.name == "Content2")
            {
                if(GameObject.Find("MatchPanel").GetComponent<CardPositions>().enlargedObject == null)
                {
                    gameObject.GetComponent<RectTransform>().sizeDelta = originalSize * 1.5f;
                    GameObject.Find("MatchPanel").GetComponent<CardPositions>().enlargedObject = gameObject;
                }
            }
            */
        }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.parent.name == "Content1" || transform.parent.name == "Content2")
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = originalSize;
        }
        /*
        Debug.Log("poiter exit");
        if (transform.parent.name == "Content1" || transform.parent.name == "Content2")
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = originalSize;
        }
        if(GameObject.Find("MatchPanel").GetComponent<CardPositions>().enlargedObject == gameObject
            && !gameObject.GetComponent<IsDraggable>().currentlyDragging)
        {
            GameObject.Find("MatchPanel").GetComponent<CardPositions>().enlargedObject = null;
        }*/
    }
}
