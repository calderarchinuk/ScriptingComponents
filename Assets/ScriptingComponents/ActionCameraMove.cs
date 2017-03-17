using UnityEngine;
using System.Collections;

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
		Quaternion startRot = TargetCamera.transform.rotation;
		while (normalTime < 1)
		{
			normalTime += Time.deltaTime / Duration;
			TargetCamera.transform.position = Vector3.Lerp(startPos,transform.position,Duration);
			TargetCamera.transform.rotation = Quaternion.Lerp(startRot,transform.rotation,Duration);
			yield return null;
		}
		TargetCamera.transform.position = transform.position;
		TargetCamera.transform.rotation = transform.rotation;
	}

	public override void OnDrawGizmos ()
	{
		base.OnDrawGizmos ();
		if (TargetCamera == null){return;}
		Gizmos.DrawLine(TargetCamera.transform.position,transform.position);
	}

	public void OnDrawGizmosSelected()
	{
		if (TargetCamera == null){return;}

		//destination
		Gizmos.color = new Color(1,1,1,0.3f);
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawFrustum(transform.position,TargetCamera.fieldOfView,Mathf.Min(TargetCamera.farClipPlane,100),Mathf.Max(1,TargetCamera.nearClipPlane),TargetCamera.aspect);

		for (int i = 0; i<4; i++)
		{
			Gizmos.color = new Color(1,1,1,0.1f);
			var matrix = Utility.MatrixLerp(TargetCamera.transform.localToWorldMatrix,transform.localToWorldMatrix,i/4f);
			Gizmos.matrix = matrix;
			Gizmos.DrawFrustum(TargetCamera.transform.position,TargetCamera.fieldOfView,Mathf.Min(TargetCamera.farClipPlane,100),Mathf.Max(1,TargetCamera.nearClipPlane),TargetCamera.aspect);
		}
	}
}
