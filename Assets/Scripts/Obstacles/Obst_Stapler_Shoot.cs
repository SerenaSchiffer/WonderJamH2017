using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Stapler_Shoot : MonoBehaviour {

    public float speed;
    public float timeToLive;
    Rigidbody2D leBody;
	// Use this for initialization
	void Start () {
        leBody = gameObject.GetComponent<Rigidbody2D>();

        leBody.AddForce(Vector2.up * speed);

        Destroy(gameObject, timeToLive);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
