﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderManHero : PlayableHero
{
    public GameObject webPrefab;
    public float webSpeed;

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

            GameObject web = Instantiate(webPrefab);

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

            Debug.Log(velVector);
            web.transform.position = thisPlayerPosition;
            web.GetComponent<Web>().SetSpawnerName(name);
            web.GetComponent<Rigidbody2D>().velocity = velVector * webSpeed;

            cptPowerInLevel++;
            powerUsed = true;
        }
    }
}
    

