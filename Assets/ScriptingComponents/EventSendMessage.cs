using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSendMessage : EventBase
{
    public void ReceiveMessage()
    {
        if (used) { return; }
        ActivateActions(null);
    }
}
