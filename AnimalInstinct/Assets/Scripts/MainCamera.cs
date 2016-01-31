using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
	public float dampTime = 0.15f;


	private Transform player;
	private Vector3 nonMovingCameraLocation;
	private Vector3 velocity = Vector3.zero;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    public void SwitchPlayer()
    {
        
        GameObject temp;
        temp = GameObject.FindGameObjectWithTag("Player");
        if (temp != null)
        {
            player = temp.transform;

        }

    }
	void LateUpdate ()
	{
        if (player == null)
        {
            GameObject temp;
            temp = GameObject.FindGameObjectWithTag("Player");
            if(temp!= null)
            {
                player = temp.transform;

            }
            
        }
        else
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(player.position);
            Vector3 delta = player.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.40f, 5F));
            Vector3 destination = transform.position + delta;

            nonMovingCameraLocation = new Vector3(player.position.x, -10.5F, -5F);

            transform.position = Vector3.SmoothDamp(transform.position, nonMovingCameraLocation, ref velocity, dampTime);
        }

	}
}
