using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_FallingCeilingPiece : MonoBehaviour {


    SpriteRenderer sprite;
    BoxCollider2D theCollider;

    public bool alwaysVisible = false;
    public float gravityScaleFalling = 1.0f;
    

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = alwaysVisible;
        theCollider = GetComponent<BoxCollider2D>();
        theCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "Player")
        {
            //coll.GetComponent<PlayableHero>().Kill();
            //Destroy(gameObject);
        }
    }

    public void StartFalling()
    {
        sprite.enabled = true;
        theCollider.enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = gravityScaleFalling;
    }
}
