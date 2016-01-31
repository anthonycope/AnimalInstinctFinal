using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DogCage : MonoBehaviour {
    
    // 0 is dog, 1 is dragon
    public int type;

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
            if (type == 0 && Character.canUnlockDog)
            {
                //free dog
                GameObject.Find("DogButton").GetComponent<Button>().interactable = true;
                this.gameObject.SetActive(false);
            }
            else if(type == 1 && Character.canUnlockDragon)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().EndLevel();
                this.gameObject.SetActive(false);
            }
        }
    }
}
