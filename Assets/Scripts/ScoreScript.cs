using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScript : MonoBehaviour
{
		int score;
		internal float startingHealth;
		Text text;
		internal bool numFullBatteriesHasChanged;
		GameObject[] batteries;
		GameObject battery;

		void Start ()
		{
				text = GetComponent<Text> ();
				score = 0;
				startingHealth = 100;
				StartCoroutine (UpdateScore ());			
		}

		void Update ()
		{
				if (Input.GetKey (KeyCode.Escape)) {
						Application.Quit ();
				}
		}

		IEnumerator UpdateScore ()
		{
				yield return new WaitForSeconds (1);
				while (true) {
						//if (numFullBatteriesHasChanged) {
								batteries = GameObject.FindGameObjectsWithTag ("battery");
								for (int i = 0; i < batteries.Length; i++) {
										if (batteries [i].GetComponent<BatteryScript> ().currentHealth >= startingHealth) {
												score++;
												GameManagerScript.score = score;
												
										}
								}
								text.text = "Score: " + score;
								yield return new WaitForSeconds (1);
					//	}
					//	numFullBatteriesHasChanged = false;
				}
		}
	

		public void GameOver ()
		{
				GameManagerScript.score = score;
				Application.LoadLevel ("game over level");
		}
}