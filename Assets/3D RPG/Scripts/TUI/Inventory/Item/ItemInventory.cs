using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInventory: ItemIInventoryBase
{
    private void Awake()
    {
        SetData(PlayerData.instance);
    }
    private void OnEnable()
    {
        GameManager.instance.OnItemPurchased += OnItemPurchased;
    }
    private void OnDisable()
    {
        GameManager.instance.OnItemPurchased -= OnItemPurchased;
    }
    private void OnItemPurchased()
    {
        Refresh();
    }
    public override void SetData(PlayerData data)
    {
        playerData = data;
        playerItemData = playerData.playerItemInventoryData;//
        //playerItemData = GetItemInventoryDataByType(inventoryType);
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
                        Debug.Log("�κ��丮�� Ŭ�� ���� ����");
                    }
                });
            }
            slots.Add(slot);
        }
        return slots.ToArray();
    }
}


