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
		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position,new Vector3(0.8f,0.8f,0));

		#if UNITY_EDITOR
		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.green; 
		UnityEditor.Handles.Label(transform.position + Vector3.up, gameObject.name,style);
		#endif
	}
}
