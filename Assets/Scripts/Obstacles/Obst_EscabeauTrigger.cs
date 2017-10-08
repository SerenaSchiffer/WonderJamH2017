using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_EscabeauTrigger : MonoBehaviour {

    int cpt = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            int rdm = Random.Range(0, 2);
            if (rdm == 1)
            {
                cpt--;
                GetComponentInParent<Obst_Escabeau>().ThrowStep();
                if (cpt == 0)
                {
                    Destroy(gameObject);
                }
                    
            }
        }
    }
}
