using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Stapler : MonoBehaviour {

    public GameObject bullet;
    public float shootingRate;
    Transform pos;
    float time;
    bool switching;
	// Use this for initialization
	void Start () {
        pos = gameObject.GetComponent<Transform>();
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if(time > shootingRate)
        {
            Shot();
            time = 0;
        }

    }

    void Shot()
    {
        Vector3 vec;
        vec.x = pos.position.x;
        vec.y = pos.position.y + 1;
        vec.z = pos.position.z;

        Instantiate(bullet, vec, pos.rotation);
    }
}
