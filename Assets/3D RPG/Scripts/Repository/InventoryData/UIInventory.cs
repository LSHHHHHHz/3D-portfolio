using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIInventory : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    public Transform inventorySlotGrid;
    public PlayerDataSave playerData;
    public DragAndDropData dragAndDropData;
    public void SetData(PlayerDataSave data, DragAndDropData dragdropData)
    {
        playerData = data;
        dragAndDropData = dragdropData;
        Refresh(); 
    }
    public virtual void Refresh()
    {
        if (playerData != null && playerData.equipmentData != null)
        {
            foreach (var item in playerData.equipmentData.equipmentItems)
            {
                // ���� ���������κ��� �� ���� �ν��Ͻ� ����
                GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotGrid);
                // item ������ �������� ������ ����
                // slot.GetComponent<ItemSlot>().Setup(item);
            }
        }
        if (playerData != null && playerData.skillInventoryData != null)
        {
            foreach (var item in playerData.skillInventoryData.skills)
            {
                // ���� ���������κ��� �� ���� �ν��Ͻ� ����
                GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotGrid);
                // item ������ �������� ������ ����
                // slot.GetComponent<SkillSlot>().Setup(item);
            }
        }
        if (playerData != null && playerData.ingameSkillData != null)
        {
            foreach (var item in playerData.ingameSkillData.ingameSkills)
            {
                // ���� ���������κ��� �� ���� �ν��Ͻ� ����
                GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotGrid);
                // item ������ �������� ������ ����
                // slot.GetComponent<SkillSlot>().Setup(item);
            }
        }
    }
}


