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
        GameObject convoPanel = GameObject.Find("ConversationPanel");
        Transform dialogueContainer = convoPanel.transform.Find("Dialogue").transform;

        foreach(Transform child in dialogueContainer)
        {
            child.GetComponentInChildren<Text>().text = "temp_text";
        }

        convoPanel.SetActive(false);
        Continue();
    }
}
