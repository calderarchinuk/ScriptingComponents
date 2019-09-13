﻿using UnityEngine;
using System.Collections;

public class ActionPlaySound : ActionBase
{
	//prefab
	public GameObject SoundEffect;

	public override void Activate (GameObject entity)
	{
		Instantiate(SoundEffect,transform.position,transform.rotation);
	}
}
