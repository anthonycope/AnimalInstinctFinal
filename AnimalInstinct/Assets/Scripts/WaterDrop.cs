using UnityEngine;
using System.Collections;

public class WaterDrop : MonoBehaviour {

	private Vector3 initialLocation;
	private Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
		initialLocation = transform.position;
		myRigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other){
		transform.position = initialLocation;
		myRigidBody.velocity = new Vector2 (myRigidBody.velocity.x, 0);;
	}

}
