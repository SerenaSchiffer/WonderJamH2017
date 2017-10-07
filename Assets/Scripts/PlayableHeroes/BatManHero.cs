using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManHero : PlayableHero {

    public GameObject bataCroutePrefab;
    public float bataCrouteSpeed;

    GameObject[] players;
    GameObject othPlayer;

    public override void Awake()
    {
        base.Awake();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public override void Spell1()
    {
        base.Spell1();

        if (gameObject == players[0])
        {
            othPlayer = players[1];
        }
        else
        {
            othPlayer = players[0];
        }

        GameObject web = Instantiate(bataCroutePrefab);

        Vector3 thisPlayerPosition = transform.position;
        Vector3 velVector = othPlayer.transform.position - transform.position;
        if (velVector.x >= 0)
        {
            thisPlayerPosition.x += 0.4f;
        }
        else
        {
            thisPlayerPosition.x -= 0.4f;
        }

        web.transform.position = thisPlayerPosition;
        web.GetComponent<BataCroute>().SetSpawnerName(name);
        web.GetComponent<Rigidbody2D>().velocity = velVector * bataCrouteSpeed;
    }

}
