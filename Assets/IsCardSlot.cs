using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IsCardSlot : MonoBehaviour, IDropHandler, IPointerExitHandler
{
    public GameObject display; //make sure item registered in inspector
    public bool canAdd = false;

    public void OnDrop(PointerEventData eventData)
    {
        canAdd = true;//this part helps to set child and parent in IsDraggable.cs
        StartCoroutine(DelayDisplay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canAdd = false;
    }

    IEnumerator DelayDisplay()
    {
        yield return new WaitForEndOfFrame();
        if(transform.childCount != 0)
        {
            display.GetComponent<Text>().text = transform.GetComponentInChildren<Text>().text;
        }
    }
}
