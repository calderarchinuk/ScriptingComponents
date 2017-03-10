using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConditionGlobalVariable : ConditionComponent
{
	[SerializeField] public string VariableName;
	[SerializeField] public VariableType ExpectedType;

	//public VariableSource VariableSource = VariableSource.Global;

	public CompareOperator compareOperator;


	//public Fungus.VariableSource VariableSource;

	[Tooltip("Boolean value to compare against")]
	//[SerializeField] protected BooleanData booleanData;
	[HideInInspector]
	public bool boolCompare;

	[Tooltip("Integer value to compare against")]
	[HideInInspector]
	//[SerializeField] protected IntegerData integerData;
	public int integerCompare;

	[Tooltip("Float value to compare against")]
	[HideInInspector]
	//[SerializeField] protected FloatData floatData;
	public float floatCompare;

	[Tooltip("String value to compare against")]
	[HideInInspector]
	//[SerializeField] protected StringDataMulti stringData;
	public string stringCompare;

	public override void Activate (GameObject instigator)
	{
		if (EvaluateCondition())
		{
			ActivatePassActions(instigator);
		}
		else
		{
			ActivateFailActions(instigator);
		}
	}

	protected virtual bool EvaluateCondition()
	{
		/*BooleanVariable booleanVariable = variable as BooleanVariable;
            IntegerVariable integerVariable = variable as IntegerVariable;
            FloatVariable floatVariable = variable as FloatVariable;
            StringVariable stringVariable = variable as StringVariable;*/

		bool condition = false;

		bool booleanData = false;
		int integerData = 0;
		float floatData = 0;
		string stringData = "";



		switch (ExpectedType)
		{
		case VariableType.Boolean:
			booleanData = FindBoolVariable();
			switch (compareOperator)
			{
			case CompareOperator.Equals:
				if (booleanData == boolCompare)condition = true;
				break;
			case CompareOperator.NotEquals:
				if (booleanData != boolCompare)condition = true;
				break;
			}
			break;
		case VariableType.Integer:
			integerData = FindIntVariable();
			switch (compareOperator)
			{
			case CompareOperator.Equals:
				if (integerData == integerCompare)condition = true;
				break;
			case CompareOperator.NotEquals:
				if (integerData != integerCompare)condition = true;
				break;
			case CompareOperator.LessThan:
				if (integerData < integerCompare)condition = true;
				break;
			case CompareOperator.GreaterThan:
				if (integerData > integerCompare)condition = true;
				break;
			case CompareOperator.LessThanOrEquals:
				if (integerData <= integerCompare)condition = true;
				break;
			case CompareOperator.GreaterThanOrEquals:
				if (integerData >= integerCompare)condition = true;
				break;
			}
			break;

		case VariableType.Float:
			floatData = FindFloatVariable();
			switch (compareOperator)
			{
			case CompareOperator.Equals:
				if (floatData == floatCompare)condition = true;
				break;
			case CompareOperator.NotEquals:
				if (floatData != floatCompare)condition = true;
				break;
			case CompareOperator.LessThan:
				if (floatData < floatCompare)condition = true;
				break;
			case CompareOperator.GreaterThan:
				if (floatData > floatCompare)condition = true;
				break;
			case CompareOperator.LessThanOrEquals:
				if (floatData <= floatCompare)condition = true;
				break;
			case CompareOperator.GreaterThanOrEquals:
				if (floatData >= floatCompare)condition = true;
				break;
			}
			break;

		case VariableType.String:
			stringData = FindStringVariable();
			switch (compareOperator)
			{
			case CompareOperator.Equals:
				if (stringData == stringCompare)condition = true;
				break;
			case CompareOperator.NotEquals:
				if (stringData != stringCompare)condition = true;
				break;
			}
			break;
		}

		/*if (booleanVariable != null)
            {
                condition = booleanVariable.Evaluate(compareOperator, booleanData.Value);
            }
            else if (integerVariable != null)
            {
                condition = integerVariable.Evaluate(compareOperator, integerData.Value);
            }
            else if (floatVariable != null)
            {
                condition = floatVariable.Evaluate(compareOperator, floatData.Value);
            }
            else if (stringVariable != null)
            {
                condition = stringVariable.Evaluate(compareOperator, stringData.Value);
            }*/

		return condition;
	}

	bool FindBoolVariable ()
	{
		return PlayerPrefs.GetInt(VariableName) == 1;
	}

	int FindIntVariable ()
	{
		return PlayerPrefs.GetInt(VariableName);
	}

	float FindFloatVariable ()
	{
		return PlayerPrefs.GetFloat(VariableName);
	}

	string FindStringVariable ()
	{
		return PlayerPrefs.GetString(VariableName);
	}
}
