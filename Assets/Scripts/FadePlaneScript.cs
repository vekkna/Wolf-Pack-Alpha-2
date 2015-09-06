using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadePlaneScript : MonoBehaviour {

	public Canvas canvas;

	void Start () {
		StartCoroutine(fader());
	}

	void Update () {
	
	}

	private IEnumerator fader()
	{
		Color start = renderer.material.color;
		Color end = new Color (start.r, start.g, start.b, 0.0f);
		float t = 0.0f;
		float fadeRate = 0.5f;
		while (renderer.material.color.a >= 0f) {
			t += Time.deltaTime;
			renderer.material.color = Color.Lerp (start, end, t * fadeRate);
			yield return null;
		}
		canvas.enabled = true;
	

	}
}
