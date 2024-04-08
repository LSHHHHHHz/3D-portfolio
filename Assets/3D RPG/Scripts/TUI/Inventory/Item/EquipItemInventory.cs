using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipItemInventory : ItemIInventoryBase
{
    private void Awake()
    {
        SetData(PlayerData.instance);
    }
    public override void SetData(PlayerData data)
    {
        playerData = data;
        playerItemData = GetItemInventoryDataByType(inventoryType);
        itemInventorySlots = GetChildItemSlots(inventorySlotGrid);
        InitializeItemInventoryData(inventoryType, itemInventorySlots.Length);
        Refresh();
    }
    public override ItemSlot[] GetChildItemSlots(RectTransform parent)
    {
        List<ItemSlot> slots = new List<ItemSlot>();
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform imageTransform = parent.GetChild(i);
            ItemSlot slot = imageTransform.GetComponentInChildren<ItemSlot>();
            if (slot != null)
            {
                Button slotButton = slot.GetComponent<Button>();
                slotButton.onClick.AddListener(() =>
                {
                    if (slot.itemInfo != null)
                    {
                        Debug.Log("장착은 클릭 구현 안함");
                    }
                });
            }
            slots.Add(slot);
        }
        return slots.ToArray();
    }
}


