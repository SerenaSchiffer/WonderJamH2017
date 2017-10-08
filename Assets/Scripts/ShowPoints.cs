using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money
{
    public static float moneyP1 = 0.00f;
    public static float moneyP2 = 0.00f;
}

public class ShowPoints : MonoBehaviour {
    public Text ui_money_player1;
    public Text ui_money_player2;
    public Text ui_current_floor;
    public Text ui_power_cooldown_p1;
    public Text ui_power_cooldown_p2;

    private PlayableHero player1;
    private PlayableHero player2;

    private void Start()
    {
    }

    void Update()
    {
        ui_money_player1.text = "P1      " + Money.moneyP1 + "$";
        ui_money_player2.text = "P2      " + Money.moneyP2 + "$";
        ui_current_floor.text = "FLOOR " + (GenerateMap.actualFloor+1);
    }
}
