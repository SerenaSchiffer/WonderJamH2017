using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_FallingCeilingPiece : MonoBehaviour {


    SpriteRenderer sprite;
    BoxCollider2D theCollider;

    public bool alwaysVisible = false;
    public float gravityScaleFalling = 1.0f;
    public bool canReappear = false;

    Vector3 initialPosition;
    Quaternion initialRotation;
    

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = alwaysVisible;
        theCollider = GetComponent<BoxCollider2D>();
        theCollider.enabled = false;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "under" && gravityScaleFalling < 0)
        {
            if (canReappear)
            {
                transform.position = initialPosition;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().Sleep();
                transform.rotation = initialRotation;
                sprite.enabled = alwaysVisible;
                theCollider.enabled = false;
            }
            else
                Destroy(gameObject);
        }
        if (coll.gameObject.tag == "Ground" && gravityScaleFalling >= 0)
        {
            if(canReappear)
            {
                transform.position = initialPosition;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().Sleep();
                transform.rotation = initialRotation;
                sprite.enabled = alwaysVisible;
                theCollider.enabled = false;
            }
            else
                Destroy(gameObject);
        }
    }

    public void StartFalling()
    {
        sprite.enabled = true;
        theCollider.enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = gravityScaleFalling;
    }

    public bool getCanReappear()
    {
        return canReappear;
    }
}
