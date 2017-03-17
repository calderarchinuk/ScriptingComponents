using UnityEngine;
using System.Collections;

//could tie into other event systems, like a string listener/messenger system
//https://unity3d.com/learn/tutorials/topics/scripting/events-creating-simple-messaging-system
public class EventOnListener : EventBase
{
	public string EventName = "YourEvent";

	public void Start()
	{
		//EventManager.StartListening (EventName, Listen);
	}

	void Listen()
	{
		ActivateActions(null);
	}

	void OnDestroy()
	{
		//EventManager.StopListening (EventName, someListener);
	}
}