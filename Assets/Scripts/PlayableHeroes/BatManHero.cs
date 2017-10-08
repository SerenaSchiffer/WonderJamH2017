using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManHero : PlayableHero {

    public GameObject bataCroutePrefab;
    public float bataCrouteSpeed;

    

    GameObject[] players;
    GameObject othPlayer;

    public override void Start()
    {
        base.Start();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public override void Spell1()
    {
        base.Spell1();

        if (powerDelay <= 0)
        {
            if (gameObject == players[0])
            {
                othPlayer = players[1];
            }
            else
            {
                othPlayer = players[0];
            }

            GameObject batCroutte = Instantiate(bataCroutePrefab);

            Vector3 thisPlayerPosition = transform.position;
            Vector3 velVector = othPlayer.transform.position - transform.position;
            if (velVector.x >= 0)
            {
                thisPlayerPosition.x += 1.0f;
            }
            else
            {
                thisPlayerPosition.x -= 1.0f;
            }

            int idSound = (int)Mathf.Round(Random.Range(0f, 1f));
            sounds[idSound].Play();

            batCroutte.transform.position = thisPlayerPosition;

            batCroutte.GetComponent<BataCroute>().SetSpawnerName(name);
            batCroutte.GetComponent<BataCroute>().SetOthPlayer(othPlayer);
            batCroutte.GetComponent<BataCroute>().SetThisPlayer(gameObject);
            batCroutte.GetComponent<BataCroute>().SetBataCroutteSpeed(bataCrouteSpeed);

            batCroutte.GetComponent<Rigidbody2D>().velocity = velVector * bataCrouteSpeed;

            cptPowerInLevel++;
            powerUsed = true;
        }
    }
}
