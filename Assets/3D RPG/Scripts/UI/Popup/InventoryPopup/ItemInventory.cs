using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    public RectTransform slotsParent;
    ItemSlot[] itemInventorySlots;

    private void Awake()
    {
        itemInventorySlots = GetChildItemSlots(slotsParent);
        GetSlotNumber();
    }
    private void Start()
    {
        SetData();
        ItemInventoryManager.instance.OnItemInventoryChanged += SetData; 
    }

    ItemSlot[] GetChildItemSlots(RectTransform parent)
    {
        List<ItemSlot> slots = new List<ItemSlot>();
        for(int i =0; i< parent.childCount; i++)
        {
            ItemSlot slot = parent.GetChild(i).GetComponent<ItemSlot>();
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener(() => 
            {
                ItemInventoryManager.instance.SelectedItemInfoData(slot.itemInfo, PopupType.ItemInventory);
            });
            slots.Add(slot); 
        }
        return slots.ToArray();
    }
    void SetData()
    {
        foreach (ItemSlot slot in itemInventorySlots)
        {
            slot.ClearData();
        }
        for(int i=0; i<ItemInventoryManager.instance.myItems.Count; i++)
        {
            ItemInstance instance = ItemInventoryManager.instance.myItems[i];
            ItemSlot slot = itemInventorySlots[i];
            if(slot.IsEmpty())
            {
                slot.SetData(instance);
            }
        }
    }

    void GetSlotNumber()
    {
        for(int i =0; i< itemInventorySlots.Length; i++)
        {
            itemInventorySlots[i].slotNumber = i;
        }
    }
}
