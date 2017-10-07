using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_FallingCeilingPiece : MonoBehaviour {


    SpriteRenderer sprite;
    BoxCollider2D theCollider;
    Vector3 target;
    public bool alwaysVisible = false;
    bool isFalling = false;

    public float speed = 15.0f;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = alwaysVisible;
        theCollider = GetComponent<BoxCollider2D>();
        theCollider.enabled = false;
        target = transform.position;
        target.y = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (isFalling)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //other.GetComponent<PlayableHero>().Kill();
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    public void StartFalling()
    {
        sprite.enabled = true;
        theCollider.enabled = true;
        isFalling = true;
    }
}
