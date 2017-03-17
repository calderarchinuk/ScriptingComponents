using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionPathTo : ActionBase
{

	public NavMeshAgent Agent;
	public Vector3 Destination;

	public override void Activate (GameObject instigator)
	{
		Agent.SetDestination(Destination);
	}

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		Utility.GroundGizmo(transform.position);

		if (Agent != null)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(Agent.transform.position,Destination);
		}
	}

}
