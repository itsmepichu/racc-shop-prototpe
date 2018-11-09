using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("From Item Collsion");
    //    Debug.Log(collision.gameObject.name);
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("From Item Trigger");
        Debug.Log(collision.gameObject.name);
    }
}
