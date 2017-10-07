using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_TriggerCeilingPiece : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInParent<Obst_FallingCeilingPiece>().StartFalling();
            Destroy(gameObject);
        }
    }
}
