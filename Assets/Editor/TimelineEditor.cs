using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

//At the moment, this is just a visualizer
//this system is only for delaying actions - this is not meant to be a full cinematic editor!
//10 seconds is the maximum

//TODO make keys draggable on the timeline
//TODO make this a reorderable list

[CustomEditor(typeof(ActionTimeline))]
public class TimelineEditor : Editor
{
	ActionTimeline _timeline;
	//int _selectionSeed; //so colours of actions don't change every update
	Rect _timelineRect;
	//bool _hasCinematicEnd = false;
	//bool _hasCinematicFade = false;

	Color barColour = new Color(0.7f,0.7f,0.7f);

	public enum CinematicActionTypes
	{
		Fade,
		CinematicEnd,
		CinematicPower,
		CameraMove,
		CameraCut,
	}

	public override void OnInspectorGUI ()
	{
		_timeline = (ActionTimeline)target;
		if (Utility.CurrentSeed == 0)
		{
			Utility.CurrentSeed = System.DateTime.Now.Minute + System.DateTime.Now.Millisecond;
		}

		//draw across the top of the inspector to get the current width
		GUILayout.BeginHorizontal();
		GUILayout.Label("");
		Rect _headerRect = GUILayoutUtility.GetLastRect();
		if (GUI.Button(new Rect (_headerRect.width-50,_headerRect.y,50,_headerRect.height),"Refresh")){Utility.CurrentSeed = 0;}
		GUILayout.EndHorizontal();

		//removes any actions that were set to null, usually through deletion
		_timeline.actions.RemoveAll(delegate(ActionTimeline.CinematicAction obj) {return obj.Action == null;});


		GUILayout.Space(210);

		//draw timeline box
		float offset = 5;
		_timelineRect = new Rect(offset,_headerRect.height + _headerRect.y + offset,_headerRect.width,200);
		GUI.Box(_timelineRect,"");

		//vertical lines
		for (int i = 1; i<10; i++)
		{
			EditorGUI.DrawRect(new Rect(_timelineRect.width/10 * i + offset,_timelineRect.y,1,_timelineRect.height),barColour);
		}

		Color actionColour;
		for (int i = 0; i< _timeline.actions.Count; i++)
		{
			actionColour = Utility.RandomColour(Utility.CurrentSeed + _timeline.actions[i].Action.GetInstanceID());

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("-",GUILayout.Width(30)))
			{
				RemoveAction(_timeline.actions[i]);
				continue;
			}
			GUI.color = actionColour;
			GUILayout.Box("",GUILayout.Width(20));
			GUI.color = Color.white;

			_timeline.actions[i].Action.name = GUILayout.TextField(_timeline.actions[i].Action.name,GUILayout.Width(100));
			EditorGUILayout.ObjectField("",_timeline.actions[i].Action,_timeline.actions[i].Action.GetType(),true,GUILayout.Width(50));

			_timeline.actions[i].Delay = EditorGUILayout.Slider(_timeline.actions[i].Delay,0,10);//,GUILayout.Width(200));
			DrawKeyOnTimeline(i, _timeline.actions[i], actionColour);
			GUILayout.EndHorizontal();


			//if (_timeline.actions[i].Action.GetType() == typeof(ActionSetToGameplay)){_hasCinematicEnd = true;}
			//if (_timeline.actions[i].Action.GetType() == typeof(ActionFadeBlackMask)){_hasCinematicFade = true;}
		}

		/*if (!_hasCinematicEnd || !_hasCinematicFade)
		{
			if (!_hasCinematicEnd)AddErrorMessage("Need Cinematic End");
			if (!_hasCinematicFade)AddErrorMessage("Need Cinematic Fade");
			DisplayErrorBox();
		}*/
		GUILayout.Space(20);

		//TODO common cinematic actions
		/*GUILayout.Label("Add Cinematic Action");
		GUILayout.BeginHorizontal();
			if (GUILayout.Button("Fade to Black")){NewAction(CinematicActionTypes.Fade, 0, true);}
			if (GUILayout.Button("Fade to Game")){NewAction(CinematicActionTypes.Fade, 0, false);}
			if (GUILayout.Button("End")){NewAction(CinematicActionTypes.CinematicEnd);}
			if (GUILayout.Button("Cinematic Camera On")){NewAction(CinematicActionTypes.CinematicPower,0,true);}
			if (GUILayout.Button("Cinematic Camera Off")){NewAction(CinematicActionTypes.CinematicPower,0,false);}
		GUILayout.EndHorizontal();*/

		GUILayout.Label("Add Camera Action");
		GUILayout.BeginHorizontal();
			if (GUILayout.Button("Cam Move")){NewAction(CinematicActionTypes.CameraMove);}
			if (GUILayout.Button("Cam Cut")){NewAction(CinematicActionTypes.CameraCut);}
		GUILayout.EndHorizontal();

		GUILayout.Space(20);

		GUILayout.Label("Add Other Action");
		Object newAction = EditorGUILayout.ObjectField(null,typeof(GameObject),true);
		if (newAction != null)
		{
			NewAction((GameObject)newAction);
		}
	}

	void NewAction(GameObject newAction)
	{
		foreach (var v in newAction.GetComponents<ActionBase>())
		{
			_timeline.actions.Add(NewCinematicAction(v));
		}
	}

	void NewAction(CinematicActionTypes newType, float value1 = 0, bool value2 = false)
	{
		GameObject go = new GameObject(newType.ToString());
		go.transform.SetParent(_timeline.transform);
		go.transform.localPosition = Vector3.zero;

		ActionBase newAC = null;
		switch (newType)
		{
		/*case CinematicActionTypes.Fade:
			newAC = go.AddComponent<ActionFadeBlackMask>();
			((ActionFadeBlackMask)newAC).FadeToCoverScreen = value2;
			break;
		case CinematicActionTypes.CinematicEnd:
			//newAC = go.AddComponent<ActionSetToGameplay>();
			newAC = go.AddComponent<ActionCinematicEnd>();
			break;
		case CinematicActionTypes.CinematicPower:
			newAC = go.AddComponent<ActionCinematicPower>();
			((ActionCinematicPower)newAC).Enable = value2;
			break;*/
		case CinematicActionTypes.CameraMove:
			newAC = go.AddComponent<ActionCameraMove>();
			go.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
			go.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
			break;
		case CinematicActionTypes.CameraCut:
			newAC = go.AddComponent<ActionCameraCut>();
			go.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
			go.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
			break;
		}

		if (newAC != null)
		{
			_timeline.actions.Add(NewCinematicAction(newAC));
		}
	}

	ActionTimeline.CinematicAction NewCinematicAction(ActionBase action)
	{
		return new ActionTimeline.CinematicAction(){Action = action};
	}

	void RemoveAction(ActionTimeline.CinematicAction removeAction)
	{
		_timeline.actions.Remove(removeAction);
		removeAction.Action.gameObject.name = "UNUSED_" + removeAction.Action.gameObject.name;
	}

	void DrawKeyOnTimeline(int verticalPosition, ActionTimeline.CinematicAction action, Color keyColour)
	{
		float pixelPerSecond = _timelineRect.width / 10;
		GUI.color = keyColour;
		Rect timelineBoxRect = new Rect(_timelineRect.x + action.Delay * pixelPerSecond,_timelineRect.y + verticalPosition * 20,20,20);
		GUI.Box(timelineBoxRect,new GUIContent("",action.Action.name +"\n"+action.Action.GetType().ToString()));

		Event e = Event.current;
		{
			if (Utility.MousePositionOverRect(e.mousePosition,timelineBoxRect))
			{
				action.Delay += e.delta.x / pixelPerSecond;
				if (e.alt)
				{
					action.Delay = Mathf.RoundToInt(action.Delay);
				}
			}
		}

		GUI.color = Color.white;
	}

	void AddErrorMessage(string s)
	{
		errors.Add(s);
	}

	List<string>errors = new List<string>();
	void DisplayErrorBox()
	{
		string allErrors = "";
		foreach (string s in errors)
		{
			allErrors += s + "\n";
		}
		if (allErrors == ""){return;}
		allErrors = allErrors.Substring(0,allErrors.Length-1);
		EditorGUILayout.HelpBox(allErrors,MessageType.Error);
		errors.Clear ();
	}
}
