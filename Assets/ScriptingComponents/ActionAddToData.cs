using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ActionAddToData : ActionBase
{
    public string VariableName;

    public VariableType VariableType;
    public float FloatValue;
    public int IntValue;
    public bool BoolValue;
    public string StringValue;

    public bool AddMathValue = false;

    public override void Activate(GameObject instigator)
    {
        switch (VariableType)
        {
            case VariableType.Boolean:
                Blackboard.Instance.AppendData(VariableName, BoolValue);
                break;
            case VariableType.String:
                Blackboard.Instance.AppendData(VariableName, StringValue);
                break;
            case VariableType.Integer:
                if (AddMathValue)
                {
                    int i = Blackboard.Instance.GetData<int>(VariableName);
                    i += IntValue;
                    Blackboard.Instance.AppendData(VariableName, i);
                }
                else
                    Blackboard.Instance.AppendData(VariableName, IntValue);
                break;
            case VariableType.Float:
                if (AddMathValue)
                {
                    float f = Blackboard.Instance.GetData<float>(VariableName);
                    f += FloatValue;
                    Blackboard.Instance.AppendData(VariableName, f);
                }
                else
                    Blackboard.Instance.AppendData(VariableName, FloatValue);
                break;
        }

        base.Activate(instigator);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(ActionAddToData))]
public class AddToDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ActionAddToData a = (ActionAddToData)target;

        a.VariableName = EditorGUILayout.TextField("Variable Name",a.VariableName);

        a.VariableType = (VariableType)EditorGUILayout.EnumPopup("Variable Type", a.VariableType);

        if (a.VariableType == VariableType.Boolean)
        {
            a.BoolValue = EditorGUILayout.Toggle("Bool Value", a.BoolValue);
        }
        if (a.VariableType == VariableType.String)
        {
            a.StringValue = EditorGUILayout.TextField("String Value", a.StringValue);
        }
        if (a.VariableType == VariableType.Integer)
        {
            a.IntValue = EditorGUILayout.IntField("Integer Value", a.IntValue);
            a.AddMathValue = EditorGUILayout.Toggle("Add Value", a.AddMathValue);
        }
        if (a.VariableType == VariableType.Float)
        {
            a.FloatValue = EditorGUILayout.FloatField("Float Value", a.FloatValue);
            a.AddMathValue = EditorGUILayout.Toggle("Add Value", a.AddMathValue);
        }

        if (GUI.changed && !Application.isPlaying)
        {
            EditorUtility.SetDirty(a);
            //Undo.RecordObject(a, "Changed Blackboard Data");
            UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
        }
    }
}


#endif