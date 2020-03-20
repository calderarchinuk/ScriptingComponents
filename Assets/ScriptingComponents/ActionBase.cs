using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ILinkable
{
	List<GameObject> GetLinks();
	//GameObject GameObject{get;}
}

public class ActionBase : MonoBehaviour, ILinkable
{
	public virtual void Activate(GameObject instigator)
	{
		//do something
	}

	///returns a list of actions activated by timelines and conditions
	public virtual List<GameObject> GetLinks()
	{
		return null;
	}

	//public GameObject GameObject{ get{return gameObject;}}

	public virtual void OnDrawGizmos()
	{
		#if UNITY_EDITOR
		if (Vector3.Dot(Camera.current.transform.forward,(transform.position - Camera.current.transform.position).normalized) > 0)
		{
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.green; 
			UnityEditor.Handles.Label(transform.position + Vector3.up*1.5f, gameObject.name,style);
		}
		#endif

		Gizmos.color = new Color(0,1.5f,0);
		Gizmos.DrawCube(transform.position,new Vector3(0.8f,0.8f,0));

		Gizmos.color = new Color(1,1,1,0.5f);
	}
}
