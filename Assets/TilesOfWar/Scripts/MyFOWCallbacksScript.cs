using UnityEngine;
using System.Collections;

public class MyFOWCallbacksScript : MonoBehaviour {

	FoWCallbacks callbacks;
	AudioSource hiss;

	void Start () 
	{

		callbacks = FindObjectOfType(typeof(FoWCallbacks)) as FoWCallbacks;
		hiss = GetComponent<AudioSource>();

		if (callbacks != null)
		{
			callbacks.OnNonPlayerUnitBecomesVisible = OnSpiderVisible;
			callbacks.OnNonPlayerUnitBecomesHidden = OnSpiderHidden;
			callbacks.OnNonPlayerUnitBecomesExplored = OnSpiderHidden;
		}
	}

	private void OnSpiderVisible(GameObject spider)
	{
		if (spider.tag == "enemy" && hiss != null)
		{
			hiss.Stop();
			hiss.Play();
			Canvas canvas = spider.GetComponentInChildren<Canvas>();
			if (canvas)
			spider.GetComponentInChildren<Canvas>().enabled = true;
			spider.GetComponent<EnemyScript>().anim.enabled = true;
		}
	}

	private void OnSpiderHidden(GameObject spider)
	{
		if (spider.tag == "enemy")
		{
				Canvas canvas = spider.GetComponentInChildren<Canvas>();
				if (canvas)
				spider.GetComponentInChildren<Canvas>().enabled = false;
				spider.GetComponent<EnemyScript>().anim.enabled = false;

		}
	}
}