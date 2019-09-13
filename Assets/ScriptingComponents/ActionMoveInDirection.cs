using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//move in a direction in update. used for removing stuff like caps

public class ActionMoveInDirection : ActionBase
{
    public Transform Target;
    public Vector3 Vector = new Vector3(0, 0.2f, 0);
    bool doMovement = false;

    public override void Activate(GameObject instigator)
    {
        base.Activate(instigator);
        doMovement = true;
    }

    private void Update()
    {
        if (doMovement && Target != null)
            Target.position += Vector * Time.deltaTime;
    }
}
