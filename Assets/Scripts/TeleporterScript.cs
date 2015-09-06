using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TeleporterScript : MonoBehaviour {

	ParticleSystem particles;
	public SpawnMeterScript spawner;


	void Start () {

		particles = GetComponentInChildren<ParticleSystem>();
		particles.enableEmission = false;
	
	}
	

	void Update () {
	
	}

	void OnMouseOver()
	{
		if (spawner.isSpawning)
		{
			particles.enableEmission = true;
		}
	}

	void OnMouseExit()
	{
		particles.enableEmission = false;
	}

	void OnMouseDown()
	{
		if (spawner.isSpawning)
		{
			spawner.isSpawning = false;
			spawner.clone.GetComponent<EnemyScript>().Arrive(transform.position);
			spawner.isGrowing = true;

		}
	}
}
