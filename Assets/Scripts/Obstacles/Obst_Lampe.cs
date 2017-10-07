using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Lampe : MonoBehaviour {


    Transform leTrans;
    bool switchSide;
    public int maxSpeed;
	// Use this for initialization
	void Start () {
        leTrans = gameObject.GetComponent<Transform>();
        switchSide = true;
	}
	
	// Update is called once per frame
	void Update () {
        float x = leTrans.localEulerAngles.z;
        //GAUCHE -0.4
        //DROIT 0.4

        if(leTrans.rotation.z <= -0.4)
        {
            switchSide = true;
            //VERS LA DROITE
        }
        if (leTrans.rotation.z >= 0.4)
        {
            switchSide = false;
            //VERS LA GAUCHE
        }

        if(switchSide)
            leTrans.Rotate(0, 0, Time.deltaTime * (maxSpeed / (Mathf.Abs(leTrans.rotation.z)*(maxSpeed/10) + 1)));
        if(!switchSide)
            leTrans.Rotate(0, 0, -Time.deltaTime * (maxSpeed / (Mathf.Abs(leTrans.rotation.z)*(maxSpeed/10) + 1)));

        //0.4 = 45;
        //0 = 1;
    }
}
