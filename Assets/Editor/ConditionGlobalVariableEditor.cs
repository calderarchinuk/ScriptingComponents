using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//TODO put target.value, comparison and compare value all on one line
[CustomEditor(typeof(ConditionGlobalVariable))]
public class ConditionGlobalVariableEditor : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();

		var t = target as ConditionGlobalVariable;

		if (t.ExpectedType == VariableType.Boolean)
		{
			t.boolCompare = EditorGUILayout.Toggle("Bool Compare",t.boolCompare);
		}
		if (t.ExpectedType == VariableType.Float)
		{
			t.floatCompare = EditorGUILayout.FloatField("Float Compare",t.floatCompare);
		}
		if (t.ExpectedType == VariableType.Integer)
		{
			t.integerCompare = EditorGUILayout.IntField("Int Compare",t.integerCompare);
		}
		if (t.ExpectedType == VariableType.String)
		{
			t.stringCompare = EditorGUILayout.TextField("String Compare",t.stringCompare);
		}
	}
}
