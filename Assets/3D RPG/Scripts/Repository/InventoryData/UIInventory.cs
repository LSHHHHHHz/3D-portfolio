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
                // 슬롯 프리팹으로부터 새 슬롯 인스턴스 생성
                GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotGrid);
                // item 정보를 바탕으로 슬롯을 구성
                // slot.GetComponent<ItemSlot>().Setup(item);
            }
        }
        if (playerData != null && playerData.ingamePortionData != null)
        {
            foreach (var item in playerData.ingamePortionData.ingamePortionItems)
            {
                // 슬롯 프리팹으로부터 새 슬롯 인스턴스 생성
                GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotGrid);
                // item 정보를 바탕으로 슬롯을 구성
                // slot.GetComponent<ItemSlot>().Setup(item);
            }
        }
        if (playerData != null && playerData.skillInventoryData != null)
        {
            foreach (var item in playerData.skillInventoryData.skills)
            {
                // 슬롯 프리팹으로부터 새 슬롯 인스턴스 생성
                GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotGrid);
                // item 정보를 바탕으로 슬롯을 구성
                // slot.GetComponent<SkillSlot>().Setup(item);
            }
        }
        if (playerData != null && playerData.ingameSkillData != null)
        {
            foreach (var item in playerData.ingameSkillData.ingameSkills)
            {
                // 슬롯 프리팹으로부터 새 슬롯 인스턴스 생성
                GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotGrid);
                // item 정보를 바탕으로 슬롯을 구성
                // slot.GetComponent<SkillSlot>().Setup(item);
            }
        }
    }
}


