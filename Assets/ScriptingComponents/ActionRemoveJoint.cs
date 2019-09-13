using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRemoveJoint : ActionBase
{
    public Joint Target;
    public override void Activate(GameObject instigator)
    {
        base.Activate(instigator);
        Destroy(Target);
    }
}
