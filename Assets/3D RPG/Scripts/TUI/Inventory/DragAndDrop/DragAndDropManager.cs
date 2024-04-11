using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragAndDropManager : MonoBehaviour
{
    public static DragAndDropManager instance;

    public IPlayerData dragData;
    public InventoryType dragInventoryType;
    public IPlayerData dropData;
    public InventoryType dropInventoryType;
    private void Awake()
    {
        instance = this;
    }
    public void SetDataInventorySlot()
    {
        if (!CanDrop())
        {
            return;
        }
        switch (dropInventoryType)
        {
            case InventoryType.IngameSkillInventory:
                SkillInventoryDrop();
                break;
            case InventoryType.ItemEquipInventory:
                EquipInventoryDrop();
                break;
            default:
                EquipInventoryDrop();
                DefaultDrop();
                break;
        }
        FinalizeDrop();
    }
    private bool CanDrop()
    {
        return dropInventoryType != InventoryType.SkillInventory && !(dropInventoryType == InventoryType.IngameSkillInventory && dragData.GetItemType() != InfoType.Skill);
    }

    private void SkillInventoryDrop()
    {
        bool matchFound = false;
        foreach (var skill in PlayerData.instance.playerIngameSkillData)
        {
            if (skill.skill != null)
            {
                if (skill.skill.skillInfo == (dragData.GetData() as SkillInstance).skillInfo)
                {
                    matchFound = true;
                    break;
                }
            }
        }
        if(!matchFound)
        {
            dropData.SetData(dragData.GetData() as SkillInstance);
        }
    }
    private void EquipInventoryDrop()
    {
        ItemInstance dragItem = dragData.GetData() as ItemInstance;
        ItemInstance dropItem = dropData.GetData() as ItemInstance;

        if (dragData.GetItemType() == InfoType.Sword || dragData.GetItemType() == InfoType.Shield)
        {
            if (dropItem == null)
            {
                ItemInstance newItem = new ItemInstance
                {
                    itemInfo = dragItem.itemInfo,
                    count = 1
                };
                dropData.SetData(newItem);              
                if (dragItem.count > 1)
                {
                    dragItem.count -= 1;
                    dragData.SetData(dragItem); 
                }
                else
                {
                    dragData.ClearData(); 
                }
            }
            else
            {
                if(dropItem == dragItem)
                {
                    return;
                }
                ItemInstance newItem = new ItemInstance
                {
                    itemInfo = dragItem.itemInfo,
                    count = 1
                };
                dropData.SetData(newItem);
                if (dragItem.count > 1)
                {
                    dragItem.count -= 1;
                    dragData.SetData(dragItem);
                }
                else
                {
                    dragData.ClearData();
                }
                foreach(var item in PlayerData.instance.playerItemInventoryData)
                {
                    if(item.GetData() == null)
                    {
                        item.SetData(dropItem);
                        break;
                    }
                }
            }
            GameManager.instance.ChangeSlot();
        }
    }
    private void DefaultDrop()
    {
        ItemInstance dragitem = dragData.GetData() as ItemInstance;
        ItemInstance dropitem = dropData.GetData() as ItemInstance;

        if (dragData == dropData || dragitem == null) return;

        if (dropitem == null || dragitem.itemInfo != dropitem.itemInfo)
        {
            TransferOrSwapItem(dragitem);
        }
        else
        {
            MergeItems(dragitem, dropitem);
        }
    }
    private void TransferOrSwapItem(ItemInstance dragitem)
    {
        if (dropData.GetData() == null)
        {
            dropData.SetData(dragitem);
            dragData.ClearData();
        }
        else
        {
            ItemInstance temp = dropData.GetData() as ItemInstance;
            dropData.SetData(dragitem);
            dragData.SetData(temp);
        }
    }
    private void MergeItems(ItemInstance dragitem, ItemInstance dropitem)
    {
        dropitem.count += dragitem.count;
        dragData.ClearData();
    }
    private void FinalizeDrop()
    {
        if ((dropInventoryType == InventoryType.IngameSkillInventory) || (dropInventoryType == InventoryType.SkillInventory))
        {
            GameManager.instance.SetSkillI();
            ClearData();
        }
        else
        {
            GameManager.instance.ChangeSlot();
        }
        ClearData();
    }
    public void ClearData()
    {
        dropData = null;
        dropInventoryType = InventoryType.None;
        dragData = null;
        dragInventoryType = InventoryType.None;
    }
}
