using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public Text moneyText;
    protected PlayerRobot player = null;

    private float timer;

    public void Start() {
        player = FindObjectOfType<PlayerRobot>();
    }
    public void Update() {
        timer += Time.deltaTime;
        moneyText.text = "MONEY: " + player.GetMoney() + "\n" + "TIME: " + (int)(timer / 60);
    }
}
