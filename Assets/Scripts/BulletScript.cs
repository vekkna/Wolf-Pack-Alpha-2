using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float damage;
	public GameObject sprite;
	AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource>();
	}

	void Update()
	{
	}

	void OnTriggerEnter(Collider other) {

		//Vector3 vel = transform.rigidbody.velocity;

		//other.rigidbody.AddForce(vel * 5);

		//audio.Play();
		if (other.tag == "enemy") {
		//	other.GetComponent<CharacterController>().enabled = false;


			EnemyScript script = other.GetComponent<EnemyScript>();
			script.healthHasChanged = true;
			script.currentHealth -= damage;
			script.lastHit = gameObject.tag;
			script.Flee();
			}

			StartCoroutine(Explode());

	}

	IEnumerator Explode()
	{
		sprite.renderer.enabled = false;
		collider.enabled = false;
		rigidbody.velocity = Vector3.zero;
		yield return new WaitForSeconds(1.0f);
		Destroy(gameObject);
	}
}