using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class DialogueSystem : MonoBehaviour
{
    public Flowchart mainFlow;

    //private int totalLines;
    private int displayLine;
    private bool allSixDisplayed;
    //old variables

    public DialogueClass currentSentences;

    public int i = 0;

    private AudioSource a;

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

    public GameObject MatchQMark;
    public GameObject NextIsFinish;
    public GameObject NextIsLeft;
    public GameObject NextIsRight;

    public bool LeadsToMatchPanel;

    private void Awake()
    {
        a = GetComponent<AudioSource>();

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
        WhiteLeftBubble = Resources.Load<Sprite>("ConvoPanelSprites/convo_bubble_white_circle");
        WhiteRightBubble = Resources.Load<Sprite>("ConvoPanelSprites/convo_bubble_white_rect");

        MatchQMark = transform.Find("MatchPanelQMark").gameObject;
        NextIsFinish = transform.Find("NextLineFinish").gameObject;
        NextIsLeft = transform.Find("NextLineLeft").gameObject;
        NextIsRight = transform.Find("NextLineRight").gameObject;

    }

    private void OnEnable()
    {
        //i = 0;

        if (currentSentences.sentence.Count > 0)
        {
            if (currentSentences.LeftCharSpeaking[0])
            {
                NextIsLeft.SetActive(true);
            }
            else
            {
                NextIsRight.SetActive(true);
            }
        }

    }

    public void DisplayNextLine()
    {
        a.Play();

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
            }
            newLine.GetComponent<ConversationState>().status = StatementStatus.AboutToSpeak;
            newLine.GetComponentInChildren<Text>().text = currentSentences.sentence[i];

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
        Transform dialogueText = start.GetChild(0);
        float timer = 0f;
        while(timer < totalTime && start.GetComponent<ConversationState>().status != StatementStatus.OutOfView)
        {
            //prevent error if clicking past text too fast
            if (start.GetComponent<RectTransform>() == null)
                break;

            start.position = Vector2.Lerp(start.position, end, Mathf.Min(timer / totalTime, 1f));
            if(start.localScale.x > 0.75f)
            {
                start.localScale = scale - scale * timer / totalTime;
                dialogueText.localScale = start.localScale;
            }
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
    }

    private IEnumerator LerpUp(Transform start, Vector2 end, float totalTime)
    {
        float timer = 0f;
        while (timer < totalTime)
        {
            //prevent error if clicking past text too fast
            if (start.GetComponent<RectTransform>() == null)
                break;

            start.position = Vector2.Lerp(start.position, end, Mathf.Min(timer / totalTime, 1f));
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
    }
}
