using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

	public CharacterType type;
	public int speed = 1;
	public int jump = 1;

	private Rigidbody2D characterRigidBody;
	private Renderer myRenderer;

    public bool canMove;

	// Use this for initialization
	void Start ()
	{
		characterRigidBody = GetComponent<Rigidbody2D> ();
		myRenderer = GetComponent<Renderer> ();
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

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag.Equals("Enemy") && type == CharacterType.Cat){
			StartCoroutine (CharacterHit ());
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

	private IEnumerator CharacterHit(){
		int initialSpped = speed;
		speed = 1;
		myRenderer.material.color = new Color (myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b, 0.5F);
		yield return new WaitForSeconds (3F);

		speed = initialSpped;
		myRenderer.material.color = new Color (myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b, 1F);


	}

}


