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


    public Text[] player1;
    public Text[] player2;
    public Text creditsText;

    bool coolDownP1;
    bool coolDownP2;
    bool P1Selected;
    bool P2Selected;
    int player1Choice = 0;
    int player2Choice = 1;
    int credits = 0;

	// Use this for initialization
	void Start () {
        coolDownP1 = false;
        coolDownP2 = false;
        P1Selected = false;
        P2Selected = false;
	}

    int vraimod(float a, float b)
    {
        return (int)Mathf.Floor(a - b * Mathf.Floor(a / b));
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Player1Horizontal") > 0.5f && !coolDownP1 && !P1Selected)
        {
            player1Choice = vraimod( player1Choice + 1, 4 );
            if (player1Choice == player2Choice)
                player1Choice = vraimod(player1Choice + 1, 4);
            coolDownP1 = true;
        }
        
        if (Input.GetAxis("Player1Horizontal") < -0.5f && !coolDownP1 && !P1Selected)
        {
            player1Choice = vraimod(player1Choice - 1, 4);
            if (player1Choice == player2Choice)
                player1Choice = vraimod(player1Choice - 1, 4);
            coolDownP1 = true;
        }

        if (Input.GetAxis("Player1Horizontal") == 0)
            coolDownP1 = false;

        if (Input.GetAxis("Player2Horizontal") > 0.5f && !coolDownP2 && !P2Selected)
        {
            player2Choice = vraimod(player2Choice + 1, 4);
            if (player2Choice == player1Choice)
                player2Choice = vraimod(player2Choice + 1, 4);
            coolDownP2 = true;
        }

        if (Input.GetAxis("Player2Horizontal") < -0.5f && !coolDownP2 && !P2Selected)
        {
            player2Choice = vraimod(player2Choice - 1, 4);
            if (player2Choice == player1Choice)
                player2Choice = vraimod(player2Choice - 1, 4);
            coolDownP2 = true;
        }
        if (Input.GetAxis("Player2Horizontal") == 0)
            coolDownP2 = false;

        if (Input.GetButtonDown("Player1Fire1") || Input.GetButtonDown("Player1Fire2") || Input.GetButtonDown("Player2Fire1") || Input.GetButtonDown("Player2Fire2"))
            credits++;

        creditsText.text = "CREDITS : " + credits + " ( " + credits * 3 + " FLOORS ) - SPECIAL TO ADD COIN";
        
        if (Input.GetButtonDown("Player1Jump") && credits > 0)
            P1Selected = true;

        if (Input.GetButtonDown("Player2Jump") && credits > 0)
            P2Selected = true;
        
        foreach (Text t in player1)
            t.color = Color.white;
        foreach (Text t in player2)
            t.color = Color.white;
        
        player1[player1Choice].color = ( P1Selected == true ? Color.cyan : Color.yellow);
        player2[player2Choice].color = ( P2Selected == true ? Color.cyan : Color.yellow);
        player1[player2Choice].color = Color.grey;
        player2[player1Choice].color = Color.grey;


        if(P1Selected && P2Selected)
        {
            nombreEtages = credits * 3;
            player1Char = (Characters)player1Choice;
            player2Char = (Characters)player2Choice;
            SceneManager.LoadScene("MainGameLoop");
        }
    }
}
