using UnityEngine;
using System.Collections;

public class AmmoPickUpScript : MonoBehaviour {

	private ShooterScript ss;
	public ParticleSystem collect, destroy;
	AudioSource audio;
	public float collectSpeed = 10f;
	private Vector3 ammoDepot;
	public float speed = 40f;
	private bool isActive;
	FoWNonPlayerUnit fow;

	void Start()
	{
		isActive = true;
		fow = GetComponentInChildren<FoWNonPlayerUnit>();
		audio = GetComponent<AudioSource>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (isActive)
		{
		if (other.tag == "player")
		{
			isActive = false;
			fow.ShowInHidden = true;
			fow.ShowInExplored = true;
			ss = other.GetComponentInChildren<ShooterScript>();
			ammoDepot = ss.ammoDepot.position;
			
			StartCoroutine(Move (ammoDepot));
			audio.Stop ();
			audio.Play();
			
		}

		else if (other.tag == "enemy")
		{
			Destroy(gameObject);
		}
		}
	}

	private IEnumerator Move(Vector3 pos)
	{
		while (true)
		{
		transform.position = Vector3.MoveTowards(transform.position, ammoDepot, speed * Time.deltaTime);
		if (Vector3.SqrMagnitude(transform.position - ammoDepot) < 4f)
			{
	
				ss.numBullets = 5;
				ss.ammoText.text = "Ammo: " + 5;
				Destroy(gameObject);
			}
		yield return null;
		}
	}
}