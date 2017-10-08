using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class IntroScreen : MonoBehaviour {
    enum moment
    {
        unfade,
        stay,
        fade,
        end
    }



    public GameObject blackScreen;
    public GameObject theText;
    private SpriteRenderer FadePannel;
    private moment currentMoment;

    private int secondForStay;
    public int secondForStayLimit;

    private float fadeRate = 0.01f;

	// Use this for initialization
	void Start () {
        currentMoment = moment.unfade;
        FadePannel = blackScreen.GetComponent<SpriteRenderer>();
        secondForStay = 8;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Player1Fire1") || Input.GetButtonDown("Player1Fire2") || Input.GetButtonDown("Player2Fire1") || Input.GetButtonDown("Player2Fire2"))
            changeScene();
    }

    void Fade()
    {
        FadePannel.color = new Color(FadePannel.color.r, FadePannel.color.g, FadePannel.color.b, FadePannel.color.a - fadeRate);
        Debug.Log(FadePannel.color.a);
        //theText.GetComponent<Text>().color = new Color(theText.GetComponent<Text>().color.r, theText.GetComponent<Text>().color.g, theText.GetComponent<Text>().color.b, theText.GetComponent<Text>().color.a - fadeRate);
        if (FadePannel.color.a <= 0.03)
        {
            CancelInvoke("Fade");
            currentMoment = moment.stay;
        }
    }

    void Stay()
    {
        Debug.Log(secondForStay);
        secondForStay++;
        if(secondForStay == secondForStayLimit)
        {
            CancelInvoke("Stay");
            currentMoment = moment.fade;
        }
    }

    void Unfade()
    {
        FadePannel.color = new Color(FadePannel.color.r, FadePannel.color.g, FadePannel.color.b, FadePannel.color.a + fadeRate);
        //theText.GetComponent<Text>().color = new Color(theText.GetComponent<Text>().color.r, theText.GetComponent<Text>().color.g, theText.GetComponent<Text>().color.b, theText.GetComponent<Text>().color.a - fadeRate);
        if (FadePannel.color.a >= 0.97)
        {
            CancelInvoke("Fade");
            currentMoment = moment.stay;
        }
    }

    void changeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharSelect");
    }
}
