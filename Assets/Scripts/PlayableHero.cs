using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentPlayer
{
    Player1,
    Player2
}

public class PlayableHero : MonoBehaviour {

    public float speed = 5f;
    public float jumpHeight = 50f;

    public CurrentPlayer currentPlayer;

    Rigidbody2D rgb;
    bool isJumping;

    public virtual void Awake()
    {

    }

	// Use this for initialization
	public void Start () {
        rgb = GetComponent<Rigidbody2D>();
        isJumping = false;
	}
	
	// Update is called once per frame
	public void Update () {
		if (Input.GetAxis(currentPlayer.ToString() + "Horizontal") != 0)
            rgb.velocity = new Vector2(Input.GetAxis(currentPlayer.ToString() + "Horizontal") * speed, rgb.velocity.y);

        if (Input.GetButtonDown(currentPlayer.ToString() + "Jump"))
            rgb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Force);

        if (Input.GetButtonDown(currentPlayer.ToString() + "Fire1"))
            Debug.Log("Power" + currentPlayer.ToString());
	}

    protected virtual void Spell1()
    {

    }
}
