using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_RandomKillingThing : MonoBehaviour {

    SpriteRenderer sprite;
    BoxCollider2D theCollider;
    Vector3 initialPosition;
    Vector3 target;
    public float repeatTime;
    bool isMoving = false;

    public float speed = 15.0f;
    public float minTimeRespawn = 5.0f;
    public float maxTimeRespawn = 20.0f;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        theCollider = GetComponent<BoxCollider2D>();
        theCollider.enabled = false;
        initialPosition = transform.position;

        target.x = transform.position.x + 15;
        target.y = transform.position.y + 15;
        StartCoroutine(RepeatingFunction());
        repeatTime = Random.Range(minTimeRespawn, maxTimeRespawn);
    }
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (transform.position == target)
            {
                transform.position = initialPosition;
                sprite.enabled = false;
                theCollider.enabled = false;
                isMoving = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //other.GetComponent<PlayableHero>.Kill();
            //
            ///////TODO
            //
        }
    }

    IEnumerator RepeatingFunction()
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatTime);
            sprite.enabled = true;
            theCollider.enabled = true;
            isMoving = true;
            repeatTime = Random.Range(minTimeRespawn, maxTimeRespawn);  
        }
    }
}
