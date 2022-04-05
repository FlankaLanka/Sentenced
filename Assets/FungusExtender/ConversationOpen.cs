using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using UnityEngine.Assertions;

[CommandInfo("ConversationMenuz", "Enable Conversation Panel", "Enable Conversation Panel. Please fill in all areas or the" +
    " old assets from a previous conversation panel will carry over. Make sure list of sentences and list of who is" +
    " speaking are equal.")]

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
    public bool LeadsToMatchOrBranch;
    public DialogueClass sentences;

    [Header("Transition")]
    public bool chooseToFade;


    private Transform convoPanel;

    public override void OnEnter()
    {
        Assert.IsTrue(sentences.sentence.Count == sentences.LeftCharSpeaking.Count);
        Assert.IsTrue(sentences.sentence.Count > 0 && sentences.LeftCharSpeaking.Count > 0);

        GameObject.Find("ConversationCanvas").transform.Find("ConversationPanel").gameObject.SetActive(true);

        convoPanel = GameObject.Find("ConversationPanel").transform;
        convoPanel.Find("RightBG").GetComponent<Image>().sprite = RightImage;
        convoPanel.Find("LeftBG").GetComponent<Image>().sprite = LeftImage;
        convoPanel.Find("RightPersonImage").GetComponent<Image>().sprite = RightPersonImage;
        convoPanel.Find("RightPersonImage").GetComponent<Image>().material = null;
        convoPanel.Find("LeftPersonImage").GetComponent<Image>().sprite = LeftPersonImage;
        convoPanel.Find("RightPersonName").GetComponent<Text>().text = RightPersonName;
        convoPanel.Find("LeftPersonName").GetComponent<Text>().text = LeftPersonName;

        convoPanel.GetComponent<ConvoPanelTransitionIn>().chooseToFade = chooseToFade;


        DialogueSystem d = convoPanel.GetComponent<DialogueSystem>();

        d.currentSentences.CopyDialogue(sentences);
        d.i = 0;
        d.LeadsToMatchPanel = LeadsToMatchOrBranch;

        d.MatchQMark.SetActive(false);
        d.NextIsFinish.SetActive(false);
        d.NextIsLeft.SetActive(false);
        d.NextIsRight.SetActive(false);

        if(sentences.LeftCharSpeaking[0])
        {
            d.NextIsLeft.SetActive(true);
        }
        else
        {
            d.NextIsRight.SetActive(true);
        }

        Continue();
    }
}
