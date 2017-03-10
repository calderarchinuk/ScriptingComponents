using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//event oninitialize
//if generation
//post generation
//post cinematic finished

//else, immediately call post generation event
//post cinematic finished


public class EventBase : MonoBehaviour
{
	public List<GameObject>actions = new List<GameObject>();
	public bool InitializeOnEnable = true;

	void OnEnable()
	{
		if (InitializeOnEnable){Initialize();}
	}

	public virtual void Initialize()
	{
		//use this instead of start. called from begin scene load before generation
	}

	protected void ActivateActions(GameObject instigator)
	{
		foreach (var v in GetComponents<ActionBase>())
		{
			v.Activate(instigator);
		}
		foreach (var v in actions)
		{
			foreach (var a in v.GetComponents<ActionBase>())
				a.Activate(instigator);
		}
	}

	public virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(transform.position,new Vector3(1,1,0));
		Gizmos.color = Color.green;
		foreach (var v in actions)
		{
			if (v == null){continue;}
			Gizmos.DrawLine(transform.position,v.transform.position);
		}
		#if UNITY_EDITOR
		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.red; 
		UnityEditor.Handles.Label(transform.position + Vector3.up, gameObject.name,style);
		#endif
	}
}
