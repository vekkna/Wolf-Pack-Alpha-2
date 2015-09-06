using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour
{

		Text t;
		public int mins, secs;
		string timeString;

		void Start ()
		{
				t = GetComponent<Text> ();
				PrintTime ();
				StartCoroutine (FormatTimeString ());
		}

		IEnumerator FormatTimeString ()
		{
				while (true) {
						yield return new WaitForSeconds (1f);
	
						if (secs == 0 && mins == 0) {
								GameManagerScript.spiderWon = false;
								Application.LoadLevel ("game over level");
						} else if (secs == 0) {
								secs = 59;
								mins--;
						} else {
								secs--;
						}
	
						PrintTime ();
				}
		}

		private void PrintTime ()
		{
				if (secs > 9) {
						timeString = mins + ":" + secs;
				} else {
						timeString = mins + ":0" + secs;
				}
				t.text = timeString;
		}
}