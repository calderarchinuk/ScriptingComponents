using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConditionCriteria : ConditionBase
{
    public enum CriteriaCheckType
    {
        And,
        Or
    }

    public CriteriaCheckType CheckType;

    public List<CriteriaObject> AllCriteria = new List<CriteriaObject>();

    public override void Activate (GameObject instigator)
	{
        if (CheckType == CriteriaCheckType.And)
        {
            foreach (var v in AllCriteria)
            {
                if (v.EvaluateCondition() == false)
                {
                    ActivateFailActions(instigator);
                    return;
                }
            }
            ActivatePassActions(instigator);
        }
        else if (CheckType == CriteriaCheckType.Or)
        {
            foreach (var v in AllCriteria)
            {
                if (v.EvaluateCondition() == true)
                {
                    ActivatePassActions(instigator);
                    return;
                }
            }
            ActivateFailActions(instigator);
        }
	}

#if UNITY_EDITOR
    void Update()
    {
        foreach (var v in AllCriteria)
        {
            if (!v.ValidComparisons())
            {
                Debug.LogError("EventBlackboardChanged has invalid comparison operators!", this);
            }
        }
    }
#endif

    void OnValidate()
    {
        foreach (var v in AllCriteria)
        {
            if (!v.ValidComparisons())
            {
                Debug.LogError("EventBlackboardChanged has invalid comparison operators!", this);
            }
        }
    }
}