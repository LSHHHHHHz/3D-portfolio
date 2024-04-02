using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCurrency : MonoBehaviour
{
    public static PlayerCurrency Instance;
    public Text coinText;
    public Text diamondText;
    public int coin;
    public int diamond;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        coinText.text = coin.ToString();
        diamondText.text= diamond.ToString();
    }
}
