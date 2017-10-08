using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParentFeedback : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position;
    }
}
