using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillScoreScript : MonoBehaviour {

	Text text;
	public string color;

	void Start () {
		text = GetComponent<Text>();
		text.text = "Kill score:";
	}

	void Update () {
	}

	public void UpdateScore(string color, int points)
	{
		if (color == "red")
		{
			GameManagerScript.redKillScore += points;
			text.text = "Kill score: " + GameManagerScript.redKillScore;
		}
		else
		{
			GameManagerScript.blueKillScore += points;
			text.text = "Kill score: " + GameManagerScript.blueKillScore;
		}

	}
}