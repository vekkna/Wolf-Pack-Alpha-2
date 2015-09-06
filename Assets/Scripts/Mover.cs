using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	
	public float MoveSpeed;	
	public float TurnSpeed;		
	public float reverseSpeed;
	public string Left, Right, Up, Down;
	Vector3 forward;
	CharacterController controller;
	bool isMoving;
	public GameObject explosion;
	public GameObject scoreScript;
	Transform trans;
	bool isDead;
	internal Animation anim;
	internal bool isFrozen;
	public ParticleSystem electricity;
	internal AudioSource audio;
	
	void Start(){
		controller = GetComponent<CharacterController>();
		forward = new Vector3();
		trans = transform;
		anim = animation;
	//	electricity = GetComponent<ParticleSystem>();
		electricity.enableEmission = false;
		audio = GetComponent<AudioSource>();
	}
	
	void Update()
	{
		controller.Move(new Vector3(0.000001f, 0.0f, 0.0f));
		if (!isFrozen)
		{
		if (Input.GetKey (Left)) {
			anim.Play("move");
			trans.Rotate(0, TurnSpeed * -1 * Time.deltaTime, 0);
				}
		if (Input.GetKey(Right)){
			anim.Play("move");
			trans.Rotate(0, TurnSpeed * Time.deltaTime, 0);
		}

		if (Input.GetKey (Up)) {
			anim["move"].speed = -1;
			anim.Play("move");
			forward = trans.TransformDirection (Vector3.forward);
			controller.Move(forward * MoveSpeed * Time.deltaTime);
		} 	
		if (Input.GetKey (Down)) {
			anim["move"].speed = 1;
			anim.Play("move");
			forward = trans.TransformDirection(Vector3.back);
			controller.Move(forward * reverseSpeed * Time.deltaTime);
		}

		if (Input.GetKeyUp (Left) | Input.GetKeyUp (Right) | Input.GetKeyUp (Up) | Input.GetKeyUp (Down))
		    {
			anim.Stop("move");
		}
		}
	}
//
//	void OnMouseDown()
//	{
//		GameManagerScript.selectedSpider.GetComponent<AIPath>().target = trans;
//	}
	
//	public void Die()
//	{
//		if(!isDead)
//		StartCoroutine(DieCoRoutine());
//	}
//
//	IEnumerator DieCoRoutine()
//	{
//		isDead = true;
//		Instantiate(explosion, new Vector3(trans.position.x, trans.position.y + 5, trans.position.z), trans.rotation);
//		GameManagerScript.numberOfPlayers--;
//		yield return new WaitForSeconds(3.0f);
//		scoreScript.GetComponent<ScoreScript>().GameOver();
//		Destroy(gameObject);
//	}
}