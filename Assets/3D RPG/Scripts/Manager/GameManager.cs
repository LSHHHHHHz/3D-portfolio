using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Action OnItemPurchased;
    private void Awake()
    {
        instance = this;
        PlayerData.Load();
    }
    public void ItemPurchased()
    {
        OnItemPurchased?.Invoke();
    }
}
