using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class IngameItemSlots : MonoBehaviour
{
    public RectTransform slotsParent;
    ItemSlot[] ingameItemslots;

    private void Awake()
    {
        if(slotsParent ==null)
        {
            return;
        }
        ingameItemslots = GetChildItemSlots(slotsParent);
        GetSlotNumber();
    }
    private void Start()
    {
        SetData();
        ItemInventoryManager.instance.OnEquippedIngamePortionItemChanged += SetData;
    }
    ItemSlot[] GetChildItemSlots(RectTransform parent)
    {
        List<ItemSlot> slots = new List<ItemSlot>();
        for (int i = 0; i < parent.childCount; i++)
        {
            ItemSlot slot = parent.GetChild(i).GetComponent<ItemSlot>();
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener(() =>
            {
                EquipIngameItemSlot(slot);
            });
            slots.Add(slot);
        }
        return slots.ToArray();
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
    void SetData()
    {
        foreach (ItemSlot slot in ingameItemslots)
        {
            slot.ClearData();
        }
        for (int i = 0; i < ItemInventoryManager.instance.equipPortionItems.Count; i++)
        {
            ItemInstance instance = ItemInventoryManager.instance.equipPortionItems[i];
            ItemSlot slot = ingameItemslots[i];
            if (slot.IsEmpty() && instance.itemInfo != null)
            {
                slot.SetData(instance);
            }
        }
    }
    void GetSlotNumber()
    {
        for (int i = 0; i < ingameItemslots.Length; i++)
        {
            ingameItemslots[i].slotNumber = i;
        }
    }
}
