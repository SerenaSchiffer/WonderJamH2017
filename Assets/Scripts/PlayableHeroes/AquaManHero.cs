using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaManHero : PlayableHero {

    AudioSource powerSound;

    public GameObject[] players;
    GameObject othPlayer;

    public override void Start()
    {
        base.Start();
        players = GameObject.FindGameObjectsWithTag("Player");
        powerSound = GetComponent<AudioSource>();
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

            int idSound = (int)Mathf.Round(Random.Range(0f, 1f));
            sounds[idSound].Play();

            othPlayer.GetComponent<PlayableHero>().Sauce();
            cptPowerInLevel++;
            powerUsed = true;
        }
    }
}
