using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlickerScript : MonoBehaviour {

	public float rate = 0.5f;
	bool NeilIsGreat = true;

	void Start () {
		StartCoroutine(Flicker(rate));
	}

	IEnumerator Flicker(float rate)
	{
		while (NeilIsGreat)
		{
			yield return new WaitForSeconds(rate);
			GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
			yield return new WaitForSeconds(rate);
			GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
		}
	}
}
