using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class DialogueSystem : MonoBehaviour
{
    public Flowchart mainFlow;

    //private int totalLines;
    private int displayLine;
    private bool allSixDisplayed;
    private Transform dialogueContainer;

    void OnEnable()
    {
        displayLine = 1;
        allSixDisplayed = false;

        dialogueContainer = transform.Find("Dialogue");
        foreach (Transform child in dialogueContainer)
        {
            child.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            child.gameObject.GetComponentInChildren<Text>().color = new Color(0.2f, 0.2f, 0.2f, 0f);
        }
    }

    public void DisplayNextLine()
    {
        GameObject currentDialogue = dialogueContainer.Find("Dialogue" + (displayLine).ToString()).gameObject;
        if(currentDialogue.GetComponentInChildren<Text>().text != "temp_text" && !allSixDisplayed)
        {
            currentDialogue.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            currentDialogue.GetComponentInChildren<Text>().color = new Color(0.2f, 0.2f, 0.2f, 1f);
        }
        else
        {
            mainFlow.SetBooleanVariable("NextStep", true);
        }

        if(displayLine < 6)
        {
            displayLine++;
        }
        else
        {
            allSixDisplayed = true;
        }
    }

}
