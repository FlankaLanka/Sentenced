using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeelingsList : MonoBehaviour
{
    public Dictionary<string, int> FeelingsDict;

    private Text DebugFeelings;

    private void Start()
    {
        DebugFeelings = gameObject.GetComponent<Text>();
        FeelingsDict = new Dictionary<string, int>();
        FeelingsDict.Add("Default", 100);
        FeelingsDict.Add("Justin", 100);
        FeelingsDict.Add("David", 100);
        FeelingsDict.Add("Father", 100);
        FeelingsDict.Add("MLP", 100);
        FeelingsDict.Add("Captain", 100);
        FeelingsDict.Add("Antagonist", 100);
        FeelingsDict.Add("OldFriend", 100);
    }

    private void Update()
    {
        DebugFeelings.text = "";
        foreach(KeyValuePair<string,int> entry in FeelingsDict)
        {
            DebugFeelings.text += entry.Key + ":" + entry.Value + "\n";
        }
    }

}
