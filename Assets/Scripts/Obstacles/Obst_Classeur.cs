using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Classeur : MonoBehaviour {
    public float openingTime;
    public float speed;
    float time = 0.00f;
    Transform tiroirTransform;
    BoxCollider2D tiroirCollider;
    bool switching;
    Vector3 vec;
    float distance;
    bool activate;
    float tiroirWidth;
    float targetOpen;
    float targetClose;
    // Use this for initialization
    void Start () {
        tiroirTransform = gameObject.GetComponent<Transform>().GetChild(0);
        tiroirCollider = gameObject.GetComponent<BoxCollider2D>();
        switching = true;
        vec = tiroirTransform.position;
        distance = 0;
        activate = false;
        tiroirWidth = tiroirCollider.size.x;
        //target =tiroirTransform.position.x- tiroirWidth*transform.parent.localScale.x;
        targetOpen = tiroirTransform.position.x - 1 / tiroirTransform.localScale.x ;
        targetClose = tiroirTransform.position.x;

    }

    // Update is called once per frame
    void Update() {
        if(activate)
        {
            if (time >= openingTime)
            {
                switching = false;
                distance = 0;


            }
            if (time <= 0)
            {
                switching = true;
                distance = 0;

            }

            if (switching)
            {
                time += Time.deltaTime;
                if (distance <= tiroirWidth)
                {
                    distance =0.01f * speed;
                    if(vec.x >targetOpen)
                    {
                        vec.x = Mathf.MoveTowards(vec.x, targetOpen, distance);
                        tiroirTransform.position = vec;
                    }
                       
                }


            }
            if (!switching)
            {
                time -= Time.deltaTime;

                if (distance <= tiroirWidth)
                {
                    distance = 0.01f * speed;
                    if(vec.x < targetClose)
                    {
                        vec.x = Mathf.MoveTowards(vec.x,targetClose, distance);
                        tiroirTransform.position = vec;
                    }
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        activate = true;
    }

}
