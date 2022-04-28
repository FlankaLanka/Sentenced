using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class ConversationTransitions : Command
{
    [Header("Transition")]
    public bool chooseToFade;
    public bool comingInFromMatchPanel;

    public override void OnEnter()
    {

        Continue();
    }
}
