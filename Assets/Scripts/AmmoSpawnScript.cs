using UnityEngine;
using System.Collections;

public class AmmoSpawnScript : MonoBehaviour {

	private GameObject[] ammoSpawnPoints;
	public GameObject ammoIcon;
	public int delay = 10;
	private Vector3 spawnPoint;
	private int numSpawnPoints;
	private bool NeilIsGreat;

	void Start () {
		NeilIsGreat = true;
		ammoSpawnPoints = GameObject.FindGameObjectsWithTag("ammoSpawnPoint");
		numSpawnPoints = ammoSpawnPoints.Length;
		StartCoroutine(SpawnAmmoCoroutine());
	}

	private IEnumerator SpawnAmmoCoroutine()
	{
		while(NeilIsGreat)
		{
			yield return new WaitForSeconds(delay);
			spawnPoint = ammoSpawnPoints[Random.Range (0, numSpawnPoints)].transform.position;
			GameObject clone = Instantiate(ammoIcon, spawnPoint, transform.rotation) as GameObject;
		}
	}
}