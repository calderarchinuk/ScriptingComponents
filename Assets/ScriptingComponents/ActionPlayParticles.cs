using UnityEngine;
using System.Collections;

public class ActionPlayParticles : ActionBase
{
	public ParticleSystem[] _targetParticles;
	public bool SpawnParticles = false;
	public GameObject _spawnParticles;

	public override void Activate (GameObject entity)
	{
		if (SpawnParticles)
		{
			Instantiate(_spawnParticles,transform.position,transform.rotation);
			return;
		}

		for (int i = 0; i<_targetParticles.Length;i++)
		{
			_targetParticles[i].Play();
		}

	}
}
