using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class IngameItemSlotsInventory : UIInventory
{
    public RectTransform slotsParent;
    ItemSlot[] ingameItemslots;

    private void Awake()
    {
        ingameItemslots = GetChildItemSlots(slotsParent);
        SetData(PlayerDataSave.instance, DragAndDropData.instance);
    }
    private void Start()
    {
        DragAndDropData.instance.startRefresh += Refresh; 
    }
    public override void Refresh()
    {
        if (playerData != null && playerData.ingamePortionData != null)
        {
            for (int i = 0; i < ingameItemslots.Length; i++)
            {
                ItemSlot slot = ingameItemslots[i].GetComponentInChildren<ItemSlot>();
                slot.slotNumber = i;
                if (i < playerData.ingamePortionData.ingamePortionItems.Count)
                {
                    if (slot != null)
                    {
                        slot.GetComponent<ItemSlot>().ClearData();
                        if (playerData.ingamePortionData.ingamePortionItems[i] != null)
                        {
                            slot.GetComponent<ItemSlot>().SetData(playerData.ingamePortionData.ingamePortionItems[i].itemInfo);
                            slot.GetComponent<ItemSlot>().SetData(playerData.ingamePortionData.ingamePortionItems[i]);

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
                        EquipSlotIndicateManager.instance.SetIngameSlotIndicate(slot.itemInfo.itemType);
                    }
                });
            }
            slots.Add(slot);
        }
        return slots.ToArray();
    }
    private void OnDragSlot(int slotIndex)
    {
        DragAndDropData.instance.SetDragData(playerData.ingamePortionData.GetItem(slotIndex), slotIndex, InventoryType.InGamePortionInventory);
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
            DragAndDropData.instance.SetDropData(playerData.ingamePortionData.GetItem(slotIndex), slotIndex, InventoryType.InGamePortionInventory);
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
                    playerData.inventoryData.RemoveItem(DragAndDropData.instance.draggedItemSlotNumber);
                    playerData.ingamePortionData.MergeOtherInventoryItem(DragAndDropData.instance.draggedItem, DragAndDropData.instance.dropedItemSlotNumber);
                    break;
                case InventoryType.InGamePortionInventory:

                    playerData.ingamePortionData.MergeItem(DragAndDropData.instance.draggedItemSlotNumber, DragAndDropData.instance.dropedItemSlotNumber);
                    break;
                case InventoryType.ItemEquipInventory:
                    playerData.equipmentData.RemoveItem(DragAndDropData.instance.draggedItemSlotNumber);
                    playerData.ingamePortionData.MergeOtherInventoryItem(DragAndDropData.instance.draggedItem, DragAndDropData.instance.dropedItemSlotNumber);
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
                    playerData.inventoryData.SetItem(DragAndDropData.instance.dropedItem, DragAndDropData.instance.draggedItemSlotNumber);
                    playerData.ingamePortionData.SetItem(DragAndDropData.instance.draggedItem, DragAndDropData.instance.draggedItemSlotNumber);
                    break;
                case InventoryType.InGamePortionInventory:
                    playerData.ingamePortionData.ChangeItem(DragAndDropData.instance.draggedItemSlotNumber, DragAndDropData.instance.dropedItemSlotNumber);
                    break;
                case InventoryType.ItemEquipInventory:
                    playerData.equipmentData.SetItem(DragAndDropData.instance.dropedItem, DragAndDropData.instance.draggedItemSlotNumber);
                    playerData.ingamePortionData.SetItem(DragAndDropData.instance.draggedItem, DragAndDropData.instance.draggedItemSlotNumber);
                    break;
            }
            Refresh();
        }
    }

    void EquipIngameItemSlot(ItemSlot slot)
    {
        if (SelectedSlotInfoManager.instance.selectedItemInfo != null)
        {
            bool matchFound = false;
            for (int i = 0; i < ingameItemslots.Length; i++)
            {
                if (ingameItemslots[i].itemInfo == SelectedSlotInfoManager.instance.selectedItemInfo)
                {
                    matchFound = true;
                    break;
                }
            }
            if (!matchFound)
            {
                ItemInventoryManager.instance.SetEquipPortion(SelectedSlotInfoManager.instance.selectedItemInfo, slot.slotNumber, SelectedSlotInfoManager.instance.selectedItemInfoNum);
                //slot.SetData(GameManager.instance.selectedSkillInfo);
            }
            EquipSlotIndicateManager.instance.ReverseSetIngameSlotIndicate(SelectedSlotInfoManager.instance.selectedItemInfo.itemType);
            SelectedSlotInfoManager.instance.ClearItemInfo();
        }
    }
}
