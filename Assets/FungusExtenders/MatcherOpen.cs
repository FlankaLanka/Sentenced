using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("MatchMenu", "Enable Match Panel", "Enable Match Panel")]

public class MatcherOpen : Command
{
    private GameObject matchCanvas;
    public override void OnEnter()
    {
        matchCanvas = GameObject.Find("MatchCanvas");
        matchCanvas.transform.Find("MatchPanel").gameObject.SetActive(true);
        Continue();
    }
}