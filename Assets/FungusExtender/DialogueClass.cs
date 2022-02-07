using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DialogueClass
{
    public enum OutlineColors
    {
        Red,
        Green,
        Blue,
        Yellow,
        None
    }

    public enum Emotions
    {
        None,
        Angry,
        Exclamation,
        Idea,
        Messy,
        Question,
        Sweat
    }

    public DialogueClass()
    {
        sentence = new List<string>();
        LeftCharSpeaking = new List<bool>();
        voiceClips = new List<AudioClip>();
        emotionSprites = new List<Emotions>();
        emotionOutlines = new List<OutlineColors>();
    }

    public void CopyDialogue(DialogueClass x)
    {
        this.sentence = new List<string>(x.sentence);
        this.LeftCharSpeaking = new List<bool>(x.LeftCharSpeaking);
        this.voiceClips = new List<AudioClip>(x.voiceClips);
        this.emotionSprites = new List<Emotions>(x.emotionSprites);
        this.emotionOutlines = new List<OutlineColors>(x.emotionOutlines);
    }


    [TextArea(3, 10)]
    public List<string> sentence;
    public List<bool> LeftCharSpeaking;
    public List<AudioClip> voiceClips;
    public List<Emotions> emotionSprites;
    public List<OutlineColors> emotionOutlines;
}
