using UnityEngine;
using System.Collections;
using System;

public class Character : MonoBehaviour
{

	public CharacterType type;
	public int speed = 1;
	public int jump = 1;
	public bool flying = false;

	Rigidbody2D characterRigidBody;

    public bool canMove;

	// Use this for initialization
	void Start ()
	{
		characterRigidBody = GetComponent<Rigidbody2D> ();
        canMove = false;

        if (type == CharacterType.Dog)
        {
            InitializeDog();
        }
        else if(type == CharacterType.Cat)
        {
            InitializeCat();
        }
	}

    private void InitializeCat()
    {
        canMove = true;
    }

    private void InitializeDog()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update ()
	{
        if (canMove )
        {
            Move();
        }
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


