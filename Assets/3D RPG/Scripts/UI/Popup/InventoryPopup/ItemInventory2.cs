using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemInventory2 : UIInventory
{
    public RectTransform slotsParent;
    public ItemSlot[] itemSlots;

    public bool dataDirty = false;
    private void Awake()
    {
        itemSlots = GetChildItemSlots(slotsParent);
        SetData(PlayerDataSave.instance, DragAndDropData.instance);
    }
    private void Start()
    {        
        DragAndDropData.instance.startRefresh += Refresh; 
    }
    public override void Refresh()
    {
        if (playerData != null && playerData.inventoryData != null)
        {
            for(int i =0; i< itemSlots.Length; i++)
            {
                ItemSlot slot = itemSlots[i].GetComponentInChildren<ItemSlot>();
                slot.slotNumber = i;
                if (i < playerData.inventoryData.items.Count)
                {                    
                    if (slot != null)
                    {
                        slot.GetComponent<ItemSlot>().ClearData();
                        if (playerData.inventoryData.items[i] != null)
                        {
                            slot.GetComponent<ItemSlot>().SetData(playerData.inventoryData.items[i].itemInfo);
                            slot.GetComponent<ItemSlot>().SetData(playerData.inventoryData.items[i]);

                        }
                    }
                }
            }
        }
    }
    ItemSlot[] GetChildItemSlots(RectTransform parent)
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
                        SelectedSlotInfoManager.instance.SetItemInfo(slot.itemInfo, slot.slotNumber);
                        EquipSlotIndicateManager.instance.SetIngameSlotIndicate(slot.itemInfo.itemSort);
                    }
                });
            }
            slots.Add(slot);
        }
        return slots.ToArray();
    }
    private void OnDragSlot(int slotIndex)
    {
        DragAndDropData.instance.SetDragData(playerData.inventoryData.GetItem(slotIndex), slotIndex, InventoryType.ItemInventory);
    }
    private void EndDragSlot(int slotIndex)
    {
       
    }
    private void OnDropSlot(int slotIndex)
    {
        if (DragAndDropData.instance.draggedItem == null)
        {
            return;
        }
        if (DragAndDropData.instance.draggedItem != null)
        {
            DragAndDropData.instance.SetDropData(playerData.inventoryData.GetItem(slotIndex), slotIndex, InventoryType.ItemInventory);
        }
        MergeSlot();
        ChangeSlot();
    }
    void MergeSlot()
    {
        if (DragAndDropData.instance.dropedItem != null && DragAndDropData.instance.draggedItem.itemInfo == DragAndDropData.instance.dropedItem.itemInfo)
        {
            switch (DragAndDropData.instance.draggedItemSlotParentType)
            {
                case InventoryType.ItemInventory:
                    playerData.inventoryData.MergeItem(DragAndDropData.instance.draggedItemSlotNumber, DragAndDropData.instance.dropedItemSlotNumber);
                    break;
                case InventoryType.InGamePortionInventory:
                    playerData.ingamePortionData.RemoveItem(DragAndDropData.instance.draggedItemSlotNumber);
                    playerData.inventoryData.MergeOtherInventoryItem(DragAndDropData.instance.draggedItem, DragAndDropData.instance.dropedItemSlotNumber);
                    break;
                case InventoryType.ItemEquipInventory:
                    playerData.equipmentData.RemoveItem(DragAndDropData.instance.draggedItemSlotNumber);
                    playerData.inventoryData.MergeOtherInventoryItem(DragAndDropData.instance.draggedItem, DragAndDropData.instance.dropedItemSlotNumber);
                    break;
            }
            DragAndDropData.instance.draggedItem = null;
            Refresh();
        }
    }
    void ChangeSlot()
    {
        if (DragAndDropData.instance.draggedItem != null)
        {
            switch (DragAndDropData.instance.draggedItemSlotParentType)
            {
                case InventoryType.ItemInventory:
                    playerData.inventoryData.ChangeItem(DragAndDropData.instance.draggedItemSlotNumber, DragAndDropData.instance.dropedItemSlotNumber);
                    break;
                case InventoryType.InGamePortionInventory:
                    playerData.ingamePortionData.SetItem(DragAndDropData.instance.dropedItem, DragAndDropData.instance.draggedItemSlotNumber);
                    playerData.inventoryData.SetItem(DragAndDropData.instance.draggedItem, DragAndDropData.instance.draggedItemSlotNumber);
                    break;
                case InventoryType.ItemEquipInventory:
                    playerData.equipmentData.SetItem(DragAndDropData.instance.dropedItem, DragAndDropData.instance.draggedItemSlotNumber);
                    playerData.inventoryData.SetItem(DragAndDropData.instance.draggedItem, DragAndDropData.instance.draggedItemSlotNumber);
                    break;
            }
            Refresh();
        }
    }
}
