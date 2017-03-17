using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ActionPathTo))]
public class PathToEditor : Editor
{
	void OnSceneGUI()
	{
		var t = target as ActionPathTo;

		t.Destination = Handles.PositionHandle(t.Destination,Quaternion.identity);
		//TODO mark dirty or whatever
	}
}
