using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public static bool canCollide;

	// Use this for initialization
	void Start () {
        canCollide = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag.Equals("Player") && canCollide){

            if (other.gameObject.GetComponent<Character> ().type == CharacterType.Dog) {
				DestroyObject (this.gameObject);
			}
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().ReduceTime();
                StartCoroutine(other.transform.GetComponent<Character>().CharacterHit());

            }
		}
    }
}
