using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IsDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject parentObj;
    private GameObject canvas;
    private GameObject cardSlot1;
    private GameObject cardSlot2;
    //private GameObject content1;
    //private GameObject content2;
    private CanvasGroup group;

    private void Start()
    {
        canvas = GameObject.Find("MatchCanvas");
        cardSlot1 = GameObject.Find("CardSlot1");
        cardSlot2 = GameObject.Find("CardSlot2");
        //content1 = GameObject.Find("Content1");
        //content2 = GameObject.Find("Content2");
        group = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentObj = transform.parent.gameObject;
        transform.SetParent(canvas.transform);
        group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cardSlot1.gameObject.GetComponent<IsCardSlot>().canAdd && parentObj.name == "Content1")
        {
            if(cardSlot1.transform.childCount > 0)
            {
                GameObject childCard = cardSlot1.transform.GetChild(0).gameObject;
                childCard.GetComponent<IsDraggable>().enabled = true;
                childCard.GetComponent<CardClickFunctions>().jumpingBack = true;
                StartCoroutine(childCard.GetComponent<CardClickFunctions>().DialogueLerp());
                childCard.transform.SetParent(parentObj.transform);
                
            }
            transform.SetParent(cardSlot1.transform);
            gameObject.gameObject.GetComponent<IsDraggable>().enabled = false;
        }
        else if (cardSlot2.gameObject.GetComponent<IsCardSlot>().canAdd && parentObj.name == "Content2")
        {
            if (cardSlot2.transform.childCount > 0)
            {
                GameObject childCard = cardSlot2.transform.GetChild(0).gameObject;
                childCard.GetComponent<IsDraggable>().enabled = true;
                childCard.GetComponent<CardClickFunctions>().jumpingBack = true;
                StartCoroutine(childCard.GetComponent<CardClickFunctions>().DialogueLerp());
                childCard.transform.SetParent(parentObj.transform);
            }
            transform.SetParent(cardSlot2.transform);
            gameObject.gameObject.GetComponent<IsDraggable>().enabled = false;
        }
        else
        {
            transform.SetParent(parentObj.transform);
        }


        group.blocksRaycasts = true;

    }

}

