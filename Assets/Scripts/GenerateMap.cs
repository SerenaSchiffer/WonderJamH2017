using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMap : MonoBehaviour {
    public float baseX;
    public float baseY;
    public int towerSize;
    public int numberOfFloor;
    public GameObject[] tableFloor;
    public GameObject player1, player2;
    public int actualFloor = 0;
    public float test = 0f;
    public GameObject brickWall;
    public float targetImagey;
    float height;
    // Use this for initialization
    void Start () {
        height = 2f * Camera.main.orthographicSize;
        tableFloor = new GameObject[towerSize];

        for (int i = 0; i < towerSize;i++)
        {
            int randomFloor = Random.Range(1, numberOfFloor + 1);
            GameObject newFloor = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Levels/Level" + randomFloor));
            newFloor.transform.position = new Vector3(baseX, baseY+i*height, 0);
            tableFloor[i] = newFloor;
        }


        switch (CharacterSelect.player1Char)
        {
            case CharacterSelect.Characters.Batcroute:
                player1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Batman"));
                player1.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player1;
                player1.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
                player1.transform.position = player1.GetComponent<PlayableHero>().spawn.position;
                break;

            case CharacterSelect.Characters.JeanGras:
                player1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Jean"));
                player1.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player1;
                player1.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
                player1.transform.position = player1.GetComponent<PlayableHero>().spawn.position;
                break;

            case CharacterSelect.Characters.Sauceman:
                player1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Aquaman"));
                player1.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player1;
                player1.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
                player1.transform.position = player1.GetComponent<PlayableHero>().spawn.position;
                break;

            case CharacterSelect.Characters.Spidercheese:
                player1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_SpiderMan"));
                player1.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player1;
                player1.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
                player1.transform.position = player1.GetComponent<PlayableHero>().spawn.position;
                break;
        }


        switch (CharacterSelect.player2Char)
        {
            case CharacterSelect.Characters.Batcroute:
                player2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Batman"));
                player2.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player2;
                player2.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
                player2.transform.position = player2.GetComponent<PlayableHero>().spawn.position;
                break;

            case CharacterSelect.Characters.JeanGras:
                player2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Jean"));
                player2.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player2;
                player2.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
                player2.transform.position = player2.GetComponent<PlayableHero>().spawn.position;
                break;

            case CharacterSelect.Characters.Sauceman:
                player2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Aquaman"));
                player2.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player2;
                player2.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
                player2.transform.position = player2.GetComponent<PlayableHero>().spawn.position;
                break;

            case CharacterSelect.Characters.Spidercheese:
                player2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_SpiderMan"));
                player2.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player2;
                player2.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
                player2.transform.position = player2.GetComponent<PlayableHero>().spawn.position;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetUp();
        }
		if(test > 0)
        {
            brickWall.SetActive(true);
            test -= Time.deltaTime;
            float y = Mathf.MoveTowards(brickWall.transform.position.y, targetImagey, 25f);
            brickWall.transform.position = new Vector3(0, y, 0);
        }
        else
        {
            brickWall.transform.position = new Vector3(0, 0, 0);
            brickWall.SetActive(false);
        }
	}

    public void GetUp()
    {
        actualFloor++;
        player1.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
        player1.transform.position = player1.GetComponent<PlayableHero>().spawn.position;
        player2.GetComponent<PlayableHero>().spawn = tableFloor[actualFloor].transform.GetChild(1);
        player2.transform.position = player1.GetComponent<PlayableHero>().spawn.position;
        Camera.main.transform.Translate(Vector3.up * height);
        test = 3f;
    }

    public void DropWall()
    {

    }
}
