using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;

public class CardClickFunctions : MonoBehaviour
{
    //currently only handles all buttons' color change and contains cardAttributes value
    //for everything else check MatchMechanic

    [Serializable]
    public class cardAttributes
    {

        public bool selected;
        //private int category; //1 or 2, 0 is default and needs to be assigned
        [SerializeField]
        private string words;

        public cardAttributes(string w)
        {
            selected = false;
            words = w;
        }

        public void setWord(string w)
        {
            words = w;
        }

        public string getWord()
        {
            return words;
        }
    }


    public string defaultWord = "DEFAULT_TEXT"; //use this variable in inspector to set the words
    public cardAttributes ca;

    private void Start()
    {
        ca = new cardAttributes(defaultWord);
        SetTexttoWords(ca);
        //StartCoroutine(LateStart());
    }

    private void Update()
    {
        //SetTexttoWords(ca);
    }
    private void SetTexttoWords(cardAttributes a)
    {
        gameObject.GetComponentInChildren<Text>().text = a.getWord();
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(3f);
        SetTexttoWords(ca);
    }

    /*
    public void SelectCard()
    {
        if (transform.parent.name == "Content1")
        {
            DecolorAllCards(content1);
            displayCard1.GetComponent<Text>().text = ca.getWord();
        }
        else if (transform.parent.name == "Content2")
        {
            DecolorAllCards(content2);
            displayCard2.GetComponent<Text>().text = ca.getWord();
        }

        //displayCard1.GetComponent<Text>().text = output1 + output2;
        ca.selected = true;
        //color the selected card
        curButton.colors = newColors;
    }

    private void DecolorAllCards(GameObject content)
    {
        
        foreach (Transform child in content.transform)
        {
            Assert.IsNotNull(child.GetComponent<CardClickFunctions>());
            child.GetComponent<CardClickFunctions>().ca.selected = false;
            child.GetComponent<Button>().colors = oldColors;
        }
    }


    */
}
