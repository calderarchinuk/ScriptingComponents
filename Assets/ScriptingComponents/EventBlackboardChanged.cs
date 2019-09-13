using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EventBlackboardChanged : EventBase
{

    public List<CriteriaObject> AllCriteria = new List<CriteriaObject>();

    private void Start()
    {
        Blackboard.OnChangeBlackboard += Blackboard_OnChangeBlackboard;
    }

    private void OnDestroy()
    {
        Blackboard.OnChangeBlackboard -= Blackboard_OnChangeBlackboard;
    }

    private void Blackboard_OnChangeBlackboard()
    {
        foreach(var v in AllCriteria)
        {
            if (v.EvaluateCondition() == false)
            {
                return;
            }
        }

        //compare stuff
        ActivateActions(null);
    }

    private void OnValidate()
    {
        foreach(var v in AllCriteria)
        {
            if (!v.ValidComparisons())
            {
                Debug.LogError("EventBlackboardChanged has invalid comparison operators!", this);
            }
        }
    }
}