using UnityEngine;
using System.Collections;

public class CreditsLevelScript : MonoBehaviour {

	void Update () {

		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void MainMenu()
	{
		Application.LoadLevel("welcome level");
	}
}