using UnityEngine;
using System.Collections;

//in overworld, used to call path.scan after a gate or something has been opened
public class ActionScanNav : ActionBase
{
	static float nextScanTime = 0;
	public override void Activate (GameObject entity)
	{
		if (Time.time < nextScanTime){return;}
		nextScanTime = Time.time + 1f;
		StartCoroutine(DelayScan());
	}
	IEnumerator DelayScan()
	{
		yield return new WaitForSeconds(0.5f);
		//AstarPath.active.Scan();
		Debug.Log("ActionScan SCAN");
	}
}
