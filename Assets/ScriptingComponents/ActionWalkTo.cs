using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionWalkTo : ActionBase
{

	public NavMeshAgent Agent;
	public Transform DestinationTarget;

	public override void Activate (GameObject instigator)
	{
		Agent.SetDestination(DestinationTarget.position);
	}

	public void OnDrawGizmos()
	{
		#if UNITY_EDITOR

		//Destination = UnityEditor.Handles.PositionHandle(Destination,Quaternion.identity);
		RaycastHit hit = new RaycastHit();

		if (DestinationTarget == null){return;}
		if (Physics.Raycast(DestinationTarget.position,Vector3.down,out hit, 10f))
		{
			Gizmos.DrawLine(DestinationTarget.position,hit.point);
			Gizmos.DrawWireCube(hit.point,new Vector3(0.5f,0,0.5f));
		}

		if (Agent != null)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(Agent.transform.position,DestinationTarget.position);
		}

		#endif
	}

}
