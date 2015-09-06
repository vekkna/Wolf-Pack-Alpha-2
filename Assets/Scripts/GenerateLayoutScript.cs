using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateLayoutScript : MonoBehaviour {

	GameObject[] walls, batteries;
	public int numberOfBatteries = 8;
	public bool GetMouseInfo;


	void Awake () {

		walls  = GameObject.FindGameObjectsWithTag("wall");
		batteries = GameObject.FindGameObjectsWithTag("battery");

		int random;
		for (int i = 0; i < walls.Length; i++)
		{
			random = Random.Range(0,4);
			if (random == 3)
			{
			walls[i].SetActive(false);
			}
		}

		for (int i = 0; i < batteries.Length; i++)
		{
			batteries[i].SetActive(false);
		}

		List<GameObject> inactiveBatteries = new List<GameObject>();
		for (int i = 0; i < batteries.Length; i++)
		{
			if (batteries[i].activeSelf == false)
			{
				inactiveBatteries.Add(batteries[i]);
			}
		}

		int numberOfActiveBatteries = 0;
		int index;
		while(numberOfActiveBatteries < numberOfBatteries)
		{
			index = Random.Range(0, inactiveBatteries.Count);
			inactiveBatteries[index].SetActive(true);
			inactiveBatteries.RemoveAt(index);
			numberOfActiveBatteries++;
		} 
	}

	void Start()
	{
		AstarPath.active.Scan () ;
	}

	void Update()
	{
		if (GetMouseInfo)
		{
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				Debug.Log ("Name = " + hit.collider.name);
				Debug.Log ("Tag = " + hit.collider.tag);
				Debug.Log ("Hit Point = " + hit.point);
				Debug.Log ("Object position = " + hit.collider.gameObject.transform.position);
				Debug.Log ("--------------");
			}
		}
		}
	}
}