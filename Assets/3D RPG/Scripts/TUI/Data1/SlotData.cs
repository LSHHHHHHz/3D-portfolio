using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SlotDatanitialize 
{
    protected void InitializeSlots(ref List<SlotData> slots, int count)
    {
        slots = new List<SlotData>(count);
        for (int i = 0; i < count; i++)
        {
            slots.Add(new SlotData());
        }
    }
}
[System.Serializable]
public class SlotData
{
    public ItemData item;
    public int count;
    public int maxCount = 99;
    public void AddItem(ItemData newItem, int newCount)
    {
        if (newItem == null || newCount <= 0)
        {
            return;
        }

        if (item == null || item != newItem)
        {
            item = newItem;
            count = newCount;
        }
        else
        {
            count += newCount;
            if (count > maxCount)
            {
                return;
            }
        }
    }
    public void RemoveItem()
    {
        item = null;
        count = 0;
    }
    public ItemData GetItem()
    {
        return item;
    }
}
[System.Serializable]
public class QuickPortionSlotData : SlotDatanitialize, IData
{
    public List<SlotData> slotDatas;
    public QuickPortionSlotData()
    {
        InitializeSlots(ref slotDatas, UserDataConstants.QUICKPORTIONSLOTSDATA_COUNT);
    }
}
[System.Serializable]
public class QuickSkillSlotData : SlotDatanitialize, IData
{
    public List<SlotData> slotDatas;
    public QuickSkillSlotData()
    {
        InitializeSlots(ref slotDatas, UserDataConstants.QUICKSKILLSLOTDATA_COUNT);
    }
}
[System.Serializable]
public class InventoryData : SlotDatanitialize, IData
{
    public List<SlotData> slotDatas;
    public InventoryData()
    {
        InitializeSlots(ref slotDatas, 28);
    }
}
[System.Serializable]
public class SkillInventoryData : SlotDatanitialize, IData
{
    public List<SlotData> slotDatas;
    public SkillInventoryData()
    {
        InitializeSlots(ref slotDatas, 8);
    }
}
[System.Serializable]
public class EquipmentData : SlotDatanitialize, IData
{
    public List<SlotData> slotDatas;
    public EquipmentData()
    {
        InitializeSlots(ref slotDatas, 2);
    }
}
[System.Serializable]
public class ShopData : SlotDatanitialize, IData
{
    public List<SlotData> portionShopSlotDatas;
    public List<SlotData> equipShopSlotData;
    public ShopData()
    {
        InitializeSlots(ref portionShopSlotDatas, 8);
        InitializeSlots(ref equipShopSlotData, 8);
    }
    public List<SlotData> GetSlotsByNumber(int shopNumber)
    {
        if (shopNumber == 1)
        {
            return portionShopSlotDatas;
        }
        else if (shopNumber == 2)
        {
            return equipShopSlotData;
        }
        else
        {
            return null;
        }
    }
}