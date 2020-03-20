using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//event oninitialize
//if generation
//post generation
//post cinematic finished

//else, immediately call post generation event
//post cinematic finished


public class EventBase : MonoBehaviour, ILinkable
{
	public List<GameObject> actions = new List<GameObject>();
	public bool OneTimeOnly = false;
	protected bool used = false;

	public virtual List<GameObject> GetLinks()
	{
		return actions;
	}
	//public GameObject GameObject{ get{return gameObject;}}

	protected void ActivateActions(GameObject instigator)
	{
		if (used){return;}

		foreach (var v in GetComponents<ActionBase>())
		{
			v.Activate(instigator);
		}
		foreach (var v in actions)
		{
			foreach (var a in v.GetComponents<ActionBase>())
				a.Activate(instigator);
		}
		if (OneTimeOnly)
		{
			used = true;
		}
	}

	public virtual void OnDrawGizmos()
	{
		#if UNITY_EDITOR
		if (Vector3.Dot(Camera.current.transform.forward,(transform.position - Camera.current.transform.position).normalized) > 0)
		{
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.red; 
			UnityEditor.Handles.Label(transform.position + Vector3.up*1.5f, gameObject.name,style);
		}
		#endif

		Gizmos.color = new Color(1.5f,0,0);
		Gizmos.DrawCube(transform.position,new Vector3(1,1,0));
		Gizmos.color = Color.green;
		foreach (var v in actions)
		{
			if (v == null){continue;}
			Gizmos.DrawLine(transform.position,v.transform.position);
		}
	}
}
