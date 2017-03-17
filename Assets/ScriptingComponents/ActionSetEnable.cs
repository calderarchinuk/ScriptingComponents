using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionSetEnable : ActionBase
{
	public GameObject[] _enableTargets;
	public bool enable = true;

	public override void Activate (GameObject instigator)
	{
		for (int i = 0; i<_enableTargets.Length;i++)
		{
			if (_enableTargets[i] != null)
				_enableTargets[i].SetActive(enable);
		}
	}

	//TODO this should use a property drawer to tie a button to this function
	[ContextMenu("set children")]
	void AddChildren()
	{
		List<GameObject>append = new List<GameObject>();;
		for (int i = 0; i< transform.childCount; i++)
		{
			append.Add(transform.GetChild(i).gameObject);
		}
		_enableTargets = append.ToArray();
	}

	public override void OnDrawGizmos ()
	{
		base.OnDrawGizmos ();
		foreach(var v in _enableTargets)
		{
			if (v != null)
			{
				Debug.DrawLine(transform.position,v.transform.position);
			}
		}
	}
}
