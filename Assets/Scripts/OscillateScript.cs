using UnityEngine;
using System.Collections;

public class OscillateScript : MonoBehaviour {

	float rotation;
	bool isIncreasing;
	public float low, high, speed;

	void Start () {
		rotation = low;
	}

	void Update () {

		if (rotation == low)
		{
			isIncreasing = true;
		}
		else if (rotation == high)
		{
			isIncreasing = false;
		}

		if (isIncreasing)
		{
			rotation += Time.deltaTime * speed;
		}
		else
		{
			rotation -= Time.deltaTime * speed;
		}

		transform.Rotate(0, Mathf.Sin(rotation), 0);
	
	}
}
