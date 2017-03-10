using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionSetGlobalVariable : ActionBase
{
	//public SerializedVariable serializedVariable;
	public string VariableName;
	public string VariableValue;

	public override void Activate (GameObject instigator)
	{
		PlayerPrefs.SetString(VariableName,VariableValue);
	}

	public override void OnDrawGizmos ()
	{
		#if UNITY_EDITOR
		UnityEditor.Handles.Label(transform.position + Vector3.up*2, VariableName + " " + VariableValue);
		#endif

		base.OnDrawGizmos ();
	}
}
