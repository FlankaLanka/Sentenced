using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("MatchPanel", "Cards Fade In", "Fade the cards in for transition. Use after the last create card command.")]

public class CardFadeIn : Command
{
    private GameObject cardContainer;
    public override void OnEnter()
    {
        cardContainer = GameObject.Find("");
    }

}