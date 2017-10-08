using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BataCroute : MonoBehaviour {

    private string spawnerName;
    private AudioSource sound;

    GameObject othPlayer;
    GameObject thisPlayer;
    float BataCroutteSpeed;

	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 velVector = othPlayer.transform.position - thisPlayer.transform.position;
        GetComponent<Rigidbody2D>().velocity = velVector * BataCroutteSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player" && other.name != spawnerName)
        {
            sound.Play();
            int side;
            GameObject batman = GameObject.Find(spawnerName);
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

    public void SetOthPlayer(GameObject othPlayer) { this.othPlayer = othPlayer; }
    public void SetThisPlayer(GameObject thisPlayer) { this.thisPlayer = thisPlayer; }
    public void SetBataCroutteSpeed(float BataCroutteSpeed) { this.BataCroutteSpeed = BataCroutteSpeed; }
}
