using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class ItemIInventoryBase : MonoBehaviour
{
    public PlayerData playerData;
    public IPlayerData[] playerItemData;
    public InventoryType inventoryType;

    public RectTransform inventorySlotGrid;
    public ItemSlot[] itemInventorySlots;

    public abstract void SetData(PlayerData data);
    public abstract ItemSlot[] GetChildItemSlots(RectTransform parent);
    public void Refresh()
    {
        if (playerData != null )
        {
            for (int i = 0; i < itemInventorySlots.Length; i++)
            {
                ItemSlot slot = itemInventorySlots[i].GetComponentInChildren<ItemSlot>();
                slot.slotNumber = i;
                if (i < playerItemData.Length)
                {
                    if (slot != null)
                    {
                        slot.GetComponent<ItemSlot>().ClearData();
                        if (playerItemData[i].GetData() != null)
                        {
                            var itemInstance = playerItemData[i].GetData() as ItemInstance;
                            slot.GetComponent<ItemSlot>().SetData(itemInstance.itemInfo);
                            slot.GetComponent<ItemSlot>().SetData(itemInstance);

                        }
                    }
                }
            }
        }
    }
    public void InitializeItemInventoryData(InventoryType type, int inventorySize)
    {
        switch (type)
        {
            case InventoryType.ItemInventory:
                for (int i = 0; i < playerItemData.Length; i++)
                {
                    playerItemData[i] = new PlayerItemInventoryData();
                }
                break;
            case InventoryType.ItemEquipInventory:
                for (int i = 0; i < playerItemData.Length; i++)
                {
                    playerItemData[i] = new PlayerEquipData();
                }
                break;
            case InventoryType.InGamePortionInventory:
                for (int i = 0; i < playerItemData.Length; i++)
                {
                    playerItemData[i] = new PlayerIngamePortionData();
                }
                break;
            default:
                Debug.LogError("없는 타입");
                break;
        }
    }
    public void InitializeItemInventoryData2(InventoryType type , int inventorySize)
    {
        switch (type)
        {
            case InventoryType.ItemInventory:
                playerItemData = new PlayerItemInventoryData[inventorySize];
                for (int i = 0; i < inventorySize; i++)
                {
                    playerItemData[i] = new PlayerItemInventoryData();
                }
                break;
            case InventoryType.ItemEquipInventory:
                playerItemData = new PlayerEquipData[inventorySize];
                for (int i = 0; i < inventorySize; i++)
                {
                    playerItemData[i] = new PlayerEquipData();
                }
                break;
            case InventoryType.InGamePortionInventory:
                playerItemData = new PlayerIngamePortionData[inventorySize];
                for (int i = 0; i < inventorySize; i++)
                {
                    playerItemData[i] = new PlayerIngamePortionData();
                }
                break;
            default:
                Debug.LogError("없는 타입");
                break;
        }
    }
    public IPlayerData[] GetItemInventoryDataByType(InventoryType type)
    {
        switch (type)
        {
            case InventoryType.ItemInventory:
                return playerData.playerItemInventoryData;
            case InventoryType.ItemEquipInventory:
                return playerData.playerEquipData;
            case InventoryType.InGamePortionInventory:
                return playerData.playerIngamePortionData;
            default:
                Debug.LogError("없는 타입");
                return null;
        }
    }
}


