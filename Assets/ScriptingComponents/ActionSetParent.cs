using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSetParent : ActionBase
{
    public Transform Target;
    public Transform Parent;

    public override void Activate(GameObject instigator)
    {
        if (Target != null)
        {
            Target.SetParent(Parent);
        }
    }
}
