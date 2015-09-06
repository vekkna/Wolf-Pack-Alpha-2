using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShooterScript : MonoBehaviour
{
		public string fireButton;
		public float speed, reloadTime;
		public Slider slider;
		public GameObject bullet;
		public GameObject vehicle;
		Animation anim;
		bool canShoot;
		public string tag;
		internal bool isFrozen;
		public int numBullets = 5;
		AudioSource audio;
		public Text ammoText;
		Transform trans;
		public Transform ammoDepot;

		void Start ()
		{
				anim = vehicle.animation;
				anim ["attack"].speed = 1.5f;
				canShoot = true;
				trans = transform;
				audio = GetComponent<AudioSource>();
		}

		void Update ()
		{
				if (Input.GetKeyDown (fireButton)) 
				{
					StartCoroutine (Fire ());
				}
		}

		IEnumerator Fire ()
		{
			if (canShoot && !isFrozen && numBullets > 0)
		{
				canShoot = false;
				numBullets --;
				ammoText.text = "Ammo: " + numBullets;
				audio.Stop();
				audio.Play();
				GameObject clone = GameObject.Instantiate (bullet, new Vector3 (trans.position.x, trans.position.y + 1, trans.position.z), trans.rotation) as GameObject;
				clone.rigidbody.AddForce (trans.forward * speed);
				clone.tag = tag;
				anim.Play ("attack");
				yield return new WaitForSeconds (reloadTime);
				canShoot = true;
		}
		}
}
