using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Stapler : MonoBehaviour {

    public GameObject bullet;
    public float shootingRate;
    Transform pos;
    float time;
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
        vec.y = pos.position.y + 0.1f;
        vec.z = pos.position.z;
        Quaternion myRotation = pos.rotation;
        myRotation.w = 15;
        myRotation.z = 1;

        Instantiate(bullet, vec, myRotation);
    }
}
