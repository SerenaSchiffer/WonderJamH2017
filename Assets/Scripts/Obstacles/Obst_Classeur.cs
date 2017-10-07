using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Classeur : MonoBehaviour {
    public float openingTime;
    public float speed;
    float time = 0.00f;
    Transform tiroirTransform;
    bool switching;
    Vector3 vec;
    float distance;
    // Use this for initialization
    void Start () {
        tiroirTransform = gameObject.GetComponent<Transform>().GetChild(0);
        switching = true;
        vec = tiroirTransform.position;
        distance = 0;
    }

    // Update is called once per frame
    void Update() {
        if (time >= openingTime)
        {
            switching = false;
            distance = 0;
            
            
        }
        if(time <= 0)
        {
            switching = true;
            distance = 0;
           
        }

        if(switching)
        {
            time += Time.deltaTime;
            if (distance <= 0.21f)
            {
                distance += 0.01f * speed;
                vec.x -= distance;
                tiroirTransform.position = vec;
            }
                

        }
        if(!switching)
        {
            time -= Time.deltaTime;

            if (distance <= 0.21f)
            {
                distance += 0.01f *speed;
                vec.x += distance;
                tiroirTransform.position = vec;
            }
        }
	}
}
