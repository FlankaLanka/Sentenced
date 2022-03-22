using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;
using UnityEngine.Assertions;

[CommandInfo("Narrative",
             "DialogueBoxModifier",
             "Modifies dialog box. Current values are the default.")]
public class SayExtended : Command
{
    public enum DialogueLocation
    {
        Left,
        Right,
        Center
    }

    [Tooltip("Determine Dialogue Settings For Sentence")]
    
    [SerializeField] protected DialogueLocation dialogueSpot = DialogueLocation.Left;
    [SerializeField] protected int TextFont = 55;
    [SerializeField] protected FontStyle TextStyle = FontStyle.Normal;
    [SerializeField] protected float LineSpacing = 1f;

    [Header("Dialogue Box Size")]
    [SerializeField] protected float LeftStart = 358.1f;
    [SerializeField] protected float TopStart = 62.4389f;
    [SerializeField] protected float RightEnd = 519.425f;
    [SerializeField] protected float BottomEnd = 83.0685f;

    [Header("Drag the Sentenced Dialogue Box here again")]
    [SerializeField] protected GameObject SentencedDialoguePrefab;

    

    public override void OnEnter()
    {
        
        /*GameObject SentencedDialoguePrefab = null;
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if(go.name == "SentencedDialogue")
            {
                SentencedDialoguePrefab = go;
            }
        }
        */
        Transform SentencedDialogueStory = SentencedDialoguePrefab.transform.Find("Panel").transform.Find("StoryText");
        Assert.IsNotNull(SentencedDialoguePrefab);
        Assert.IsNotNull(SentencedDialogueStory);

        RectTransform StoryTextBox = SentencedDialogueStory.GetComponent<RectTransform>();
        Text StoryText = SentencedDialogueStory.GetComponent<Text>();

        //set rects for story box
        SetLeft(StoryTextBox, LeftStart);
        SetRight(StoryTextBox, RightEnd);
        SetTop(StoryTextBox, TopStart);
        SetBottom(StoryTextBox, BottomEnd);

        //set text stuff for story box
        if (dialogueSpot == DialogueLocation.Left)
        {
            StoryText.alignment = TextAnchor.UpperLeft;
        }
        else if (dialogueSpot == DialogueLocation.Center)
        {
            StoryText.alignment = TextAnchor.UpperCenter;
        }
        else if (dialogueSpot == DialogueLocation.Right)
        {
            StoryText.alignment = TextAnchor.UpperRight;
        }
        StoryText.fontSize = TextFont;
        StoryText.lineSpacing = LineSpacing;

        if(TextStyle == FontStyle.Normal)
        {
            StoryText.fontStyle = FontStyle.Normal;
        }
        else if(TextStyle == FontStyle.Bold)
        {
            StoryText.fontStyle = FontStyle.Bold;
        }
        else if(TextStyle == FontStyle.Italic)
        {
            StoryText.fontStyle = FontStyle.Italic;
        }
        else if(TextStyle == FontStyle.BoldAndItalic)
        {
            StoryText.fontStyle = FontStyle.BoldAndItalic;
        }


        //reinstance saydialogue
        SayDialog.ActiveSayDialog = SentencedDialoguePrefab.GetComponent<SayDialog>();

        Continue();
    }
    public static void SetLeft(RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

}
