using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//TODO add property drawer on this to draw in a reorderable 

[System.Serializable]
public class CriteriaObject
{
	public enum VariableSource
	{
		Blackboard,
		PlayerPrefs
	}

    public string VariableName;
    public VariableType ExpectedType;
    
    public CompareOperator compareOperator;
    
    public bool boolCompare;
    
    public int integerCompare;
    
    public float floatCompare;
    
    public string stringCompare;

	public static VariableSource Source;

    //TODO split up this into EvaluateBool(comparetype,source,target), EvaluateInt(comparetype,source,target), etc
    public bool EvaluateCondition()
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
                        if (booleanData == boolCompare) condition = true;
                        break;
                    case CompareOperator.NotEquals:
                        if (booleanData != boolCompare) condition = true;
                        break;
                }
                break;
            case VariableType.Integer:
                integerData = FindIntVariable();
                switch (compareOperator)
                {
                    case CompareOperator.Equals:
                        if (integerData == integerCompare) condition = true;
                        break;
                    case CompareOperator.NotEquals:
                        if (integerData != integerCompare) condition = true;
                        break;
                    case CompareOperator.LessThan:
                        if (integerData < integerCompare) condition = true;
                        break;
                    case CompareOperator.GreaterThan:
                        if (integerData > integerCompare) condition = true;
                        break;
                    case CompareOperator.LessThanOrEquals:
                        if (integerData <= integerCompare) condition = true;
                        break;
                    case CompareOperator.GreaterThanOrEquals:
                        if (integerData >= integerCompare) condition = true;
                        break;
                }
                break;

            case VariableType.Float:
                floatData = FindFloatVariable();
                switch (compareOperator)
                {
                    case CompareOperator.Equals:
                        if (floatData == floatCompare) condition = true;
                        break;
                    case CompareOperator.NotEquals:
                        if (floatData != floatCompare) condition = true;
                        break;
                    case CompareOperator.LessThan:
                        if (floatData < floatCompare) condition = true;
                        break;
                    case CompareOperator.GreaterThan:
                        if (floatData > floatCompare) condition = true;
                        break;
                    case CompareOperator.LessThanOrEquals:
                        if (floatData <= floatCompare) condition = true;
                        break;
                    case CompareOperator.GreaterThanOrEquals:
                        if (floatData >= floatCompare) condition = true;
                        break;
                }
                break;

            case VariableType.String:
                stringData = FindStringVariable();
                switch (compareOperator)
                {
                    case CompareOperator.Equals:
                        if (stringData == stringCompare) condition = true;
                        break;
                    case CompareOperator.NotEquals:
                        if (stringData != stringCompare) condition = true;
                        break;
                }
                break;
        }

        return condition;
    }

    //used OnValidate to check that compare operators are fine
    public bool ValidComparisons()
    {
        if (ExpectedType == VariableType.String || ExpectedType == VariableType.Boolean)
        {
            if (compareOperator == CompareOperator.Equals || compareOperator == CompareOperator.NotEquals)
            {
                return true;
            }
            return false;
        }
        return true;
    }

    bool FindBoolVariable()
    {
		if (Source == VariableSource.Blackboard)
        	return Blackboard.Instance.GetData<bool>(VariableName);
		else 
			return PlayerPrefs.GetInt(VariableName) == 1;
    }

    int FindIntVariable()
    {
		if (Source == VariableSource.Blackboard)
        	return Blackboard.Instance.GetData<int>(VariableName);
		else 
			return PlayerPrefs.GetInt(VariableName);
    }

    float FindFloatVariable()
    {
		if (Source == VariableSource.Blackboard)
			return Blackboard.Instance.GetData<float>(VariableName);
		else 
			return PlayerPrefs.GetFloat(VariableName);
    }

    string FindStringVariable()
    {
		if (Source == VariableSource.Blackboard)
			return Blackboard.Instance.GetData<string>(VariableName);
		else 
			return PlayerPrefs.GetString(VariableName);
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(CriteriaObject))]
class CriteriaObjectDrawer : PropertyDrawer
{

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        Rect typeRect = new Rect(position.x, position.y, 60, position.height);
        Rect varNameRect = new Rect(position.x + 65, position.y, 100, position.height);
        Rect compareRect = new Rect(position.x + 170, position.y, 50, position.height);
        Rect valueRect = new Rect(position.x + 235, position.y, position.width - 235, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(varNameRect, property.FindPropertyRelative("VariableName"), GUIContent.none);
        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("ExpectedType"), GUIContent.none);
        EditorGUI.PropertyField(compareRect, property.FindPropertyRelative("compareOperator"), GUIContent.none);

        var vt = (VariableType)property.FindPropertyRelative("ExpectedType").enumValueIndex;
        if (vt == VariableType.Boolean)
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("boolCompare"), GUIContent.none);
        if (vt == VariableType.String)
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("stringCompare"), GUIContent.none);
        if (vt == VariableType.Integer)
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("integerCompare"), GUIContent.none);
        if (vt == VariableType.Float)
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("floatCompare"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    List<GUIContent> GetOperators(VariableType expectedType)
    {
        List<GUIContent> operatorList = new List<GUIContent>();
        operatorList.Add(new GUIContent("=="));
        operatorList.Add(new GUIContent("!="));

        if (expectedType == VariableType.Integer ||
            expectedType == VariableType.Float)
        {
            operatorList.Add(new GUIContent("<"));
            operatorList.Add(new GUIContent(">"));
            operatorList.Add(new GUIContent("<="));
            operatorList.Add(new GUIContent(">="));
        }

        return operatorList;
    }
}


#endif