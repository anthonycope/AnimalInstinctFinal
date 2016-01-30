using UnityEngine;
using System.Collections;
using System;

public class BreakableObject : MonoBehaviour {

	private bool vaseBroke = false;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		try{
			gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		}catch(Exception e){
			Debug.Log ("GameManager doesn't exist in the scene");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!other.tag.Equals ("Player")) {
			
			if (transform.rotation.z >= 0.50F|| transform.rotation.z <= -0.50F) {
				if (!vaseBroke) {
					Debug.Log ("Vase broke");
					if (gameManager != null) {
						gameManager.ReduceTime ();
					}
					vaseBroke = true;
				}
			}
		}
	}
}
