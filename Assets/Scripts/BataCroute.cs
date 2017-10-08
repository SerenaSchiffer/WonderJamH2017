using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataCroute : MonoBehaviour {

    private string spawnerName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player" && other.name != spawnerName)
        {
            int side;
            GameObject batman = GameObject.Find("Char_Batman");
            if (batman.transform.position.x > other.transform.position.x)
                side = -1;
            else
                side = 1;
            other.GetComponent<PlayableHero>().Bump(side);

            Destroy(gameObject);
        }
    }

    public void SetSpawnerName(string spawnerName)
    {
        this.spawnerName = spawnerName;
    }
}
