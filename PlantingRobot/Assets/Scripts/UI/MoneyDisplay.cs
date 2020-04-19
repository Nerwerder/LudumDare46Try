using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public Text moneyText;
    protected PlayerRobot player = null;

    public void Start() {
        player = FindObjectOfType<PlayerRobot>();
    }
    public void Update() {
        moneyText.text = "MONEY: " + player.GetMoney();
    }
}
