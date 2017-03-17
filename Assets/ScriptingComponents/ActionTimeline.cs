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
}
