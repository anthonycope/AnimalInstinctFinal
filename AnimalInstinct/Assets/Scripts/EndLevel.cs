﻿using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            //free dog
            GameObject.Find("GameManager").GetComponent<GameManager>().EndLevel();
           // this.gameObject.SetActive(false);
        }
    }
}