using UnityEngine;
using System.Collections;
using System;

public class Character : MonoBehaviour
{

	public CharacterType type;
	public int speed = 1;
	public int jump = 1;

	private Rigidbody2D characterRigidBody;
	private Renderer myRenderer;
	public bool canMove;
	private bool facingRight = false;

	private Animator myAnimator;

    public static bool canUnlockDog;
    public static bool canUnlockDragon;

	// Use this for initialization
	void Start ()
	{
		characterRigidBody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		canMove = true;

		if (type == CharacterType.Dog) {
			InitializeDog ();
		} else if (type == CharacterType.Cat) {
			InitializeCat ();
		}
        else if(type == CharacterType.Dragon)
        {
            InitializeDragon();
        }
	}

	private void InitializeCat ()
	{
		canMove = true;
	}

	private void InitializeDog ()
	{
		canMove = false;
	}

    private void InitializeDragon()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update ()
	{
		if (canMove) {
			Move ();
            if(type == CharacterType.Dragon)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    ShootFire(true);
                }
                else if(Input.GetKeyUp(KeyCode.Space))
                {
                    ShootFire(false);
                }
                ParticleSystem ps = GetComponentInChildren<ParticleSystem>();

                if (facingRight)
                {
                    Vector3 angles = ps.transform.rotation.eulerAngles;
                    Quaternion newRotation = ps.transform.rotation;
                    newRotation.z = 0f;
                    ps.transform.rotation = newRotation;
                }
                else
                {
                    Vector3 angles = ps.transform.rotation.eulerAngles;
                    Quaternion newRotation = ps.transform.rotation;
                    newRotation.z = 180f;
                    ps.transform.rotation = newRotation;
                }
            }
		}
	}

	private void Move ()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		if (facingRight && horizontal < 0) {
			Flip ();
			facingRight = false;
		} else if (!facingRight && horizontal > 0) {
			Flip ();
			facingRight = true;
		}

        if (horizontal > 0 || horizontal < 0)
        {
            myAnimator.SetInteger("Movement", 1);
        }
        else
        {
            myAnimator.SetInteger("Movement", 0);

        }

        if (vertical > 0f)
        {
            AudioSource sound = this.GetComponent<AudioSource>();
            if (!sound.isPlaying)
            {
                sound.Play();
            }
        }

        //Only jump if character hasn't jump already
        if ((characterRigidBody.velocity.y == 0 && vertical > 0) || (type == CharacterType.Dragon && vertical > 0 && characterRigidBody.velocity.y < 0)) {
			characterRigidBody.AddForce (new Vector2 (0, jump), ForceMode2D.Impulse);
		}

		characterRigidBody.velocity = new Vector2 (horizontal * speed, characterRigidBody.velocity.y);
	}

	private IEnumerator CharacterHit ()
	{
		int initialSpped = speed;
		speed = 1;
		myRenderer.material.color = new Color (myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b, 0.5F);

		yield return new WaitForSeconds (3F);

		speed = initialSpped;
		myRenderer.material.color = new Color (myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b, 1F);

	}

	private void Flip ()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void ShootFire(bool start)
    {
        if (start)
        {
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            var em = ps.emission;
            em.enabled = true;
            //var rate = em.rate;

            //rate.constantMax = 1000f;

            //em.rate = rate;

            if (facingRight)
            {
                Vector3 angles = ps.transform.rotation.eulerAngles;
                Quaternion newRotation = ps.transform.rotation;
                newRotation.z = 0f;
                ps.transform.rotation = newRotation;
            }
            else
            {
                Vector3 angles = ps.transform.rotation.eulerAngles;
                Quaternion newRotation = ps.transform.rotation;
                newRotation.z = 180f;
                ps.transform.rotation = newRotation;
            }
            ps.Play();

        }
        else
        {
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            var em = ps.emission;

            //var rate = em.rate;

            //rate.constantMax = 0f;

            //em.rate = rate;

            em.enabled = false;
        }

    }
}


