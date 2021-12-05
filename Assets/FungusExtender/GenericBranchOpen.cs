using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

[CommandInfo("GenericBranch", "Enable GenericBranch Panel", "Enable GenericBranch Panel. Fill up to 3 options and" +
    "fill in the previous text to what the npc said before.")]

public class GenericBranchOpen : Command
{
    public string option1;
    public string option2;
    public string option3;
    [Space]
    [TextArea(3,10)]
    public string previousText;

    public override void OnEnter()
    {
        Transform branchingCanvas = GameObject.Find("GenericBranchingCanvas").transform;
        Transform branchingPanel = branchingCanvas.Find("GenericBranchingPanel");
        branchingPanel.gameObject.SetActive(true);

        branchingPanel.Find("PreviousText").GetComponentInChildren<Text>().text = previousText;

        GameObject optionsButton;

        //set buttons to appropriate state
        optionsButton = branchingPanel.Find("GenericBranchingCard1").gameObject;
        if(option1 == "")
        {
            optionsButton.SetActive(false);
        }
        else
        {
            optionsButton.SetActive(true);
            optionsButton.GetComponentInChildren<Text>().text = option1;
        }

        optionsButton = branchingPanel.Find("GenericBranchingCard2").gameObject;
        if (option2 == "")
        {
            optionsButton.SetActive(false);
        }
        else
        {
            optionsButton.SetActive(true);
            optionsButton.GetComponentInChildren<Text>().text = option2;
        }

        optionsButton = branchingPanel.Find("GenericBranchingCard3").gameObject;
        if (option3 == "")
        {
            optionsButton.SetActive(false);
        }
        else
        {
            optionsButton.SetActive(true);
            optionsButton.GetComponentInChildren<Text>().text = option3;
        }
        Continue();
    }
    

}
