using UnityEngine;
using System.Collections;

//TODO this should include rotation
//TODO this is a quick hack to remove references to GameCamera. camera movement should be evaluated along a 0-1 curve
//linear movement is boring
public class ActionCameraMove : ActionBase
{
	public float Duration = 2;
	public Camera TargetCamera;

	public override void Activate (GameObject entity)
	{
		StartCoroutine(SlowMove());
	}

	IEnumerator SlowMove()
	{
		float normalTime = 0;
		Vector3 startPos = TargetCamera.transform.position;
		while (normalTime < 1)
		{
			normalTime += Time.deltaTime / Duration;
			TargetCamera.transform.position = Vector3.Lerp(startPos,transform.position,Duration);
			yield return null;
		}
		TargetCamera.transform.position = transform.position;
	}

	public override void OnDrawGizmos ()
	{
		base.OnDrawGizmos ();
		if (TargetCamera == null){return;}
		Gizmos.DrawLine(TargetCamera.transform.position,transform.position);
	}
}
