using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTargetsNearby : EventBase
{
    //get hand position

    public float Distance = 0.1f;

    public GameObject[] targets;

	public IteratorType RequireAny = IteratorType.Any;

    private void Start()
    {
		if (targets == null)
        {
			Debug.LogWarning("EventTargetsNearby targets not set");
        }
    }

    private void FixedUpdate()
    {
		if (RequireAny == IteratorType.Any)
		{
			foreach(var go in targets)
			{
				if (Vector3.Distance(transform.position,go.transform.position) < Distance)
				{
					ActivateActions(null);
					return;
				}
			}
		}
		else
		{
			bool withinRange = true;
			foreach(var go in targets)
			{
				if (Vector3.Distance(transform.position,go.transform.position) > Distance)
				{
					withinRange = false;
				}
			}
			if (withinRange)
			{
				ActivateActions(null);
			}
		}
    }
}
