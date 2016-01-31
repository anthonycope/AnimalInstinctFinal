using UnityEngine;
using System.Collections;

public class PacingMovement : MonoBehaviour
{
	//Waypoints to travel between
	public GameObject[] waypoints;
	//Speed of the character
	public float speed = 1F;
	//How close to get to the waypoint before going to next
	public float distance = 0.1F;

	private int currentPoint = 0;
	private Vector3 targetPosition;

	// Use this for initialization
	void Start ()
	{
		targetPosition = waypoints [currentPoint].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{        
        if ((Vector3.Distance (transform.position, targetPosition) < distance)) {
			NextPoint ();
		} else {
			float direction = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards (this.transform.position, targetPosition, direction);
		}
	}

	private void NextPoint(){
		if (currentPoint >= waypoints.Length - 1) {
			currentPoint = 0;
		} else {
			currentPoint++;
		}
		targetPosition = waypoints [currentPoint].transform.position;
        Flip();
	}

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
