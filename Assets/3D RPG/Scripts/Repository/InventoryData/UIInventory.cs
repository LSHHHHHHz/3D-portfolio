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


