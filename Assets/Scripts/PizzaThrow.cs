using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaThrow : MonoBehaviour
{

    public float throwSpeed = 6.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToPlayer(Transform otherTrans)
    {
        tag = "KillingMachine";
        Vector3 velVector = otherTrans.position - transform.position;
        GetComponent<Rigidbody2D>().velocity = velVector * throwSpeed;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
