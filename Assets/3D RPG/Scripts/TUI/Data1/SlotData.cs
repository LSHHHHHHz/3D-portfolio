using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlotDatanitialize : IData
{
    protected void InitializeSlots(List<SlotData> slots, int count)
    {
        slots = new List<SlotData>(count);
        for (int i = 0; i < count; i++)
        {
            slots.Add(new SlotData());
        }
    }
}
public class SlotData 
{
    public ItemData item;
    public int count;
  
    public void AddItem(ItemData item, int count)
    {
        this.item = item;
        this.count = count;
    }   
}
public class PortionQuickSlotData : SlotDatanitialize
{
    public List<SlotData> SlotDatas;
    public PortionQuickSlotData()
    {
        InitializeSlots(SlotDatas, UserDataConstants.QUICKPORTIONSLOTSDATA_COUNT);
    }
}
public class QuickSkillSlotData : SlotDatanitialize
{
    public List<SlotData> SlotDatas;
    public QuickSkillSlotData()
    {
        InitializeSlots(SlotDatas, UserDataConstants.QUICKSKILLSLOTDATA_COUNT);
    }
}
public class InventoryData : SlotDatanitialize
{
    public List<SlotData> SlotDatas;
    public InventoryData()
    {
        InitializeSlots(SlotDatas, 28);
    }
}
public class SkillInventoryData : SlotDatanitialize
{
    public List<SlotData> SlotDatas;
    public SkillInventoryData()
    {
        InitializeSlots(SlotDatas, 8);
    }
}
public class EquipmentData : SlotDatanitialize
{
    public List<SlotData> SlotDatas;
    public EquipmentData()
    {
        InitializeSlots(SlotDatas, 2);
    }
}
public class ShopData : SlotDatanitialize
{
    public List<SlotData> SlotDatas;
    public ShopData()
    {
        InitializeSlots(SlotDatas, 8);
    }
}