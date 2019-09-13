using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//uses gameobject variable or override if one is set
//

public class ActionLockDynamic : ActionBase
{
    public Transform IdealTransform;
    public bool DestroyRigidbody = false;
    public GameObject InstigatorOverride;

    public override void Activate(GameObject instigator)
    {
        if (InstigatorOverride != null)
            instigator = InstigatorOverride;
        instigator.GetComponent<Rigidbody>().isKinematic = true;

        StartCoroutine(SetIdealPosition(instigator));
    }

    IEnumerator SetIdealPosition(GameObject instigator)
    {
        if (DestroyRigidbody) { Destroy(instigator.GetComponent<Rigidbody>()); }
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 0.5f;
            instigator.transform.position = Vector3.Lerp(instigator.transform.position, IdealTransform.position, t);
            instigator.transform.rotation = Quaternion.Lerp(instigator.transform.rotation, IdealTransform.rotation, t);
            yield return null;
        }
        if (!DestroyRigidbody)
            instigator.GetComponent<Rigidbody>().isKinematic = true;
    }
}
