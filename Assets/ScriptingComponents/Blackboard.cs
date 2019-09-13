using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Blackboard : MonoBehaviour {

    public Dictionary<string, object> Data = new Dictionary<string, object>();

    public delegate void onChangeBlackboard();
    /// <summary>
    /// Update. Called through Manager's update function for easy enabling/disabling
    /// </summary>
    public static event onChangeBlackboard OnChangeBlackboard;
    public void BlackboardChangedEvent() { if (OnChangeBlackboard != null) { OnChangeBlackboard(); } }

    public static Blackboard Instance;
    void OnEnable()
    {
        Instance = this;
    }

    public void AppendData(string name, object data)
    {
        if (Data.ContainsKey(name))
        {
            Data[name] = data;
        }
        else
        {
            Data.Add(name, data);
        }
        BlackboardChangedEvent();
    }

    public T GetData<T>(string name)
    {
        if (Data.ContainsKey(name))
        {
            return (T)Data[name];
        }
        return default(T);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(Blackboard))]
public class BlackboardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(15);

        Dictionary<string,object> D = ((Blackboard)target).Data;

        EditorGUI.BeginDisabledGroup(true);
        foreach(var kvp in D)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(kvp.Key);

            GUILayout.Label(GetDisplayValue(kvp.Value));

            GUILayout.EndHorizontal();
        }
        EditorGUI.EndDisabledGroup();

        Repaint();
    }

    string GetDisplayValue(object obj)
    {
        if (obj.GetType() == typeof(int))
        {
            var i = (int)obj;
            return i.ToString();
        }
        if (obj.GetType() == typeof(float))
        {
            var f = (float)obj;
            return f.ToString();
        }
        if (obj.GetType() == typeof(bool))
        {
            var b = (bool)obj;
            return b.ToString();
        }
        if (obj.GetType() == typeof(string))
        {
            var s = (string)obj;
            return s.ToString();
        }

        return "unknown";
    }
}


#endif