using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO actions with durations
public class ActionTimeline : ActionBase
{
	[System.Serializable]
	public class CinematicAction
	{
		public ActionBase Action;
		public float Delay;
		public bool Activated = false;
	}

	public List<CinematicAction> actions = new List<CinematicAction>();

	private float _currentTime = 0;

	//figure out when the last action happens. only increase time until it's passed
	private float _endTime = 0;

	public override void Activate (GameObject entity)
	{
		foreach (var v in actions)
		{
			if (v.Delay > _endTime)
			{
				_endTime = v.Delay;
			}
		}
		_endTime += 1;
		StartCoroutine(TimeGo());
	}

	IEnumerator TimeGo()
	{
		while (_currentTime < _endTime)
		{
			foreach (var v in actions)
			{
				if (!v.Activated && _currentTime > v.Delay)
				{
					v.Activated = true;
					v.Action.Activate(null);
				}
			}
			_currentTime += Time.deltaTime;
			yield return null;
		}
	}

	public override List<GameObject> GetLinks ()
	{
		List<GameObject> linkedActions = new List<GameObject>();

		foreach(var a in actions)
		{
			if (a.Action != null)
				linkedActions.Add(a.Action.gameObject);
		}
		return linkedActions;
	}

	public override void OnDrawGizmos ()
	{
		base.OnDrawGizmos ();
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position - transform.right*5 + Vector3.up,transform.position + transform.right*5 + Vector3.up);
		for (int i = 0; i<=10; i++)
		{
			Gizmos.DrawRay(Vector3.Lerp(transform.position - transform.right*5,transform.position + transform.right*5,i/10f) + Vector3.up,Vector3.up*0.2f);
		}

		Gizmos.color = Color.green;
		foreach (var v in actions)
		{
			if (v == null){continue;}
			var actionColour = Utility.RandomColour(Utility.CurrentSeed + v.Action.GetInstanceID());
			Gizmos.color = actionColour;
			Gizmos.DrawLine(v.Action.transform.position,Vector3.Lerp(transform.position - transform.right*5,transform.position + transform.right*5,v.Delay/10f) + Vector3.up);
			Gizmos.DrawRay(Vector3.Lerp(transform.position - transform.right*5,transform.position + transform.right*5,v.Delay/10f) + Vector3.up,Vector3.up*0.2f);
		}
	}
}
