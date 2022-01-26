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
        voiceClips = new List<AudioClip>();
    }

    public void CopyDialogue(DialogueClass x)
    {
        this.sentence = new List<string>(x.sentence);
        this.LeftCharSpeaking = new List<bool>(x.LeftCharSpeaking);
        this.voiceClips = new List<AudioClip>(x.voiceClips);
    }


    [TextArea(3, 10)]
    public List<string> sentence;
    public List<bool> LeftCharSpeaking;
    public List<AudioClip> voiceClips;
}
