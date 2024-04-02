using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInventoryData
{
    public List<ItemInstance> myItems = new();
    public List<ItemInstance> equipItems = new();
}
public class ItemInventoryManager : MonoBehaviour
{
    public static ItemInventoryManager instance;
    public ItemInfo selectedShopItemInfoData;
    public ItemInfo selectedInventoryItemInfoData;

    public event Action OnItemInventoryChanged;
    public event Action OnEquippedItemChanged;
    public event Action<ItemInfo> actionSelectedShopItemInfoData;
    public event Action<ItemInfo> actionSelectedInventoryItemInfoData;

    public List<ItemInstance> myItems = new();
    public List<ItemInstance> equipItems = new();
    public void Awake()
    {
        instance = this;

    }
    public void AddItem(ItemInfo itemInfo)
    {
        ItemInstance existItem = myItems.Find(item => item.itemInfo == itemInfo);
        if(existItem != null)
        {
            existItem.count++;
        }
        else
        {
            myItems.Add(new ItemInstance()
            {
                itemInfo = itemInfo,
                count = 1,
            });
        }
        OnItemInventoryChanged?.Invoke();
    }
    public void AddItem(ItemInfo itemInfo, int count)
    {
        bool added = false;
        for (int i = 0; i < myItems.Count; i++)
        {
            if (myItems[i] == null) 
            {
                myItems[i] = new ItemInstance 
                {
                    itemInfo = itemInfo,
                    count = count
                };
                added = true;
                break; 
            }
        }

        if (!added)
        {
            myItems.Add(new ItemInstance
            {
                itemInfo = itemInfo,
                count = count
            });
        }

        OnItemInventoryChanged?.Invoke();
    }

    public void MergeItems(ItemInfo targetItemInfo, ItemInfo dragItemInfo)
    {
        ItemInstance targetItem = myItems.Find(item => item.itemInfo == targetItemInfo);
        ItemInstance sourceItem = myItems.Find(item => item.itemInfo == dragItemInfo);

        if (targetItem != null && sourceItem != null)
        {
            targetItem.count += sourceItem.count;

            myItems.Remove(sourceItem);
            OnItemInventoryChanged?.Invoke();
        }
    }
    public void MergeItems(ItemSlot dropItemSlot, ItemSlot dragItemSlot)
    {
        ItemInstance dropItem = null;
        ItemInstance dragItem = null;
        for (int i =0; i< myItems.Count; i++)
        {
            if(i == dropItemSlot.slotNumber)
            {
                dropItem = myItems[i];
            }
            if(i ==dragItemSlot.slotNumber)
            {
                dragItem = myItems[i];
            }
        }
        dropItem.count += dragItem.count;
        myItems[dragItemSlot.slotNumber] = null;
        OnItemInventoryChanged?.Invoke();
    }
    public void SelectedItemInfoData(ItemInfo info, PopupType type)
    {
        switch(type)
        {
            case PopupType.ItemShop:
                selectedShopItemInfoData = info;
                actionSelectedShopItemInfoData?.Invoke(info);
                break;
            case PopupType.ItemInventory:
                selectedInventoryItemInfoData = info;
                actionSelectedInventoryItemInfoData?.Invoke(info);
                break;
        }
    }
}
