using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Classeur : MonoBehaviour {
    public float openingTime;
    float time = 0.00f;
    BoxCollider2D maBox;
	// Use this for initialization
	void Start () {
        maBox = gameObject.GetComponent<BoxCollider2D>();
	}

    // Update is called once per frame
    void Update() {
        if (time == openingTime)
        {
            while(time != 0)
            {
                time -= Time.deltaTime;

                if (time < 0)
                    time = 0;
            }
            
            maBox.enabled = false;
        }
        else
        {
            while (time != openingTime)
            {
                time += Time.deltaTime;

                if (time > openingTime)
                    time = openingTime;
            }
            maBox.enabled = true;
        }
        
	}
}
