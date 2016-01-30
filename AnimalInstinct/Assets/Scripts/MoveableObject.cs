using UnityEngine;
using System.Collections;

public class MoveableObject : MonoBehaviour {

	Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals ("Player")) {
			if (other.GetComponent<Character> ().type == CharacterType.Dog) {
				myRigidBody.constraints = RigidbodyConstraints2D.None;

			} else {
				myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

			}
		}
	}
}
