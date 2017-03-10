using UnityEngine;
using System.Collections;

///in overworld for condition checks
public class EventPostSceneTransition : EventBase
{
	public void Activate()
	{
		ActivateActions(null);
	}
}
