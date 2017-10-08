using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Travailleur : MonoBehaviour {


    public float speed = 4.0f;
    public float maxLeft = 10f;
    public float maxRight = 10f;
    public float minTimeReverse = 0.5f;
    public float maxTimeReverse = 4.0f;
    private int direction;
    private float repeatTime = 1.0f;

    // Use this for initialization
    void Start () {
        maxRight = transform.position.x + maxRight;
        maxLeft = transform.position.x - maxLeft;
        int startdirection = Random.Range(-5, 6);
        if (startdirection < 0)
            direction = -1;
        else
            direction = 1;
        StartCoroutine(RepeatingFunction());
    }
	
	// Update is called once per frame
	void Update () {
        switch (direction)
        {
            case -1:
                // Moving Left
                if (transform.position.x > maxLeft)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                    GetComponent<SpriteRenderer>().flipX = true;
                    if(transform.childCount > 0)
                        transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    direction = 1;
                }
                break;
            case 1:
                //Moving Right
                if (transform.position.x < maxRight)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                    GetComponent<SpriteRenderer>().flipX = false;
                    if (transform.childCount > 0)
                        transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    direction = -1;
                }
                break;
        }
    }

    IEnumerator RepeatingFunction()
    {
        while (true)
        {
            //execute code here.
            if (direction == -1)
                direction = 1;
            else
                direction = -1;
            repeatTime = Random.Range(minTimeReverse, maxTimeReverse);
            yield return new WaitForSeconds(repeatTime);
        }
    }

}
