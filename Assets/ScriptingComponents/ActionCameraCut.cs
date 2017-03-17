using UnityEngine;
using System.Collections;

public class ActionCameraCut : ActionBase
{
	public Camera TargetCamera;

	public override void Activate (GameObject entity)
	{
		foreach(var cam in FindObjectsOfType<Camera>())
		{
			cam.enabled = false;
		}
		TargetCamera.enabled = true;
	}

	public override void OnDrawGizmos ()
	{
		base.OnDrawGizmos ();

		if (TargetCamera == null){return;}

		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawFrustum(transform.position,TargetCamera.fieldOfView,Mathf.Min(TargetCamera.farClipPlane,100),Mathf.Max(1,TargetCamera.nearClipPlane),TargetCamera.aspect);
	}
}
