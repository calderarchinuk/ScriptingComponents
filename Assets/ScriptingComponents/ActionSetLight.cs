using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionSetLight : ActionBase
{
	public Light[] TargetLights;
	public Color TargetColor;
	public float Duration = 2;

	public override void Activate (GameObject entity)
	{
		StartCoroutine(SlowMove());
	}

	IEnumerator SlowMove()
	{
		float normalTime = 0;

		Dictionary<Light,Color> lightcolors = new Dictionary<Light, Color>();
		foreach (var v in TargetLights)
		{
			lightcolors.Add(v,v.color);
		}

		while (normalTime < 1)
		{
			normalTime += Time.deltaTime / Duration;

			for (int i = 0; i<TargetLights.Length;i++)
			{
				TargetLights[i].color = Color.Lerp(lightcolors[TargetLights[i]],TargetColor,Duration);
			}
			yield return null;
		}

		for (int i = 0; i<TargetLights.Length;i++)
		{
			TargetLights[i].color = TargetColor;
		}
	}
}
