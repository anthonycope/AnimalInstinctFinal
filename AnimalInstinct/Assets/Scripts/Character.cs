using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

	public CharacterType type;
	public int speed = 1;
	public int jump = 1;

	Rigidbody2D characterRigidBody;

    public bool canMove;

	// Use this for initialization
	void Start ()
	{
		characterRigidBody = GetComponent<Rigidbody2D> ();
        //canMove = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (canMove)
        {
            Move();
        }
	}

	private void Move ()
	{
		float horizonal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		//Only jump if character hasn't jump already
		if ((characterRigidBody.velocity.y == 0 && vertical > 0) || (type == CharacterType.Dragon && vertical > 0 && characterRigidBody.velocity.y < 0)) {
				characterRigidBody.AddForce (new Vector2 (0, jump), ForceMode2D.Impulse);
		}

		characterRigidBody.velocity = new Vector2 (horizonal * speed, characterRigidBody.velocity.y);
	}

}


