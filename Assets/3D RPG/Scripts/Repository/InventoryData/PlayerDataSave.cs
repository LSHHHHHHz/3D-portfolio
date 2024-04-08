using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerDataSave
{
    public static PlayerDataSave instance;

    public InventoryData inventoryData = new InventoryData();
    public EquipmentData equipmentData = new EquipmentData();
    public IngamePortionData ingamePortionData = new IngamePortionData();
    public SkillInventoryData skillInventoryData = new SkillInventoryData();
    public IngameSkillData ingameSkillData = new IngameSkillData();

    public void Save()
    {

    }

    public static void Load()
    {
        if (instance == null)
        {
            instance = new PlayerDataSave();
        }
    }
}
public interface IInventoryData
{
    void AddItem(ItemInstance item);
    void SetItem(ItemInstance item, int index);
    void RemoveItem(int index);
    void MergeItem(int dragIndex, int dropIndex);
    void MergeOtherInventoryItem(ItemInstance item, int dropIndex);
    void ChangeItem(int dragIndex, int dropIndex);
    ItemInstance GetItem(int index);

}
public class InventoryData : IInventoryData
{
    // 어느 슬롯에 어느 아이템 들어갔는지 그런 정보도 들어가야함
    public List<ItemInstance> items;
    public int maxSlots = 28;
    public ItemInstance selectedItem;
    public InventoryData()
    {
        items = new List<ItemInstance>();
        InitializeSlotLength();
    }
    private void InitializeSlotLength()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            items.Add(null);
        }
    }
    public void AddItem(ItemInstance item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                return;
            }
        }
        items.Add(item);

        if (items.Count > maxSlots)
        {
            Debug.Log("슬롯 부족");
        }
    }
    public void SetItem(ItemInstance item, int index)
    {
        items[index] = item;
    }
    public void ExpandInventory(int additionalSlots)
    {
        maxSlots += additionalSlots;
    }
    public void RemoveItem(int index)
    {
        items[index] = null;
    }
    public void MergeItem(int dragIndex, int dropIndex)
    {
        GetItem(dropIndex).count += GetItem(dragIndex).count;
        RemoveItem(dragIndex);
    }
    public void MergeOtherInventoryItem(ItemInstance item, int dropIndex)
    {
        GetItem(dropIndex).count += item.count;
    }
    public void ChangeItem(int dragIndex, int dropIndex)
    {
        if (dragIndex >= 0 && dragIndex < items.Count && dropIndex >= 0 && dropIndex < items.Count)
        {
            ItemInstance temp = items[dragIndex];
            items[dragIndex] = items[dropIndex];
            items[dropIndex] = temp;
        }
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

public class EquipmentData : IInventoryData
{
    // 플레이어가 착용하고 있는 아이템 정보 관리
    public List<ItemInstance> equipmentItems;
    public int maxSlots = 2;
    public ItemInstance selectedItem;
    public EquipmentData()
    {
        equipmentItems = new List<ItemInstance>();
    }
    public void AddItem(ItemInstance item)
    {
        if (equipmentItems.Count < maxSlots)
        {
            for (int i = 0; i < equipmentItems.Count; i++)
            {
                if (equipmentItems[i] == null)
                {
                    equipmentItems[i] = item;
                    return;
                }
            }
            equipmentItems.Add(item);
        }
    }
    public void SetItem(ItemInstance item, int index)
    {
        equipmentItems[index] = item;
    }
    public void RemoveItem(int index)
    {
        equipmentItems[index] = null;
    }
    public void MergeItem(int dragIndex, int dropIndex)
    {
        GetItem(dropIndex).count += GetItem(dragIndex).count;
        RemoveItem(dragIndex);
    }
    public void MergeOtherInventoryItem(ItemInstance item, int dropIndex)
    {
        GetItem(dropIndex).count += item.count;
    }
    public void ChangeItem(int dragIndex, int dropIndex)
    {
        if (dragIndex >= 0 && dragIndex < equipmentItems.Count && dropIndex >= 0 && dropIndex < equipmentItems.Count)
        {
            ItemInstance temp = equipmentItems[dragIndex];
            equipmentItems[dragIndex] = equipmentItems[dropIndex];
            equipmentItems[dropIndex] = temp;
        }
    }
    public ItemInstance GetItem(int index)
    {
        if (index >= 0 && index < equipmentItems.Count)
        {
            return equipmentItems[index];
        }
        return null;
    }
}
public class IngamePortionData : IInventoryData
{
    public List<ItemInstance> ingamePortionItems;
    public int maxSlots = 2;
    public ItemInstance selectedItem;
    public IngamePortionData()
    {
        ingamePortionItems = new List<ItemInstance>(maxSlots);
        InitializeListWithNulls();
    }
    private void InitializeListWithNulls()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            ingamePortionItems.Add(null);
        }
    }
    public void AddItem(ItemInstance item)
    {
        if (ingamePortionItems.Count < maxSlots)
        {
            for (int i = 0; i < ingamePortionItems.Count; i++)
            {
                if (ingamePortionItems[i] == null)
                {
                    ingamePortionItems[i] = item;
                    return;
                }
            }
            ingamePortionItems.Add(item);
        }
    }
    public void SetItem(ItemInstance item, int index)
    {
        ingamePortionItems[index] = item;
    }
    public void RemoveItem(int index)
    {
        ingamePortionItems[index] = null;
    }
    public void MergeItem(int dragIndex, int dropIndex)
    {
        GetItem(dropIndex).count += GetItem(dragIndex).count;
        RemoveItem(dragIndex);
    }
    public void MergeOtherInventoryItem(ItemInstance item, int dropIndex)
    {
        GetItem(dropIndex).count += item.count;
    }
    public void ChangeItem(int dragIndex, int dropIndex)
    {
        if (dragIndex >= 0 && dragIndex < ingamePortionItems.Count && dropIndex >= 0 && dropIndex < ingamePortionItems.Count)
        {
            ItemInstance temp = ingamePortionItems[dragIndex];
            ingamePortionItems[dragIndex] = ingamePortionItems[dropIndex];
            ingamePortionItems[dropIndex] = temp;
        }
    }
    public void ChangeOtherInventoryItem(int dragIndex, int dropIndex)
    {
        if (dragIndex >= 0 && dragIndex < ingamePortionItems.Count && dropIndex >= 0 && dropIndex < ingamePortionItems.Count)
        {
            ItemInstance temp = ingamePortionItems[dragIndex];
            ingamePortionItems[dragIndex] = ingamePortionItems[dropIndex];
            ingamePortionItems[dropIndex] = temp;
        }
    }
    public ItemInstance GetItem(int index)
    {
        if (index >= 0 && index < ingamePortionItems.Count)
        {
            return ingamePortionItems[index];
        }
        return null;
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


