using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {
    public enum Characters
    {
        Spidercheese,
        Batcroute,
        Sauceman,
        JeanGras
    };

    public static int nombreEtages = 3;
    public static Characters player1Char = Characters.Sauceman;
    public static Characters player2Char = Characters.Spidercheese;


    public Image[] player1;
    public Image[] player2;
    public Text creditsText;

    bool coolDownP1;
    bool coolDownP2;
    bool P1Selected;
    bool P2Selected;
    int player1Choice = 0;
    int player2Choice = 1;
    int credits = 0;

    AudioSource[] menuSounds;

	// Use this for initialization
	void Start () {
        coolDownP1 = false;
        coolDownP2 = false;
        P1Selected = false;
        P2Selected = false;
        menuSounds = GetComponents<AudioSource>();
	}

    int vraimod(float a, float b)
    {
        return (int)Mathf.Floor(a - b * Mathf.Floor(a / b));
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Player1Vertical") > 0.5f && !coolDownP1 && !P1Selected)
        {
            player1Choice = vraimod( player1Choice + 1, 4 );
            if (player1Choice == player2Choice)
                player1Choice = vraimod(player1Choice + 1, 4);
            coolDownP1 = true;
        }
        
        if (Input.GetAxis("Player1Vertical") < -0.5f && !coolDownP1 && !P1Selected)
        {
            player1Choice = vraimod(player1Choice - 1, 4);
            if (player1Choice == player2Choice)
                player1Choice = vraimod(player1Choice - 1, 4);
            coolDownP1 = true;
        }

        if (Input.GetAxis("Player1Vertical") == 0)
            coolDownP1 = false;

        if (Input.GetAxis("Player2Vertical") > 0.5f && !coolDownP2 && !P2Selected)
        {
            player2Choice = vraimod(player2Choice + 1, 4);
            if (player2Choice == player1Choice)
                player2Choice = vraimod(player2Choice + 1, 4);
            coolDownP2 = true;
        }

        if (Input.GetAxis("Player2Vertical") < -0.5f && !coolDownP2 && !P2Selected)
        {
            player2Choice = vraimod(player2Choice - 1, 4);
            if (player2Choice == player1Choice)
                player2Choice = vraimod(player2Choice - 1, 4);
            coolDownP2 = true;
        }
        if (Input.GetAxis("Player2Vertical") == 0)
            coolDownP2 = false;

        if (Input.GetButtonDown("Player1Fire1") || Input.GetButtonDown("Player1Fire2") || Input.GetButtonDown("Player2Fire1") || Input.GetButtonDown("Player2Fire2"))
        {
            menuSounds[2].Play();
            credits++;
        }

        creditsText.text = "CREDITS : " + credits + " ( " + credits * 3 + " FLOORS ) - SPECIAL TO ADD COIN";
        
        if (Input.GetButtonDown("Player1Jump") && credits > 0)
        {
            menuSounds[0].Play();
            P1Selected = true;
        }
        else if (Input.GetButtonDown("Player1Jump") && credits <= 0)
        {
            menuSounds[1].Play();
        }

        if (Input.GetButtonDown("Player2Jump") && credits > 0)
        {
            menuSounds[0].Play();
            P2Selected = true;
        }
        else if (Input.GetButtonDown("Player1Jump") && credits <= 0)
        {
            menuSounds[1].Play();
        }
        
        foreach (Image t in player1)
            t.color = Color.black;
        foreach (Image t in player2)
            t.color = Color.black;
        
        player1[player1Choice].color = ( P1Selected == true ? Color.white : Color.white);
        player2[player2Choice].color = ( P2Selected == true ? Color.white : Color.white);


        if(P1Selected && P2Selected)
        {
            nombreEtages = credits * 3;
            player1Char = (Characters)player1Choice;
            player2Char = (Characters)player2Choice;
            SceneManager.LoadScene("MainGameLoop");
        }
    }
}
