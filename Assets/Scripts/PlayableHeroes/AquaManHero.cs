using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaManHero : PlayableHero {

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

            othPlayer.GetComponent<PlayableHero>().Sauce();
            cptPowerInLevel++;
            powerUsed = true;
        }
    }
}
