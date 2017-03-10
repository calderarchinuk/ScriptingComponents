using UnityEngine;
using System.Collections;
using System;

public class ConditionProgression : ActionBase
{
	public string genericProgressionCheck;
	//public Progression RequiredProgression = Progression.Gold;
	//public SpellType SpellProgression = SpellType.None;
	public int ItemProgression = -1;
	//public SessionProgression SessionProgression = SessionProgression.None;

	public GameObject[] passActions;
	public GameObject[] failActions; //if the condition fails, do this. otherwise, do the linked actions




	[ContextMenu("clear all progression")]
	public void ClearAllPogression()
	{
		PlayerPrefs.DeleteAll();
	}

	public override void Activate (GameObject entity)
	{
		bool passed = false;
		if (!string.IsNullOrEmpty(genericProgressionCheck))
		{
			passed = PlayerPrefs.HasKey(genericProgressionCheck);
		}


		if (passed)
		{
			foreach (GameObject go in passActions)
			{
				foreach (ActionBase ac in go.GetComponents<ActionBase>())
					ac.Activate(null);
			}
		}
		else
		{
			foreach (GameObject go in failActions)
			{
				foreach (ActionBase ac in go.GetComponents<ActionBase>())
					ac.Activate(null);
			}
		}
	}
}