using UnityEngine;
using System.Collections;


//scene manager calls initialize on all events immediately after loading the scene
public class EventOnStart : EventBase
{
	public void Start()
	{
		ActivateActions(null);
	}
}