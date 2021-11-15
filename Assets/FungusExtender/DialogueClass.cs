using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DialogueClass
{
    public DialogueClass()
    {
        sentence = new List<string>();
        LeftCharSpeaking = new List<bool>();
    }

    public void CopyDialogue(DialogueClass x)
    {
        this.sentence = new List<string>(x.sentence);
        this.LeftCharSpeaking = new List<bool>(x.LeftCharSpeaking);
    }


    [TextArea(3, 10)]
    public List<string> sentence;
    public List<bool> LeftCharSpeaking;
}
