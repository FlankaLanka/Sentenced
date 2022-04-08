using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour
{
    public Flowchart mainFlow;

    [Header("Emotion Icon Sprites")]
    public Sprite AngryIcon;
    public Sprite ExclamIcon;
    public Sprite IdeaIcon;
    public Sprite MessyIcon;
    public Sprite QuestionIcon;
    public Sprite SweatIcon;

    private AudioSource[] a;
    private GameObject emotionIcon;

    private Transform dialogueContainer;
    private Transform tempDialogue;
    private Transform LeftOutOfView;
    private Transform LeftMainText;
    private Transform LeftAboveText;
    private Transform RightOutOfView;
    private Transform RightMainText;
    private Transform RightAboveText;

    private GameObject LeftBubble;
    private GameObject RightBubble;
    private Sprite WhiteLeftBubble;
    private Sprite WhiteRightBubble;

    private GameObject LeftPerson;
    private GameObject RightPerson;

    [Header("Outline Materials")]
    public Material ColoredOutline;
    public Material NoOutline;
    private DialogueClass.OutlineColors prevOutline;


    [HideInInspector]
    public DialogueClass currentSentences;
    [HideInInspector]
    public int i = 0;
    [HideInInspector]
    public GameObject MatchQMark;
    [HideInInspector]
    public GameObject NextIsFinish;
    [HideInInspector]
    public GameObject NextIsLeft;
    [HideInInspector]
    public GameObject NextIsRight;
    [HideInInspector]
    public bool LeadsToMatchPanel;

    private void Awake()
    {
        //set outline color mat to 0, it gets modified in game and applies to material
        ColoredOutline.SetFloat("DrawTime", 0f);

        a = GetComponents<AudioSource>();
        emotionIcon = transform.Find("EmotionIcon").gameObject;
        emotionIcon.SetActive(false);

        dialogueContainer = transform.Find("Dialogue");
        tempDialogue = dialogueContainer.Find("TempDialogue");

        LeftOutOfView = dialogueContainer.Find("LeftOutOfView");
        LeftMainText = dialogueContainer.Find("LeftMain");
        LeftAboveText = dialogueContainer.Find("LeftTop");

        RightOutOfView = dialogueContainer.Find("RightOutOfView");
        RightMainText = dialogueContainer.Find("RightMain");
        RightAboveText = dialogueContainer.Find("RightTop");

        LeftBubble = Resources.Load("Prefabs/LeftMain") as GameObject;
        RightBubble = Resources.Load("Prefabs/RightMain") as GameObject;
        WhiteLeftBubble = Resources.Load<Sprite>("ConvoPanelSprites/convo_small_white");
        WhiteRightBubble = Resources.Load<Sprite>("ConvoPanelSprites/convo_small_white_r");

        MatchQMark = transform.Find("MatchPanelQMark").gameObject;
        NextIsFinish = transform.Find("NextLineFinish").gameObject;
        NextIsLeft = transform.Find("NextLineLeft").gameObject;
        NextIsRight = transform.Find("NextLineRight").gameObject;

        LeftPerson = transform.Find("LeftPersonImage").gameObject;
        RightPerson = transform.Find("RightPersonImage").gameObject;
    }

    private void OnEnable()
    {
        prevOutline = DialogueClass.OutlineColors.None;

        //i = 0;
        if (currentSentences.sentence.Count > 0)
        {
            if (currentSentences.LeftCharSpeaking[0])
            {
                NextIsLeft.SetActive(true);
                NextIsLeft.GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                NextIsRight.SetActive(true);
                NextIsLeft.GetComponent<Button>().onClick.Invoke();  
            }
        }
    }

    public void DisplayNextLine()
    {
        //0 is voice
        //1 is button sound
        //a[0].Play();
        a[1].Play();

        //this will fix the color of the button to original since onpointerexit doesnt trigger when clicked
        
        NextIsFinish.GetComponent<ConvoChangeOnPointer>().changeToOriginal();
        NextIsLeft.GetComponent<ConvoChangeOnPointer>().changeToOriginal();
        NextIsRight.GetComponent<ConvoChangeOnPointer>().changeToOriginal();
        
        MatchQMark.SetActive(false);
        NextIsFinish.SetActive(false);
        NextIsLeft.SetActive(false);
        NextIsRight.SetActive(false);

        if (i < currentSentences.sentence.Count)
        {
            //create the new dialogue
            GameObject newLine;
            if (currentSentences.LeftCharSpeaking[i])
            {
                newLine = Instantiate(LeftBubble, LeftMainText.position, Quaternion.identity, tempDialogue);
                newLine.GetComponent<ConversationState>().IsLeftChat = true;
            }
            else
            {
                newLine = Instantiate(RightBubble, RightMainText.position, Quaternion.identity, tempDialogue);
                newLine.GetComponent<ConversationState>().IsLeftChat = false;
                //for right dialogue, get the emotion image
                //newLine.transform.Find("EmotionImageInText").GetComponent<Image>().sprite = emotionIcon.GetComponent<Image>().sprite;

                //change emotion icon
                if (currentSentences.emotionSprites.Count > i)
                {
                    if (currentSentences.emotionSprites[i] == DialogueClass.Emotions.None)
                    {
                        newLine.transform.Find("EmotionImageInText").GetComponent<Image>().sprite = null;
                        newLine.transform.Find("EmotionImageInText").GetComponent<Image>().enabled = false;
                    }
                    else
                    {
                        newLine.transform.Find("EmotionImageInText").GetComponent<Image>().enabled = true;
                        if (currentSentences.emotionSprites[i] == DialogueClass.Emotions.Angry)
                        {
                            newLine.transform.Find("EmotionImageInText").GetComponent<Image>().sprite = AngryIcon;
                        }
                        else if (currentSentences.emotionSprites[i] == DialogueClass.Emotions.Exclamation)
                        {
                            newLine.transform.Find("EmotionImageInText").GetComponent<Image>().sprite = ExclamIcon;
                        }
                        else if (currentSentences.emotionSprites[i] == DialogueClass.Emotions.Idea)
                        {
                            newLine.transform.Find("EmotionImageInText").GetComponent<Image>().sprite = IdeaIcon;
                        }
                        else if (currentSentences.emotionSprites[i] == DialogueClass.Emotions.Messy)
                        {
                            newLine.transform.Find("EmotionImageInText").GetComponent<Image>().sprite = MessyIcon;
                        }
                        else if (currentSentences.emotionSprites[i] == DialogueClass.Emotions.Question)
                        {
                            newLine.transform.Find("EmotionImageInText").GetComponent<Image>().sprite = QuestionIcon;
                        }
                        else if (currentSentences.emotionSprites[i] == DialogueClass.Emotions.Sweat)
                        {
                            newLine.transform.Find("EmotionImageInText").GetComponent<Image>().sprite = SweatIcon;
                        }

                    }
                }
            }
            newLine.GetComponent<ConversationState>().status = StatementStatus.AboutToSpeak;
            newLine.GetComponentInChildren<Text>().text = currentSentences.sentence[i];
            //change font
            if (currentSentences.textFont.Count > i)
            {
                if (currentSentences.textFont[i] != 0)
                    newLine.GetComponentInChildren<Text>().fontSize = currentSentences.textFont[i];
            }

            


            //dialogue play
            if (currentSentences.voiceClips.Count > i)
            {
                a[0].clip = currentSentences.voiceClips[i];
                a[0].Play();
            }

            //change character outline
            if(currentSentences.emotionOutlines.Count > i)
            {
                if (currentSentences.emotionOutlines[i] == DialogueClass.OutlineColors.None)
                {
                    prevOutline = DialogueClass.OutlineColors.None;
                    RightPerson.GetComponent<Image>().material = NoOutline;
                }
                else if (currentSentences.emotionOutlines[i] == DialogueClass.OutlineColors.Red &&
                         prevOutline != DialogueClass.OutlineColors.Red)
                {
                    prevOutline = DialogueClass.OutlineColors.Red;
                    RightPerson.GetComponent<Image>().material = ColoredOutline;
                    StartCoroutine(OutlineTrace(Color.red, ColoredOutline));
                }
                else if (currentSentences.emotionOutlines[i] == DialogueClass.OutlineColors.Green &&
                         prevOutline != DialogueClass.OutlineColors.Green)
                {
                    prevOutline = DialogueClass.OutlineColors.Green;
                    RightPerson.GetComponent<Image>().material = ColoredOutline;
                    StartCoroutine(OutlineTrace(Color.green, ColoredOutline));
                }
                else if (currentSentences.emotionOutlines[i] == DialogueClass.OutlineColors.Blue &&
                         prevOutline != DialogueClass.OutlineColors.Blue)
                {
                    prevOutline = DialogueClass.OutlineColors.Blue;
                    RightPerson.GetComponent<Image>().material = ColoredOutline;
                    StartCoroutine(OutlineTrace(Color.blue, ColoredOutline));
                }
                else if (currentSentences.emotionOutlines[i] == DialogueClass.OutlineColors.Yellow &&
                         prevOutline != DialogueClass.OutlineColors.Yellow)
                {
                    prevOutline = DialogueClass.OutlineColors.Yellow;
                    RightPerson.GetComponent<Image>().material = ColoredOutline;
                    StartCoroutine(OutlineTrace(Color.yellow, ColoredOutline));
                }
            }


            //move all other dialogues
            foreach(Transform child in tempDialogue)
            {
                MoveDialogue(child);
            }

            i++;

            //decide which button to display
            if(i <= currentSentences.sentence.Count)
            {
                if(i == currentSentences.sentence.Count)
                {
                    //last sentence leads to match panel or main
                    if(LeadsToMatchPanel)
                    {
                        NextIsLeft.SetActive(true);
                        MatchQMark.SetActive(true);
                    }
                    else
                    {
                        NextIsFinish.SetActive(true);
                    }
                }
                else
                {
                    if(currentSentences.LeftCharSpeaking[i])
                    {
                        NextIsLeft.SetActive(true);
                    }
                    else
                    {
                        NextIsRight.SetActive(true);
                    }
                }
            }
        }
        else
        {
            //end the conversation panel and go to next part in fungus
            mainFlow.SetBooleanVariable("NextStep", true);

            //delete all the statements and reset i
            foreach(Transform child in tempDialogue)
            {
                Destroy(child.gameObject);
            }
        }


        //reset audioclip to the click sound
    }


    private IEnumerator OutlineTrace(Color outlineColor, Material outline)
    {
        outline.SetColor("OutlineColor", outlineColor);

        float timer = 0f;
        float outlineTimer = 1f;
        while(timer < outlineTimer)
        {
            yield return null;
            timer += Time.deltaTime;
            outline.SetFloat("DrawTime", 1 - timer / outlineTimer);
        }
    }




    private void MoveDialogue(Transform bubble)
    {
        bool isLeft = bubble.GetComponent<ConversationState>().IsLeftChat;
        StatementStatus bubbleStatus = bubble.GetComponent<ConversationState>().status;
        if (bubbleStatus == StatementStatus.AboutToSpeak)
        {
            bubble.GetComponent<ConversationState>().status = StatementStatus.JustSpoke;
        }
        else if (bubbleStatus == StatementStatus.JustSpoke)
        {
            if(isLeft)
            {
                StartCoroutine(LerpDiagonal(bubble, LeftAboveText.position, 0.5f));
                bubble.GetComponent<Image>().sprite = WhiteLeftBubble;
            }
            else
            {
                StartCoroutine(LerpDiagonal(bubble, RightAboveText.position, 0.5f));
                bubble.GetComponent<Image>().sprite = WhiteRightBubble;
            }
            bubble.GetComponentInChildren<Text>().color = Color.black;
            //bubble.GetComponentInChildren<Text>().fontSize = (int)(bubble.GetComponentInChildren<Text>().fontSize / 0.6f);
            bubble.GetComponent<ConversationState>().status = StatementStatus.AlreadyMovedUp;
        }
        else if (bubbleStatus == StatementStatus.AlreadyMovedUp)
        {
            if(isLeft)
            {
                StartCoroutine(LerpUp(bubble, LeftOutOfView.position, 0.5f));
            }
            else
            {
                StartCoroutine(LerpUp(bubble, RightOutOfView.position, 0.5f));
            }
            bubble.GetComponent<ConversationState>().status = StatementStatus.OutOfView;
        }
        /*
        else if (bubbleStatus == StatementStatus.OutOfView)
        {
            //do nothing, the objects will be deleted when conversation panel closes
        }
        */
    }

    private IEnumerator LerpDiagonal(Transform start, Vector2 end, float totalTime)
    {
        Vector3 scale = start.localScale;
        //Transform dialogueText = start.GetChild(0);
        float timer = 0f;

        //

        while(timer < totalTime && start != null && start.GetComponent<ConversationState>().status != StatementStatus.OutOfView)
        {
            start.position = Vector2.Lerp(start.position, end, Mathf.Min(timer / totalTime, 1f));
            if(start.localScale.x > 0.75f)
            {
                start.localScale = scale - scale * timer / totalTime;
                //dialogueText.localScale = start.localScale;
            }
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
    }

    private IEnumerator LerpUp(Transform start, Vector2 end, float totalTime)
    {
        float timer = 0f;
        while (timer < totalTime && start != null)
        {
            start.position = Vector2.Lerp(start.position, end, Mathf.Min(timer / totalTime, 1f));
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
    }
}
