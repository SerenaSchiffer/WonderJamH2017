using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Classeur : MonoBehaviour {
    public float openingTime;
    float time = 0.00f;
    BoxCollider2D maBox;
    bool switching;
	// Use this for initialization
	void Start () {
        maBox = gameObject.GetComponent<BoxCollider2D>();
        switching = true;
	}

    // Update is called once per frame
    void Update() {
        if (time >= openingTime)
        {
            switching = false;
            maBox.enabled = false;
        }
        if(time <= 0)
        {
            switching = true;
            maBox.enabled = true;
        }

        if(switching)
        {
            time += Time.deltaTime;
        }
        if(!switching)
        {
            time -= Time.deltaTime;
        }
	}
}
