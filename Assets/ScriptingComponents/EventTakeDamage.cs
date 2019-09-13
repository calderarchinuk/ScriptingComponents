using UnityEngine;
using System.Collections;

//called from some player/enemy/entity/whatever
public class EventTakeDamage : EventBase
{
	public int MinimumAmount;

	public void OnTakeDamage(GameObject source, int amount)
	{
		if (amount < MinimumAmount){return;}

		ActivateActions(source);
	}
}
