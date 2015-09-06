using UnityEngine;
using System.Collections;

public class InstructionsPageScript : MonoBehaviour {

	void Start () {
	}

	void Update () {
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.LoadLevel("welcome level");
		}
	}

	public void MainMenu()
	{
		Application.LoadLevel("welcome level");
	}
}
