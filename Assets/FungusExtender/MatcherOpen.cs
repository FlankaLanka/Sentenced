using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

[CommandInfo("MatchMenu", "Enable Match Panel", "Enable Match Panel")]

public class MatcherOpen : Command
{
    private GameObject matchPanel;
    public override void OnEnter()
    {
        matchPanel = GameObject.Find("MatchCanvas").transform.Find("MatchPanel").gameObject;
        matchPanel.SetActive(true);
        
        //Color matchColor = matchPanel.GetComponent<Image>().color;
        matchPanel.transform.Find("CardSlot1").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        matchPanel.transform.Find("CardSlot2").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        Continue();
    }
}