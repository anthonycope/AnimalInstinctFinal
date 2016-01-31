using UnityEngine;
using System.Collections;

public class WaterDrop : MonoBehaviour {

	private Vector3 initialLocation;
	private Rigidbody2D myRigidBody;
    public static bool canCollide;

	// Use this for initialization
	void Start () {
		initialLocation = transform.position;
		myRigidBody = GetComponent<Rigidbody2D> ();
        //this.GetComponent<AudioSource>().Play();
        canCollide = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other){

        transform.position = initialLocation;

        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (manager.inGame)
        {
         //   this.GetComponent<AudioSource>().Play();
        }
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
        if (other.transform.tag == "Player" && canCollide)
        {
            manager.ReduceTime();
            StartCoroutine(other.transform.GetComponent<Character>().CharacterHit());
            
        }
	}

}
