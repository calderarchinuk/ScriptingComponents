using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConditionVariable))]
public class ConditionVariableEditor : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();

		SerializedProperty compareOperatorProp = serializedObject.FindProperty("compareOperator");

		var t = target as ConditionVariable;

		t.ExpectedType = (VariableType)EditorGUILayout.EnumPopup("Expected Type",t.ExpectedType);

		EditorGUILayout.BeginHorizontal();
		t.VariableName = EditorGUILayout.TextField(t.VariableName);
		//t.compareOperator = (CompareOperator)EditorGUILayout.EnumPopup(t.compareOperator);

		compareOperatorProp.enumValueIndex = EditorGUILayout.Popup(
			compareOperatorProp.enumValueIndex, 
			GetOperators(t.ExpectedType).ToArray());


		if (t.ExpectedType == VariableType.Boolean)
		{
			t.boolCompare = EditorGUILayout.Toggle(t.boolCompare);
		}
		if (t.ExpectedType == VariableType.Float)
		{
			t.floatCompare = EditorGUILayout.FloatField(t.floatCompare);
		}
		if (t.ExpectedType == VariableType.Integer)
		{
			t.integerCompare = EditorGUILayout.IntField(t.integerCompare);
		}
		if (t.ExpectedType == VariableType.String)
		{
			t.stringCompare = EditorGUILayout.TextField(t.stringCompare);
		}

		EditorGUILayout.EndHorizontal();
		serializedObject.ApplyModifiedProperties();
	}

	List<GUIContent> GetOperators(VariableType expectedType)
	{
		List<GUIContent> operatorList = new List<GUIContent>();
		operatorList.Add(new GUIContent("=="));
		operatorList.Add(new GUIContent("!="));

		if (expectedType == VariableType.Integer ||
			expectedType == VariableType.Float )
		{
			operatorList.Add(new GUIContent("<"));
			operatorList.Add(new GUIContent(">"));
			operatorList.Add(new GUIContent("<="));
			operatorList.Add(new GUIContent(">="));
		}

		return operatorList;
	}
}
