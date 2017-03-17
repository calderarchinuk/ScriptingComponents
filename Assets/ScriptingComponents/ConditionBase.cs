using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConditionBase : ActionBase
{
	public List<GameObject> PassActions = new List<GameObject>();
	public List<GameObject> FailActions = new List<GameObject>();

	public override void Activate (GameObject instigator)
	{
		
	}

	protected void ActivatePassActions(GameObject instigator)
	{
		foreach (var v in PassActions)
		{
			foreach (var a in v.GetComponents<ActionBase>())
				a.Activate(instigator);
		}
	}

	protected void ActivateFailActions(GameObject instigator)
	{
		foreach (var v in FailActions)
		{
			foreach (var a in v.GetComponents<ActionBase>())
				a.Activate(instigator);
		}
	}

	public override void OnDrawGizmos ()
	{
		#if UNITY_EDITOR
		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.blue; 
		UnityEditor.Handles.Label(transform.position + Vector3.up*1.5f, gameObject.name,style);
		#endif

		Gizmos.color = new Color(0.5f,0.5f,1.5f);
		Gizmos.DrawCube(transform.position,new Vector3(0.8f,1.8f,0));



		Gizmos.color = Color.green;
		foreach (var v in PassActions)
		{
			if (v == null){continue;}
			Gizmos.DrawLine(transform.position+Vector3.up*0.5f,v.transform.position);
		}

		Gizmos.color = Color.red;
		foreach (var v in FailActions)
		{
			if (v == null){continue;}
			Gizmos.DrawLine(transform.position+Vector3.down*0.5f,v.transform.position);
		}
	}
}
