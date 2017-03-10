using UnityEngine;
using System.Collections;

/// only used in the delivery dungeon and tutorial. activated when a nearby encounter is completed

public class EventDeliveryGate : EventBase
{
	public float Distance = 20;

	public void Activate()
	{
		ActivateActions(null);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position,Distance);
	}
}