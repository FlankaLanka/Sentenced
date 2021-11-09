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
    public class cardAttributes
    {
        public int index; //1-8, 1-4 for content1 and 5-8 for content2
        public int content; //1 or 2, 0 is default and needs to be assigned
        private string words;

        public cardAttributes(string w, int c)
        {
            words = w;
            content = c;
            index = 0;
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
    public int content = 0;
    public cardAttributes ca;

    public bool jumpingBack = false;


    private void Start()
    {
        ca = new cardAttributes(defaultWord, content);
        
        Transform contentTransform;
        if (ca.content == 1)
        {
            contentTransform = GameObject.Find("Content1").transform;
            //do adjustments to get text and sprite to look good
            Sprite revDialogue = Resources.Load<Sprite>("dialogue_rev_3x");
            gameObject.GetComponent<Image>().sprite = revDialogue;
            gameObject.GetComponent<VerticalLayoutGroup>().padding.left = -70;
        }
        else
        {
            contentTransform = GameObject.Find("Content2").transform;
            ca.index = 4;
        }

        //sets the index to the correct index to when card added
        int temp = 0;
        foreach (Transform child in contentTransform)
        {
            if (gameObject.transform == child)
            {
                ca.index += temp;
                break;
            }
            else
            {
                temp++;
            }
        }

        SetTexttoWords(ca);
    }

    private void SetTexttoWords(cardAttributes a)
    {
        gameObject.GetComponentInChildren<Text>().text = a.getWord();
    }

    private void Update()
    {
        if (transform.parent != null && !jumpingBack)
        {
            if(transform.parent.name == "Content1" || transform.parent.name == "Content2")
            {
                transform.position = GameObject.Find("MatchPanel").GetComponent<CardPositions>().cardPos[ca.index];

                //gameObject.GetComponent<RectTransform>().anchoredPosition = 
                //    GameObject.Find("MatchPanel").GetComponent<CardPositions>().cardPos[ca.index];
            }
        }
    }


    public IEnumerator DialogueLerp()
    {   
        float timeStep = 0.05f;
        Vector3 start = transform.position;
        Vector3 end = GameObject.Find("MatchPanel").GetComponent<CardPositions>().cardPos[ca.index];
        Vector3 distance = end - start;
        Vector3 step = distance * timeStep;
        Vector3 traveled = new Vector3(0, 0, 0);

        while(traveled.x < Mathf.Abs(distance.x) && traveled.y < Mathf.Abs(distance.y))
        {
            transform.position += step;
            traveled.x += Mathf.Abs(step.x);
            traveled.y += Mathf.Abs(step.y);
            yield return new WaitForSeconds(0.01f);
        }
        jumpingBack = false;
    }



    /*
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(3f);
        SetTexttoWords(ca);
    }
    */

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
