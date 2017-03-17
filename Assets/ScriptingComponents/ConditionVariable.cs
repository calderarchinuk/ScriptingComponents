using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//TODO this should be expandable to check a global/local blackboard of data, not just PlayerPrefs
public class ConditionVariable : ConditionBase
{
	[HideInInspector] public string VariableName;
	[HideInInspector] public VariableType ExpectedType;

	[HideInInspector] public CompareOperator compareOperator;


	[HideInInspector]
	public bool boolCompare;

	[HideInInspector]
	public int integerCompare;

	[HideInInspector]
	public float floatCompare;

	[HideInInspector]
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

	//TODO split up this into EvaluateBool(comparetype,source,target), EvaluateInt(comparetype,source,target), etc
	protected virtual bool EvaluateCondition()
	{
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
