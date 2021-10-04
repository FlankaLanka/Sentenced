using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

[CommandInfo("Card", "Create Card", "Create Card, must fill in which content to put and conditions to be grey")]

public class CardCreate : Command
{
    public enum NPCchars
    {
        Default,
        Justin,
        David,
        Father,
        MLP,
        Captain,
        Antagonist,
        OldFriend,
    }

    public enum ContentBelongsTo
    {
        One,
        Two
    }

    private Dictionary<string,int> FeelingsDict;
    private GameObject CardPrefab;
    public string InputWords;
    public ContentBelongsTo ContentNumber;
    public NPCchars GreyIf;
    public int FeelingsLessThan;

    private GameObject newCard;


    public override void OnEnter()
    {
        CardPrefab = Resources.Load("Prefabs/Card", typeof(GameObject)) as GameObject;

        FeelingsDict = GameObject.Find("CharacterFeelings").GetComponent<FeelingsList>().FeelingsDict;

        newCard = Instantiate(CardPrefab) as GameObject;
        newCard.GetComponent<CardClickFunctions>().defaultWord = InputWords;
        
        //this is where to add card
        GameObject ContentObj;
        if (ContentNumber == ContentBelongsTo.One)
        {
            ContentObj = GameObject.Find("Content1");
            newCard.transform.SetParent(ContentObj.transform, false);
        }
        else if (ContentNumber == ContentBelongsTo.Two)
        {
            ContentObj = GameObject.Find("Content2");
            newCard.transform.SetParent(ContentObj.transform, false);
        }

        //this is whether card is grey or not
        string npc = GreyIf.ToString();
        if(FeelingsDict[npc] < FeelingsLessThan)
        {
            newCard.GetComponent<IsDraggable>().enabled = false;
            newCard.GetComponent<Image>().color = Color.gray;
        }

        Continue();
    }

    
    
}
