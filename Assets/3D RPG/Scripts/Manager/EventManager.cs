using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public event Action<ItemData> ChangeItemSlot;
    public event Action<int> UnEquipPreItem;
    private void Awake()
    {
        instance = this;
        PlayerData.Load();
    }
    public void ChangeEquipSlot(ItemData itemData)
    {
        ChangeItemSlot?.Invoke(itemData);
    }
    public void UnEquipItem(int itemData)
    {
        UnEquipPreItem?.Invoke(itemData);
    }
}
