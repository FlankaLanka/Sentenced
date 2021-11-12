using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPositions : MonoBehaviour
{
    public Dictionary<int, Vector2> cardPos = new Dictionary<int, Vector2>(); //used in CardClickFunctions script
    //public GameObject enlargedObject; //used in EnlargeOnPointer script
    public bool curDragging = false; //used in EnlargeOnPointer and IsDraggable scripts

    private Transform matchCanvasTempCards;
    private Vector2 resolution;
    private void Awake()
    {
        resolution = new Vector2(Screen.width, Screen.height);
    }


    private void Start()
    {
        /*
        cardPos.Add(0, new Vector2(219, 700));
        cardPos.Add(1, new Vector2(530, 547));
        cardPos.Add(2, new Vector2(289, 324));
        cardPos.Add(3, new Vector2(610, 149));
        cardPos.Add(4, new Vector2(1578, 696));
        cardPos.Add(5, new Vector2(1337, 507));
        cardPos.Add(6, new Vector2(1600, 321));
        cardPos.Add(7, new Vector2(1337, 138));
        */

        matchCanvasTempCards = transform.Find("CardPositionHandler").transform;

        //make active first to get positions
        matchCanvasTempCards.gameObject.SetActive(true);

        int i = 0;
        foreach(Transform child in matchCanvasTempCards)
        {
            cardPos.Add(i, new Vector2(child.position.x, child.position.y));
            i++;
        }
    }


    private void Update()
    {
        if(resolution.x != Screen.width || resolution.y != Screen.height)
        {
            resolution = new Vector2(Screen.width, Screen.height);

            int i = 0;
            foreach (Transform child in matchCanvasTempCards)
            {
                cardPos[i] = new Vector2(child.position.x, child.position.y);
                i++;
            }
        }
    }

}
