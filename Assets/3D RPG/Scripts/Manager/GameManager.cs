using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Action OnItemPurchased;
    public event Action ChangeItemSlot;
    public event Action StartGachaPopup;
    public event Action SetSkillInventory;
    private void Awake()
    {
        instance = this;
        PlayerData.Load();
    }
    public void ItemPurchased()
    {
        OnItemPurchased?.Invoke();
    }
    public void ChangeSlot()
    {
        ChangeItemSlot?.Invoke();
    }
    public void StartGacha()
    {
        StartGachaPopup?.Invoke();
    }
    public void SetSkillI()
    {
        SetSkillInventory?.Invoke();
    }
}
