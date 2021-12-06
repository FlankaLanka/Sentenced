using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BranchingCardSoundOnHover : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource a;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<AudioSource>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        a.Play();
    }

}
