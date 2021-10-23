using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("MatchMenu", "Disable Match Panel", "Disable Match Panel")]

public class MatcherClose : Command
{
    private GameObject matchCanvas;
    private GameObject content1;
    private GameObject content2;
    //public bool DeleteCards;
    public override void OnEnter()
    {
        content1 = GameObject.Find("Content1");
        content2 = GameObject.Find("Content2");

        //if(DeleteCards)
        //{
        foreach (Transform child in content1.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in content2.transform)
        {
            Destroy(child.gameObject);
        }
        //}

        matchCanvas = GameObject.Find("MatchCanvas");
        matchCanvas.transform.Find("MatchPanel").gameObject.SetActive(false);

        Continue();
    }
}