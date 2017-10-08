using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
	void Update () {
        if (Input.GetButtonDown("Player1Jump") || Input.GetButtonDown("Player2Jump"))
            SceneManager.LoadScene("CharSelect");
	}
}
