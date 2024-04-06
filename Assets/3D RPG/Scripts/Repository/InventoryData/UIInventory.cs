using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIInventory : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    public Transform inventorySlotGrid;
    public PlayerData playerData; 
    public void SetData(PlayerData data)
    {
        playerData = data;
        Refresh(); 
    }
    public virtual void Refresh()
    {
        foreach (Transform child in inventorySlotGrid)
        {
            Destroy(child.gameObject);
        }
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
        if (playerData != null && playerData.ingamePortionData != null)
        {
            foreach (var item in playerData.ingamePortionData.ingamePortionItems)
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


