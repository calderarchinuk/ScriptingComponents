using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//adds a line to quickly connect events to actions

[CanEditMultipleObjects]
[CustomEditor(typeof(EventBase),true)]
public class EventInspector : Editor
{
	void OnSceneGUI()
	{
		//consume input
		//screen gui



		if (!Event.current.shift)
		{
			Handles.BeginGUI();
			GUILayout.Box("Shift to attach <color=red>Event</color> to <color=green>Action</color>");
			Handles.EndGUI();
		}



		//return;
		var e = target as EventBase;

		if (Event.current.shift)
		{
			//z should be distance from target
			var z = Vector3.Distance(Camera.current.transform.position,e.transform.position);
			var mousePos = new Vector3(Event.current.mousePosition.x/Screen.width*2,(Event.current.mousePosition.y/Screen.height)*2,z);

			mousePos.y = 1-mousePos.y;

			Vector3 mouseWorldPos = Camera.current.ViewportToWorldPoint(mousePos);

			Vector3 worldMouseDirection = (mouseWorldPos - Camera.current.transform.position).normalized;

			//Debug.DrawRay(Camera.current.transform.position,worldMouseDirection);

			Handles.color = Color.red;
			Handles.DrawLine(e.transform.position,mouseWorldPos);

			var nearestActionVis = DotRaycastToAction(e,Camera.current.transform.position,worldMouseDirection);
			if (nearestActionVis != null)
			{
				//Vector3 pointerPosition = Camera.current.ScreenToWorldPoint(Event.current.mousePosition);
				Handles.Label(nearestActionVis.transform.position,"Space");

				Handles.color = new Color(1,1,1,0.5f);
				Handles.SphereHandleCap(0,nearestActionVis.transform.position,Quaternion.identity,1,EventType.Repaint);
				Handles.color = Color.white;
				Handles.DrawDottedLine(e.transform.position,nearestActionVis.transform.position,5);
			}

			if ((Event.current.button == 0 && Event.current.type == EventType.MouseDown) || Event.current.keyCode == KeyCode.Space && Event.current.type == EventType.KeyUp)
			{
				var nearestAction = DotRaycastToAction(e,Camera.current.transform.position,worldMouseDirection);
				if (nearestAction == null)
				{
					return;
				}
				if (e.actions.Contains(nearestAction.gameObject))
				{
					e.actions.Remove(nearestAction.gameObject);
				}
				else
				{
					e.actions.Add(nearestAction.gameObject);
				}
			}
		}
	}

	ActionBase DotRaycastToAction(EventBase selected, Vector3 position, Vector3 direction)
	{
		var allactions = FindObjectsOfType<ActionBase>();

		float nearestdot = 0;
		ActionBase nearestAction = null;

		foreach (var v in allactions)
		{
			if (v.gameObject == selected.gameObject){continue;}
			var dot = Vector3.Dot(direction,(v.transform.position - position).normalized);

			if (dot>nearestdot)
			{
				nearestdot = dot;
				nearestAction = v;
			}
		}
		return nearestAction;
	}
}
