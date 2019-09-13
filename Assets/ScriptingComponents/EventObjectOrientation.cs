using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//checks to see if a gameobject with a mesh by name is within a distance and orientation of a target
//action activated should snap or lock or whatever. instigator is the root

public class EventObjectOrientation : EventBase
{
    public Transform Target;
    public bool SearchForTool = false;

    public Transform[] ObjectTarget; //for specific objects to compare against

    public float Distance = 0.1f;
    public float UpDot = 0.9f;
    public bool AllowUpsideDown = false;
    public bool UseForwardDot = true;
    public float ForwardDot = 0.9f;

    private void Update()
    {
        if (used) { return; }

		for (int i = 0; i < ObjectTarget.Length; i++)
		{
			if (ObjectTarget[i] == null) { continue; }

			var dot = Vector3.Dot(Target.up, ObjectTarget[i].up);
			if (dot < UpDot && (!AllowUpsideDown || dot > -UpDot)) { continue; }
			if (UseForwardDot && Vector3.Dot(Target.forward, ObjectTarget[i].forward) < ForwardDot) { continue; }

			if (Vector3.Distance(Target.position, ObjectTarget[i].position) < Distance)
			{
				if (OneTimeOnly)
				{
					ActivateActions(ObjectTarget[i].gameObject);
					return;
				}
			}
		}
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (Target != null)
        {
            Gizmos.DrawWireSphere(Target.position, Distance);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(Target.position, Target.up*0.1f);
            if (UseForwardDot)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(Target.position, Target.forward*0.1f);
            }
        }
    }
}
