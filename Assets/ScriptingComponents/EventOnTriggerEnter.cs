using UnityEngine;
using System.Collections;

public class EventOnTriggerEnter : EventBase
{
	public bool DisableOnEnter;
	public bool PlayerOnly = true;

	void OnTriggerEnter(Collider c)
	{
		if (PlayerOnly)
		{
			if (c.tag != "Player"){return;}
		}
		if (DisableOnEnter)
		{
			enabled = false;
		}
		ActivateActions(c.gameObject);
	}
}
