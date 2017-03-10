using UnityEngine;
using System.Collections;

/// called from an npc when a progression thing (gate, bridge, etc) is bought
public class EventProgressionBought : EventBase
{
	public void Activate()
	{
		ActivateActions(null);
	}
}
