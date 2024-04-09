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
        ItemInstance dragitem = dragData.GetData() as ItemInstance;
        ItemInstance dropitem = dropData.GetData() as ItemInstance;
        SkillInstance dragSkill= dragData.GetData() as SkillInstance;
        SkillInstance dropSkill = dropData.GetData() as SkillInstance;
        if (dropInventoryType == InventoryType.SkillInventory)
        {
            return;
        }
        if (dropInventoryType == InventoryType.IngameSkillInventory && (dragData.GetItemType() != InfoType.Skill))
        {
            return;
        }
        if (dropInventoryType == InventoryType.IngameSkillInventory)
        {
            if(dragInventoryType== InventoryType.IngameSkillInventory)
            {
                return;
            }
            dropData.SetData(dragSkill);
            ClearData();
            GameManager.instance.SetSkillI();
            return;
        }

        if (dropInventoryType == InventoryType.ItemEquipInventory && (dragData.GetItemType() != InfoType.Sword && dragData.GetItemType() != InfoType.Shield))
        {
            return;
        }
        if (dropInventoryType == InventoryType.ItemEquipInventory && (dragData.GetItemType() == InfoType.Sword || dragData.GetItemType() == InfoType.Shield))
        {
            dropData.SetData(dragitem);
            if(dragitem.count>1)
            {
                dropitem.count = 1;
                dragitem.count--;
            }
            if(dragitem.count<=1)
            {
                dragData.ClearData();
            }
            GameManager.instance.ChangeSlot();
            ClearData();
            return;
        }

        if (dropInventoryType == InventoryType.InGamePortionInventory && (dragData.GetItemType() != InfoType.HPPortion && dragData.GetItemType() != InfoType.MPPortion))
        {
            return;
        }

        if (dragitem != null && dropitem != null && dragitem.itemInfo == dropitem.itemInfo)
        {
            dropitem.count += dragitem.count;
            dragData.ClearData();
            GameManager.instance.ChangeSlot();
            ClearData();
            return;
        }

        if (dragData == dropData)
        {
            return;
        }
        
        if(dropData.GetData() == null)
        {
            dropData.SetData(dragData.GetData());
            dragData.ClearData();
        }
        else
        {
            ItemInstance temp = dropData.GetData() as ItemInstance;
            dropData.SetData(dragData.GetData());
            dragData.SetData(temp);
        }
        ClearData();
        GameManager.instance.ChangeSlot();
    }
    public void ClearData()
    {
        if (dropData != null)
        {
            dropData = null;
            dropInventoryType = InventoryType.None;
        }
        if(dragData != null)
        {
            dragData = null;
            dragInventoryType= InventoryType.None;
        }
    }
}
