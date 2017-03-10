using UnityEngine;
using System.Collections;

//TODO this should include rotation
public class ActionCameraCut : ActionBase
{
	public Camera targetCamera;

	public override void Activate (GameObject entity)
	{
		foreach(var cam in FindObjectsOfType<Camera>())
		{
			cam.enabled = false;
		}
		targetCamera.enabled = true;
	}
}
