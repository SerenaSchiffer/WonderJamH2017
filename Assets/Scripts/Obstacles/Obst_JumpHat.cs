using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_JumpHat : MonoBehaviour {

    public float jumpHeight = 340.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpHeight);
        }
    }
}
