using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatementStatus
{
    AboutToSpeak, //when it comes up on screen first time
    JustSpoke, //when it is already on screen as the most recent sentence said
    AlreadyMovedUp, //when it has already moved up one time
    OutOfView
}

public class ConversationState : MonoBehaviour
{
    public bool IsLeftChat;
    public StatementStatus status;
}
