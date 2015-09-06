using UnityEngine;
using System.Collections;

public class LightFluctuator : MonoBehaviour {

	bool isFading;
	public float min = 5f;
	public float max = 8f;
	public float rate = 1f;
	Light l;
	
	void Start () {
		l = GetComponent<Light>();
	}
	

	void Update () {

		if (isFading)
		{
			if (l.intensity > min)
			{
				l.intensity -= rate * Time.deltaTime;
			}
			else
			{
				isFading = false;
			}
		}
		else
		{
			if (l.intensity < max)
			{
				l.intensity += rate * Time.deltaTime;
			}
			else
			{
				isFading = true;
			}
		}
	}
}
