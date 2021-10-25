using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class MatchMechanic : MonoBehaviour
{
    public Flowchart mainFlow;

    public GameObject cardPrefab;
    
    /*
    private GameObject content1;
    private GameObject content2;
    private GameObject matchPanel;

    private void Update()//"Update" for now, prob need to find a way to make it "Start"
    {
        content1 = GameObject.Find("Content1");
        content2 = GameObject.Find("Content2");
        matchPanel = GameObject.Find("MatchPanel");
    }
    */

    public void SelectChoice() //also means confirm choice and move to different scene based on what is picked
    {
        GameObject card1;
        GameObject card2;
        if(GameObject.Find("CardSlot1").transform.childCount != 0 && GameObject.Find("CardSlot2").transform.childCount != 0)
        {
            card1 = GameObject.Find("CardSlot1").transform.GetChild(0).gameObject;
            card2 = GameObject.Find("CardSlot2").transform.GetChild(0).gameObject;

            string PlayerTextChoice = card1.GetComponent<CardClickFunctions>().ca.getWord() + card2.GetComponent<CardClickFunctions>().ca.getWord();

            mainFlow.SetStringVariable("MatchChoice", PlayerTextChoice);
            mainFlow.SetBooleanVariable("NextStep", true);
        }


    }

}
