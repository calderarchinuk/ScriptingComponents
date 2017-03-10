using UnityEngine;
using System.Collections;


public class ActionDestroy : ActionBase
{
	[SerializeField]
	private GameObject[] _destroyTargets;

	public override void Activate (GameObject entity)
	{
		for (int i = 0; i<_destroyTargets.Length;i++)
		{
			Destroy(_destroyTargets[i]);
		}
	}
}
