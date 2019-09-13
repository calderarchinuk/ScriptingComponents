using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSetLineWidth : ActionBase
{
    public LineRenderer Line;
    public float lineWidth = 0.01f;
    public override void Activate(GameObject instigator)
    {
        base.Activate(instigator);
        Line.startWidth = lineWidth;
        Line.endWidth = lineWidth;
    }
}
