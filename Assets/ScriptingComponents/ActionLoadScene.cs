using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionLoadScene : ActionBase
{
	public string scenename = "overworld";
	public override void Activate (GameObject instigator)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(scenename);
	}
}
