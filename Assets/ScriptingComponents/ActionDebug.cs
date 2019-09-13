using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDebug : ActionBase
{
    [SerializeField] private float ActivateTime;
    [SerializeField] private string Message;
    public override void Activate(GameObject instigator)
    {
        base.Activate(instigator);
        Debug.Log("[DEBUG ACTION] "+Message + ": " + Time.time,this);
        ActivateTime = Time.time;
    }
}
