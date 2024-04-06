using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static PlayerData instance = new PlayerData();

    public InventoryData inventoryData;
    public EquipmentData equipmentData;
    public IngamePortionData ingamePortionData;
    public SkillInventoryData skillInventoryData;
    public IngameSkillData ingameSkillData;

    public void Save()
    {

    }

    public static PlayerData Load()
    {
        return new PlayerData();
    }

}
public class InventoryData
{
    // 어느 슬롯에 어느 아이템 들어갔는지 그런 정보도 들어가야함
    public List<ItemInstance> items;
    public ItemInstance firstSelectInventoryItem;
    public InventoryData()
    {
        items = new List<ItemInstance>();
    }
    public void AddItem(ItemInstance item)
    {
        items.Add(item);
    }
    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
    }
    public void SelectItem(int index)
    {
        firstSelectInventoryItem = GetItem(index);
    }
    public ItemInstance GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null; 
    }
}

public class EquipmentData
{
    // 플레이어가 착용하고 있는 아이템 정보 관리
    public List<ItemInstance> equipmentItems;
    public EquipmentData()
    {
        equipmentItems = new List<ItemInstance>();
    }
    public void AddItem(ItemInstance item)
    {
        equipmentItems.Add(item);
    }
    public void RemoveItem(ItemInstance item)
    {
        equipmentItems.Remove(item);
    }
    public ItemInstance GetItem(int index)
    {
        if (index >= 0 && index < equipmentItems.Count)
        {
            return equipmentItems[index];
        }
        return null;
    }
    public List<ItemInstance> GetAllItems()
    {
        return new List<ItemInstance>(equipmentItems);
    }
}
public class IngamePortionData
{
    public List<ItemInstance> ingamePortionItems;
    public IngamePortionData()
    {
        ingamePortionItems = new List<ItemInstance>();
    }
    public void AddItem(ItemInstance item)
    {
        ingamePortionItems.Add(item);
    }
    public void RemoveItem(ItemInstance item)
    {
        ingamePortionItems.Remove(item);
    }
    public ItemInstance GetItem(int index)
    {
        if (index >= 0 && index < ingamePortionItems.Count)
        {
            return ingamePortionItems[index];
        }
        return null;
    }
    public List<ItemInstance> GetAllItems()
    {
        return new List<ItemInstance>(ingamePortionItems);
    }
}

public class SkillInventoryData
{
    public List<SkillInstance> skills;
    public void AddSkill()
    {

    }
    public void RemoveSkill()
    {

    }
    public void Clear()
    {

    }
}
public class IngameSkillData
{
    public List<SkillInstance> ingameSkills;
    public void AddSkill()
    {

    }
    public void RemoveSkill()
    {

    }
    public void Clear()
    {

    }
}


