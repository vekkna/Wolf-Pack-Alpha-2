using UnityEngine;
using System.Collections;

public class DontDestroyScript : MonoBehaviour {

	AudioSource song;

	void Awake()
	{
		DontDestroyOnLoad(gameObject.transform);
		GameObject other = GameObject.Find("Start screen");
		if (other != null)
			Destroy(other.gameObject);
		song = GetComponent<AudioSource>();
		if (!song.isPlaying)
			song.Play();
	}
}
