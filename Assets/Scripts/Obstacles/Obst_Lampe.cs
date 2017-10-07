using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Lampe : MonoBehaviour {

    public float cordLength;
    public int maxSpeed;
    Transform leTransPivot;
    Transform leTransLamp;
    bool switchSide;
    BoxCollider2D lampBox;
    LineRenderer lampCord;
    Vector3 vecDebutCord;
    Vector3 vecFinCord;
    Vector3 vecCord;
    // Use this for initialization
    void Start() {
        leTransPivot = gameObject.GetComponent<Transform>().GetChild(1);
        leTransLamp = gameObject.GetComponent<Transform>();
        
        lampBox = gameObject.GetComponent<BoxCollider2D>();
        lampCord = gameObject.GetComponent<LineRenderer>();
    
        switchSide = true;
        
        

        vecDebutCord = lampBox.bounds.center;
        vecCord = new Vector3(0,cordLength);
        vecFinCord = vecDebutCord + vecCord;
        lampCord.SetPosition(1, vecFinCord);
        
        leTransPivot.position = vecFinCord;
        Debug.Log(vecDebutCord);
        Debug.Log(vecFinCord);
        Debug.Log(leTransPivot.position);
        

    }
	
	// Update is called once per frame
	void Update () {
        //GAUCHE -0.4 (-45deg)
        //DROIT 0.4 (45deg)

        vecDebutCord = lampBox.bounds.center;
        lampCord.SetPosition(0, vecDebutCord);

        if (leTransPivot.rotation.z <= -0.4)
        {
            switchSide = true;
            //VERS LA DROITE
        }
        if (leTransPivot.rotation.z >= 0.4)
        {
            switchSide = false;
            //VERS LA GAUCHE
        }

        if (switchSide)
            leTransLamp.RotateAround(leTransPivot.position,Vector3.forward, Time.deltaTime * (maxSpeed / (Mathf.Abs(leTransPivot.rotation.z) * (maxSpeed / 10) + 1)));
        if(!switchSide)
            leTransLamp.RotateAround(leTransPivot.position, Vector3.forward, - Time.deltaTime * (maxSpeed / (Mathf.Abs(leTransPivot.rotation.z) * (maxSpeed / 10) + 1)));

        

        //0.4 = 45;
        //0 = 1;
    }
}
