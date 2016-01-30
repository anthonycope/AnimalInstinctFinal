using UnityEngine;
using System.Collections;

public class FirstLoad : MonoBehaviour {

    public static bool firstLoad = true;// true;

    void Awake()
    {
       //firstLoad = false;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnApplicationQuit()
    {
        firstLoad = true;
    }
}
