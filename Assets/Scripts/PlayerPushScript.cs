using UnityEngine;
using System.Collections;

public class PlayerPushScript : MonoBehaviour {

	Vector3 vel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//vel = transform.rigidbody.velocity;
		//Debug.Log("vel:" + vel);
	}

//	void OnTriggerEnter(Collider other)
//	{
//		if (other.tag=="battery")
//		{
//		
//		float speed = GetComponent<Mover>().MoveSpeed;
//		Vector3 dir = new Vector3(rigidbody.m;
//		other.rigidbody.velocity = dir;
//
//		}
//	}

	public float pushPower = 2.0F;
	void OnControllerColliderHit(ControllerColliderHit hit) 
	{
				Rigidbody body = hit.collider.attachedRigidbody;
				if (body == null || body.isKinematic)
					return;
				
				//if (hit.moveDirection.y < -0.3F)
				//	return;
				
				Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
				body.velocity = pushDir * pushPower;
	}
}
