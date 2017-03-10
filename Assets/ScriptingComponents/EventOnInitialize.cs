using UnityEngine;
using System.Collections;


//scene manager calls initialize on all events immediately after loading the scene
public class EventOnInitialize : EventBase
{
	//TODO hunt down where initialize on start is used. replace with initialize on enable in base
	[SerializeField]
	private bool InitializeOnStart = false;

	public void Start()
	{
		if (InitializeOnStart){Initialize();}
	}

	public override void Initialize ()
	{
		base.Initialize ();
		ActivateActions(null);
	}
}