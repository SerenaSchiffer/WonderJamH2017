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
    public float test = 1f;
    public Image brique;
    float baseImagey;
    float height;
    // Use this for initialization
    void Start () {
        baseImagey = brique.transform.position.y;
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
                player1.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
                player1.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player1;
                break;

            case CharacterSelect.Characters.JeanGras:
                player1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Jean"));
                player1.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
                player1.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player1;
                break;

            case CharacterSelect.Characters.Sauceman:
                player1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Aquaman"));
                player1.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
                player1.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player1;
                break;

            case CharacterSelect.Characters.Spidercheese:
                player1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_SpiderMan"));
                player1.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
                player1.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player1;
                break;
        }


        switch (CharacterSelect.player2Char)
        {
            case CharacterSelect.Characters.Batcroute:
                player2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Batman"));
                player2.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
                player2.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player2;
                break;

            case CharacterSelect.Characters.JeanGras:
                player2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Jean"));
                player2.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
                player2.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player2;
                break;

            case CharacterSelect.Characters.Sauceman:
                player2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_Aquaman"));
                player2.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
                player2.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player2;
                break;

            case CharacterSelect.Characters.Spidercheese:
                player2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Char_SpiderMan"));
                player2.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
                player2.GetComponent<PlayableHero>().currentPlayer = CurrentPlayer.Player2;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(test > 0)
        {
            
            test -= Time.deltaTime;
            float y = Mathf.MoveTowards(brique.transform.position.y, -baseImagey, 40f);
            brique.transform.position = new Vector3(445, y, 0);
        }
        else
        {
            test = 1f;
            GetUp();
        }
	}

    public void GetUp()
    {
        actualFloor++;
        player1.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
        player2.transform.position = tableFloor[actualFloor].transform.GetChild(1).position;
        Camera.main.transform.Translate(Vector3.up * height);
    }
}
