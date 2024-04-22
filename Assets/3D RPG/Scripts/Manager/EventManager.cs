using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public event Action<ItemData> changeItemSlot;
    public event Action<int> unEquipPreItem;
    public event Action<int,int> cameraTarget;
    private void Awake()
    {
        instance = this;
        PlayerData.Load();
    }
    public void ChangeEquipSlot(ItemData itemData)
    {
        changeItemSlot?.Invoke(itemData);
    }
    public void UnEquipItem(int itemData)
    {
        unEquipPreItem?.Invoke(itemData);
    }
    public void ChangeCameraTarget(int y ,int z)
    {
        cameraTarget?.Invoke(y,z);
    }
}
