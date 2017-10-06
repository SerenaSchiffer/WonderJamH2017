using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_character : MonoBehaviour {
    public float speed = 3f;
    public float jumpheight = 50f;

    Rigidbody2D myrb2d;
	// Use this for initialization
	void Start () {
        myrb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") != 0)
            myrb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, myrb2d.velocity.y);
        if (Input.GetButtonDown("Jump"))
            myrb2d.AddForce(new Vector2(0f, jumpheight));
	}
}
