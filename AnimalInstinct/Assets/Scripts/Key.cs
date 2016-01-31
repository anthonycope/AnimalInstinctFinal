using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{
    //0 for dog, 1 for dragon
    public int type;

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
            if (type == 0)
            {
                Character.canUnlockDog = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().ShowText(0);
                this.gameObject.SetActive(false);

            }
            else if(type == 1)
            {
                Character.canUnlockDragon = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().ShowText(1);
                this.gameObject.SetActive(false);
            }
        }
    }
}
