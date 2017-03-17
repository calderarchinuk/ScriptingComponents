using UnityEngine;
using System.Collections;

public class ActionSpawnGameObject : ActionBase
{
	public GameObject instance;
	public override void Activate (GameObject entity)
	{
		Instantiate(instance,transform.position,transform.rotation);
	}

	public override void OnDrawGizmos ()
	{
		base.OnDrawGizmos ();
		Utility.GroundGizmo(transform.position);
	}
}
