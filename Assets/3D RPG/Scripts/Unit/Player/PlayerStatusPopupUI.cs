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
            damage.text = "���ݷ� : " + player.totalAttack.ToString();
            hp.text = "ü�� : " + player.current_HP + " / " + player.max_HP;
            mp.text = "���� : "+ player.current_MP + " / " + player.max_MP;
            coin.text = "���� : " +currency.coin.ToString();
        }
    }
}
