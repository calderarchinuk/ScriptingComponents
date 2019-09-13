using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSetTransform : ActionBase
{
    public Transform Target;
    public bool DestroyRigidbody;
    public Transform FinalTransform;

    public override void Activate(GameObject instigator)
    {
        if (instigator != null)
        {
            Target = instigator.transform;
        }

        if (DestroyRigidbody)
            Destroy(Target.GetComponent<Rigidbody>());
        Target.position = FinalTransform.position;
        Target.rotation = FinalTransform.rotation;
    }
}
