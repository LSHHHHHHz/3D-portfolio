using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IngameSkillInventory : SkillInventoryBase
{
    private void Awake()
    {
        SetData(PlayerData.instance);
    }
    private void OnEnable()
    {
        GameManager.instance.SetSkillInventory += RefreshData;
    }
    private void OnDisable()
    {
        GameManager.instance.SetSkillInventory -= RefreshData;
    }
    private void RefreshData()
    {
        Refresh();
    }
    public override void SetData(PlayerData data)
    {
        playerData = data;
        playerSkillData = GetSkillInventoryDataByType(inventoryType);
        skillInventorySlots = GetChildSkillSlots(inventorySlotGrid);
        InitializeSkillInventoryData(inventoryType, skillInventorySlots.Length);
        Refresh();
    }
    public override SkillSlot[] GetChildSkillSlots(RectTransform parent)
    {
        List<SkillSlot> slots = new List<SkillSlot>();
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform imageTransform = parent.GetChild(i);
            SkillSlot slot = imageTransform.GetComponentInChildren<SkillSlot>();
            if (slot != null)
            {
                int slotIndex = i;
                Button slotButton = slot.GetComponent<Button>();
                slot.beingDragSlot += () => SelectDragData(slotIndex);
                slot.OnDropSlot += () => SelectDropData(slotIndex);
                slotButton.onClick.AddListener(() =>
                {
                    Debug.Log("스킬 공격");
                });
            }
            slots.Add(slot);
        }
        return slots.ToArray();
    }
    public void SelectDragData(int num)
    {
        DragAndDropManager.instance.dragData = playerSkillData[num];
        DragAndDropManager.instance.dragInventoryType = InventoryType.IngameSkillInventory;
    }
    public void SelectDropData(int num)
    {
        DragAndDropManager.instance.dropData = playerSkillData[num];
        DragAndDropManager.instance.dropInventoryType = InventoryType.IngameSkillInventory;
    }
}


