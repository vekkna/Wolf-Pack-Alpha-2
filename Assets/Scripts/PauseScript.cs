using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScript : MonoBehaviour {

	bool isPaused;
	Image image;
	Color on, off;



	void Start () {
		image = GetComponent<Image>();
		on = new Color(255f, 255f, 255f, 1f);
		off = new Color(255f, 255f, 255f, 0f);
		image.color = off;
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.P))
		{
			if(!isPaused)
			{
				Time.timeScale = 0.0f;
				isPaused = true;
				image.color = on;
			}
			else
			{
				Time.timeScale = 1.0f;
				isPaused = false;
				image.color = off;
			}
		}
	
	}
}