using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Escabeau : MonoBehaviour {


    public float throwForce = 20.0f;
    int cpt = 3;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void ThrowStep()
    {
        cpt--;
        if(cpt == 2)
        {
            int rdm = Random.Range(0, 3);
            transform.GetChild(rdm).gameObject.tag = "KillingMachine";
            transform.GetChild(rdm).GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
        }
        else if(cpt == 1)
        {
            int rdm = Random.Range(0, 2);
            transform.GetChild(rdm).gameObject.tag = "KillingMachine";
            transform.GetChild(rdm).GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
        }
        else
        {
            transform.GetChild(0).gameObject.tag = "KillingMachine";
            transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
        }
        
    }
}
