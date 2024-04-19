using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerStatusPopupUI : MonoBehaviour
{
    public Text damage;
    public Text hp;
    public Text mp;
    public Text coin;

    private void Update()
    {
        PlayerStatus player = UnitManager.instance.player.playerStatus;
        PlayerCurrency currency = PlayerCurrency.Instance;
        if (player != null)
        {
            damage.text = "공격력 : " + player.totalAttack.ToString();
            hp.text = "체력 : " + player.current_HP + " / " + player.max_HP;
            mp.text = "마력 : "+ player.current_MP + " / " + player.max_MP;
            coin.text = "코인 : " +currency.coin.ToString();
        }
    }
}
