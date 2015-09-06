using UnityEngine;
using System.Collections;

public class HideEnemyScript : MonoBehaviour {

	private GameObject[] players;
	private bool canBeSeen;

	void Start () {
		players = GameObject.FindGameObjectsWithTag("player");
	}

	void Update () {
		RaycastHit hit;
		foreach(GameObject player in players)
		{
			if (Physics.Raycast(transform.position, player.transform.position, out hit))
			{
				if (hit.collider.tag == "player")
				{
					canBeSeen = true;
				}
			}
		}
	}
}
