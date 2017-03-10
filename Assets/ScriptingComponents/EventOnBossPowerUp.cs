using UnityEngine;
using System.Collections;

///called when a boss NPC controller 'powers up' ie reaches half health
public class EventOnBossPowerUp : EventBase
{
	public void Activate(GameObject boss)
	{
		ActivateActions(boss);
	}
}
