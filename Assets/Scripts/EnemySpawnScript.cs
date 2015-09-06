using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawnScript : MonoBehaviour {

	public GameObject scout, soldier, queen;
	EnemyScript enemyScript;
	GameObject[] spawnPoints;
	int numSpawnPoints;
	Quaternion id;
	public int dangerRate = 4;
	internal int size;
	public float spawnRate = 10f;
	float stagger;// to stagger the spawns so that AI methods are spread out

	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag("enemy spawn");
		numSpawnPoints = spawnPoints.Length;
		StartCoroutine(Spawn());
		id = Quaternion.identity;
	}

	int index;
	IEnumerator Spawn()
	{
		yield return new WaitForSeconds(10f);
		while (true)
		{
			Instantiate(scout, spawnPoints[Random.Range(0, numSpawnPoints)].transform.position, id);

			stagger = Random.Range(10, 90) * 0.01f;
			yield return new WaitForSeconds(spawnRate + stagger);
		}
	}
}