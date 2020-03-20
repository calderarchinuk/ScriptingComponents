using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//TODO sort/layout windows

//"child height" when links have multiple targets
//used height = vertical height + max child height per 'row'

//organize gameobjects into dragable windows. draw lines between events/actions
//holds info about how to draw lines to linked action gameobjects
public class ActionInfo
{
	public int OffsetHeight = 0;
	public ActionBase Action;
	public List<GameObject> LinkedGameObjects = new List<GameObject>();

	public ActionInfo(int offset, ActionBase action, List<GameObject> linked)
	{
		OffsetHeight = offset;
		Action = action;
		LinkedGameObjects = linked;
	}
}

public class EventInfo
{
	public int OffsetHeight = 0;
	public EventBase Event;
	public List<GameObject> LinkedGameObjects = new List<GameObject>();
	public EventInfo(int offset, EventBase eventbase,List<GameObject> linked)
	{
		Event = eventbase;
		OffsetHeight = offset;
		LinkedGameObjects = linked;
	}
}

//holds scripting components on a gameobject
public class ScriptingComponents
{
	public List<EventInfo> events = new List<EventInfo>(); //events are linked to gameobjects specifically
	public List<ActionInfo> actions = new List<ActionInfo>();

	public int HeirarchyDepth;
	public static int StaticVerticalOffset = 0;
	public int VerticalDepth;

	public ScriptingComponents()
	{
		VerticalDepth = StaticVerticalOffset;
		StaticVerticalOffset++;
	}

	public int GetItemCount()
	{
		return events.Count + actions.Count;
	}
}

public class ScriptingEventWindow : EditorWindow
{
    [MenuItem("Window/Scripting Event Window")]
    public static void Init()
    {
        EditorWindow.GetWindow(typeof(ScriptingEventWindow), false, "Scripting Event Window");
    }

	Dictionary<GameObject, ScriptingComponents> ScriptingGameObjectDict = null;
	Dictionary<GameObject, GUIWindow> Windows;

	//layout events/actions as if in a 2d grid?

	//https://www.what-could-possibly-go-wrong.com/scene-traversal-recipes-for-unity/

	List<GameObject> GetChildLinks(List<GameObject> hierarchyLevel)
	{
		var children = new List<GameObject>();

		foreach (var parentGameObject in hierarchyLevel)
		{
			var linkable = parentGameObject.GetComponent<ILinkable>();
			if (linkable != null && linkable.GetLinks() != null)
			{
				foreach(var l in linkable.GetLinks())
				{
					children.Add(l);
				}
			}
			//foreach (Transform childTransform in parentGameObject.transform)
			//{
			//	var childGameObject = childTransform.gameObject;
			//	children.Add(childGameObject);
			//}		
		}

		return children;
	}

	void LinkBreadthFirstTraversal(List<GameObject> hierarchyLevel)
	{
		MaxDepth --;
		if (MaxDepth < 0){return;}

		foreach (var gameObject in hierarchyLevel)
		{
			// ... Do something with the game object ...
			//Debug.Log(gameObject.name);
			if (hierarchyLevelDict.ContainsKey(gameObject))
			{
				hierarchyLevelDict[gameObject] = Mathf.Max(hierarchyLevelDict[gameObject],currentLevel);
			}
			else
				hierarchyLevelDict.Add(gameObject,currentLevel);
		}
		if (hierarchyLevel.Count == 0)
		{
			Debug.Log("BREAK! reached level with nothing!");
			return;
		}

		currentLevel ++;
		LinkBreadthFirstTraversal(GetChildLinks(hierarchyLevel)); // Recursion!
	}


	int MaxDepth = 100;
	int currentLevel = 0;
	Dictionary<GameObject,int> hierarchyLevelDict = new Dictionary<GameObject, int>();

	void LinkBreadthFirstTraversal()
	{
		MaxDepth = 100;
		currentLevel = 0;
		hierarchyLevelDict = new Dictionary<GameObject, int>();
		var events = FindObjectsOfType<EventBase>();
		var gameobjects = new List<GameObject>();
		foreach(var v in events)
		{
			if (!gameobjects.Contains(v.gameObject))
				gameobjects.Add(v.gameObject);
		}
		LinkBreadthFirstTraversal(gameobjects);

		Debug.Log("max depth: " + MaxDepth);

		//LinkBreadthFirstTraversal(SceneManager.GetActiveScene().GetRootGameObjects());
	}




    void Refresh()
    {
		LinkBreadthFirstTraversal();
		foreach(var v in hierarchyLevelDict)
		{
			Debug.Log(v.Key.name + " " +v.Value);
		}



		ScriptingComponents.StaticVerticalOffset = 0;
		ScriptingGameObjectDict = new Dictionary<GameObject, ScriptingComponents>();
        var allevents = FindObjectsOfType<EventBase>();
		foreach(var v in allevents)
		{
			if (!ScriptingGameObjectDict.ContainsKey(v.gameObject))
			{
				ScriptingGameObjectDict.Add(v.gameObject,new ScriptingComponents());
			}
		}

		var allactions = FindObjectsOfType<ActionBase>();
		foreach(var v in allactions)
		{
			if (!ScriptingGameObjectDict.ContainsKey(v.gameObject))
			{
				ScriptingGameObjectDict.Add(v.gameObject,new ScriptingComponents());
			}
		}

        foreach(var v in allevents)
        {
			//append events on that gameobject
			ScriptingGameObjectDict[v.gameObject].events.Add(new EventInfo(ScriptingGameObjectDict[v.gameObject].GetItemCount()*20,v,v.actions));
			foreach(var a in v.actions)
			{
				if (ScriptingGameObjectDict.ContainsKey(a.gameObject))
				{
					//ScriptingGameObjectDict[a.gameObject].HeirarchyDepth ++;
					//ScriptingGameObjectDict[a.gameObject].HeirarchyDepth = Mathf.Max(ScriptingGameObjectDict[a.gameObject].HeirarchyDepth,ScriptingGameObjectDict[v.gameObject].HeirarchyDepth + 1);
					ScriptingGameObjectDict[a.gameObject].HeirarchyDepth = hierarchyLevelDict[a.gameObject];
				}
			}

			//get all linked components
			/*foreach(var actiongo in v.actions)
			{
				if (actiongo == null) { continue; }
				foreach(var action in actiongo.GetComponents<ActionBase>())
				{
					ScriptingGameObjectDict[v.gameObject].actions.Add(new ActionInfo(ScriptingGameObjectDict[v.gameObject].GetItemCount()*20,action,action.GetLinkedActions()));
				}
			}*/
        }

		foreach(var v in allactions)
		{
			//get all components
			foreach (var action in v.gameObject.GetComponents<ActionBase>())
			{
				if (action == null){Debug.LogWarning("action is null!"); continue;}
				if (ScriptingGameObjectDict == null){Debug.Log("null");}
				if (ScriptingGameObjectDict[v.gameObject] == null){Debug.Log("null");}
				if (ScriptingGameObjectDict[v.gameObject].actions == null){Debug.Log("null");}


				if (ScriptingGameObjectDict[v.gameObject].GetItemCount() == null){Debug.Log("null");}

				var linkedActions = action.GetLinks();
				//if (linkedActions == null){Debug.Log("null");}

				ScriptingGameObjectDict[v.gameObject].actions.Add(new ActionInfo(ScriptingGameObjectDict[v.gameObject].GetItemCount()*20,action,linkedActions));

				if (linkedActions != null)
				{
					foreach(var aa in linkedActions)
					{
						if (ScriptingGameObjectDict[aa.gameObject] == null){continue;}
						//ScriptingGameObjectDict[aa.gameObject].HeirarchyDepth = Mathf.Max(ScriptingGameObjectDict[aa.gameObject].HeirarchyDepth,ScriptingGameObjectDict[v.gameObject].HeirarchyDepth + 1);
						ScriptingGameObjectDict[aa.gameObject].HeirarchyDepth = hierarchyLevelDict[aa.gameObject];
					}
				}
				//ScriptingGameObjectDict[v.gameObject].HeirarchyDepth ++;
			}
		}

		Windows = new Dictionary<GameObject, GUIWindow>();
		foreach(var v in ScriptingGameObjectDict)
		{
			int id = Random.Range(0,1000000);
			var window = new GUIWindow();
			window.Target = v.Key;
			window.components = v.Value;
			window.Id = id;
			window.Name = v.Key.name;

			window.Rect = window.CalculateSize();
			Windows.Add(window.Target,window);
		}
    }

    void OnGUI()
    {
		if (ScriptingGameObjectDict == null)
            Refresh();

		if (GUILayout.Button("refresh"))
		{
			Refresh();
		}

		BeginWindows();
		//draw windows
		foreach(var kvp in Windows)
		{
			kvp.Value.Rect = GUILayout.Window(kvp.Value.Id,kvp.Value.Rect,kvp.Value.WindowFunction,kvp.Value.Name);
		}
		EndWindows();

		//draw lines
		foreach(var kvp in Windows)
		{
			foreach(var components in kvp.Value.components.actions)
			{
				if (components.LinkedGameObjects == null || components.LinkedGameObjects.Count == 0){continue;}
				//draw lines to linked windows

				foreach(var v in components.LinkedGameObjects)
				{
					if (!Windows.ContainsKey(v)){continue;}

					Vector2 p0 = GetLinkPoint(kvp.Value,components.OffsetHeight,true);
					Vector2 p1 = GetLinkPoint(Windows[v],0,false);

					Drawing.DrawLine(p0,p1,Color.green,2);
				}
			}
			foreach(var components in kvp.Value.components.events)
			{
				if (components.LinkedGameObjects == null || components.LinkedGameObjects.Count == 0){continue;}
				//draw lines to linked windows

				foreach(var v in components.LinkedGameObjects)
				{
					if (!Windows.ContainsKey(v)){continue;}

					Vector2 p0 = GetLinkPoint(kvp.Value,components.OffsetHeight,true);
					Vector2 p1 = GetLinkPoint(Windows[v],0,false);

					Drawing.DrawLine(p0,p1,Color.red,2);
				}
			}
		}
    }

	Vector2 GetLinkPoint(GUIWindow window, int offset, bool right)
	{
		if (right)
		{
			var x = window.Rect.x + window.Rect.width;
			var y = window.Rect.y + offset + 50;
			return new Vector2(x,y);
		}
		else
		{
			var x = window.Rect.x;
			var y = window.Rect.y + offset + 10;
			return new Vector2(x,y);
		}
	}
}

public class GUIWindow
{
	public int Id;
	public Rect Rect;
	public string Name;
	public GameObject Target;
	public ScriptingComponents components;

	public Rect CalculateSize ()
	{
		int rightness = 50 + components.HeirarchyDepth * 200;
		int downness = 50 + components.VerticalDepth * 100;
		return new Rect(rightness,downness,150,20*components.actions.Count + 20 * components.events.Count);
	}

	public void WindowFunction(int windowID)
	{
		GUI.DragWindow(new Rect(0, 0, 10000, 20));
		GUILayout.Label("hierarchy " + components.HeirarchyDepth);
		//GUILayout.BeginVertical();
		if (GUILayout.Button("select"))
		{
			Selection.activeGameObject = Target;
		}
		GUI.color = Color.red;
		foreach(var events in components.events)
		{
			GUILayout.Label(events.Event.GetType().ToString());
		}
		GUI.color = Color.green;
		foreach(var actions in components.actions)
		{
			GUILayout.Label(actions.Action.GetType().ToString());
		}
		GUI.color = Color.white;
		//GUILayout.EndVertical();
	}
}

public class Drawing
{
	//****************************************************************************************************
	//  static function DrawLine(rect : Rect) : void
	//  static function DrawLine(rect : Rect, color : Color) : void
	//  static function DrawLine(rect : Rect, width : float) : void
	//  static function DrawLine(rect : Rect, color : Color, width : float) : void
	//  static function DrawLine(Vector2 pointA, Vector2 pointB) : void
	//  static function DrawLine(Vector2 pointA, Vector2 pointB, color : Color) : void
	//  static function DrawLine(Vector2 pointA, Vector2 pointB, width : float) : void
	//  static function DrawLine(Vector2 pointA, Vector2 pointB, color : Color, width : float) : void
	//  
	//  Draws a GUI line on the screen.
	//  
	//  DrawLine makes up for the severe lack of 2D line rendering in the Unity runtime GUI system.
	//  This function works by drawing a 1x1 texture filled with a color, which is then scaled
	//   and rotated by altering the GUI matrix.  The matrix is restored afterwards.
	//****************************************************************************************************

	public static Texture2D lineTex;

	public static void DrawLine(Rect rect) { DrawLine(rect, GUI.contentColor, 1.0f); }
	public static void DrawLine(Rect rect, Color color) { DrawLine(rect, color, 1.0f); }
	public static void DrawLine(Rect rect, float width) { DrawLine(rect, GUI.contentColor, width); }
	public static void DrawLine(Rect rect, Color color, float width) { DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), color, width); }
	public static void DrawLine(Vector2 pointA, Vector2 pointB) { DrawLine(pointA, pointB, GUI.contentColor, 1.0f); }
	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color) { DrawLine(pointA, pointB, color, 1.0f); }
	public static void DrawLine(Vector2 pointA, Vector2 pointB, float width) { DrawLine(pointA, pointB, GUI.contentColor, width); }
	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
	{
		// Save the current GUI matrix, since we're going to make changes to it.
		Matrix4x4 matrix = GUI.matrix;

		// Generate a single pixel texture if it doesn't exist
		if (!lineTex) { lineTex = new Texture2D(1, 1); }

		// Store current GUI color, so we can switch it back later,
		// and set the GUI color to the color parameter
		Color savedColor = GUI.color;
		GUI.color = color;

		// Determine the angle of the line.
		float angle = Vector3.Angle(pointB - pointA, Vector2.right);

		// Vector3.Angle always returns a positive number.
		// If pointB is above pointA, then angle needs to be negative.
		if (pointA.y > pointB.y) { angle = -angle; }

		// Use ScaleAroundPivot to adjust the size of the line.
		// We could do this when we draw the texture, but by scaling it here we can use
		//  non-integer values for the width and length (such as sub 1 pixel widths).
		// Note that the pivot point is at +.5 from pointA.y, this is so that the width of the line
		//  is centered on the origin at pointA.
		GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));

		// Set the rotation for the line.
		//  The angle was calculated with pointA as the origin.
		GUIUtility.RotateAroundPivot(angle, pointA);

		// Finally, draw the actual line.
		// We're really only drawing a 1x1 texture from pointA.
		// The matrix operations done with ScaleAroundPivot and RotateAroundPivot will make this
		//  render with the proper width, length, and angle.
		GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1, 1), lineTex);

		// We're done.  Restore the GUI matrix and GUI color to whatever they were before.
		GUI.matrix = matrix;
		GUI.color = savedColor;
	}
}