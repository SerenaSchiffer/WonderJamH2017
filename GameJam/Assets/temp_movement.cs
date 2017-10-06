using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_movement : MonoBehaviour {

    Rigidbody2D myrb2d;
    public float speed = 1f;
    public float jumpforce = 30f;
	// Use this for initialization
	void Start () {
        myrb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float kekx = Input.GetAxis("Horizontal");
        myrb2d.velocity = new Vector2(kekx * speed, myrb2d.velocity.y);

        if (Input.GetButtonDown("Jump"))
            myrb2d.AddForce(new Vector2(0f, jumpforce));
	}
}
