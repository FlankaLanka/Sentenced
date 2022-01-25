using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

[CommandInfo("ConversationMenu", "Disable Conversation Panel", "Disable Conversation Panel. Make sure to re-add all assets" +
    " when you open conversation again.")]

public class ConversationClose : Command
{
    public override void OnEnter()
    {

        Transform convoPanel = GameObject.Find("ConversationCanvas").transform.Find("ConversationPanel");
        Transform tempDialogue = convoPanel.Find("Dialogue").Find("TempDialogue");

        /*
        foreach(Transform child in tempDialogue)
        {
            Destroy(gameObject);
        }*/
        for (int I = 0; I < tempDialogue.childCount; ++I)
        {
            Destroy(tempDialogue.GetChild(I).gameObject);
        }


        convoPanel.gameObject.SetActive(false);
        Continue();
    }
}
