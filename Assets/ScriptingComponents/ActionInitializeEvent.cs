using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//events are initialized on level startup, so this is only needed for events created during the scene

public class ActionInitializeEvent : ActionBase
{
	public List<EventBase> Events = new List<EventBase>();

	public override void Activate (GameObject entity)
	{
		foreach (var v in Events)
		{
			v.Initialize();
		}
	}
}
