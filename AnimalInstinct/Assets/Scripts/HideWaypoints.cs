using UnityEngine;
using System.Collections;

public class HideWaypoints : MonoBehaviour {


	private Transform[] waypoints;

	// Use this for initialization
	void Start () {
		waypoints = gameObject.GetComponentsInChildren<Transform> ();

		foreach (Transform temp in waypoints) {
			if (temp.name.Equals ("Waypoint") || temp.name.Equals ("Waypoint 2")) {
				temp.GetComponent<Renderer> ().material.color = new Color(0, 0, 0, 0);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
