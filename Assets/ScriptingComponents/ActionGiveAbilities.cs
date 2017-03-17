using UnityEngine;
using System.Collections;

//this was useful for the tutorial
public class ActionGiveAbilities : ActionBase
{
	public string[] AbilityNames;

	public override void Activate (GameObject entity)
	{
		/*var v = entity.GetAbility(PlayerInput.Spell);

		if (v == null)
		{
			entity.AddAbility(PlayerInput.Spell,AbilityNames[Random.Range(0,AbilityNames.Length)]);
		}*/
	}
}
