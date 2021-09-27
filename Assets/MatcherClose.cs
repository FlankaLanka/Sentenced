using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("MatchMenu", "Disable Match Panel", "Disable Match Panel")]

public class MatcherClose : Command
{
    private GameObject matchCanvas;
    public override void OnEnter()
    {
        matchCanvas = GameObject.Find("MatchCanvas");
        matchCanvas.transform.Find("MatchPanel").gameObject.SetActive(false);
        Continue();
    }
}