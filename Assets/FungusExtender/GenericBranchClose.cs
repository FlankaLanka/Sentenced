using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

[CommandInfo("GenericBranch", "Disable GenericBranch Panel", "Disable GenericBranch Panel.")]

public class GenericBranchClose : Command
{
    public override void OnEnter()
    {
        GameObject branchingPanel = GameObject.Find("GenericBranchingCanvas").transform.Find("GenericBranchingPanel").gameObject;

        branchingPanel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        branchingPanel.transform.Find("PersonImage").position = branchingPanel.GetComponent<GBFadeIn>().originalImagePosition;
        branchingPanel.transform.Find("PersonImage").localScale = branchingPanel.GetComponent<GBFadeIn>().originalScale;


        GameObject choiceButton = branchingPanel.transform.Find("GenericBranchingCard1").gameObject;
        choiceButton.GetComponent<ConvoChangeOnPointer>().changeToOriginal();
        choiceButton.GetComponentInChildren<Text>().color = Color.black;

        choiceButton = branchingPanel.transform.Find("GenericBranchingCard2").gameObject;
        choiceButton.GetComponent<ConvoChangeOnPointer>().changeToOriginal();
        choiceButton.GetComponentInChildren<Text>().color = Color.black;

        choiceButton = branchingPanel.transform.Find("GenericBranchingCard3").gameObject;
        choiceButton.GetComponent<ConvoChangeOnPointer>().changeToOriginal();
        choiceButton.GetComponentInChildren<Text>().color = Color.black;


        branchingPanel.SetActive(false);
        Continue();
    }
}
