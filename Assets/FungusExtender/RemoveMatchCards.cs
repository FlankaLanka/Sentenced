using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

[CommandInfo("MatchMenu", "Remove Cards", "Removes the cards that are used to chat (in the 2 boxes)")]

public class RemoveMatchCards : Command
{
    public override void OnEnter()
    {
        GameObject card1 = GameObject.Find("CardSlot1").transform.GetChild(0).gameObject;
        GameObject card2 = GameObject.Find("CardSlot2").transform.GetChild(0).gameObject;
        GameObject display1 = GameObject.Find("DisplayCard1");
        GameObject display2 = GameObject.Find("DisplayCard2");

        display1.GetComponent<Text>().text = "";
        display2.GetComponent<Text>().text = "";
        Destroy(card1);
        Destroy(card2);

        Continue();
    }
}
