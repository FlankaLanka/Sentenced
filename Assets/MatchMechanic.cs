using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class MatchMechanic : MonoBehaviour
{
    public Flowchart mainFlow;

    public GameObject cardPrefab;
    private GameObject newCard;
    private GameObject content1;
    private GameObject content2;
    private GameObject matchPanel;

    private void Update()//"Update" for now, prob need to find a way to make it "Start"
    {
        content1 = GameObject.Find("Content1");
        content2 = GameObject.Find("Content2");
        matchPanel = GameObject.Find("MatchPanel");
    }

    public void addCardScroll1()
    {
        newCard = Instantiate(cardPrefab) as GameObject;
        if (content1 != null)
        {
            newCard.transform.SetParent(content1.transform, false);
        }
    }

    public void addCardScroll2()
    {
        newCard = Instantiate(cardPrefab) as GameObject;
        if (content2 != null)
        {
            newCard.transform.SetParent(content2.transform, false);
        }
    }

    public void SelectChoice() //also means confirm choice and move to different scene based on what is picked
    {
        GameObject card1 = GameObject.Find("CardSlot1").transform.GetChild(0).gameObject;
        GameObject card2 = GameObject.Find("CardSlot2").transform.GetChild(0).gameObject;
        string PlayerTextChoice = card1.GetComponent<CardClickFunctions>().ca.getWord() +
                                  card2.GetComponent<CardClickFunctions>().ca.getWord();

        mainFlow.SetStringVariable("MatchChoice", PlayerTextChoice);
        mainFlow.SetBooleanVariable("NextStep", true);

        //StartCoroutine(WaitCommand(card1, card2));

        /*
        //remove from list the selected cards
        foreach (Transform child in content1.transform)
        {
            Assert.IsNotNull(child.GetComponent<CardClickFunctions>());
            if(child.GetComponent<CardClickFunctions>().ca.selected)
            {
                PlayerTextChoice += child.GetComponent<CardClickFunctions>().ca.getWord();
                card1 = child.gameObject;
                //Destroy(child.gameObject);
            }
            

        }
        foreach (Transform child in content2.transform)
        {
            Assert.IsNotNull(child.GetComponent<CardClickFunctions>());
            if (child.GetComponent<CardClickFunctions>().ca.selected)
            {
                PlayerTextChoice += child.GetComponent<CardClickFunctions>().ca.getWord();
                card2 = child.gameObject;
                //Destroy(child.gameObject);
            }
        }*/

    }
    /*
    public void RemoveCards()
    {
        GameObject card1 = GameObject.Find("CardSlot1").transform.GetChild(0).gameObject;
        GameObject card2 = GameObject.Find("CardSlot2").transform.GetChild(0).gameObject;
        GameObject display1 = GameObject.Find("DisplayCard1");
        GameObject display2 = GameObject.Find("DisplayCard2");

        display1.GetComponent<Text>().text = "";
        display2.GetComponent<Text>().text = "";
        Destroy(card1);
        Destroy(card2);
    }

    IEnumerator WaitCommand(GameObject c1, GameObject c2) //need some time for tryagainblock. Check out MatcherClose.cs
    {
        yield return new WaitForSeconds(0.05f);
        if(mainFlow.GetBooleanVariable("TryAgainBlock") == false)
        {
            GameObject display1 = GameObject.Find("DisplayCard1");
            GameObject display2 = GameObject.Find("DisplayCard2");
            display1.GetComponent<Text>().text = "";
            display2.GetComponent<Text>().text = "";
            Destroy(c1);
            Destroy(c2);
        }
    }
    */

}
