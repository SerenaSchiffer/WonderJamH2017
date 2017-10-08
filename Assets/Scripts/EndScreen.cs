using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour {

    public GameObject[] characters;
    public Text textScore;
	// Use this for initialization
	void Start () {
        if (Money.moneyP1 > Money.moneyP2)
            characters[(int)CharacterSelect.player1Char].SetActive(true);
        else characters[(int)CharacterSelect.player2Char].SetActive(true);

        textScore.text = Money.moneyP1 + "$ vs "+ Money.moneyP2 +"$";
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Player1Fire1") || Input.GetButtonDown("Player1Fire2") || Input.GetButtonDown("Player2Fire1") || Input.GetButtonDown("Player2Fire2"))
            SceneManager.LoadScene("title");
	}
}
