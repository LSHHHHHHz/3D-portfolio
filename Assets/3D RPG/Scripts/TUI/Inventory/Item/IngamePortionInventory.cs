using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IngamePortionInventory : ItemIInventoryBase 
{
    Action ConsumptionPortionIngame;

    private void Awake()
    {
        SetData(PlayerData.instance);
        ConsumptionPortionIngame += Refresh;
    }
    public override void SetData(PlayerData data)
    {
        playerData = data;
        playerItemData = playerData.playerIngamePortionData;//
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
                // 클로저 문제를 해결하기 위해 i 값을 복사
                int slotIndex = i;
                slot.clickButton += () => ConsumptionPortion(slotIndex);
                slot.beingDragSlot += () => SelectDragData(slotIndex);
                //  slot.endDragSlot += () => EndDragSlot(slotIndex);
                slot.OnDropSlot += () => SelectDropData(slotIndex);
            }
            slots.Add(slot);
        }
        return slots.ToArray();
    }
    public void SelectDragData(int num)
    {
        DragAndDropManager.instance.dragData = playerItemData[num];
    }
    public void SelectDropData(int num)
    {
        DragAndDropManager.instance.dropData = playerItemData[num];
    }
    public void ConsumptionPortion(int num)
    {
        var itemInstance = playerItemData[num].GetData() as ItemInstance;
        if (playerItemData[num].GetData()== null)
        {
            return;
        }
        if (itemInstance.itemInfo != null)
        {
            playerItemData[num].RemoveData(1);
            if (itemInstance.count <= 0)
            {
                playerItemData[num].SetData(null);
            }
            ConsumptionPortionIngame?.Invoke();
        }
    }
}


