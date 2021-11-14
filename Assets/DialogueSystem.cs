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

    public List<string> s;
    public List<bool> l;
    public DialogueClass currentSentences;

    public int i = 0;

    private Transform dialogueContainer;
    private Transform tempDialogue;
    private Transform LeftOutOfView;
    private Transform LeftMainText;
    private Transform LeftAboveText;
    private Transform RightOutOfView;
    private Transform RightMainText;
    private Transform RightAboveText;
    public GameObject LeftBubble;
    public GameObject RightBubble;

    private void Start()
    {
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
    }

    /*
    void OnEnable()
    {
        //i = 0;

        
        displayLine = 1;
        allSixDisplayed = false;

        dialogueContainer = transform.Find("Dialogue");
        foreach (Transform child in dialogueContainer)
        {
            child.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            child.gameObject.GetComponentInChildren<Text>().color = new Color(0.2f, 0.2f, 0.2f, 0f);
        }
        
    }
*/
    private void Update()
    {
        Debug.Log(currentSentences.sentence.Count);
    }


    public void DisplayNextLine()
    {
        Debug.Log("SPEAking");
        //Debug.Log(currentSentences.sentence.Count);


        
        if (i < currentSentences.sentence.Count)
        {
            Debug.Log("SPEAKER");
            
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




        /*
        GameObject currentDialogue = dialogueContainer.Find("Dialogue" + (displayLine).ToString()).gameObject;
        if(currentDialogue.GetComponentInChildren<Text>().text != "temp_text" && !allSixDisplayed)
        {
            currentDialogue.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            currentDialogue.GetComponentInChildren<Text>().color = new Color(0.2f, 0.2f, 0.2f, 1f);
        }
        else
        {
            mainFlow.SetBooleanVariable("NextStep", true);
        }

        if(displayLine < 6)
        {
            displayLine++;
        }
        else
        {
            allSixDisplayed = true;
        }
        */
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
            }
            else
            {
                StartCoroutine(LerpDiagonal(bubble, RightAboveText.position, 0.5f));
            }
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
        else if (bubbleStatus == StatementStatus.OutOfView)
        {
            //do nothing, the objects will be deleted when conversation panel closes
        }
    }

    private IEnumerator LerpDiagonal(Transform start, Vector2 end, float totalTime)
    {
        float timer = 0f;
        while(timer < totalTime && start.GetComponent<ConversationState>().status != StatementStatus.OutOfView)
        {
            //prevent error if clicking past text too fast
            if (start.GetComponent<RectTransform>() == null)
                break;

            start.position = Vector2.Lerp(start.position, end, Mathf.Min(timer / totalTime, 1f));
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
