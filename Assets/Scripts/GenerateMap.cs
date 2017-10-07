using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {
    public float baseXP1;
    public float baseXP2;
    public float baseY;
    public float ySize;
    public int towerSize;
    public int numberOfFloor;
	// Use this for initialization
	void Start () {
		for(int i = 0; i <= towerSize;i++)
        {
            int randomFloor = Random.Range(1, numberOfFloor + 1);
            GameObject newFloor = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Floor" + randomFloor));
            GameObject newFloor2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Floor" + randomFloor));
            newFloor.transform.position = new Vector2(baseXP1, baseY + ySize * i);
            newFloor2.transform.position = new Vector2(baseXP2, baseY + ySize * i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
