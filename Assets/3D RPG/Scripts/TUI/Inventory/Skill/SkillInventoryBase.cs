using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class SkillInventoryBase : MonoBehaviour
{
    public PlayerData playerData;
    public IPlayerData[] playerSkillData;
    public InventoryType inventoryType;

    public RectTransform inventorySlotGrid;
    public SkillSlot[] skillInventorySlots;

    public abstract void SetData(PlayerData data);
    public abstract SkillSlot[] GetChildSkillSlots(RectTransform parent);
    public void Refresh()
    {
        if (playerData != null && playerSkillData != null)
        {
            for (int i = 0; i < skillInventorySlots.Length; i++)
            {
                SkillSlot slot = skillInventorySlots[i].GetComponentInChildren<SkillSlot>();
                slot.skillSlotNumber = i;
                if (i < playerSkillData.Length)
                {
                    if (slot != null)
                    {
                        slot.GetComponent<SkillSlot>().ClearData();
                        if (playerSkillData[i].GetData() != null)
                        {
                            var skillinstance = playerSkillData[i].GetData() as SkillInstance;
                            slot.GetComponent<SkillSlot>().SetData(skillinstance.skillInfo);
                            slot.GetComponent<SkillSlot>().SetData(skillinstance);

                        }
                    }
                }
            }
        }
    }
    public void InitializeSkillInventoryData(InventoryType type , int inventorySize)
    {
        switch (type)
        {
            case InventoryType.SkillInventory:
                for (int i = 0; i < inventorySize; i++)
                {
                    playerSkillData[i] = new PlayerSkillInventoryData();
                }
                break;
            case InventoryType.IngameSkillInventory:
                for (int i = 0; i < inventorySize; i++)
                {
                    playerSkillData[i] = new PlayerIngameSkillData();
                }
                break;
            default:
                Debug.LogError("없는 타입");
                break;
        }
    }

    public PlayerSkillData[] GetSkillInventoryDataByType(InventoryType type)
    {
        switch (type)
        {
            case InventoryType.SkillInventory:
                return playerData.playerSkillInventoryData;
            case InventoryType.IngameSkillInventory:
                return playerData.playerIngameSkillData;
            default:
                Debug.LogError("없는 타입");
                return null;
        }
    }

  
}


