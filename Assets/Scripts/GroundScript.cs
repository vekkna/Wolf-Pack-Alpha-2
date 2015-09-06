using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
	
	}

	void OnMouseDown()
	{
		Transform t = new GameObject().transform;
		t.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);;
		GameManagerScript.selectedSpider.GetComponent<AIPath>().target = t;
	}
}
