using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fires when rotation reaches certain thresholds

public class EventRocked : EventBase
{
    public float LeftUpDot = 0.2f;
    bool reachedLeftUpDot;
    public float RightUpDot = 0.2f;
    bool reachedRightUpDot;
    public Rigidbody LockedRigidbody;

    private void Update()
    {
        FixedUpdate();

        if (used) { return; }

        if (Vector3.Dot(transform.up, Vector3.left) > LeftUpDot)
            reachedLeftUpDot = true;
        if (Vector3.Dot(transform.up, Vector3.right) > RightUpDot)
            reachedRightUpDot = true;

        if (reachedRightUpDot && reachedLeftUpDot)
        {
            ActivateActions(gameObject);
        }
    }

    //ROTATION
    Quaternion center;
    Quaternion last;
    public bool skiprot;
    private void FixedUpdate()
    {
        //as the dot product of left/right gets higher, raise the y position slightly
        Vector3 pos = new Vector3();
        pos.x = 0;
        pos.z = 0.549f;
        pos.y = Mathf.Clamp(LockedRigidbody.position.y, 1.297f, 1.4f);

        LockedRigidbody.transform.position = pos;

        //x = 0
        //y = 0
        //z < 0.2 && > -0.2
        //w = 1-abs(z)
        //LockedRigidbody.rotation = last;
        //last = Quaternion.AngleAxis(LockedRigidbody.rotation.eulerAngles.z, Vector3.forward);

        //LockedRigidbody.rotation = Quaternion.Slerp(LockedRigidbody.rotation, Quaternion.identity, Time.fixedDeltaTime * 0.1f);

        //LockedRigidbody.rotation *= Quaternion.AngleAxis(-LockedRigidbody.rotation.eulerAngles.x, Vector3.right);
        //LockedRigidbody.rotation *= Quaternion.AngleAxis(-LockedRigidbody.rotation.eulerAngles.y, Vector3.up);


        //return;

        

        Vector3 limit = LockedRigidbody.rotation.eulerAngles;
        limit.x = 0;
        limit.y = 0;

        if (limit.z > 180)
        {
            float reasonablez = limit.z - 360;
            if (reasonablez<-30)
            {
                limit.z = -30;
            }
        }
        else
        {
            if (limit.z > 30)
            {
                limit.z = 30;
            }
        }
        LockedRigidbody.transform.rotation = Quaternion.Euler(limit);

        return;

        float z = Mathf.Clamp(LockedRigidbody.rotation.z, -0.2f, 0.2f);

        center = new Quaternion(0, 0, z, 1);
        if (skiprot) { return; }
        LockedRigidbody.rotation = last;
        last = NormalizeQuaternion(center);

        return;
        
        if (Vector3.Dot(transform.up, Vector3.right) > RightUpDot)
        {
            if (Quaternion.Angle(center, Quaternion.FromToRotation(Vector3.up, transform.up)) > 20)
            {
                LockedRigidbody.rotation = Quaternion.AngleAxis(-19, Vector3.forward);
            }
        }
        else if (Vector3.Dot(transform.up, Vector3.left) > LeftUpDot)
        {
            if (Quaternion.Angle(center, Quaternion.FromToRotation(Vector3.up, transform.up)) > 20)
            {
                LockedRigidbody.rotation = Quaternion.AngleAxis(19, Vector3.forward);
            }
        }
        else
        {
            var qy = Quaternion.AngleAxis(0, Vector3.up);
            var qp = Quaternion.AngleAxis(0, Vector3.right);

            //LockedRigidbody.rotation *= qy * qp;
        }

        //euler.x = 0;
        ///euler.y = 0;

        //LockedRigidbody.rotation = Quaternion.Euler(euler);

    }

    //i'm sure casting doubles to floats will be fine
    static Quaternion NormalizeQuaternion(Quaternion q1)
    {
        Quaternion q2;
        float num2 = (((q1.x * q1.x) + (q1.y * q1.y)) + (q1.z * q1.z)) + (q1.w * q1.w);
        float num = 1f / ((float)Mathf.Sqrt((float)num2));
        q2.x = (float)(q1.x * num);
        q2.y = (float)(q1.y * num);
        q2.z = (float)(q1.z * num);
        q2.w = (float)(q1.w * num);
        return q2;
    }
}
