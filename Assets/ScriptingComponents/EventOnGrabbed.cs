using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnGrabbed : EventBase
{
    public void OnGrabbed(GameObject hand)
    {
        ActivateActions(hand);
    }
}
