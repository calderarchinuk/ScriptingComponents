using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTeleportOffset : ActionBase
{
    public Transform[] Targets;
    public Vector3 ApplyOffset;
    public override void Activate(GameObject instigator)
    {
        foreach(var t in Targets)
        {
            t.position += ApplyOffset;
        }
    }
}
