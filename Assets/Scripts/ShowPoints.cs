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

    void Update()
    {
        ui_money_player1.text = "PLAYER 1 : " + Money.moneyP1 + "$";
        ui_money_player2.text = "PLAYER 2 : " + Money.moneyP2 + "$";
    }
}
