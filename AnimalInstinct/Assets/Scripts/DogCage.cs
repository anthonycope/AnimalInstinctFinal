using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DogCage : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            //free dog
            GameObject.Find("DogButton").GetComponent<Button>().interactable = true;
            this.gameObject.SetActive(false);
        }
    }
}
