using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("MatchMenu", "Disable Match Panel", "Disable Match Panel")]

public class MatcherClose : Command
{
    private GameObject matchCanvas;
    private Transform content1;
    private Transform content2;
    //public bool DeleteCards;
    public override void OnEnter()
    {
        matchCanvas = GameObject.Find("MatchCanvas");
        Transform matchPanel = matchCanvas.transform.Find("MatchPanel");
        if (matchPanel.gameObject.activeInHierarchy)
        {
            content1 = matchPanel.transform.Find("Content1");
            content2 = matchPanel.transform.Find("Content2");

            //if(DeleteCards)
            //{
            foreach (Transform child in content1)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in content2)
            {
                Destroy(child.gameObject);
            }
            //}

            matchPanel.gameObject.SetActive(false);
        }


        Continue();
    }
}