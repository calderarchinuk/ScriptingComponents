using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRecenter : EventBase
{
	void Update ()
    {
        if (used) { return; }
        if (Input.GetKeyDown(KeyCode.JoystickButton8) || Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            ActivateActions(null);
        }
    }
}
