using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using UnityEngine.Assertions;

[CommandInfo("ConversationMenuz", "Enable Conversation Panel", "Enable Conversation Panel. Please fill in all areas or the" +
    " old assets from a previous conversation panel will carry over.")]

public class ConversationOpen : Command
{
    [Header("Background")]
    public Sprite LeftImage;
    public Sprite RightImage;

    [Header("People")]
    public string LeftPersonName;
    public Sprite LeftPersonImage;
    public string RightPersonName;
    public Sprite RightPersonImage;

    [Header("Dialogue")]
    public DialogueClass sentences;
    
    private Transform convoPanel;

    public override void OnEnter()
    {
        Assert.IsTrue(sentences.sentence.Count == sentences.LeftCharSpeaking.Count);

        GameObject.Find("ConversationCanvas").transform.Find("ConversationPanel").gameObject.SetActive(true);

        convoPanel = GameObject.Find("ConversationPanel").transform;
        convoPanel.Find("RightBG").GetComponent<Image>().sprite = RightImage;
        convoPanel.Find("LeftBG").GetComponent<Image>().sprite = LeftImage;
        convoPanel.Find("RightPersonImage").GetComponent<Image>().sprite = RightPersonImage;
        convoPanel.Find("LeftPersonImage").GetComponent<Image>().sprite = LeftPersonImage;
        convoPanel.Find("RightPersonName").GetComponent<Text>().text = RightPersonName;
        convoPanel.Find("LeftPersonName").GetComponent<Text>().text = LeftPersonName;

        //convoPanel.GetComponent<DialogueSystem>().l = sentences.LeftCharSpeaking;
        //convoPanel.GetComponent<DialogueSystem>().s = sentences.sentence;
        convoPanel.GetComponent<DialogueSystem>().currentSentences.CopyDialogue(sentences);
        convoPanel.GetComponent<DialogueSystem>().i = 0;
        //Transform dialogueContainer = convoPanel.Find("Dialogue").GetComponent<DialogueSystem>().currentSentences;

        //Transform[] dialogues = new Transform[dialogueContainer.childCount]; //should be 6
        /*
        foreach(string a in convoPanel.GetComponent<DialogueSystem>().s)
        {
            Debug.Log(a);
        }
        */

        /*
        int i = 0;
        foreach(Transform child in dialogueContainer)
        {
            if(sentences.Length > i)
            {
                child.gameObject.GetComponentInChildren<Text>().text = sentences[i];
                i++;
            }
        }
        */

        Continue();
    }
}
