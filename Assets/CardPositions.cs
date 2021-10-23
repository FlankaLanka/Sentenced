using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPositions : MonoBehaviour
{
    public Dictionary<int, Vector2> cardPos = new Dictionary<int, Vector2>();

    private void Start()
    {
        cardPos.Add(0, new Vector2(219, 700));
        cardPos.Add(1, new Vector2(530, 547));
        cardPos.Add(2, new Vector2(289, 324));
        cardPos.Add(3, new Vector2(610, 149));
        cardPos.Add(4, new Vector2(1578, 733));
        cardPos.Add(5, new Vector2(1337, 540));
        cardPos.Add(6, new Vector2(1600, 372));
        cardPos.Add(7, new Vector2(1337, 138));
    }
}
