using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderManHero : PlayableHero
{
    public GameObject webPrefab;
    public float webSpeed = 25;

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

        GameObject web = Instantiate(webPrefab);

        Vector3 thisPlayerPosition = transform.position;
        thisPlayerPosition.x += 0.375f;

        web.transform.position = thisPlayerPosition;
        web.GetComponent<Rigidbody2D>().velocity = (othPlayer.transform.position - transform.position) * webSpeed;
    }

}
    

