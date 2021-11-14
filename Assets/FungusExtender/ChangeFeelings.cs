using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("NPC", "Change Feeling", "Choose NPC to add to their feeling, subtracting = adding negatives")]

public class ChangeFeelings : Command
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

    private Dictionary<string, int> FeelingsDict;
    public NPCchars Name;
    public int AmountToAdd;
    public override void OnEnter()
    {
        FeelingsDict = GameObject.Find("CharacterFeelings").GetComponent<FeelingsList>().FeelingsDict;
        FeelingsDict[Name.ToString()] += AmountToAdd;
        Continue();
    }
}
