using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{


	public CharacterType type;
	public int speed = 1;
	public int jump = 1;
	public bool flying = false;

	Rigidbody2D characterRigidBody;

	// Use this for initialization
	void Start ()
	{
		characterRigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Move ();
	}

	private void Move ()
	{
		float horizonal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		//Only jump if character hasn't jump already
		if ((characterRigidBody.velocity.y == 0 && vertical > 0) || (flying && vertical > 0 && characterRigidBody.velocity.y < 0)) {
			if (flying) {
				characterRigidBody.AddForce (new Vector2 (0, jump), ForceMode2D.Impulse);
			} else {
				characterRigidBody.AddForce (new Vector2 (0, jump), ForceMode2D.Impulse);
			}
		}

		characterRigidBody.velocity = new Vector2 (horizonal * speed, characterRigidBody.velocity.y);
	}

}


