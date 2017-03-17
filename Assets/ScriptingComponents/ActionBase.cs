using UnityEngine;
using System.Collections;

public class ActionBase : MonoBehaviour
{
	public virtual void Activate(GameObject instigator)
	{
		//do something
	}

	public virtual void OnDrawGizmos()
	{
		#if UNITY_EDITOR
		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.green; 
		UnityEditor.Handles.Label(transform.position + Vector3.up*1.5f, gameObject.name,style);
		#endif

		Gizmos.color = new Color(0,1.5f,0);
		Gizmos.DrawCube(transform.position,new Vector3(0.8f,0.8f,0));

		Gizmos.color = new Color(1,1,1,0.5f);
	}
}
