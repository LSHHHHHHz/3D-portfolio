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
    private void OnEnable()
    {
        GameManager.instance.ChangeItemSlot += RefreshData;
    }
    private void OnDisable()
    {
        GameManager.instance.ChangeItemSlot -= RefreshData;
    }
    private void RefreshData()
    {
        Refresh();
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
                int slotIndex = i;
                Button slotButton = slot.GetComponent<Button>();
                slot.beingDragSlot += () => SelectDragData(slotIndex);
                //  slot.endDragSlot += () => EndDragSlot(slotIndex);
                slot.OnDropSlot += () => SelectDropData(slotIndex);
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
    public void SelectDragData(int num)
    {
        DragAndDropManager.instance.dragData = playerItemData[num];
        DragAndDropManager.instance.dragInventoryType = InventoryType.ItemEquipInventory;
    }
    public void SelectDropData(int num)
    {
        DragAndDropManager.instance.dropData = playerItemData[num];
        DragAndDropManager.instance.dropInventoryType = InventoryType.ItemEquipInventory;
    }
}


