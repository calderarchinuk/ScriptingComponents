using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//only used to set

public class ActionSetRigidbody : ActionBase
{
    public Rigidbody Target;

    public bool LockXRotation;
    public bool LockYRotation;
    public bool LockZRotation;

    public bool LockXPosition;
    public bool LockYPosition;
    public bool LockZPosition;

    public bool SetInitialTransform;
    Vector3 InitialPos;
    Quaternion InitialRot;
    private void Start()
    {
        InitialPos = Target.position;
        InitialRot = Target.rotation;
    }

    public override void Activate(GameObject instigator)
    {
        base.Activate(instigator);
        if (SetInitialTransform)
        {
            Target.MovePosition(InitialPos);
            Target.MoveRotation(InitialRot);
        }
        RigidbodyConstraints NewConstraints = new RigidbodyConstraints();

        if (LockXPosition)
            NewConstraints |= RigidbodyConstraints.FreezePositionX;
        if (LockYPosition)
            NewConstraints |= RigidbodyConstraints.FreezePositionY;
        if (LockZPosition)
            NewConstraints |= RigidbodyConstraints.FreezePositionZ;

        if (LockXRotation)
            NewConstraints |= RigidbodyConstraints.FreezeRotationX;
        if (LockYRotation)
            NewConstraints |= RigidbodyConstraints.FreezeRotationY;
        if (LockZRotation)
            NewConstraints |= RigidbodyConstraints.FreezeRotationZ;

        Target.constraints = NewConstraints;
    }
}
